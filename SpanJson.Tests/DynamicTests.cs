using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using Jil;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Formatters.Dynamic;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class DynamicTests
    {
        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllDynamicUtf16(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(model, deserialized, DynamicEqualityComparer.Default);
        }


        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllDynamicUtf8(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(model, deserialized, DynamicEqualityComparer.Default);
        }

        public static IEnumerable<object[]> GetModels()
        {
            var models = typeof(AccessToken).Assembly
                .GetTypes()
                .Where(t => t.Namespace == typeof(AccessToken).Namespace && !t.IsEnum && !t.IsInterface &&
                            !t.IsAbstract)
                .ToList();
            return models.Select(a => new object[] {a});
        }

        public class MyDynamicObject : DynamicObject
        {
            private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return _dictionary.Keys;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                if (_dictionary.TryGetValue(binder.Name, out result))
                {
                    return true;
                }

                return base.TryGetMember(binder, out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                _dictionary[binder.Name] = value;
                return true;
            }
        }

        public class DynamicObjectWithKnownMembers : DynamicObject
        {
            private readonly Dictionary<string, object> _dictionary = new Dictionary<string, object>();

            public override IEnumerable<string> GetDynamicMemberNames()
            {
                return _dictionary.Keys;
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                if (_dictionary.TryGetValue(binder.Name, out result))
                {
                    return true;
                }

                return base.TryGetMember(binder, out result);
            }

            public override bool TrySetMember(SetMemberBinder binder, object value)
            {
                _dictionary[binder.Name] = value;
                return true;
            }

            public int Value { get; set; }

            public int ReadOnly { get; } = 8;

            public IList<string> NotSupported { get; set; }
        }

        [Fact]
        public void DeserializeDynamic()
        {
            var fixture = new ExpressionTreeFixture();
            var data = fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(data);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            var dt = (DateTime?) deserialized.locked_date;
            Assert.NotNull(dt);
            foreach (var comment in deserialized.comments)
            {
                Assert.NotNull(comment);
            }

            for (var i = 0; i < deserialized.comments.Length; i++)
            {
                var comment = deserialized.comments[i];

                Assert.NotNull(comment);
            }

            var dict = (IDictionary<string, dynamic>) deserialized;
            Assert.NotNull(dict);
        }

        [Fact]
        public void DynamicObjectWithKnownMembersUtf16()
        {
            dynamic dynamicObject = new DynamicObjectWithKnownMembers();
            dynamicObject.Value = 5;

            var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(5, (int) dynamicObject.Value);
        }


        [Fact]
        public void DynamicObjectWithKnownMembersUtf8()
        {
            dynamic dynamicObject = new DynamicObjectWithKnownMembers();
            dynamicObject.Value = 5;

            var serialized = JsonSerializer.Generic.Utf8.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(5, (int) dynamicObject.Value);
        }


        [Fact]
        public void DynamicObjectTestOnePropertyUtf16()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";

            var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string) dynamicObject.Text);
        }


        [Fact]
        public void DynamicObjectTestOnePropertyUtf8()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";

            var serialized = JsonSerializer.Generic.Utf8.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string)dynamicObject.Text);
        }



        [Fact]
        public void DynamicObjectTestOnePropertyMultipleTimes()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            for (int i = 0; i < 10000; i++)
            {
                var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
                Assert.NotNull(serialized);
                var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
                Assert.NotNull(deserialized);
                Assert.Equal("Hello World", (string) dynamicObject.Text);
            }
        }

        [Fact]
        public void DynamicObjectTestOnePropertyMultipleTimesJil()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            var serialized = JSON.SerializeDynamic(dynamicObject);
            Assert.NotNull(serialized);
            for (int i = 0; i < 10000; i++)
            {
                var deserialized = JSON.DeserializeDynamic(serialized);
                Assert.NotNull(deserialized);
                Assert.Equal("Hello World", (string)dynamicObject.Text);
            }
        }

        [Fact]
        public void DynamicObjectTestTwoProperties()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;

            var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string) dynamicObject.Text);
            Assert.Equal(5, (int) dynamicObject.Value);
        }

        [Fact]
        public void DynamicObjectSerializeTwice()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;
            dynamicObject.Array = new string[] {"Hello", "World"};

            var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            var serialized2 = JsonSerializer.Generic.Utf16.Serialize(deserialized);
            Assert.Equal(serialized, serialized2);
        }

        [Fact]
        public void DynamicObjectSerializeTwiceDifferentEncodingFirst()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;
            dynamicObject.Array = new string[] { "Hello", "World" };

            var serialized = JsonSerializer.Generic.Utf8.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            var serialized2 = JsonSerializer.Generic.Utf16.Serialize(deserialized);
            Assert.Equal(Encoding.UTF8.GetString(serialized), serialized2);
        }

        [Fact]
        public void DynamicObjectSerializeTwiceDifferentEncodingSecond()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;
            dynamicObject.Array = new string[] { "Hello", "World" };

            var serialized = JsonSerializer.Generic.Utf16.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            var serialized2 = JsonSerializer.Generic.Utf8.Serialize(deserialized);
            Assert.Equal(serialized, Encoding.UTF8.GetString(serialized2));
        }

        [Fact]
        public void DynamicObjectTestTwoPropertiesIncludeNull()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;
            dynamicObject.NullValue = null;

            var serialized = JsonSerializer.Generic.Utf16.Serialize<MyDynamicObject, IncludeNullsOriginalCaseResolver<char>>(dynamicObject);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<MyDynamicObject, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string) dynamicObject.Text);
            Assert.Equal(5, (int) dynamicObject.Value);
            Assert.Null(deserialized.NullValue);
        }

        [Theory]
        [InlineData("\"Hello World\"", typeof(SpanJsonDynamicString<char>.DynamicTypeConverter))]
        [InlineData("12345", typeof(SpanJsonDynamicNumber<char>.DynamicTypeConverter))]
        public void GetTypeConverterUtf16(string input, Type type)
        {
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<object>(input);
            Assert.NotNull(deserialized);
            var typeConverter = TypeDescriptor.GetConverter(deserialized);
            Assert.IsType(type, typeConverter);
        }

        [Theory]
        [InlineData("\"Hello World\"", typeof(SpanJsonDynamicString<byte>.DynamicTypeConverter))]
        [InlineData("12345", typeof(SpanJsonDynamicNumber<byte>.DynamicTypeConverter))]
        public void GetTypeConverterUtf8(string input, Type type)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<object>(bytes);
            Assert.NotNull(deserialized);
            var typeConverter = TypeDescriptor.GetConverter(deserialized);
            Assert.IsType(type, typeConverter);
        }

        [Fact]
        public void SerializeDeserializeDynamicChildUtf16()
        {
            var parent = new NonDynamicParent();
            var child1 = new NonDynamicParent.DynamicChild {Fixed = Guid.NewGuid()};
            child1.Add("Id", Guid.NewGuid());
            parent.Children.Add(child1);
            var child2 = new NonDynamicParent.DynamicChild {Fixed = Guid.NewGuid()};
            child2.Add("Name", "Hello World");
            parent.Children.Add(child2);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(parent);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<NonDynamicParent>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(parent.Children[0].Fixed, deserialized.Children[0].Fixed);
            Assert.Equal(parent.Children[1].Fixed, deserialized.Children[1].Fixed);
            dynamic dynamicChild1 = parent.Children[0];
            dynamic dynamicChild2 = parent.Children[1];
            dynamic deserializedDynamic = deserialized;
            Assert.Equal(dynamicChild1.Id, (Guid) deserializedDynamic.Children[0].Id);
            Assert.Equal(dynamicChild2.Name, (string) deserializedDynamic.Children[1].Name);
        }


        [Fact]
        public void SerializeDeserializeDynamicChildUtf8()
        {
            var parent = new NonDynamicParent();
            var child1 = new NonDynamicParent.DynamicChild { Fixed = Guid.NewGuid() };
            child1.Add("Id", Guid.NewGuid());
            parent.Children.Add(child1);
            var child2 = new NonDynamicParent.DynamicChild { Fixed = Guid.NewGuid() };
            child2.Add("Name", "Hello World");
            parent.Children.Add(child2);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(parent);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<NonDynamicParent>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(parent.Children[0].Fixed, deserialized.Children[0].Fixed);
            Assert.Equal(parent.Children[1].Fixed, deserialized.Children[1].Fixed);
            dynamic dynamicChild1 = parent.Children[0];
            dynamic dynamicChild2 = parent.Children[1];
            dynamic deserializedDynamic = deserialized;
            Assert.Equal(dynamicChild1.Id, (Guid)deserializedDynamic.Children[0].Id);
            Assert.Equal(dynamicChild2.Name, (string)deserializedDynamic.Children[1].Name);
        }

        [Fact]
        public void DeserializeLargeArrayUtf16()
        {
            var array = Enumerable.Range(1, 100000).ToArray();
            var serialized = JsonSerializer.Generic.Utf16.Serialize(array);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<dynamic>(serialized);
            Assert.Equal(100000, deserialized.Length);
            for (int i = 0; i < deserialized.Length; i++)
            {
                Assert.Equal(array[i], (int) deserialized[i]);
            }
        }

        [Fact]
        public void DeserializeLargeArrayUtf8()
        {
            var array = Enumerable.Range(1, 100000).ToArray();
            var serialized = JsonSerializer.Generic.Utf8.Serialize(array);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<dynamic>(serialized);
            Assert.Equal(100000, deserialized.Length);
            for (int i = 0; i < deserialized.Length; i++)
            {
                Assert.Equal(array[i], (int)deserialized[i]);
            }
        }

        public class NonDynamicParent
        {
            public class DynamicChild : DynamicObject
            {
                public Guid Fixed { get; set; }
                public string Name { get; } = "Hello World";
                private static readonly string[] extraFields = new string[] {nameof(Fixed), nameof(Name)};
                private readonly Dictionary<string, object> _extra = new Dictionary<string, object>();
                public override IEnumerable<string> GetDynamicMemberNames()
                {
                    return _extra.Keys.Concat(extraFields);
                }

                public override bool TryGetMember(GetMemberBinder binder, out object result)
                {
                    return _extra.TryGetValue(binder.Name, out result);
                }

                public override bool TrySetMember(SetMemberBinder binder, object value)
                {
                    return _extra.TryAdd(binder.Name, value);
                }

                public void Add(string key, object value)
                {
                    _extra.Add(key, value);
                }
            }
            public List<DynamicChild> Children { get; set; } = new List<DynamicChild>();
        }
    }
}