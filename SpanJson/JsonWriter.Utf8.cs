using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonWriter<TSymbol> where TSymbol : struct
    {
        public void WriteUtf8SByte(sbyte value)
        {
            WriteUtf8Int64Internal(value);
        }

        public void WriteUtf8Int16(short value)
        {
            WriteUtf8Int64Internal(value);
        }

        public void WriteUtf8Int32(int value)
        {
            WriteUtf8Int64Internal(value);
        }

        public void WriteUtf8Int64(long value)
        {
            WriteUtf8Int64Internal(value);
        }

        public byte[] ToByteArray()
        {
            var result =  _bytes.Slice(0, _pos).ToArray();
            Dispose();
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8Int64Internal(long value)
        {
            ref var pos = ref _pos;
            if (value == long.MinValue)
            {
                if (pos > _bytes.Length - 21)
                {
                    Grow(21);
                }

                LongMinValueUtf8.AsSpan().TryCopyTo(_bytes.Slice(pos));
                pos += LongMinValueUtf16.Length;
            }
            else if (value < 0)
            {
                if (pos > _bytes.Length - 1)
                {
                    Grow(1);
                }

                _bytes[pos++] = (byte) '-';
                value = unchecked(-value);
            }

            WriteUtf8UInt64Internal((ulong)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8UInt64Internal(ulong value)
        {
            ref var pos = ref _pos;
            if (value < 10)
            {
                if (pos > _bytes.Length - 1)
                {
                    Grow(1);
                }

                _bytes[pos++] = (byte) ('0' + value);
                return;
            }

            var digits = FormatterUtils.CountDigits(value);

            if (pos > _bytes.Length - digits)
            {
                Grow(digits);
            }

            for (var i = digits; i > 0; i--)
            {
                var temp = '0' + value;
                value /= 10;
                _bytes[pos + i - 1] = (byte) (temp - value * 10);
            }

            pos += digits;
        }

        public void WriteUtf8Byte(byte value)
        {
            WriteUtf8UInt64Internal(value);
        }

        public void WriteUtf8UInt16(ushort value)
        {
            WriteUtf8UInt64Internal(value);
        }

        public void WriteUtf8UInt32(uint value)
        {
            WriteUtf8UInt64Internal(value);
        }

        public void WriteUtf8UInt64(ulong value)
        {
            WriteUtf8UInt64Internal(value);
        }

        public void WriteUtf8Single(float value)
        {
            Span<byte> span = stackalloc byte[25]; // TODO find out how long
            Utf8Formatter.TryFormat(value, span, out var bytesWritten);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - bytesWritten)
            {
                Grow(bytesWritten);
            }

            span.Slice(0, bytesWritten).CopyTo(_bytes.Slice(pos));
            pos += bytesWritten;
        }

        public void WriteUtf8Double(double value)
        {
            Span<byte> span = stackalloc byte[50]; // TODO find out how long
            Utf8Formatter.TryFormat(value, span, out var bytesWritten);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - bytesWritten)
            {
                Grow(bytesWritten);
            }

            span.Slice(0, bytesWritten).CopyTo(_bytes.Slice(pos));
            pos += bytesWritten;
        }

        public void WriteUtf8Decimal(decimal value)
        {
            Span<byte> span = stackalloc byte[100]; // TODO find out how long
            Utf8Formatter.TryFormat(value, span, out var bytesWritten);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - bytesWritten)
            {
                Grow(bytesWritten);
            }

            span.Slice(0, bytesWritten).CopyTo(_bytes.Slice(pos));
            pos += bytesWritten;
        }

        public void WriteUtf8Boolean(bool value)
        {
            ref var pos = ref _pos;
            if (value)
            {
                const int trueLength = 4;
                if (pos > _bytes.Length - trueLength)
                {
                    Grow(trueLength);
                }

                _bytes[pos++] = (byte) JsonConstant.True;
                _bytes[pos++] = (byte)'r';
                _bytes[pos++] = (byte)'u';
                _bytes[pos++] = (byte)'e';
            }
            else
            {
                const int falseLength = 5;
                if (pos > _bytes.Length - falseLength)
                {
                    Grow(falseLength);
                }

                _bytes[pos++] = (byte)JsonConstant.False;
                _bytes[pos++] = (byte)'a';
                _bytes[pos++] = (byte)'l';
                _bytes[pos++] = (byte)'s';
                _bytes[pos++] = (byte)'e';
            }
        }

        public void WriteUtf8Char(char value)
        {
            ref var pos = ref _pos;
            const int size = 8; // 1-6 chars + two JsonConstant.DoubleQuote
            if (pos > _bytes.Length - size)
            {
                Grow(size);
            }

            WriteUtf8DoubleQuote();
            char[] array = null;
            try
            {
                array = ArrayPool<char>.Shared.Rent(1);
                array[0] = value; // TODO find better way
                WriteUtf8CharInternal(ref pos, array, 0);
            }
            finally
            {
                if (array != null)
                {
                    ArrayPool<char>.Shared.Return(array);
                }
            }

            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8DateTime(DateTime value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two JsonConstant.DoubleQuote
            if (pos > _bytes.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf8DoubleQuote();
            Utf8Formatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten, 'O');
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8DateTimeOffset(DateTimeOffset value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two JsonConstant.DoubleQuote
            if (pos > _bytes.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf8DoubleQuote();
            Utf8Formatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten, 'O');
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8TimeSpan(TimeSpan value)
        {
            ref var pos = ref _pos;
            const int dtSize = 20; // Form o + two JsonConstant.DoubleQuote
            if (pos > _bytes.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf8DoubleQuote();
            Utf8Formatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten);
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8Guid(Guid value)
        {
            ref var pos = ref _pos;
            const int guidSize = 42; // Format D + two JsonConstant.DoubleQuote;
            if (pos > _bytes.Length - guidSize)
            {
                Grow(guidSize);
            }

            WriteUtf8DoubleQuote();
            Utf8Formatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten);
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8String(string value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 2;
            if (pos > _bytes.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf8DoubleQuote();
            var span = value.AsSpan();
            for (int i = 0; i < span.Length; i++)
            {
                WriteUtf8CharInternal(ref pos, span, i);
            }

            WriteUtf8DoubleQuote();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8CharInternal(ref int pos, ReadOnlySpan<char> span, int offset)
        {
            ref readonly var current = ref span[offset];
            switch (current)
            {
                case JsonConstant.DoubleQuote:
                    WriteUtf8SingleEscapedChar(JsonConstant.DoubleQuote);
                    break;
                case JsonConstant.ReverseSolidus:
                    WriteUtf8SingleEscapedChar(JsonConstant.ReverseSolidus);
                    break;
                case '\b':
                    WriteUtf8SingleEscapedChar('b');
                    break;
                case '\f':
                    WriteUtf8SingleEscapedChar('f');
                    break;
                case '\n':
                    WriteUtf8SingleEscapedChar('n');
                    break;
                case '\r':
                    WriteUtf8SingleEscapedChar('r');
                    break;
                case '\t':
                    WriteUtf8SingleEscapedChar('t');
                    break;
                case '\x0':
                    WriteUtf8DoubleEscapedChar('0', '0');
                    break;
                case '\x1':
                    WriteUtf8DoubleEscapedChar('0', '1');
                    break;
                case '\x2':
                    WriteUtf8DoubleEscapedChar('0', '2');
                    break;
                case '\x3':
                    WriteUtf8DoubleEscapedChar('0', '3');
                    break;
                case '\x4':
                    WriteUtf8DoubleEscapedChar('0', '4');
                    break;
                case '\x5':
                    WriteUtf8DoubleEscapedChar('0', '5');
                    break;
                case '\x6':
                    WriteUtf8DoubleEscapedChar('0', '6');
                    break;
                case '\x7':
                    WriteUtf8DoubleEscapedChar('0', '7');
                    break;
                case '\xB':
                    WriteUtf8DoubleEscapedChar('0', 'B');
                    break;
                case '\xE':
                    WriteUtf8DoubleEscapedChar('0', 'E');
                    break;
                case '\xF':
                    WriteUtf8DoubleEscapedChar('0', 'F');
                    break;
                case '\x10':
                    WriteUtf8DoubleEscapedChar('1', '0');
                    break;
                case '\x11':
                    WriteUtf8DoubleEscapedChar('1', '1');
                    break;
                case '\x12':
                    WriteUtf8DoubleEscapedChar('1', '2');
                    break;
                case '\x13':
                    WriteUtf8DoubleEscapedChar('1', '3');
                    break;
                case '\x14':
                    WriteUtf8DoubleEscapedChar('1', '4');
                    break;
                case '\x15':
                    WriteUtf8DoubleEscapedChar('1', '5');
                    break;
                case '\x16':
                    WriteUtf8DoubleEscapedChar('1', '6');
                    break;
                case '\x17':
                    WriteUtf8DoubleEscapedChar('1', '7');
                    break;
                case '\x18':
                    WriteUtf8DoubleEscapedChar('1', '8');
                    break;
                case '\x19':
                    WriteUtf8DoubleEscapedChar('1', '9');
                    break;
                case '\x1A':
                    WriteUtf8DoubleEscapedChar('1', 'A');
                    break;
                case '\x1B':
                    WriteUtf8DoubleEscapedChar('1', 'B');
                    break;
                case '\x1C':
                    WriteUtf8DoubleEscapedChar('1', 'C');
                    break;
                case '\x1D':
                    WriteUtf8DoubleEscapedChar('1', 'D');
                    break;
                case '\x1E':
                    WriteUtf8DoubleEscapedChar('1', 'E');
                    break;
                case '\x1F':
                    WriteUtf8DoubleEscapedChar('1', 'F');
                    break;
                default:
                    if (current < 0x100)
                    {
                        _bytes[pos++] = (byte) current;
                    }
                    else
                    {
                        WriteNonAsciiUtf8Char(ref pos, span);
                    }

                    break;
            }
        }

        private void WriteNonAsciiUtf8Char(ref int pos, ReadOnlySpan<char> span)
        {
            var spanOffset = 0;
            bool completed;
            do
            {
                _encoder.Convert(span.Slice(spanOffset, 1), _bytes.Slice(pos), false, out var charsUsed, out var bytesUsed, out completed);
                if (!completed)
                {
                    spanOffset += charsUsed;
                    Grow(4);
                }

                pos += bytesUsed;
            } while (!completed);

            _encoder.Reset();
        }

        public void WriteUtf8Verbatim(byte[] value)
        {
            WriteUtf8Verbatim(value.AsSpan());
        }

        public void WriteUtf8Verbatim(ReadOnlySpan<byte> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length;
            if (pos > _bytes.Length - sLength)
            {
                Grow(sLength);
            }

            value.CopyTo(_bytes.Slice(pos));
            pos += value.Length;
        }

        /// <summary>
        ///     The value should already be properly escaped
        /// </summary>
        /// <param name="value"></param>
        public void WriteUtf8Name(string value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 3;
            if (pos > _bytes.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf8DoubleQuote();
            _encoder.Convert(value, _bytes.Slice(pos), true, out _, out var bytesUsed, out _);
            _encoder.Reset();
            pos += bytesUsed;
            WriteUtf8DoubleQuote();
            _bytes[pos++] = (byte) JsonConstant.NameSeparator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8SingleEscapedChar(char toEscape)
        {
            ref var pos = ref _pos;
            _bytes[pos++] = (byte) JsonConstant.ReverseSolidus;
            _bytes[pos++] = (byte) toEscape;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8DoubleEscapedChar(char firstToEscape, char secondToEscape)
        {
            ref var pos = ref _pos;
            _bytes[pos++] = (byte) JsonConstant.ReverseSolidus;
            _bytes[pos++] = (byte) 'u';
            _bytes[pos++] = (byte) '0';
            _bytes[pos++] = (byte) '0';
            _bytes[pos++] = (byte) firstToEscape;
            _bytes[pos++] = (byte) secondToEscape;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8BeginObject()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = (byte)JsonConstant.BeginObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8EndObject()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = (byte)JsonConstant.EndObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8BeginArray()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = (byte)JsonConstant.BeginArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8EndArray()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = (byte)JsonConstant.EndArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8ValueSeparator()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = (byte)JsonConstant.ValueSeparator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Null()
        {
            ref var pos = ref _pos;
            const int nullLength = 4;
            if (pos > _bytes.Length - nullLength)
            {
                Grow(nullLength);
            }

            _bytes[pos++] = (byte)JsonConstant.Null;
            _bytes[pos++] = (byte)'u';
            _bytes[pos++] = (byte)'l';
            _bytes[pos++] = (byte)'l';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8DoubleQuote()
        {
            _bytes[_pos++] = (byte)JsonConstant.String;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Version(Version value)
        {
            ref var pos = ref _pos;
            const int versionLength = 45; // 4 * int + 3 . + 2 double quote
            if (pos > _bytes.Length - versionLength)
            {
                Grow(versionLength);
            }

            WriteUtf8DoubleQuote();
            Span<char> tempSpan = stackalloc char[45];
            value.TryFormat(tempSpan, out _);
            _encoder.Convert(tempSpan, _bytes.Slice(pos), true, out _, out var bytesUsed, out _);
            _encoder.Reset();
            pos += bytesUsed;
            WriteUtf8DoubleQuote();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Uri(Uri value)
        {
            WriteUtf8String(value.ToString()); // Uri does not implement ISpanFormattable
        }
    }
}
