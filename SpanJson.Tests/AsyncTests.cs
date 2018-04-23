using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpanJson.Tests
{
    public class AsyncTests
    {
        public class AsyncTestObject : IEquatable<AsyncTestObject>
        {
            public string Text { get; set; }

            public bool Equals(AsyncTestObject other)
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
                return Equals((AsyncTestObject) obj);
            }

            public override int GetHashCode()
            {
                return 0;
            }
        }

        [Fact]
        public async Task SerializeDeserializeGeneric()
        {
            var sb = new StringBuilder();
            var input = new AsyncTestObject {Text = "Hello World"};
            using (var tw = new StringWriter(sb))
            {
                await JsonSerializer.Generic.SerializeAsync(input, tw);
            }

            AsyncTestObject deserialized = null;
            using (var tr = new StringReader(sb.ToString()))
            {
                deserialized = await JsonSerializer.Generic.DeserializeAsync<AsyncTestObject>(tr);
            }

            Assert.Equal(input, deserialized);
        }

        [Fact]
        public async Task SerializeDeserializeNonGeneric()
        {
            var sb = new StringBuilder();
            var input = new AsyncTestObject {Text = "Hello World"};
            using (var tw = new StringWriter(sb))
            {
                await JsonSerializer.NonGeneric.SerializeAsync(input, tw);
            }

            AsyncTestObject deserialized = null;
            using (var tr = new StringReader(sb.ToString()))
            {
                deserialized = (AsyncTestObject) await JsonSerializer.NonGeneric.DeserializeAsync(tr, typeof(AsyncTestObject));
            }

            Assert.Equal(input, deserialized);
        }
    }
}
