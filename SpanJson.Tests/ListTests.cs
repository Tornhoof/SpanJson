using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SpanJson.Tests
{
    public class ListTests
    {
        [Fact]
        public void Deserialize()
        {
            var list = new List<string> {"Hello", "World", "Universe"};
            var deserialized = JsonSerializer.Generic.Deserialize<List<string>>("[\"Hello\",\"World\",\"Universe\"]");
            Assert.Equal(list, deserialized);
        }

        [Fact]
        public void Serialize()
        {
            var list = new List<string> {"Hello", "World", "Universe"};
            var serialized = JsonSerializer.Generic.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
        }

        [Fact]
        public void SerializeCollection()
        {
            var list = new LinkedList<string>(new[] {"Hello", "World", "Universe"});
            var serialized = JsonSerializer.Generic.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
        }

        [Fact]
        public void SerializeLinq()
        {
            var list = new List<string> { "Hello", "World", "Universe" };
            var serialized = JsonSerializer.Generic.Serialize(list.Where(a => a != "Universe"));
            Assert.Equal("[\"Hello\",\"World\"]", serialized);
        }
    }
}