namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        public static readonly IJsonFormatterResolver Default = new DefaultResolver();
    }
}