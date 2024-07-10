using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SpanJson.Helpers
{
    /// <summary>
    ///     Largely based on
    ///     https://raw.githubusercontent.com/dotnet/corefx/f5d31619f821e7b4a0bcf7f648fe1dc2e4e2f09f/src/System.Memory/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
    ///     Copyright (c) .NET Foundation and Contributors
    ///     See THIRD_PARTY_NOTICES for license
    ///     Modified to work for char and removed the 7 fractions requirement
    ///     Non-UTC is slow, as it needs to go through the timezone stuff to get the right offset
    /// </summary>
    public static partial class DateTimeParser
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseDateTimeOffset(in ReadOnlySpan<char> source, out DateTimeOffset value, out int charsConsumed)
        {
            if (TryParseDate(source, out var date, out charsConsumed))
            {
                switch (date.Kind)
                {
                    // for local/unspecified we go through datetime to make sure we get the offsets correct
                    case DateTimeKind.Local:
                    {
                        value = new DateTime(date.Ticks, date.Kind);
                        return true;
                    }
                    case DateTimeKind.Utc:
                    case DateTimeKind.Unspecified:
                    {
                        value = new DateTimeOffset(date.Ticks, date.Offset);
                        return true;
                    }
                }
            }

            value = default;
            charsConsumed = 0;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseDateTime(in ReadOnlySpan<char> source, out DateTime value, out int charsConsumed)
        {
            if (TryParseDate(source, out var date, out charsConsumed))
            {
                value = new DateTime(date.Ticks, date.Kind);
                switch (date.Kind)
                {
                    case DateTimeKind.Unspecified:
                    {
                        var offset = TimeZoneInfo.Local.GetUtcOffset(value) - date.Offset;
                        value = value.Add(offset);
                        return true;
                    }
                    case DateTimeKind.Local:
                    case DateTimeKind.Utc:
                    {
                        return true;
                    }
                }
            }

            value = default;
            charsConsumed = 0;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseDateOnly(ReadOnlySpan<char> source, out DateOnly value)
        {
            if (source.Length != 10 || source[4] != '-' || source[7] != '-')
            {
                value = default;
                return false;
            }

            int year;
            {
                var digit1 = source[0] - 48U; // '0' (this makes it uint)
                var digit2 = source[1] - 48U;
                var digit3 = source[2] - 48U;
                var digit4 = source[3] - 48U;

                if (digit1 > 9 || digit2 > 9 || digit3 > 9 || digit4 > 9)
                {
                    value = default;
                    return false;
                }

                year = (int) (digit1 * 1000 + digit2 * 100 + digit3 * 10 + digit4);
            }

            int month;
            {
                var digit1 = source[5] - 48U;
                var digit2 = source[6] - 48U;

                if (digit1 > 2 || digit2 > 9 || digit1 == 1 && digit2 > 2)
                {
                    value = default;
                    return false;
                }

                month = (int) (digit1 * 10 + digit2);
            }

            int day;
            {
                var digit1 = source[8] - 48U;
                var digit2 = source[9] - 48U;

                if (digit1 > 3 || digit2 > 9 || digit1 == 3 && digit2 > 1)
                {
                    value = default;
                    return false;
                }

                day = (int) (digit1 * 10 + digit2);
            }

            value = new DateOnly(year, month, day);
            return true;
        }

        // <summary>
        // Supports the formats:
        // 23:59
        // 23:59:59
        // 23:59:59.9
        // 23:59:59.9999999
        // </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseTimeOnly(ReadOnlySpan<char> source, out TimeOnly value, out int charsConsumed)
        {
            var ticks = 0L;
            switch (source.Length) {
                case < 5:
                case 6: // Ending in :
                case 7: // Ending in tens of seconds
                case 9: // Ending in .
                case > 16:
                    goto InvalidDate;
                case 16: // 0.000_000_1 seconds
                {
                    var digit = source[15] - 48U; // '0' (this makes it uint)
                    if (digit > 9) goto InvalidDate;
                    ticks += digit;
                    goto case 15;
                }
                case 15: // 0.000_001 seconds
                {
                    var digit = source[14] - 48U;
                    if (digit > 9) goto InvalidDate;
#if NET7_0_OR_GREATER
                    ticks += TimeSpan.TicksPerMicrosecond * digit;
#else
                    ticks += 10 * digit;
#endif
                    goto case 14;
                }
                case 14: // 0.000_01 seconds
                {
                    var digit = source[13] - 48U;
                    if (digit > 9) goto InvalidDate;
#if NET7_0_OR_GREATER
                    ticks += 10 * TimeSpan.TicksPerMicrosecond * digit;
#else
                    ticks += 100 * digit;
#endif
                    goto case 13;
                }
                case 13: // 0.000_1 seconds
                {
                    var digit = source[12] - 48U;
                    if (digit > 9) goto InvalidDate;
#if NET7_0_OR_GREATER
                    ticks += 100 * TimeSpan.TicksPerMicrosecond * digit;
#else
                    ticks += 1000 * digit;
#endif
                    goto case 12;
                }
                case 12: // 0.001 seconds
                {
                    var digit = source[11] - 48U;
                    if (digit > 9) goto InvalidDate;
                    ticks += TimeSpan.TicksPerMillisecond * digit;
                    goto case 11;
                }
                case 11: // 0.01 seconds
                {
                    var digit = source[10] - 48U;
                    if (digit > 9) goto InvalidDate;
                    ticks += 10 * TimeSpan.TicksPerMillisecond * digit;
                    goto case 10;
                }
                case 10: // 0.1 seconds
                {
                    if (source[8] != '.') goto InvalidDate;
                    var digit = source[9] - 48U;
                    if (digit > 9) goto InvalidDate;
                    ticks += 100 * TimeSpan.TicksPerMillisecond * digit;
                    goto case 8;
                }
                case 8: // Seconds
                {
                    if (source[5] != ':') goto InvalidDate;
                    var digit1 = source[6] - 48U;
                    var digit2 = source[7] - 48U;
                    if (digit1 > 5 || digit2 > 9) goto InvalidDate;
                    ticks += TimeSpan.TicksPerSecond * (digit1 * 10 + digit2);
                    goto case 5;
                }
                case 5: // Hours and minutes
                {
                    var hourDigit1 = source[0] - 48U;
                    var hourDigit2 = source[1] - 48U;
                    var minuteDigit1 = source[3] - 48U;
                    var minuteDigit2 = source[4] - 48U;
                    if (source[2] != ':' || hourDigit1 > 2 || hourDigit2 > 9 || hourDigit1 == 1 && hourDigit2 > 2 || minuteDigit1 > 5 || minuteDigit2 > 9) goto InvalidDate;
                    ticks += TimeSpan.TicksPerHour * (hourDigit1 * 10 + hourDigit2);
                    ticks += TimeSpan.TicksPerMinute * (minuteDigit1 * 10 + minuteDigit2);
                    break;
                }
            }

            charsConsumed = source.Length;
            value = new TimeOnly(ticks);
            return true;

            InvalidDate:
            value = default;
            charsConsumed = 0;
            return false;
        }

        /// <summary>
        ///     2017-06-12T05:30:45.7680000-07:00
        ///     2017-06-12T05:30:45.7680000Z
        ///     2017-06-12T05:30:45.7680000 (local)
        ///     2017-06-12T05:30:45.768-07:00
        ///     2017-06-12T05:30:45.768Z
        ///     2017-06-12T05:30:45.768 (local)
        ///     2017-06-12T05:30:45
        ///     2017-06-12T05:30:45Z
        ///     2017-06-12 (local)
        /// </summary>
        private static bool TryParseDate(in ReadOnlySpan<char> source, out Date value, out int charsConsumed)
        {
            if (source.Length < 10)
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int year;
            {
                var digit1 = source[0] - 48U; // '0' (this makes it uint)
                var digit2 = source[1] - 48U;
                var digit3 = source[2] - 48U;
                var digit4 = source[3] - 48U;

                if (digit1 > 9 || digit2 > 9 || digit3 > 9 || digit4 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                year = (int) (digit1 * 1000 + digit2 * 100 + digit3 * 10 + digit4);
            }

            if (source[4] != '-')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int month;
            {
                var digit1 = source[5] - 48U;
                var digit2 = source[6] - 48U;

                if (digit1 > 2 || digit2 > 9 || digit1 == 1 && digit2 > 2)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                month = (int) (digit1 * 10 + digit2);
            }

            if (source[7] != '-')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int day;
            {
                var digit1 = source[8] - 48U;
                var digit2 = source[9] - 48U;

                if (digit1 > 3 || digit2 > 9 || digit1 == 3 && digit2 > 1)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                day = (int) (digit1 * 10 + digit2);
            }

            if (source.Length == 10)
            {
                value = new Date(year, month, day, 0, 0, 0, 0, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = 10;
                return true;
            }

            if (source[10] != 'T')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int hour;
            {
                var digit1 = source[11] - 48U;
                var digit2 = source[12] - 48U;

                if (digit1 > 2 || digit2 > 9 || digit1 == 2 && digit2 > 3)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                hour = (int) (digit1 * 10 + digit2);
            }

            if (source.Length == 13)
            {
                value = new Date(year, month, day, hour, 0, 0, 0, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = 13;
                return true;
            }

            if (source[13] != ':')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int minute;
            {
                var digit1 = source[14] - 48U;
                var digit2 = source[15] - 48U;

                if (digit1 > 5 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                minute = (int) (digit1 * 10 + digit2);
            }

            if (source.Length == 16)
            {
                value = new Date(year, month, day, hour, minute, 0, 0, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = 16;
                return true;
            }

            if (source[16] != ':')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int second;
            {
                var digit1 = source[17] - 48U;
                var digit2 = source[18] - 48U;

                if (digit1 > 5 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                second = (int) (digit1 * 10 + digit2);
            }

            if (source.Length == 19)
            {
                value = new Date(year, month, day, hour, minute, second, 0, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = 19;
                return true;
            }

            var currentOffset = 19; // up until here everything is fixed
            var fraction = 0;
            if (source.Length > currentOffset + 1 && source[currentOffset] == '.')
            {
                currentOffset++;
                var temp = source[currentOffset++] - 48U; // one needs to exist
                if (temp > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                var maxDigits = source.Length - currentOffset;
                int digitCount;
                for (digitCount = 0; digitCount < maxDigits; digitCount++)
                {
                    var digit = source[currentOffset] - 48U;
                    if (digit > 9)
                    {
                        break;
                    }

                    if (digitCount < 6)
                    {
                        temp = temp * 10 + digit;
                    }

                    currentOffset++;
                }

                digitCount++; // add one for the first digit
                switch (digitCount)
                {
                    case 7:
                        break;

                    case 6:
                        temp *= 10;
                        break;

                    case 5:
                        temp *= 100;
                        break;

                    case 4:
                        temp *= 1000;
                        break;

                    case 3:
                        temp *= 10000;
                        break;

                    case 2:
                        temp *= 100000;
                        break;
                    case 1:
                        temp *= 1000000;
                        break;
                }

                fraction = (int) temp;
            }

            var offsetChar = source.Length <= currentOffset ? default : source[currentOffset];
            if (offsetChar != 'Z' && offsetChar != '+' && offsetChar != '-')
            {
                value = new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = currentOffset;
                return true;
            }

            currentOffset++;

            if (offsetChar == 'Z')
            {
                value = new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Utc, TimeSpan.Zero);
                charsConsumed = currentOffset;
                return true;
            }

            Debug.Assert(offsetChar == '+' || offsetChar == '-');
            int offsetHours;
            {
                var digit1 = source[currentOffset++] - 48U;
                var digit2 = source[currentOffset++] - 48U;

                if (digit1 > 2 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                offsetHours = (int) (digit1 * 10 + digit2);
            }
            if (source[currentOffset++] != ':')
            {
                value = default;
                charsConsumed = 0;
                return false;
            }

            int offsetMinutes;
            {
                var digit1 = source[currentOffset++] - 48U;
                var digit2 = source[currentOffset++] - 48U;

                if (digit1 > 6 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                offsetMinutes = (int) (digit1 * 10 + digit2);
            }

            var offsetTicks = (offsetHours * 3600 + offsetMinutes * 60) * TimeSpan.TicksPerSecond;
            var timeSpan = new TimeSpan(offsetChar == '-' ? -offsetTicks : offsetTicks);
            value = timeSpan == TimeSpan.Zero
                ? new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Utc, timeSpan)
                : new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Unspecified, timeSpan);

            charsConsumed = currentOffset;
            return true;
        }
    }
}