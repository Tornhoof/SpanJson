namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        public static TResolver GetResolver<TResolver>() where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            return Inner<TResolver>.Default;
        }

        private static class Inner<TResolver> where TResolver : IJsonFormatterResolver<TResolver>, new()
        {
            public static readonly TResolver Default = new TResolver();
        }
    }
}