using Xunit;

namespace SpanJson.Tests
{
    public class JsonWriterTests
    {
        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        public void WriteEscaped(string input, string output)
        {
            var writer = new JsonWriter<char>(100);
            writer.WriteUtf16String(input);
            var serialized = writer.ToString();
            Assert.Equal(output, serialized);
        }
    }
}
