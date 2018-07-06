using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Buffers
{
    public ref struct ReadBuffer<TSymbol> where TSymbol : struct
    {
        private readonly bool _isStreaming;
        private readonly TextReader _reader;
        private readonly Stream _stream;
        private TSymbol[] _data;
        private int _totalSize;
        public int Length;
        public ReadOnlySpan<char> Chars;
        public ReadOnlySpan<byte> Bytes;
        public int Pos;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadBuffer(in ReadOnlySpan<TSymbol> input)
        {
            Length = input.Length;
            Pos = 0;
            _data = null;
            _isStreaming = false;
            _stream = default;
            _reader = default;
            _totalSize = 0;
            if (typeof(TSymbol) == typeof(char))
            {
                Chars = MemoryMarshal.Cast<TSymbol, char>(input);
                Bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                Bytes = MemoryMarshal.Cast<TSymbol, byte>(input);
                Chars = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadBuffer(TextReader reader)
        {
            Pos = 0;
            _isStreaming = true;
            _stream = default;
            _reader = reader;
            _totalSize = 0;
            Length = BufferConstants.ReadBufferSize;
            if (typeof(TSymbol) == typeof(char))
            {
                _data = ArrayPool<TSymbol>.Shared.Rent(BufferConstants.ReadBufferSize);
                Chars = MemoryMarshal.Cast<TSymbol, char>(_data.AsSpan());
                Bytes = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
                _data = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadBuffer(Stream stream)
        {
            Pos = 0;
            _isStreaming = true;
            _stream = stream;
            _reader = default;
            _totalSize = 0;
            Length = BufferConstants.ReadBufferSize;
            if (typeof(TSymbol) == typeof(byte))
            {
                _data = ArrayPool<TSymbol>.Shared.Rent(BufferConstants.ReadBufferSize);
                Bytes = MemoryMarshal.Cast<TSymbol, byte>(_data.AsSpan());
                Chars = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
                _data = default;
            }
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }
    }
}
