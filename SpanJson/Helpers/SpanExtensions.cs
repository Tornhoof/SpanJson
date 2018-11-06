using System;
using System.Runtime.InteropServices;

namespace SpanJson.Helpers
{
    public static class SpanExtensions
    {
        public static ReadOnlySpan<T> Trim<T>(this ReadOnlySpan<T> input) where T : struct 
        {
            if (typeof(T) == typeof(char))
            {
                var charSpan = MemoryMarshal.Cast<T, char>(input);
                var trimmed = MemoryExtensions.Trim(charSpan);
                return MemoryMarshal.Cast<char, T>(trimmed);
            }

            if (typeof(T) == typeof(byte))
            {
                // TODO: trim of bytes
            }
            return input;
        }
    }
}
