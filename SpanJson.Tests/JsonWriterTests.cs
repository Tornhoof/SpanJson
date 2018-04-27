using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class JsonWriterTests
    {
        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        [InlineData("Test' \\\"@vnni47dg", "\"Test' \\\\\\\"@vnni47dg\"")]
        public void WriteEscapedUtf16(string input, string output)
        {
            var serializedJil = Jil.JSON.Serialize(input);
            var writer = new JsonWriter<char>(100);
            writer.WriteUtf16String(input);
            var serialized = writer.ToString();
            Assert.Equal(output, serialized);
        }

        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        [InlineData("Test' \\\"@vnni47dg", "\"Test' \\\\\\\"@vnni47dg\"")]
        public void WriteEscapedUtf8(string input, string output)
        {
            var writer = new JsonWriter<byte>(100);
            writer.WriteUtf8String(input);
            var serialized = writer.ToByteArray();
            var outputBytes = Encoding.UTF8.GetBytes(output);
            Assert.Equal(outputBytes, serialized);
        }

        [Fact]
        public void WriteMultiCharStringUtf8()
        {
            var input = "칱칳칶칹칼캠츧";
            var writer = new JsonWriter<byte>(4);
            writer.WriteUtf8String(input);
            var output = writer.ToByteArray();
            Assert.Equal($"\"{input}\"", Encoding.UTF8.GetString(output));

        }

        [Fact]
        public void WriteMultiCharStringUtf16()
        {
            var input = "칱칳칶칹칼캠츧";
            var writer = new JsonWriter<char>(4);
            writer.WriteUtf16String(input);
            var output = writer.ToString();
            Assert.Equal($"\"{input}\"", output);
        }
    }
}