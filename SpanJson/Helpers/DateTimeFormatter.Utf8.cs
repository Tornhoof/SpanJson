using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson.Helpers
{
    public static partial class DateTimeFormatter
    {

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTimeOffset value, Span<byte> output,  out int bytesWritten)
        {
            bytesWritten = default;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTime value, Span<byte> output, out int bytesWritten)
        {
            bytesWritten = default;
            return true;
        }

        private static bool TryFormat(DateTime value, TimeSpan offset, Span<byte> output, out int bytesWritten)
        {
            if (output.Length < 33)
            {
                bytesWritten = default;
                return false;
            }
            bytesWritten = default;
            return false;
        }
    }
}
