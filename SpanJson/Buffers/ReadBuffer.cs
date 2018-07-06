using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SpanJson.Helpers;

namespace SpanJson.Buffers
{
    public ref struct ReadBuffer<TSymbol> where TSymbol : struct
    {
        private readonly bool _isStreaming;
        private readonly TextReader _reader;
        private readonly Stream _stream;
        private TSymbol[] _data;
        private int _totalSize;
        public ReadOnlySpan<char> Chars;
        public ReadOnlySpan<byte> Bytes;
        public int Pos;
        public int Length;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadBuffer(in ReadOnlySpan<TSymbol> input)
        {
            Pos = 0;
            _data = null;
            _isStreaming = false;
            _stream = default;
            _reader = default;
            _totalSize = 0;
            Length = input.Length;            
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

        public bool TrySlideOrResize()
        {
            if (_isStreaming)
            {
                var remaining = _data.Length - Pos;
                _data.AsSpan(Pos).CopyTo(_data); // copy the remaining data to the beginning
                if (typeof(TSymbol) == typeof(char))
                {
                    var temp = Unsafe.As<TSymbol[], char[]>(ref _data);
                    var read = _reader.Read(temp, remaining, temp.Length - remaining);
                    Length = read + remaining;
                    _totalSize += Pos;
                    Pos = 0;
                    return true;
                }

                if (typeof(TSymbol) == typeof(byte))
                {
                    var temp = Unsafe.As<TSymbol[], byte[]>(ref _data);
                    var read = _stream.Read(temp, remaining, temp.Length - remaining);
                    Length = read + remaining;
                    _totalSize += Pos;
                    Pos = 0;
                    return true;
                }

                ThrowNotSupportedException();
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ReadBuffer(TextReader reader)
        {
            Pos = 0;
            _isStreaming = true;
            _stream = default;
            _reader = reader;
            _totalSize = 0;
            if (typeof(TSymbol) == typeof(char))
            {
                _data = ArrayPool<TSymbol>.Shared.Rent(BufferConstants.ReadBufferSize);
                var temp = Unsafe.As<TSymbol[], char[]>(ref _data);
                Length = _reader.Read(temp);
                Chars = MemoryMarshal.Cast<TSymbol, char>(_data.AsSpan());
                Bytes = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
                _data = default;
                Length = default;
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
            if (typeof(TSymbol) == typeof(byte))
            {
                _data = ArrayPool<TSymbol>.Shared.Rent(BufferConstants.ReadBufferSize);
                var temp = Unsafe.As<TSymbol[], byte[]>(ref _data);
                Length = _stream.Read(temp);
                Bytes = MemoryMarshal.Cast<TSymbol, byte>(_data.AsSpan());
                Chars = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
                _data = default;
                Length = default;
            }
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetTotalSize()
        {
            if (_isStreaming)
            {
                return _totalSize;
            }
            else
            {
                return Pos;
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            if (_isStreaming && _data != null)
            {
                ArrayPool<TSymbol>.Shared.Return(_data);
            }
        }
    }
}
