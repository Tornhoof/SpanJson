using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonWriter<TSymbol> where TSymbol : struct
    {
        private static readonly char[] LongMinValueUtf16 = long.MinValue.ToString().ToCharArray();
        private static readonly byte[] LongMinValueUtf8 = Encoding.UTF8.GetBytes(long.MinValue.ToString());
        private TSymbol[] _arrayToReturnToPool;
        private Span<char> _chars;
        private Span<byte> _bytes;
        private int _pos;

        public JsonWriter(int initialSize)
        {
            _arrayToReturnToPool = ArrayPool<TSymbol>.Shared.Rent(initialSize);
            if (typeof(TSymbol) == typeof(char))
            {

                _chars = MemoryMarshal.Cast<TSymbol, char>(_arrayToReturnToPool);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(_arrayToReturnToPool);
            }
            else
            {
                throw new NotSupportedException();
            }

            _pos = 0;
        }

        public override string ToString()
        {
            var s = _chars.Slice(0, _pos).ToString();
            Dispose();
            return s;
        }

        public int Position => _pos;

        public TSymbol[] Data => _arrayToReturnToPool;



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            var toReturn = _arrayToReturnToPool;
            this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private void Grow(int requiredAdditionalCapacity)
        {
            Debug.Assert(requiredAdditionalCapacity > 0);

            var poolArray =
                ArrayPool<TSymbol>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _chars.Length * 2));
            var toReturn = _arrayToReturnToPool;
            if (typeof(TSymbol) == typeof(char))
            {
                var converted = MemoryMarshal.Cast<TSymbol, char>(toReturn);
                _chars.CopyTo(converted);
                _chars = converted;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                var converted = MemoryMarshal.Cast<TSymbol, byte>(toReturn);
                _bytes.CopyTo(converted);
                _bytes = converted;
            }

            _arrayToReturnToPool = poolArray;

            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }
    }
}