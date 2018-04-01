namespace SpanJson.Formatters
{
    public sealed class ByteFormatter : IJsonFormatter<byte>
    {
        public static readonly ByteFormatter Default = new ByteFormatter();

        public void Serialize(ref JsonWriter writer, byte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteByte(value);
        }

        public byte DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadByte();
        }
    }
}