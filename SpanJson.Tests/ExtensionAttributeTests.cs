using System;
using System.Collections.Generic;
using SpanJson.Resolvers;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;
using Xunit;
// disabling the redundant cast prevents resharper from removing the (dynamic) casts, they are necessary for the typeconverter to work
// ReSharper disable RedundantCast 

namespace SpanJson.Tests
{
    public class ExtensionAttributeTests
    {
        public class ExtensionTestDTO : IEquatable<ExtensionTestDTO>
        {
            public string Key;
            public string Value;

            [JsonExtensionData]
            public IDictionary<string, object> AdditionalValues { get; set; }

            public bool Equals(ExtensionTestDTO other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;

                if (Key != other.Key)
                {
                    return false;
                }

                if (Value != other.Value)
                {
                    return false;
                }

                if (AdditionalValues != null && other.AdditionalValues == null)
                {
                    return false;
                }

                if (AdditionalValues == null && other.AdditionalValues != null)
                {
                    return false;
                }

                if (AdditionalValues != null)
                {
                    if (AdditionalValues.Count != other.AdditionalValues.Count)
                    {
                        return false;
                    }

                    foreach (var av in AdditionalValues)
                    {
                        if (!other.AdditionalValues.TryGetValue(av.Key, out var value))
                        {
                            return false;
                        }

                        if (!Equals(av.Value, value))
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

            public override bool Equals(object obj)
            {
                if (obj is ExtensionTestDTO extensionTestDto)
                {
                    return Equals(extensionTestDto);
                }

                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    // ReSharper disable NonReadonlyMemberInGetHashCode
                    var hashCode = Key?.GetHashCode() ?? 0;
                    hashCode = (hashCode * 397) ^ Value?.GetHashCode() ?? 0;
                    if (AdditionalValues != null)
                    {
                        foreach (var av in AdditionalValues)
                        {
                            hashCode = (hashCode * 397) ^ av.Key.GetHashCode();
                            hashCode = (hashCode * 397) ^ av.Value?.GetHashCode() ?? 0;
                        }
                    }

                    return hashCode;
                    // ReSharper restore NonReadonlyMemberInGetHashCode
                }
            }
        }

        public class PropertyBag
        {
            [JsonExtensionData]
            public Dictionary<string, object> AdditionalValues { get; set; }
        }


        [Fact]
        public void DeserializeIntoPropertyBagUtf16()
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create<Answer>();
            var output = JsonSerializer.Generic.Utf16.Serialize(model);
            Assert.NotNull(output);
            var propertyBag = JsonSerializer.Generic.Utf16.Deserialize<PropertyBag>(output);
            Assert.NotNull(propertyBag);
            Assert.NotEmpty(propertyBag.AdditionalValues);
            Assert.True(propertyBag.AdditionalValues.ContainsKey(nameof(Answer.answer_id)));

            Assert.Equal(model.community_owned_date, (DateTime?) (dynamic) propertyBag.AdditionalValues[nameof(Answer.community_owned_date)]);
        }

        [Fact]
        public void DeserializeIntoPropertyBagUtf8()
        {
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create<Answer>();
            var output = JsonSerializer.Generic.Utf8.Serialize(model);
            Assert.NotNull(output);
            var propertyBag = JsonSerializer.Generic.Utf8.Deserialize<PropertyBag>(output);
            Assert.NotNull(propertyBag);
            Assert.NotEmpty(propertyBag.AdditionalValues);
            Assert.True(propertyBag.AdditionalValues.ContainsKey(nameof(Answer.answer_id)));
            Assert.Equal(model.title, (string) (dynamic) propertyBag.AdditionalValues[nameof(Answer.title)]);
        }

        [Fact]
        public void SerializeDeserializeCamelCase()
        {
            var dto = new ExtensionTestDTO {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Test", 1.0m}}};
            var output = JsonSerializer.Generic.Utf16.Serialize<ExtensionTestDTO, ExcludeNullsCamelCaseResolver<char>>(dto);
            Assert.Contains("key", output);
            Assert.Contains("test", output);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ExtensionTestDTO, ExcludeNullsCamelCaseResolver<char>>(output);
            Assert.True(deserialized.AdditionalValues.ContainsKey("test"));
        }

        [Fact]
        public void SerializeDeserializeExcludeNulls()
        {
            var dto = new ExtensionTestDTO {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Test", null}}};
            var output = JsonSerializer.Generic.Utf16.Serialize(dto);
            Assert.DoesNotContain("Test", output);
        }

        [Fact]
        public void SerializeDeserializeIncludeNulls()
        {
            var dto = new ExtensionTestDTO {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Test", null}}};
            var output = JsonSerializer.Generic.Utf16.Serialize<ExtensionTestDTO, IncludeNullsOriginalCaseResolver<char>>(dto);
            Assert.Contains("null", output);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ExtensionTestDTO, IncludeNullsOriginalCaseResolver<char>>(output);
            Assert.True(deserialized.AdditionalValues.ContainsKey("Test"));
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var dto = new ExtensionTestDTO
                {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Test", 1.0m}, {"Test2", "Hello Universe"}}};
            var output = JsonSerializer.Generic.Utf16.Serialize(dto);
            Assert.Contains("Test", output);
            Assert.Contains("Test2", output);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<ExtensionTestDTO>(output);
            Assert.True(deserialized.AdditionalValues.TryGetValue("Test", out var value));
            Assert.Equal(1.0m, (decimal) (dynamic) value);
            Assert.True(deserialized.AdditionalValues.TryGetValue("Test2", out var stringValue));
            Assert.Equal("Hello Universe", (string) (dynamic) stringValue);
        }

        [Fact]
        public void SerializeDeserializeUtf16NoDuplicate()
        {
            var dto = new ExtensionTestDTO {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Key", 1.0m}}};
            var output = JsonSerializer.Generic.Utf16.Serialize(dto);
            Assert.Contains("World", output);
            Assert.DoesNotContain("1.0", (dynamic) output);
        }

        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var dto = new ExtensionTestDTO {Key = "Hello", Value = "World", AdditionalValues = new Dictionary<string, object> {{"Test", 1.0m}}};
            var output = JsonSerializer.Generic.Utf8.Serialize(dto);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<ExtensionTestDTO>(output);
            Assert.True(deserialized.AdditionalValues.TryGetValue("Test", out var value));
            Assert.Equal(1.0m, (decimal) (dynamic) value);
        }
    }
}