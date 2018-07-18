using System;
using System.Text;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class CamelCaseTests
    {
        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var input = new TestObject {Text = "Hello World"};
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestObject, ExcludeNullsCamelCaseResolver<char>>(input);
            Assert.Contains("\"text\":", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestObject, ExcludeNullsCamelCaseResolver<char>>(serialized);
            Assert.Equal(input, deserialized);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var input = new TestObject {Text = "Hello World"};
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestObject, ExcludeNullsCamelCaseResolver<byte>>(input);
            Assert.Contains("\"text\":", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestObject, ExcludeNullsCamelCaseResolver<byte>>(serialized);
            Assert.Equal(input, deserialized);
        }

        public class TestObject : IEquatable<TestObject>
        {
            public string Text { get; set; }

            public bool Equals(TestObject other)
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
                return Equals((TestObject) obj);
            }

            public override int GetHashCode()
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                return Text?.GetHashCode() ?? 0;
            }
        }
    }
}