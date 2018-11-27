using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;

namespace SpanJson.Tests
{
    [SuppressMessage("ReSharper", "ImpureMethodCallOnReadonlyValueField")]
    public class ImmutableTests
    {
        [Fact]
        public void SerializeDeserializeImmutableArrayUtf8()
        {
            var input = ImmutableArray<string>.Empty.AddRange(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ImmutableArray<string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableArrayUtf16()
        {
            var input = ImmutableArray<string>.Empty.AddRange(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ImmutableArray<string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableDictionaryUtf8()
        {
            var input = ImmutableDictionary<string, string>.Empty.AddRange(new[]
            {
                new KeyValuePair<string, string>("Key1", "Value1"), new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")
            });
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ImmutableDictionary<string, string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableDictionaryUtf16()
        {
            var input = ImmutableDictionary<string, string>.Empty.AddRange(new[]
            {
                new KeyValuePair<string, string>("Key1", "Value1"), new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")
            });
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ImmutableDictionary<string, string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableHashSetUtf8()
        {
            var input = ImmutableHashSet<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf8.Deserialize<ImmutableHashSet<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeImmutableHashSetUtf16()
        {
            var input = ImmutableHashSet<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf16.Deserialize<ImmutableHashSet<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeImmutableListUtf8()
        {
            var input = ImmutableList<string>.Empty.AddRange(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ImmutableList<string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableListUtf16()
        {
            var input = ImmutableList<string>.Empty.AddRange(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ImmutableList<string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableSortedDictionaryUtf8()
        {
            var input = ImmutableSortedDictionary<string, string>.Empty.AddRange(new[]
            {
                new KeyValuePair<string, string>("Key1", "Value1"), new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")
            });
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ImmutableSortedDictionary<string, string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableSortedDictionaryUtf16()
        {
            var input = ImmutableSortedDictionary<string, string>.Empty.AddRange(new[]
            {
                new KeyValuePair<string, string>("Key1", "Value1"), new KeyValuePair<string, string>("Key2", "Value2"),
                new KeyValuePair<string, string>("Key3", "Value3")
            });
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ImmutableSortedDictionary<string, string>>(serialized);
            Assert.Equal(input.ToArray(), deserialized.ToArray());
        }

        [Fact]
        public void SerializeDeserializeImmutableSortedSetUtf8()
        {
            var input = ImmutableSortedSet<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf8.Deserialize<ImmutableSortedSet<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeImmutableSortedSetUtf16()
        {
            var input = ImmutableSortedSet<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf16.Deserialize<ImmutableSortedSet<string>>(serialized));
        }


        [Fact]
        public void SerializeDeserializeImmutableQueueUtf8()
        {
            var input = ImmutableQueue<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf8.Deserialize<ImmutableQueue<string>>(serialized));
        }

        [Fact]
        public void SerializeDeserializeImmutableQueueUtf16()
        {
            var input = ImmutableQueue<string>.Empty.Union(new[] {"Hello", "World"});
            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            Assert.Throws<NotSupportedException>(() => JsonSerializer.Generic.Utf16.Deserialize<ImmutableQueue<string>>(serialized));
        }

    }
}
