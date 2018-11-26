using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

            public override bool Equals(object other)
            {
                if (other is DictionaryValue value)
                {
                    return Equals(value);
                }

                return false;
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        public class CustomReadOnlyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>, IEquatable<CustomReadOnlyDictionary<TKey, TValue>>
        {
            private readonly IDictionary<TKey, TValue> _internal;

            public CustomReadOnlyDictionary(IDictionary<TKey, TValue> input)
            {
                _internal = input;
            }

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _internal.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public int Count => _internal.Count;
            public bool ContainsKey(TKey key) => _internal.ContainsKey(key);

            public bool TryGetValue(TKey key, out TValue value) => _internal.TryGetValue(key, out value);

            public TValue this[TKey key] => _internal[key];

            public IEnumerable<TKey> Keys => _internal.Keys;
            public IEnumerable<TValue> Values => _internal.Values;

            public bool Equals(CustomReadOnlyDictionary<TKey, TValue> other)
            {
                if (Count != other.Count)
                {
                    return false;
                }

                foreach (var key in Keys)
                {
                    if (!other.TryGetValue(key, out var otherValue) || !TryGetValue(key, out var value) || !value.Equals(otherValue))
                    {
                        return false;
                    }
                }

                return true;
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
                {"Привет мир!0", 0},
                {"Привет мир!1", 1},
                {"Привет мир!2", 2},
                {"Привет мир!3", 3},
                {"Привет мир!4", 4},
                {"Привет мир!5", 5},
                {"Привет мир!6", 6},
                {"Привет мир!7", 7},
                {"Привет мир!8", 8},
                {"Привет мир!9", 9},
                {"Привет мир!10", 10},
                {"Привет мир!11", 11},
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
                {"Привет мир!0", 0},
                {"Привет мир!1", 1},
                {"Привет мир!2", 2},
                {"Привет мир!3", 3},
                {"Привет мир!4", 4},
                {"Привет мир!5", 5},
                {"Привет мир!6", 6},
                {"Привет мир!7", 7},
                {"Привет мир!8", 8},
                {"Привет мир!9", 9},
                {"Привет мир!10", 10},
                {"Привет мир!11", 11},
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeReadOnlyDictionaryUtf16()
        {
            IReadOnlyDictionary<string, DictionaryValue> dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeReadOnlyDictionaryUtf8()
        {
            IReadOnlyDictionary<string, DictionaryValue> dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }

        [Fact]
        public void SerializeDeserializeIReadOnlyDictionaryUtf16()
        {
            IReadOnlyDictionary<string, DictionaryValue> dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeIReadOnlyDictionaryUtf8()
        {
            IReadOnlyDictionary<string, DictionaryValue> dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<IReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(dictionary, deserialized);
        }

        [Fact]
        public void SerializeDeserializeCustomReadOnlyDictionaryUtf16()
        {
            var dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var customReadOnlyDictionary = new CustomReadOnlyDictionary<string, DictionaryValue>(dictionary);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(customReadOnlyDictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<CustomReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(customReadOnlyDictionary, deserialized);
        }


        [Fact]
        public void SerializeDeserializeCustomReadOnlyDictionaryUtf8()
        {
            var dictionary = new Dictionary<string, DictionaryValue>
            {
                {"Alice1", new DictionaryValue {Name = "Bob1"}},
                {"Alice2", new DictionaryValue {Name = "Bob2"}},
                {"Alice3", new DictionaryValue {Name = "Bob3"}}
            };
            var customReadOnlyDictionary = new CustomReadOnlyDictionary<string, DictionaryValue>(dictionary);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(customReadOnlyDictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<CustomReadOnlyDictionary<string, DictionaryValue>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(customReadOnlyDictionary, deserialized);
        }

        [Fact]
        public void DeserializeWithWhitespaceUtf8()
        {
            var dict = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, object>>(System.Text.Encoding.UTF8.GetBytes(@"{""a"": 1, ""b"": ""2""}"));
            Assert.True(dict.TryGetValue("a", out dynamic first));
            Assert.Equal(1, (int) first);
            Assert.True(dict.TryGetValue("b", out dynamic second));
            Assert.Equal("2", (string) second);
        }

        [Fact]
        public void DeserializeWithWhitespaceUtf16()
        {
            var dict = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<string, object>>(@"{""a"": 1, ""b"": ""2""}");
            Assert.True(dict.TryGetValue("a", out dynamic first));
            Assert.Equal(1, (int) first);
            Assert.True(dict.TryGetValue("b", out dynamic second));
            Assert.Equal("2", (string) second);
        }
    }
}