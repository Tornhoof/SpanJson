using System;
using System.Collections.Generic;
using System.Linq;
using SpanJson.Benchmarks;
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

        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface && !t.IsAbstract)
                .ToList();
            return models.Select(a => new object[] {a});
        }
    }
}
