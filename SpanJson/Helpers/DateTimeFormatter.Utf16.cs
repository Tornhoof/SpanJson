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
            if (output.Length < 33)
            {
                charsWritten = default;
                return false;
            }

            ref var c = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value.DateTime, ref c, out charsWritten);

            if (value.Offset == TimeSpan.Zero)
            {
                Unsafe.Add(ref c, charsWritten++) = 'Z';
            }
            else
            {
                WriteTimeZone(value.Offset, ref c, ref charsWritten);
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryFormat(DateTime value, Span<char> output, out int charsWritten)
        {
            if (output.Length < 33)
            {
                charsWritten = default;
                return false;
            }

            ref var c = ref MemoryMarshal.GetReference(output);
            WriteDateAndTime(value, ref c, out charsWritten);

            if (value.Kind == DateTimeKind.Local)
            {
                WriteTimeZone(TimeZoneInfo.Local.GetUtcOffset(value), ref c, ref charsWritten);
            }
            else if (value.Kind == DateTimeKind.Utc)
            {
                Unsafe.Add(ref c, charsWritten++) = 'Z';
            }

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDateAndTime(DateTime value, ref char c, out int charsWritten)
        {
            WriteFourDigits((uint) value.Year, ref c, 0);
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
            var fraction = (uint) ((ulong) value.Ticks % TimeSpan.TicksPerSecond);
            if (fraction > 0)
            {
                Unsafe.Add(ref c, 19) = '.';
                WriteDigits(fraction, ref c, 20);
                charsWritten = 27;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTimeZone(TimeSpan offset, ref char c, ref int charsWritten)
        {
            char sign;
            if (offset < default(TimeSpan))
            {
                sign = '-';
                offset = TimeSpan.FromTicks(-offset.Ticks);
            }
            else
            {
                sign = '+';
            }

            Unsafe.Add(ref c, charsWritten) = sign;
            WriteTwoDigits(offset.Hours, ref c, charsWritten + 1);
            Unsafe.Add(ref c, charsWritten + 3) = ':';
            WriteTwoDigits(offset.Minutes, ref c, charsWritten + 4);
            charsWritten += 6;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteFourDigits(uint value, ref char c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 3) = (char) (temp - value * 10);

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 2) = (char) (temp - value * 10);

            temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (char) (temp - value * 10);

            Unsafe.Add(ref c, startIndex) = (char) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteTwoDigits(int value, ref char c, int startIndex)
        {
            var temp = '0' + value;
            value /= 10;
            Unsafe.Add(ref c, startIndex + 1) = (char) (temp - value * 10);
            Unsafe.Add(ref c, startIndex) = (char) ('0' + value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void WriteDigits(uint value, ref char c, int pos)
        {
            for (var i = 7; i > 0; i--)
            {
                ulong temp = '0' + value;
                value /= 10;
                Unsafe.Add(ref c, pos + i - 1) = (char) (temp - value * 10);
            }
        }
    }
}