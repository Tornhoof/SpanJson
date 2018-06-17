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

        protected static SerializeDelegate<T, TSymbol> BuildSerializeDelegate<T, TSymbol, TResolver>()
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
                Expression.Lambda<SerializeDelegate<T, TSymbol>>(blockExpression, writerParameter, valueParameter, nestingLimitParameter);
            return lambda.Compile();
        }

        protected static DeserializeDelegate<T, TSymbol> BuildDeserializeDelegate<T, TSymbol, TResolver>()
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
                    .Lambda<DeserializeDelegate<T, TSymbol>>(Expression.Block(
                            Expression.Throw(Expression.Constant(new NotSupportedException($"{typeof(T).Name} contains abstract or interface members."))),
                            Expression.Default(typeof(T))),
                        readerParameter).Compile();
            }

            if (typeof(T).IsAbstract)
            {
                return Expression.Lambda<DeserializeDelegate<T, TSymbol>>(Expression.Default(typeof(T)), readerParameter).Compile();
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

                return Expression.Lambda<DeserializeDelegate<T, TSymbol>>(createExpression, readerParameter).Compile();
            }

            var returnValue = Expression.Variable(typeof(T), "result");
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

            var nameSpan = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            var lengthParameter = Expression.Variable(typeof(int), "length");
            var endOfBlockLabel = Expression.Label();
            var nameSpanExpression = Expression.Call(readerParameter, nameSpanMethodInfo);
            var assignNameSpan = Expression.Assign(nameSpan, nameSpanExpression);
            var lengthExpression = Expression.Assign(lengthParameter, Expression.PropertyOrField(nameSpan, "Length"));
            var byteNameSpan = Expression.Variable(typeof(ReadOnlySpan<byte>), "byteNameSpan");
            var parameters = new List<ParameterExpression>{nameSpan, lengthParameter};
            if (typeof(TSymbol) == typeof(char))
            {
                Expression<Action> functor = () => MemoryMarshal.AsBytes(new ReadOnlySpan<char>());
                var asBytesMethodInfo = (functor.Body as MethodCallExpression).Method;
                nameSpanExpression = Expression.Call(null, asBytesMethodInfo, assignNameSpan);
                assignNameSpan = Expression.Assign(byteNameSpan, nameSpanExpression);
                parameters.Add(byteNameSpan);
            }
            else
            {
                byteNameSpan = nameSpan;
            }
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

            var expressions = new List<Expression>
            {
                assignNameSpan,
                lengthExpression,
                BuildMemberComparisons<TSymbol, TResolver>(memberInfos, 0, lengthParameter, byteNameSpan, readerParameter, endOfBlockLabel, matchExpressionFunctor),
                Expression.Call(readerParameter, skipNextMethodInfo),
                Expression.Label(endOfBlockLabel),
            };

            var ifBlock = Expression.Block(parameters, expressions);
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
                            ifBlock), loopAbort
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
                            ifBlock), loopAbort
                    ),
                    Expression.Label(returnTarget, returnValue)
                );
            }

            var lambda = Expression.Lambda<DeserializeDelegate<T, TSymbol>>(block, readerParameter);
            return lambda.Compile();
        }

        private static bool IsNoRuntimeDecisionRequired(Type memberType)
        {
            return memberType.IsValueType || memberType.IsSealed;
        }

        protected delegate T DeserializeDelegate<out T, TSymbol>(ref JsonReader<TSymbol> reader) where TSymbol : struct;


        protected delegate void SerializeDelegate<in T, TSymbol>(ref JsonWriter<TSymbol> writer, T value, int nestingLimit) where TSymbol : struct;

        private static Expression BuildMemberComparisons<TSymbol, TResolver>(List<JsonMemberInfo> memberInfos, int index, ParameterExpression lengthParameter,
            ParameterExpression nameSpanExpression, ParameterExpression readerParameter, LabelTarget endOfBlockLabel,
            Func<string, Expression, Expression> matchExpressionFunctor)
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var symbolSize = GetSymbolSize<TSymbol>();
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var grouping = memberInfos.Where(a => a.CanRead && GetLength<TSymbol>(a.Name) >= index).GroupBy(a => CalculateKey<TSymbol>(a.Name, index))
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
                    var length = index + group.Key.offset;
                    var formatter = resolver.GetFormatter(memberInfo.MemberType);
                    var formatterType = formatter.GetType();
                    var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                    var matchExpression = matchExpressionFunctor(memberInfo.MemberName,
                        Expression.Call(Expression.Field(null, fieldInfo), formatter.GetType().GetMethod("Deserialize"), readerParameter));
                    matchExpression = Expression.Block(matchExpression, Expression.Goto(endOfBlockLabel));
                    Expression comparisonExpression = null;
                    if (group.Key.Key != 0 || group.Key.offset != 0)
                    {
                        comparisonExpression = Expression.Equal(GetReadMethod(nameSpanExpression, group.Key.intType, Expression.Constant(index * symbolSize)),
                            GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType));
                        var nameLength = GetLength<TSymbol>(memberInfo.Name);
                        while (length < nameLength)
                        {
                            var subKey = CalculateKey<TSymbol>(memberInfo.Name, length);
                            comparisonExpression = Expression.AndAlso(comparisonExpression,
                                Expression.Equal(GetReadMethod(nameSpanExpression, subKey.intType, Expression.Constant(length * symbolSize)),
                                    GetConstantExpressionForGroupKey(subKey.Key, subKey.intType)));
                            length += subKey.offset;
                        }
                    }

                    var lengthExpression = Expression.Equal(lengthParameter, Expression.Constant(length));
                    var ifExpression = comparisonExpression == null ? lengthExpression : Expression.AndAlso(lengthExpression, comparisonExpression);
                    expressions.Add(Expression.IfThen(ifExpression, matchExpression));
                }
                else
                {
                    var nextLength = index + group.Key.offset;
                    var ifExpression = Expression.AndAlso(Expression.GreaterThanOrEqual(lengthParameter, Expression.Constant(nextLength)),
                        Expression.Equal(GetReadMethod(nameSpanExpression, group.Key.intType, Expression.Constant(index * symbolSize)),
                            GetConstantExpressionForGroupKey(group.Key.Key, group.Key.intType)));
                    var subBlock = BuildMemberComparisons<TSymbol, TResolver>(group.ToList(), nextLength, lengthParameter, nameSpanExpression, readerParameter,
                        endOfBlockLabel, matchExpressionFunctor);
                    expressions.Add(Expression.IfThen(ifExpression, subBlock));
                }
            }

            return Expression.Block(expressions);
        }

        private static int GetLength<TSymbol>(string name)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return name.Length;
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Encoding.UTF8.GetByteCount(name);
            }

            throw new NotSupportedException();
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

        private static Expression GetReadMethod(ParameterExpression nameSpanExpression, Type intType, Expression offsetParameter)
        {
            string methodName;
            if (intType == typeof(byte))
            {
                methodName = nameof(SpanHelper.ReadByte);
            }

            else if (intType == typeof(ushort))
            {
                methodName = nameof(SpanHelper.ReadUInt16);
            }

            else if (intType == typeof(uint))
            {
                methodName = nameof(SpanHelper.ReadUInt32);
            }

            else if (intType == typeof(ulong))
            {
                methodName = nameof(SpanHelper.ReadUInt64);
            }
            else
            {
                throw new NotSupportedException();
            }

            var methodInfo = typeof(SpanHelper).GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);
            return Expression.Call(methodInfo, nameSpanExpression, offsetParameter);
        }

        private static (ulong Key, Type intType, int offset) CalculateKey<TSymbol>(string memberName, int index)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return CalculateKeyUtf16(memberName, index); // for calculating the key the index is actually only half as much due to two byte chars
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return CalculateKeyUtf8(memberName, index);
            }

            throw new NotSupportedException();
        }

        private static (ulong Key, Type intType, int offset) CalculateKeyUtf8(string memberName, int index)
        {
            int remaining = GetLength < byte >(memberName) - index;
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
            int remaining = GetLength<char>(memberName) - index;
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

    }
}