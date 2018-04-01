namespace SpanJson.Formatters
{
    public sealed class SingleFormatter : IJsonFormatter<float>
    {
        public static readonly SingleFormatter Default = new SingleFormatter();

        public void Serialize(ref JsonWriter writer, float value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteSingle(value);
        }

        public float DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadSingle();
        }
    }
}