using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

// ReSharper disable ImpureMethodCallOnReadonlyValueField

namespace SpanJson
{
    /// <summary>
    /// This assumes that the buffer is always larger than the data to be written
    /// This is easily achieved for everything but string, for string we might need to buffer it elsewhere
    /// Even for strings it shouldn't be necessary for the average use case
    /// </summary>
    public ref partial struct StreamingJsonWriter<TSymbol> where TSymbol : struct
    {
        private readonly TextWriter _writer;
        private readonly Stream _stream;
        private readonly TSymbol[] _buffer;
        private JsonWriter<TSymbol> _jsonWriter;

        public StreamingJsonWriter(Stream stream, int sizeHint)
        {
            _stream = stream;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(sizeHint);
            _writer = default;
            _jsonWriter = new JsonWriter<TSymbol>(_buffer);
        }

        public StreamingJsonWriter(TextWriter writer, int sizeHint)
        {
            _writer = writer;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(sizeHint);
            _stream = default;
            _jsonWriter = new JsonWriter<TSymbol>(_buffer);
        }

        /// <summary>
        /// Calls flush is the buffer is getting full
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush()
        {
            if ((uint) _buffer.Length < _jsonWriter.Position + JsonSharedConstant.MaxFormattedValueLength)
            {
                FlushAll();
            }
        }

        /// <summary>
        /// This is called outside of the writer, after flush the same buffer can be reused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FlushAll()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var temp = _buffer;
                var chars = Unsafe.As<TSymbol[], char[]>(ref temp);
                var length = _jsonWriter.Position;
                _writer.Write(chars, 0, length);
                _jsonWriter.Reset();
            }

            else if (typeof(TSymbol) == typeof(byte))
            {
                var temp = _buffer;
                var bytes = Unsafe.As<TSymbol[], byte[]>(ref temp);
                var length = _jsonWriter.Position;
                _stream.Write(bytes, 0, length);
                _jsonWriter.Reset();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }
        public void Return()
        {
            ArrayPool<TSymbol>.Shared.Return(_buffer);
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteName(in ReadOnlySpan<char> name)
        {
            var current = name;
            while (true)
            {
                var currentLength = GetRealLength(current.Length);
                var remaining = _buffer.Length - _jsonWriter.Position; // remaining free bytes in buffer
                if (currentLength > remaining) 
                {
                    FlushAll();
                    var sliceSize = GetSliceSize(remaining, current.Length);
                    _jsonWriter.WriteName(current.Slice(0, sliceSize));
                    current = current.Slice(sliceSize);
                }
                else
                {
                    _jsonWriter.WriteName(current);
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteVerbatim(in ReadOnlySpan<TSymbol> values)
        {
            var current = values;
            while (true)
            {
                var remaining = _buffer.Length - _jsonWriter.Position;
                if (current.Length > remaining)
                {
                    FlushAll();
                    _jsonWriter.WriteVerbatim(current.Slice(0, remaining));
                    current = current.Slice(remaining);
                }
                else
                {
                    _jsonWriter.WriteVerbatim(current);
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteString(string value)
        {
            var current = value.AsSpan();
            while (true)
            {
                var currentLength = GetRealLength(current.Length);
                var remaining = _buffer.Length - _jsonWriter.Position;
                if (currentLength > remaining)
                {
                    FlushAll();
                    var sliceSize = GetSliceSize(remaining, current.Length);
                    _jsonWriter.WriteString(current.Slice(0, sliceSize));
                    current = current.Slice(sliceSize);
                }
                else
                {
                    _jsonWriter.WriteString(current);
                    break;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetSliceSize(int remaining, int charLength)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return Math.Min(charLength - 7, remaining);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Math.Min(remaining >> 2, charLength - 7); // for simplicity we assume that utf8 is 4 bytes max
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static int GetRealLength(int length)
        {
            const int safetyMargin = 10; // some methods have extra characters, e.g. double quotes or escapes, this is a safety margin
            if (typeof(TSymbol) == typeof(char))
            {
                return length + safetyMargin;
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return Encoding.UTF8.GetMaxByteCount(length) + safetyMargin;
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8String(string value)
        {
            _jsonWriter.WriteUtf8String(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16String(string value)
        {
            _jsonWriter.WriteUtf16String(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Verbatim(string value)
        {
            _jsonWriter.WriteUtf16Verbatim(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Verbatim(byte[] value)
        {
            _jsonWriter.WriteUtf8Verbatim(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16Verbatim(in ReadOnlySpan<char> value)
        {
            _jsonWriter.WriteUtf16Verbatim(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8Verbatim(in ReadOnlySpan<byte> value)
        {
            _jsonWriter.WriteUtf8Verbatim(value);
        }
    }
}
