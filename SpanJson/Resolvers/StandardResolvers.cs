using System.Runtime.CompilerServices;

namespace SpanJson.Resolvers
{
    public static class StandardResolvers
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TResolver GetResolver<TSymbol, TResolver>() where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
#if NET7_0_OR_GREATER
            return TResolver.Default;
#else
            return Inner<TSymbol, TResolver>.Default;
#endif
        }

#if NET7_0_OR_GREATER
#else
        private static class Inner<TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            public static readonly TResolver Default = CreateResolver();

            private static TResolver CreateResolver()
            {
                var result = new TResolver();

                return result;
            }
        }
#endif
    }
}