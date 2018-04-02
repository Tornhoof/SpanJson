using System;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson
{
    public ref struct JsonWriter
    {
        private static readonly char[] DateTimeFormat = {'o'};
        private char[] _arrayToReturnToPool;
        private Span<char> _chars;
        private int _pos;
        private Guid guid;

        public JsonWriter(Span<char> initialBuffer)
        {
            _arrayToReturnToPool = null;
            _chars = initialBuffer;
            _pos = 0;
            guid = Guid.NewGuid();
        }

        public JsonWriter(int initialSize)
        {
            _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialSize);
            _chars = _arrayToReturnToPool;
            _pos = 0;
            guid = Guid.NewGuid();
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

        public void WriteSByte(sbyte value)
        {
            ref var pos = ref _pos;
            const int digits = 3;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteInt16(short value)
        {
            ref var pos = ref _pos;
            const int digits = 6;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteInt32(int value)
        {
            ref var pos = ref _pos;
            const int digits = 10;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteInt64(long value)
        {
            ref var pos = ref _pos;
            const int digits = 20;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteByte(byte value)
        {
            ref var pos = ref _pos;
            const int digits = 3;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteUInt16(ushort value)
        {
            ref var pos = ref _pos;
            const int digits = 6;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteUInt32(uint value)
        {
            ref var pos = ref _pos;
            const int digits = 10;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

        public void WriteUInt64(ulong value)
        {
            ref var pos = ref _pos;
            const int digits = 20;
            if (pos > _chars.Length - digits)
            {
                Grow(digits);
            }

            value.TryFormat(_chars.Slice(pos), out var written, provider: CultureInfo.InvariantCulture);
            pos += written;
        }

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
            WriteDoubleQuote();
            ref var pos = ref _pos;
            const int size = 3; // 1 char + two '"'
            if (pos > _chars.Length - size)
            {
                Grow(size);
            }

            _chars[pos++] = value;
            WriteDoubleQuote();
        }

        public void WriteDateTime(DateTime value)
        {
            WriteDoubleQuote();
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            value.TryFormat(_chars.Slice(pos), out var written, DateTimeFormat, CultureInfo.InvariantCulture);
            pos += written;
            WriteDoubleQuote();
        }

        public void WriteDateTimeOffset(DateTimeOffset value)
        {
            WriteDoubleQuote();
            ref var pos = ref _pos;
            const int dtSize = 35; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

            value.TryFormat(_chars.Slice(pos), out var written, DateTimeFormat, CultureInfo.InvariantCulture);
            pos += written;
            WriteDoubleQuote();
        }

        public void WriteTimeSpan(TimeSpan value)
        {
            WriteDoubleQuote();
            ref var pos = ref _pos;
            const int dtSize = 20; // Form o + two '"'
            if (pos > _chars.Length - dtSize)
            {
                Grow(dtSize);
            }

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
            if (value.Length == 1 && pos < _chars.Length + 2)
            {
                WriteDoubleQuote();
                _chars[pos++] = value[0];
                WriteDoubleQuote();
            }
            else
            {
                WriteStringSlow(value);
            }
        }

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
            value.AsSpan().CopyTo(_chars.Slice(pos));
            pos += value.Length;
            WriteDoubleQuote();
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
