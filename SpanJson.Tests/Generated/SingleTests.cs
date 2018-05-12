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
    }
}
