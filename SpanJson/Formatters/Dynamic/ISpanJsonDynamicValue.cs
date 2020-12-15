using System;
using System.Diagnostics.CodeAnalysis;

namespace SpanJson.Formatters.Dynamic
{
    public interface ISpanJsonDynamicValue<out TSymbol> : ISpanJsonDynamic where TSymbol : struct
    {
        TSymbol[] Symbols { get; }
        bool TryConvert(Type outputType, [MaybeNullWhen(false)] out object? result);
    }
}