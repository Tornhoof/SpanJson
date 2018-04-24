using System;
using System.Linq;
using System.Text;
using SpanJson.Benchmarks.Fixture;
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
            var serialized = JsonSerializer.Generic.SerializeToString(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<char[]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullCharUtf8()
        {
            var chars = new char[5];
            Array.Fill(chars, '\0');
            var serialized = JsonSerializer.Generic.SerializeToByteArray(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<char[]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }
    }
}
