using System.Linq;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class StringTests
    {
        [Fact]
        public void SerializeDeserializeAllAsciiUtf8()
        {
            var chars = string.Join(", ", Enumerable.Range(0, 0x80).Select(a => (char) a));
            var serialized = JsonSerializer.Generic.Utf8.Serialize(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<string>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }

        [Fact]
        public void SerializeDeserializeAllAsciiUtf16()
        {
            var chars = string.Join(", ", Enumerable.Range(0, 0x80).Select(a => (char) a));
            var serialized = JsonSerializer.Generic.Utf16.Serialize(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<string>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }

    }
}