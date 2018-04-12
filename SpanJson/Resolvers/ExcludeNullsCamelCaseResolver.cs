namespace SpanJson.Resolvers
{
    public sealed class ExcludeNullsCamelCaseResolver : ResolverBase<ExcludeNullsCamelCaseResolver>
    {
        public ExcludeNullsCamelCaseResolver() : base(NullOptions.ExcludeNulls, NamingConventions.CamelCase)
        {
        }
    }
}