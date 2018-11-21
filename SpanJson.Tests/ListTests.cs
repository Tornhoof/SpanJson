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
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<List<string>>("[\"Hello\",\"World\",\"Universe\"]");
            Assert.Equal(list, deserialized);
        }

        [Fact]
        public void Serialize()
        {
            var list = new List<string> {"Hello", "World", "Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
        }

        [Fact]
        public void SerializeCollection()
        {
            var list = new LinkedList<string>(new[] {"Hello", "World", "Universe"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
        }

        [Fact]
        public void SerializeDeserializeIList()
        {
            IList<string> list = new List<string> {"Hello", "World", "Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(list);
            Assert.Equal("[\"Hello\",\"World\",\"Universe\"]", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IList<string>>(serialized);
            Assert.Equal(list, deserialized);
        }

        [Fact]
        public void SerializeLinq()
        {
            var list = new List<string> {"Hello", "World", "Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(list.Where(a => a != "Universe"));
            Assert.Equal("[\"Hello\",\"World\"]", serialized);
        }

        [Fact]
        public void MultiDimArray()
        {
            var jaggedArray = new int[][] {new[] {1, 2, 3}, new[] {4, 5, 6}};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(jaggedArray);
            Assert.Equal("[[1,2,3],[4,5,6]]", serialized);
        }
    }
}