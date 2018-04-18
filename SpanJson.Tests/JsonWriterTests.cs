using System;
using System.Collections.Generic;
using System.Text;
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
            Span<char> span = stackalloc char[100];
            var writer = new JsonWriter(span);
            writer.WriteString(input);
            var serialized = writer.ToString();
            Assert.Equal(output, serialized);
        }
    }
}
