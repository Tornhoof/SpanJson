using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class TimeSpanTests
    {
        [Theory]
        [MemberData(nameof(GetTimeSpans))]
        public void ExtraValuesUtf16(TimeSpan value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TimeSpan>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [MemberData(nameof(GetTimeSpans))]
        public void ExtraValuesUtf8(TimeSpan value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TimeSpan>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData("\"00:00:00.1000000\"", 1000000)]
        [InlineData("\"00:00:00.0100000\"", 0100000)]
        [InlineData("\"00:00:00.0010000\"", 0010000)]
        [InlineData("\"00:00:00.0001000\"", 0001000)]
        [InlineData("\"00:00:00.0000100\"", 0000100)]
        [InlineData("\"00:00:00.0000010\"", 0000010)]
        [InlineData("\"00:00:00.0000001\"", 0000001)]
        //[InlineData("\"00:00:00.00000001\"", 0000000)] // TODO readd
        public void DeserializeShort(string input, long ticks)
        {
            var deserializeUtf16 = JsonSerializer.Generic.Utf16.Deserialize<TimeSpan>(input);
            Assert.Equal(0, deserializeUtf16.Days);
            Assert.Equal(0, deserializeUtf16.Hours);
            Assert.Equal(0, deserializeUtf16.Minutes);
            Assert.Equal(0, deserializeUtf16.Seconds);
            Assert.Equal(ticks, deserializeUtf16.Ticks);
            var deserializeUtf8 = JsonSerializer.Generic.Utf8.Deserialize<TimeSpan>(Encoding.UTF8.GetBytes(input));
            Assert.Equal(deserializeUtf16, deserializeUtf8);
        }

        public static IEnumerable<object[]> GetTimeSpans()
        {
            yield return new object[] {TimeSpan.MinValue};
            yield return new object[] {TimeSpan.MaxValue};
            for (int i = -5; i < 5; i++)
            {
                yield return new object[] {new TimeSpan(i, i + 10, i + 10, i + 10)};
            }
        }
    }
}