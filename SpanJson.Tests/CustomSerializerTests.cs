using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using SpanJson.Formatters;
using Xunit;

namespace SpanJson.Tests
{
    public class CustomSerializerTests
    {
        public class TestDTO : IEquatable<TestDTO>
        {
            public enum TestEnum
            {
                Null = 0,
                First = 1,
                Second = 2,
            }

            public string Name { get; set; }

            [JsonCustomSerializer(typeof(LongAsStringFormatter))]
            public long Value { get; set; }

            [JsonCustomSerializer(typeof(EnumAsIntFormatter))]
            public TestEnum Enum { get; set; }

            [JsonCustomSerializer(typeof(MultiplyFormatter), 2)]
            public long Multiplied { get; set; }

            public bool Equals(TestDTO other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Name, other.Name) && Value == other.Value && Enum == other.Enum;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((TestDTO) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (Name != null ? Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ Value.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) Enum;
                    return hashCode;
                }
            }
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var test = new TestDTO {Enum = TestDTO.TestEnum.First, Name = "Hello World", Value = 12345678, Multiplied = 100};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(test);
            Assert.Contains("\"Value\":\"12345678\"", serialized);
            Assert.Contains("\"Enum\":1", serialized);
            Assert.Contains("\"Multiplied\":200", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestDTO>(serialized);
            Assert.Equal(test, deserialized);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var test = new TestDTO {Enum = TestDTO.TestEnum.First, Name = "Hello World", Value = 12345678, Multiplied = 200 };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(test);
            var stringEncoded = Encoding.UTF8.GetString(serialized);
            Assert.Contains("\"Value\":\"12345678\"", stringEncoded);
            Assert.Contains("\"Enum\":1", stringEncoded);
            Assert.Contains("\"Multiplied\":400", stringEncoded);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestDTO>(serialized);
            Assert.Equal(test, deserialized);
        }


        public sealed class LongAsStringFormatter : ICustomJsonFormatter<long>
        {
            public static readonly LongAsStringFormatter Default = new LongAsStringFormatter();

            public object Arguments { get; set; }

            public void Serialize(ref JsonWriter<char> writer, long value)
            {
                StringUtf16Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture));
            }

            public long Deserialize(ref JsonReader<char> reader)
            {
                var value = StringUtf16Formatter.Default.Deserialize(ref reader);
                if (long.TryParse(value, out long longValue))
                {
                    return longValue;
                }

                throw new InvalidOperationException("Invalid value.");
            }

            public void Serialize(ref JsonWriter<byte> writer, long value)
            {
                StringUtf8Formatter.Default.Serialize(ref writer, value.ToString(CultureInfo.InvariantCulture));
            }

