namespace SpanJson.Resolvers
{
    public sealed class IncludeNullsOriginalCaseResolver : ResolverBase<IncludeNullsOriginalCaseResolver>
    {
        public IncludeNullsOriginalCaseResolver() : base(NullOptions.IncludeNulls, NamingConventions.OriginalCase)
        {
        }
    }
}