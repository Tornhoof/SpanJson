using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class OptionalDeserialization
    {
        public const string DefaultValue = "Hello World";

        public class Optional
        {
            [DataMember(Name = "AnotherName")] public string DifferentName;

            [IgnoreDataMember]
            public int Excluded { get; set; }

            public string ExcludeNull { get; set; }
            public string OnlyIfNotDefault { get; set; } = DefaultValue;

            public bool ShouldSerializeOnlyIfNotDefault()
            {
                return OnlyIfNotDefault != DefaultValue;
            }
        }


        public class ShouldDeserializeDO
        {
            public string First { get; set; } = DefaultValue;
            public string Second { get; set; } = DefaultValue;
        }

        public class ShouldDeserializeCtor
        {
            [JsonConstructor]
            public ShouldDeserializeCtor(string first, string second = DefaultValue + "2")
            {
                First = first;
                Second = second;
            }

            public string First { get; }
            public string Second { get; set; }
            public string Third { get; set; } = DefaultValue + "3";
        }

        public class AllCtor
        {
            [JsonConstructor]
            public AllCtor(string @string = "foo", string stringNull = null, int int32 = 42, int? nullableInt32 = 1337, int? nullableInt32Null = null,
                ConsoleKey @enum = ConsoleKey.Y, ConsoleKey? nullableEnum = ConsoleKey.Z, ConsoleKey? nullableEnumNull = null, AllCtor complex = null,
                IReadOnlyList<AllCtor> complexes = null)
            {
                String = @string;
                StringNull = stringNull;
                Int32 = int32;
                NullableInt32 = nullableInt32;
                NullableInt32Null = nullableInt32Null;
                Enum = @enum;
                NullableEnum = nullableEnum;
                NullableEnumNull = nullableEnumNull;
                Complex = complex;
                Complexes = complexes;
            }

            public string String { get; }
            public string StringNull { get; }
            public int Int32 { get; }
            public int? NullableInt32 { get; }
            public int? NullableInt32Null { get; }
            public ConsoleKey Enum { get; }
            public ConsoleKey? NullableEnum { get; }
            public ConsoleKey? NullableEnumNull { get; }
            public AllCtor Complex { get; }
            public IReadOnlyList<AllCtor> Complexes { get; }
        }

        [Fact]
        public void DifferentName()
        {
            const string serialized = "{\"AnotherName\": \"Hello World\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(DefaultValue, deserialized.DifferentName);
        }

        [Fact]
        public void Excluded()
        {
            const string serialized = "{\"Excluded\": 5}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(0, deserialized.Excluded);
        }

        [Fact]
        public void OnlyIfNotDefaultWithValue()
        {
            const string serialized = "{\"OnlyIfNotDefault\": \"Hello Universe\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal("Hello Universe", deserialized.OnlyIfNotDefault);
        }

        [Fact]
        public void OnlyIfNotDefaultWithoutValue()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Optional, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.Equal(DefaultValue, deserialized.OnlyIfNotDefault);
        }

        [Fact]
        public void ShouldDeserializeTestAll()
        {
            const string serialized = "{\"First\": \"Hello Foo\", \"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestFirst()
        {
            const string serialized = "{\"First\": \"Hello Foo\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal(DefaultValue, deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestNone()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal(DefaultValue, deserialized.First);
            Assert.Equal(DefaultValue, deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeTestSecond()
        {
            const string serialized = "{\"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeDO>(serialized);
            Assert.Equal(DefaultValue, deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
        }

        [Fact]
        public void ShouldDeserializeCtorTestAll()
        {
            const string serialized = "{\"First\": \"Hello Foo\", \"Second\": \"Hello Bar\", \"Third\": \"Hello Baz\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
            Assert.Equal("Hello Baz", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestFirst()
        {
            const string serialized = "{\"First\": \"Hello Foo\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Equal("Hello Foo", deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestNone()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestSecond()
        {
            const string serialized = "{\"Second\": \"Hello Bar\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal("Hello Bar", deserialized.Second);
            Assert.Equal(DefaultValue + "3", deserialized.Third);
        }

        [Fact]
        public void ShouldDeserializeCtorTestThird()
        {
            const string serialized = "{\"Third\": \"Hello Baz\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ShouldDeserializeCtor>(serialized);
            Assert.Null(deserialized.First);
            Assert.Equal(DefaultValue + "2", deserialized.Second);
            Assert.Equal("Hello Baz", deserialized.Third);
        }

        [Fact]
        public void AllCtorTestNone()
        {
            const string serialized = "{}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<AllCtor>(serialized);
            Assert.Equal("foo", deserialized.String);
            Assert.Null(deserialized.StringNull);
            Assert.Equal(42, deserialized.Int32);
            Assert.Equal(1337, deserialized.NullableInt32);
            Assert.Null(deserialized.NullableInt32Null);
            Assert.Equal(ConsoleKey.Y, deserialized.Enum);
            Assert.Equal(ConsoleKey.Z, deserialized.NullableEnum);
            Assert.Null(deserialized.NullableEnumNull);
            Assert.Null(deserialized.Complex);
            Assert.Null(deserialized.Complexes);
        }

        [Fact]
        public void AllCtorTestAll()
        {
            const string serialized = "{" +
                                      "\"String\": \"Bar\"," +
                                      "\"StringNull\": null," +
                                      "\"Int32\": 44," +
                                      "\"NullableInt32\": 7331," +
                                      "\"NullableInt32Null\": null," +
                                      "\"Enum\": \"A\"," +
                                      "\"NullableEnum\": \"B\"," +
                                      "\"NullableEnumNull\": null," +
                                      "\"Complex\": {}," +
                                      "\"Complexes\": [{\"String\": \"Baz\"}]" +
                                      "}";

            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<AllCtor>(serialized);
            Assert.Equal("Bar", deserialized.String);
            Assert.Null(deserialized.StringNull);
            Assert.Equal(44, deserialized.Int32);
            Assert.Equal(7331, deserialized.NullableInt32);
            Assert.Null(deserialized.NullableInt32Null);
            Assert.Equal(ConsoleKey.A, deserialized.Enum);
            Assert.Equal(ConsoleKey.B, deserialized.NullableEnum);
            Assert.Null(deserialized.NullableEnumNull);

            Assert.NotNull(deserialized.Complex);
            Assert.Equal("foo", deserialized.Complex.String);
            Assert.Null(deserialized.Complex.StringNull);
            Assert.Equal(42, deserialized.Complex.Int32);
            Assert.Equal(1337, deserialized.Complex.NullableInt32);
            Assert.Null(deserialized.Complex.NullableInt32Null);
            Assert.Equal(ConsoleKey.Y, deserialized.Complex.Enum);
            Assert.Equal(ConsoleKey.Z, deserialized.Complex.NullableEnum);
            Assert.Null(deserialized.Complex.NullableEnumNull);
            Assert.Null(deserialized.Complex.Complex);
            Assert.Null(deserialized.Complex.Complexes);

            Assert.Equal(1, deserialized.Complexes.Count);
            Assert.Equal("Baz", deserialized.Complexes[0].String);
            Assert.Null(deserialized.Complexes[0].StringNull);
            Assert.Equal(42, deserialized.Complexes[0].Int32);
            Assert.Equal(1337, deserialized.Complexes[0].NullableInt32);
            Assert.Null(deserialized.Complexes[0].NullableInt32Null);
            Assert.Equal(ConsoleKey.Y, deserialized.Complexes[0].Enum);
            Assert.Equal(ConsoleKey.Z, deserialized.Complexes[0].NullableEnum);
            Assert.Null(deserialized.Complexes[0].NullableEnumNull);
            Assert.Null(deserialized.Complexes[0].Complex);
            Assert.Null(deserialized.Complexes[0].Complexes);
        }
    }
}