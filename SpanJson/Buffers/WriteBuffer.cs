using System;
using System.Buffers;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Buffers
{
    public ref struct WriteBuffer<TSymbol> where TSymbol : struct
    {
        private readonly bool _isFixedSize;
        private readonly TextWriter _writer;
        private readonly Stream _stream;
        public Span<char> Chars;
        public Span<byte> Bytes;
        public int Pos;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WriteBuffer(int initialSize)
        {
            Data = ArrayPool<TSymbol>.Shared.Rent(initialSize);
            Pos = 0;
            _isFixedSize = false;
            _stream = default;
            _writer = default;
            if (typeof(TSymbol) == typeof(char))
            {
                Chars = MemoryMarshal.Cast<TSymbol, char>(Data);
                Bytes = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                Bytes = MemoryMarshal.Cast<TSymbol, byte>(Data);
                Chars = null;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = null;
                Bytes = null;
            }
        }


        /// <summary>
        /// Calls flush is the buffer is getting full
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Flush()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                if ((uint) Chars.Length < Pos + JsonSharedConstant.MaxVersionLength)
                {
                    FlushAll();
                }
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                if ((uint) Bytes.Length < Pos + JsonSharedConstant.MaxVersionLength)
                {
                    FlushAll();
                }
            }
            else
            {
                ThrowNotSupportedException();
                Chars = null;
                Bytes = null;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WriteBuffer(Stream stream)
        {
            _stream = stream;
            _writer = default;
            _isFixedSize = true;
            Data = ArrayPool<TSymbol>.Shared.Rent(4096);
            Pos = 0;
            if (typeof(TSymbol) == typeof(byte))
            {
                Bytes = MemoryMarshal.Cast<TSymbol, byte>(Data);
                Chars = default;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WriteBuffer(TextWriter writer)
        {
            _writer = writer;
            _stream = default;
            _isFixedSize = true;
            Data = ArrayPool<TSymbol>.Shared.Rent(4096);
            Pos = 0;
            if (typeof(TSymbol) == typeof(char))
            {
                Chars = MemoryMarshal.Cast<TSymbol, char>(Data);
                Bytes = default;
            }
            else
            {
                ThrowNotSupportedException();
                Chars = default;
                Bytes = default;
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
                _writer.Write(Chars.Slice(0, Pos));
                Pos = 0;
            }

            else if (typeof(TSymbol) == typeof(byte))
            {
                _stream.Write(Bytes.Slice(0, Pos));
                Pos = 0;
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

        public void Grow(int requiredAdditionalCapacity)
        {
            Debug.Assert(requiredAdditionalCapacity > 0);

            if (_isFixedSize)
            {
                Flush();
            }
            else
            {
                var toReturn = Data;
                if (typeof(TSymbol) == typeof(char))
                {
                    var poolArray =
                        ArrayPool<TSymbol>.Shared.Rent(Math.Max(Pos + requiredAdditionalCapacity, Chars.Length * 2));
                    var converted = MemoryMarshal.Cast<TSymbol, char>(poolArray);
                    Chars.CopyTo(converted);
                    Chars = converted;
                    Data = poolArray;
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    var poolArray =
                        ArrayPool<TSymbol>.Shared.Rent(Math.Max(Pos + requiredAdditionalCapacity, Bytes.Length * 2));
                    var converted = MemoryMarshal.Cast<TSymbol, byte>(poolArray);
                    Bytes.CopyTo(converted);
                    Bytes = converted;
                    Data = poolArray;
                }

                if (toReturn != null)
                {
                    ArrayPool<TSymbol>.Shared.Return(toReturn);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            var toReturn = Data;
            this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }


        public TSymbol[] Data { get; private set; }
    }
}
