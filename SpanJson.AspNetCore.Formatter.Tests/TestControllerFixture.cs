using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public sealed class TestControllerFixture : IDisposable, IAsyncLifetime
    {
        private readonly IHost _host;
        private readonly TestServer _testServer;


        public TestControllerFixture()
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseTestServer();
                });
            _host = builder.Build();
            _testServer = _host.GetTestServer();
            BaseAddress = _testServer.BaseAddress;
        }

        public HttpClient Client { get; private set; }


        public Uri BaseAddress { get; }

        public async Task InitializeAsync()
        {
            await _host.StartAsync();
            Client = _testServer.CreateClient();
        }


        public async Task DisposeAsync()
        {
            await _host.StopAsync();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
            _host.Dispose();
        }
    }
}