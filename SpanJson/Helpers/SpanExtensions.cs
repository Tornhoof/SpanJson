using System;
using System.Runtime.CompilerServices;

namespace SpanJson.Helpers
{
    public static class SpanExtensions
    {
        /// <summary>
        /// This is based on https://github.com/dotnet/corefx/blob/master/src/Common/src/CoreLib/System/MemoryExtensions.cs
        /// Copyright (c) .NET Foundation and Contributors
        /// It just handles both bytes and chars.
        /// </summary>
        public static ReadOnlySpan<T> Trim<T>(this ReadOnlySpan<T> input) where T : struct
        {
            var start = 0;
            for (; start < input.Length; start++)
            {
                if (!IsWhiteSpace(input[start]))
                {
                    break;
                }
            }

            var end = input.Length - 1;
            for (; end >= start; end--)
            {
                if (!IsWhiteSpace(input[end]))
                {
                    break;
                }
            }

            return input.Slice(start, end - start + 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsWhiteSpace<T>(T value) where T : struct
        {
            if (typeof(T) == typeof(byte))
            {
                var b = Unsafe.As<T, byte>(ref value);
                return char.IsWhiteSpace((char) b);
            }

            if (typeof(T) == typeof(char))
            {
                var c = Unsafe.As<T, char>(ref value);
                return char.IsWhiteSpace(c);
            }

            ThrowNotSupportedException();
            return default;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }
    }
}
