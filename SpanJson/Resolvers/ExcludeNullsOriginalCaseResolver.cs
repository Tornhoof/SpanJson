namespace SpanJson.Resolvers
{
    public sealed class ExcludeNullsOriginalCaseResolver<TSymbol> : ResolverBase<TSymbol, ExcludeNullsOriginalCaseResolver<TSymbol>> where TSymbol : struct 
    {
        public ExcludeNullsOriginalCaseResolver() : base(NullOptions.ExcludeNulls, NamingConventions.OriginalCase)
        {
        }
    }
}