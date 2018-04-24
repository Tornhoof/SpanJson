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
    public static partial class DateTimeParser
    {
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