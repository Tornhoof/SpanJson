namespace SpanJson.Formatters
{
    public sealed class Int16Formatter : IJsonFormatter<short>
    {
        public static readonly Int16Formatter Default = new Int16Formatter();

        public void Serialize(ref JsonWriter writer, short value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt16(value);
        }

        public short DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt16();
        }

        public int AllocSize { get; } = 100;
    }
}