using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SpanJson
{
    public struct BufferReader<TSymbol> where TSymbol : struct 
    {
        private readonly Stream _stream;
        private readonly TextReader _reader;
        private TSymbol[] _buffer;
        private int _length;

        public BufferReader(Stream stream)
        {
            _stream = stream;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
            _reader = default;
            _length = 0;
        }

        public BufferReader(TextReader reader)
        {
            _stream = default;
            _reader = reader;
            _buffer = ArrayPool<TSymbol>.Shared.Rent(8192);
            _length = 0;
        }
        
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
                _length = _stream.Read(byteBuffer);
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var charBuffer = MemoryMarshal.Cast<TSymbol, char>(_buffer);
                _length = _reader.Read(charBuffer);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SlideWindow(ref int pos)
        {
            int remaining = _length - pos;
            Array.Copy(_buffer, pos, _buffer, 0, remaining);
            pos = remaining;
            _length = pos;
            if (typeof(TSymbol) == typeof(byte))
            {                
                var byteBuffer = MemoryMarshal.Cast<TSymbol, byte>(_buffer);
                _length += _stream.Read(byteBuffer.Slice(pos));
            }
            else if (typeof(TSymbol) == typeof(char))
            {
                var charBuffer = MemoryMarshal.Cast<TSymbol, char>(_buffer);
                _length += _reader.Read(charBuffer);
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

    }
}
