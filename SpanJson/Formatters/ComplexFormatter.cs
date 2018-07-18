using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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

            MethodInfo seperatorWriteMethodInfo;
            MethodInfo writeBeginObjectMethodInfo;
            MethodInfo writeEndObjectMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                seperatorWriteMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16ValueSeparator));
                writeBeginObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16BeginObject));
                writeEndObjectMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16EndObject));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
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
                var formatterType = resolver.GetFormatter(memberInfo).GetType();
                Expression serializerInstance = null;
                MethodInfo serializeMethodInfo;
                Expression memberExpression = Expression.PropertyOrField(valueParameter, memberInfo.MemberName);
                var parameterExpressions = new List<Expression> {writerParameter, memberExpression};
                var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                if (IsNoRuntimeDecisionRequired(memberInfo.MemberType))
                {
                    var underlyingType = Nullable.GetUnderlyingType(memberInfo.MemberType);
                    // if it's nullable and we don't need the null, we call the underlying provider directly
                    if (memberInfo.ExcludeNull && underlyingType != null)
                    {
                        formatterType = resolver.GetFormatter(memberInfo, underlyingType).GetType();
                        fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                        var methodInfo = memberInfo.MemberType.GetMethod("GetValueOrDefault", Type.EmptyTypes);
                        memberExpression = Expression.Call(memberExpression, methodInfo);
                        parameterExpressions = new List<Expression> {writerParameter, memberExpression};
                    }

                    serializeMethodInfo = FindPublicInstanceMethod(formatterType, "Serialize", writerParameter.Type.MakeByRefType(),
                        underlyingType ?? memberInfo.MemberType, typeof(int));
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

                ConstantExpression[] writeNameExpressions;
                var formattedMemberInfoName = $"\"{memberInfo.Name}\":";

                MethodInfo propertyNameWriterMethodInfo;
                if (typeof(TSymbol) == typeof(char))
                {
                    writeNameExpressions = new[] {Expression.Constant(formattedMemberInfoName)};
                    propertyNameWriterMethodInfo =
                        FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim), typeof(string));
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    // Everything above a length of 32 is not optimized
                    if (formattedMemberInfoName.Length > 32)
                    {
                        writeNameExpressions = new[] {Expression.Constant(Encoding.UTF8.GetBytes(formattedMemberInfoName))};
                        propertyNameWriterMethodInfo =
                            FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typeof(byte[]));
                    }
                    else
                    {
                        writeNameExpressions = GetIntegersForMemberName(formattedMemberInfoName);
                        var typesToMatch = writeNameExpressions.Select(a => a.Value.GetType());
                        propertyNameWriterMethodInfo =
                            FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typesToMatch.ToArray());
                    }
                }
                else
                {
                    throw new NotSupportedException();
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

                valueExpressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo, writeNameExpressions));
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

        /// <summary>
        /// This is basically the same algorithm as in the t4 template to create the methods
        /// It's necessary to update both
        /// </summary>
        private static ConstantExpression[] GetIntegersForMemberName(string formattedMemberInfoName)
        {
            var result = new List<ConstantExpression>();
            var bytes = Encoding.UTF8.GetBytes(formattedMemberInfoName);
            var remaining = bytes.Length;
            var ulongCount = Math.DivRem(remaining, 8, out remaining);
            int offset = 0;
            for (int j = 0; j < ulongCount; j++)
            {
                result.Add(Expression.Constant(BitConverter.ToUInt64(bytes, offset)));
                offset += sizeof(ulong);
            }

            var uintCount = Math.DivRem(remaining, 4, out remaining);
            for (int j = 0; j < uintCount; j++)
            {
                result.Add(Expression.Constant(BitConverter.ToUInt32(bytes, offset)));
                offset += sizeof(uint);
            }

            var ushortCount = Math.DivRem(remaining, 2, out remaining);
            for (int j = 0; j < ushortCount; j++)
            {
                result.Add(Expression.Constant(BitConverter.ToUInt16(bytes, offset)));
                offset += sizeof(ushort);
            }

            var byteCount = Math.DivRem(remaining, 1, out remaining);
            for (int j = 0; j < byteCount; j++)
            {
                result.Add(Expression.Constant(bytes[offset]));
                offset++;
            }

            Debug.Assert(remaining == 0);
            Debug.Assert(offset == bytes.Length);
            return result.ToArray();
        }

        protected static DeserializeDelegate<T, TSymbol> BuildDeserializeDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var objectDescription = resolver.GetObjectDescription<T>();
            var memberInfos = objectDescription.Where(a => a.CanWrite).ToList();
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            // can't deserialize abstract and only support interfaces based on IEnumerable<T> (this includes, IList, IReadOnlyList, IDictionary et al.
            foreach (var memberInfo in memberInfos)
            {
                var memberType = memberInfo.MemberType;
                if (memberType.IsAbstract)
                {
                    if (memberType.TryGetTypeOfGenericInterface	(typeof(IEnumerable<>), out _))
                    {
                        continue;
                    }

                    return Expression
                        .Lambda<DeserializeDelegate<T, TSymbol>>(Expression.Block(
                                Expression.Throw(Expression.Constant(new NotSupportedException($"{typeof(T).Name} contains abstract members."))),
                                Expression.Default(typeof(T))),
                            readerParameter).Compile();
                }
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

            // We need to decide during generation if we handle constructors or normal member assignment, the difference is done in the functor below
            Func<JsonMemberInfo, Expression> matchExpressionFunctor;
            Expression[] constructorParameterExpresssions = null;
            if (objectDescription.Constructor != null)
            {
                var dict = objectDescription.ConstructorMapping;
                constructorParameterExpresssions = new Expression[dict.Count];
                foreach (var valueTuple in dict)
                {
                    constructorParameterExpresssions[valueTuple.Value.Index] = Expression.Variable(valueTuple.Value.Type);
                }

                matchExpressionFunctor = memberInfo =>
                {
                    var element = dict[memberInfo.MemberName];
                    var formatter = resolver.GetFormatter(memberInfo);
                    var formatterType = formatter.GetType();
                    var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                    return Expression.Assign(constructorParameterExpresssions[element.Index],
                        Expression.Call(Expression.Field(null, fieldInfo),
                            FindPublicInstanceMethod(formatterType, "Deserialize", readerParameter.Type.MakeByRefType()),
                            readerParameter));
                };
            }
            else
            {
                // The normal assign to member type
                matchExpressionFunctor = memberInfo =>
                {
                    var formatter = resolver.GetFormatter(memberInfo);
                    var formatterType = formatter.GetType();
                    var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                    return Expression.Assign(Expression.PropertyOrField(returnValue, memberInfo.MemberName),
                        Expression.Call(Expression.Field(null, fieldInfo),
                            FindPublicInstanceMethod(formatterType, "Deserialize", readerParameter.Type.MakeByRefType()),
                            readerParameter));
                };
            }

            var nameSpan = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            var lengthParameter = Expression.Variable(typeof(int), "length");
            var endOfBlockLabel = Expression.Label();
            var nameSpanExpression = Expression.Call(readerParameter, nameSpanMethodInfo);
            var assignNameSpan = Expression.Assign(nameSpan, nameSpanExpression);
            var lengthExpression = Expression.Assign(lengthParameter, Expression.PropertyOrField(nameSpan, "Length"));
            var byteNameSpan = Expression.Variable(typeof(ReadOnlySpan<byte>), "byteNameSpan");
            var parameters = new List<ParameterExpression> {nameSpan, lengthParameter};
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
                MemberComparisonBuilder.Build<TSymbol>(memberInfos, 0, lengthParameter, byteNameSpan, endOfBlockLabel, matchExpressionFunctor),
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
    }
}