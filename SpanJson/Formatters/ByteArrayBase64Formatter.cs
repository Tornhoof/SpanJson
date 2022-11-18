namespace SpanJson.Formatters
{
    public sealed class ByteArrayBase64Formatter<TSymbol, TResolver> : IJsonFormatter<byte[], TSymbol>, IJsonFormatterStaticDefault<byte[], TSymbol, ByteArrayBase64Formatter<TSymbol, TResolver>>
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        public static IJsonFormatter<byte[], TSymbol> Default {get;} = new ByteArrayBase64Formatter<TSymbol, TResolver>();

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