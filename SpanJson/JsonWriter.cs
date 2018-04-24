using System;
using System.Buffers;
using System.Buffers.Text;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using SpanJson.Helpers;

namespace SpanJson
{
    public ref partial struct JsonWriter<T> where T : struct 
    {
        private static readonly char[] LongMinValue = long.MinValue.ToString().ToCharArray();
        private char[] _arrayToReturnToPool;
        private Span<char> _chars;
        private int _pos;

        public JsonWriter(int initialSize)
        {
            _arrayToReturnToPool = ArrayPool<char>.Shared.Rent(initialSize);
            _chars = _arrayToReturnToPool;
            _pos = 0;
        }

        public override string ToString()
        {
            var s = _chars.Slice(0, _pos).ToString();
            Dispose();
            return s;
        }

        public int Position => _pos;

        public char[] Data => _arrayToReturnToPool;



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            var toReturn = _arrayToReturnToPool;
            this = default; // for safety, to avoid using pooled array if this instance is erroneously appended to again
            if (toReturn != null)
            {
                ArrayPool<char>.Shared.Return(toReturn);
            }
        }

    }
}