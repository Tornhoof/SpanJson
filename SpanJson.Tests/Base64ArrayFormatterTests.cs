using System;
using System.Linq;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class Base64ArrayFormatterTests
    {
        private readonly Random _random = new Random(666);

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(198)]
        [InlineData(199)]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(1022)]
        [InlineData(1023)]
        [InlineData(1024)]
        [InlineData(1025)]

        public void SerializeDeserializeUtf16(int length)
        {
            var bytes = new byte[length];
            _random.NextBytes(bytes);
            var test = new TestDTO { Name = "Hello World", Value = bytes, ValueString = "Hello Universe" };
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestDTO, ExcludeNullCamelCaseBase64ArrayResolver<char>>(test);
            if (length % 3 != 0)
            {
                Assert.Contains("=", serialized);
            }

            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestDTO, ExcludeNullCamelCaseBase64ArrayResolver<char>>(serialized);
            Assert.Equal(test, deserialized);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(16)]
        [InlineData(17)]
        [InlineData(198)]
        [InlineData(199)]
        [InlineData(200)]
        [InlineData(201)]
        [InlineData(1022)]
        [InlineData(1023)]
        [InlineData(1024)]
        [InlineData(1025)]
        public void SerializeDeserializeUtf8(int length)
        {
            var bytes = new byte[length];
            _random.NextBytes(bytes);
            var test = new TestDTO { Name = "Hello World", Value = bytes, ValueString = "Hello Universe" };
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestDTO, ExcludeNullCamelCaseBase64ArrayResolver<byte>>(test);
            if (length % 3 != 0)
            {
                Assert.Contains((byte)'=', serialized);
            }

            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestDTO, ExcludeNullCamelCaseBase64ArrayResolver<byte>>(serialized);
            Assert.Equal(test, deserialized);
        }

        public sealed class ExcludeNullCamelCaseBase64ArrayResolver<TSymbol> : ResolverBase<TSymbol, ExcludeNullCamelCaseBase64ArrayResolver<TSymbol>>
            where TSymbol : struct
        {
            public ExcludeNullCamelCaseBase64ArrayResolver() : base(new SpanJsonOptions
            {
                NullOption = NullOptions.ExcludeNulls,
                NamingConvention = NamingConventions.CamelCase,
                ByteArrayOption = ByteArrayOptions.Base64
            })
            {
            }
        }

        [Theory]
        [InlineData(true, 10, @"{""id"":42,""bytes"":""AAECAwQFBgcICQ==""}")]
        [InlineData(true, 0, @"{""id"":42,""bytes"":""""}")]
        [InlineData(true, null, @"{""id"":42}")]
        [InlineData(false, 10, @"{""id"":42,""bytes"":""AAECAwQFBgcICQ==""}")]
        [InlineData(false, 0, @"{""id"":42,""bytes"":""""}")]
        [InlineData(false, null, @"{""id"":42}")]
        public void Serialize_ByteArray_ShouldUseBase64(bool utf8, int? arraySize, string expected)
        {
            // Arrange
            var bytes = arraySize.HasValue ? Enumerable.Range(0, arraySize.Value).Select(b => (byte)b).ToArray() : null;
            var data = new SimpleData { Id = 42, Bytes = bytes };

            // Act
            var result = utf8
                ? Encoding.UTF8.GetString(JsonSerializer.Generic.Utf8.Serialize<SimpleData, ExcludeNullCamelCaseBase64ArrayResolver<byte>>(data))
                : JsonSerializer.Generic.Utf16.Serialize<SimpleData, ExcludeNullCamelCaseBase64ArrayResolver<char>>(data);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(true, 10, @"{""id"":42,""bytes"":""AAECAwQFBgcICQ==""}")]
        [InlineData(true, 0, @"{""id"":42,""bytes"":""""}")]
        [InlineData(true, null, @"{""id"":42}")]
        [InlineData(false, 10, @"{""id"":42,""bytes"":""AAECAwQFBgcICQ==""}")]
        [InlineData(false, 0, @"{""id"":42,""bytes"":""""}")]
        [InlineData(false, null, @"{""id"":42}")]
        public void Deserialize_ByteArray_ShouldUseBase64(bool utf8, int? arraySize, string value)
        {
            // Arrange
            var bytes = arraySize.HasValue ? Enumerable.Range(0, arraySize.Value).Select(b => (byte)b).ToArray() : null;
            var expected = new SimpleData { Id = 42, Bytes = bytes };

            // Act
            var result = utf8
                ? JsonSerializer.Generic.Utf8.Deserialize<SimpleData, ExcludeNullCamelCaseBase64ArrayResolver<byte>>(Encoding.UTF8.GetBytes(value))
                : JsonSerializer.Generic.Utf16.Deserialize<SimpleData, ExcludeNullCamelCaseBase64ArrayResolver<char>>(value);

            // Assert
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Bytes, result.Bytes);
        }


        public class SimpleData
        {
            public int Id { get; set; }
            public byte[] Bytes { get; set; }
        }

        public class TestDTO : IEquatable<TestDTO>
        {
            public string Name { get; set; }

            public byte[] Value { get; set; }

            public string ValueString { get; set; }

            public bool Equals(TestDTO other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Name == other.Name && Value.AsSpan().SequenceEqual(other.Value) && ValueString == other.ValueString;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestDTO)obj);
            }

            public override int GetHashCode()
            {
                // ReSharper disable NonReadonlyMemberInGetHashCode
                return HashCode.Combine(Name, Value, ValueString);
                // ReSharper restore NonReadonlyMemberInGetHashCode
            }
        }
    }
}