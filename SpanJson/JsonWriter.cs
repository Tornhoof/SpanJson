using System;
using System.Buffers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson
{
    public ref partial struct JsonWriter<TSymbol> where TSymbol : struct
    {
        private Span<char> _chars;
        private Span<byte> _bytes;
        private int _pos;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JsonWriter(int initialSize)
        {
            Data = ArrayPool<TSymbol>.Shared.Rent(initialSize);
            _pos = 0;
            _depth = 0;
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
                ThrowNotSupportedException();
                _chars = null;
                _bytes = null;
            }
        }

        public int Position => _pos;

        public TSymbol[] Data { get; private set; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            var toReturn = Data;
            this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int requiredAdditionalCapacity)
        {
            Debug.Assert(requiredAdditionalCapacity > 0);

            var toReturn = Data;
            if (typeof(TSymbol) == typeof(char))
            {
                var poolArray =
                    ArrayPool<TSymbol>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _chars.Length * 2));
                var converted = MemoryMarshal.Cast<TSymbol, char>(poolArray);
                _chars.CopyTo(converted);
                _chars = converted;
                Data = poolArray;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                var poolArray =
                    ArrayPool<TSymbol>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _bytes.Length * 2));
                var converted = MemoryMarshal.Cast<TSymbol, byte>(poolArray);
                _bytes.CopyTo(converted);
                _bytes = converted;
                Data = poolArray;
            }


            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndArray()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16EndArray();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8EndArray();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginArray()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16BeginArray();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8BeginArray();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginObject()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16BeginObject();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8BeginObject();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndObject()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16EndObject();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8EndObject();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueSeparator()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16ValueSeparator();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8ValueSeparator();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Null();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Null();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteName(in ReadOnlySpan<char> name)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Name(name);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Name(name);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteString(in ReadOnlySpan<char> value)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16String(value);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8String(value);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteVerbatim(in ReadOnlySpan<TSymbol> values)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Verbatim(MemoryMarshal.Cast<TSymbol, char>(values));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Verbatim(MemoryMarshal.Cast<TSymbol, byte>(values));
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNewLine()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Verbatim(JsonUtf16Constant.NewLine);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Verbatim(JsonUtf8Constant.NewLine);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteIndentation(int count)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                if (_pos > _chars.Length - count)
                {
                    Grow(count);
                }

                for (var i = 0; i < count; i++)
                {
                    _chars[_pos++] = ' ';
                }
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_pos > _bytes.Length - count)
                {
                    Grow(count);
                }

                for (var i = 0; i < count; i++)
                {
                    _bytes[_pos++] = (byte) ' ';
                }
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteDoubleQuote()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                if (_pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                WriteUtf16DoubleQuote();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_pos > _bytes.Length - 1)
                {
                    Grow(1);
                }

                WriteUtf8DoubleQuote();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNameSeparator()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                if (_pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                WriteUtf16NameSeparator();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_pos > _bytes.Length - 1)
                {
                    Grow(1);
                }

                WriteUtf8NameSeparator();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteVerbatimNameSpan(in ReadOnlySpan<TSymbol> values)
        {
            var remaining = values.Length + 3;
            if (typeof(TSymbol) == typeof(char))
            {
                if (_pos > _chars.Length - remaining)
                {
                    Grow(remaining);
                }

                WriteUtf16DoubleQuote();
                WriteUtf16Verbatim(MemoryMarshal.Cast<TSymbol, char>(values));
                WriteUtf16DoubleQuote();
                _chars[_pos++] = JsonUtf16Constant.NameSeparator;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_pos > _bytes.Length - remaining)
                {
                    Grow(remaining);
                }

                WriteUtf8DoubleQuote();
                WriteUtf8Verbatim(MemoryMarshal.Cast<TSymbol, byte>(values));
                WriteUtf8DoubleQuote();
                _bytes[_pos++] = JsonUtf8Constant.NameSeparator;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        private int _depth;

        public void IncrementDepth() => _depth++;

        public void DecrementDepth() => _depth--;

        public void AssertDepth()
        {
            if (_depth > JsonSharedConstant.NestingLimit)
            {
                throw new InvalidOperationException($"Nesting Limit of {JsonSharedConstant.NestingLimit} exceeded.");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void ThrowArgumentException(string message, string paramName)
        {
            throw new ArgumentException(message, paramName);
        }
    }
}