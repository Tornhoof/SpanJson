namespace SpanJson.Formatters
{
    public sealed class ByteArrayBase64Formatter<TSymbol, TResolver> : IJsonFormatter<byte[], TSymbol>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static readonly ByteArrayBase64Formatter<TSymbol, TResolver> Default = new ByteArrayBase64Formatter<TSymbol, TResolver>();

        public void Serialize(ref JsonWriter<TSymbol> writer, byte[] value)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }
            writer.WriteBase64EncodedArray(value);
        }

        public byte[] Deserialize(ref JsonReader<TSymbol> reader)
        {
            if (reader.ReadIsNull())
            {
                return null;
            }

            return reader.ReadBase64EncodedArray();
        }
    }
}