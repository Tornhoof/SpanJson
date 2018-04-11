namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        public static TResolver GetResolver<TResolver>() where TResolver : IJsonFormatterResolver, new()
        {
            return Inner<TResolver>.Default;
        }

        private static class Inner<TResolver> where TResolver : IJsonFormatterResolver, new()
        {
            public static readonly TResolver Default = new TResolver();
        }
    }
}