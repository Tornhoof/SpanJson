using System;
using System.Linq;
using SpanJson.Benchmarks.Fixture;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class CharArrayTests
    {
        [Fact]
        public void SerializeDeserializeNullChar()
        {
            var chars = new char[5];
            Array.Fill(chars, '\0');
            var serialized = JsonSerializer.Generic.Serialize(chars);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<char[]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(chars, deserialized);
        }
    }
}
