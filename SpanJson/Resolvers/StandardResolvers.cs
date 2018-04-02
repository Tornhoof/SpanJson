using System;
using System.Collections.Generic;
using System.Text;

namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        public static readonly IJsonFormatterResolver Default = new DefaultResolver();
    }
}
