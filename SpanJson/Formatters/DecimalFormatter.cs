namespace SpanJson.Formatters
{
    public sealed class DecimalFormatter : IJsonFormatter<decimal>
    {
        public static readonly DecimalFormatter Default = new DecimalFormatter();

        public void Serialize(ref JsonWriter writer, decimal value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDecimal(value);
        }

        public decimal DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDecimal();
        }
    }
}