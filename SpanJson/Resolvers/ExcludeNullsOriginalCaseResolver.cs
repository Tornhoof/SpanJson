namespace SpanJson.Resolvers
{
    public sealed class ExcludeNullsOriginalCaseResolver<TSymbol> : ResolverBase<TSymbol, ExcludeNullsOriginalCaseResolver<TSymbol>> where TSymbol : struct
    {
        public ExcludeNullsOriginalCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            NamingConvention = NamingConventions.OriginalCase,
            EnumOption = EnumOptions.String
        })
        {
        }
    }
}