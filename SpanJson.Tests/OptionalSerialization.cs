using System.Runtime.Serialization;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class OptionalSerialization
    {
        public class Optional
        {
            [DataMember(Name = "AnotherName")] public string DifferentName;

            [IgnoreDataMember]
            public int Excluded { get; set; }

            public string ExcludeNull { get; set; }
            public string OnlyIfHelloWorld { get; set; }

            public bool ShouldSerializeOnlyIfHelloWorld()
            {
                return OnlyIfHelloWorld == "Hello World";
            }
        }


        public class ShouldSerializeDO
        {
            public string First { get; set; }

            public string Second { get; set; }

            public bool ShouldSerializeFirst()
            {
                return First == "Hello World";
            }

            public bool ShouldSerializeSecond()
            {
                return Second == "Hello World";
            }
        }

        public class EverythingNonOptional
        {
            public int First { get; set; }
            public int Second { get; set; }
        }

        [Fact]
        public void DifferentName()
        {
            var optional = new Optional {DifferentName = "Hello World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(optional);
            Assert.Contains("\"AnotherName\":\"Hello World\"", serialized);
            Assert.False(serialized.EndsWith(",}"));

            var deserialized =
                JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(
                    "{\"AnotherName\": \"Hello World\"}");
            Assert.Equal("Hello World", deserialized.DifferentName);
        }

        [Fact]
        public void Excluded()
        {
            var optional = new Optional {Excluded = 5};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(optional);
            Assert.DoesNotContain("\"Excluded\":", serialized);
        }

        [Fact]
        public void ExcludeNull()
        {
            var optional = new Optional {ExcludeNull = null};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(optional);
            Assert.DoesNotContain("\"ExcludeNull\":", serialized);

            var deserialized =
                JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>("{\"Excluded\": 1}");
            Assert.Equal(0, deserialized.Excluded);
        }

        [Fact]
        public void OnlineIfHelloWorld()
        {
            var optional = new Optional {OnlyIfHelloWorld = "Hello Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(optional);
            Assert.DoesNotContain("\"OnlyIfHelloWorld\":", serialized);
            Assert.False(serialized.EndsWith(",}"));
            optional.OnlyIfHelloWorld = "Hello World";
            serialized = JsonSerializer.Generic.Utf16.Serialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(optional);
            Assert.Contains("\"OnlyIfHelloWorld\":\"Hello World\"", serialized);
        }

        [Fact]
        public void SerializeEveryting()
        {
            var everything = new EverythingNonOptional {First = 5, Second = 10};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(everything);
            Assert.Contains(",", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EverythingNonOptional>(serialized);
            Assert.NotNull(deserialized);
        }

        [Fact]
        public void ShouldSerializeTestAll()
        {
            var shouldSerializeAll = new ShouldSerializeDO {First = "Hello World", Second = "Hello World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(shouldSerializeAll);
            Assert.NotNull(serialized);
            Assert.False(serialized.StartsWith("{,"));
            Assert.False(serialized.EndsWith(",}"));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldSerializeDO>(serialized);
            Assert.NotNull(deserialized);
        }

        [Fact]
        public void ShouldSerializeTestFirst()
        {
            var shouldSerializeAll = new ShouldSerializeDO {First = "Hello World", Second = "Hello Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(shouldSerializeAll);
            Assert.NotNull(serialized);
            Assert.False(serialized.StartsWith("{,"));
            Assert.False(serialized.EndsWith(",}"));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldSerializeDO>(serialized);
            Assert.NotNull(deserialized);
        }

        [Fact]
        public void ShouldSerializeTestNone()
        {
            var shouldSerializeAll = new ShouldSerializeDO {First = "Hello Universe", Second = "Hello Universe"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(shouldSerializeAll);
            Assert.NotNull(serialized);
            Assert.False(serialized.StartsWith("{,"));
            Assert.False(serialized.EndsWith(",}"));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldSerializeDO>(serialized);
            Assert.NotNull(deserialized);
        }

        [Fact]
        public void ShouldSerializeTestSecond()
        {
            var shouldSerializeAll = new ShouldSerializeDO {First = "Hello Universe", Second = "Hello World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(shouldSerializeAll);
            Assert.NotNull(serialized);
            Assert.False(serialized.StartsWith("{,"));
            Assert.False(serialized.EndsWith(",}"));
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldSerializeDO>(serialized);
            Assert.NotNull(deserialized);
        }
    }
}