using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests.Generated
{
    // Can't use InlineData, won't compile
    public partial class DecimalTests
    {
        [Theory]
        [MemberData(nameof(GetDecimalTestData))]
        public void SerializeDeserializeMinMaxUtf8(decimal input)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<decimal>(serialized);
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [MemberData(nameof(GetDecimalTestData))]
        public void SerializeDeserializeMinMaxUtf16(decimal input)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<decimal>(serialized);
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [InlineData("-0.1", -0.1d)]
        [InlineData("0.1", 0.1d)]
        [InlineData("-1.23", -1.23)]
        [InlineData("1.23", 1.23)]
        [InlineData("-1.23e+6", -1230000)]
        [InlineData("1.23e+6", 1230000)]
        [InlineData("-1.23e-6", -0.00000123)]
        [InlineData("1.23e-6", 0.00000123)]
        public void SerializeDeserializeExpontentialUtf16(string input, decimal expected)
        {
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<decimal>(input);
            Assert.Equal(expected, deserialized);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(deserialized);
            var deserialized2 = JsonSerializer.Generic.Utf16.Deserialize<decimal>(serialized);
            Assert.Equal(expected, deserialized2);
        }


        [Theory]
        [InlineData("-0.1", -0.1d)]
        [InlineData("0.1", 0.1d)]
        [InlineData("-1.23", -1.23)]
        [InlineData("1.23", 1.23)]
        [InlineData("-1.23e+6", -1230000)]
        [InlineData("1.23e+6", 1230000)]
        [InlineData("-1.23e-6", -0.00000123)]
        [InlineData("1.23e-6", 0.00000123)]
        public void SerializeDeserializeExpontentialUtf8(string input, decimal expected)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<decimal>(bytes);
            Assert.Equal(expected, deserialized);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(deserialized);
            var deserialized2 = JsonSerializer.Generic.Utf8.Deserialize<decimal>(serialized);
            Assert.Equal(expected, deserialized2);
        }

        public static IEnumerable<object[]> GetDecimalTestData()
        {
            yield return new object[] {decimal.MinValue};
            yield return new object[] {decimal.MaxValue};
        }
    }
}