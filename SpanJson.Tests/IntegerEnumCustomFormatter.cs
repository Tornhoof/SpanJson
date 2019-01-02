using SpanJson.Resolvers;

namespace SpanJson.Tests
{
    public sealed class ExcludeNullCamelCaseIntegerEnumResolver<TSymbol> : ResolverBase<TSymbol, ExcludeNullCamelCaseIntegerEnumResolver<TSymbol>>
        where TSymbol : struct
    {
        public ExcludeNullCamelCaseIntegerEnumResolver() : base(new SpanJsonOptions
        {
            NullOption = NullOptions.ExcludeNulls,
            NamingConvention = NamingConventions.CamelCase,
            EnumOption = EnumOptions.Integer
        })
        {
        }
    }
}