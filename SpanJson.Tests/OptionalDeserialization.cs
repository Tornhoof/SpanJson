using System.Runtime.Serialization;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class OptionalDeserialization
    {
        public const string DefaultValue = "Hello World";

        public class Optional
        {
            [DataMember(Name = "AnotherName")] public string DifferentName;

            [IgnoreDataMember]
            public int Excluded { get; set; }

            public string ExcludeNull { get; set; }
            public string OnlyIfNotDefault { get; set; } = DefaultValue;

            public bool ShouldSerializeOnlyIfNotDefault()
            {
                return OnlyIfNotDefault != DefaultValue;
            }
        }


        public class ShouldDeserializeDO
        {
            public string First { get; set; } = DefaultValue;
            public string Second { get; set; } = DefaultValue;
        }

        public class ShouldDeserializeCtor
        {
            [JsonConstructor]
            public ShouldDeserializeCtor(string first, string second = DefaultValue + "2")
            {
                First = first;
                Second = second;
            }

            public string First { get; }
            public string Second { get; set; }
            public string Third { get; set; } = DefaultValue + "3";
        }

        [Fact]
        public void DifferentName()
        {
            const string serialized = "{\"AnotherName\": \"Hello World\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(DefaultValue, deserialized.DifferentName);
        }

        [Fact]
        public void Excluded()
        {
            const string serialized = "{\"Excluded\": 5}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(0, deserialized.Excluded);
        }

        [Fact]
        public void OnlyIfNotDefaultWithValue()
        {
            const string serialized = "{\"OnlyIfNotDefault\": \"Hello Universe\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal("Hello Universe", deserialized.OnlyIfNotDefault);
        }

        [Fact]
        public void OnlyIfNotDefaultWithoutValue()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(DefaultValue, deserialized.OnlyIfNotDefault);
        }

        [Fact]
        public void ShouldDeserializeTestAll()
        {
            const string serialized = "{\"First\": \"Hello Foo\", \"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestFirst()
        {
            const string serialized = "{\"First\": \"Hello Foo\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal(DefaultValue, deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestNone()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal(DefaultValue, deserialized.First);
            Assert.Equal(DefaultValue, deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestSecond()
        {
            const string serialized = "{\"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal(DefaultValue, deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeCtorTestAll()
        {
            const string serialized = "{\"First\": \"Hello Foo\", \"Second\": \"Hello Bar\", \"Third\": \"Hello Baz\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
            Assert.Equal("Hello Baz", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestFirst()
        {
            const string serialized = "{\"First\": \"Hello Foo\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestNone()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestSecond()
        {
            const string serialized = "{\"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestThird()
        {
            const string serialized = "{\"Third\": \"Hello Baz\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal("Hello Baz", deserialized.Third);
        }
    }
}