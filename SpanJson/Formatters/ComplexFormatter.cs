using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter
    {
        /// <summary>
        ///     if the propertyType is object, we need to do it during runtime
        ///     if the type is RuntimeHelpers.IsReferenceOrContainsReferences -> false, we can do everything statically (struct and
        ///     struct children case)
        ///     if the type is sealed or struct, then the type during generation is the type we can use for static lookup
        ///     else we need to do runtime lookup
        /// </summary>
        protected static SerializeDelegate<T, TResolver> BuildSerializeDelegate<T, TResolver>()
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            var resolver = StandardResolvers.GetResolver<TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>().Where(a => a.CanRead).ToList();
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");

            var expressions = new List<Expression>();
            var propertyNameWriterMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteName));
            var seperatorWriteMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteValueSeparator));
            expressions.Add(Expression.Call(writerParameter,
                FindMethod(writerParameter.Type, nameof(JsonWriter.WriteObjectStart))));
            var isNotReferenceOrContainsReference = !RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            var writeSeperator = Expression.Variable(typeof(bool), "writeSeperator");
            for (var i = 0; i < memberInfos.Count; i++)
            {
                var memberInfo = memberInfos[i];
                Expression formatterExpression;
                MethodInfo serializeMethodInfo;
                if (isNotReferenceOrContainsReference || IsSealedOrStruct(memberInfo.MemberType))
                {
                    var formatter = resolver.GetFormatter(memberInfo.MemberType);
                    formatterExpression = Expression.Constant(formatter);
                    serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
                }
                else
                {
                    var formatter = RuntimeFormatter<TResolver>.Default;
                    formatterExpression = Expression.Constant(formatter);
                    serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
                }

                var valueExpressions = new List<Expression>();
                // we need to add the separator, but only if a value was written before
                // we reset the indicator after each seperator write and set it after writing each field
                // todo find better way, it's not really fast at runtime
                if (i > 0)
                {
                    valueExpressions.Add(
                        Expression.IfThen(
                            writeSeperator,
                            Expression.Block(
                                Expression.Call(writerParameter, seperatorWriteMethodInfo),
                                Expression.Assign(writeSeperator, Expression.Constant(false)))
                        ));
                }

                valueExpressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo,
                    Expression.Constant(memberInfo.Name)));
                valueExpressions.Add(Expression.Call(formatterExpression,
                    serializeMethodInfo, writerParameter,
                    Expression.PropertyOrField(valueParameter, memberInfo.MemberName)));
                valueExpressions.Add(Expression.Assign(writeSeperator, Expression.Constant(true)));
                Expression testNullExpression = null;
                if (memberInfo.ExcludeNull)
                {
                    if (memberInfo.MemberType.IsClass)
                    {
                        testNullExpression = Expression.ReferenceNotEqual(
                            Expression.PropertyOrField(valueParameter, memberInfo.MemberName),
                            Expression.Constant(null));
                    }
                    else if (memberInfo.MemberType.IsValueType && Nullable.GetUnderlyingType(memberInfo.MemberType) != null) // nullable value type
                    {
                        testNullExpression = Expression.IsTrue(
                            Expression.Property(Expression.PropertyOrField(valueParameter, memberInfo.MemberName), "HasValue"));
                    }
                }

                var shouldSerializeExpression = memberInfo.ShouldSerialize != null
                    ? Expression.IsTrue(Expression.Call(valueParameter, memberInfo.ShouldSerialize))
                    : null;
                Expression testExpression = null;
                if (testNullExpression != null && shouldSerializeExpression != null)
                {
                    testExpression = Expression.AndAlso(testNullExpression, shouldSerializeExpression);
                }
                else if (testNullExpression != null)
                {
                    testExpression = testNullExpression;
                }
                else if (shouldSerializeExpression != null)
                {
                    testExpression = shouldSerializeExpression;
                }

                if (testExpression != null)
                {
                    expressions.Add(Expression.IfThen(testExpression, Expression.Block(valueExpressions)));
                }
                else
                {
                    expressions.AddRange(valueExpressions);
                }
            }

            expressions.Add(Expression.Call(writerParameter,
                FindMethod(writerParameter.Type, nameof(JsonWriter.WriteObjectEnd))));
            var blockExpression = Expression.Block(new ParameterExpression[] { writeSeperator }, expressions);
            var lambda =
                Expression.Lambda<SerializeDelegate<T, TResolver>>(blockExpression, writerParameter, valueParameter);
            return lambda.Compile();
        }

        protected static int EstimateSize<T>()
        {
            var queue = new Queue<Type>();
            var alreadyseen = new HashSet<Type>();
            queue.Enqueue(typeof(T));
            var result = 0;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (alreadyseen.Add(current))
                {
                    var propertyInfos = current.GetProperties();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        result += propertyInfo.Name.Length + 3; // two quotes + :
                        result += 20; // find better estimation
                        if (propertyInfo.PropertyType.IsClass)
                        {
                            result += 4;
                            queue.Enqueue(propertyInfo.PropertyType);
                        }
                    }
                }
            }

            return result;
        }

        protected static DeserializeDelegate<T, TResolver> BuildDeserializeDelegate<T, TResolver>()
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            var resolver = StandardResolvers.GetResolver<TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>().Where(a => a.CanWrite).ToList();
            if (memberInfos.Count == 0)
            {
                return (ref JsonReader parser) => default;
            }

            var readerParameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");
            var returnValue = Expression.Variable(typeof(T), "result");
            var switchValue = Expression.Variable(typeof(ReadOnlySpan<char>), "switchValue");
            var switchValueAssignExpression = Expression.Assign(switchValue,
                Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.ReadNameSpan))));
            var switchExpression = Expression.Block(new[] { switchValue }, switchValueAssignExpression,
                BuildPropertyComparisonSwitchExpression(resolver, memberInfos, null, 0, switchValue, returnValue, readerParameter));
            var countExpression = Expression.Parameter(typeof(int), "count");
            var abortExpression = Expression.IsTrue(Expression.Call(readerParameter,
                readerParameter.Type.GetMethod(nameof(JsonReader.TryReadIsEndObjectOrValueSeparator)),
                countExpression));
            var readBeginObject = Expression.Call(readerParameter,
                FindMethod(readerParameter.Type, nameof(JsonReader.ReadBeginObjectOrThrow)));
            var loopAbort = Expression.Label(typeof(void));
            var returnTarget = Expression.Label(returnValue.Type);
            var block = Expression.Block(new[] { returnValue, countExpression }, readBeginObject,
                Expression.Assign(returnValue, Expression.New(returnValue.Type)),
                Expression.Loop(
                    Expression.IfThenElse(abortExpression, Expression.Break(loopAbort),
                        switchExpression), loopAbort
                ),
                Expression.Label(returnTarget, returnValue)
            );

            var lambda = Expression.Lambda<DeserializeDelegate<T, TResolver>>(block, readerParameter);
            return lambda.Compile();
        }

        private static bool IsSealedOrStruct(Type type)
        {
            return type.IsValueType || type.IsSealed;
        }

        private static MethodInfo FindMethod(Type type, string name)
        {
            return type.GetMethod(name);
        }

        /// <summary>
        ///     We group the field names by the nth character and nest the switch tables to find the appropriate field/property to
        ///     assign to
        /// </summary>
        private static Expression BuildPropertyComparisonSwitchExpression<TResolver>(TResolver resolver, ICollection<JsonMemberInfo> memberInfos, string prefix, int index,
            ParameterExpression switchValue,
            Expression returnValue, Expression readerParameter) where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            var group = memberInfos.Where(a => (prefix == null || a.Name.StartsWith(prefix)) && a.Name.Length > index).GroupBy(a => a.Name[index]).ToList();
            if (!group.Any())
            {
                return null;
            }

            var cases = new List<SwitchCase>();
            var equalityMethod =
                typeof(ComplexFormatter).GetMethod(nameof(StringEquals), BindingFlags.NonPublic | BindingFlags.Static);
            var defaultValue = Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.SkipNextSegment)));
            foreach (var groupedMemberInfos in group)
            {
                var memberInfosPerChar = groupedMemberInfos.Count();
                if (memberInfosPerChar == 1) // only one hit, we compare the remaining name and and assign the field if true
                {
                    var memberInfo = groupedMemberInfos.Single();
                    var formatter = resolver.GetFormatter(memberInfo.MemberType);
                    var checkLength = (prefix?.Length ?? 0) + 1;
                    var testExpression = Expression.Call(equalityMethod, switchValue, Expression.Constant(checkLength),
                        Expression.Constant(memberInfo.Name.Substring(checkLength)));
                    var matchExpression = Expression.Assign(Expression.PropertyOrField(returnValue, memberInfo.MemberName),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"), readerParameter));
                    var switchCase =
                        Expression.SwitchCase(
                            Expression.IfThenElse(testExpression, matchExpression, defaultValue),
                            Expression.Constant(groupedMemberInfos.Key));
                    cases.Add(switchCase);
                }
                else // Either we have found an exact match for the name or we need to build a further level of nested switch tables
                {
                    var nextPrefix = prefix + groupedMemberInfos.Key;
                    var nextSwitch =
                        BuildPropertyComparisonSwitchExpression(resolver, memberInfos, nextPrefix, index + 1, switchValue, returnValue, readerParameter);
                    var exactMatch = groupedMemberInfos.SingleOrDefault(a => a.Name == nextPrefix);
                    Expression directMatchExpression = null;
                    if (exactMatch != null)
                    {
                        var formatter = resolver.GetFormatter(exactMatch.MemberType);
                        var matchExpression = Expression.Assign(Expression.PropertyOrField(returnValue, exactMatch.MemberName),
                            Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"), readerParameter));
                        var testExpression = Expression.Equal(Expression.Property(switchValue, "Length"), Expression.Constant(nextPrefix.Length));
                        directMatchExpression = Expression.IfThenElse(testExpression, matchExpression, nextSwitch);
                    }

                    var switchCase =
                        Expression.SwitchCase(directMatchExpression ?? nextSwitch,
                            Expression.Constant(groupedMemberInfos.Key));
                    cases.Add(switchCase);
                }
            }

            var indexedSwitchValue = Expression.Call(typeof(ComplexFormatter).GetMethod(nameof(GetChar), BindingFlags.NonPublic | BindingFlags.Static),
                switchValue, Expression.Constant(index));
            var switchExpression = Expression.Switch(typeof(void), indexedSwitchValue, defaultValue, null, cases.ToArray());
            return switchExpression;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool StringEquals(ReadOnlySpan<char> span, int offset, string comparison)
        {
            return span.Slice(offset).SequenceEqual(comparison.AsSpan());
        }

        /// <summary>
        ///     Couldn't get it working with Expression Trees,ref return lvalues do not work yet
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static char GetChar(in ReadOnlySpan<char> span, int index)
        {
            return span[index];
        }

        protected delegate T DeserializeDelegate<out T, in TResolver>(ref JsonReader reader)
            where TResolver : IJsonFormatterResolver<TResolver>, new();


        protected delegate void SerializeDelegate<in T, in TResolver>(ref JsonWriter writer, T value)
            where TResolver : IJsonFormatterResolver<TResolver>, new();
    }
}