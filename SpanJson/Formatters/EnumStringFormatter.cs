using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class EnumStringFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate();
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        public static readonly EnumStringFormatter<T, TSymbol, TResolver> Default = new EnumStringFormatter<T, TSymbol, TResolver>();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            throw new NotImplementedException();
        }

        public T Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value);
        }

        /// <summary>
        ///     Not sure if it's useful to build something like the automaton from the compelxformatter here
        /// </summary>
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

        private static SerializeDelegate BuildSerializeDelegate()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            MethodInfo writerMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim), typeof(string));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typeof(byte[]));
            }
            else
            {
                throw new NotSupportedException();
            }

            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Expression valueConstant;
                var formattedValue = $"\"{GetFormattedValue(value)}\"";
                if (typeof(TSymbol) == typeof(char))
                {
                    valueConstant = Expression.Constant(formattedValue);
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    valueConstant = Expression.Constant(Encoding.UTF8.GetBytes(formattedValue));
                }
                else
                {
                    throw new NotSupportedException();
                }

                var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, writerMethodInfo, valueConstant), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression =
                Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter, valueParameter);
            return lambdaExpression.Compile();
        }

        private static string GetFormattedValue(object enumValue)
        {
            var name = enumValue.ToString();
            return typeof(T).GetMember(name)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
        }

        private delegate T DeserializeDelegate(ref JsonReader<TSymbol> reader);

        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);
    }
}