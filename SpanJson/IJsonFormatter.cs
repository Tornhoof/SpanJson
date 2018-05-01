namespace SpanJson
{
    public interface IJsonFormatter
    {
    }

    public interface IJsonFormatter<T, TSymbol, in TResolver> : IJsonFormatter
        where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value, int nestingLimit);
        T Deserialize(ref JsonReader<TSymbol> reader);
    }
}