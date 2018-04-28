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
            var writer = new JsonWriter<char>(16);
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
            var writer = new JsonWriter<byte>(16);
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

        /// <summary>
        /// This hits the resizing, otherwise the ascii case would hit array bounds
        /// </summary>
        [Theory]
        [InlineData("칱칳칶칹칼캠츧.txt","{\"Name\":\"칱칳칶칹칼캠츧.txt\"}")]
        [InlineData("Hello\" World.txt", "{\"Name\":\"Hello\\\" World.txt\"}")]
        [InlineData("Hello\"\"World.txt", "{\"Name\":\"Hello\\\"\\\"World.txt\"}")]
        public void WriteMultiCharStringUtf8Resizing(string input, string comparison)
        {
            var writer = new JsonWriter<byte>(32);
            writer.WriteUtf8BeginObject();
            writer.WriteUtf8Name("Name");
            writer.WriteUtf8String(input);
            writer.WriteUtf8EndObject();
            var output = writer.ToByteArray();
            Assert.Equal(comparison, Encoding.UTF8.GetString(output));
        }

        /// <summary>
        /// This hits the resizing, otherwise the ascii case would hit array bounds
        /// </summary>
        [Theory]
        [InlineData("칱칳칶칹칼캠츧.txt", "{\"Name\":\"칱칳칶칹칼캠츧.txt\"}")]
        [InlineData("Hello\" World.txt", "{\"Name\":\"Hello\\\" World.txt\"}")]
        [InlineData("Hello\"\"World.txt", "{\"Name\":\"Hello\\\"\\\"World.txt\"}")]
        public void WriteMultiCharStringUtf16(string input, string comparison)
        {
            var writer = new JsonWriter<char>(32);
            writer.WriteUtf16BeginObject();
            writer.WriteUtf16Name("Name");
            writer.WriteUtf16String(input);
            writer.WriteUtf16EndObject();
            var output = writer.ToString();
            Assert.Equal(comparison, output);
        }
    }
}