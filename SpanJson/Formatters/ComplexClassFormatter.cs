namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T, TSymbol, TResolver> : ComplexFormatter, IJsonFormatter<T, TSymbol>
        where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private static readonly DeserializeDelegate<T, TSymbol> Deserializer =
            BuildDeserializeDelegate<T, TSymbol, TResolver, DeserializeDelegate<T, TSymbol>>();

        private static readonly SerializeDelegate<T, TSymbol> Serializer = BuildSerializeDelegate<T, TSymbol, TResolver, SerializeDelegate<T, TSymbol>>();

        private static readonly StreamingDeserializeDelegate<T, TSymbol> StreamingDeserializer =
            BuildDeserializeDelegate<T, TSymbol, TResolver, StreamingDeserializeDelegate<T, TSymbol>>();

        private static readonly StreamingSerializeDelegate<T, TSymbol> StreamingSerializer =
            BuildSerializeDelegate<T, TSymbol, TResolver, StreamingSerializeDelegate<T, TSymbol>>();

        public static readonly ComplexClassFormatter<T, TSymbol, TResolver> Default = new ComplexClassFormatter<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return Deserializer(ref reader);
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            StreamingSerializer(ref writer, value, nestingLimit);
        }

        public T Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return StreamingDeserializer(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            Serializer(ref writer, value, nestingLimit);
        }
    }
}