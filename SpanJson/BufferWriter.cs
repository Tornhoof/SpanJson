using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SpanJson
{
    public ref struct BufferWriter<TSymbol> where TSymbol : struct
    {
        private readonly Stream _stream;
        private readonly TextWriter _writer;
        private readonly TSymbol[] _buffer;

        public BufferWriter(Stream stream)
        {
            _stream = stream;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
            _writer = default;
        }

        public BufferWriter(TextWriter writer)
        {
            _stream = default;
            _writer = writer;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
        }

        public Span<TSymbol> GetSpan()
        {
            return _buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush(ref int pos)
        {
            if (typeof(TSymbol) == typeof(byte))
            {
                var temp = _buffer;
                var byteBuffer = Unsafe.As<TSymbol[], byte[]>(ref temp);
                _stream.Write(byteBuffer, 0, pos);
                pos = 0;
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var temp = _buffer;
                var charBuffer = Unsafe.As<TSymbol[], char[]>(ref temp);
                _writer.Write(charBuffer, 0, pos);
                pos = 0;
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf8(in ReadOnlySpan<byte> values)
        {
            if (typeof(TSymbol) == typeof(byte))
            {
                _stream.Write(values);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteUtf16(in ReadOnlySpan<char> values)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                _writer.Write(values);
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
                _writer.Write(JsonUtf16Constant.DoubleQuote);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _stream.WriteByte(JsonUtf8Constant.DoubleQuote);
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

        public void Dispose()
        {
            if (_buffer != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_buffer);
            }
        }
    }
}
