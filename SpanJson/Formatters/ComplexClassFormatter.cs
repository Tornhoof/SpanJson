namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T, TSymbol, TResolver> : ComplexFormatter<T, TSymbol, TResolver>, IJsonFormatter<T, TSymbol>
        where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ComplexClassFormatter<T, TSymbol, TResolver> Default = new ComplexClassFormatter<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return DeserializeInternal(ref reader);
        }

        public void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            SerializeInternal(ref writer, value, nestingLimit);
        }
    }
}