using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class JsonReaderTests
    {
        [Theory]
        [InlineData(@"{""Name"":""\\"", ""Test"":""Something""}", "Something")]
        [InlineData(@"{""Name"":"""", ""Test"":""Something""}", "Something")]
        public void ReadNextSegmentTestUtf16(string json, string expected)
        {
            var reader = new JsonReader<char>(json);
            reader.ReadUtf16BeginObjectOrThrow();
            var count = 0;
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16EscapedNameSpan();
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
        [InlineData(@"{""Name"":""\\"", ""Test"":""Something""}", "Something")]
        [InlineData(@"{""Name"":"""", ""Test"":""Something""}", "Something")]
        public void ReadNextSegmentTestUtf8(string json, string expected)
        {
            var reader = new JsonReader<byte>(Encoding.UTF8.GetBytes(json));
            reader.ReadUtf8BeginObjectOrThrow();
            var count = 0;
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = Encoding.UTF8.GetString(reader.ReadUtf8EscapedNameSpan());
                if (name == "Test")
                {
                    var value = reader.ReadUtf8String();
                    Assert.Equal(expected, value);
                }
                else if (name == "Name")
                {
                    reader.SkipNextUtf8Segment();
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
        [InlineData("\"Test' \\\\\\\"@vnni47dg\"", "Test' \\\"@vnni47dg")]
        public void ReadStringUtf16(string escaped, string comparison)
        {
            var reader = new JsonReader<char>(escaped);
            var unescaped = reader.ReadUtf16String();
            Assert.Equal(comparison, unescaped);
        }

        [Theory]
        [InlineData("\"Hello\\nWorld\"", "Hello\nWorld")]
        [InlineData("\"Hello\\nWorld\\r\\tUniverse\"", "Hello\nWorld\r\tUniverse")]
        [InlineData("\"Hello\\u000AWorld\"", "Hello\nWorld")]
        [InlineData("\"Test' \\\\\\\"@vnni47dg\"", "Test' \\\"@vnni47dg")]
        public void ReadStringUtf8(string escaped, string comparison)
        {
            var reader = new JsonReader<byte>(Encoding.UTF8.GetBytes(escaped));
            var unescaped = reader.ReadUtf8String();
            Assert.Equal(comparison, unescaped);
        }

        private static List<KeyValuePair<string, char>> CreateChars()
        {
            var result = new List<KeyValuePair<string, char>>();
            for (var i = 0; i <= 0xFFFF; i++)
            {
                var c = (char) i;
                if (char.IsHighSurrogate(c) || char.IsLowSurrogate(c))
                {
                    continue;
                }

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
        public void ReadCharsUtf16()
        {
            var chars = CreateChars();
            foreach (var keyValuePair in chars)
            {
                var reader = new JsonReader<char>(keyValuePair.Key);
                var c = reader.ReadUtf16Char();
                Assert.Equal(keyValuePair.Value, c);
            }
        }


        [Fact]
        public void ReadCharsUtf8()
        {
            var chars = CreateChars();
            foreach (var keyValuePair in chars)
            {
                var reader = new JsonReader<byte>(Encoding.UTF8.GetBytes(keyValuePair.Key));
                var c = reader.ReadUtf8Char();
                Assert.Equal(keyValuePair.Value, c);
            }
        }

        [Theory]
        [MemberData(nameof(CreateSurrogatePairs))]
        public void ReadSurrogatePairUtf16(int codePoint, string input)
        {
            var reader = new JsonReader<char>(input);
            var s = reader.ReadUtf16String();
            var decoded = char.ConvertToUtf32(s, 0);
            Assert.Equal(codePoint, decoded);
        }

        public static IEnumerable<object[]> CreateSurrogatePairs()
        {
            for (var i = 0x1F601; i < 0x1F64F; i++)
            {
                yield return new object[]
                    {i, "\"" + string.Concat(Char.ConvertFromUtf32(i).ToCharArray().Select(a => string.Format(@"\u{0:x4}", (int) a))) + "\""};
            }
        }

        [Theory]
        [MemberData(nameof(CreateSurrogatePairs))]
        public void ReadSurrogatePairUtf8(int codePoint, string input)
        {
            var reader = new JsonReader<byte>(Encoding.UTF8.GetBytes(input));
            var s = reader.ReadUtf8String();
            var decoded = char.ConvertToUtf32(s, 0);
            Assert.Equal(codePoint, decoded);
        }

        [Fact]
        public void SkipLargeArrayUtf16()
        {
            var array = Enumerable.Range(1, 100000).ToArray();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(array);

            var reader = new JsonReader<char>(serialized);
            reader.SkipNextUtf16Segment();
            Assert.Equal(serialized.Length, reader.Position);
        }

        [Fact]
        public void SkipLargeArrayUtf8()
        {
            var array = Enumerable.Range(1, 100000).ToArray();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(array);

            var reader = new JsonReader<byte>(serialized);
            reader.SkipNextUtf8Segment();
            Assert.Equal(serialized.Length, reader.Position);
        }

        [Fact]
        public void SkipLargeZeroArrayUtf16()
        {
            var array = Enumerable.Range(1, 100000).Select(x => new int[0]).ToArray();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(array);

            var reader = new JsonReader<char>(serialized);
            reader.SkipNextUtf16Segment();
            Assert.Equal(serialized.Length, reader.Position);
        }

        [Fact]
        public void SkipLargeZeroArrayUtf8()
        {
            var array = Enumerable.Range(1, 100000).Select(x => new int[0]).ToArray();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(array);

            var reader = new JsonReader<byte>(serialized);
            reader.SkipNextUtf8Segment();
            Assert.Equal(serialized.Length, reader.Position);
        }

        [Fact]
        public void ReadSequence()
        {

            var input1 = "\"Hello";
            var input2 = "\\nWorld\"";
            var ros = ReadOnlySequenceFactory<char>.CreateSegments(Enumerable.Repeat(' ', 10).ToArray(), input1.ToCharArray(), input2.ToCharArray());
            var bf = new BufferReader<char>(ros);
            var jsonReader = new JsonReader<char>(bf);
            jsonReader.ReadString();
        }

    }
}