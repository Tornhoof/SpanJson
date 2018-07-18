using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SpanJson.Resolvers;
using SpanJson.Shared;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public class WebTests : IClassFixture<TestControllerFixture>
    {
        private readonly TestControllerFixture _fixture;

        public WebTests(TestControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task PingPong()
        {
            var uri = new UriBuilder(_fixture.BaseAddress) {Path = "api/test/PingPong"};
            var fixture = new ExpressionTreeFixture();
            var model = fixture.Create<TestObject>();
            model.World = null;
            using (var message = new HttpRequestMessage(HttpMethod.Post, uri.Uri))
            {
                message.Content = new ByteArrayContent(JsonSerializer.Generic.Utf8.Serialize<TestObject, AspNetCoreDefaultResolver<byte>>(model));
                message.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

                using (var response = await _fixture.Client.SendAsync(message).ConfigureAwait(false))
                {
                    Assert.True(response.IsSuccessStatusCode);
                    var body = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                    var text = Encoding.UTF8.GetString(body);
                    var resultModel = JsonSerializer.Generic.Utf8.Deserialize<TestObject, AspNetCoreDefaultResolver<byte>>(body);
                    Assert.Equal(model, resultModel);
                }
            }
        }
    }
}