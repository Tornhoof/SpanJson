using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class SerializationTests
    {
        public abstract class Human
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class AnotherParent : Human
        {
            public List<Child> Children { get; set; }
        }

        public class Parent : Human
        {
            public List<Child> Children { get; set; }
            public Human Partner { get; set; }
        }

        public class Child : Human
        {
        }

        public class Son : Child
        {
            public bool SonSpecific { get; set; }
        }

        public class Daughter : Child
        {
            public bool DaughterSpecific { get; set; }
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
            var parent = new AnotherParent {Age = 30, Name = "Adam", Children = new List<Child> {new Child {Name = "Cain", Age = 5}}};
            var serializedWithCamelCase =
                JsonSerializer.Generic.Utf16.Serialize<AnotherParent, ExcludeNullsOriginalCaseResolver<char>>(parent);
            serializedWithCamelCase = serializedWithCamelCase.ToLowerInvariant();
            Assert.Contains("age", serializedWithCamelCase);
            var deserialized =
                JsonSerializer.Generic.Utf16.Deserialize<AnotherParent, ExcludeNullsOriginalCaseResolver<char>>(serializedWithCamelCase);
            Assert.NotNull(deserialized);
            Assert.Null(deserialized.Children);
            Assert.Equal(0, deserialized.Age);
            Assert.Null(deserialized.Name);
        }

        [Fact]
        public void Loops()
        {
            var node = new Node {Id = Guid.NewGuid()};
            node.Children.Add(node);
            var ex = Assert.Throws<InvalidOperationException>(() => JsonSerializer.Generic.Utf16.Serialize(node));
            Assert.Contains("Nesting Limit", ex.Message);
        }


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

        public class ObjectChildModel
        {
            public object Object { get; set; }
        }

        [Fact]
        public void SerializeDeserializeObjectChildModel()
        {
            var ocm = new ObjectChildModel {Object = new object()};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(ocm);
            Assert.Contains("\"Object\":{}", serialized);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ObjectChildModel>(serialized);
            Assert.NotNull(deserialized);
            Assert.NotNull(deserialized.Object);
        }

        [Fact]
        public void SerializePolymorphic()
        {
            var parent = new Parent
            {
                Age = 30,
                Name = "Adam",
                Children = new List<Child>
                {
                    new Son {Name = "Cain", Age = 5, SonSpecific = true},
                    new Daughter {Name = "Lilith", Age = 8, DaughterSpecific = true}
                },
                Partner = new Parent
                {
                    Age = 30,
                    Name = "Eve",
                    Children = new List<Child>
                    {
                        new Son {Name = "Cain", Age = 5, SonSpecific = true},
                        new Daughter {Name = "Lilith", Age = 8, DaughterSpecific = true}
                    }
                }
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(parent);
            Assert.Contains("\"Name\":\"Eve\"", serialized);
            Assert.Contains("\"SonSpecific\":true", serialized);
            Assert.Contains("\"DaughterSpecific\":true", serialized);
        }

        [Theory]
        [InlineData("칱칳칶칹칼캠츧")]
        [InlineData("칱칳칶칹칼캠츧\t칱칳칶칹칼캠츧")]
        [InlineData("\t칱칳칶칹칼캠츧\t칱칳칶칹칼캠츧\n")]
        [InlineData("😷Hello\t칱칳칶칹칼캠츧\t칱칳칶칹칼캠츧\nWorld😷")]
        [InlineData("칱칳칶칹칼캠츧😁칱칳칶칹칼캠츧")]
        [InlineData("Hello 😁 World")]
        [InlineData("😷Hello 😁 World😷")]
        public void SerializeDeserializeMultiCharStringUtf8(string input)
        {
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<string>(serialized);
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [InlineData("칱칳칶칹칼캠츧")]
        [InlineData("칱칳칶칹칼캠츧\t칱칳칶칹칼캠츧")]
        [InlineData("칱칳칶칹칼캠츧😁칱칳칶칹칼캠츧")]
        [InlineData("Hello 😁 World")]
        [InlineData("😷Hello 😁 World😷")]
        public void SerializeDeserializeMultiCharStringUtf16(string input)
        {
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<string>(serialized);
            Assert.Equal(input, deserialized);
        }

        struct A
        {
            public string X;

            public ReadOnlySpan<char> SubX => X.Substring(2);
        }

        [Fact]
        public void SerializeDeserializeStructWithByRefProperty()
        {
            var result = JsonSerializer.Generic.Utf8.Deserialize<A>(System.Text.Encoding.UTF8.GetBytes(@"{""X"":""001"", ""SubX"":""2""}"));
            Assert.Equal("001", result.X);
        }


        public class ByRefTestObject
        {
            public Span<int> Property
            {
                get => default;
                set => throw new NotImplementedException();
            }

            public Span<int> this[int i]
            {
                get => default;
                set => throw new NotImplementedException();
            }


            public MyRefStruct Second
            {
                get => default;
            }

            public ref struct MyRefStruct
            {
                public MyRefStruct(string value)
                {
                    Value = value;
                }

                public string Value { get; set; }
            }
        }

        [Fact]
        public void SerializeDeserializeByRefUtf16()
        {
            var byRef = new ByRefTestObject();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(byRef);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ByRefTestObject>(serialized);
            Assert.NotNull(deserialized);
        }

        [Fact]
        public void SerializeDeserializeByRefUtf8()
        {
            var byRef = new ByRefTestObject();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(byRef);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ByRefTestObject>(serialized);
            Assert.NotNull(deserialized);
        }
    }

}