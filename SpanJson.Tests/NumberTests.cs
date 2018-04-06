using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class NumberTests
    {
        [Theory]
        [InlineData(1234567890)]
        public void Integer(int value)
        {
            var text = value.ToString(CultureInfo.InvariantCulture);
            var number = JsonSerializer.Generic.Deserialize<int>(text);
            Assert.Equal(value, number);
        }
    }

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
