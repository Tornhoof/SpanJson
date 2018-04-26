using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using Xunit;

namespace SpanJson.Tests
{
    public class DictionaryTests
    {
        public class DictionaryValue : IEquatable<DictionaryValue>
        {
            public string Name { get; set; }

            public bool Equals(DictionaryValue other)
            {
                return other?.Name == Name;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        [Fact]
        public void ExpandoObject()
        {
            var expando = new ExpandoObject();
            expando.TryAdd("Hello", "World");
            var serialized = JsonSerializer.Generic.Utf16.Serialize(expando);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ExpandoObject>(serialized);
            Assert.NotNull(deserialized);
            var dict = (IDictionary<string, object>) expando;
            Assert.NotNull(dict);
            Assert.True(dict.ContainsKey("Hello"));
        }

        [Fact]
        public void SerializeDeserializeConcurrentDictionary()
        {
            var dictionary = new ConcurrentDictionary<string, DictionaryValue>();
            dictionary.TryAdd("Alice1", new DictionaryValue {Name = "Bob1"});
            dictionary.TryAdd("Alice2", new DictionaryValue {Name = "Bob2"});
            dictionary.TryAdd("Alice3", new DictionaryValue {Name = "Bob3"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ConcurrentDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }

        [Fact]
        public void SerializeDeserializeDictionary()
        {
            var dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeIDictionary()
        {
            IDictionary<string, DictionaryValue> dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }
    }
}