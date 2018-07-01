using System;
using System.Collections.Generic;
using System.Text;
using SpanJson.Resolvers;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
    {
        //    public CustomResolver() : base(new SpanJsonOptions
        //    {
        //        NullOption = NullOptions.ExcludeNulls,
        //        NamingConvention = NamingConventions.CamelCase,
        //        EnumOption = EnumOptions.Integer
        //    })
        //    {
        //    }
        public CustomResolver() : base(NullOptions.ExcludeNulls, NamingConventions.CamelCase)
        {

        }
    }
}
