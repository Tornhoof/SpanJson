namespace SpanJson.Resolvers
{
    public sealed class ExcludeNullsOriginalCaseResolver : ResolverBase<ExcludeNullsOriginalCaseResolver>
    {
        public ExcludeNullsOriginalCaseResolver() : base(NullOptions.ExcludeNulls, NamingConventions.OriginalCase)
        {
        }
    }
}