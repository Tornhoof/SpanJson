using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public sealed class RuntimeFormatter<TSymbol, TResolver> : BaseFormatter, IJsonFormatter<object, TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private readonly struct Delegates
        {
            private Delegates(SerializeDelegate serializer, StreamingSerializeDelegate streamingSerializer)
            {
                Serializer = serializer;
                StreamingSerializer = streamingSerializer;
            }
            public SerializeDelegate Serializer { get; }
            public StreamingSerializeDelegate StreamingSerializer { get; }

            public static Delegates Create(Type type)
            {
                return new Delegates(BuildSerializeDelegate<SerializeDelegate>(type), BuildSerializeDelegate<StreamingSerializeDelegate>(type));
            }
        }

        public static readonly RuntimeFormatter<TSymbol, TResolver> Default = new RuntimeFormatter<TSymbol, TResolver>();

        private static readonly ConcurrentDictionary<Type, Delegates> RuntimeSerializerDictionary =
            new ConcurrentDictionary<Type, Delegates>();

        public object Deserialize(ref JsonReader<TSymbol> reader)
        {
            return reader.ReadDynamic();
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, object value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            // ReSharper disable ConvertClosureToMethodGroup
            var serializer = RuntimeSerializerDictionary.GetOrAdd(value.GetType(), x => Delegates.Create(x));
            serializer.StreamingSerializer(ref writer, value, nestingLimit);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        public object Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            return reader.ReadDynamic();
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, object value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }


            // ReSharper disable ConvertClosureToMethodGroup
            var serializer = RuntimeSerializerDictionary.GetOrAdd(value.GetType(), x => Delegates.Create(x));
            serializer.Serializer(ref writer, value, nestingLimit);
            // ReSharper restore ConvertClosureToMethodGroup
        }

        private static TDelegate BuildSerializeDelegate<TDelegate>(Type type) where TDelegate : Delegate
        {
            var writerType = GetReaderWriterTypeFromDelegate<TDelegate>();
            var writerParameter = Expression.Parameter(writerType, "writer");
            var valueParameter = Expression.Parameter(typeof(object), "value");
            var nestingLimitParameter = Expression.Parameter(typeof(int), "nestingLimit");
            Expression lambdaBody;
            if (type == typeof(object)) // if it's an object we can't do anything about so we write an empty object
            {
                lambdaBody = Expression.Block(Expression.Call(writerParameter,
                        FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteBeginObject))),
                    Expression.Call(writerParameter,
                        FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteEndObject))));
            }
            else
            {
                var formatterType = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter(type).GetType();
                var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                var serializeMethodInfo = FindPublicInstanceMethod(formatterType, "Serialize", writerType);
                lambdaBody = Expression.Call(Expression.Field(null, fieldInfo), serializeMethodInfo, writerParameter,
                    Expression.Convert(valueParameter, type), nestingLimitParameter);
            }

            var lambda = Expression.Lambda<TDelegate>(lambdaBody, writerParameter, valueParameter, nestingLimitParameter);
            return lambda.Compile();
        }

        private delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, object value, int nestingLimit);
        private delegate void StreamingSerializeDelegate(ref StreamingJsonWriter<TSymbol> writer, object value, int nestingLimit);
    }
}