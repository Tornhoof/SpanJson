using System.Runtime.Serialization;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class NonLatinEnumDataMemberTests
    {
        [Fact]
        public void Utf8CanSerializeEnumsWithNonLatinValueInEnumMemberAnnotations()
        {
            var input = new StatusContainer
            {
                Status = Status.Busy,
            };

            var serialized = JsonSerializer.Generic.Utf8.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<StatusContainer>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.Status, deserialized.Status);
        }

        [Fact]
        public void Utf16CanSerializeEnumsWithNonLatinValueInEnumMemberAnnotations()
        {
            var input = new StatusContainer
            {
                Status = Status.Busy,
            };

            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<StatusContainer>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.Status, deserialized.Status);
        }

        [Fact]
        public void Utf8CanDeserializeEnumsWithNonLatinValueInEnumMemberAnnotations()
        {
            var serialized = "{\"status\":\"\u0441\u0432\u043e\u0431\u043e\u0434\u043d\u044b\"}";

            var serializedUtf8 = Encoding.UTF8.GetBytes(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<StatusContainer>(serializedUtf8);
            Assert.NotNull(deserialized);
            Assert.Equal(Status.Free, deserialized.Status);
        }

        [Fact]
        public void Utf16CanDeserializeEnumsWithNonLatinValueInEnumMemberAnnotations()
        {
            var serialized = "{\"status\":\"\u0441\u0432\u043e\u0431\u043e\u0434\u043d\u044b\"}";
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<StatusContainer>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(Status.Free, deserialized.Status);
        }

        [DataContract]
        public class StatusContainer
        {
            [DataMember]
            public Status Status { get; set; }
        }


        [DataContract]
        public enum Status
        {
            [EnumMember(Value = "заняты")]
            Busy,
            [EnumMember(Value = "свободны")]
            Free,
        }
    }
}