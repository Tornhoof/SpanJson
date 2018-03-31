using System;
using System.Collections.Generic;
using SpanJson.Benchmarks;
using Xunit;

namespace SpanJson.Tests
{
    public class Tests
    {
        [Fact]
        public void Test1()
        {
            var accessToken = new AccessToken
            {
                access_token = "Hello World",
                account_id = 2500,
                expires_on_date = DateTime.Now,
                scope = new List<string> {"Hello", "World"}
            };
            var serialized = JsonSerializer.Serialize(accessToken);
        }
    }
}
