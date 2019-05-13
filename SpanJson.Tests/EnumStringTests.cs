using System;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumStringTests
    {
        public enum TestEnum
        {
            Hello,
            World,
            [EnumMember(Value = "SolarSystem")] Renamed,
            Universe,
        }

        [Flags]
        public enum FlagsEnum
        {
            Zero = 0,
            First = 1,
            Second = 2,
            Third = 4,
            Fourth = 8,
        }

        public enum DuplicateEnum
        {
            First = 1,
            Second = 2,
            Third = 1,
        }

        [Flags]
        public enum DuplicateFlagsEnum
        {
            First = 1,
            Second = 2,
            Third = 1,
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

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.World)]
        [InlineData(TestEnum.Universe)]
        [InlineData(TestEnum.Renamed)]
        public void SerializeDeserializeUtf16(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(DuplicateEnum.First)]
        [InlineData(DuplicateEnum.Second)]
        public void SerializeDeserializeDuplicateUtf8(DuplicateEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<DuplicateEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(DuplicateEnum.First)]
        [InlineData(DuplicateEnum.Second)]
        public void SerializeDeserializeDuplicateUtf16(DuplicateEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDuplicateUtf16Manual()
        {
            var value = DuplicateEnum.Third;
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDuplicateFlagsUtf16Manual()
        {
            var value = DuplicateFlagsEnum.First | DuplicateFlagsEnum.Second | DuplicateFlagsEnum.Third;
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateFlagsEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDuplicateFlagsUtf16NotFound()
        {
            string value = "\"   First, Second   , Third, Unknown  \"";
            Assert.Throws<InvalidOperationException>(() => JsonSerializer.Generic.Utf16.Deserialize<DuplicateFlagsEnum>(value));
        }

        [Fact]
        public void DeserializeFlagsEnumWithWhitespaceUtf8()
        {
            string value = "\"   First, Second   , Third,    Fourth  \"";
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<FlagsEnum>(Encoding.UTF8.GetBytes(value));
            Assert.Equal(FlagsEnum.First | FlagsEnum.Second | FlagsEnum.Third | FlagsEnum.Fourth, deserialized);
        }

        [Fact]
        public void SerializeDeserializeFlagsEnumWithWhitespaceUtf16()
        {
            string value = "\"   First, Second   , Third,    Fourth   \"";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<FlagsEnum>(value);
            Assert.Equal(FlagsEnum.First | FlagsEnum.Second | FlagsEnum.Third | FlagsEnum.Fourth, deserialized);
        }

        public class TestDO
        {
            public TestEnum? Value { get; set; }

            public FlagsEnum? Flags { get; set; }

            public int? AnotherValue { get; set; }
        }

        [Fact]
        public void SerializeDeserializeNullableEnumUtf16()
        {
            var test = new TestDO {Value = null, AnotherValue = 1, Flags = FlagsEnum.Fourth | FlagsEnum.First};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestDO, IncludeNullsOriginalCaseResolver<char>>(test);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestDO, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Value);
        }

        [Fact]
        public void SerializeDeserializeNullableEnumUtf8()
        {
            var test = new TestDO {Value = null, AnotherValue = 1, Flags = null};
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestDO, IncludeNullsOriginalCaseResolver<byte>>(test);
            Assert.Contains("null", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestDO, IncludeNullsOriginalCaseResolver<byte>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Value);
        }

        [Fact]
        public void DeserializeUnknownEnumUtf8()
        {
            var serialized = Encoding.UTF8.GetBytes("{\"Value\":\"Unused\",\"AnotherValue\":1}");
            Assert.Throws<InvalidOperationException>(() => JsonSerializer.Generic.Utf8.Deserialize<TestDO>(serialized));
        }

        [Fact]
        public void DeserializeUnknownEnumUtf16()
        {
            var serialized = "{\"Value\":\"Unused\",\"AnotherValue\":1}";
            Assert.Throws<InvalidOperationException>(() => JsonSerializer.Generic.Utf16.Deserialize<TestDO>(serialized));
        }

        [Theory]
        [InlineData(FlagsEnum.Zero, "\"Zero\"")]
        [InlineData(FlagsEnum.First, "\"First\"")]
        [InlineData(FlagsEnum.Zero | FlagsEnum.First | FlagsEnum.Second, "\"First,Second\"")]
        public void SerializeDeserializeWithDefaultValueUtf16(FlagsEnum value, string output)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.NotNull(serialized);
            Assert.Equal(output, serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<FlagsEnum>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(FlagsEnum.Zero, "\"Zero\"")]
        [InlineData(FlagsEnum.First, "\"First\"")]
        [InlineData(FlagsEnum.Zero | FlagsEnum.First | FlagsEnum.Second, "\"First,Second\"")]
        public void SerializeDeserializeWithDefaultValueUtf8(FlagsEnum value, string output)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            Assert.Equal(output, Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<FlagsEnum>(serialized);
            Assert.Equal(value, deserialized);
        }
    }
}