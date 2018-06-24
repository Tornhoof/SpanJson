namespace SpanJson
{
    public interface IJsonFormatter
    {
    }
    public interface ICustomJsonFormatter<T> : IJsonFormatter<T, byte>, IJsonFormatter<T, char>
    {

    }

    public interface IJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit);
        T Deserialize(ref JsonReader<TSymbol> reader);
    }
}