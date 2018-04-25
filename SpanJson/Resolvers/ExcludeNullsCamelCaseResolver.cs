namespace SpanJson.Resolvers
{
    public sealed class ExcludeNullsCamelCaseResolver<TSymbol> : ResolverBase<TSymbol, ExcludeNullsCamelCaseResolver<TSymbol>> where TSymbol : struct
    {
        public ExcludeNullsCamelCaseResolver() : base(NullOptions.ExcludeNulls, NamingConventions.CamelCase)
        {
        }
    }
}