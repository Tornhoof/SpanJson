namespace SpanJson.Formatters
{
    public sealed class UInt16Formatter : IJsonFormatter<ushort>
    {
        public static readonly UInt16Formatter Default = new UInt16Formatter();

        public void Serialize(ref JsonWriter writer, ushort value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt16(value);
        }

        public ushort DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt16();
        }

        public int AllocSize { get; } = 100;
    }
}