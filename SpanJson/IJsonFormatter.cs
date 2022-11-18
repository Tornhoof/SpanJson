namespace SpanJson
{
    public interface IJsonFormatter
    {

    }

    public interface ICustomJsonFormatter
    {
        object Arguments { get; set; }
    }

    public interface ICustomJsonFormatter<T> : IJsonFormatter<T, byte>, IJsonFormatter<T, char>, ICustomJsonFormatter
    {
    }

    public interface IJsonFormatter<T, TSymbol> : IJsonFormatter where TSymbol : struct
    {
        void Serialize(ref JsonWriter<TSymbol> writer, T value);
        T Deserialize(ref JsonReader<TSymbol> reader);

#if NET7_0_OR_GREATER
        public static abstract IJsonFormatter<T, TSymbol> Default { get; }
#endif
    }


    public interface IJsonFormatterStaticDefault<T, TSymbol, out TSelf>  where TSelf : IJsonFormatter<T, TSymbol>, new() where TSymbol : struct
    {
#if NET7_0_OR_GREATER
        public static IJsonFormatter<T, TSymbol> Default { get; } = new TSelf();
#endif
    }
}