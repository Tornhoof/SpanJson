using System;
using System.Globalization;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumIntegerTests
    {
        public enum TestEnum
        {
            Hello = -1,
            None = 0,
            World = 1,
        }

        public enum TestLongEnum : long
        {
            Min = long.MinValue,
            None = 0,
            Max = long.MaxValue,
        }

        public enum DuplicateEnum
        {
            First = 1,
            Second = 2,
            Third = 1,
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.None)]
        [InlineData(TestEnum.World)]
        public void SerializeDeserializeUtf16(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.Contains(((int) value).ToString(CultureInfo.InvariantCulture), serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.None)]
        [InlineData(TestEnum.World)]
        public void SerializeDeserializeUtf8(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(value);
            Assert.Contains(((int) value).ToString(CultureInfo.InvariantCulture), Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestLongEnum.Min)]
        [InlineData(TestLongEnum.None)]
        [InlineData(TestLongEnum.Max)]
        public void SerializeDeserializeLongUtf16(TestLongEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestLongEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.Contains(((long) value).ToString(CultureInfo.InvariantCulture), serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestLongEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestLongEnum.Min)]
        [InlineData(TestLongEnum.None)]
        [InlineData(TestLongEnum.Max)]
        public void SerializeDeserializeLongUtf8(TestLongEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestLongEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(value);
            Assert.Contains(((long) value).ToString(CultureInfo.InvariantCulture), Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestLongEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }



        [Theory]
        [InlineData(DuplicateEnum.First)]
        [InlineData(DuplicateEnum.Second)]
        public void SerializeDeserializeDuplicateUtf8(DuplicateEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(DuplicateEnum.First)]
        [InlineData(DuplicateEnum.Second)]
        public void SerializeDeserializeDuplicateUtf16(DuplicateEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Flags]
        public enum FlagsEnum
        {
            First = 1,
            Second = 2,
            Third = 4
        }


        [Flags]
        public enum DuplicateFlagsEnum
        {
            First = 1,
            Second = 2,
            Third = 2
        }


        [Theory]
        [InlineData(DuplicateFlagsEnum.First)]
        [InlineData(DuplicateFlagsEnum.First | DuplicateFlagsEnum.Second)]
        public void SerializeDeserializeDuplicateFlagsUtf16(DuplicateFlagsEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }


        [Theory]
        [InlineData(DuplicateFlagsEnum.First)]
        [InlineData(DuplicateFlagsEnum.First | DuplicateFlagsEnum.Second)]
        public void SerializeDeserializeDuplicateFlagsUtf8(DuplicateFlagsEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }


        [Theory]
        [InlineData(FlagsEnum.First)]
        [InlineData(FlagsEnum.First | FlagsEnum.Second)]
        [InlineData(FlagsEnum.First | FlagsEnum.Second | FlagsEnum.Third)]
        public void SerializeDeserializeFlagsUtf16(FlagsEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<FlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<FlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }


        [Theory]
        [InlineData(FlagsEnum.First)]
        [InlineData(FlagsEnum.First | FlagsEnum.Second)]
        [InlineData(FlagsEnum.First | FlagsEnum.Second | FlagsEnum.Third)]
        public void SerializeDeserializeFlagsUtf8(FlagsEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<FlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<FlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);

        }

        [Fact]
        public void SerializeDeserializeDuplicateUtf16Manual()
        {
            var value = DuplicateEnum.Third;
            var serialized = JsonSerializer.Generic.Utf16.Serialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDuplicateFlagsUtf16Manual()
        {
            var value = DuplicateFlagsEnum.First | DuplicateFlagsEnum.Second | DuplicateFlagsEnum.Third;
            var serialized = JsonSerializer.Generic.Utf16.Serialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DuplicateFlagsEnum, ExcludeNullCamelCaseIntegerEnumResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }
    }
}