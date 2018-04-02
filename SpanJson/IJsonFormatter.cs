namespace SpanJson
{
    public delegate void SerializationDelegate<in T>(in JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);

    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T> : IJsonFormatter
    {
        void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);
        T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver);
    }

    public interface IJsonCompositeFormatter : IJsonFormatter
    {
    }
    public interface IJsonCompositeFormatter<T> : IJsonCompositeFormatter
    {

    }
}