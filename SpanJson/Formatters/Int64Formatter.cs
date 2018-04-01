namespace SpanJson.Formatters
{
    public sealed class Int64Formatter : IJsonFormatter<long>
    {
        public static readonly Int64Formatter Default = new Int64Formatter();

        public void Serialize(ref JsonWriter writer, long value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt64(value);
        }

        public long DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt64();
        }
    }
}