using System;
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

                public bool Equals(Nested other)
                {
                    if (ReferenceEquals(null, other)) return false;
                    if (ReferenceEquals(this, other)) return true;
                    return string.Equals(Text, other.Text);
                }

                public override bool Equals(object obj)
                {
                    if (ReferenceEquals(null, obj)) return false;
                    if (ReferenceEquals(this, obj)) return true;
                    if (obj.GetType() != GetType()) return false;
                    return Equals((Nested) obj);
                }

                public override int GetHashCode()
                {
                    return 0;
                }
            }
        }

        [Fact]
        public void SerializeDeserializeGeneric()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<IncludeNull, char, IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<IncludeNull, char, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }

        [Fact]
        public void SerializeDeserializeNonGeneric()
        {
            var includeNull = new IncludeNull {Key = 1};
            var serialized = JsonSerializer.NonGeneric.Utf16.Serialize<char, IncludeNullsOriginalCaseResolver<char>>(includeNull);
            Assert.NotNull(serialized);
            Assert.Contains("null", serialized);
            var deserialized = JsonSerializer.NonGeneric.Deserialize<char, IncludeNullsOriginalCaseResolver<char>>(serialized, typeof(IncludeNull));
            Assert.NotNull(deserialized);
            Assert.Equal(includeNull, deserialized);
        }
    }
}