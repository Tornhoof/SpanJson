using System.Globalization;
using Xunit;

namespace SpanJson.Tests
{
    public class BooleanTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void Boolean(bool value)
        {
            var text = value.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
            var boolean = JsonSerializer.Generic.Deserialize<bool>(text);
            Assert.Equal(value, boolean);
        }
    }
}