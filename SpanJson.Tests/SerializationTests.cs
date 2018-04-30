using System;
using System.Collections.Generic;
using SpanJson.Resolvers;
using Xunit;

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

        public class Node
        {
            public Guid Id { get; set; }
            public List<Node> Children { get; set; } = new List<Node>();
        }


        public class OneChinesePropertyName
        {
            public string 你好 { get; set; }
        }

        public class PartialChinesePropertyName
        {
            public string 你好 { get; set; }
            public string 你好你好 { get; set; }
        }

        [Fact]
        public void NoNameMatches()
        {
            var parent = new Parent {Age = 30, Name = "Adam", Children = new List<Child> {new Child {Name = "Cain", Age = 5}}};
            var serializedWithCamelCase =
                JsonSerializer.Generic.Utf16.Serialize<Parent, char, ExcludeNullsOriginalCaseResolver<char>>(parent);
            serializedWithCamelCase = serializedWithCamelCase.ToLowerInvariant();
            Assert.Contains("age", serializedWithCamelCase);
            var deserialized =
                JsonSerializer.Generic.Utf16.Deserialize<Parent, char, ExcludeNullsOriginalCaseResolver<char>>(serializedWithCamelCase);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Children);
            Assert.Equal(0, deserialized.Age);
            Assert.Null(deserialized.Name);
        }

        //[Fact] // TODO Break Recursion
        //public void Loops()
        //{
        //    var node = new Node {Id = Guid.NewGuid()};
        //    node.Children.Add(node);
        //    //var serialized = JsonSerializer.Generic.Serialize(node);
        //    Assert.NotNull(serialized);
        //}


        [Fact]
        public void SerializeDeserializeOneChinesePropertyNameUtf16()
        {
            var wpn = new OneChinesePropertyName {你好 = "Hello"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(wpn);
            Assert.NotNull(serialized);
            Assert.Contains("\"你好\":", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<OneChinesePropertyName>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(wpn.你好, deserialized.你好);
        }

        [Fact]
        public void SerializeDeserializeOneChinesePropertyNameUtf8()
        {
            var wpn = new OneChinesePropertyName {你好 = "Hello"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize(wpn);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<OneChinesePropertyName>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(wpn.你好, deserialized.你好);
        }


        [Fact]
        public void SerializeDeserializePartialChinesePropertyNameUtf16()
        {
            var wpn = new PartialChinesePropertyName {你好 = "Hello", 你好你好 = "World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(wpn);
            Assert.NotNull(serialized);
            Assert.Contains("\"你好\":", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<OneChinesePropertyName>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(wpn.你好, deserialized.你好);
        }

        [Fact]
        public void SerializeDeserializePartialChinesePropertyNameUtf8()
        {
            var wpn = new PartialChinesePropertyName {你好 = "Hello", 你好你好 = "World"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize(wpn);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<OneChinesePropertyName>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(wpn.你好, deserialized.你好);
        }

        public class OutputModel
        {
            public string Name { get; set; }
        }
    }
}