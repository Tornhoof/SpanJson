using System;
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
                ByteArrayOptions = ByteArrayOptions.Base64
            })
            {
            }
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