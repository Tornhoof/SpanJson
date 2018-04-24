using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class RuntimeFormatter<TSymbol, TResolver> : BaseFormatter, IJsonFormatter<object, TSymbol, TResolver>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly RuntimeFormatter<TSymbol, TResolver> Default = new RuntimeFormatter<TSymbol, TResolver>();

        private static readonly ConcurrentDictionary<Type, SerializeDelegate> RuntimeSerializerDictionary =
            new ConcurrentDictionary<Type, SerializeDelegate>();

        public object Deserialize(ref JsonReader<TSymbol> reader)
        {
            return reader.ReadDynamic();
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, object value)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            // ReSharper disable ConvertClosureToMethodGroup
            var serializer = RuntimeSerializerDictionary.GetOrAdd(value.GetType(), x => BuildSerializeDelegate(x));
            serializer(ref writer, value);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        private static SerializeDelegate BuildSerializeDelegate(Type type)
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(object), "value");

            var formatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter(type);
            var formatterExpression = Expression.Constant(formatter);
            var serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
            var lambda = Expression.Lambda<SerializeDelegate>(
                Expression.Call(formatterExpression, serializeMethodInfo, writerParameter,
                    Expression.Convert(valueParameter, type)), writerParameter, valueParameter);
            return lambda.Compile();
        }

        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, object value);
    }
}