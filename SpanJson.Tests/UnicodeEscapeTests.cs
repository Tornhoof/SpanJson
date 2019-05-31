using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class UnicodeEscapeTests
    {
        public class Person
        {
            /// <summary>
            /// People
            /// </summary>
            public string 人 { get; set; }
            /// <summary>
            /// Name
            /// </summary>
            public string 名称 { get; set; }
            /// <summary>
            /// Number
            /// </summary>
            public Numbers 数 { get; set; }
        }

        public enum Numbers
        {
            一,
            二,
            三,
        }

        [Fact]
        public void SerializeDeserializePersonUtf16()
        {
            var person = new Person
            {
                人 = "自",
                名称 = "男",
                数 = Numbers.三,
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(person);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Person>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(person.人,deserialized.人);
            Assert.Equal(person.名称, deserialized.名称);
            Assert.Equal(person.数, deserialized.数);
        }

        [Fact]
        public void SerializeDeserializePersonUtf8()
        {
            var person = new Person
            {
                人 = "自",
                名称 = "男",
                数 = Numbers.三,
            };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(person);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Person>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(person.人, deserialized.人);
            Assert.Equal(person.名称, deserialized.名称);
            Assert.Equal(person.数, deserialized.数);
        }

        [Fact]
        public void SerializeDeserializePersonFullyEncodedUtf16()
        {
            var person = new Person
            {
                人 = "自",
                名称 = "男",
                数 = Numbers.三,
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(person);
            var encoded = EscapeHelper.NonAsciiEscape(serialized);
            Assert.NotNull(encoded);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Person>(encoded);
            Assert.NotNull(deserialized);
            Assert.Equal(person.人, deserialized.人);
            Assert.Equal(person.名称, deserialized.名称);
            Assert.Equal(person.数, deserialized.数);
        }

        [Fact]
        public void SerializeDeserializePersonFullyEncodedUtf8()
        {
            var person = new Person
            {
                人 = "自",
                名称 = "男",
                数 = Numbers.三,
            };
            var serialized = JsonSerializer.Generic.Utf16.Serialize(person);
            var encoded = EscapeHelper.NonAsciiEscape(serialized);
            Assert.NotNull(encoded);
            var bytes = Encoding.UTF8.GetBytes(encoded);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Person>(bytes);
            Assert.NotNull(deserialized);
            Assert.Equal(person.人, deserialized.人);
            Assert.Equal(person.名称, deserialized.名称);
            Assert.Equal(person.数, deserialized.数);
        }

        [Fact]
        public void EmoticonsUtf16()
        {
            var input = "😀😁";
            var fullyEscaped = "\"\\ud83d\\ude00\\ud83d\\ude01\"";
            var output = JsonSerializer.Generic.Utf16.Deserialize<string>(fullyEscaped);
            Assert.Equal(input, output);
        }


        [Fact]
        public void EmoticonsUtf8()
        {
            var input = "😀😁";
            var fullyEscaped = Encoding.UTF8.GetBytes("\"\\ud83d\\ude00\\ud83d\\ude01\"");
            var output = JsonSerializer.Generic.Utf8.Deserialize<string>(fullyEscaped);
            Assert.Equal(input, output);
        }
    }
}
