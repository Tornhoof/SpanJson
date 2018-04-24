using System;
using System.Collections.Generic;
using Xunit;

namespace SpanJson.Tests
{
    public class JsonReaderTests
    {
        [Theory]
        [InlineData(@"{""Name"":""\\"", ""Test"":""Something""}", "Something")]
        [InlineData(@"{""Name"":"""", ""Test"":""Something""}", "Something")]
        public void ReadNextSegmentTest(string json, string expected)
        {
            var reader = new JsonReader<char>(json);
            reader.ReadUtf16BeginObjectOrThrow();
            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                if (name.ToString() == "Test")
                {
                    var value = reader.ReadUtf16String();
                    Assert.Equal(expected, value);
                }
                else if (name.ToString() == "Name")
                {
                    reader.SkipNextUtf16Segment();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }

        [Theory]
        [InlineData("\"Hello\\nWorld\"", "Hello\nWorld")]
        [InlineData("\"Hello\\nWorld\\r\\tUniverse\"", "Hello\nWorld\r\tUniverse")]
        [InlineData("\"Hello\\u000AWorld\"", "Hello\nWorld")]
        public void ReadString(string escaped, string comparison)
        {
            var reader = new JsonReader<char>(escaped);
            var unescaped = reader.ReadUtf16String();
            Assert.Equal(comparison, unescaped);
        }

        private static List<KeyValuePair<string, char>> CreateChars()
        {
            var result = new List<KeyValuePair<string, char>>();
            for (var i = 0; i <= 0xFFFF; i++)
            {
                var c = (char) i;
                switch (c)
                {
                    case '"':
                        result.Add(new KeyValuePair<string, char>("\"\\\"\"", c));
                        break;
                    case '\\':
                        result.Add(new KeyValuePair<string, char>("\"\\\\\"", c));
                        break;
                    case '\b':
                        result.Add(new KeyValuePair<string, char>("\"\\b\"", c));
                        break;
                    case '\f':
                        result.Add(new KeyValuePair<string, char>("\"\\f\"", c));
                        break;
                    case '\n':
                        result.Add(new KeyValuePair<string, char>("\"\\n\"", c));
                        break;
                    case '\r':
                        result.Add(new KeyValuePair<string, char>("\"\\r\"", c));
                        break;
                    case '\t':
                        result.Add(new KeyValuePair<string, char>("\"\\t\"", c));
                        break;
                    case char cc when cc >= 0x0 && cc <= 0x1F:
                        result.Add(new KeyValuePair<string, char>($"\"\\u{(int) c:X4}\"", c));
                        break;
                    default:
                        result.Add(new KeyValuePair<string, char>($"\"{c}\"", c));
                        break;
                }
            }

            return result;
        }

        [Fact]
        public void ReadChars()
        {
            var chars = CreateChars();
            foreach (var keyValuePair in chars)
            {
                var reader = new JsonReader<char>(keyValuePair.Key);
                var c = reader.ReadUtf16Char();
                Assert.Equal(keyValuePair.Value, c);
            }
        }
    }
}