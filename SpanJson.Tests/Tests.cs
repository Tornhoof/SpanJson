using System;
using System.Collections.Generic;
using System.Linq;
using Jil;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using Xunit;

namespace SpanJson.Tests
{
    public class Tests
    {
        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanSerializeAll(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Serialize(model);
            Assert.NotNull(serialized);
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAll(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, UntypedEqualityComparer.Default);
        }


        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllFromJil(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JSON.Serialize(model, Options.ISO8601ExcludeNullsIncludeInherited);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(serialized, modelType);
            Assert.NotNull(deserialized);
            Assert.IsType(modelType, deserialized);
            Assert.Equal(model, deserialized, UntypedEqualityComparer.Default);
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
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        public void WriteEscaped(string input, string output)
        {
            var serialized = JsonSerializer.Generic.Serialize(input);
            var jilSerialized = JSON.Serialize(input);
            Assert.Equal(output, serialized);
            Assert.Equal(jilSerialized, serialized);
        }
    }
}