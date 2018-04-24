using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter : BaseFormatter
    {
        /// <summary>
        ///     if the propertyType is object, we need to do it during runtime
        ///     if the type is RuntimeHelpers.IsReferenceOrContainsReferences -> false, we can do everything statically (struct and
        ///     struct children case)
        ///     if the type is sealed or struct, then the type during generation is the type we can use for static lookup
        ///     else we need to do runtime lookup
        /// </summary>
        protected static SerializeDelegate<T, TSymbol, TResolver> BuildSerializeDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>().Where(a => a.CanRead).ToList();
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");

            var expressions = new List<Expression>();
            MethodInfo propertyNameWriterMethodInfo;
            MethodInfo seperatorWriteMethodInfo;
            MethodInfo writeBeginObjectMethodInfo;
            MethodInfo writeEndObjectMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                propertyNameWriterMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim));
                seperatorWriteMethodInfo =  FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16ValueSeparator));
                writeBeginObjectMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16BeginObject));
                writeEndObjectMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16EndObject));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                propertyNameWriterMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim));
                seperatorWriteMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8ValueSeparator));
                writeBeginObjectMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8BeginObject));
                writeEndObjectMethodInfo = FindMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8EndObject));
            }
            else
            {
                throw new NotSupportedException();
            }
            expressions.Add(Expression.Call(writerParameter, writeBeginObjectMethodInfo));
            var isReferenceOrContainsReference = RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            var writeSeperator = Expression.Variable(typeof(bool), "writeSeperator");
            for (var i = 0; i < memberInfos.Count; i++)
            {
                var memberInfo = memberInfos[i];
                Expression runtimeFormatterExpression = null;
                Expression runtimeDecisionExpression = null;
                MethodInfo runtimeSerializeMethodInfo = null;
                var formatter = resolver.GetFormatter(memberInfo.MemberType);
                Expression formatterExpression = Expression.Constant(formatter);
                var serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
                var memberExpression = Expression.PropertyOrField(valueParameter, memberInfo.MemberName);
                if (isReferenceOrContainsReference && !IsSealedOrStruct(memberInfo.MemberType)) // decide at runtime
                {
                    var backupFormatter = RuntimeFormatter<TSymbol, TResolver>.Default;
                    runtimeFormatterExpression = Expression.Constant(backupFormatter);
                    runtimeSerializeMethodInfo = backupFormatter.GetType().GetMethod("Serialize");
                    // switch based on type to the static one or the runtime one, assuming the static one is the normal case
                    var getTypeMethodInfo = typeof(object).GetMethod(nameof(GetType));
                    runtimeDecisionExpression =
                        Expression.Equal(Expression.Call(memberExpression, getTypeMethodInfo), Expression.Constant(memberInfo.MemberType));
                }

                var valueExpressions = new List<Expression>();
                // we need to add the separator, but only if a value was written before
                // we reset the indicator after each seperator write and set it after writing each field
                if (i > 0)
                {
                    valueExpressions.Add(
                        Expression.IfThen(
                            writeSeperator,
                            Expression.Block(
                                Expression.Call(writerParameter, seperatorWriteMethodInfo))
                        ));
                }

                var writerName = $"\"{memberInfo.Name}\":";
                Expression writerNameConstant;
                if (typeof(TSymbol) == typeof(char))
                {
                    writerNameConstant = Expression.Convert(Expression.Constant(writerName), typeof(ReadOnlySpan<char>));
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    writerNameConstant = Expression.Convert(Expression.Constant(Encoding.UTF8.GetBytes(writerName)), typeof(ReadOnlySpan<byte>));
                }
                else
                {
                    throw new NotSupportedException();
                }
                valueExpressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo, writerNameConstant));
                Expression serializerCall = Expression.Call(formatterExpression, serializeMethodInfo, writerParameter, memberExpression);
                if (runtimeDecisionExpression != null) // if we need to decide at runtime we 
                {
                    var backupSerializerCall = Expression.Call(runtimeFormatterExpression,
                        runtimeSerializeMethodInfo, writerParameter, memberExpression);
                    serializerCall = Expression.IfThenElse(runtimeDecisionExpression, serializerCall, backupSerializerCall);
                }

                if (!memberInfo.ExcludeNull)
                {
                    if (memberInfo.MemberType.IsClass)
                    {
                        var writeNullMi = writerParameter.Type.GetMethod(nameof(JsonWriter<TSymbol>.WriteUtf16Null));
                        serializerCall = Expression.IfThenElse(Expression.ReferenceEqual(memberExpression, Expression.Constant(null)),
                            Expression.Call(writerParameter, writeNullMi), serializerCall);
                    }
                }
                valueExpressions.Add(serializerCall);
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

            expressions.Add(Expression.Call(writerParameter, writeEndObjectMethodInfo));
            var blockExpression = Expression.Block(new[] {writeSeperator}, expressions);
            var lambda =
                Expression.Lambda<SerializeDelegate<T, TSymbol, TResolver>>(blockExpression, writerParameter, valueParameter);
            return lambda.Compile();
        }

        protected static DeserializeDelegate<T, TSymbol, TResolver> BuildDeserializeDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var memberInfos = resolver.GetMemberInfos<T>().Where(a => a.CanWrite).ToList();
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            // can't deserialize abstract or interface
            if (memberInfos.Any(a => a.MemberType.IsAbstract || a.MemberType.IsInterface))
            {
                return Expression
                    .Lambda<DeserializeDelegate<T, TSymbol, TResolver>>(Expression.Block(
                            Expression.Throw(Expression.Constant(new NotSupportedException($"{typeof(T).Name} contains abstract or interface members."))),
                            Expression.Default(typeof(T))),
                        readerParameter).Compile();
            }

            if (typeof(T).IsAbstract)
            {
                return Expression.Lambda<DeserializeDelegate<T, TSymbol, TResolver>>(Expression.Default(typeof(T)), readerParameter).Compile();
            }

            if (memberInfos.Count == 0)
            {
                Expression createExpression = null;
                if (typeof(T).IsClass)
                {
                    var ci = typeof(T).GetConstructor(Type.EmptyTypes);
                    if (ci != null)
                    {
                        createExpression = Expression.New(ci);
                    }
                }

                if (createExpression == null)
                {
                    createExpression = Expression.Default(typeof(T));
                }

                return Expression.Lambda<DeserializeDelegate<T, TSymbol, TResolver>>(createExpression, readerParameter).Compile();
            }

            var returnValue = Expression.Variable(typeof(T), "result");
            var switchValue = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "switchValue");
            MethodInfo nameSpanMethodInfo;
            MethodInfo tryReadEndObjectMethodInfo;
            MethodInfo beginObjectOrThrowMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                nameSpanMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16NameSpan));
                tryReadEndObjectMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.TryReadUtf16IsEndObjectOrValueSeparator));
                beginObjectOrThrowMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16BeginObjectOrThrow));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                nameSpanMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8NameSpan));
                tryReadEndObjectMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.TryReadUtf8IsEndObjectOrValueSeparator));
                beginObjectOrThrowMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8BeginObjectOrThrow));
            }
            else
            {
                throw new NotSupportedException();
            }

            var switchValueAssignExpression = Expression.Assign(switchValue, Expression.Call(readerParameter, nameSpanMethodInfo));
            var switchExpression = Expression.Block(new[] {switchValue}, switchValueAssignExpression,
                BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(resolver, memberInfos, null, 0, switchValue, returnValue, readerParameter));
            var countExpression = Expression.Parameter(typeof(int), "count");
            var abortExpression = Expression.IsTrue(Expression.Call(readerParameter, tryReadEndObjectMethodInfo, countExpression));
            var readBeginObject = Expression.Call(readerParameter, beginObjectOrThrowMethodInfo);
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

            var lambda = Expression.Lambda<DeserializeDelegate<T, TSymbol, TResolver>>(block, readerParameter);
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
        private static Expression BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(TResolver resolver, ICollection<JsonMemberInfo> memberInfos, string prefix,
            int index,
            ParameterExpression switchValue, Expression returnValue, Expression readerParameter) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var group = memberInfos.Where(a => (prefix == null || a.Name.StartsWith(prefix)) && a.Name.Length > index).GroupBy(a => a.Name[index]).ToList();
            if (!group.Any())
            {
                return null;
            }
            MethodInfo skipNextMethodInfo;
            MethodInfo equalityMethodInfo;
            Expression indexedSwitchValue;
            if (typeof(TSymbol) == typeof(char))
            {
                skipNextMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf16Segment));
                equalityMethodInfo = typeof(ComplexFormatter).GetMethod(nameof(StringEquals), BindingFlags.NonPublic | BindingFlags.Static);
                indexedSwitchValue = Expression.Call(typeof(ComplexFormatter).GetMethod(nameof(GetChar), BindingFlags.NonPublic | BindingFlags.Static),
                    switchValue, Expression.Constant(index));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                skipNextMethodInfo = FindMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf8Segment));
                equalityMethodInfo = typeof(ComplexFormatter).GetMethod(nameof(ByteEquals), BindingFlags.NonPublic | BindingFlags.Static);
                indexedSwitchValue = Expression.Call(typeof(ComplexFormatter).GetMethod(nameof(GetByte), BindingFlags.NonPublic | BindingFlags.Static),
                    switchValue, Expression.Constant(index));
            }
            else
            {
                throw new NotSupportedException();
            }

            var cases = new List<SwitchCase>();
            var defaultValue = Expression.Call(readerParameter, skipNextMethodInfo);
            // if any group key is not an ascii char we need to compare longer parts of the byte array 
            var extendedComparisonNecessary = typeof(TSymbol) == typeof(byte) && group.Any(a => a.Key > 0xFF);
            var extendedComparsionMethodInfo = extendedComparisonNecessary
                ? typeof(ComplexFormatter).GetMethod(nameof(SwitchByteEquals), BindingFlags.NonPublic | BindingFlags.Static)
                : null;
            foreach (var groupedMemberInfos in group)
            {
                Expression switchKey;
                Expression equalitySwitchValue = switchValue;
                if (typeof(TSymbol) == typeof(char))
                {
                    switchKey = Expression.Constant(groupedMemberInfos.Key);
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    if (extendedComparisonNecessary) // ascii key
                    {
                        var encoded = Encoding.UTF8.GetBytes(new[] {groupedMemberInfos.Key});
                        indexedSwitchValue = Expression.Call(switchValue, "Slice", Type.EmptyTypes, Expression.Constant(index),
                            Expression.Constant(encoded.Length));
                        equalitySwitchValue = Expression.Call(switchValue, "Slice", Type.EmptyTypes, Expression.Constant(encoded.Length)); // For the equality comparison we need to change the switch value
                        switchKey = Expression.Constant(encoded);
                    }
                    else
                    {
                        switchKey = Expression.Constant((byte)groupedMemberInfos.Key);
                    }
                }
                else
                {
                    throw new NotSupportedException();
                }
                var memberInfosPerChar = groupedMemberInfos.Count();
                if (memberInfosPerChar == 1) // only one hit, we compare the remaining name and and assign the field if true
                {
                    var memberInfo = groupedMemberInfos.Single();
                    var formatter = resolver.GetFormatter(memberInfo.MemberType);
                    var checkLength = (prefix?.Length ?? 0) + 1;
                    var equalityCheckStart = extendedComparisonNecessary ? 0 : checkLength;
                    Expression memberInfoConstant;
                    if (typeof(TSymbol) == typeof(char))
                    {
                        memberInfoConstant = Expression.Constant(memberInfo.Name.Substring(checkLength));
                    }
                    else if (typeof(TSymbol) == typeof(byte))
                    {
                        memberInfoConstant = Expression.Constant(Encoding.UTF8.GetBytes(memberInfo.Name.Substring(checkLength)));
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    var testExpression = Expression.Call(equalityMethodInfo, equalitySwitchValue, Expression.Constant(equalityCheckStart), memberInfoConstant);
                    var matchExpression = Expression.Assign(Expression.PropertyOrField(returnValue, memberInfo.MemberName),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"), readerParameter));
                    var switchCase = Expression.SwitchCase(Expression.IfThenElse(testExpression, matchExpression, defaultValue), switchKey);
                    cases.Add(switchCase);
                }
                else // Either we have found an exact match for the name or we need to build a further level of nested switch tables
                {
                    var nextPrefix = prefix + groupedMemberInfos.Key;
                    var nextSwitch =
                        BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(resolver, memberInfos, nextPrefix, index + 1, switchValue, returnValue, readerParameter);
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

                    var switchCase = Expression.SwitchCase(directMatchExpression ?? nextSwitch, switchKey);
                    cases.Add(switchCase);
                }
            }
            var switchExpression = Expression.Switch(typeof(void), indexedSwitchValue, defaultValue, extendedComparsionMethodInfo, cases.ToArray());
            return switchExpression;
        }

        /// <summary>
        /// Faster than SequenceEqual
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool StringEquals(ReadOnlySpan<char> span, int offset, string comparison)
        {
            if (span.Length - offset != comparison.Length)
            {
                return false;
            }
            for (var i = 0; i < comparison.Length; i++)
            {
                ref readonly var left = ref span[offset + i];
                if (comparison[i] != left)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Faster than SequenceEqual, this needs to be a byte array and not a string otherwise we might run into problems with non ascii property names
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ByteEquals(ReadOnlySpan<byte> span, int offset, byte[] comparison)
        {
            if (span.Length - offset != comparison.Length)
            {
                return false;
            }
            for (var i = 0; i < comparison.Length; i++)
            {
                ref readonly var left = ref span[offset + i];
                if (comparison[i] != left)
                {
                    return false;
                }
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool SwitchByteEquals(ReadOnlySpan<byte> span, byte[] comparison)
        {
            return ByteEquals(span, 0, comparison);
        }

        /// <summary>
        ///     Couldn't get it working with Expression Trees,ref return lvalues do not work yet
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static char GetChar(ReadOnlySpan<char> span, int index)
        {
            return span[index];
        }

        /// <summary>
        ///     Couldn't get it working with Expression Trees,ref return lvalues do not work yet
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte GetByte(ReadOnlySpan<byte> span, int index)
        {
            return span[index];
        }

        protected delegate T DeserializeDelegate<out T, TSymbol, in TResolver>(ref JsonReader<TSymbol> reader)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct;


        protected delegate void SerializeDelegate<in T, TSymbol, in TResolver>(ref JsonWriter<TSymbol> writer, T value)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct;
    }
}