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

            [IgnoreDataMember] public int Excluded { get; set; }

            public string ExcludeNull { get; set; }
            public string OnlyIfHelloWorld { get; set; }

            public bool ShouldSerializeOnlyIfHelloWorld()
            {
                return OnlyIfHelloWorld == "Hello World";
            }
        }

        [Fact]
        public void DifferentName()
        {
            var optional = new Optional {DifferentName = "Hello World"};
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.Contains("\"AnotherName\":\"Hello World\"", serialized);

            var deserialized =
                JsonSerializer.Generic.Deserialize<Optional, ExcludeNullsOriginalCaseResolver>(
                    "{\"AnotherName\": \"Hello World\"}");
            Assert.Equal("Hello World", deserialized.DifferentName);
        }

        [Fact]
        public void Excluded()
        {
            var optional = new Optional {Excluded = 5};
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.DoesNotContain("\"Excluded\":", serialized);
        }

        [Fact]
        public void ExcludeNull()
        {
            var optional = new Optional {ExcludeNull = null};
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.DoesNotContain("\"ExcludeNull\":", serialized);

            var deserialized =
                JsonSerializer.Generic.Deserialize<Optional, ExcludeNullsOriginalCaseResolver>("{\"Excluded\": 1}");
            Assert.Equal(0, deserialized.Excluded);
        }

        [Fact]
        public void OnlineIfHelloWorld()
        {
            var optional = new Optional {OnlyIfHelloWorld = "Hello Universe"};
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.DoesNotContain("\"OnlyIfHelloWorld\":", serialized);
            optional.OnlyIfHelloWorld = "Hello World";
            serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.Contains("\"OnlyIfHelloWorld\":\"Hello World\"", serialized);
        }
    }
}