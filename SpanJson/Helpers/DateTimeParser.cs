using System;
using System.Security.Cryptography;

namespace SpanJson.Helpers
{
    /// <summary>
    ///     Largely based on
    ///     https://raw.githubusercontent.com/dotnet/corefx/f5d31619f821e7b4a0bcf7f648fe1dc2e4e2f09f/src/System.Memory/src/System/Buffers/Text/Utf8Parser/Utf8Parser.Date.O.cs
    ///     Copyright (c) .NET Foundation and Contributors
    ///     Modified to work for char and removed the 7 fractions requirement
    ///     Non-UTC is slow, as it needs to go through the timezone stuff to get the right offset
    /// </summary>
    public static partial class DateTimeParser
    {
        private readonly ref struct Date
        {
            public readonly long Ticks;
            public readonly DateTimeKind Kind;
            public readonly TimeSpan Offset;

            public Date(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind,
                TimeSpan offset)
            {
                var days = DateTime.IsLeapYear(year) ? DaysToMonth366 : DaysToMonth365;
                var yearMinusOne = year - 1;
                var totalDays = yearMinusOne * 365 + yearMinusOne / 4 - yearMinusOne / 100 + yearMinusOne / 400 + days[month - 1] + day - 1;
                var ticks = totalDays * TimeSpan.TicksPerDay;
                var totalSeconds = hour * 3600 + minute * 60 + second;
                ticks += totalSeconds * TimeSpan.TicksPerSecond;
                ticks += fraction;
                Ticks = ticks;
                Kind = kind;
                Offset = offset;
            }

            private static readonly int[] DaysToMonth365 = {0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365};
            private static readonly int[] DaysToMonth366 = {0, 31, 60, 91, 121, 152, 182, 213, 244, 274, 305, 335, 366};
        }
    }
}