namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexStructFormatter<T, TSymbol, TResolver> : ComplexFormatter<T, TSymbol, TResolver>, IJsonFormatter<T, TSymbol, TResolver>
        where T : struct where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ComplexStructFormatter<T, TSymbol, TResolver> Default = new ComplexStructFormatter<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            return DeserializeInternal(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            SerializeInternal(ref writer, value, nestingLimit);
        }
    }
}