using System.Runtime.CompilerServices;

namespace SpanJson.Formatters
{
    public sealed class StringFormatter : IJsonFormatter<string>
    {
        public static readonly StringFormatter Default = new StringFormatter();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(ref JsonWriter writer, string value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteString(value);
        }

        public string DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadString();
        }

        public int AllocSize { get; } = 100;
    }
}