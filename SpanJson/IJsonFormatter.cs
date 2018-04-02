namespace SpanJson
{
    public delegate void SerializationDelegate<in T>(ref JsonWriter writer, T value,
        IJsonFormatterResolver formatterResolver);

    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T> : IJsonFormatter
    {
        void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
        int AllocSize { get; }
    }
}