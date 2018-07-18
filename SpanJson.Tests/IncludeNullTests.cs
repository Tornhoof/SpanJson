using System;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class IncludeNullTests
    {
        public class IncludeNull : IEquatable<IncludeNull>
        {
            public int Key { get; set; }
            public string Value { get; set; }
            public Nested Child { get; set; }

            public bool Equals(IncludeNull other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Key == other.Key && string.Equals(Value, other.Value) && Equals(Child, other.Child);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != GetType()) return false;
                return Equals((IncludeNull) obj);
            }

            public override int GetHashCode()
            {
                return 0;
            }

            public class Nested : IEquatable<Nested>
            {
                public string Text { get; set; }

                public int? IntValue { get; set; }

                public bool Equals(Nested other)
                {
                    if (ReferenceEquals(null, other)) return false;
                    if (ReferenceEquals(this, other)) return true;
                    return string.Equals(Text, other.Text) && IntValue == other.IntValue;
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != this.GetType()) return false;
                    return Equals((Nested) obj);
                }

                public override int GetHashCode()
                {
                    return 0;
                }
            }
        }

        [Fact]
        public void SerializeDeserializeGenericUtf16()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }


        [Fact]
        public void SerializeDeserializeGenericUtf16PrettyPrinted()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            var pretty = JsonSerializer.PrettyPrinter.Print(serialized);
            Assert.Contains("null", pretty);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(pretty);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNonGenericUtf16()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize<IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.NonGeneric.Utf16.Deserialize<IncludeNullsOriginalCaseResolver<char>>(serialized, typeof(IncludeNull));
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }


        [Fact]
        public void SerializeDeserializeGenericUtf8()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.Generic.Utf8.Serialize<IncludeNull, IncludeNullsOriginalCaseResolver<byte>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<IncludeNull, IncludeNullsOriginalCaseResolver<byte>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNonGenericUtf8()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize<IncludeNullsOriginalCaseResolver<byte>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize<IncludeNullsOriginalCaseResolver<byte>>(serialized, typeof(IncludeNull));
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNonGenericUtf8PrettyPrinted()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.NonGeneric.Utf8.Serialize<IncludeNullsOriginalCaseResolver<byte>>(includeNull);
            Assert.NotNull(serialized);
            var pretty = JsonSerializer.PrettyPrinter.Print(serialized);
            var prettyPrinted = Encoding.UTF8.GetString(pretty);
            Assert.Contains("null", prettyPrinted);
            var deserialized = JsonSerializer.NonGeneric.Utf8.Deserialize<IncludeNullsOriginalCaseResolver<byte>>(pretty, typeof(IncludeNull));
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }


        [Fact]
        public void SerializeDeserializeGenericNestedUtf16()
        {
            var includeNull = new IncludeNull {Key = 1, Child = new IncludeNull.Nested {IntValue = null}};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("\"IntValue\":null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IncludeNull, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }

        [Fact]
        public void SerializeDeserializeGenericNestedUtf8()
        {
            var includeNull = new IncludeNull {Key = 1, Child = new IncludeNull.Nested {IntValue = null}};
            var serialized = JsonSerializer.Generic.Utf8.Serialize<IncludeNull, IncludeNullsOriginalCaseResolver<byte>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("\"IntValue\":null", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<IncludeNull, IncludeNullsOriginalCaseResolver<byte>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }
    }
}