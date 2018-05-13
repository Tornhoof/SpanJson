using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class ConstructorAttributeTests
    {

        public class DefaultDO : IEquatable<DefaultDO>
        {
            [JsonConstructor]
            public DefaultDO(string key, int value)
            {
                Key = key;
                Value = value;
            }

            public string Key { get; }
            public int Value { get; }

            public bool Equals(DefaultDO other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Key, other.Key) && Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((DefaultDO) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ Value;
                }
            }
        }

        public readonly struct NamedDO : IEquatable<NamedDO>
        {
            [JsonConstructor(nameof(Key), nameof(Value))]
            public NamedDO(string key, int value)
            {
                Key = key;
                Value = value;
            }


            public string Key { get; }
            public int Value { get; }

            public bool Equals(NamedDO other)
            {
                return string.Equals(Key, other.Key) && Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is NamedDO && Equals((NamedDO) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Key != null ? Key.GetHashCode() : 0) * 397) ^ Value;
                }
            }
        }

        [Fact]
        public void TestDefaultUtf8()
        {
            var defaultdo = new DefaultDO("Hello", 5);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(defaultdo);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<DefaultDO>(serialized);
            Assert.Equal(defaultdo, deserialized);
        }

        [Fact]
        public void TestDefaultUtf16()
        {
            var defaultdo = new DefaultDO("Hello", 5);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(defaultdo);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<DefaultDO>(serialized);
            Assert.Equal(defaultdo, deserialized);
        }

        [Fact]
        public void TestNamedUtf8()
        {
            var nameddo = new NamedDO("Hello", 5);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(nameddo);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<NamedDO>(serialized);
            Assert.Equal(nameddo, deserialized);
        }

        [Fact]
        public void TestNamedUtf16()
        {
            var nameddo = new NamedDO("Hello", 5);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(nameddo);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<NamedDO>(serialized);
            Assert.Equal(nameddo, deserialized);
        }
    }
}
