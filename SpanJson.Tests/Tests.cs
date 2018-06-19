using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jil;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Resolvers;
using Utf8Json.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class Tests
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
        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface &&
                            !t.IsAbstract)
                .ToList();
            return models.Select(a => new object[] {a});
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
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }
    }
}