using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Buffers
{
    public ref struct WriteBuffer<TSymbol> where TSymbol : struct
    {
        public Span<char> Chars;
        public Span<byte> Bytes;
        public int Pos;


        public WriteBuffer(int initialSize)
        {
            Data = ArrayPool<TSymbol>.Shared.Rent(initialSize);
            Pos = 0;
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

        public WriteBuffer(Stream stream)
        {
            Chars = null;
            Bytes = null;
            Pos = 0;
        }

        public WriteBuffer(TextWriter writer)
        {
            Data
                Pos = 0;
            Chars = null;
            Bytes = null;
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }

        public void Grow(int requiredAdditionalCapacity)
        {
            Debug.Assert(requiredAdditionalCapacity > 0);

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
