using System;
using System.Buffers;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
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
            return _bytes.Slice(0, _pos).ToArray();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8Int64Internal(long value)
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            if (value < 0)
            {
                _bytes[pos++] = (byte) '-';
                value = unchecked(-value);
            }

            WriteUtf8UInt64Internal((ulong) value);
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
            if (!float.IsFinite(value))
            {
                ThrowArgumentException("Invalid float value for JSON", nameof(value));
                return;
            }

            Span<char> span = stackalloc char[JsonSharedConstant.MaxNumberBufferSize];
            value.TryFormat(span, out var written, provider: CultureInfo.InvariantCulture);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - written)
            {
                Grow(written);
            }

            for (var i = 0; i < written; i++)
            {
                _bytes[pos++] = (byte) span[i];
            }
        }

        public void WriteUtf8Double(double value)
        {
            if (!double.IsFinite(value))
            {
                ThrowArgumentException("Invalid double value for JSON", nameof(value));
                return;
            }

            Span<char> span = stackalloc char[JsonSharedConstant.MaxNumberBufferSize];
            value.TryFormat(span, out var written, provider: CultureInfo.InvariantCulture);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - written)
            {
                Grow(written);
            }

            for (var i = 0; i < written; i++)
            {
                _bytes[pos++] = (byte) span[i];
            }
        }

        public void WriteUtf8Decimal(decimal value)
        {
            Span<byte> span = stackalloc byte[JsonSharedConstant.MaxNumberBufferSize];
            Utf8Formatter.TryFormat(value, span, out var bytesWritten);
            ref var pos = ref _pos;
            if (pos > _bytes.Length - bytesWritten)
            {
                Grow(bytesWritten);
            }

            span.Slice(0, bytesWritten).CopyTo(_bytes.Slice(pos));
            pos += bytesWritten;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Boolean(bool value)
        {
            if (value)
            {
                WriteUtf8Verbatim(0x65757274); // True
            }
            else
            {
                WriteUtf8Verbatim(0x736C6166, 0x65); // False
            }
        }

        public void WriteUtf8Char(char value)
        {
            ref var pos = ref _pos;
            const int size = 8; // 1-6 bytes + two JsonUtf8Constant.DoubleQuote
            if (pos > _bytes.Length - size)
            {
                Grow(size);
            }

            WriteUtf8DoubleQuote();
            WriteUtf8CharInternal(value);
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8DateTime(DateTime value)
        {
            ref var pos = ref _pos;
            const int dtSize = JsonSharedConstant.MaxDateTimeLength; // Form o + two JsonUtf8Constant.DoubleQuote
            if (pos > _bytes.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf8DoubleQuote();
            DateTimeFormatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten);
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8DateTimeOffset(DateTimeOffset value)
        {
            ref var pos = ref _pos;
            const int dtSize = JsonSharedConstant.MaxDateTimeOffsetLength; // Form o + two JsonUtf8Constant.DoubleQuote
            if (pos > _bytes.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf8DoubleQuote();
            DateTimeFormatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten);
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8TimeSpan(TimeSpan value)
        {
            ref var pos = ref _pos;
            const int tsSize = JsonSharedConstant.MaxTimeSpanLength; // Form o + two JsonUtf8Constant.DoubleQuote
            if (pos > _bytes.Length - tsSize)
            {
                Grow(tsSize);
            }

            WriteUtf8DoubleQuote();
            Utf8Formatter.TryFormat(value, _bytes.Slice(pos), out var bytesWritten);
            pos += bytesWritten;
            WriteUtf8DoubleQuote();
        }

        public void WriteUtf8Guid(Guid value)
        {
            ref var pos = ref _pos;
            const int guidSize = JsonSharedConstant.MaxGuidLength; // Format D + two JsonUtf8Constant.DoubleQuote;
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
            WriteUtf8String(value.AsSpan());
        }

        public void WriteUtf8String(in ReadOnlySpan<char> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 2; // ascii case + two double quotes
            if (pos > _bytes.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf8DoubleQuote();
            var index = 0;
            var from = 0;
            while (index < value.Length)
            {
                ref readonly var c = ref value[index];
                if (c < 0x20 || c == JsonUtf8Constant.DoubleQuote || c == JsonUtf8Constant.Solidus || c == JsonUtf8Constant.ReverseSolidus)
                {
                    WriteUtf8ReadonlySpan(value.Slice(from, index - from), 7); // The EscapedUtf8Char takes at most 6 bytes + 1 for double quote
                    WriteEscapedUtf8CharInternal(c);
                    from = index + 1;
                }

                index++;
            }

            // Still chars to encode
            if (from < value.Length)
            {
                WriteUtf8ReadonlySpan(value.Slice(from), 1); // make sure we have place for doublequote
            }

            WriteUtf8DoubleQuote();
        }

        /// <summary>
        /// The new Utf8 api from .net core 3, we make sure we have a bit of buffer left to write e.g. double quotes
        /// </summary>
        private void WriteUtf8ReadonlySpan(in ReadOnlySpan<char> value, int extraBytes)
        {
            ref var pos = ref _pos;
            OperationStatus opStatus;
            ReadOnlySpan<char> toWrite = value;
            do
            {
                var output = _bytes.Slice(pos, _bytes.Length - pos - extraBytes);
                opStatus = Utf8.FromUtf16(toWrite, output, out var charsRead, out var bytesWritten);
                pos += bytesWritten;
                if (opStatus == OperationStatus.DestinationTooSmall)
                {
                    Grow(toWrite.Length - charsRead);
                    toWrite = toWrite.Slice(charsRead);
                }
            } while (opStatus != OperationStatus.Done);
        }

        private void WriteEscapedUtf8CharInternal(char value)
        {
            switch (value)
            {
                case JsonUtf16Constant.DoubleQuote:
                    WriteUtf8SingleEscapedChar(JsonUtf16Constant.DoubleQuote);
                    break;
                case JsonUtf16Constant.Solidus:
                    WriteUtf8SingleEscapedChar(JsonUtf16Constant.Solidus);
                    break;
                case JsonUtf16Constant.ReverseSolidus:
                    WriteUtf8SingleEscapedChar(JsonUtf16Constant.ReverseSolidus);
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
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8CharInternal(char value)
        {
            ref var pos = ref _pos;
            if (value < 0x20 || value == JsonUtf8Constant.DoubleQuote || value == JsonUtf8Constant.Solidus || value == JsonUtf8Constant.ReverseSolidus)
            {
                WriteEscapedUtf8CharInternal(value);
            }

            else if (value < 0x80)
            {
                _bytes[pos++] = (byte) value;
            }
            else
            {
                var span = MemoryMarshal.CreateReadOnlySpan(ref value, 1);
                WriteUtf8ReadonlySpan(span, 0);
            }
        }

        public void WriteUtf8Verbatim(byte[] value)
        {
            WriteUtf8Verbatim(value.AsSpan());
        }


        public void WriteUtf8Verbatim(in ReadOnlySpan<byte> value)
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
        public void WriteUtf8Name(in ReadOnlySpan<char> value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 3;
            if (pos > _bytes.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf8DoubleQuote();
            WriteUtf8ReadonlySpan(value, 2);
            WriteUtf8DoubleQuote();
            _bytes[pos++] = JsonUtf8Constant.NameSeparator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8SingleEscapedChar(char toEscape)
        {
            ref var pos = ref _pos;
            var span = _bytes.Slice(pos);
            span[1] = (byte) toEscape;
            span[0] = JsonUtf8Constant.ReverseSolidus;
            pos += 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8DoubleEscapedChar(char firstToEscape, char secondToEscape)
        {
            ref var pos = ref _pos;
            var span = _bytes.Slice(pos);
            span[5] = (byte) secondToEscape;
            span[4] = (byte) firstToEscape;
            span[3] = (byte) '0';
            span[2] = (byte) '0';
            span[1] = (byte) 'u';
            span[0] = JsonUtf8Constant.ReverseSolidus;
            pos += 6;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8BeginObject()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = JsonUtf8Constant.BeginObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8EndObject()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = JsonUtf8Constant.EndObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8BeginArray()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = JsonUtf8Constant.BeginArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8EndArray()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = JsonUtf8Constant.EndArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8ValueSeparator()
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - 1)
            {
                Grow(1);
            }

            _bytes[pos++] = JsonUtf8Constant.ValueSeparator;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Null()
        {
            WriteUtf8Verbatim(0x6C6C756E);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8DoubleQuote()
        {
            _bytes[_pos++] = JsonUtf8Constant.String;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf8NameSeparator()
        {
            _bytes[_pos++] = JsonUtf8Constant.NameSeparator;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Version(Version value)
        {
            ref var pos = ref _pos;
            if (pos > _bytes.Length - JsonSharedConstant.MaxVersionLength)
            {
                Grow(JsonSharedConstant.MaxVersionLength);
            }

            WriteUtf8DoubleQuote();
            Span<char> tempSpan = stackalloc char[JsonSharedConstant.MaxVersionLength];
            value.TryFormat(tempSpan, out _);
            pos += Encoding.UTF8.GetBytes(tempSpan, _bytes.Slice(pos));
            WriteUtf8DoubleQuote();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Uri(Uri value)
        {
            WriteUtf8String(value.OriginalString); // Uri does not implement ISpanFormattable
        }
    }
}