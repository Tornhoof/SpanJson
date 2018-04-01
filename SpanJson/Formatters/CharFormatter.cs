namespace SpanJson.Formatters
{
    public sealed class CharFormatter : IJsonFormatter<char>
    {
        public static readonly CharFormatter Default = new CharFormatter();

        public void Serialize(ref JsonWriter writer, char value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteChar(value);
        }

        public char DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadChar();
        }
    }
}