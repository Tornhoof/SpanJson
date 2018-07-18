﻿using System;
using System.Globalization;
using Xunit;

namespace SpanJson.Tests.Generated
{
    // Easiest way to compare is with ToString()
    //https://www.hanselman.com/blog/WhyYouCantDoubleParseDoubleMaxValueToStringOrSystemOverloadExceptionsWhenUsingDoubleParse.aspx
    public partial class DoubleTests
    {
        [Theory]
        [InlineData("-1.79769313486231E+308")]
        [InlineData("1.79769313486231E+308")]
        public void SerializeDeserializeMinMaxUtf8(string input)
        {
            var doubleValue = double.Parse(input, CultureInfo.InvariantCulture);
            var serialized = JsonSerializer.Generic.Utf8.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf8.Deserialize<Double>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }


        [Theory]
        [InlineData("-1.79769313486231E+308")]
        [InlineData("1.79769313486231E+308")]
        public void SerializeDeserializeMinMaxUtf16(string input)
        {
            var doubleValue = double.Parse(input, CultureInfo.InvariantCulture);
            var serialized = JsonSerializer.Generic.Utf16.Serialize(doubleValue);
            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Double>(serialized);
            Assert.Equal(input.ToString(CultureInfo.InvariantCulture), deserialized.ToString(CultureInfo.InvariantCulture));
        }
    }
}