using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Validators;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Helpers;
using Xunit;

namespace SpanJson.Tests
{
    public class DynamicTests
    {
        [Fact]
        public void DeserializeDynamic()
        {
            var fixture = new ExpressionTreeFixture();
            var data = fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Serialize(data);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            var answer_id = (int?)deserialized.answer_id;
            var dt = (DateTime?) deserialized.locked_date;
            Assert.NotNull(dt);
            foreach (var comment in deserialized.comments)
            {
                Assert.NotNull(comment);                
            }

            for (int i = 0; i < deserialized.comments.Length;i++)
            {
                var comment = deserialized.comments[i];

                Assert.NotNull(comment);
            }
        }

        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllDynamic(Type modelType)
        {
            SpanJsonDynamicNumber s = new SpanJsonDynamicNumber(new []{'0'});
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(model, deserialized, DynamicEqualityComparer.Default);
        }

        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface &&
                            !t.IsAbstract)
                .ToList();
            return models.Select(a => new object[] { a });
        }
    }
}
