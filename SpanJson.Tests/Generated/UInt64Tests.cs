using System;
using System.Text;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public partial class UInt64Tests
    {

        [Fact]
        public void SerializeDeserializeOverflowUtf8()
        {
            // ulong.MaxValue+1
            Assert.Throws<OverflowException>(() => JsonSerializer.Generic.Utf8.Deserialize<UInt64>(Encoding.UTF8.GetBytes("18446744073709551616")));
        }

        [Fact]
        public void SerializeDeserializeOverflowUtf16()
        {
            // ulong.MaxValue+1
            Assert.Throws<OverflowException>(() => JsonSerializer.Generic.Utf16.Deserialize<UInt64>("18446744073709551616"));
        }
    }
}
