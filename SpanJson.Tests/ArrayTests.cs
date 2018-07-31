using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class ArrayTests
    {

        [Fact]
        public void JaggedArrayUtf16()
        {
            var input = new int[5][] { new[] { 1, 2 }, new[] { 3, 4 }, new[] { 5, 6, 7 }, new[] { 7, 8 }, new[] { 9, 10 } };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<int[][]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void JaggedArrayUtf8()
        {
            var input = new int[5][] { new[] { 1, 2 }, new[] { 3, 4 }, new[] { 5, 6, 7 }, new[] { 7, 8 }, new[] { 9, 10 } };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<int[][]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void Nested2DArrayUtf16()
        {
            var input = new int[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 5, 6, 7 }, { 7, 8, 9 } };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<int[,]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void Nested2DArrayEmptyUtf16()
        {
            var input = new int[,] {{ }, { }, { }, { }};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Equal("[[],[],[],[]]", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<int[,]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }


        [Fact]
        public void Nested2DArrayUtf8()
        {
            var input = new int[,] { { 1, 2, 3 }, { 3, 4, 5 }, { 5, 6, 7 }, { 7, 8, 9 } };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<int[,]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void Nested3DArrayUtf16()
        {
            var input = new int[,,] { { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } } };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<int[,,]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }



        [Fact]
        public void Nested3DArrayUtf8()
        {
            var input = new int[,,] { { { 1, 2, 3 }, { 4, 5, 6 } },
                { { 7, 8, 9 }, { 10, 11, 12 } } };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<int[,,]>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }
    }
}
