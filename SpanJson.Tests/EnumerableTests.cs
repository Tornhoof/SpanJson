using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xunit;

namespace SpanJson.Tests
{
    public class EnumerableTests
    {
        public class CustomEnumerable<T> : IEnumerable<T>, IEquatable<CustomEnumerable<T>>
        {
            private readonly IEnumerable<T> _values;

            public CustomEnumerable(IEnumerable<T> values)
            {
                _values = values;
            }

            public IEnumerator<T> GetEnumerator() => _values.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public bool Equals(CustomEnumerable<T> other)
            {
                using (var enumerator = GetEnumerator())
                {
                    using (var otherEnumerator = other.GetEnumerator())
                    {
                        if (enumerator.MoveNext() != otherEnumerator.MoveNext())
                        {
                            return false;
                        }

                        if (!Equals(enumerator.Current, otherEnumerator.Current))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public override bool Equals(object other)
            {
                if (other is CustomEnumerable<T> value)
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

        [Fact]
        public void SerializeDeserializeCollectionUtf16()
        {
            var collection = new Collection<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Collection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeCollectionUtf8()
        {
            var collection = new Collection<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Collection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeReadOnlyCollectionUtf16()
        {
            var collection = new ReadOnlyCollection<string>(new List<string> {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ReadOnlyCollection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeReadOnlyCollectionUtf8()
        {
            var collection = new ReadOnlyCollection<string>(new List<string> {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ReadOnlyCollection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeIReadOnlyCollectionUtf16()
        {
            var collection = new List<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IReadOnlyCollection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeIReadOnlyCollectionUtf8()
        {
            var collection = new List<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<IReadOnlyCollection<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeIEnumerableUtf16()
        {
            var collection = new List<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IEnumerable<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeIEnumerableUtf8()
        {
            var collection = new List<string> {"Hello", "World"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<IEnumerable<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeCustomIEnumerableUtf16()
        {
            var collection = new List<string> {"Hello", "World"};
            var customEnumerable = new CustomEnumerable<string>(collection);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(customEnumerable);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<CustomEnumerable<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeCustomIEnumerableUtf8()
        {
            var collection = new List<string> {"Hello", "World"};
            var customEnumerable = new CustomEnumerable<string>(collection);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(customEnumerable);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<CustomEnumerable<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeQueueUtf16()
        {
            var collection = new Queue<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Queue<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeQueueUtf8()
        {
            var collection = new Queue<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Queue<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeConcurrentQueueUtf16()
        {
            var collection = new ConcurrentQueue<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ConcurrentQueue<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }


        [Fact]
        public void SerializeDeserializeConcurrentQueueUtf8()
        {
            var collection = new ConcurrentQueue<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ConcurrentQueue<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection, deserialized);
        }

        [Fact]
        public void SerializeDeserializeConcurrentBagUtf16()
        {
            var collection = new ConcurrentBag<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ConcurrentBag<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }


        [Fact]
        public void SerializeDeserializeConcurrentBagUtf8()
        {
            var collection = new ConcurrentBag<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ConcurrentBag<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }

        [Fact]
        public void SerializeDeserializeHashSetUtf16()
        {
            var collection = new HashSet<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<HashSet<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }


        [Fact]
        public void SerializeDeserializeHashSetUtf8()
        {
            var collection = new HashSet<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<HashSet<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }

        [Fact]
        public void SerializeDeserializeISetSetUtf16()
        {
            var collection = new HashSet<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ISet<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }


        [Fact]
        public void SerializeDeserializeISetUtf8()
        {
            var collection = new HashSet<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ISet<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }

        [Fact]
        public void SerializeDeserializeStackUtf16()
        {
            var collection = new Stack<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf16.Deserialize<ConcurrentStack<string>>(serialized));
        }


        [Fact]
        public void SerializeDeserializeStackUtf8()
        {
            var collection = new Stack<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf8.Deserialize<ConcurrentStack<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeConcurrentStackUtf16()
        {
            var collection = new ConcurrentStack<string>(new[] {"Hello", "World", "Universe"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf16.Deserialize<ConcurrentStack<string>>(serialized));
        }


        [Fact]
        public void SerializeDeserializeConcurrentStackUtf8()
        {
            var collection = new ConcurrentStack<string>(new[] {"Hello", "World", "Universe"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf8.Deserialize<ConcurrentStack<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeLinkedListUtf16()
        {
            var collection = new LinkedList<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<LinkedList<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }


        [Fact]
        public void SerializeDeserializeLinkedListUtf8()
        {
            var collection = new LinkedList<string>(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(collection);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<LinkedList<string>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(collection.Count, deserialized.Count);
            Assert.Contains("Hello", deserialized);
            Assert.Contains("World", deserialized);
        }

        public class EnumerableHelper : IEquatable<EnumerableHelper>
        {
            public IReadOnlyCollection<int> Values { get; set; }
            public IDictionary<string,string> Dictionary { get; set; }

            public bool Equals(EnumerableHelper other)
            {
                if (Values.Count != other.Values.Count)
                {
                    return false;
                }
                if (Dictionary.Count != other.Dictionary.Count)
                {
                    return false;
                }
                var list = new List<int>(Values);
                var otherList = new List<int>(other.Values);
                return list.SequenceEqual(otherList) && Dictionary.SequenceEqual(other.Dictionary);
            }

            public override bool Equals(object obj)
            {
                if (obj is EnumerableHelper value)
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

        [Fact]
        public void SerializeDeserializeEnumerableHelperUtf16()
        {
            var eh = new EnumerableHelper
            {
                Values = new List<int> {1, 2, 3, 4, 5},
                Dictionary = new Dictionary<string, string> {{"a", "b"}, {"b", "c"}}
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(eh);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnumerableHelper>(serialized);
            Assert.NotNull(deserialized);
        }


        [Fact]
        public void SerializeDeserializeEnumerableHelperUtf8()
        {
            var eh = new EnumerableHelper
            {
                Values = new List<int> { 1, 2, 3, 4, 5 },
                Dictionary = new Dictionary<string, string> { { "a", "b" }, { "b", "c" } }
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(eh);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnumerableHelper>(serialized);
            Assert.NotNull(deserialized);
        }
    }
}