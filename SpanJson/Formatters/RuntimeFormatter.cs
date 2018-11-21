using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class RuntimeFormatter<TSymbol, TResolver> : BaseFormatter, IJsonFormatter<object, TSymbol>
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
                writer.WriteNull();
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
            if (type == typeof(object)) // if it's an object we can't do anything about so we write an empty object
            {
                return (ref JsonWriter<TSymbol> writer, object value) =>
                {
                    writer.WriteBeginObject();
                    writer.WriteEndObject();
                };
            }

            var formatterType = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter(type).GetType();
            var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
            var serializeMethodInfo = formatterType.GetMethod("Serialize");
            var lambda = Expression.Lambda<SerializeDelegate>(
                Expression.Call(Expression.Field(null, fieldInfo), serializeMethodInfo, writerParameter,
                    Expression.Convert(valueParameter, type)), writerParameter, valueParameter);
            return lambda.Compile();
        }

        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, object value);
    }
}