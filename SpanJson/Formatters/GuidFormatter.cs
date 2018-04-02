using System;

namespace SpanJson.Formatters
{
    public sealed class GuidFormatter : IJsonFormatter<Guid>
    {
        public static readonly GuidFormatter Default = new GuidFormatter();

        public void Serialize(ref JsonWriter writer, Guid value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteGuid(value);
        }

        public Guid DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadGuid();
        }

        public int AllocSize { get; } = 100;
    }
}