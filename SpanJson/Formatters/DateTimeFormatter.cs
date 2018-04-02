using System;

namespace SpanJson.Formatters
{
    public sealed class DateTimeFormatter : IJsonFormatter<DateTime>
    {
        public static readonly DateTimeFormatter Default = new DateTimeFormatter();

        public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDateTime(value);
        }

        public DateTime DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDateTime();
        }

        public int AllocSize { get; } = 100;
    }
}