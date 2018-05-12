using System;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class SurrogateTests
    {
        [Theory]
        [InlineData(0x1F4A9)]
        [InlineData(0x1F3BC)]
        public void SurrogatePairsUtf8FromInt(int input)
        {
            string surrogate = Char.ConvertFromUtf32(input);
            var utf8Bytes = Encoding.UTF8.GetBytes(surrogate);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(surrogate);
            Assert.True(serialized.AsSpan().Slice(1, utf8Bytes.Length).SequenceEqual(utf8Bytes));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<string>(serialized);
            var utf32 = Char.ConvertToUtf32(deserialized, 0);
            Assert.Equal(input, utf32);
        }

        [Theory]
        [InlineData("💩")]
        [InlineData("🎼")]
        public void SurrogatePairsUtf8FromString(string input)
        {
            var utf8Bytes = Encoding.UTF8.GetBytes(input);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.True(serialized.AsSpan().Slice(1, utf8Bytes.Length).SequenceEqual(utf8Bytes));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<string>(serialized);
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [InlineData("💩")]
        [InlineData("🎼")]
        public void SurrogatePairsUtf8CrossSerialize(string input)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            var utf8JsonSerialized = Utf8Json.JsonSerializer.Serialize(input);
            var utf8JsonDeserialized = Utf8Json.JsonSerializer.Deserialize<string>(serialized);
            Assert.Equal(input, utf8JsonDeserialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<string>(utf8JsonSerialized);
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [InlineData(0x1F4A9)]
        [InlineData(0x1F3BC)]
        public void SurrogatePairsUtf16(int input)
        {
            string surrogate = Char.ConvertFromUtf32(input);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(surrogate);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<string>(serialized);
            var utf32 = Char.ConvertToUtf32(deserialized, 0);
            Assert.Equal(input, utf32);
        }

        [Theory]
        [InlineData("💩")]
        [InlineData("🎼")]
        public void SurrogatePairsUtf16FromString(string input)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.Equal($"\"{input}\"", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<string>(serialized);
            Assert.Equal(input, deserialized);
        }
    }
}