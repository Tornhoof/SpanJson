namespace SpanJson.Formatters
{
    public sealed class DoubleFormatter : IJsonFormatter<double>
    {
        public static readonly DoubleFormatter Default = new DoubleFormatter();

        public void Serialize(ref JsonWriter writer, double value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDouble(value);
        }

        public double DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDouble();
        }

        public int AllocSize { get; } = 100;
    }
}