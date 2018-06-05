using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter : BaseFormatter
    {
        private const int NestingLimit = 256;

        protected static SerializeDelegate<T, TSymbol, TResolver> BuildSerializeDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var memberInfos = resolver.GetObjectDescription<T>().Where(a => a.CanRead).ToList();
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var nestingLimitParameter = Expression.Parameter(typeof(int), "nestingLimit");
            var expressions = new List<Expression>();
            if (RecursionCandidate<T>.IsRecursionCandidate)
            {
                expressions.Add(Expression.IfThen(
                    Expression.GreaterThan(
                        nestingLimitParameter, Expression.Constant(NestingLimit)),
                    Expression.Throw(
                        Expression.Constant(new InvalidOperationException($"Nesting Limit of {NestingLimit} exceeded in Type {typeof(T).Name}.")))));
            }

            MethodInfo propertyNameWriterMethodInfo;
            MethodInfo seperatorWriteMethodInfo;
            MethodInfo writeBeginObjectMethodInfo;
            MethodInfo writeEndObjectMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                propertyNameWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim), typeof(string));
                seperatorWriteMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16ValueSeparator));
                writeBeginObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16BeginObject));
                writeEndObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16EndObject));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                propertyNameWriterMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typeof(byte[]));
                seperatorWriteMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8ValueSeparator));
                writeBeginObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8BeginObject));
                writeEndObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8EndObject));
            }
            else
            {
                throw new NotSupportedException();
            }

            expressions.Add(Expression.Call(writerParameter, writeBeginObjectMethodInfo));
            var writeSeperator = Expression.Variable(typeof(bool), "writeSeperator");
            for (var i = 0; i < memberInfos.Count; i++)
            {
                var memberInfo = memberInfos[i];
                var formatterType = resolver.GetFormatter(memberInfo.MemberType).GetType();
                Expression serializerInstance = null;
                MethodInfo serializeMethodInfo;
                var memberExpression = Expression.PropertyOrField(valueParameter, memberInfo.MemberName);
                var parameterExpressions = new List<Expression> {writerParameter, memberExpression};
                var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                if (IsNoRuntimeDecisionRequired(memberInfo.MemberType))
                {
                    serializeMethodInfo = formatterType.GetMethod("Serialize", BindingFlags.Public | BindingFlags.Instance);
                    serializerInstance = Expression.Field(null, fieldInfo);
                }
                else
                {
                    serializeMethodInfo = typeof(BaseFormatter)
                        .GetMethod(nameof(SerializeRuntimeDecisionInternal), BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(memberInfo.MemberType, typeof(TSymbol), typeof(TResolver));
                    parameterExpressions.Add(Expression.Field(null, fieldInfo));
                }

                if (RecursionCandidate.LookupRecursionCandidate(memberInfo.MemberType)) // only for possible candidates
                {
                    parameterExpressions.Add(Expression.Add(nestingLimitParameter, Expression.Constant(1)));
                }
                else
                {
                    parameterExpressions.Add(nestingLimitParameter);
                }

                var writerNameConstant = GetConstantExpressionOfString<TSymbol>($"\"{memberInfo.Name}\":");
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

                valueExpressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo, writerNameConstant));
                valueExpressions.Add(Expression.Call(serializerInstance, serializeMethodInfo, parameterExpressions));
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
                Expression.Lambda<SerializeDelegate<T, TSymbol, TResolver>>(blockExpression, writerParameter, valueParameter, nestingLimitParameter);
            return lambda.Compile();
        }

        protected static DeserializeDelegate<T, TSymbol, TResolver> BuildDeserializeDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var objectDescription = resolver.GetObjectDescription<T>();
            var memberInfos = objectDescription.Where(a => a.CanWrite).ToList();
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
                nameSpanMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16NameSpan));
                tryReadEndObjectMethodInfo =
                    FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.TryReadUtf16IsEndObjectOrValueSeparator));
                beginObjectOrThrowMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16BeginObjectOrThrow));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                nameSpanMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8NameSpan));
                tryReadEndObjectMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.TryReadUtf8IsEndObjectOrValueSeparator));
                beginObjectOrThrowMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8BeginObjectOrThrow));
            }
            else
            {
                throw new NotSupportedException();
            }

            Func<string, Expression, Expression> matchExpressionFunctor;
            Expression[] constructorParameterExpresssions = null;
            if (objectDescription.Constructor != null)
            {
                var dict = objectDescription.ConstructorMapping;
                constructorParameterExpresssions = new Expression[dict.Count];
                foreach (var valueTuple in dict)
                {
                    constructorParameterExpresssions[valueTuple.Value.Index] = Expression.Variable(valueTuple.Value.Type);
                }

                matchExpressionFunctor = (memberName, valueExpression) =>
                {
                    var element = dict[memberName];
                    return Expression.Assign(constructorParameterExpresssions[element.Index], valueExpression);
                };
            }
            else
            {
                // The normal assign to member type
                matchExpressionFunctor = (memberName, valueExpression) =>
                    Expression.Assign(Expression.PropertyOrField(returnValue, memberName), valueExpression);
            }

            var switchValueAssignExpression = Expression.Assign(switchValue, Expression.Call(readerParameter, nameSpanMethodInfo));
            var switchExpression = Expression.Block(new[] {switchValue}, switchValueAssignExpression,
                BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(resolver, memberInfos, null, 0, switchValue, matchExpressionFunctor,
                    readerParameter));
            var countExpression = Expression.Parameter(typeof(int), "count");
            var abortExpression = Expression.IsTrue(Expression.Call(readerParameter, tryReadEndObjectMethodInfo, countExpression));
            var readBeginObject = Expression.Call(readerParameter, beginObjectOrThrowMethodInfo);
            var loopAbort = Expression.Label(typeof(void));
            var returnTarget = Expression.Label(returnValue.Type);
            Expression block;
            if (objectDescription.Constructor != null)
            {
                var blockParameters = new List<ParameterExpression> {returnValue, countExpression};
                // ReSharper disable AssignNullToNotNullAttribute
                blockParameters.AddRange(constructorParameterExpresssions.OfType<ParameterExpression>());
                // ReSharper restore AssignNullToNotNullAttribute
                block = Expression.Block(blockParameters, readBeginObject,
                    Expression.Loop(
                        Expression.IfThenElse(abortExpression, Expression.Break(loopAbort),
                            switchExpression), loopAbort
                    ),
                    Expression.Assign(returnValue, Expression.New(objectDescription.Constructor, constructorParameterExpresssions)),
                    Expression.Label(returnTarget, returnValue)
                );
            }
            else
            {
                block = Expression.Block(new[] {returnValue, countExpression}, readBeginObject,
                    Expression.Assign(returnValue, Expression.New(returnValue.Type)),
                    Expression.Loop(
                        Expression.IfThenElse(abortExpression, Expression.Break(loopAbort),
                            switchExpression), loopAbort
                    ),
                    Expression.Label(returnTarget, returnValue)
                );
            }

            var lambda = Expression.Lambda<DeserializeDelegate<T, TSymbol, TResolver>>(block, readerParameter);
            return lambda.Compile();
        }

        private static bool IsNoRuntimeDecisionRequired(Type memberType)
        {
            return memberType.IsValueType || memberType.IsSealed;
        }

        /// <summary>
        ///     We group the field names by the nth character and nest the switch tables to find the appropriate field/property to
        ///     assign to
        /// </summary>
        private static Expression BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(TResolver resolver, ICollection<JsonMemberInfo> memberInfos,
            string prefix,
            int index, ParameterExpression switchValue, Func<string, Expression, Expression> matchExpressionFunctor, Expression readerParameter)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
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
                skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf16Segment));
                equalityMethodInfo = FindHelperMethod(nameof(StringEquals));
                indexedSwitchValue = Expression.Call(typeof(ComplexFormatter).GetMethod(nameof(GetChar), BindingFlags.NonPublic | BindingFlags.Static),
                    switchValue, Expression.Constant(index));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf8Segment));
                equalityMethodInfo = FindHelperMethod(nameof(ByteEquals));
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
            var extendedComparisonNecessary = typeof(TSymbol) == typeof(byte) && group.Any(a => a.Key > 0x7F);
            var extendedComparsionMethodInfo = extendedComparisonNecessary ? FindHelperMethod(nameof(SwitchByteEquals)) : null;
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
                        equalitySwitchValue =
                            Expression.Call(switchValue, "Slice", Type.EmptyTypes,
                                Expression.Constant(encoded.Length)); // For the equality comparison we need to change the switch value
                        switchKey = Expression.Constant(encoded);
                    }
                    else
                    {
                        switchKey = Expression.Constant((byte) groupedMemberInfos.Key);
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
                    var formatterType = resolver.GetFormatter(memberInfo.MemberType).GetType();
                    var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                    var checkLength = (prefix?.Length ?? 0) + 1;
                    var equalityCheckStart = extendedComparisonNecessary ? 0 : checkLength;
                    Expression memberInfoConstant = GetConstantExpressionOfString<TSymbol>(memberInfo.Name.Substring(checkLength));
                    var testExpression = Expression.Call(equalityMethodInfo, equalitySwitchValue, Expression.Constant(equalityCheckStart), memberInfoConstant);
                    var matchExpression = matchExpressionFunctor(memberInfo.MemberName,
                        Expression.Call(Expression.Field(null, fieldInfo), formatterType.GetMethod("Deserialize"), readerParameter));

                    var switchCase = Expression.SwitchCase(Expression.IfThenElse(testExpression, matchExpression, defaultValue), switchKey);
                    cases.Add(switchCase);
                }
                else // Either we have found an exact match for the name or we need to build a further level of nested switch tables
                {
                    var nextPrefix = prefix + groupedMemberInfos.Key;
                    var nextSwitch =
                        BuildPropertyComparisonSwitchExpression<TSymbol, TResolver>(resolver, memberInfos, nextPrefix, index + 1, switchValue,
                            matchExpressionFunctor,
                            readerParameter);
                    var exactMatch = groupedMemberInfos.SingleOrDefault(a => a.Name == nextPrefix);
                    Expression directMatchExpression = null;
                    if (exactMatch != null)
                    {
                        var formatterType = resolver.GetFormatter(exactMatch.MemberType).GetType();
                        var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                        var matchExpression = matchExpressionFunctor(exactMatch.MemberName,
                            Expression.Call(Expression.Field(null, fieldInfo), formatterType.GetMethod("Deserialize"), readerParameter));

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


        protected delegate void SerializeDelegate<in T, TSymbol, in TResolver>(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct;
    }

    public abstract class ComplexFormatter<T, TSymbol, TResolver> : ComplexFormatter
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();

        private static readonly Func<T> CreateFunctor = BuildCreateFunctor<T>(typeof(T));
        private static readonly SerializeDelegate<T, TSymbol, TResolver> Serializer = BuildSerializeDelegate<T, TSymbol, TResolver>();
        private static readonly int SymbolSize = GetSymbolSize();

        private static int GetSymbolSize()
        {
            return Unsafe.SizeOf<TSymbol>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void SerializeInternal(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value, nestingLimit);
        }

        // TODO: Find a way to support constructor attribute
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected T DeserializeInternal(ref JsonReader<TSymbol> reader)
        {
            var result = CreateFunctor();
            var count = 0;
            reader.ReadBeginObjectOrThrow();
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadNameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                Deserializer(length, result, ref b, ref reader);
            }

            return result;
        }

        private delegate T DeserializeDelegate(int length, T result, ref byte b, ref JsonReader<TSymbol> reader);

        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var objectDescription = resolver.GetObjectDescription<T>();
            var memberInfos = objectDescription.Where(a => a.CanWrite).ToList();
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            var byteParameter = Expression.Parameter(typeof(byte).MakeByRefType(), "b");
            var lengthParameter = Expression.Parameter(typeof(int), "length");
            var resultParameter = Expression.Parameter(typeof(T), "result");
            var endOfBlockLabel = Expression.Label();
            var expressions = new List<Expression>
            {
                BuildMemberComparisons(memberInfos, 0, lengthParameter, byteParameter, resultParameter, readerParameter, endOfBlockLabel),
                Expression.Label(endOfBlockLabel)
            };
            var block = Expression.Block(expressions);
            var lambda = Expression.Lambda<DeserializeDelegate>(block, lengthParameter, resultParameter, byteParameter, readerParameter);
            return lambda.Compile();
        }

        private static Expression BuildMemberComparisons(List<JsonMemberInfo> memberInfos, int index, ParameterExpression lengthParameter, ParameterExpression byteParameter, ParameterExpression resultParameter, ParameterExpression readerParameter, LabelTarget endOfBlockLabel)
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var grouping = memberInfos.Where(a => a.CanRead && a.Name.Length >= index).GroupBy(a => CalculateKey(a.Name, index))
                .OrderByDescending(a => a.Key.Key).ToList();
            if (!grouping.Any())
            {
                throw new InvalidOperationException(); // should never happen
            }

            var expressions = new List<Expression>();
            foreach (var group in grouping)
            {
                if (group.Count() == 1) // need to check remaining values too 
                {
                    var memberInfo = group.Single();
                    var i = index + group.Key.offset;
                    var lengthEqualExpression = Expression.Equal(lengthParameter, Expression.Constant(i));
                    var formatter = resolver.GetFormatter(memberInfo.MemberType);
                    var matchExpression =
                        Expression.Block(Expression.Assign(resultParameter,
                                Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"), readerParameter)),
                            Expression.Goto(endOfBlockLabel));
                    if (group.Key.Key != 0 || group.Key.offset != 0)
                    {
                        Expression ifExpression = lengthEqualExpression;
                        ifExpression = Expression.AndAlso(ifExpression,
                            Expression.Equal(GetReadMethod(byteParameter, group.Key.intType, Expression.Constant(index)),
                                GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType)));
                        while (i < memberInfo.Name.Length)
                        {
                            var subKey = CalculateKey(memberInfo.Name, i);
                            ifExpression = Expression.AndAlso(ifExpression,
                                Expression.Equal(GetReadMethod(byteParameter, subKey.intType, Expression.Constant(index)),
                                    GetConstantExpressionForGroupKey(subKey.Key, subKey.intType)));
                            i += subKey.offset;
                        }

                        expressions.Add(Expression.IfThen(ifExpression, matchExpression));
                    }
                }
                else
                {
                    var nextIndex = index + group.Key.offset;
                    var ifExpression = Expression.AndAlso(Expression.GreaterThanOrEqual(lengthParameter, Expression.Constant(nextIndex)), 
                        Expression.Equal(GetReadMethod(byteParameter, group.Key.intType, Expression.Constant(index)),
                            GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType)));
                    var subBlock = BuildMemberComparisons(group.ToList(), nextIndex, lengthParameter, byteParameter, resultParameter, readerParameter, endOfBlockLabel);
                    expressions.Add(Expression.IfThen(ifExpression, subBlock));
                    MethodInfo skipNextMethodInfo;
                    if (typeof(TSymbol) == typeof(char))
                    {
                        skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf16Segment));
                    }
                    else if (typeof(TSymbol) == typeof(byte))
                    {
                        skipNextMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.SkipNextUtf8Segment));
                    }
                    else
                    {
                        throw new NotSupportedException();
                    }

                    expressions.Add(Expression.Call(readerParameter, skipNextMethodInfo));
                    expressions.Add(Expression.Goto(endOfBlockLabel));
                }
            }
            return null;
        }

        private static Expression GetConstantExpressionForGroupKey(ulong key, Type intType)
        {
            if (intType == typeof(byte))
            {
                return Expression.Constant((byte) key);
            }

            if (intType == typeof(ushort))
            {
                return Expression.Constant((ushort) key);
            }

            if (intType == typeof(uint))
            {
                return Expression.Constant((uint) key);
            }

            if (intType == typeof(ulong))
            {
                return Expression.Constant(key);
            }
            throw new NotSupportedException();
        }

        private static Expression GetReadMethod(ParameterExpression byteParameter, Type intType, Expression offsetParameter)
        {
            string methodName;
            if (intType == typeof(byte))
            {
                methodName = nameof(ReadByte);
            }

            else if (intType == typeof(ushort))
            {
                methodName = nameof(ReadUInt16);
            }

            else if (intType == typeof(uint))
            {
                methodName = nameof(ReadUInt32);
            }

            else if (intType == typeof(ulong))
            {
                methodName = nameof(ReadUInt64);
            }
            else
            {
                throw new NotSupportedException();
            }

            var methodInfo = typeof(ComplexFormatter<T, TSymbol, TResolver>).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            return Expression.Call(methodInfo, byteParameter, offsetParameter);
        }

        private static (ulong Key, Type intType, int offset) CalculateKey(string memberName, int index)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return CalculateKeyUtf16(memberName, index);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return CalculateKeyUtf8(memberName, index);
            }

            throw new NotSupportedException();
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf8(string memberName, int index)
        {
            int remaining = memberName.Length - index;
            if (remaining >= 8)
            {
                return (BitConverter.ToUInt64(Encoding.UTF8.GetBytes(memberName), index), typeof(ulong), 8);
            }

            if (remaining >= 4)
            {
                return (BitConverter.ToUInt32(Encoding.UTF8.GetBytes(memberName), index), typeof(uint), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt16(Encoding.UTF8.GetBytes(memberName), index), typeof(ushort), 2);
            }

            if (remaining >= 1)
            {
                return (Encoding.UTF8.GetBytes(memberName)[index], typeof(byte), 1);
            }

            return (0, typeof(uint), 0);
        }


        private static (ulong Key, Type intType, int offset) CalculateKeyUtf16(string memberName, int index)
        {
            int remaining = memberName.Length - index;
            if (remaining >= 4)
            {
                return (BitConverter.ToUInt64(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ulong), 4);
            }

            if (remaining >= 2)
            {
                return (BitConverter.ToUInt32(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(uint), 2);
            }

            if (remaining >= 1)
            {
                return (BitConverter.ToUInt16(Encoding.Unicode.GetBytes(memberName), index * 2), typeof(ushort), 1);
            }

            return (0, typeof(uint), 0);
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static byte ReadByte(ref byte b, int offset)
        {
            return Unsafe.Add(ref b, offset);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ushort ReadUInt16(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<ushort>(ref Unsafe.Add(ref b, offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static uint ReadUInt32(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<uint>(ref Unsafe.Add(ref b, offset));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected static ulong ReadUInt64(ref byte b, int offset)
        {
            return Unsafe.ReadUnaligned<ulong>(ref Unsafe.Add(ref b, offset));
        }
    }
}