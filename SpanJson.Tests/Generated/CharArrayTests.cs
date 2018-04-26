using System;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class CharArrayTests
    {
        [Fact]
        public void SerializeDeserializeNullCharUtf16()
        {
            var chars = new char[5];
            Array.Fill(chars, '\0');
            var serialized = JsonSerializer.Generic.Utf16.Serialize(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<char[]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullCharUtf8()
        {
            var chars = new char[5];
            Array.Fill(chars, '\0');
            var serialized = JsonSerializer.Generic.Utf8.Serialize(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<char[]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }
    }
}