            public long Deserialize(ref JsonReader<byte> reader)
            {
                var value = StringUtf8Formatter.Default.Deserialize(ref reader);
                if (long.TryParse(value, out long longValue))
                {
                    return longValue;
                }

                throw new InvalidOperationException("Invalid value.");
            }
        }

        public sealed class EnumAsIntFormatter : ICustomJsonFormatter<TestDTO.TestEnum>
        {
            public static readonly EnumAsIntFormatter Default = new EnumAsIntFormatter();

            public object Arguments { get; set; }

            public void Serialize(ref JsonWriter<char> writer, TestDTO.TestEnum value)
            {
                Int32Utf16Formatter.Default.Serialize(ref writer, (int) value);
            }

            public TestDTO.TestEnum Deserialize(ref JsonReader<char> reader)
            {
                return (TestDTO.TestEnum) Int32Utf16Formatter.Default.Deserialize(ref reader);
            }

            public void Serialize(ref JsonWriter<byte> writer, TestDTO.TestEnum value)
            {
                Int32Utf8Formatter.Default.Serialize(ref writer, (int) value);
            }

            public TestDTO.TestEnum Deserialize(ref JsonReader<byte> reader)
            {
                return (TestDTO.TestEnum) Int32Utf8Formatter.Default.Deserialize(ref reader);
            }
        }

        public sealed class MultiplyFormatter : ICustomJsonFormatter<long>
        {
            public static readonly MultiplyFormatter Default = new MultiplyFormatter();

            public object Arguments { get; set; }

            public void Serialize(ref JsonWriter<byte> writer, long value)
            {
                Int64Utf8Formatter.Default.Serialize(ref writer, value * (int) Arguments);
            }

            public long Deserialize(ref JsonReader<byte> reader)
            {
                return Int64Utf8Formatter.Default.Deserialize(ref reader) / (int) Arguments;
            }

            public void Serialize(ref JsonWriter<char> writer, long value)
            {
                Int64Utf16Formatter.Default.Serialize(ref writer, value * (int) Arguments);
            }

            public long Deserialize(ref JsonReader<char> reader)
            {
                return Int64Utf16Formatter.Default.Deserialize(ref reader) / (int) Arguments;
            }
        }

        [JsonCustomSerializer(typeof(TwcsCustomSerializer))]
        public class TypeWithCustomSerializer : IEquatable<TypeWithCustomSerializer>
        { 

            public long Value { get; set; }

            public bool Equals(TypeWithCustomSerializer other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return Value == other.Value;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return obj is TypeWithCustomSerializer twcs && Equals(twcs);
            }

            public override int GetHashCode()
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                return Value.GetHashCode();
            }
        }

        public sealed class TwcsCustomSerializer : ICustomJsonFormatter<TypeWithCustomSerializer>
        {
            public static readonly TwcsCustomSerializer Default = new TwcsCustomSerializer();

            public object Arguments { get; set; }

            private void SerializeInternal<TSymbol>(ref JsonWriter<TSymbol> writer, TypeWithCustomSerializer value) where TSymbol : struct
            {
                if (value == null)
                {
                    writer.WriteNull();
                    return;
                }

                writer.WriteBeginObject();

                writer.WriteName(nameof(TypeWithCustomSerializer.Value));
                
                writer.WriteUtf16Int64(value.Value);

                writer.WriteEndObject();
            }

            public void Serialize(ref JsonWriter<byte> writer, TypeWithCustomSerializer value)
            {
                SerializeInternal(ref writer, value);
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private TypeWithCustomSerializer DeserializeInternal<TSymbol>(ref JsonReader<TSymbol> reader) where TSymbol : struct
            {
                if (reader.ReadIsNull())
                {
                    return null;
                }

                reader.ReadBeginObjectOrThrow();
                var result = new TypeWithCustomSerializer {Value = reader.ReadInt64()};
                reader.ReadEndObjectOrThrow();
                return result;
            }

            public TypeWithCustomSerializer Deserialize(ref JsonReader<byte> reader)
            {
                return DeserializeInternal(ref reader);
            }

            public void Serialize(ref JsonWriter<char> writer, TypeWithCustomSerializer value)
            {
                SerializeInternal(ref writer, value);
            }

            public TypeWithCustomSerializer Deserialize(ref JsonReader<char> reader)
            {
                return DeserializeInternal(ref reader);
            }
        }

        [Fact]
        public void SerializeDeserializeTwcsUtf16()
        {
            var test = new TypeWithCustomSerializer {Value = 100};
            var serialized = JsonSerializer.Generic.Utf16.Serialize(test);
            Assert.Contains("\"Value\":100", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TypeWithCustomSerializer>(serialized);
            Assert.Equal(test, deserialized);
        }

        [Fact]
        public void SerializeDeserializeTwcsUtf8()
        {
            var test = new TypeWithCustomSerializer { Value = 100 };
            var serialized = JsonSerializer.Generic.Utf8.Serialize(test);
            var stringEncoded = Encoding.UTF8.GetString(serialized);
            Assert.Contains("\"Value\":100", stringEncoded);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TypeWithCustomSerializer>(serialized);
            Assert.Equal(test, deserialized);
        }
    }
}