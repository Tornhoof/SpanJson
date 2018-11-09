using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFormatter<T, TSymbol, TResolver> : BaseEnumStringFormatterr<T, TSymbol>, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate(s => "\"" + s + "\"");
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        public static readonly EnumStringFormatter<T, TSymbol, TResolver> Default = new EnumStringFormatter<T, TSymbol, TResolver>();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value);
        }

        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
            MethodInfo nameSpanMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                nameSpanMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16StringSpan));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                nameSpanMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8StringSpan));
            }
            else
            {
                throw new NotSupportedException();
            }

            var returnValue = Expression.Variable(typeof(T), "returnValue");
            var nameSpan = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            var lengthParameter = Expression.Variable(typeof(int), "length");
            var endOfBlockLabel = Expression.Label();
            var nameSpanExpression = Expression.Call(readerParameter, nameSpanMethodInfo);
            var assignNameSpan = Expression.Assign(nameSpan, nameSpanExpression);
            var lengthExpression = Expression.Assign(lengthParameter, Expression.PropertyOrField(nameSpan, "Length"));
            var byteNameSpan = Expression.Variable(typeof(ReadOnlySpan<byte>), "byteNameSpan");
            var parameters = new List<ParameterExpression> {nameSpan, lengthParameter, returnValue};
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

            var memberInfos = new List<JsonMemberInfo>();
            var dict = new Dictionary<string, T>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var formattedValue = GetFormattedValue(value);
                memberInfos.Add(new JsonMemberInfo(value.ToString(), typeof(T), null, formattedValue, false, true, false, null));
                dict.Add(value.ToString(), (T) value);
            }

            Expression MatchExpressionFunctor(JsonMemberInfo memberInfo)
            {
                var enumValue = dict[memberInfo.MemberName];
                return Expression.Assign(returnValue, Expression.Constant(enumValue));
            }

            var returnTarget = Expression.Label(returnValue.Type);
            var returnLabel = Expression.Label(returnTarget, returnValue);
            var expressions = new List<Expression>
            {
                assignNameSpan,
                lengthExpression,
                MemberComparisonBuilder.Build<TSymbol>(memberInfos, 0, lengthParameter, byteNameSpan, endOfBlockLabel, MatchExpressionFunctor),
                Expression.Throw(Expression.Constant(new InvalidOperationException())),
                Expression.Label(endOfBlockLabel),
                returnLabel
            };
            var blockExpression = Expression.Block(parameters, expressions);
            var lambdaExpression =
                Expression.Lambda<DeserializeDelegate>(blockExpression, readerParameter);
            return lambdaExpression.Compile();
        }


        private delegate T DeserializeDelegate(ref JsonReader<TSymbol> reader);
    }
}