using System;
using System.Globalization;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Helpers;
using Xunit;

namespace SpanJson.Tests
{
    public class DateTests
    {
        [Theory]
        [InlineData("2017-06-12T05:30:45.7680000-07:30", 33, 2017, 6, 12, 5, 30, 45, 7680000, true, 7, 30,
            DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.7680000Z", 28, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.7680000", 27, 2017, 6, 12, 5, 30, 45, 7680000, false, 0, 0,
            DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.768+08:00", 29, 2017, 6, 12, 5, 30, 45, 768, false, 8, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.768Z", 24, 2017, 6, 12, 5, 30, 45, 768, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.768", 23, 2017, 6, 12, 5, 30, 45, 768, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45+01:00", 25, 2017, 6, 12, 5, 30, 45, 0, false, 1, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45Z", 20, 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45", 19, 2017, 6, 12, 5, 30, 45, 0, false, 0, 0, DateTimeKind.Unspecified)]
        [InlineData("2017-06-12T05:30:45.0010+01:00", 30, 2017, 6, 12, 5, 30, 45, 10, false, 1, 0, DateTimeKind.Local)]
        [InlineData("2017-06-12T05:30:45.0010Z", 25, 2017, 6, 12, 5, 30, 45, 10, false, 0, 0, DateTimeKind.Utc)]
        [InlineData("2017-06-12T05:30:45.0010", 24, 2017, 6, 12, 5, 30, 45, 10, false, 0, 0, DateTimeKind.Unspecified)]
        public void Parse(string input, int length, int year, int month, int day, int hour, int minute,
            int second, int fraction, bool negative, int offsethours, int offsetminutes, DateTimeKind kind)
        {
            Assert.True(DateTimeParser.TryParseDateTimeOffset(input.AsSpan(), out var dtoValue, out var dtoConsumed));
            Assert.Equal(length, input.Length);
            Assert.Equal(length, dtoConsumed);
            Assert.True(DateTimeParser.TryParseDateTime(input.AsSpan(), out var dtValue, out var dtConsumed));
            Assert.Equal(length, dtConsumed);
            AssertDateTime(dtoValue.DateTime, year, month, day, hour, minute, second, fraction);
            var offset = new TimeSpan(negative ? -offsethours : offsethours, offsetminutes, 0);
            switch (kind)
            {
                case DateTimeKind.Local:
                    Assert.Equal(offset, dtoValue.DateTime - dtoValue.UtcDateTime);
                    Assert.Equal(dtValue, dtoValue.LocalDateTime);
                    break;
                case DateTimeKind.Unspecified:
                    Assert.Equal(offset, dtoValue.DateTime - dtoValue.LocalDateTime);
                    Assert.Equal(dtValue, dtoValue.DateTime);
                    break;
                case DateTimeKind.Utc:
                    Assert.Equal(offset, dtoValue.DateTime - dtoValue.UtcDateTime);
                    Assert.Equal(dtValue, dtoValue.UtcDateTime);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(kind), kind, null);
            }
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


        private void AssertDateTime(in DateTime dateTime, int year, int month, int day, int hour, int minute,
            int second, int fraction)
        {
            var comparison = new DateTime(year, month, day, hour, minute, second).AddTicks(fraction);
            Assert.Equal(comparison, dateTime);
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
    }
}