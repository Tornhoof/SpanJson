namespace SpanJson
{
    public class AsyncWriter<TSymbol> where TSymbol : struct
    {
        public TSymbol[] Data { get; }

        public AsyncWriter(TSymbol[] data)
        {
            Data = data;
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(Data);
        }
    }
}
