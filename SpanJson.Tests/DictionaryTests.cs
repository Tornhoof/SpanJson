using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class DictionaryTests
    {
        [Fact]
        public void SerializeDeserializeDictionary()
        {
            var dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}},
            };
            var serialized = JsonSerializer.Generic.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<Dictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }

        [Fact]
        public void SerializeDeserializeConcurrentDictionary()
        {
            var dictionary = new ConcurrentDictionary<string, DictionaryValue>();
            dictionary.TryAdd("Alice1", new DictionaryValue {Name = "Bob1"});
            dictionary.TryAdd("Alice2", new DictionaryValue {Name = "Bob2"});
            dictionary.TryAdd("Alice3", new DictionaryValue {Name = "Bob3"});
            var serialized = JsonSerializer.Generic.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<ConcurrentDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


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
    }
}
