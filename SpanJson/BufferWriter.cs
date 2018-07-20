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
    public struct BufferWriter<TSymbol> : IDisposable where TSymbol : struct
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
        public void Write(in ReadOnlySpan<TSymbol> values)
        {
            if (typeof(TSymbol) == typeof(byte))
            {
                var byteBuffer = MemoryMarshal.Cast<TSymbol, byte>(values);
                _stream.Write(byteBuffer);
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var charBuffer = MemoryMarshal.Cast<TSymbol, char>(values);
                _writer.Write(charBuffer);
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
