using System;
using System.Text;
using Jil;
using SpanJson.Shared;
using SpanJson.Shared.Fixture;
using Utf8Json.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class Tests : ModelTestBase
    {
        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllFullyEscapedUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            var encoded = EscapeHelper.NonAsciiEscape(serialized);
            Assert.NotNull(encoded);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(encoded, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllFullyEscapedUtf8(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            var encoded = EscapeHelper.NonAsciiEscape(serialized);
            Assert.NotNull(encoded);
            var encodedBytes = Encoding.UTF8.GetBytes(encoded);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(encodedBytes, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllUtf8(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }


        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllUtf8IntegerEnum(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize<ExcludeNullCamelCaseIntegerEnumResolver<byte>>(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize<ExcludeNullCamelCaseIntegerEnumResolver<byte>>(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllMixed(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            var utf8Bytes = Encoding.UTF8.GetBytes(serialized);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(utf8Bytes, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllWithJil(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JSON.Serialize(model, Options.ISO8601ExcludeNullsIncludeInherited);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            deserialized = JSON.Deserialize(serialized, modelType, Options.ISO8601ExcludeNullsIncludeInherited);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllWithUtf8Json(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = Utf8Json.JsonSerializer.NonGeneric.Serialize(model, StandardResolver.ExcludeNull);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            deserialized = Utf8Json.JsonSerializer.NonGeneric.Deserialize(modelType, serialized, StandardResolver.ExcludeNull);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllWithJilIncludeNull(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            ValueHelper.RandomlySetValuesToNull(model, 4); // 25%
            var serialized = JSON.Serialize(model, Options.ISO8601IncludeInherited);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            deserialized = JSON.Deserialize(serialized, modelType, Options.ISO8601IncludeInherited);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllWithUtf8JsonIncludeNull(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            ValueHelper.RandomlySetValuesToNull(model, 4); // 25%
            var serialized = Utf8Json.JsonSerializer.NonGeneric.Serialize(model, StandardResolver.Default);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
            serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            deserialized = Utf8Json.JsonSerializer.NonGeneric.Deserialize(modelType, serialized, StandardResolver.Default);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllPrettyPrintedUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            serialized = JsonSerializer.PrettyPrinter.Print(serialized);
            Assert.Contains("\r\n", serialized);
            Assert.Contains(": ", serialized);
            serialized = serialized.Replace(": ", " : "); // make sure we have even more whitespaces in
            serialized = serialized.Replace(",", " ,");
            serialized = serialized.Replace("]", " ]");
            serialized = serialized.Replace("[", " [");
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllPrettyPrintedUtf8(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            serialized = JsonSerializer.PrettyPrinter.Print(serialized);
            Assert.NotNull(serialized);
            var serializedAsString = Encoding.UTF8.GetString(serialized);
            Assert.Contains("\r\n", serializedAsString);
            Assert.Contains(": ", serializedAsString);
            serializedAsString = serializedAsString.Replace(": ", " : ");
            serializedAsString = serializedAsString.Replace(",", " ,");
            serializedAsString = serializedAsString.Replace("]", " ]");
            serializedAsString = serializedAsString.Replace("[", " [");
            serialized = Encoding.UTF8.GetBytes(serializedAsString);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }


        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllMinifiedUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            var prettyPrinted = JsonSerializer.PrettyPrinter.Print(serialized);
            Assert.NotNull(prettyPrinted);
            var minified = JsonSerializer.Minifier.Minify(prettyPrinted);
            Assert.NotNull(minified);
            Assert.DoesNotContain("\r\n", minified);
            Assert.DoesNotContain(": ", minified);
            Assert.Equal(serialized, minified);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeDeserializeAllMinifiedUtf8(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            var prettyPrinted = JsonSerializer.PrettyPrinter.Print(serialized);
            Assert.NotNull(prettyPrinted);
            var minified = JsonSerializer.Minifier.Minify(prettyPrinted);
            Assert.NotNull(minified);
            var minifiedAsString = Encoding.UTF8.GetString(minified);
            Assert.DoesNotContain("\r\n", minifiedAsString);
            Assert.DoesNotContain(": ", minifiedAsString);
            Assert.Equal(serialized, minified);
        }
    }
}