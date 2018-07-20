using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Helpers;

namespace SpanJson
{
    public struct BufferReader<TSymbol> where TSymbol : struct 
    {
        private readonly Stream _stream;
        private readonly TextReader _reader;
        private TSymbol[] _buffer;

        public BufferReader(Stream stream)
        {
            _stream = stream;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
            _reader = default;
            Length = 0;
        }

        public BufferReader(TextReader reader)
        {
            _stream = default;
            _reader = reader;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
            Length = 0;
        }

        public int Length { get; private set; }

        public ReadOnlySpan<TSymbol> GetSpan()
        {
            return _buffer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Fill()
        {
            if (typeof(TSymbol) == typeof(byte))
            {
                var byteBuffer = MemoryMarshal.Cast<TSymbol, byte>(_buffer);
                Length = _stream.Read(byteBuffer);
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var charBuffer = MemoryMarshal.Cast<TSymbol, char>(_buffer);
                Length = _reader.Read(charBuffer);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SlideWindow(ref int pos)
        {
            int remaining = Length - pos;
            Array.Copy(_buffer, pos, _buffer, 0, remaining);
            pos = remaining;
            Length = pos;
            if (typeof(TSymbol) == typeof(byte))
            {                
                var byteBuffer = MemoryMarshal.Cast<TSymbol, byte>(_buffer);
                Length += _stream.Read(byteBuffer.Slice(pos));
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var charBuffer = MemoryMarshal.Cast<TSymbol, char>(_buffer);
                Length += _reader.Read(charBuffer);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resize(ref int pos)
        {
            FormatterUtils.GrowArray(ref _buffer);
            SlideWindow(ref pos);
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

    }
}
