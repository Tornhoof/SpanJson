using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SpanJson.Benchmarks;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Formatters;
using Xunit;
using Xunit.Abstractions;

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

        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface && !t.IsAbstract)
                .ToList();
            return models.Select(a => new object[] {a});
        }


        [Theory]
        [InlineData("Hello \"World", "\"Hello \\\"World\"")]
        [InlineData("Hello \"Univ\"erse", "\"Hello \\\"Univ\\\"erse\"")]
        public void WriteEscaped(string input, string output)
        {
            var serialized = JsonSerializer.Generic.Serialize(input);
            var jilSerialized = Jil.JSON.Serialize(input);
            Assert.Equal(output, serialized);
            Assert.Equal(jilSerialized, serialized);
        }
    }

    public class UntypedEqualityComparer : IEqualityComparer<object>
    {
        public static readonly UntypedEqualityComparer Default = new UntypedEqualityComparer();
        private delegate bool TypedEqualityComparer(object x, object y);
        private static ConcurrentDictionary<Type, TypedEqualityComparer> Comparers = new ConcurrentDictionary<Type, TypedEqualityComparer>();

        public bool Equals(object x, object y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (x == null && y != null)
            {
                return false;
            }

            if (x != null && y == null)
            {
                return false;
            }

            var xType = x.GetType();
            var yType = y.GetType();
            if (!ReferenceEquals(xType, yType))
            {
                return false;
            }

            // ReSharper disable ConvertClosureToMethodGroup
            var typedComparer = Comparers.GetOrAdd(xType, t => BuildTypedComparer(t));
            // ReSharper restore ConvertClosureToMethodGroup
            return typedComparer(x, y);
        }

        private TypedEqualityComparer BuildTypedComparer(Type type)
        {
            var xParameter = Expression.Parameter(typeof(object), "x");
            var yParameter = Expression.Parameter(typeof(object), "y");
            var typedXParameter = Expression.Convert(xParameter, type);
            var typedYParameter = Expression.Convert(yParameter, type);

            return Expression.Lambda<TypedEqualityComparer>(
                Expression.Call(typedXParameter, type.GetMethod("Equals", new Type[] {type}), typedYParameter),
                xParameter, yParameter).Compile();
        }

        public int GetHashCode(object obj)
        {
            return 0;
        }
    }
}
