using SpanJson.Shared;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests
{
    public partial class JsonWriterPropertyNameLengthTests
    {
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var model = Fixture.Create<PropertyNameLengths>();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<PropertyNameLengths>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var model = Fixture.Create<PropertyNameLengths>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<PropertyNameLengths>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }
    }
}