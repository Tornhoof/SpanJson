using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests.Generated
{
    public abstract class TestBase
    {
        // ReSharper disable StaticMemberInGenericType
        protected static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        // ReSharper restore StaticMemberInGenericType
    }

    public abstract class TestBase<T> : TestBase
    {
        [Theory]
        [MemberData(nameof(GenerateData))]
        public void SerializeDeserializeUtf16(T value)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<T>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, typeof(T));
            var typedNonGeneric = Assert.IsType<T>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }

        [Theory]
        [MemberData(nameof(GenerateData))]
        public void SerializeDeserializeUtf8(T value)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<T>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, typeof(T));
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

        protected static byte[] EscapeMore(byte[] input)
        {
            return Encoding.UTF8.GetBytes(EscapeMore(Encoding.UTF8.GetString(input)));
        }


        protected static string EscapeMore(string input)
        {
            return EscapeHelper.EscapeMore(input);
        }

        [Fact]
        public void DynamicCastUtf8()
        {
            var value = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.Equal(value, (T) deserialized);
        }

        [Fact]
        public void DynamicCastUtf16()
        {
            var value = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.Equal(value, (T) deserialized);
        }
    }

    public abstract class ListTestBase<T> : TestBase
    {
        [Fact]
        public void SerializeDeserializeNullUtf16()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Utf16.Serialize<List<T>>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<List<T>>(input);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullUtf8()
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<List<T>>(null);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<List<T>>(serialized);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToList();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<List<T>>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, typeof(List<T>));
            var typedNonGeneric = Assert.IsType<List<T>>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToList();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<List<T>>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, typeof(List<T>));
            var typedNonGeneric = Assert.IsType<List<T>>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }
    }

    public abstract class ArrayTestBase<T> : TestBase
    {
        [Fact]
        public void SerializeDeserializeDynamicUtf16()
        {
            var input = Fixture.CreateMany<T>(10).ToArray();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T[]) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeDynamicUtf8()
        {
            var input = Fixture.CreateMany<T>(10).ToArray();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T[]) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeNullUtf16()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Utf16.Serialize<T[]>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<T[]>(input);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullUtf8()
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<T[]>(null);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<T[]>(serialized);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToArray();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            Assert.False(string.IsNullOrEmpty(serialized));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<T[]>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, typeof(T[]));
            var typedNonGeneric = Assert.IsType<T[]>(nonGeneric);
            Assert.Equal(value, typedNonGeneric);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var value = new HashSet<T>(Fixture.CreateMany<T>(10)).ToArray();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<T[]>(serialized);
            Assert.Equal(value, deserialized);
            var nonGeneric = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, typeof(T[]));
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
        public void SerializeDeserializeDynamicUtf16()
        {
            var input = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeDynamicUtf8()
        {
            var input = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeNullUtf16()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Utf16.Serialize<T>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<T>(input);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullUtf8()
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<T>(null);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<T>(serialized);
            Assert.Null(deserialized);
        }
    }

    public abstract class StructTestBase<T> : TestBase<T> where T : struct
    {
        [Fact]
        public void SerializeDeserializeDynamicUtf16()
        {
            var input = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T) deserialized);
            Assert.Equal(input, (T?) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeDynamicUtf8()
        {
            var input = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, (T) deserialized);
            Assert.Equal(input, (T?) deserialized);
            var deserializedText = deserialized.ToString();
            Assert.NotNull(deserializedText);
        }

        [Fact]
        public void SerializeDeserializeNullUtf16()
        {
            const string input = "null";
            var serialized = JsonSerializer.Generic.Utf16.Serialize<T?>(null);
            Assert.Equal(input, serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<T?>(input);
            Assert.Null(deserialized);
        }

        [Fact]
        public void SerializeDeserializeNullUtf8()
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize<T?>(null);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<T?>(serialized);
            Assert.Null(deserialized);
        }


        [Fact]
        public void DynamicCastNullableNullUtf8()
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize((T?)null);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.Null((T?)deserialized);
        }

        [Fact]
        public void DynamicCastNullableNullUtf16()
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize((T?)null);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.Null((T?) deserialized);
        }

        [Fact]
        public void DynamicCastNullableUtf8()
        {
            var value = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(value);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.Equal(value, (T?)deserialized);
        }

        [Fact]
        public void DynamicCastNullableUtf16()
        {
            var value = Fixture.Create<T>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(value);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.Equal(value, (T?)deserialized);
        }
    }
}