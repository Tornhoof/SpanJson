using System;
using System.Linq.Expressions;

namespace SpanJson.Formatters
{
    public sealed class EnumIntegerFormatter<T, TSymbol, TResolver> : BaseEnumFormatter<T, TSymbol>, IJsonFormatter<T, TSymbol> where T : Enum
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
        where TSymbol : struct
    {
        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate<SerializeDelegate>();
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate<DeserializeDelegate>();

        private static readonly StreamingSerializeDelegate StreamingSerializer = BuildSerializeDelegate<StreamingSerializeDelegate>();
        private static readonly StreamingDeserializeDelegate StreamingDeserializer = BuildDeserializeDelegate<StreamingDeserializeDelegate>();

        public static readonly EnumIntegerFormatter<T, TSymbol, TResolver> Default = new EnumIntegerFormatter<T, TSymbol, TResolver>();
        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value);
        }

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            StreamingSerializer(ref writer, value);
        }

        public T Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            return StreamingDeserializer(ref reader);
        }


        private static TDelegate BuildSerializeDelegate<TDelegate>() where TDelegate : Delegate
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(T));
            var writerType = GetReaderWriterTypeFromDelegate<TDelegate>();
            var writerParameter = Expression.Parameter(writerType, "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            string methodName;
            if (typeof(TSymbol) == typeof(char))
            {
                methodName = $"WriteUtf16{underlyingType.Name}";
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                methodName = $"WriteUtf8{underlyingType.Name}";
            }
            else
            {
                throw new NotSupportedException();
            }

            var writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, methodName, underlyingType);
            var lambda = Expression.Lambda<TDelegate>(Expression.Call(writerParameter, writerMethodInfo,
                Expression.Convert(valueParameter, underlyingType)), writerParameter, valueParameter);
            return lambda.Compile();
        }


        private static TDelegate BuildDeserializeDelegate<TDelegate>() where TDelegate : Delegate 
        {
            var underlyingType = Enum.GetUnderlyingType(typeof(T));
            var readerType = GetReaderWriterTypeFromDelegate<TDelegate>();
            var readerParameter = Expression.Parameter(readerType, "reader");
            string methodName;
            if (typeof(TSymbol) == typeof(char))
            {
                methodName = $"ReadUtf16{underlyingType.Name}";
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                methodName = $"ReadUtf8{underlyingType.Name}";
            }
            else
            {
                throw new NotSupportedException();
            }

            var readerMethodInfo = FindPublicInstanceMethod(readerParameter.Type, methodName);
            var lambda = Expression.Lambda<TDelegate>(Expression.Convert(Expression.Call(readerParameter, readerMethodInfo), typeof(T)), readerParameter);
            return lambda.Compile();
        }
    }
}
