namespace SpanJson.Formatters
{
    public sealed class UInt32Formatter : IJsonFormatter<uint>
    {
        public static readonly UInt32Formatter Default = new UInt32Formatter();

        public void Serialize(ref JsonWriter writer, uint value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt32(value);
        }

        public uint DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt32();
        }
    }
}