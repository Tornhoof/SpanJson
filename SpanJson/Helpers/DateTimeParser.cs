using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SpanJson.Helpers
{
    /// <summary>
    ///     Largely based on
    ///     https://raw.githubusercontent.com/dotnet/corefx/f5d31619f821e7b4a0bcf7f648fe1dc2e4e2f09f/src/System.Memory/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
    ///     Copyright (c) .NET Foundation and Contributors
    ///     Modified to work for char and removed the 7 fractions requirement
    ///     Non-UTC is slow, as it needs to go through the timezone stuff to get the right offset
    /// </summary>
    public static class DateTimeParser
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseDateTimeOffset(ReadOnlySpan<char> source, out DateTimeOffset value, out int charsConsumed)
        {
            if (TryParseDate(source, out var date, out charsConsumed))
                switch (date.Kind)
                {
                    // for local/unspecified we go through datetime to make sure we get the offsets correct
                    case DateTimeKind.Local:
                    {
                        value = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second,
                            date.Kind).AddTicks(date.Fraction);
                        return true;
                    }
                    case DateTimeKind.Unspecified:
                    {
                        value = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second,
                            date.Kind).AddTicks(date.Fraction), date.Offset);
                        return true;
                    }
                    case DateTimeKind.Utc:
                    {
                        value = new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second,
                            date.Offset).AddTicks(date.Fraction);
                        return true;
                    }
                }

            value = default;
            charsConsumed = 0;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryParseDateTime(ReadOnlySpan<char> source, out DateTime value, out int charsConsumed)
        {
            if (TryParseDate(source, out var date, out charsConsumed))
            {
                value = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Kind)
                    .AddTicks(date.Fraction);
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

        /// <summary>
        ///     2017-06-12T05:30:45.7680000-07:00
        ///     2017-06-12T05:30:45.7680000Z
        ///     2017-06-12T05:30:45.7680000 (local)
        ///     2017-06-12T05:30:45.768-07:00
        ///     2017-06-12T05:30:45.768Z
        ///     2017-06-12T05:30:45.768 (local)
        ///     2017-06-12T05:30:45
        ///     2017-06-12T05:30:45Z
        ///     2017-06-12T05:30:45 (local)
        /// </summary>
        private static bool TryParseDate(ReadOnlySpan<char> source, out Date value,
            out int charsConsumed)
        {
            if (source.Length < 19)
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

                if (digit1 > 9 || digit2 > 9)
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

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                day = (int) (digit1 * 10 + digit2);
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

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                hour = (int) (digit1 * 10 + digit2);
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

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                minute = (int) (digit1 * 10 + digit2);
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

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                second = (int) (digit1 * 10 + digit2);
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

                var maxDigits = Math.Min(6, source.Length - currentOffset); // max 6 remaining fraction digits
                for (var i = 0; i < maxDigits; i++)
                {
                    var digit = source[currentOffset] - 48U;
                    if (digit > 9) break;

                    temp = temp * 10 + digit;
                    currentOffset++;
                }

                fraction = (int) temp;
            }

            var offsetChar = source.Length <= currentOffset ? default : source[currentOffset++];
            if (offsetChar != 'Z' && offsetChar != '+' && offsetChar != '-')
            {
                value = new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Local, TimeSpan.Zero);
                charsConsumed = currentOffset;
                return true;
            }

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

                if (digit1 > 9 || digit2 > 9)
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

                if (digit1 > 9 || digit2 > 9)
                {
                    value = default;
                    charsConsumed = 0;
                    return false;
                }

                offsetMinutes = (int) (digit1 * 10 + digit2);
            }
            offsetHours = offsetChar == '-' ? -offsetHours : offsetHours;
            var timeSpan = new TimeSpan(offsetHours, offsetMinutes, 0);
            value = timeSpan == TimeSpan.Zero
                ? new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Utc, timeSpan)
                : new Date(year, month, day, hour, minute, second, fraction, DateTimeKind.Unspecified, timeSpan);

            charsConsumed = currentOffset;
            return true;
        }

        private readonly ref struct Date
        {
            public readonly int Year;
            public readonly int Month;
            public readonly int Day;
            public readonly int Hour;
            public readonly int Minute;
            public readonly int Second;
            public readonly int Fraction;
            public readonly DateTimeKind Kind;
            public readonly TimeSpan Offset;

            public Date(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind,
                TimeSpan offset)
            {
                Year = year;
                Month = month;
                Day = day;
                Hour = hour;
                Minute = minute;
                Second = second;
                Fraction = fraction;
                Kind = kind;
                Offset = offset;
            }
        }
    }
}