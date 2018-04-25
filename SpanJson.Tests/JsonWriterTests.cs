using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class JsonWriterTests
    {
        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        public void WriteEscapedUtf16(string input, string output)
        {
            var writer = new JsonWriter<char>(100);
            writer.WriteUtf16String(input);
            var serialized = writer.ToString();
            Assert.Equal(output, serialized);
        }

        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        public void WriteEscapedUtf8(string input, string output)
        {
            var writer = new JsonWriter<byte>(100);
            writer.WriteUtf8String(input);
            var serialized = writer.ToByteArray();
            var outputBytes = Encoding.UTF8.GetBytes(output);
            Assert.Equal(outputBytes, serialized);
        }
    }
}
