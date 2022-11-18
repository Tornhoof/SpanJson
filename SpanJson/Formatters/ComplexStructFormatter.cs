namespace SpanJson.Formatters
{
    /// <summary>
    /// Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T, TSymbol, TResolver> : ComplexFormatter, IJsonFormatter<T, TSymbol>, IJsonFormatterStaticDefault<T, TSymbol, ComplexStructFormatter<T, TSymbol, TResolver>>
        where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static IJsonFormatter<T, TSymbol> Default {get;} = new ComplexStructFormatter<T, TSymbol, TResolver>();
        private static readonly DeserializeDelegate<T, TSymbol> Deserializer = BuildDeserializeDelegate<T, TSymbol, TResolver>();
        private static readonly SerializeDelegate<T, TSymbol> Serializer = BuildSerializeDelegate<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return Deserializer(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
        {
            Serializer(ref writer, value);
        }
    }
}