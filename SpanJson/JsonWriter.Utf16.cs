using System;
using System.Buffers.Text;
using System.Globalization;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonWriter<TSymbol> where TSymbol : struct
    {
        public void WriteUtf16SByte(sbyte value)
        {
            WriteUtf16Int64Internal(value);
        }

        public void WriteUtf16Int16(short value)
        {
            WriteUtf16Int64Internal(value);
        }

        public void WriteUtf16Int32(int value)
        {
            WriteUtf16Int64Internal(value);
        }

        public void WriteUtf16Int64(long value)
        {
            WriteUtf16Int64Internal(value);
        }

        public override string ToString()
        {
            return _chars.Slice(0, _pos).ToString();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16Int64Internal(long value)
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            if (value < 0)
            {
                _chars[pos++] = '-';
                value = unchecked(-value);
            }

            WriteUtf16UInt64Internal((ulong)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16UInt64Internal(ulong value)
        {
            ref var pos = ref _pos;
            if (value < 10)
            {
                if (pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                _chars[pos++] = (char)('0' + value);
                return;
            }

            var digits = FormatterUtils.CountDigits(value);

            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            for (var i = digits; i > 0; i--)
            {
                var temp = '0' + value;
                value /= 10;
                _chars[pos + i - 1] = (char)(temp - value * 10);
            }

            pos += digits;
        }

        public void WriteUtf16Byte(byte value)
        {
            WriteUtf16UInt64Internal(value);
        }

        public void WriteUtf16UInt16(ushort value)
        {
            WriteUtf16UInt64Internal(value);
        }

        public void WriteUtf16UInt32(uint value)
        {
            WriteUtf16UInt64Internal(value);
        }

        public void WriteUtf16UInt64(ulong value)
        {
            WriteUtf16UInt64Internal(value);
        }

        public void WriteUtf16Single(float value)
        {
            if (!float.IsFinite(value))
            {
                ThrowArgumentException("Invalid float value for JSON", nameof(value));
                return;
            }

            Span<char> span = stackalloc char[JsonSharedConstant.MaxNumberBufferSize];
            value.TryFormat(span, out var written, provider: CultureInfo.InvariantCulture);
            ref var pos = ref _pos;
            if (pos > _chars.Length - written)
            {
                Grow(written);
            }

            span.Slice(0, written).CopyTo(_chars.Slice(pos));
            pos += written;
        }

        public void WriteUtf16Double(double value)
        {
            if (!double.IsFinite(value))
            {
                ThrowArgumentException("Invalid double value for JSON", nameof(value));
                return;
            }

            Span<char> span = stackalloc char[JsonSharedConstant.MaxNumberBufferSize];
            value.TryFormat(span, out var written, provider: CultureInfo.InvariantCulture);
            ref var pos = ref _pos;
            if (pos > _chars.Length - written)
            {
                Grow(written);
            }

            span.Slice(0, written).CopyTo(_chars.Slice(pos));
            pos += written;
        }

        public void WriteUtf16Decimal(decimal value)
        {
            Span<char> span = stackalloc char[JsonSharedConstant.MaxNumberBufferSize];
            value.TryFormat(span, out var written, provider: CultureInfo.InvariantCulture);
            ref var pos = ref _pos;
            if (pos > _chars.Length - written)
            {
                Grow(written);
            }

            span.Slice(0, written).CopyTo(_chars.Slice(pos));
            pos += written;
        }

        public void WriteUtf16Boolean(bool value)
        {
            const int trueLength = 4;
            const int falseLength = 5;
            if (_pos > _chars.Length - falseLength)
            {
                Grow(falseLength);
            }

            var span = _chars.Slice(_pos);
            if (value)
            {
                span[3] = 'e';
                span[2] = 'u';
                span[1] = 'r';
                span[0] = JsonUtf16Constant.True;
                _pos += trueLength;
            }
            else
            {
                span[4] = 'e';
                span[3] = 's';
                span[2] = 'l';
                span[1] = 'a';
                span[0] = JsonUtf16Constant.False;
                _pos += falseLength;
            }
        }

        private void WriteEscapedUtf16CharInternal(char value)
        {
            switch (value)
            {
                case JsonUtf16Constant.DoubleQuote:
                    WriteUtf16SingleEscapedChar(JsonUtf16Constant.DoubleQuote);
                    break;
                case JsonUtf16Constant.Solidus:
                    WriteUtf16SingleEscapedChar(JsonUtf16Constant.Solidus);
                    break;
                case JsonUtf16Constant.ReverseSolidus:
                    WriteUtf16SingleEscapedChar(JsonUtf16Constant.ReverseSolidus);
                    break;
                case '\b':
                    WriteUtf16SingleEscapedChar('b');
                    break;
                case '\f':
                    WriteUtf16SingleEscapedChar('f');
                    break;
                case '\n':
                    WriteUtf16SingleEscapedChar('n');
                    break;
                case '\r':
                    WriteUtf16SingleEscapedChar('r');
                    break;
                case '\t':
                    WriteUtf16SingleEscapedChar('t');
                    break;
                case '\x0':
                    WriteUtf16DoubleEscapedChar('0', '0');
                    break;
                case '\x1':
                    WriteUtf16DoubleEscapedChar('0', '1');
                    break;
                case '\x2':
                    WriteUtf16DoubleEscapedChar('0', '2');
                    break;
                case '\x3':
                    WriteUtf16DoubleEscapedChar('0', '3');
                    break;
                case '\x4':
                    WriteUtf16DoubleEscapedChar('0', '4');
                    break;
                case '\x5':
                    WriteUtf16DoubleEscapedChar('0', '5');
                    break;
                case '\x6':
                    WriteUtf16DoubleEscapedChar('0', '6');
                    break;
                case '\x7':
                    WriteUtf16DoubleEscapedChar('0', '7');
                    break;
                case '\xB':
                    WriteUtf16DoubleEscapedChar('0', 'B');
                    break;
                case '\xE':
                    WriteUtf16DoubleEscapedChar('0', 'E');
                    break;
                case '\xF':
                    WriteUtf16DoubleEscapedChar('0', 'F');
                    break;
                case '\x10':
                    WriteUtf16DoubleEscapedChar('1', '0');
                    break;
                case '\x11':
                    WriteUtf16DoubleEscapedChar('1', '1');
                    break;
                case '\x12':
                    WriteUtf16DoubleEscapedChar('1', '2');
                    break;
                case '\x13':
                    WriteUtf16DoubleEscapedChar('1', '3');
                    break;
                case '\x14':
                    WriteUtf16DoubleEscapedChar('1', '4');
                    break;
                case '\x15':
                    WriteUtf16DoubleEscapedChar('1', '5');
                    break;
                case '\x16':
                    WriteUtf16DoubleEscapedChar('1', '6');
                    break;
                case '\x17':
                    WriteUtf16DoubleEscapedChar('1', '7');
                    break;
                case '\x18':
                    WriteUtf16DoubleEscapedChar('1', '8');
                    break;
                case '\x19':
                    WriteUtf16DoubleEscapedChar('1', '9');
                    break;
                case '\x1A':
                    WriteUtf16DoubleEscapedChar('1', 'A');
                    break;
                case '\x1B':
                    WriteUtf16DoubleEscapedChar('1', 'B');
                    break;
                case '\x1C':
                    WriteUtf16DoubleEscapedChar('1', 'C');
                    break;
                case '\x1D':
                    WriteUtf16DoubleEscapedChar('1', 'D');
                    break;
                case '\x1E':
                    WriteUtf16DoubleEscapedChar('1', 'E');
                    break;
                case '\x1F':
                    WriteUtf16DoubleEscapedChar('1', 'F');
                    break;
            }
        }

        public void WriteUtf16Char(char value)
        {
            ref var pos = ref _pos;
            const int size = 8; // 1-6 chars + two JsonUtf16Constant.DoubleQuote
            if (pos > _chars.Length - size)
            {
                Grow(size);
            }

            WriteUtf16DoubleQuote();
            if (value < 0x20 || value == JsonUtf16Constant.DoubleQuote || value == JsonUtf16Constant.Solidus || value == JsonUtf16Constant.ReverseSolidus)
            {
                WriteEscapedUtf16CharInternal(value);
            }
            else
            {
                _chars[pos++] = value;
            }

            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16DateTime(DateTime value)
        {
            ref var pos = ref _pos;
            const int dtSize = JsonSharedConstant.MaxDateTimeLength; // Form o + two JsonUtf16Constant.DoubleQuote
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf16DoubleQuote();
            DateTimeFormatter.TryFormat(value, _chars.Slice(pos), out var written);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16DateTimeOffset(DateTimeOffset value)
        {
            ref var pos = ref _pos;
            const int dtSize = JsonSharedConstant.MaxDateTimeOffsetLength; // Form o + two JsonUtf16Constant.DoubleQuote
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf16DoubleQuote();
            DateTimeFormatter.TryFormat(value, _chars.Slice(pos), out var written);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16TimeSpan(TimeSpan value)
        {
            ref var pos = ref _pos;
            const int tsSize = JsonSharedConstant.MaxTimeSpanLength; // Form c + two JsonUtf16Constant.DoubleQuote
            if (pos > _chars.Length - tsSize)
            {
                Grow(tsSize);
            }

            WriteUtf16DoubleQuote();
            Span<byte> byteSpan = stackalloc byte[tsSize];
            Utf8Formatter.TryFormat(value, byteSpan, out var bytesWritten);
            for (var i = 0; i < bytesWritten; i++)
            {
                _chars[i + pos] = (char)byteSpan[i];
            }

            pos += bytesWritten;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16Guid(Guid value)
        {
            ref var pos = ref _pos;
            const int guidSize = JsonSharedConstant.MaxGuidLength; // Format D + two JsonUtf16Constant.DoubleQuote;
            if (pos > _chars.Length - guidSize)
            {
                Grow(guidSize);
            }

            WriteUtf16DoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16String(string value)
        {
            WriteUtf16String(value.AsSpan());
        }

        public void WriteUtf16String(in ReadOnlySpan<char> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 7; // assume that a fully escaped char fits too (5 + two double quotes)
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf16DoubleQuote();
            for (var i = 0; i < value.Length; i++)
            {
                ref readonly var c = ref value[i];
                if (c < 0x20 || c == JsonUtf16Constant.DoubleQuote || c == JsonUtf16Constant.Solidus || c == JsonUtf16Constant.ReverseSolidus)
                {
                    WriteEscapedUtf16CharInternal(c);
                    var remaining = 5 + value.Length - i; // make sure that all characters and an extra 5 for a full escape still fit
                    if (pos > _chars.Length - remaining)
                    {
                        Grow(remaining);
                    }
                }
                else
                {
                    _chars[pos++] = c;
                }
            }

            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16Verbatim(string value)
        {
            WriteUtf16Verbatim(value.AsSpan());
        }

        public void WriteUtf16Verbatim(in ReadOnlySpan<char> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            value.CopyTo(_chars.Slice(pos));
            pos += value.Length;
        }

        /// <summary>
        ///     The value should already be properly escaped
        /// </summary>
        /// <param name="value"></param>
        public void WriteUtf16Name(in ReadOnlySpan<char> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 3;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf16DoubleQuote();
            value.CopyTo(_chars.Slice(pos));
            pos += value.Length;
            WriteUtf16DoubleQuote();
            _chars[pos++] = JsonUtf16Constant.NameSeparator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16SingleEscapedChar(char toEscape)
        {
            ref var pos = ref _pos;
            var span = _chars.Slice(pos);
            span[1] = toEscape;
            span[0] = JsonUtf16Constant.ReverseSolidus;
            pos += 2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16DoubleEscapedChar(char firstToEscape, char secondToEscape)
        {
            ref var pos = ref _pos;
            var span = _chars.Slice(pos);
            span[5] = secondToEscape;
            span[4] = firstToEscape;
            span[3] = '0';
            span[2] = '0';
            span[1] = 'u';
            span[0] = JsonUtf16Constant.ReverseSolidus;
            pos += 6;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16BeginObject()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonUtf16Constant.BeginObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16EndObject()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonUtf16Constant.EndObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16BeginArray()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonUtf16Constant.BeginArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16EndArray()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonUtf16Constant.EndArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16ValueSeparator()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonUtf16Constant.ValueSeparator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Null()
        {
            ref var pos = ref _pos;
            const int nullLength = 4;
            if (pos > _chars.Length - nullLength)
            {
                Grow(nullLength);
            }

            var span = _chars.Slice(pos);
            span[3] = 'l';
            span[2] = 'l';
            span[1] = 'u';
            span[0] = JsonUtf16Constant.Null;
            pos += 4;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16DoubleQuote()
        {
            _chars[_pos++] = JsonUtf16Constant.String;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16NameSeparator()
        {
            _chars[_pos++] = JsonUtf16Constant.NameSeparator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Version(Version value)
        {
            ref var pos = ref _pos;
            const int versionLength = JsonSharedConstant.MaxVersionLength;
            if (pos > _chars.Length - versionLength)
            {
                Grow(versionLength);
            }

            WriteUtf16DoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written);
            pos += written;
            WriteUtf16DoubleQuote();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Uri(Uri value)
        {
            WriteUtf16String(value.ToString()); // Uri does not implement ISpanFormattable
        }

        public void WriteUtf16Base64EncodedArray(in ReadOnlySpan<byte> value)
        {
            ref var pos = ref _pos;
            var expectedLength = ((4 * value.Length / 3) + 3) & ~3;
            if (pos > _chars.Length - expectedLength)
            {
                Grow(expectedLength);
            }

            WriteUtf16DoubleQuote();
            if (!Convert.TryToBase64Chars(value, _chars.Slice(pos), out var written) || written != expectedLength)
            {
                ThrowArgumentException("Can't encode Base64 array.", nameof(value));
            }

            pos += written;
            WriteUtf16DoubleQuote();
        }
    }
}