namespace SpanJson.Resolvers
{
    public sealed class IncludeNullsCamelCaseResolver<TSymbol> : ResolverBase<TSymbol, IncludeNullsCamelCaseResolver<TSymbol>> where TSymbol : struct
    {
        public IncludeNullsCamelCaseResolver() : base(NullOptions.IncludeNulls, NamingConventions.CamelCase)
        {
        }
    }
}