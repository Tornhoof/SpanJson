using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class ListTests
    {
        [Fact]
        public void Serialize()
        {
            var list = new List<string> {"Hello", "World", "Universe"};
            var serialized = JsonSerializer.Generic.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
        }

        [Fact]
        public void Deserialize()
        {
            var list = new List<string> { "Hello", "World", "Universe" };
            var deserialized = JsonSerializer.Generic.Deserialize<List<string>>("[\"Hello\",\"World\",\"Universe\"]");
            Assert.Equal(list, deserialized);
        }
    }
}
