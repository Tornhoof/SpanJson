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
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");

            var expressions = new List<Expression>();
            var propertyNameWriterMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteName));
            var seperatorWriteMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteValueSeparator));
            expressions.Add(Expression.Call(writerParameter,
                FindMethod(writerParameter.Type, nameof(JsonWriter.WriteObjectStart))));
            var isNotReferenceOrContainsReference = !RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            var resolver = StandardResolvers.GetResolver<TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>();
            for (var i = 0; i < memberInfos.Length; i++)
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

                var valueExpressions = new List<Expression>
                {
                    Expression.Call(writerParameter, propertyNameWriterMethodInfo,
                        Expression.Constant(memberInfo.Name)),
                    Expression.Call(formatterExpression,
                        serializeMethodInfo, writerParameter,
                        Expression.PropertyOrField(valueParameter, memberInfo.MemberName))
                };
                if (i != memberInfos.Length - 1)
                {
                    valueExpressions.Add(Expression.Call(writerParameter, seperatorWriteMethodInfo));
                }

                var testNullExpression = memberInfo.ExcludeNull
                    ? Expression.ReferenceNotEqual(
                        Expression.PropertyOrField(valueParameter, memberInfo.MemberName),
                        Expression.Constant(null))
                    : null;
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
            var blockExpression = Expression.Block(expressions);
            var lambda =
                Expression.Lambda<SerializeDelegate<T, TResolver>>(blockExpression, writerParameter, valueParameter);
            return lambda.Compile();
        }

        private static bool IsSealedOrStruct(Type type)
        {
            return type.IsValueType || type.IsSealed;
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

        private static MethodInfo FindMethod(Type type, string name)
        {
            return type.GetMethod(name);
        }


        protected static DeserializeDelegate<T, TResolver> BuildDeserializeDelegate<T, TResolver>()
            where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");

            var resolver = StandardResolvers.GetResolver<TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>();
            var cases = new List<SwitchCase>();
            var returnValue = Expression.Variable(typeof(T), "result");
            foreach (var memberInfo in memberInfos)
            {
                var formatter = resolver.GetFormatter(memberInfo.MemberType);
                var switchCase = Expression.SwitchCase(
                    Expression.Assign(Expression.PropertyOrField(returnValue, memberInfo.MemberName),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"),
                            readerParameter)),
                    Expression.Constant(memberInfo.Name));
                cases.Add(switchCase);
            }

            var equalityMethod =
                typeof(ComplexFormatter).GetMethod(nameof(IsEqual), BindingFlags.NonPublic | BindingFlags.Static);
            var switchExpression = Expression.Switch(typeof(void),
                Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.ReadNameSpan))), Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.ReadNextSegment))),
                equalityMethod, cases.ToArray());
            var countExpression = Expression.Parameter(typeof(int), "count");
            var abortExpression = Expression.IsTrue(Expression.Call(readerParameter,
                readerParameter.Type.GetMethod(nameof(JsonReader.TryReadIsEndObjectOrValueSeparator)),
                countExpression));
            var readBeginObject = Expression.Call(readerParameter,
                FindMethod(readerParameter.Type, nameof(JsonReader.ReadBeginObjectOrThrow)));
            var loopAbort = Expression.Label(typeof(void));
            var returnTarget = Expression.Label(returnValue.Type);
            var block = Expression.Block(new[] {returnValue, countExpression}, readBeginObject,
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEqual(ReadOnlySpan<char> span, string comparison)
        {
            //return span.Equals(comparison.AsSpan(), StringComparison.Ordinal);
            return span.SequenceEqual(comparison.AsSpan());
        }

        protected delegate void SerializeDelegate<in T, in TResolver>(ref JsonWriter writer, T value)
            where TResolver : IJsonFormatterResolver<TResolver>, new();

        protected delegate T DeserializeDelegate<out T, in TResolver>(ref JsonReader reader)
            where TResolver : IJsonFormatterResolver<TResolver>, new();
    }
}