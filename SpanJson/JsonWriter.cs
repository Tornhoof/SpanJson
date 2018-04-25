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
        private readonly Encoder _encoder;

        public JsonWriter(int initialSize)
        {
            _arrayToReturnToPool = ArrayPool<TSymbol>.Shared.Rent(initialSize);
            if (typeof(TSymbol) == typeof(char))
            {
                _chars = MemoryMarshal.Cast<TSymbol, char>(_arrayToReturnToPool);
                _encoder = null;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                _bytes = MemoryMarshal.Cast<TSymbol, byte>(_arrayToReturnToPool);
                _encoder = Encoding.UTF8.GetEncoder();
            }
            else
            {
                throw new NotSupportedException();
            }

            _pos = 0;
        }

        public int Position => _pos;

        public TSymbol[] Data => _arrayToReturnToPool;



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Dispose()
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

            var toReturn = _arrayToReturnToPool;
            if (typeof(TSymbol) == typeof(char))
            {
                var poolArray =
                    ArrayPool<TSymbol>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _chars.Length * 2));
                var converted = MemoryMarshal.Cast<TSymbol, char>(poolArray);
                _chars.CopyTo(converted);
                _chars = converted;
                _arrayToReturnToPool = poolArray;
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                var poolArray =
                    ArrayPool<TSymbol>.Shared.Rent(Math.Max(_pos + requiredAdditionalCapacity, _bytes.Length * 2));
                var converted = MemoryMarshal.Cast<TSymbol, byte>(poolArray);
                _bytes.CopyTo(converted);
                _bytes = converted;
                _arrayToReturnToPool = poolArray;
            }


            if (toReturn != null)
            {
                ArrayPool<TSymbol>.Shared.Return(toReturn);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndArray()
        {

            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16EndArray();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8EndArray();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginArray()
        {

            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16BeginArray();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8BeginArray();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteBeginObject()
        {

            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16BeginObject();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8BeginObject();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteEndObject()
        {

            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16EndObject();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8EndObject();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteValueSeparator()
        {

            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16ValueSeparator();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8ValueSeparator();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Null();
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Null();
            }
            else
            {
                ThrowNotSupportedException();
            }
        }


        public void WriteName(string name)
        {
            if (typeof(TSymbol) == typeof(char))
            {
                WriteUtf16Name(name);
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                WriteUtf8Name(name);
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        private static void ThrowNotSupportedException()
        {
            throw new NotImplementedException();
        }
    }
}