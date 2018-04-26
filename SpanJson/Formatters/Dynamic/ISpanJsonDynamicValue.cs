namespace SpanJson.Formatters.Dynamic
{
    public interface ISpanJsonDynamicValue<out TSymbol> where TSymbol : struct
    {
        TSymbol[] Symbols { get; }
    }
}