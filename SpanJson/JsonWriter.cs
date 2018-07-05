using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpanJson.Buffers;

namespace SpanJson
{
    public ref partial struct JsonWriter<TSymbol> where TSymbol : struct
    {
        private WriteBuffer<TSymbol> _buffer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public JsonWriter(int initialSize)
        {
            _buffer = new WriteBuffer<TSymbol>(initialSize);
        }

        public int Position => _buffer.Pos;

        public TSymbol[] Data => _buffer.Data;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Dispose()
        {
            _buffer.Dispose();
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int requiredAdditionalCapacity)
        {
            _buffer.Grow(requiredAdditionalCapacity);
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
        public void WriteBoolean(bool value)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Boolean(value);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Boolean(value);
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
                if (_buffer.Pos > _buffer.Chars.Length - count)
                {
                    Grow(count);
                }

                for (var i = 0; i < count; i++)
                {
                    _buffer.Chars[_buffer.Pos++] = ' ';
                }
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_buffer.Pos > _buffer.Bytes.Length - count)
                {
                    Grow(count);
                }

                for (var i = 0; i < count; i++)
                {
                    _buffer.Bytes[_buffer.Pos++] = (byte) ' ';
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
                if (_buffer.Pos > _buffer.Chars.Length - 1)
                {
                    Grow(1);
                }

                WriteUtf16DoubleQuote();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_buffer.Pos > _buffer.Bytes.Length - 1)
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
        public void WriteNameSpan(in ReadOnlySpan<TSymbol> values)
        {
            var remaining = values.Length + 3;
            if (typeof(TSymbol) == typeof(char))
            {
                if (_buffer.Pos > _buffer.Chars.Length - remaining)
                {
                    Grow(remaining);
                }

                WriteUtf16DoubleQuote();
                WriteUtf16Verbatim(MemoryMarshal.Cast<TSymbol, char>(values));
                WriteUtf16DoubleQuote();
                _buffer.Chars[_buffer.Pos++] = JsonUtf16Constant.NameSeparator;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if (_buffer.Pos > _buffer.Bytes.Length - remaining)
                {
                    Grow(remaining);
                }

                WriteUtf8DoubleQuote();
                WriteUtf8Verbatim(MemoryMarshal.Cast<TSymbol, byte>(values));
                WriteUtf8DoubleQuote();
                _buffer.Bytes[_buffer.Pos++] = JsonUtf8Constant.NameSeparator;
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
    }
}