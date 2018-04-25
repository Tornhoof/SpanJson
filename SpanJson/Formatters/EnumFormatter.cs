using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpanJson.Formatters
{
    public sealed class EnumFormatter<T, TSymbol, TResolver> : BaseFormatter, IJsonFormatter<T, TSymbol, TResolver> where T : struct
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

        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
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
            var readMethodInfo = FindPublicInstanceMethod(readerParameter.Type, nameof(JsonReader<TSymbol>.ReadStringSpan));
            var equalityMethodInfo = FindHelperMethod(nameof(SymbolSequenceEquals)).MakeGenericMethod(typeof(TSymbol));
            var expressions = new List<Expression>
            {
                Expression.Assign(jsonValue,
                    Expression.Call(readerParameter, readMethodInfo))
            };
            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var constantExpression = GetConstantExpressionOfString<TSymbol>(value.ToString());
                var switchCase = Expression.SwitchCase(Expression.Assign(returnValue, Expression.Constant(value)), constantExpression);
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(typeof(void), jsonValue,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), equalityMethodInfo, cases.ToArray());
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
            var writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteVerbatim), typeof(TSymbol[]));
            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var valueConstant = GetConstantExpressionOfString<TSymbol>($"\"{value}\"");
                var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, writerMethodInfo, valueConstant), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression =
                Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter, valueParameter);
            return lambdaExpression.Compile();
        }


        private delegate T DeserializeDelegate(ref JsonReader<TSymbol> reader);

        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);
    }
}