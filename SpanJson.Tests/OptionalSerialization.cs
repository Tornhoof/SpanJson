using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class OptionalSerialization
    {
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
            var optional = new Optional { ExcludeNull = null };
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.DoesNotContain("\"ExcludeNull\":", serialized);
        }

        [Fact]
        public void OnlineIfHelloWorld()
        {
            var optional = new Optional { OnlyIfHelloWorld = "Hello Universe" };
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.DoesNotContain("\"OnlyIfHelloWorld\":", serialized);
            optional.OnlyIfHelloWorld = "Hello World";
            serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.Contains("\"OnlyIfHelloWorld\":\"Hello World\"", serialized);
        }

        [Fact]
        public void DifferentName()
        {
            var optional = new Optional { DifferentName = "Hello World" };
            var serialized = JsonSerializer.Generic.Serialize<Optional, ExcludeNullsOriginalCaseResolver>(optional);
            Assert.Contains("\"AnotherName\":\"Hello World\"", serialized);
        }


        public class Optional
        {
            [IgnoreDataMember]
            public int Excluded { get; set; }
            public string ExcludeNull { get; set; }
            public string OnlyIfHelloWorld { get; set; }

            [DataMember(Name =  "AnotherName")]
            public string DifferentName { get; set; }

            public bool ShouldSerializeOnlyIfHelloWorld()
            {
                return OnlyIfHelloWorld == "Hello World";
            }
        }
    }
}
