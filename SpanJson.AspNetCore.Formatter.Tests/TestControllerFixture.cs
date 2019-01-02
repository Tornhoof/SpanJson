using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public sealed class TestControllerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }


        public TestControllerFixture()
        {
            var builder = new WebHostBuilder().UseStartup<Startup>();
            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
            BaseAddress = _testServer.BaseAddress;
        }

        public Uri BaseAddress { get; }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}