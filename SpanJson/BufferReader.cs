using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

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

            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(Data);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(Data);
                _chars = null;
            }
            else
            {
                _chars = default;
                _bytes = default;
            }
        }

        public ReadOnlySpan<char> Chars => _chars;
        public ReadOnlySpan<byte> Bytes => _bytes;
        private ReadOnlySpan<char> _chars;
        private ReadOnlySpan<byte> _bytes;
    }
}
