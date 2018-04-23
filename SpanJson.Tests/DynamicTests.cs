using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class DynamicTests
    {
        [Theory]
        [MemberData(nameof(GetModels))]
        public void CanDeserializeAllDynamic(Type modelType)
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create(modelType);
            var serialized = JsonSerializer.NonGeneric.Serialize(model);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<dynamic>(serialized);
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

        [Fact]
        public void DeserializeDynamic()
        {
            var fixture = new ExpressionTreeFixture();
            var data = fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Serialize(data);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<dynamic>(serialized);
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

            for (var i = 0; i < deserialized.comments.Count; i++)
            {
                var comment = deserialized.comments[i];

                Assert.NotNull(comment);
            }
        }

        [Fact]
        public void DynamicObjectTestOneProperty()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";

            var serialized = JsonSerializer.Generic.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<MyDynamicObject>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string) dynamicObject.Text);
        }


        [Fact]
        public void DynamicObjectTestTwoProperties()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;

            var serialized = JsonSerializer.Generic.Serialize(dynamicObject);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<MyDynamicObject>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string)dynamicObject.Text);
            Assert.Equal(5, (int)dynamicObject.Value);
        }

        [Fact]
        public void DynamicObjectTestTwoPropertiesIncludeNull()
        {
            dynamic dynamicObject = new MyDynamicObject();
            dynamicObject.Text = "Hello World";
            dynamicObject.Value = 5;
            dynamicObject.NullValue = null;

            var serialized = JsonSerializer.Generic.Serialize<MyDynamicObject, IncludeNullsOriginalCaseResolver>(dynamicObject);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<MyDynamicObject, IncludeNullsOriginalCaseResolver>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal("Hello World", (string)dynamicObject.Text);
            Assert.Equal(5, (int)dynamicObject.Value);
            Assert.Null(deserialized.NullValue);
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
    }
}