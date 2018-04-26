using Xunit;

namespace SpanJson.Tests
{
    public class AnonymousTypeTests
    {
        [Fact]
        public void Test()
        {
            var x = new {Name = "Alice", Age = 25};
            var output = JsonSerializer.Generic.Utf16.Serialize(x);
            Assert.Equal("{\"Name\":\"Alice\",\"Age\":25}", output);
        }
    }
}