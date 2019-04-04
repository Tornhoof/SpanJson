using System;
using System.Globalization;
using Xunit;

namespace SpanJson.Tests.Generated
{
    // Easiest way to compare is with ToString()
    //https://www.hanselman.com/blog/WhyYouCantDoubleParseDoubleMaxValueToStringOrSystemOverloadExceptionsWhenUsingDoubleParse.aspx
    public partial class DoubleTests
    {
        [Theory]
        [InlineData("-1.79769313486231E+308")]
        [InlineData("1.79769313486231E+308")]
        public void SerializeDeserializeMinMaxUtf8(string input)
        {
            var doubleValue = double.Parse(input, CultureInfo.InvariantCulture);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Double>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }


        [Theory]
        [InlineData("-1.79769313486231E+308")]
        [InlineData("1.79769313486231E+308")]
        public void SerializeDeserializeMinMaxUtf16(string input)
        {
            var doubleValue = double.Parse(input, CultureInfo.InvariantCulture);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Double>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }

        [Fact]
        public void SerializeDeserializeZeroUtf8()
        {
            var doubleValue = 0.0d;
            var serialized = JsonSerializer.Generic.Utf8.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Double>(serialized);
            Assert.Equal(doubleValue, deserialized);
        }

        [Fact]
        public void SerializeDeserializeZeroUtf16()
        {
            var doubleValue = 0.0d;
            var serialized = JsonSerializer.Generic.Utf8.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Double>(serialized);
            Assert.Equal(doubleValue, deserialized);
        }


        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void SerializeDeserializeInvalidUtf8(double input)
        {
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf8.Serialize(input));
        }


        [Theory]
        [InlineData(double.NaN)]
        [InlineData(double.NegativeInfinity)]
        [InlineData(double.PositiveInfinity)]
        public void SerializeDeserializeInvalidUtf16(double input)
        {
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf16.Serialize(input));
        }
    }
}