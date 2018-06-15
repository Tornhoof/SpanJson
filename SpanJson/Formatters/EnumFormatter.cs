using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace SpanJson.Formatters
{
    public sealed class EnumFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol, TResolver> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate();
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();
        public static readonly EnumFormatter<T, TSymbol, TResolver> Default = new EnumFormatter<T, TSymbol, TResolver>();


        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value);
        }

        /// <summary>
        ///     Not sure if it's useful to build something like the automaton from the compelxformatter here
        /// </summary>
        /// <returns></returns>
        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");

            var jsonValue = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "jsonValue");
            var returnValue = Expression.Variable(typeof(T), "returnValue");
            MethodInfo readMethodInfo;
            MethodInfo comparisonMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                readMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf16StringSpan));
                comparisonMethodInfo = FindHelperMethod(nameof(SwitchStringEquals));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                readMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadUtf8StringSpan));
                comparisonMethodInfo = FindHelperMethod(nameof(SwitchByteEquals));
            }
            else
            {
                throw new NotSupportedException();
            }

            var expressions = new List<Expression>
            {
                Expression.Assign(jsonValue,
                    Expression.Call(readerParameter, readMethodInfo))
            };
            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Expression constantExpression;
                var formattedValue = GetFormattedValue(value);
                if (typeof(TSymbol) == typeof(char))
                {
                    constantExpression = Expression.Constant(formattedValue);
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    constantExpression = Expression.Constant(Encoding.UTF8.GetBytes(formattedValue));
                }
                else
                {
                    throw new NotSupportedException();
                }

                var switchCase = Expression.SwitchCase(Expression.Assign(returnValue, Expression.Constant(value)), constantExpression);
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(typeof(void), jsonValue,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), comparisonMethodInfo, cases.ToArray());
            expressions.Add(switchExpression);
            var returnTarget = Expression.Label(returnValue.Type);
            var returnLabel = Expression.Label(returnTarget, returnValue);
            expressions.Add(returnLabel);
            var blockExpression = Expression.Block(new[] {jsonValue, returnValue}, expressions);
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