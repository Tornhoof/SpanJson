﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpanJson.Formatters;
using SpanJson.Resolvers;
using SpanJson.Shared.Fixture;
using Xunit;

namespace SpanJson.Tests
{
    public class GlobalCustomFormatterTests
    {
        public class TestDTO : IEquatable<TestDTO>
        {
            public string Value { get; set; }
            public DateTime Date { get; set; }
            public DateTime? NullableDate { get; set; }
            public DateTime?[] NullableDateArray { get; set; }
            public DateTime[] DateArray { get; set; }
            public List<DateTime> DateList { get; set; }

            public bool Equals(TestDTO other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(Value, other.Value) && Date.Equals(other.Date) &&
                       NullableDate.Equals(other.NullableDate) && NullableDateArray.SequenceEqual(other.NullableDateArray) &&
                       DateArray.SequenceEqual(other.DateArray) && DateList.SequenceEqual(other.DateList);
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
                    var hashCode = (Value != null ? Value.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ Date.GetHashCode();
                    hashCode = (hashCode * 397) ^ NullableDate.GetHashCode();
                    hashCode = (hashCode * 397) ^ (DateArray != null ? DateArray.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (NullableDateArray != null ? NullableDateArray.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (DateList != null ? DateList.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private readonly ExpressionTreeFixture _fixture = new ExpressionTreeFixture();

        public sealed class CustomResolver<TSymbol> : ResolverBase<TSymbol, CustomResolver<TSymbol>> where TSymbol : struct
        {
            public CustomResolver() : base(new SpanJsonOptions())
            {
                RegisterGlobalCustomFormatter<DateTime, DateTimeToLongFormatter>();
            }
        }

        public sealed class DateTimeToLongFormatter : ICustomJsonFormatter<DateTime>
        {
            private static readonly long MinUnixTimeSeconds = DateTimeOffset.MinValue.ToUnixTimeSeconds();
            private static readonly DateTime MinValueDateTime = DateTimeOffset.MinValue.UtcDateTime;

            public static readonly DateTimeToLongFormatter Default = new DateTimeToLongFormatter();

            public DateTime Deserialize(ref JsonReader<byte> reader)
            {
                return DateTimeOffset.FromUnixTimeSeconds(reader.ReadUtf8Int64()).DateTime;
            }

            public DateTime Deserialize(ref JsonReader<char> reader)
            {
                return DateTimeOffset.FromUnixTimeSeconds(reader.ReadUtf16Int64()).DateTime;
            }

            public void Serialize(ref JsonWriter<byte> writer, DateTime value)
            {
                writer.WriteUtf8Int64(value <= MinValueDateTime
                    ? MinUnixTimeSeconds
                    : new DateTimeOffset(DateTime.SpecifyKind(value, DateTimeKind.Utc)).ToUnixTimeSeconds());
            }

            public void Serialize(ref JsonWriter<char> writer, DateTime value)
            {
                writer.WriteUtf16Int64(value <= MinValueDateTime
                    ? MinUnixTimeSeconds
                    : new DateTimeOffset(DateTime.SpecifyKind(value, DateTimeKind.Utc)).ToUnixTimeSeconds());
            }
        }


        [Fact]
        public void SerializeDeserializeUtf8()
        {
            var model = _fixture.Create<TestDTO>();
            model.Date = DateTime.Today;
            model.DateArray[0] = model.Date;
            model.NullableDateArray[0] = model.Date;
            model.DateList[0] = model.Date;
            model.NullableDate = model.Date;
            var serialized = JsonSerializer.Generic.Utf8.Serialize<TestDTO, CustomResolver<byte>>(model);
            Assert.NotNull(serialized);
            Assert.Contains("\"Date\":1", Encoding.UTF8.GetString(serialized));
            Assert.Contains("\"DateArray\":[1", Encoding.UTF8.GetString(serialized));
            Assert.Contains("\"NullableDateArray\":[1", Encoding.UTF8.GetString(serialized));
            Assert.Contains("\"DateList\":[1", Encoding.UTF8.GetString(serialized));
            Assert.Contains("\"NullableDate\":1", Encoding.UTF8.GetString(serialized));
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<TestDTO, CustomResolver<byte>>(serialized);
            Assert.Equal(model, deserialized);
        }

        [Fact]
        public void SerializeDeserializeUtf16()
        {
            var model = _fixture.Create<TestDTO>();
            model.Date = DateTime.Today;
            model.DateArray[0] = model.Date;
            model.NullableDateArray[0] = model.Date;
            model.DateList[0] = model.Date;
            model.NullableDate = model.Date;
            var serialized = JsonSerializer.Generic.Utf16.Serialize<TestDTO, CustomResolver<char>>(model);
            Assert.NotNull(serialized);
            Assert.Contains("\"Date\":1", serialized);
            Assert.Contains("\"DateArray\":[1", serialized);
            Assert.Contains("\"NullableDateArray\":[1", serialized);
            Assert.Contains("\"DateList\":[1", serialized);
            Assert.Contains("\"NullableDate\":1", serialized);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<TestDTO, CustomResolver<char>>(serialized);
            Assert.Equal(model, deserialized);
        }
    }
}