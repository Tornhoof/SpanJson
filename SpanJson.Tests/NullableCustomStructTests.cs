using SpanJson.Formatters;
using SpanJson.Resolvers;
using Xunit;

namespace SpanJson.Tests
{
    public class NullableCustomStructTests
    {
        [Fact]
        public void CustomCustomStructWithNonNullableFormatterWithExcludeNullResolver()
        {
            var input = new EnclosingNonNullableFormatter
            {
                S = new CustomStruct
                {
                    Value = 1,
                }
            };

            var serialized = JsonSerializer.Generic.Utf16.Serialize<EnclosingNonNullableFormatter, ExcludeNullsOriginalCaseResolver<char>>(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnclosingNonNullableFormatter, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.S, deserialized.S);
        }

        [Fact]
        public void StructWithNonNullableFormatterWithIncludeNullResolver()
        {
            var input = new EnclosingNonNullableFormatter
            {
                S = new CustomStruct
                {
                    Value = 1,
                }
            };

            var serialized = JsonSerializer.Generic.Utf16.Serialize<EnclosingNonNullableFormatter, IncludeNullsOriginalCaseResolver<char>>(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnclosingNonNullableFormatter, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.S, deserialized.S);
        }

        [Fact]
        public void StructWithNullableFormatterExcludeNullsResolver()
        {
            var input = new EnclosingNullableFormatter
            {
                S = new CustomStruct
                {
                    Value = 1,
                }
            };

            var serialized = JsonSerializer.Generic.Utf16.Serialize<EnclosingNullableFormatter, ExcludeNullsOriginalCaseResolver<char>>(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnclosingNullableFormatter, ExcludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.S, deserialized.S);
        }

        [Fact]
        public void CustomStructWithNullableFormatterIncludeNullsResolver()
        {
            var input = new EnclosingNullableFormatter
            {
                S = new CustomStruct
                {
                    Value = 1,
                }
            };

            var serialized = JsonSerializer.Generic.Utf16.Serialize<EnclosingNullableFormatter, IncludeNullsOriginalCaseResolver<char>>(input);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<EnclosingNullableFormatter, IncludeNullsOriginalCaseResolver<char>>(serialized);
            Assert.NotNull(deserialized);
            Assert.Equal(input.S, deserialized.S);
        }

        public class EnclosingNonNullableFormatter
        {
            [JsonCustomSerializer(typeof(CustomStructFormatter))]
            public CustomStruct? S;
        }

        public class EnclosingNullableFormatter
        {
            [JsonCustomSerializer(typeof(NullableCustomStructFormatter))]
            public CustomStruct? S;
        }

        public struct CustomStruct
        {
            public int Value;
        }

        public sealed class CustomStructFormatter : ICustomJsonFormatter<CustomStruct>
        {
            public static CustomStructFormatter Default = new CustomStructFormatter();

            public void Serialize(ref JsonWriter<byte> writer, CustomStruct value)
            {
                Int32Utf8Formatter.Default.Serialize(ref writer, value.Value);
            }

            public CustomStruct Deserialize(ref JsonReader<byte> reader)
            {
                var i = Int32Utf8Formatter.Default.Deserialize(ref reader);
                return new CustomStruct {Value = i};
            }

            public void Serialize(ref JsonWriter<char> writer, CustomStruct value)
            {
                Int32Utf16Formatter.Default.Serialize(ref writer, value.Value);
            }

            public CustomStruct Deserialize(ref JsonReader<char> reader)
            {
                var i = Int32Utf16Formatter.Default.Deserialize(ref reader);
                return new CustomStruct { Value = i };
            }

            public object Arguments { get; set; }
        }

        public sealed class NullableCustomStructFormatter : ICustomJsonFormatter<CustomStruct?>
        {
            public static NullableCustomStructFormatter Default = new NullableCustomStructFormatter();

            public void Serialize(ref JsonWriter<byte> writer, CustomStruct? value)
            {
                if (value is CustomStruct v)
                {
                    CustomStructFormatter.Default.Serialize(ref writer, v);
                }
                else
                {
                    writer.WriteNull();
                }
            }

            public CustomStruct? Deserialize(ref JsonReader<byte> reader)
            {
                if (reader.ReadIsNull())
                {
                    return null;
                }

                return CustomStructFormatter.Default.Deserialize(ref reader);
            }

            public void Serialize(ref JsonWriter<char> writer, CustomStruct? value)
            {
                if (value is CustomStruct v)
                {
                    CustomStructFormatter.Default.Serialize(ref writer, v);
                }
                else
                {
                    writer.WriteNull();
                }
            }

            public CustomStruct? Deserialize(ref JsonReader<char> reader)
            {
                return CustomStructFormatter.Default.Deserialize(ref reader);
            }

            public object Arguments { get; set; }
        }
    }
}