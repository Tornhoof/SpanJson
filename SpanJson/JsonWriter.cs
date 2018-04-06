using System;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref struct JsonWriter
    {
        private static readonly char[] DateTimeFormat = {'r'};
        private char[] _arrayToReturnToPool;
        private Span<char> _chars;
        private int _pos;

        public JsonWriter(Span<char> initialBuffer)
        {
            _arrayToReturnToPool = null;
            _chars = initialBuffer;
            _pos = 0;
        }

        public JsonWriter(int initialSize)
        {
            _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialSize);
            _chars = _arrayToReturnToPool;
            _pos = 0;
        }

        public int Length
        {
            get => _pos;
            set
            {
                Debug.Assert(value >= 0);
                Debug.Assert(value <= _chars.Length);
                _pos = value;
            }
        }

        public int Capacity => _chars.Length;

        public void EnsureCapacity(int capacity)
        {
            if (capacity > _chars.Length)
            {
                Grow(capacity - _chars.Length);
            }
        }

        /// <summary>
        ///     Get a pinnable reference to the builder.
        /// </summary>
        /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length" /></param>
        public ref char GetPinnableReference(bool terminate = false)
        {
            if (terminate)
            {
                EnsureCapacity(Length + 1);
                _chars[Length] = '\0';
            }

            return ref MemoryMarshal.GetReference(_chars);
        }

        public ref char this[int index]
        {
            get
            {
                Debug.Assert(index < _pos);
                return ref _chars[index];
            }
        }

        public override string ToString()
        {
            var s = new string(_chars.Slice(0, _pos));
            Dispose();
            return s;
        }

        /// <summary>Returns the underlying storage of the builder.</summary>
        public Span<char> RawChars => _chars;

        /// <summary>
        ///     Returns a span around the contents of the builder.
        /// </summary>
        /// <param name="terminate">Ensures that the builder has a null char after <see cref="Length" /></param>
        public ReadOnlySpan<char> AsSpan(bool terminate)
        {
            if (terminate)
            {
                EnsureCapacity(Length + 1);
                _chars[Length] = '\0';
            }

            return _chars.Slice(0, _pos);
        }

        public ReadOnlySpan<char> AsSpan()
        {
            return _chars.Slice(0, _pos);
        }

        public ReadOnlySpan<char> AsSpan(int start)
        {
            return _chars.Slice(start, _pos - start);
        }

        public ReadOnlySpan<char> AsSpan(int start, int length)
        {
            return _chars.Slice(start, length);
        }

        public bool TryCopyTo(Span<char> destination, out int charsWritten)
        {
            if (_chars.Slice(0, _pos).TryCopyTo(destination))
            {
                charsWritten = _pos;
                Dispose();
                return true;
            }

            charsWritten = 0;
            Dispose();
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int requiredAdditionalCapacity)
        {
            Debug.Assert(requiredAdditionalCapacity > 0);

            var poolArray =
                ArrayPool<char>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _chars.Length * 2));

            _chars.CopyTo(poolArray);

            var toReturn = _arrayToReturnToPool;
            _chars = _arrayToReturnToPool = poolArray;
            if (toReturn != null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            var toReturn = _arrayToReturnToPool;
            this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
            if (toReturn != null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
        }

        public void WriteSByte(sbyte value) => WriteInt64Internal(value);

        public void WriteInt16(short value) => WriteInt64Internal(value);

        public void WriteInt32(int value) => WriteInt64Internal(value);

        public void WriteInt64(long value) => WriteInt64Internal(value);

        private static readonly char[] LongMinValue = long.MinValue.ToString().ToCharArray();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteInt64Internal(long value)
        {
            ref var pos = ref _pos;
            if (value == long.MinValue)
            {
                if (pos > _chars.Length - 21)
                {
                    Grow(21);
                }

                LongMinValue.AsSpan().TryCopyTo(_chars.Slice(pos));
                pos += LongMinValue.Length;
            }
            else if (value < 0)
            {
                if (pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                _chars[_pos++] = '-';
                value = unchecked(-value);
            }
            WriteUInt64Internal((ulong) value);
        }

        private static readonly ulong[] Powers10 = new[]
        {
            1UL,
            10UL,
            100UL,
            1000UL,
            10000UL,
            100000UL,
            1000000UL,
            10000000UL,
            100000000UL,
            1000000000UL,
            10000000000UL,
            100000000000UL,
            1000000000000UL,
            10000000000000UL,
            100000000000000UL,
            1000000000000000UL,
            10000000000000000UL,
            100000000000000000UL,
            1000000000000000000UL,
            10000000000000000000UL
//   1234567890123456789
        };

        private static readonly byte[] Tab64 = new byte[]
        {
            63, 0, 58, 1, 59, 47, 53, 2,
            60, 39, 48, 27, 54, 33, 42, 3,
            61, 51, 37, 40, 49, 18, 28, 20,
            55, 30, 34, 11, 43, 14, 22, 4,
            62, 57, 46, 52, 38, 26, 32, 41,
            50, 36, 17, 19, 29, 10, 13, 21,
            56, 45, 25, 31, 35, 16, 9, 12,
            44, 24, 15, 8, 23, 7, 6, 5
        };

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int Log2Floor(ulong value)
        {
            value |= value >> 1;
            value |= value >> 2;
            value |= value >> 4;
            value |= value >> 8;
            value |= value >> 16;
            value |= value >> 32;
            return Tab64[(value - (value >> 1)) * 0x07EDD5E59A4E28C2 >> 58];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteUInt64Internal(ulong value)
        {
            ref var pos = ref _pos;
            if (value < 10)
            {
                if (pos > _chars.Length - 1)
                {
                    Grow(1);
                }

                _chars[_pos++] = (char) ('0' + value);
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
                _chars[pos + i - 1] = (char) (temp - (value * 10));
            }

            pos += digits;
        }

        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //// https://github.com/neuecc/Utf8Json/blob/master/src/Utf8Json/Internal/NumberConverter.cs
        //// or https://stackoverflow.com/questions/4351371/c-performance-challenge-integer-to-stdstring-conversion
        //private void WriteUInt64Internal(ulong value)
        //{
        //    var num1 = value;
        //    ulong div;

        //    if (num1 < 10000)
        //    {
        //        if (num1 < 10)
        //        {
        //            goto L1;
        //        }

        //        if (num1 < 100)
        //        {
        //            goto L2;
        //        }

        //        if (num1 < 1000)
        //        {
        //            goto L3;
        //        }

        //        goto L4;
        //    }
        //    else
        //    {
        //        var num2 = num1 / 10000;
        //        num1 -= num2 * 10000;
        //        if (num2 < 10000)
        //        {
        //            if (num2 < 10)
        //            {
        //                goto L5;
        //            }

        //            if (num2 < 100)
        //            {
        //                goto L6;
        //            }

        //            if (num2 < 1000)
        //            {
        //                goto L7;
        //            }

        //            goto L8;
        //        }
        //        else
        //        {
        //            var num3 = num2 / 10000;
        //            num2 -= num3 * 10000;
        //            if (num3 < 10000)
        //            {
        //                if (num3 < 10)
        //                {
        //                    goto L9;
        //                }

        //                if (num3 < 100)
        //                {
        //                    goto L10;
        //                }

        //                if (num3 < 1000)
        //                {
        //                    goto L11;
        //                }

        //                goto L12;
        //            }
        //            else
        //            {
        //                var num4 = num3 / 10000;
        //                num3 -= num4 * 10000;
        //                if (num4 < 10000)
        //                {
        //                    if (num4 < 10)
        //                    {
        //                        goto L13;
        //                    }

        //                    if (num4 < 100)
        //                    {
        //                        goto L14;
        //                    }

        //                    if (num4 < 1000)
        //                    {
        //                        goto L15;
        //                    }

        //                    goto L16;
        //                }
        //                else
        //                {
        //                    var num5 = num4 / 10000;
        //                    num4 -= num5 * 10000;
        //                    if (num5 < 10000)
        //                    {
        //                        if (num5 < 10)
        //                        {
        //                            goto L17;
        //                        }

        //                        if (num5 < 100)
        //                        {
        //                            goto L18;
        //                        }

        //                        if (num5 < 1000)
        //                        {
        //                            goto L19;
        //                        }

        //                        goto L20;
        //                    }

        //                    L20:
        //                    _chars[_pos++] = (char) ('0' + (div = (num5 * 8389L) >> 23));
        //                    num5 -= div * 1000;
        //                    L19:
        //                    _chars[_pos++] = (char) ('0' + (div = (num5 * 5243L) >> 19));
        //                    num5 -= div * 100;
        //                    L18:
        //                    _chars[_pos++] = (char) ('0' + (div = (num5 * 6554L) >> 16));
        //                    num5 -= div * 10;
        //                    L17:
        //                    _chars[_pos++] = (char) ('0' + (num5));
        //                }

        //                L16:
        //                _chars[_pos++] = (char) ('0' + (div = (num4 * 8389L) >> 23));
        //                num4 -= div * 1000;
        //                L15:
        //                _chars[_pos++] = (char) ('0' + (div = (num4 * 5243L) >> 19));
        //                num4 -= div * 100;
        //                L14:
        //                _chars[_pos++] = (char) ('0' + (div = (num4 * 6554L) >> 16));
        //                num4 -= div * 10;
        //                L13:
        //                _chars[_pos++] = (char) ('0' + (num4));
        //            }

        //            L12:
        //            _chars[_pos++] = (char) ('0' + (div = (num3 * 8389L) >> 23));
        //            num3 -= div * 1000;
        //            L11:
        //            _chars[_pos++] = (char) ('0' + (div = (num3 * 5243L) >> 19));
        //            num3 -= div * 100;
        //            L10:
        //            _chars[_pos++] = (char) ('0' + (div = (num3 * 6554L) >> 16));
        //            num3 -= div * 10;
        //            L9:
        //            _chars[_pos++] = (char) ('0' + (num3));
        //        }

        //        L8:
        //        _chars[_pos++] = (char) ('0' + (div = (num2 * 8389L) >> 23));
        //        num2 -= div * 1000;
        //        L7:
        //        _chars[_pos++] = (char) ('0' + (div = (num2 * 5243L) >> 19));
        //        num2 -= div * 100;
        //        L6:
        //        _chars[_pos++] = (char) ('0' + (div = (num2 * 6554L) >> 16));
        //        num2 -= div * 10;
        //        L5:
        //        _chars[_pos++] = (char) ('0' + (num2));
        //    }

        //    L4:
        //    _chars[_pos++] = (char) ('0' + (div = (num1 * 8389L) >> 23));
        //    num1 -= div * 1000;
        //    L3:
        //    _chars[_pos++] = (char) ('0' + (div = (num1 * 5243L) >> 19));
        //    num1 -= div * 100;
        //    L2:
        //    _chars[_pos++] = (char) ('0' + (div = (num1 * 6554L) >> 16));
        //    num1 -= div * 10;
        //    L1:
        //    _chars[_pos++] = (char) ('0' + (num1));
        //}

        public void WriteByte(byte value) => WriteUInt64Internal(value);

        public void WriteUInt16(ushort value) => WriteUInt64Internal(value);

        public void WriteUInt32(uint value) => WriteUInt64Internal(value);

        public void WriteUInt64(ulong value) => WriteUInt64Internal(value);

        public void WriteSingle(float value)
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

        public void WriteDouble(double value)
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

        public void WriteDecimal(decimal value)
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

        public void WriteBoolean(bool value)
        {
            ref var pos = ref _pos;
            if (value)
            {
                const int trueLength = 4;
                if (pos > _chars.Length - trueLength)
                {
                    Grow(trueLength);
                }

                _chars[pos++] = 't';
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

                _chars[pos++] = 'f';
                _chars[pos++] = 'a';
                _chars[pos++] = 'l';
                _chars[pos++] = 's';
                _chars[pos++] = 'e';
            }
        }

        public void WriteChar(char value)
        {
            ref var pos = ref _pos;
            const int size = 3; // 1 char + two '"'
            if (pos > _chars.Length - size)
            {
                Grow(size);
            }

            WriteDoubleQuote();
            _chars[pos++] = value;
            WriteDoubleQuote();
        }

        public void WriteDateTime(DateTime value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteDoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, DateTimeFormat, CultureInfo.InvariantCulture);
            pos += written;
            WriteDoubleQuote();
        }

        public void WriteDateTimeOffset(DateTimeOffset value)
        {
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteDoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, DateTimeFormat, CultureInfo.InvariantCulture);
            pos += written;
            WriteDoubleQuote();
        }

        public void WriteTimeSpan(TimeSpan value)
        {
            ref var pos = ref _pos;
            const int dtSize = 20; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            WriteDoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written, DateTimeFormat, CultureInfo.InvariantCulture);
            pos += written;
            WriteDoubleQuote();
        }

        public void WriteGuid(Guid value)
        {
            ref var pos = ref _pos;
            const int guidSize = 42; // Format D + two '"';
            if (pos > _chars.Length - guidSize)
            {
                Grow(guidSize);
            }

            WriteDoubleQuote();
            value.TryFormat(_chars.Slice(pos), out var written);
            pos += written;
            WriteDoubleQuote();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteString(string value)
        {
            ref var pos = ref _pos;
            // very common case, e.g. appending strings from NumberFormatInfo like separators, percent symbols, etc.
            if (value.Length == 1 && pos < _chars.Length + 3
            ) // technically we only need 2, but escaped char might be the case
            {
                WriteDoubleQuote();
                var c = value[0];
                switch (c)
                {
                    case '"':
                        _chars[pos++] = '\\';
                        _chars[pos++] = '"';
                        break;
                    case '\\':
                        _chars[pos++] = '\\';
                        _chars[pos++] = '\\';
                        break;
                    case '\b':
                        _chars[pos++] = '\\';
                        _chars[pos++] = 'b';
                        break;
                    case '\f':
                        _chars[pos++] = '\\';
                        _chars[pos++] = 'f';
                        break;
                    case '\n':
                        _chars[pos++] = '\\';
                        ;
                        _chars[pos++] = 'n';
                        break;
                    case '\r':
                        _chars[pos++] = '\\';
                        _chars[pos++] = 'r';
                        break;
                    case '\t':
                        _chars[pos++] = '\\';
                        _chars[pos++] = 't';
                        break;
                    default:
                        _chars[pos++] = c;
                        break;
                }

                WriteDoubleQuote();
            }
            else
            {
                WriteStringSlow(value);
            }
        }

        /// <summary>
        ///     The value should already be properly escaped
        /// </summary>
        /// <param name="value"></param>
        public void WriteName(string value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 3;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteDoubleQuote();
            value.AsSpan().CopyTo(_chars.Slice(pos));
            pos += value.Length;
            WriteDoubleQuote();
            _chars[pos++] = ':';
        }

        private void WriteStringSlow(string value)
        {
            ref var pos = ref _pos;
            var sLength = value.Length + 2;
            if (pos > _chars.Length - sLength)
            {
                Grow(sLength);
            }

            WriteDoubleQuote();
            ReadOnlySpan<char> remaining = value.AsSpan();
            for (int i = 0; i < remaining.Length; i++)
            {
                var c = remaining[i];
                switch (c)
                {
                    case '"':
                        CopyAndEscape(ref remaining, ref i, '"');
                        break;
                    case '\\':
                        CopyAndEscape(ref remaining, ref i, '\\');
                        break;
                    case '\b':
                        CopyAndEscape(ref remaining, ref i, 'b');
                        break;
                    case '\f':
                        CopyAndEscape(ref remaining, ref i, 'f');
                        break;
                    case '\n':
                        CopyAndEscape(ref remaining, ref i, 'n');
                        break;
                    case '\r':
                        CopyAndEscape(ref remaining, ref i, 'r');
                        break;
                    case '\t':
                        CopyAndEscape(ref remaining, ref i, 't');
                        break;
                }
            }

            remaining.CopyTo(_chars.Slice(pos)); // if there is still something to copy we continue here
            pos += remaining.Length;
            WriteDoubleQuote();
        }

        /// <summary>
        ///     We need copy the span up to the current index, then write the escape char and continue
        ///     This is one messy thing, resetting the iterator
        /// </summary>
        private void CopyAndEscape(ref ReadOnlySpan<char> remaining, ref int i, char toEscape)
        {
            remaining.Slice(0, i).CopyTo(_chars.Slice(_pos));
            _pos += i;
            if (_pos > _chars.Length - 1) // one more now
            {
                Grow(1);
            }

            _chars[_pos++] = '\\';
            _chars[_pos++] = toEscape;
            remaining = remaining.Slice(i + 1); // continuing after the escaped char
            i = 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteObjectStart()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = '{';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteObjectEnd()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = '}';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArrayStart()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = '[';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteArrayEnd()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = ']';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteSeparator()
        {
            ref var pos = ref _pos;
            if (pos > _chars.Length - 1)
            {
                Grow(1);
            }

            _chars[pos++] = ',';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            ref var pos = ref _pos;
            const int nullLength = 4;
            if (pos > _chars.Length - nullLength)
            {
                Grow(nullLength);
            }

            _chars[pos++] = 'n';
            _chars[pos++] = 'u';
            _chars[pos++] = 'l';
            _chars[pos++] = 'l';
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteDoubleQuote()
        {
            _chars[_pos++] = '"';
        }
    }
}