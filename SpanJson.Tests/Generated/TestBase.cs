using System.Collections.Generic;
using System.Linq;
using SpanJson.Benchmarks.Fixture;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public abstract class TestBase
    {
    }

    public abstract class TestBase<T> : TestBase
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        // ReSharper restore StaticMemberInGenericType

        [Theory]
        [MemberData(nameof(GenerateData))]
        public void SerializeDeserialize(T value)
        {
            var serialized = JsonSerializer.Generic.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Deserialize<T>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Deserialize(serialized, typeof(T));
            var typedNonGeneric = Assert.IsType<T>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }

        public static IEnumerable<object[]> GenerateData()
        {
            var result = new List<object[]>();
            var randomData = new HashSet<T>(Fixture.CreateMany<T>(10));
            foreach (var data in randomData)
            {
                result.Add(new object[] {data});
            }

            return result;
        }
    }

    public abstract class ListTestBase<T> : TestBase
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        // ReSharper restore StaticMemberInGenericType


        [Fact]
        public void SerializeDeserialize()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToList();
            var serialized = JsonSerializer.Generic.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Deserialize<List<T>>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Deserialize(serialized, typeof(List<T>));
            var typedNonGeneric = Assert.IsType<List<T>>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }
    }

    public abstract class ArrayTestBase<T> : TestBase
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        // ReSharper restore StaticMemberInGenericType


        [Fact]
        public void SerializeDeserialize()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToArray();
            var serialized = JsonSerializer.Generic.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Deserialize<T[]>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Deserialize(serialized, typeof(T[]));
            var typedNonGeneric = Assert.IsType<T[]>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }
    }

    public abstract class NullableListTestBase<T> : ListTestBase<T?> where T : struct
    {
    }

    public abstract class NullableArrayTestBase<T> : ArrayTestBase<T?> where T : struct
    {
    }
    public abstract class ClassTestBase<T> : TestBase<T> where T : class
    {
        [Fact]
        public void SerializeDeserializeNull()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Serialize<T>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<T>(input);
            Assert.Null(deserialized);
        }
    }

    public abstract class StructTestBase<T> : TestBase<T> where T : struct
    {
        [Fact]
        public void SerializeDeserializeNull()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Serialize<T?>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<T?>(input);
            Assert.Null(deserialized);
        }
    }
}