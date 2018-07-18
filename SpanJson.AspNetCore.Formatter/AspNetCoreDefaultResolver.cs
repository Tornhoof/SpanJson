using SpanJson.Resolvers;

namespace SpanJson.AspNetCore.Formatter
{
    public class AspNetCoreDefaultResolver<TSymbol> : ResolverBase<TSymbol, AspNetCoreDefaultResolver<TSymbol>> where TSymbol : struct
    {
        public AspNetCoreDefaultResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.IncludeNulls,
            NamingConvention = NamingConventions.CamelCase,
            EnumOption = EnumOptions.Integer
        })
        {
        }
    }
}