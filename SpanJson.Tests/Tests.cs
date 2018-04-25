using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void CanSerializeDeserializeAllUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.SerializeToString(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(serialized, modelType);
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
            var serialized = JsonSerializer.NonGeneric.SerializeToByteArray(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(serialized, modelType);
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
            var serialized = JsonSerializer.NonGeneric.SerializeToString(model);
            var utf8Bytes = Encoding.UTF8.GetBytes(serialized);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize(utf8Bytes, modelType);
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