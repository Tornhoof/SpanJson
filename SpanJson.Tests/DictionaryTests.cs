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
        public void SerializeDeserializeDictionaryUtf16()
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
        public void SerializeDeserializeDictionaryUtf8()
        {
            var dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, DictionaryValue>>(serialized);
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


        [Fact]
        public void SerializeDeserializeMultiByteKeyDictionaryUtf16()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"Привет мир!0",0},
                {"Привет мир!1",1},
                {"Привет мир!2",2},
                {"Привет мир!3",3},
                {"Привет мир!4",4},
                {"Привет мир!5",5},
                {"Привет мир!6",6},
                {"Привет мир!7",7},
                {"Привет мир!8",8},
                {"Привет мир!9",9},
                {"Привет мир!10",10},
                {"Привет мир!11",11},
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<string, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeMultiByteKeyDictionaryUtf8()
        {
            var dictionary = new Dictionary<string, int>
            {
                {"Привет мир!0",0},
                {"Привет мир!1",1},
                {"Привет мир!2",2},
                {"Привет мир!3",3},
                {"Привет мир!4",4},
                {"Привет мир!5",5},
                {"Привет мир!6",6},
                {"Привет мир!7",7},
                {"Привет мир!8",8},
                {"Привет мир!9",9},
                {"Привет мир!10",10},
                {"Привет мир!11",11},
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }

    }
}