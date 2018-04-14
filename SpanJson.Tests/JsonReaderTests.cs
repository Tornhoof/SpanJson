using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SpanJson.Tests
{
    public class JsonReaderTests
    {
        [Theory]
        [InlineData(@"{""Name"":""\\"", ""Test"":""Something""}", "Something")]
        [InlineData(@"{""Name"":"""", ""Test"":""Something""}", "Something")]
        public void ReadNextSegmentTest(string json, string expected)
        {
            var reader = new JsonReader(json);
            reader.ReadBeginObjectOrThrow();
            int count = 0;
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadNameSpan();
                if (name.ToString() == "Test")
                {
                    var value = reader.ReadString();
                    Assert.Equal(expected, value);
                }
                else if(name.ToString() == "Name")
                {
                    reader.ReadNextSegment();
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
