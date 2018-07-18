using System.Globalization;
using System.Text;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class SByteTests
    {
        [Theory]
        [InlineData(-5)]
        [InlineData(-3)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        public void SingleDigit(sbyte input)
        {
            var stringData = input.ToString(CultureInfo.InvariantCulture);
            var bytes = Encoding.UTF8.GetBytes(stringData);
            var result = JsonSerializer.Generic.Utf8.Deserialize<sbyte>(bytes);
            Assert.Equal(input, result);
            result = JsonSerializer.Generic.Utf16.Deserialize<sbyte>(stringData);
            Assert.Equal(input, result);
        }
    }
}