using System;

namespace SpanJson.Formatters.Dynamic
{
    public interface ISpanJsonDynamicValue<out TSymbol> : ISpanJsonDynamic where TSymbol : struct
    {
        TSymbol[] Symbols { get; }
        bool TryConvert(Type outputType, out object result);
    }
}