using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
#if NET5_0
    public class RecordTests
    {
        [Fact]
        public void SerializeDeserializeRecordUtf16()
        {
            var person = new PersonRecord("Hello", "World");
            var serialized = JsonSerializer.Generic.Utf16.Serialize(person);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize(serialized);
            Assert.NotNull(deserialized);
            Assert.Equals(person, deserialized);
        }

        [Fact]
        public void SerializeDeserializeRecordUtf8()
        {
            var person = new PersonRecord("Hello", "World");
            var serialized = JsonSerializer.Generic.Utf8.Serialize(person);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize(serialized);
            Assert.NotNull(deserialized);
            Assert.Equals(person, deserialized);
        }


        public record PersonRecord(string Name, string Title);
    }
#endif
}
