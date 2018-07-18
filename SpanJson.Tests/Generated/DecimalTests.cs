using System.Collections.Generic;
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

        public static IEnumerable<object[]> GetDecimalTestData()
        {
            yield return new object[] {decimal.MinValue};
            yield return new object[] {decimal.MaxValue};
        }
    }
}