using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class RuntimeFormatter<TResolver> : IJsonFormatter<object, TResolver> where TResolver : IJsonFormatterResolver<TResolver>, new()
    {
        private delegate void SerializeDelegate(ref JsonWriter writer, object value,
            TResolver formatterResolver);

        public static readonly RuntimeFormatter<TResolver> Default = new RuntimeFormatter<TResolver>();

        private static readonly ConcurrentDictionary<Type, SerializeDelegate> RuntimeSerializerDictionary = new ConcurrentDictionary<Type, SerializeDelegate>();
        public void Serialize(ref JsonWriter writer, object value, TResolver formatterResolver)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // ReSharper disable ConvertClosureToMethodGroup
            var serializer = RuntimeSerializerDictionary.GetOrAdd(value.GetType(), x => BuildSerializeDelegate(x));
            serializer(ref writer, value, formatterResolver);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        private SerializeDelegate BuildSerializeDelegate(Type type)
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(object), "value");
            var resolverParameter = Expression.Parameter(typeof(TResolver), "formatterResolver");
            var formatter = StandardResolvers.GetResolver<TResolver>().GetFormatter(type);
            var formatterExpression = Expression.Constant(formatter);
            var serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
            var lambda = Expression.Lambda<SerializeDelegate>(
                Expression.Call(formatterExpression, serializeMethodInfo, writerParameter,
                    Expression.Convert(valueParameter, type), resolverParameter), writerParameter, valueParameter,
                resolverParameter);
            return lambda.Compile();
        }

        public object Deserialize(ref JsonReader reader, TResolver formatterResolver)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }
            throw new NotImplementedException(); // does not work like thise
        }

        public int AllocSize { get; } = 100;
    }
}
