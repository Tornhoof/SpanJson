using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpanJson.Formatters.Dynamic;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonReader<TSymbol> where TSymbol : struct 
    {
        private readonly ReadOnlySpan<char> _chars;
        private readonly int _length;
        private static readonly char[] NullTerminator = {'\0'};
        private int _pos;

        public JsonReader(ReadOnlySpan<char> input)
        {
            _chars = input;
            _length = input.Length;
            _pos = 0;
        }

    }
}