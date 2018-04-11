namespace SpanJson
{
    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T, in TResolver> : IJsonFormatter where TResolver : IJsonFormatterResolver, new()
    {
        void Serialize(ref JsonWriter writer, T value, TResolver formatterResolver);
        T Deserialize(ref JsonReader reader, TResolver formatterResolver);
        int AllocSize { get; }
    }
}