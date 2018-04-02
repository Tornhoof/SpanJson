namespace SpanJson.Formatters
{
    public sealed class UInt64Formatter : IJsonFormatter<ulong>
    {
        public static readonly UInt64Formatter Default = new UInt64Formatter();

        public void Serialize(ref JsonWriter writer, ulong value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteUInt64(value);
        }

        public ulong DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadUInt64();
        }

        public int AllocSize { get; } = 100;
    }
}