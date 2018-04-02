using System.Runtime.CompilerServices;

namespace SpanJson.Formatters
{
    public sealed class Int32Formatter : IJsonFormatter<int>
    {
        public static readonly Int32Formatter Default = new Int32Formatter();


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Serialize(ref JsonWriter writer, int value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteInt32(value);
        }

        public int DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return reader.ReadInt32();
        }

        public int AllocSize { get; } = 100;
    }
}