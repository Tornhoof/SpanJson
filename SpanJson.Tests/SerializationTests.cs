using System.Collections.Generic;
using SpanJson.Resolvers;
using Xunit;
using Xunit.Sdk;

namespace SpanJson.Tests
{
    public class SerializationTests
    {
        public class Parent
        {
            public List<Child> Children { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class Child
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        //[Fact]        
        //public void NoNameMatches()
        //{
        //    var parent = new Parent {Age = 30, Name = "Adam", Children = new List<Child> {new Child {Name = "Cain", Age = 5}}};
        //    var serializedWithCamelCase =
        //        JsonSerializer.Generic.Serialize<Parent, ExcludeNullsCamelCaseResolver>(parent);
        //    Assert.Contains("age", serializedWithCamelCase);
        //    var deserialized =
        //        JsonSerializer.Generic.Deserialize<Parent, ExcludeNullsOriginalCaseResolver>(serializedWithCamelCase);
        //    Assert.NotNull(deserialized);
        //    Assert.Null(deserialized.Children);
        //    Assert.Equal(0, deserialized.Age);
        //    Assert.Null(deserialized.Name);
        //}
    }
}