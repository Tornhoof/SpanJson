using System;
using System.Buffers;
using SpanJson.Shared;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;
using Xunit;

namespace SpanJson.Tests
{
    public class ArrayPoolSerializerTests
    {
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();

        [Fact]
        public void SerializeToArrayPoolUtf16()
        {
            var model = Fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Utf16.SerializeToArrayPool(model);
            Assert.IsType<ArraySegment<char>>(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Answer>(serialized.AsSpan(serialized.Offset, serialized.Count));
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            ArrayPool<char>.Shared.Return(serialized.Array);
        }

        [Fact]
        public void SerializeToArrayPoolUtf8()
        {
            var model = Fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Utf8.SerializeToArrayPool(model);
            Assert.IsType<ArraySegment<byte>>(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Answer>(serialized.AsSpan(serialized.Offset, serialized.Count));
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            ArrayPool<byte>.Shared.Return(serialized.Array);
        }

        [Fact]
        public void SerializeToArrayPoolNonGenericUtf16()
        {
            var model = Fixture.Create<Answer>();
            var serialized = JsonSerializer.NonGeneric.Utf16.SerializeToArrayPool(model);
            Assert.IsType<ArraySegment<char>>(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Answer>(serialized.AsSpan(serialized.Offset, serialized.Count));
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            ArrayPool<char>.Shared.Return(serialized.Array);
        }

        [Fact]
        public void SerializeToArrayPoolNonGenericUtf8()
        {
            var model = Fixture.Create<Answer>();
            var serialized = JsonSerializer.NonGeneric.Utf8.SerializeToArrayPool(model);
            Assert.IsType<ArraySegment<byte>>(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Answer>(serialized.AsSpan(serialized.Offset, serialized.Count));
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            ArrayPool<byte>.Shared.Return(serialized.Array);
        }
    }
}