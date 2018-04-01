namespace SpanJson.Formatters
{
    public sealed class BooleanFormatter : IJsonFormatter<bool>
    {
        public static readonly BooleanFormatter Default = new BooleanFormatter();

        public void Serialize(ref JsonWriter writer, bool value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteBoolean(value);
        }

        public bool DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadBoolean();
        }
    }
}