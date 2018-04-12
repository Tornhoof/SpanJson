namespace SpanJson.Resolvers
{
    public sealed class IncludeNullsCamelCaseResolver : ResolverBase<IncludeNullsCamelCaseResolver>
    {
        public IncludeNullsCamelCaseResolver() : base(NullOptions.IncludeNulls, NamingConventions.CamelCase)
        {
        }
    }
}