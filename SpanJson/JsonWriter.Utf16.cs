using System;
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
            var s = _chars.Slice(0, _pos).ToString();
            Dispose();
            return s;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16Int64Internal(long value)
        {
            ref var pos = ref _pos;
            if (value == long.MinValue)
            {
                if (pos > _chars.Length - 21)
                {
                    Grow(21);
                }

                LongMinValueUtf16.AsSpan().TryCopyTo(_chars.Slice(pos));
                pos += LongMinValueUtf16.Length;
            }
            else if (value < 0)
            {
                if (pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                _chars[pos++] = '-';
                value = unchecked(-value);
            }

            WriteUtf16UInt64Internal((ulong) value);
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

                _chars[pos++] = (char) ('0' + value);
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
                _chars[pos + i - 1] = (char) (temp - value * 10);
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
            Span<char> span = stackalloc char[25]; // TODO find out how long
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
            Span<char> span = stackalloc char[50]; // TODO find out how long
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
            Span<char> span = stackalloc char[100]; // TODO find out how long
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
            ref var pos = ref _pos;
            if (value)
            {
                const int trueLength = 4;
                if (pos > _chars.Length - trueLength)
                {
                    Grow(trueLength);
                }

                _chars[pos++] = JsonConstant.True;
                _chars[pos++] = 'r';
                _chars[pos++] = 'u';
                _chars[pos++] = 'e';
            }
            else
            {
                const int falseLength = 5;
                if (pos > _chars.Length - falseLength)
                {
                    Grow(falseLength);
                }

                _chars[pos++] = JsonConstant.False;
                _chars[pos++] = 'a';
                _chars[pos++] = 'l';
                _chars[pos++] = 's';
                _chars[pos++] = 'e';
            }
        }

        public void WriteUtf16Char(char value)
        {
            ref var pos = ref _pos;
            const int size = 8; // 1-6 chars + two JsonConstant.DoubleQuote
            if (pos > _chars.Length - size)
            {
                Grow(size);
            }

            WriteUtf16DoubleQuote();
            switch (value)
            {
                case JsonConstant.DoubleQuote:
                    WriteUtf16SingleEscapedChar(JsonConstant.DoubleQuote);
                    break;
                case JsonConstant.ReverseSolidus:
                    WriteUtf16SingleEscapedChar(JsonConstant.ReverseSolidus);
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
                default:
                    _chars[pos++] = value;
                    break;
            }

            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16DateTime(DateTime value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two JsonConstant.DoubleQuote
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf16DoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, "O", CultureInfo.InvariantCulture);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16DateTimeOffset(DateTimeOffset value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two JsonConstant.DoubleQuote
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf16DoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, "O", CultureInfo.InvariantCulture);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16TimeSpan(TimeSpan value)
        {
            ref var pos = ref _pos;
            const int dtSize = 20; // Form o + two JsonConstant.DoubleQuote
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteUtf16DoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, "c", CultureInfo.InvariantCulture);
            pos += written;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16Guid(Guid value)
        {
            ref var pos = ref _pos;
            const int guidSize = 42; // Format D + two JsonConstant.DoubleQuote;
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
            ref var pos = ref _pos;
            var sLength = value.Length + 2;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf16DoubleQuote();
            var remaining = value.AsSpan();
            for (var i = 0; i < remaining.Length; i++)
            {
                ref readonly var c = ref remaining[i];
                switch (c)
                {
                    case JsonConstant.DoubleQuote:
                        CopyUtf16AndEscape(ref remaining, ref i, JsonConstant.DoubleQuote);
                        break;
                    case JsonConstant.ReverseSolidus:
                        CopyUtf16AndEscape(ref remaining, ref i, JsonConstant.ReverseSolidus);
                        break;
                    case '\b':
                        CopyUtf16AndEscape(ref remaining, ref i, 'b');
                        break;
                    case '\f':
                        CopyUtf16AndEscape(ref remaining, ref i, 'f');
                        break;
                    case '\n':
                        CopyUtf16AndEscape(ref remaining, ref i, 'n');
                        break;
                    case '\r':
                        CopyUtf16AndEscape(ref remaining, ref i, 'r');
                        break;
                    case '\t':
                        CopyUtf16AndEscape(ref remaining, ref i, 't');
                        break;
                    case '\x0':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '0');
                        break;
                    case '\x1':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '1');
                        break;
                    case '\x2':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '2');
                        break;
                    case '\x3':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '3');
                        break;
                    case '\x4':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '4');
                        break;
                    case '\x5':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '5');
                        break;
                    case '\x6':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '6');
                        break;
                    case '\x7':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', '7');
                        break;
                    case '\xB':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', 'B');
                        break;
                    case '\xE':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', 'E');
                        break;
                    case '\xF':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '0', 'F');
                        break;
                    case '\x10':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '0');
                        break;
                    case '\x11':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '1');
                        break;
                    case '\x12':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '2');
                        break;
                    case '\x13':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '3');
                        break;
                    case '\x14':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '4');
                        break;
                    case '\x15':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '5');
                        break;
                    case '\x16':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '6');
                        break;
                    case '\x17':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '7');
                        break;
                    case '\x18':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '8');
                        break;
                    case '\x19':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', '9');
                        break;
                    case '\x1A':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'A');
                        break;
                    case '\x1B':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'B');
                        break;
                    case '\x1C':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'C');
                        break;
                    case '\x1D':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'D');
                        break;
                    case '\x1E':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'E');
                        break;
                    case '\x1F':
                        CopyUtf16AndEscapeUnicode(ref remaining, ref i, '1', 'F');
                        break;
                }
            }
            remaining.CopyTo(_chars.Slice(pos)); // if there is still something to copy we continue here
            pos += remaining.Length;
            WriteUtf16DoubleQuote();
        }

        public void WriteUtf16Verbatim(string value)
        {
            WriteUtf16Verbatim(value.AsSpan());
        }

        public void WriteUtf16Verbatim(ReadOnlySpan<char> value)
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
        public void WriteUtf16Name(string value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 3;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteUtf16DoubleQuote();
            value.AsSpan().CopyTo(_chars.Slice(pos));
            pos += value.Length;
            WriteUtf16DoubleQuote();
            _chars[pos++] = JsonConstant.NameSeparator;
        }

        /// <summary>
        ///     We need copy the span up to the current index, then WriteUtf16 the escape char and continue
        ///     This is one messy thing, resetting the iterator
        /// </summary>
        private void CopyUtf16AndEscape(ref ReadOnlySpan<char> remaining, ref int i, char toEscape)
        {
            ref var pos = ref _pos;
            remaining.Slice(0, i).CopyTo(_chars.Slice(pos));
            pos += i;
            remaining = remaining.Slice(i + 1); // continuing after the escaped char
            i = -1; // i++ in post increment
            var minWrite = 1 + remaining.Length;
            if (pos > _chars.Length - minWrite) 
            {
                Grow(minWrite); // grow to fit escaped char
            }

            WriteUtf16SingleEscapedChar(toEscape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16SingleEscapedChar(char toEscape)
        {
            ref var pos = ref _pos;
            _chars[pos++] = JsonConstant.ReverseSolidus;
            _chars[pos++] = toEscape;
        }

        private void CopyUtf16AndEscapeUnicode(ref ReadOnlySpan<char> remaining, ref int i, char firstToEscape, char secondToEscape)
        {
            ref var pos = ref _pos;
            remaining.Slice(0, i).CopyTo(_chars.Slice(pos));
            pos += i;
            remaining = remaining.Slice(i + 1); // continuing after the escaped char
            i = -1; // i++ in post increment
            var minWrite = 5 + remaining.Length; // we need 5 more chars to fit the escaped part
            if (pos > _chars.Length - minWrite)
            {
                Grow(minWrite);
            }
            WriteUtf16DoubleEscapedChar(firstToEscape, secondToEscape);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16DoubleEscapedChar(char firstToEscape, char secondToEscape)
        {
            ref var pos = ref _pos;
            _chars[pos++] = JsonConstant.ReverseSolidus;
            _chars[pos++] = 'u';
            _chars[pos++] = '0';
            _chars[pos++] = '0';
            _chars[pos++] = firstToEscape;
            _chars[pos++] = secondToEscape;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16BeginObject()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonConstant.BeginObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16EndObject()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonConstant.EndObject;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16BeginArray()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonConstant.BeginArray;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16EndArray()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonConstant.EndArray;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16ValueSeparator()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = JsonConstant.ValueSeparator;
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

            _chars[pos++] = JsonConstant.Null;
            _chars[pos++] = 'u';
            _chars[pos++] = 'l';
            _chars[pos++] = 'l';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUtf16DoubleQuote()
        {
            _chars[_pos++] = JsonConstant.String;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Version(Version value)
        {
            ref var pos = ref _pos;
            const int versionLength = 45; // 4 * int + 3 . + 2 double quote
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
    }
}