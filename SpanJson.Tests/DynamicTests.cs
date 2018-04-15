using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Validators;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using Xunit;

namespace SpanJson.Tests
{
    public class DynamicTests
    {
        [Fact]
        public void DeserializeDynamic()
        {
            var fixture = new ExpressionTreeFixture();
            var data = fixture.Create<Answer>();
            var serialized = JsonSerializer.Generic.Serialize(data);
            Assert.NotNull(serialized);
            var deserialized = JsonSerializer.Generic.Deserialize<dynamic>(serialized);
            Assert.NotNull(deserialized);
            var dt = (DateTime?) deserialized.locked_date;
            foreach (var comment in deserialized.comments)
            {
                
            }
        }
    }
}
