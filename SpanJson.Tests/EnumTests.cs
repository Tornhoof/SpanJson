using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumTests
    {
        public enum TestEnum
        {
            Hello,
            World,
            [EnumMember(Value = "SolarSystem")] Renamed,
            Universe,
        }

        [Theory]
        [InlineData(TestEnum.Hello, "\"Hello\"")]
        [InlineData(TestEnum.World, "\"World\"")]
        [InlineData(TestEnum.Universe, "\"Universe\"")]
        [InlineData(TestEnum.Renamed, "\"SolarSystem\"")]
        public void SerializeUtf16(TestEnum value, string comparison)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.Equal(comparison, serialized);
        }

        [Theory]
        [InlineData("\"Hello\"", TestEnum.Hello)]
        [InlineData("\"World\"", TestEnum.World)]
        [InlineData("\"Universe\"", TestEnum.Universe)]
        [InlineData("\"SolarSystem\"", TestEnum.Renamed)]
        public void DeserializeUtf16(string value, TestEnum comparison)
        {
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestEnum>(value);
            Assert.Equal(deserialized, comparison);
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.World)]
        [InlineData(TestEnum.Universe)]
        [InlineData(TestEnum.Renamed)]
        public void SerializeDeserializeUtf8(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        public class TestDO
        {
            public TestEnum? Value { get; set; }
        }

        [Fact]
        public void SerializeDeserializeNullableEnumUtf16()
        {
            var test = new TestDO {Value = null};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestDO, IncludeNullsOriginalCaseResolver<char>>(test);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestDO, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Value);
        }

        [Fact]
        public void SerializeDeserializeNullableEnumUtf8()
        {
            var test = new TestDO { Value = null };
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestDO, IncludeNullsOriginalCaseResolver<byte>>(test);
            Assert.Contains("null", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestDO, IncludeNullsOriginalCaseResolver<byte>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Value);
        }
    }
}