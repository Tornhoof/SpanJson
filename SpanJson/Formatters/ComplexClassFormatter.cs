namespace SpanJson.Formatters
{
    /// <summary>
    ///     Used for types which are not built-in
    /// </summary>
    public sealed class ComplexClassFormatter<T, TSymbol, TResolver> : ComplexFormatter, IJsonFormatter<T, TSymbol, TResolver>
        where T : class where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ComplexClassFormatter<T, TSymbol, TResolver> Default = new ComplexClassFormatter<T, TSymbol, TResolver>();

        private static readonly DeserializeDelegate<T, TSymbol, TResolver> Deserializer =
            BuildDeserializeDelegate<T, TSymbol, TResolver>();

        private static readonly SerializeDelegate<T, TSymbol, TResolver> Serializer = BuildSerializeDelegate<T, TSymbol, TResolver>();

        public T Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            return Deserializer(ref reader);
        }


        public void Serialize(ref JsonWriter<TSymbol> writer, T value)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            Serializer(ref writer, value);
        }
    }
}