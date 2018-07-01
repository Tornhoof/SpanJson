namespace SpanJson.Resolvers
{
    public sealed class IncludeNullsCamelCaseResolver<TSymbol> : ResolverBase<TSymbol, IncludeNullsCamelCaseResolver<TSymbol>> where TSymbol : struct
    {
        public IncludeNullsCamelCaseResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.IncludeNulls,
            NamingConvention = NamingConventions.CamelCase,
            EnumOption = EnumOptions.String
        })
        {
        }
    }
}