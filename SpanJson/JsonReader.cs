﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson
{
    public ref partial struct JsonReader<TSymbol> where TSymbol : struct
    {
        private readonly ReadOnlySpan<char> _chars;
        private readonly ReadOnlySpan<byte> _bytes;
        private readonly int _length;

        private int _pos;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JsonReader(in ReadOnlySpan<TSymbol> input)
        {
            _length = input.Length;
            _pos = 0;

            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(input);
                _bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(input);
                _chars = null;
            }
            else
            {
                ThrowNotSupportedException();
                _chars = default;
                _bytes = default;
            }
        }

        public int Position => _pos;

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
            throw new NotSupportedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndArrayOrValueSeparator(ref int count)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return TryReadUtf16IsEndArrayOrValueSeparator(ref count);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return TryReadUtf8IsEndArrayOrValueSeparator(ref count);
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object ReadDynamic()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16Dynamic();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8Dynamic();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ReadIsNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16IsNull();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8IsNull();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadEscapedName()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16EscapedName();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8EscapedName();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryReadIsEndObjectOrValueSeparator(ref int count)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return TryReadUtf16IsEndObjectOrValueSeparator(ref count);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return TryReadUtf8IsEndObjectOrValueSeparator(ref count);
            }

            ThrowNotSupportedException();
            return default;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadEndObjectOrThrow()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                ReadUtf16EndObjectOrThrow();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                ReadUtf8EndObjectOrThrow();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ReadEndArrayOrThrow()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                ReadUtf16EndArrayOrThrow();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                ReadUtf8EndArrayOrThrow();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<TSymbol> ReadStringSpan()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return MemoryMarshal.Cast<char, TSymbol>(ReadUtf16StringSpan());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return MemoryMarshal.Cast<byte, TSymbol>(ReadUtf8StringSpan());
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SkipNextSegment()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                SkipNextUtf16Segment();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                SkipNextUtf8Segment();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<TSymbol> ReadNameSpan()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return MemoryMarshal.Cast<char, TSymbol>(ReadUtf16NameSpan());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return MemoryMarshal.Cast<byte, TSymbol>(ReadUtf8NameSpan());
            }

            ThrowNotSupportedException();
            return default;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JsonToken ReadNextToken()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return ReadUtf16NextToken();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return ReadUtf8NextToken();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<TSymbol> ReadNumberSpan()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return MemoryMarshal.Cast<char, TSymbol>(ReadUtf16NumberInternal());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return MemoryMarshal.Cast<byte, TSymbol>(ReadUtf8NumberInternal());
            }

            ThrowNotSupportedException();
            return default;
        }

    }
}