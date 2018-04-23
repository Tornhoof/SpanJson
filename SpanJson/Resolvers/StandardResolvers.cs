namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        public static TResolver GetResolver<TSymbol, TResolver>() where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            return Inner<TSymbol, TResolver>.Default;
        }

        private static class Inner<TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            public static readonly TResolver Default = new TResolver();
        }
    }
}