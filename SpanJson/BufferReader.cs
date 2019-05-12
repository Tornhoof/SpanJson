using System;
using System.Runtime.CompilerServices;

namespace SpanJson
{
    public ref struct BufferReader<TSymbol> where TSymbol : struct
    {
        public ReadOnlySpan<TSymbol> Data;
        public int Length;

        public BufferReader(in ReadOnlySpan<TSymbol> data)
        {
            Data = data;
            Length = data.Length;
        }
    }
}
