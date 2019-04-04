using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SpanJson.Resolvers;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests
{
    public class ConstructorAttributeTests
    {
        public class InTest
        {
            public int Value { get; }

            [JsonConstructor]
            public InTest(in int value)
            {
                Value = value;
            }
        }

        public readonly struct RefTest
        {
            public int Value { get; }

            [JsonConstructor]
            public RefTest(ref int value)
            {
                Value = value;
            }
        }

        public class InNullableTest
        {
            public int? Value { get; }

            [JsonConstructor]
            public InNullableTest(in int? value)
            {
                Value = value;
            }
        }

        public readonly struct RefNullableTest
        {
            public int? Value { get; }

            [JsonConstructor]
            public RefNullableTest(ref int? value)
            {
                Value = value;
            }
        }

        public class DefaultDO : IEquatable<DefaultDO>
        {
            [JsonConstructor]
            public DefaultDO(int value, string key)
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
            var defaultdo = new DefaultDO(5, "Hello");
            var serialized = JsonSerializer.Generic.Utf8.Serialize(defaultdo);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<DefaultDO>(serialized);
            Assert.Equal(defaultdo, deserialized);
        }

        [Fact]
        public void TestDefaultUtf16()
        {
            var defaultdo = new DefaultDO(5, "Hello");
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

        [Fact]
        public void InTestUtf16()
        {
            var intest = new InTest(5);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(intest);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<InTest>(serialized);
            Assert.Equal(intest.Value, deserialized.Value);
        }

        [Fact]
        public void InTestUtf8()
        {
            var intest = new InTest(5);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(intest);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<InTest>(serialized);
            Assert.Equal(intest.Value, deserialized.Value);
        }

        [Fact]
        public void RefTestUtf16()
        {
            var value = 8;
            var refTest = new RefTest(ref value);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(refTest);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<RefTest>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
        }

        [Fact]
        public void RefTestUtf8()
        {
            var value = 8;
            var refTest = new RefTest(ref value);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(refTest);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<RefTest>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
        }

        [Fact]
        public void InNullableTestUtf16()
        {
            var intest = new InNullableTest(5);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(intest);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<InNullableTest>(serialized);
            Assert.Equal(intest.Value, deserialized.Value);
        }

        [Fact]
        public void InNullableTestUtf8()
        {
            var intest = new InNullableTest(5);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(intest);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<InNullableTest>(serialized);
            Assert.Equal(intest.Value, deserialized.Value);
        }

        [Fact]
        public void RefNullableTestUtf16()
        {
            int? value = 8;
            var refTest = new RefNullableTest(ref value);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(refTest);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<RefNullableTest>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
        }

        [Fact]
        public void RefNullableTestUtf8()
        {
            int? value = 8;
            var refTest = new RefNullableTest(ref value);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(refTest);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<RefNullableTest>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
        }

        [Fact]
        public void RefNullableTestNullUtf16()
        {
            int? value = null;
            var refTest = new RefNullableTest(ref value);
            var serialized = JsonSerializer.Generic.Utf16.Serialize<RefNullableTest, IncludeNullsOriginalCaseResolver<char>>(refTest);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<RefNullableTest, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
        }

        [Fact]
        public void RefNullableTestNullUtf8()
        {
            int? value = null;
            var refTest = new RefNullableTest(ref value);
            var serialized = JsonSerializer.Generic.Utf8.Serialize<RefNullableTest, IncludeNullsOriginalCaseResolver<byte>>(refTest);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<RefNullableTest, IncludeNullsOriginalCaseResolver<byte>>(serialized);
            Assert.Equal(refTest.Value, deserialized.Value);
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

        public class PrivateConstructorTest
        {
            public int Value { get; }

            [JsonConstructor]
            private PrivateConstructorTest(int value)
            {
                Value = value;
            }
        }

        [Fact]
        public void TestPrivateConstructorUtf16()
        {
            var input = "{\"Value\":5}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<PrivateConstructorTest, CustomResolver<char>>(input);
            Assert.Equal(5, deserialized.Value);
        }


        [Fact]
        public void TestPrivateConstructorUtf8()
        {
            var input = "{\"Value\":5}";
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<PrivateConstructorTest, CustomResolver<byte>>(Encoding.UTF8.GetBytes(input));
            Assert.Equal(5, deserialized.Value);
        }

        public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
        {
            public CustomResolver() : base(new SpanJsonOptions())
            {
              
            }

            protected override void TryGetAnnotatedAttributeConstructor(Type type, out ConstructorInfo constructor, out JsonConstructorAttribute attribute)
            {
                constructor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(a => a.GetCustomAttribute<JsonConstructorAttribute>() != null);
                if (constructor != null)
                {
                    attribute = constructor.GetCustomAttribute<JsonConstructorAttribute>();
                    return;
                }

                if (TryGetBaseClassJsonConstructorAttribute(type, out attribute))
                {
                    // We basically take the one with the most parameters, this needs to match the dictionary // TODO find better method
                    constructor = type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).OrderByDescending(a => a.GetParameters().Length)
                        .FirstOrDefault();
                    return;
                }

                constructor = default;
                attribute = default;
            }
        }
    }
}