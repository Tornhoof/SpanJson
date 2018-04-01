namespace SpanJson.Formatters
{
    public sealed class SByteFormatter : IJsonFormatter<sbyte>
    {
        public static readonly SByteFormatter Default = new SByteFormatter();

        public void Serialize(ref JsonWriter writer, sbyte value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSByte(value);
        }

        public sbyte DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSByte();
        }
    }
}