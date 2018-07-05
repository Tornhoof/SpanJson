namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T, TSymbol, TResolver> : ComplexFormatter, IJsonFormatter<T, TSymbol>
        where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        private static readonly DeserializeDelegate<T, TSymbol> Deserializer =
            BuildDeserializeDelegate<T, TSymbol, TResolver, DeserializeDelegate<T, TSymbol>>(false);

        private static readonly SerializeDelegate<T, TSymbol> Serializer = BuildSerializeDelegate<T, TSymbol, TResolver, SerializeDelegate<T, TSymbol>>(false);

        private static readonly StreamingDeserializeDelegate<T, TSymbol> StreamingDeserializer =
            BuildDeserializeDelegate<T, TSymbol, TResolver, StreamingDeserializeDelegate<T, TSymbol>>(true);

        private static readonly StreamingSerializeDelegate<T, TSymbol> StreamingSerializer =
            BuildSerializeDelegate<T, TSymbol, TResolver, StreamingSerializeDelegate<T, TSymbol>>(true);

        public static readonly ComplexStructFormatter<T, TSymbol, TResolver> Default = new ComplexStructFormatter<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref StreamingJsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            StreamingSerializer(ref writer, value, nestingLimit);
        }

        public T Deserialize(ref StreamingJsonReader<TSymbol> reader)
        {
            return StreamingDeserializer(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            Serializer(ref writer, value, nestingLimit);
        }
    }
}