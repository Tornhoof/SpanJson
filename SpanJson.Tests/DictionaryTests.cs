using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Text;
using SpanJson.Formatters.Dynamic;
using Xunit;

namespace SpanJson.Tests
{
    public partial class DictionaryTests
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

        [Fact]
        public void SerializeDeserializeDictionaryDynamicUtf16()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Key1", 1},
                {"Key2", "2"},
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<string, object>>(serialized);
            Assert.NotNull(deserialized);
            Assert.True(deserialized.TryGetValue("Key1", out var value1));
            Assert.True(deserialized.TryGetValue("Key2", out var value2));
            Assert.IsType<SpanJsonDynamicUtf16Number>(value1);
            Assert.IsType<SpanJsonDynamicUtf16String>(value2);
            Assert.Equal("1", value1.ToString());
            Assert.Equal("2", value2.ToString());
        }


        [Fact]
        public void SerializeDeserializeDictionaryDynamicUtf8()
        {
            var dictionary = new Dictionary<string, object>
            {
                {"Key1", 1},
                {"Key2", "2"},
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(dictionary);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, object>>(serialized);
            Assert.NotNull(deserialized);
            Assert.True(deserialized.TryGetValue("Key1", out var value1));
            Assert.True(deserialized.TryGetValue("Key2", out var value2));
            Assert.IsType<SpanJsonDynamicUtf8Number>(value1);
            Assert.IsType<SpanJsonDynamicUtf8String>(value2);
            Assert.Equal("1", value1.ToString());
            Assert.Equal("2", value2.ToString());
        }

        public enum DictionaryKey
        {
            Key1,
            Key2
        }


        [Fact]
        public void SerializeDeserializeEnumKeyDictionaryUtf8()
        {
            var input = new Dictionary<DictionaryKey, int>
            {
                {DictionaryKey.Key1, 1},
                {DictionaryKey.Key2, 2}
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.Equal("{\"Key1\":1,\"Key2\":2}",Encoding.UTF8.GetString(serialized));
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyDictionaryUtf16()
        {
            var input = new Dictionary<DictionaryKey, int>
            {
                {DictionaryKey.Key1, 1},
                {DictionaryKey.Key2, 2}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.Equal("{\"Key1\":1,\"Key2\":2}", serialized);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyConcurrentDictionaryUtf8()
        {
            var input = new ConcurrentDictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            });

            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyConcurrentDictionaryUtf16()
        {
            var input = new ConcurrentDictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            });
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyIDictionaryDictionaryUtf8()
        {
            IDictionary<DictionaryKey, int> input = new Dictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            });

            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyIDictionaryDictionaryUtf16()
        {
            IDictionary<DictionaryKey, int> input = new Dictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            });
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyReadOnlyDictionaryDictionaryUtf8()
        {
            ReadOnlyDictionary<DictionaryKey, int> input = new ReadOnlyDictionary<DictionaryKey, int>(new Dictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            }));

            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeEnumKeyReadOnlyDictionaryDictionaryUtf16()
        {
            ReadOnlyDictionary<DictionaryKey, int> input = new ReadOnlyDictionary<DictionaryKey, int>(new Dictionary<DictionaryKey, int>(new[]
            {
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key1, 1),
                new KeyValuePair<DictionaryKey, int>(DictionaryKey.Key2, 2)
            }));
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input, deserialized);
        }


        [Fact]
        public void DeserializeDuplicateKeyStringUtf16()
        {
            var serialized = "{\"Key1\":1, \"Key1\":2}";
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<string, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(2, deserialized["Key1"]);
        }


        [Fact]
        public void DeserializeDuplicateKeyStringUtf8()
        {
            var serialized = Encoding.UTF8.GetBytes("{\"Key1\":1, \"Key1\":2}");
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<string, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(2, deserialized["Key1"]);
        }

        [Fact]
        public void DeserializeDuplicateKeyEnumUtf16()
        {
            var serialized = "{\"Key1\":1, \"Key1\":2}";
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(2, deserialized[DictionaryKey.Key1]);
        }


        [Fact]
        public void DeserializeDuplicateKeyEnumUtf8()
        {
            var serialized = Encoding.UTF8.GetBytes("{\"Key1\":1, \"Key1\":2}");
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Dictionary<DictionaryKey, int>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(2, deserialized[DictionaryKey.Key1]);
        }
    }
}