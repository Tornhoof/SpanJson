using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpanJson.Helpers
{
    public static partial class DateTimeFormatter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTimeOffset value, Span<char> output, out int charsWritten)
        {
            return TryFormat(value.DateTime, value.Offset, value.Offset == default ? DateTimeKind.Utc : DateTimeKind.Local, output, out charsWritten);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTime value, Span<char> output, out int charsWritten)
        {
            var offset = value.Kind == DateTimeKind.Local ? TimeZoneInfo.Local.GetUtcOffset(value) : TimeSpan.Zero;
            return TryFormat(value, offset, value.Kind, output, out charsWritten);
        }

        private static bool TryFormat(DateTime value, TimeSpan offset, DateTimeKind kind, Span<char> output, out int charsWritten)
        {
            if (output.Length < 33)
            {
                charsWritten = default;
                return false;
            }

            {
                var unused = output[32];
            }

            ref var c = ref MemoryMarshal.GetReference(output);
            WriteFourDigits(value.Year, ref c, 0);
            Unsafe.Add(ref c, 4) = '-';
            WriteTwoDigits(value.Month, ref c, 5);
            Unsafe.Add(ref c, 7) = '-';
            WriteTwoDigits(value.Day, ref c, 8);
            Unsafe.Add(ref c, 10) = 'T';
            WriteTwoDigits(value.Hour, ref c, 11);
            Unsafe.Add(ref c, 13) = ':';
            WriteTwoDigits(value.Minute, ref c, 14);
            Unsafe.Add(ref c, 16) = ':';
            WriteTwoDigits(value.Second, ref c, 17);
            charsWritten = 19;
            var fraction = (uint)((ulong)value.Ticks % TimeSpan.TicksPerSecond);
            if (fraction > 0)
            {
                Unsafe.Add(ref c, 19) = '.';
                var pos = 20;
                WriteDigits(fraction, ref c, ref pos);
                charsWritten = pos;
            }

            if (kind == DateTimeKind.Local)
            {
                Unsafe.Add(ref c, charsWritten) = offset < default(TimeSpan) ? '-' : '+';
                WriteTwoDigits(offset.Hours, ref c, charsWritten + 1);
                Unsafe.Add(ref c, charsWritten + 3) = ':';
                WriteTwoDigits(offset.Minutes, ref c, charsWritten + 4);
                charsWritten += 6;
            }
            else if (kind == DateTimeKind.Utc)
            {
                Unsafe.Add(ref c, charsWritten++) = 'Z';
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteFourDigits(int value, ref char c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 3) = (char) (temp - (value * 10));

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 2) = (char) (temp - (value * 10));

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (char) (temp - (value * 10));

            Unsafe.Add(ref c, startIndex) = (char) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTwoDigits(int value, ref char c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (char) (temp - (value * 10));
            Unsafe.Add(ref c, startIndex) = (char) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDigits(uint value, ref char c, ref int pos)
        {
            var digits = FormatterUtils.CountDigits(value);

            for (var i = digits; i > 0; i--)
            {
                var temp = '0' + value;
                value /= 10;
                Unsafe.Add(ref c, pos + i - 1) = (char) (temp - value * 10);
            }

            pos += digits;
        }
    }
}
