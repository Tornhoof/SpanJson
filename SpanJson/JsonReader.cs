using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
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
        private readonly Decoder _decoder;

        public JsonReader(ReadOnlySpan<TSymbol> input)
        {
            _length = input.Length;
            _pos = 0;

            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(input);
                _decoder = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(input);
                _decoder = Encoding.UTF8.GetDecoder();
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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadBeginArrayOrThrow()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                ReadUtf16BeginArrayOrThrow();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                ReadUtf8BeginArrayOrThrow();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndArrayOrValueSeparator(ref int count)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                return TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object ReadDynamic()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16Dynamic();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8Dynamic();
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16IsNull();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8IsNull();
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadEscapedName()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16EscapedName();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8EscapedName();
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndObjectOrValueSeparator(ref int count)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return TryReadUtf16IsEndObjectOrValueSeparator(ref count);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                return TryReadUtf8IsEndObjectOrValueSeparator(ref count);
            }
            else
            {
                ThrowNotSupportedException();
                return default;
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadBeginObjectOrThrow()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                ReadUtf16BeginObjectOrThrow();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                ReadUtf8BeginObjectOrThrow();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }
    }
}