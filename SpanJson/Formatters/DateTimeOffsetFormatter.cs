using System;

namespace SpanJson.Formatters
{
    public sealed class DateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset>
    {
        public static readonly DateTimeOffsetFormatter Default = new DateTimeOffsetFormatter();

        public void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteDateTimeOffset(value);
        }

        public DateTimeOffset DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadDateTimeOffset();
        }
    }
}