namespace SpanJson
{
    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit);
        T Deserialize(ref JsonReader<TSymbol> reader);
    }
}