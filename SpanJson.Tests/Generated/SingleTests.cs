using System;
using System.Globalization;
using Xunit;

namespace SpanJson.Tests.Generated
{
    // Easiest way to compare is with ToString()
    public partial class SingleTests
    {
        [Theory]
        [InlineData(Single.MinValue)]
        [InlineData(Single.MaxValue)]
        public void SerializeDeserializeMinMaxUtf8(Single input)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Single>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }


        [Theory]
        [InlineData(Single.MinValue)]
        [InlineData(Single.MaxValue)]
        public void SerializeDeserializeMinMaxUtf16(Single input)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Single>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }


        [Fact]
        public void SerializeDeserializeZeroUtf8()
        {
            var singleValue = 0.0f;
            var serialized = JsonSerializer.Generic.Utf8.Serialize(singleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Single>(serialized);
            Assert.Equal(singleValue, deserialized);
        }

        [Fact]
        public void SerializeDeserializeZeroUtf16()
        {
            var singleValue = 0.0f;
            var serialized = JsonSerializer.Generic.Utf8.Serialize(singleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Single>(serialized);
            Assert.Equal(singleValue, deserialized);
        }

        [Theory]
        [InlineData(float.NaN)]
        [InlineData(float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity)]
        public void SerializeDeserializeInvalidUtf8(float input)
        {
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf8.Serialize(input));
        }


        [Theory]
        [InlineData(float.NaN)]
        [InlineData(float.NegativeInfinity)]
        [InlineData(float.PositiveInfinity)]
        public void SerializeDeserializeInvalidUtf16(float input)
        {
            Assert.Throws<ArgumentException>(() => JsonSerializer.Generic.Utf16.Serialize(input));
        }
    }
}