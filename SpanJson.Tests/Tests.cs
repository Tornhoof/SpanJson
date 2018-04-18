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
            Assert.Equal(model, deserialized, GenericEqualityComparer.Default);
        }


        /// <summary>
        ///     Jil's fraction handling is wrong
        /// </summary>
        /// <param name="modelType"></param>
        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllFromJil(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            fixture.Configure<DateTimeValueFixture>().Increment(TimeSpan.TicksPerSecond); // seconds only to get around jil's fraction handling
            var model = fixture.Create(modelType);
            var serialized = JSON.Serialize(model, Options.ISO8601ExcludeNullsIncludeInherited);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(serialized, modelType);
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
    }
}