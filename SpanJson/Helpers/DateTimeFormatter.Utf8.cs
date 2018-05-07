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

            ref var c = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value.DateTime, ref c, out bytesWritten);

            if (value.Offset == TimeSpan.Zero)
            {
                Unsafe.Add(ref c, bytesWritten++) = (byte) 'Z';

            }
            else
            {
                WriteTimeZone(value.Offset, ref c, ref bytesWritten);
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

            ref var c = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value, ref c, out bytesWritten);

            if (value.Kind == DateTimeKind.Local)
            {
                WriteTimeZone(TimeZoneInfo.Local.GetUtcOffset(value), ref c, ref bytesWritten);
            }
            else if (value.Kind == DateTimeKind.Utc)
            {
                Unsafe.Add(ref c, bytesWritten++) = (byte) 'Z';
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDateAndTime(DateTime value, ref byte c, out int bytesWritten)
        {
            WriteFourDigits((uint) value.Year, ref c, 0);
            Unsafe.Add(ref c, 4) = (byte) '-';
            WriteTwoDigits(value.Month, ref c, 5);
            Unsafe.Add(ref c, 7) = (byte) '-';
            WriteTwoDigits(value.Day, ref c, 8);
            Unsafe.Add(ref c, 10) = (byte) 'T';
            WriteTwoDigits(value.Hour, ref c, 11);
            Unsafe.Add(ref c, 13) = (byte) ':';
            WriteTwoDigits(value.Minute, ref c, 14);
            Unsafe.Add(ref c, 16) = (byte) ':';
            WriteTwoDigits(value.Second, ref c, 17);
            bytesWritten = 19;
            var fraction = (uint) ((ulong) value.Ticks % TimeSpan.TicksPerSecond);
            if (fraction > 0)
            {
                Unsafe.Add(ref c, 19) = (byte) '.';
                var pos = 20;
                WriteDigits(fraction, ref c, ref pos);
                bytesWritten = pos;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTimeZone(TimeSpan offset, ref byte c, ref int bytesWritten)
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

            Unsafe.Add(ref c, bytesWritten) = sign;
            WriteTwoDigits(offset.Hours, ref c, bytesWritten + 1);
            Unsafe.Add(ref c, bytesWritten + 3) = (byte) ':';
            WriteTwoDigits(offset.Minutes, ref c, bytesWritten + 4);
            bytesWritten += 6;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteFourDigits(uint value, ref byte c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 3) = (byte) (temp - (value * 10));

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 2) = (byte) (temp - (value * 10));

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (byte) (temp - (value * 10));

            Unsafe.Add(ref c, startIndex) = (byte) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTwoDigits(int value, ref byte c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (byte) (temp - (value * 10));
            Unsafe.Add(ref c, startIndex) = (byte) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDigits(uint value, ref byte c, ref int pos)
        {
            var digits = FormatterUtils.CountDigits(value);

            for (var i = digits; i > 0; i--)
            {
                var temp = '0' + value;
                value /= 10;
                Unsafe.Add(ref c, pos + i - 1) = (byte) (temp - value * 10);
            }

            pos += digits;
        }
    }
}
