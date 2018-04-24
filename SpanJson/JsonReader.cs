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
        private readonly ReadOnlySpan<byte> _bytes;
        private readonly int _length;
        private static readonly char[] NullTerminatorUtf16 = {'\0'};
        private static readonly byte[] NullTerminatorUtf8 = {(byte) NullTerminatorUtf16[0]};
        private int _pos;

        public JsonReader(ReadOnlySpan<TSymbol> input)
        {
            _length = input.Length;
            _pos = 0;

            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(input);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(input);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowJsonParserException(JsonParserException.ParserError error, Type type)
        {
            throw new JsonParserException(error, type, _pos);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowJsonParserException(JsonParserException.ParserError error)
        {
            throw new JsonParserException(error, _pos);
        }

    }
}