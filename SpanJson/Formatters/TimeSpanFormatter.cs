using System;

namespace SpanJson.Formatters
{
    public sealed class TimeSpanFormatter : IJsonFormatter<TimeSpan>
    {
        public static readonly TimeSpanFormatter Default = new TimeSpanFormatter();

        public void Serialize(ref JsonWriter writer, TimeSpan value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteTimeSpan(value);
        }

        public TimeSpan DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadTimeSpan();
        }

        public int AllocSize { get; } = 100;
    }
}