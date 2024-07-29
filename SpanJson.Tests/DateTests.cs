using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests
{
    public class DateTests
    {
        [Theory]
        [InlineData("2017-06-12T05:30:45.7680000-07:30", 33, 2017, 6, 12, 5, 30, 45, 7680000, true, 7, 30, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.7680000Z", 28, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.7680000", 27, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.768+08:00", 29, 2017, 6, 12, 5, 30, 45, 7680000, false, 8, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.768Z", 24, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.768", 23, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45+01:00", 25, 2017, 6, 12, 5, 30, 45, 0, false, 1, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45Z", 20, 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45", 19, 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30", 16, 2017, 6, 12, 5, 30, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05", 13, 2017, 6, 12, 5, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0010+01:00", 30, 2017, 6, 12, 5, 30, 45, 10000, false, 1, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.0010Z", 25, 2017, 6, 12, 5, 30, 45, 10000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0010", 24, 2017, 6, 12, 5, 30, 45, 10000, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.760738998+01:00", 35, 2017, 6, 12, 5, 30, 45, 7607389, false, 1, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.760738998Z", 30, 2017, 6, 12, 5, 30, 45, 7607389, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.760738998", 29, 2017, 6, 12, 5, 30, 45, 7607389, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00", 19, 2017, 6, 13, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00", 16, 2017, 6, 13, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24", 13, 2017, 6, 13, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:01", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:01", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.1", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.01", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.001", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.0001", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.00001", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.000001", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T24:00:00.0000001", 0, 0, 0, 0, 0, 0, 0, 0, false, 0, 0, DateTimeKind.Unspecified)]
        public void Parse(string input, int length, int year, int month, int day, int hour, int minute,
            int second, int fraction, bool negative, int offsetHours, int offsetMinutes, DateTimeKind kind)
        {
            var ucs2DateTimeOffsetParseResult = DateTimeParser.TryParseDateTimeOffset(input.AsSpan(), out var ucs2DtoValue, out var dtoConsumed);
            Assert.Equal(length, dtoConsumed);
            if (length > 0)
            {
                Assert.True(ucs2DateTimeOffsetParseResult);
                Assert.Equal(length, input.Length);
            }
            else
            {
                Assert.False(ucs2DateTimeOffsetParseResult);
            }
            var ucs2DateTimeParseResult = DateTimeParser.TryParseDateTime(input.AsSpan(), out var ucs2DtValue, out var dtConsumed);
            if (length > 0)
            {
                Assert.True(ucs2DateTimeParseResult);
                Assert.Equal(length, dtConsumed);
                AssertDateTime(ucs2DtoValue.DateTime, year, month, day, hour, minute, second, fraction);
                var offset = new TimeSpan(offsetHours, offsetMinutes, 0) * (negative ? -1 : 1);
                switch (kind)
                {
                    case DateTimeKind.Local:
                        Assert.Equal(offset, ucs2DtoValue.DateTime - ucs2DtoValue.UtcDateTime);
                        Assert.Equal(ucs2DtValue, ucs2DtoValue.LocalDateTime);
                        break;
                    case DateTimeKind.Unspecified:
                        Assert.Equal(offset, ucs2DtoValue.DateTime - ucs2DtoValue.LocalDateTime);
                        Assert.Equal(ucs2DtValue, ucs2DtoValue.DateTime);
                        break;
                    case DateTimeKind.Utc:
                        Assert.Equal(offset, ucs2DtoValue.DateTime - ucs2DtoValue.UtcDateTime);
                        Assert.Equal(ucs2DtValue, ucs2DtoValue.UtcDateTime);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
                }
            }
            else
            {
                Assert.False(ucs2DateTimeParseResult);
            }

            var utf8Input = Encoding.UTF8.GetBytes(input);
            var utf8DateTimeOffsetParseResult = DateTimeParser.TryParseDateTimeOffset(utf8Input, out var utf8DtoValue, out var utf8DtoConsumed);
            Assert.Equal(length, utf8DtoConsumed);
            if (length > 0)
            {
                Assert.True(utf8DateTimeOffsetParseResult);
            }
            else
            {
                Assert.False(utf8DateTimeOffsetParseResult);
            }

            var utf8DateTimeParseResult = DateTimeParser.TryParseDateTime(utf8Input, out var utf8dtValue, out var utf8DtConsumed);
            Assert.Equal(length, utf8DtConsumed);
            if (length > 0)
            {
                Assert.True(utf8DateTimeParseResult);
            }
            else
            {
                Assert.False(utf8DateTimeParseResult);
            }

            Assert.Equal(ucs2DtoValue, utf8DtoValue);
            Assert.Equal(ucs2DtValue, utf8dtValue);
        }

        [Theory]
        [InlineData("1970-01-01", 10, 1970, 01, 01)]
        [InlineData("2017-06-12", 10, 2017, 6, 12)]
        [InlineData("2050-01-01", 10, 2050, 01, 01)]
        public void ParseDate(string input, int length, int year, int month, int day)
        {
            var charSpan = input.AsSpan();
            var byteSpan = Encoding.UTF8.GetBytes(input);

            Assert.True(DateTimeParser.TryParseDateTimeOffset(charSpan, out var dtoValue, out var dtoConsumed));
            Assert.Equal(length, input.Length);
            Assert.Equal(length, dtoConsumed);
            Assert.True(DateTimeParser.TryParseDateTime(charSpan, out var dtValue, out var dtConsumed));
            Assert.Equal(length, dtConsumed);
            Assert.True(DateTimeParser.TryParseDateOnly(charSpan, out var doValue, out var dateOnlyConsumed));
            Assert.Equal(length, dateOnlyConsumed);

            Assert.Equal(year, dtoValue.Year);
            Assert.Equal(month, dtoValue.Month);
            Assert.Equal(day, dtValue.Day);
            Assert.Equal(year, dtValue.Year);
            Assert.Equal(month, dtValue.Month);
            Assert.Equal(day, dtValue.Day);
            Assert.Equal(year, doValue.Year);
            Assert.Equal(month, doValue.Month);
            Assert.Equal(day, doValue.Day);

            Assert.True(DateTimeOffset.TryParseExact(charSpan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var bclDtoValue));
            Assert.True(DateTime.TryParseExact(charSpan, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var bclDtValue));

            Assert.Equal(bclDtValue, dtValue);
            Assert.Equal(bclDtoValue, dtoValue);

            Assert.True(DateTimeParser.TryParseDateTimeOffset(byteSpan, out var utf8dtoValue, out _));
            Assert.Equal(length, input.Length);
            Assert.Equal(length, dtoConsumed);
            Assert.True(DateTimeParser.TryParseDateTime(byteSpan, out var utf8dtValue, out _));
            Assert.True(DateTimeParser.TryParseDateOnly(byteSpan, out var utf8doValue, out _));

            Assert.Equal(bclDtValue, utf8dtValue);
            Assert.Equal(bclDtoValue, utf8dtoValue);
            Assert.Equal(doValue, utf8doValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData("2017-13-01")]
        [InlineData("2017-12-32")]
        public void ParseInvalidDate(string input)
        {
            var charSpan = input.AsSpan();
            var byteSpan = Encoding.UTF8.GetBytes(input);

            Assert.False(DateTimeParser.TryParseDateOnly(charSpan, out var toValue, out var charsConsumed));
            Assert.Equal(0, charsConsumed);
            Assert.False(DateTimeParser.TryParseDateOnly(byteSpan, out var utf8ToValue, out var bytesConsumed));
            Assert.Equal(0, bytesConsumed);
            DateOnly defaultToValue = default;
            Assert.Equal(defaultToValue, toValue);
            Assert.Equal(defaultToValue, utf8ToValue);
        }

        [Theory]
        [InlineData("")]
        [InlineData("0")]
        [InlineData("12:")]
        [InlineData("12:5")]
        [InlineData("12:59:")]
        [InlineData("12:59:59.")]
        [InlineData("12:59:59:9")]
        [InlineData("12:59:59.99999999")]
        [InlineData("24:00")]
        [InlineData("24:00:00")]
        [InlineData("24:00:00.0")]
        [InlineData("01:00PM")]
        [InlineData("1:00PM")]
        [InlineData("1PM")]
        [InlineData("01:00 PM")]
        [InlineData("1:00 PM")]
        [InlineData("1 PM")]
        public void ParseInvalidTime(string input)
        {
            var charSpan = input.AsSpan();
            var byteSpan = Encoding.UTF8.GetBytes(input);

            Assert.False(DateTimeParser.TryParseTimeOnly(charSpan, out var toValue, out var charsConsumed));
            Assert.Equal(0, charsConsumed);
            Assert.False(DateTimeParser.TryParseTimeOnly(byteSpan, out var utf8ToValue, out var bytesConsumed));
            Assert.Equal(0, bytesConsumed);
            TimeOnly defaultToValue = default;
            Assert.Equal(defaultToValue, toValue);
            Assert.Equal(defaultToValue, utf8ToValue);
        }

        [Theory]
        [InlineData("00:00", 5)]
        [InlineData("13:00", 5, 13)]
        [InlineData("23:59", 5, 23, 59)]
        [InlineData("23:59:00", 8, 23, 59)]
        [InlineData("23:59:59", 8, 23, 59, 59)]
        [InlineData("23:59:59.9", 10, 23, 59, 59, 9000000)]
        [InlineData("23:59:59.98", 11, 23, 59, 59, 9800000)]
        [InlineData("23:59:59.987", 12, 23, 59, 59, 9870000)]
        [InlineData("23:59:59.9876", 13, 23, 59, 59, 9876000)]
        [InlineData("23:59:59.98765", 14, 23, 59, 59, 9876500)]
        [InlineData("23:59:59.987654", 15, 23, 59, 59, 9876540)]
        [InlineData("23:59:59.9876543", 16, 23, 59, 59, 9876543)]
        public void ParseValidTime(string input, int length, int hour = 0, int minute = 0, int second= 0, int fraction = 0)
        {
            var charSpan = input.AsSpan();
            var byteSpan = Encoding.UTF8.GetBytes(input);

            Assert.True(DateTimeParser.TryParseTimeOnly(charSpan, out var toValue, out var charsConsumed));
            Assert.Equal(length, charsConsumed);
            Assert.True(DateTimeParser.TryParseTimeOnly(byteSpan, out var utf8ToValue, out var bytesConsumed));
            Assert.Equal(length, bytesConsumed);

            AssertTimeOnly(toValue, hour, minute, second, fraction);
            AssertTimeOnly(utf8ToValue, hour, minute, second, fraction);

            Assert.Equal(toValue, utf8ToValue);
        }


        /// <summary>
        ///     To make sure the fractions are properly parsed
        /// </summary>
        [Theory]
        [InlineData("2017-06-12T05:30:45.1000000Z")]
        [InlineData("2017-06-12T05:30:45.0100000Z")]
        [InlineData("2017-06-12T05:30:45.0010000Z")]
        [InlineData("2017-06-12T05:30:45.0001000Z")]
        [InlineData("2017-06-12T05:30:45.0000100Z")]
        [InlineData("2017-06-12T05:30:45.0000010Z")]
        [InlineData("2017-06-12T05:30:45.0000001Z")]
        public void AgainstBcl(string input)
        {
            Assert.True(DateTimeParser.TryParseDateTimeOffset(input.AsSpan(), out var dtoValue, out var dtoConsumed));
            var dto = DateTimeOffset.ParseExact(input.AsSpan(), "O", CultureInfo.InvariantCulture);
            Assert.Equal(dto, dtoValue);
        }

        public static IEnumerable<object[]> GenerateValuesForDigitChecking()
        {
            var startDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

            for (int i = 1; i < 13; i++)
            {
                yield return new object[] { new DateTime(startDate + TimeSpan.FromDays(31).Ticks * i) };
            }

            for (int i = 1; i < 31; i++)
            {
                yield return new object[] { new DateTime(startDate + TimeSpan.FromDays(i).Ticks) };
            }

            for (int i = 0; i < 24; i++)
            {
                yield return new object[] { new DateTime(startDate + TimeSpan.FromHours(i).Ticks) };
            }

            for (int i = 0; i < 60; i++)
            {
                yield return new object[] { new DateTime(startDate + TimeSpan.FromMinutes(i).Ticks) };
            }

            for (int i = 0; i < 60; i++)
            {
                yield return new object[] { new DateTime(startDate + TimeSpan.FromSeconds(i).Ticks) };
            }
        }

        [Theory]
        [MemberData(nameof(GenerateValuesForDigitChecking))]
        public void DigitChecking(DateTime dt)
        {
            Span<char> outputChars = stackalloc char[50];
            Assert.True(DateTimeFormatter.TryFormat(dt, outputChars, out var written));
            Assert.True(DateTimeParser.TryParseDateTime(outputChars, out var outputDt, out var consumed));
            Assert.Equal(dt, outputDt);
            Assert.Equal(written, consumed);

            Span<byte> outputBytes = stackalloc byte[50];
            Assert.True(DateTimeFormatter.TryFormat(dt, outputBytes, out written));
            Assert.True(DateTimeParser.TryParseDateTime(outputBytes, out outputDt, out consumed));
            Assert.Equal(dt, outputDt);
            Assert.Equal(written, consumed);

            var dto = new DateTimeOffset(dt);

            Assert.True(DateTimeFormatter.TryFormat(dto, outputChars, out written));
            Assert.True(DateTimeParser.TryParseDateTime(outputChars, out var outputdto, out consumed));
            Assert.Equal(dto, outputdto);
            Assert.Equal(written, consumed);

            Assert.True(DateTimeFormatter.TryFormat(dto, outputBytes, out written));
            Assert.True(DateTimeParser.TryParseDateTime(outputBytes, out outputdto, out consumed));
            Assert.Equal(dto, outputdto);
            Assert.Equal(written, consumed);

            var dateOnly = new DateOnly(dto.Year, dto.Month, dto.Day);
            Assert.True(DateTimeFormatter.TryFormat(dateOnly, outputChars, out written));
            Assert.True(DateTimeParser.TryParseDateOnly(outputChars.Slice(0, written), out var outputDateOnly, out consumed));
            Assert.Equal(dateOnly, outputDateOnly);
            Assert.Equal(written, consumed);

            var timeOnly = new TimeOnly(dto.Hour, dto.Minute, dto.Second, dto.Millisecond);
            Assert.True(DateTimeFormatter.TryFormat(timeOnly, outputChars, out written));
            Assert.True(DateTimeParser.TryParseTimeOnly(outputChars.Slice(0, written), out var outputTimeOnly, out consumed));
            Assert.Equal(timeOnly, outputTimeOnly);
            Assert.Equal(written, consumed);
        }


        private void AssertDateTime(DateTime dateTime, int year, int month, int day, int hour, int minute,
            int second, int fraction)
        {
            var comparison = new DateTime(year, month, day, hour, minute, second).AddTicks(fraction);
            Assert.Equal(comparison, dateTime);
        }

        private void AssertTimeOnly(TimeOnly timeOnly, int hour, int minute, int second, int fraction)
        {
            Assert.Equal(timeOnly.Hour, hour);
            Assert.Equal(timeOnly.Minute, minute);
            Assert.Equal(timeOnly.Second, second);
            var comparison = new TimeOnly(hour, minute, second).Add(TimeSpan.FromTicks(fraction));
            Assert.Equal(comparison, timeOnly);
        }

        [Fact]
        public void SerializeDeserializeDateTime()
        {
            var fixture = new ExpressionTreeFixture();
            var dt = fixture.Create<DateTime>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dt);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DateTime>(serialized);
            Assert.Equal(dt, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDateTimeOffset()
        {
            var fixture = new ExpressionTreeFixture();
            var dt = fixture.Create<DateTimeOffset>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dt);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DateTimeOffset>(serialized);
            Assert.Equal(dt, deserialized);
        }

        [Theory]
        [InlineData("2017-06-12T05:30:45.7680000", 2017, 6, 12, 5, 30, 45, 7680000,
            DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7680000Z", 2017, 6, 12, 5, 30, 45, 7680000, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45", 2017, 6, 12, 5, 30, 45, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45Z", 2017, 6, 12, 5, 30, 45, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0010000", 2017, 6, 12, 5, 30, 45, 10000, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0010000Z", 2017, 6, 12, 5, 30, 45, 10000, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.7607389", 2017, 6, 12, 5, 30, 45, 7607389, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7607389Z", 2017, 6, 12, 5, 30, 45, 7607389, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0000001Z", 2017, 6, 12, 5, 30, 45, 0000001, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0000001", 2017, 6, 12, 5, 30, 45, 0000001, DateTimeKind.Unspecified)]
        public void FormatDateTime(string comparison, int year, int month, int day, int hour, int minute,
            int second, int fraction, DateTimeKind kind)
        {
            var value = new DateTime(year, month, day, hour, minute, second, kind).AddTicks(fraction);
            Span<char> charSpan = stackalloc char[35];
            Assert.True(DateTimeFormatter.TryFormat(value, charSpan, out var symbolsWritten));
            Assert.Equal(comparison, charSpan.Slice(0, symbolsWritten).ToString());


            Span<byte> byteSpan = stackalloc byte[35];
            Assert.True(DateTimeFormatter.TryFormat(value, byteSpan, out symbolsWritten));
            Assert.Equal(comparison, Encoding.UTF8.GetString(byteSpan.Slice(0, symbolsWritten)));
        }

        [Theory]
        [InlineData("2017-06-12T05:30:45.7680000-07:30", 2017, 6, 12, 5, 30, 45, 7680000, true, 7, 30,
            DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7680000Z", 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.7680000Z", 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0,
            DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7680000+08:00", 2017, 6, 12, 5, 30, 45, 7680000, false, 8, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45+01:00", 2017, 6, 12, 5, 30, 45, 0, false, 1, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45Z", 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45Z", 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0010000+01:00", 2017, 6, 12, 5, 30, 45, 10000, false, 1, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0010000Z", 2017, 6, 12, 5, 30, 45, 10000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0010000Z", 2017, 6, 12, 5, 30, 45, 10000, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7607389+01:00", 2017, 6, 12, 5, 30, 45, 7607389, false, 1, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.7607389Z", 2017, 6, 12, 5, 30, 45, 7607389, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.7607389Z", 2017, 6, 12, 5, 30, 45, 7607389, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0000001Z", 2017, 6, 12, 5, 30, 45, 0000001, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0000001Z", 2017, 6, 12, 5, 30, 45, 0000001, false, 0, 0, DateTimeKind.Unspecified)]
        public void FormatDateTimeOffset(string comparison, int year, int month, int day, int hour, int minute,
            int second, int fraction, bool negative, int offsethours, int offsetminutes, DateTimeKind kind)
        {
            var offset = new TimeSpan(0, offsethours, offsetminutes, 0) * (negative ? -1 : 1);
            var dt = new DateTime(year, month, day, hour, minute, second, kind).AddTicks(fraction);
            var value = new DateTimeOffset(dt, offset);

            Span<char> charSpan = stackalloc char[35];
            Assert.True(DateTimeFormatter.TryFormat(value, charSpan, out var symbolsWritten));
            Assert.Equal(comparison, charSpan.Slice(0, symbolsWritten).ToString());

            Span<byte> byteSpan = stackalloc byte[35];
            Assert.True(DateTimeFormatter.TryFormat(value, byteSpan, out symbolsWritten));
            Assert.Equal(comparison, Encoding.UTF8.GetString(byteSpan.Slice(0, symbolsWritten)));
        }

        [Fact]
        public void LocalTime()
        {
            var value = DateTime.Now;
            value = value.AddTicks(-(value.Ticks % TimeSpan.TicksPerSecond));
            var offset = TimeZoneInfo.Local.GetUtcOffset(value);
            var output = value.ToString("yyyy-MM-ddTHH:mm:ss");
            var sign = offset.Hours < 0 ? "" : "+";
            output = output + sign + $"{offset.Hours:D2}:{offset.Minutes:D2}";
            Span<char> charSpan = stackalloc char[35];
            Assert.True(DateTimeFormatter.TryFormat(value, charSpan, out var symbolsWritten));
            Assert.Equal(output, charSpan.Slice(0, symbolsWritten).ToString());

            Span<byte> byteSpan = stackalloc byte[35];
            Assert.True(DateTimeFormatter.TryFormat(value, byteSpan, out symbolsWritten));
            Assert.Equal(output, Encoding.UTF8.GetString(byteSpan.Slice(0, symbolsWritten)));
        }
    }
}