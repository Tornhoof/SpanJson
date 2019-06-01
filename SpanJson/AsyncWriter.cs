using System;
using System.Runtime.CompilerServices;

namespace SpanJson
{
    public class AsyncWriter<TSymbol> where TSymbol : struct
    {
        public TSymbol[] Data { get; }

        public AsyncWriter(TSymbol[] data)
        {
            Data = data;
        }

        public JsonWriter<TSymbol> Create()
        {
            return new JsonWriter<TSymbol>(Data);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WriteNull()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                // TODO
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                // TODO
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
                // TODO
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                // TODO
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
                // TODO
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                // TODO
            }
            else
            {
                ThrowNotSupportedException();
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }
    }
}
