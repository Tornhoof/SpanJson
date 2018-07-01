using System.Globalization;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumIntegerTests
    {
        public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
        {
            public CustomResolver() : base(new SpanJsonOptions
            {
                NullOption = NullOptions.ExcludeNulls,
                NamingConvention = NamingConventions.CamelCase,
                EnumOption = EnumOptions.Integer
            })
            {
            }
        }



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

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.None)]
        [InlineData(TestEnum.World)]
        public void SerializeDeserializeUtf16(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestEnum, CustomResolver<char>>(value);
            Assert.Contains(((int) value).ToString(CultureInfo.InvariantCulture), serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestEnum, CustomResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.None)]
        [InlineData(TestEnum.World)]
        public void SerializeDeserializeUtf8(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestEnum, CustomResolver<byte>>(value);
            Assert.Contains(((int)value).ToString(CultureInfo.InvariantCulture), Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestEnum, CustomResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestLongEnum.Min)]
        [InlineData(TestLongEnum.None)]
        [InlineData(TestLongEnum.Max)]
        public void SerializeDeserializeLongUtf16(TestLongEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestLongEnum, CustomResolver<char>>(value);
            Assert.Contains(((long) value).ToString(CultureInfo.InvariantCulture), serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestLongEnum, CustomResolver<char>>(serialized);
            Assert.Equal(value, deserialized);
        }

        [Theory]
        [InlineData(TestLongEnum.Min)]
        [InlineData(TestLongEnum.None)]
        [InlineData(TestLongEnum.Max)]
        public void SerializeDeserializeLongUtf8(TestLongEnum value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestLongEnum, CustomResolver<byte>>(value);
            Assert.Contains(((long)value).ToString(CultureInfo.InvariantCulture), Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestLongEnum, CustomResolver<byte>>(serialized);
            Assert.Equal(value, deserialized);
        }
    }
}
