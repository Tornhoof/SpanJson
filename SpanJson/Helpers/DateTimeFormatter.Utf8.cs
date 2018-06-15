using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Helpers
{
    public static partial class DateTimeFormatter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTimeOffset value, Span<byte> output, out int bytesWritten)
        {
            if (output.Length < 33)
            {
                bytesWritten = default;
                return false;
            }

            ref var b = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value.DateTime, ref b, out bytesWritten);

            if (value.Offset == TimeSpan.Zero)
            {
                Unsafe.Add(ref b, bytesWritten++) = (byte) 'Z';
            }
            else
            {
                WriteTimeZone(value.Offset, ref b, ref bytesWritten);
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTime value, Span<byte> output, out int bytesWritten)
        {
            if (output.Length < 33)
            {
                bytesWritten = default;
                return false;
            }

            ref var b = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value, ref b, out bytesWritten);

            if (value.Kind == DateTimeKind.Local)
            {
                WriteTimeZone(TimeZoneInfo.Local.GetUtcOffset(value), ref b, ref bytesWritten);
            }
            else if (value.Kind == DateTimeKind.Utc)
            {
                Unsafe.Add(ref b, bytesWritten++) = (byte) 'Z';
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDateAndTime(DateTime value, ref byte b, out int bytesWritten)
        {
            WriteFourDigits((uint) value.Year, ref b, 0);
            Unsafe.Add(ref b, 4) = (byte) '-';
            WriteTwoDigits(value.Month, ref b, 5);
            Unsafe.Add(ref b, 7) = (byte) '-';
            WriteTwoDigits(value.Day, ref b, 8);
            Unsafe.Add(ref b, 10) = (byte) 'T';
            WriteTwoDigits(value.Hour, ref b, 11);
            Unsafe.Add(ref b, 13) = (byte) ':';
            WriteTwoDigits(value.Minute, ref b, 14);
            Unsafe.Add(ref b, 16) = (byte) ':';
            WriteTwoDigits(value.Second, ref b, 17);
            bytesWritten = 19;
            var fraction = (uint) ((ulong) value.Ticks % TimeSpan.TicksPerSecond);
            if (fraction > 0)
            {
                Unsafe.Add(ref b, 19) = (byte) '.';
                WriteDigits(fraction, ref b, 20);
                bytesWritten = 27;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTimeZone(TimeSpan offset, ref byte b, ref int bytesWritten)
        {
            byte sign;
            if (offset < default(TimeSpan))
            {
                sign = (byte) '-';
                offset = TimeSpan.FromTicks(-offset.Ticks);
            }
            else
            {
                sign = (byte) '+';
            }

            Unsafe.Add(ref b, bytesWritten) = sign;
            WriteTwoDigits(offset.Hours, ref b, bytesWritten + 1);
            Unsafe.Add(ref b, bytesWritten + 3) = (byte) ':';
            WriteTwoDigits(offset.Minutes, ref b, bytesWritten + 4);
            bytesWritten += 6;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteFourDigits(uint value, ref byte b, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref b, startIndex + 3) = (byte) (temp - value * 10);

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref b, startIndex + 2) = (byte) (temp - value * 10);

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref b, startIndex + 1) = (byte) (temp - value * 10);

            Unsafe.Add(ref b, startIndex) = (byte) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTwoDigits(int value, ref byte b, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref b, startIndex + 1) = (byte) (temp - value * 10);
            Unsafe.Add(ref b, startIndex) = (byte) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDigits(uint value, ref byte b, int pos)
        {
            for (var i = 7; i > 0; i--)
            {
                ulong temp = '0' + value;
                value /= 10;
                Unsafe.Add(ref b, pos + i - 1) = (byte) (temp - value * 10);
            }
        }
    }
}