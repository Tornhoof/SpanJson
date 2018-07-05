using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SpanJson
{
    public ref partial struct StreamingJsonReader<TSymbol> where TSymbol : struct
    {
        private readonly TextReader _reader;
        private TSymbol[] _buffer;
        private int _length;
        private readonly Stream _stream;
        private JsonReader<TSymbol> _jsonReader;

        public StreamingJsonReader(Stream stream, int sizeHint)
        {
            _stream = stream;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(sizeHint);
            _reader = default;
            _jsonReader = default;
            _length = default;
        }

        public StreamingJsonReader(TextReader reader, int sizeHint)
        {
            _reader = reader;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(sizeHint);
            _stream = default;
            _jsonReader = default;
            _length = default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fill(int start)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var temp = _buffer;
                var chars = Unsafe.As<TSymbol[], char[]>(ref temp);
                _length = _reader.Read(chars, start, chars.Length - start) + start;
                _jsonReader = new JsonReader<TSymbol>(_buffer.AsSpan(0, _length));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                var temp = _buffer;
                var bytes = Unsafe.As<TSymbol[], byte[]>(ref temp);
                _length = _stream.Read(bytes, start, bytes.Length - start) + start;
                _jsonReader = new JsonReader<TSymbol>(_buffer.AsSpan(0, _length));
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        /// <summary>
        /// As soon as we reach buffer.Length - JsonSharedConstant.MaxFormattedValueLength, we need to move the buffer further ahead
        /// i.e. copy the remaining bytes to the front and fill the remainder with new data
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SlideWindow()
        {
            // We assume that the window starts at the beginning
            int remaining = _length - _jsonReader.Position;
            if (remaining < JsonSharedConstant.MaxFormattedValueLength)
            {
                if (typeof(TSymbol) == typeof(byte))
                {
                    var temp = _buffer;
                    var bytes = Unsafe.As<TSymbol[], byte[]>(ref temp);
                    bytes.AsSpan(_jsonReader.Position, remaining).CopyTo(bytes);
                }
                else if (typeof(TSymbol) == typeof(char))
                {
                    var temp = _buffer;
                    var chars = Unsafe.As<TSymbol[], char[]>(ref temp);
                    chars.AsSpan(_jsonReader.Position, remaining).CopyTo(chars);
                }
                else
                {
                    ThrowNotSupportedException();
                }

                Fill(remaining);
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

        /// <summary>
        /// All the string related stuff is special, as it might be longer than the fixed size buffer
        /// If the buffer is big enough and the window is moved often enough to the beginning then the the strings should fit most of the time
        /// Steps:
        /// 1. Read String
        /// 2. If it throws, we need to resize Grow the buffer and read more data at the end
        /// 3. Repeat until done, worst case scenario are huge strings in one member, e.g. for comments or similar, but we can't help that
        /// TODO: find out if throwing is actually useful or if we need a custom handling without exception
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadString()
        {
            SlideWindow();
            if (typeof(TSymbol) == typeof(char))
            {
                return _jsonReader.ReadUtf16String();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return _jsonReader.ReadUtf8String();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<TSymbol> ReadStringSpan()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return MemoryMarshal.Cast<char, TSymbol>(_jsonReader.ReadUtf16StringSpan());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return MemoryMarshal.Cast<byte, TSymbol>(_jsonReader.ReadUtf8StringSpan());
            }

            ThrowNotSupportedException();
            return default;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadEscapedName()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return _jsonReader.ReadUtf16EscapedName();
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return _jsonReader.ReadUtf8EscapedName();
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadOnlySpan<TSymbol> ReadNameSpan()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                return MemoryMarshal.Cast<char, TSymbol>(_jsonReader.ReadUtf16NameSpan());
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                return MemoryMarshal.Cast<byte, TSymbol>(_jsonReader.ReadUtf8NameSpan());
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf8String()
        {
            return _jsonReader.ReadUtf8String();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public string ReadUtf16String()
        {
            return _jsonReader.ReadUtf16String();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public object ReadDynamic()
        {
            throw new NotImplementedException();
        }
    }
}