using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpanJson.Benchmarks.Fixture;
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
            public NamedDO(string first, int second)
            {
                Key = first;
                Value = second;
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


        [Theory]
        [MemberData(nameof(CreateBaseClassTestData))]
        public void BaseClassAnnotationUtf8(object input)
        {
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize(input);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize(serialized, input.GetType());
            Assert.Equal(input, deserialized);
        }

        [Theory]
        [MemberData(nameof(CreateBaseClassTestData))]
        public void BaseClassAnnotationUtf16(object input)
        {
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize(input);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize(serialized, input.GetType());
            Assert.Equal(input, deserialized);
        }

        public static IEnumerable<object[]> CreateBaseClassTestData()
        {
            Type[] types =
            {
                typeof(KeyValuePair<,>),
                typeof(Tuple<,>),
                typeof(Tuple<,,>),
                typeof(Tuple<,,,>),
                typeof(Tuple<,,,,>),
                typeof(Tuple<,,,,,>),
                typeof(Tuple<,,,,,,>),
                typeof(ValueTuple<,>),
                typeof(ValueTuple<,,>),
                typeof(ValueTuple<,,,>),
                typeof(ValueTuple<,,,,>),
                typeof(ValueTuple<,,,,,>),
                typeof(ValueTuple<,,,,,,>),
            };

            var fixture = new ExpressionTreeFixture();

            Type[] parameterTypes =
            {
                typeof(int), typeof(string)
            };

            foreach (var type in types)
            {
                foreach (var parameterType in parameterTypes)
                {
                    var argLength = type.GetGenericArguments().Length;
                    var closedType = type.MakeGenericType(Enumerable.Repeat(parameterType, argLength).ToArray());
                    var args = new object[argLength];
                    for (int i = 0; i < argLength; i++)
                    {
                        args[i] = fixture.Create(parameterType);
                    }

                    yield return new object[] {Activator.CreateInstance(closedType, args)};
                }
            }
        }
    }
}

