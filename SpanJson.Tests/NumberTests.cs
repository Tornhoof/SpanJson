using System.Globalization;
using Xunit;

namespace SpanJson.Tests
{
    public class NumberTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(1234)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(1234567)]
        [InlineData(12345678)]
        [InlineData(123456789)]
        [InlineData(1234567890)]
        [InlineData(int.MaxValue)]
        [InlineData(-1)]
        [InlineData(-12)]
        [InlineData(-123)]
        [InlineData(-1234)]
        [InlineData(-12345)]
        [InlineData(-123456)]
        [InlineData(-1234567)]
        [InlineData(-12345678)]
        [InlineData(-123456789)]
        [InlineData(-1234567890)]
        [InlineData(int.MinValue)]
        public void Int32(int value)
        {
            var text = value.ToString(CultureInfo.InvariantCulture);
            var number = JsonSerializer.Generic.Utf16.Deserialize<int>(text);
            Assert.Equal(value, number);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(12)]
        [InlineData(123)]
        [InlineData(1234)]
        [InlineData(12345)]
        [InlineData(123456)]
        [InlineData(1234567)]
        [InlineData(12345678)]
        [InlineData(123456789)]
        [InlineData(1234567890)]
        [InlineData(uint.MaxValue)]
        public void Uint332(uint value)
        {
            var text = value.ToString(CultureInfo.InvariantCulture);
            var number = JsonSerializer.Generic.Utf16.Deserialize<uint>(text);
            Assert.Equal(value, number);
        }
    }
}