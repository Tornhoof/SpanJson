using System.Buffers.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumTests
    {
        public enum TestEnum
        {
            Hello,
            World,
            Universe
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.World)]
        [InlineData(TestEnum.Universe)]
        public void SerializeUtf16(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.SerializeToString(value);
            Assert.Equal($"\"{value}\"", serialized);
        }

        [Theory]
        [InlineData("\"Hello\"", TestEnum.Hello)]
        [InlineData("\"World\"", TestEnum.World)]
        [InlineData("\"Universe\"", TestEnum.Universe)]
        public void DeserializeUtf16(string value, TestEnum comparison)
        {
            var deserialized = JsonSerializer.Generic.Deserialize<TestEnum>(value);
            Assert.Equal(deserialized, comparison);
        }

        [Theory]
        [InlineData(TestEnum.Hello)]
        [InlineData(TestEnum.World)]
        [InlineData(TestEnum.Universe)]
        public void SerializeDeserializeUtf8(TestEnum value)
        {
            var serialized = JsonSerializer.Generic.SerializeToByteArray(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<TestEnum>(serialized);
            Assert.Equal(value, deserialized);
        }
    }
}