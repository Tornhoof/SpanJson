using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class AnonymousTypeTests
    {
        [Fact]
        public void Test()
        {
            var x = new {Name = "Alice", Age = 25};
            var output = JsonSerializer.Generic.Serialize(x);
            Assert.Equal("{\"Name\":\"Alice\",\"Age\":25}", output);
        }
    }
}
