using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class AnonymousTypeTests
    {
        [Fact]
        public void TestUtf16()
        {
            var x = new {Name = "Alice", Age = 25};
            var output = JsonSerializer.Generic.Utf16.Serialize(x);
            Assert.Equal("{\"Name\":\"Alice\",\"Age\":25}", output);
        }

        [Fact]
        public void TestUtf8()
        {
            var x = new {Name = "Alice", Age = 25};
            var outputBytes = JsonSerializer.Generic.Utf8.Serialize(x);
            Assert.Equal("{\"Name\":\"Alice\",\"Age\":25}", Encoding.UTF8.GetString(outputBytes));
        }
    }
}