using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace SpanJson.AspNetCore.Formatter.Tests
{
    public class ExtensionTests
    {
        private static void TestCoreBuilderInternal<TResolver>(Action<IMvcCoreBuilder> useAction)
            where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var options = GetOptions<MvcOptions>(services =>
            {
                var mock = new Mock<IMvcCoreBuilder>();
                mock.Setup(a => a.Services).Returns(services);
                useAction(mock.Object);
            });
            Assert.NotNull(options);
            Assert.Single(options.InputFormatters);
            Assert.Single(options.OutputFormatters);
            Assert.IsType<SpanJsonInputFormatter<TResolver>>(options.InputFormatters[0]);
            Assert.IsType<SpanJsonOutputFormatter<TResolver>>(options.OutputFormatters[0]);
        }

        private static void TestBuilderInternal<TResolver>(Action<IMvcBuilder> useAction) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            var options = GetOptions<MvcOptions>(services =>
            {
                var mock = new Mock<IMvcBuilder>();
                mock.Setup(a => a.Services).Returns(services);
                useAction(mock.Object);
            });
            Assert.NotNull(options);
            Assert.Single(options.InputFormatters);
            Assert.Single(options.OutputFormatters);
            Assert.IsType<SpanJsonInputFormatter<TResolver>>(options.InputFormatters[0]);
            Assert.IsType<SpanJsonOutputFormatter<TResolver>>(options.OutputFormatters[0]);
        }

        private static T GetOptions<T>(Action<IServiceCollection> action = null)
            where T : class, new()
        {
            var serviceProvider = GetServiceProvider(action);
            return serviceProvider.GetRequiredService<IOptions<T>>().Value;
        }

        private static IServiceProvider GetServiceProvider(Action<IServiceCollection> action = null)
        {
            var loggerMock = new Mock<ILoggerFactory>();
            loggerMock.Setup(a => a.CreateLogger(It.IsAny<string>())).Returns(new Mock<ILogger>().Object);
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(new ApplicationPartManager());
            serviceCollection.AddMvc();
            serviceCollection
                .AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>()
                .AddTransient(provider => loggerMock.Object);

            action?.Invoke(serviceCollection);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider;
        }

        [Fact]
        public void Builder()
        {
            TestBuilderInternal<CustomResolver<byte>>(a => a.AddSpanJsonCustom<CustomResolver<byte>>());
            TestBuilderInternal<AspNetCoreDefaultResolver<byte>>(a => a.AddSpanJsonCustom<AspNetCoreDefaultResolver<byte>>());
        }

        [Fact]
        public void CoreBuilder()
        {
            TestCoreBuilderInternal<CustomResolver<byte>>(a => a.AddSpanJsonCustom<CustomResolver<byte>>());
            TestCoreBuilderInternal<AspNetCoreDefaultResolver<byte>>(a => a.AddSpanJsonCustom<AspNetCoreDefaultResolver<byte>>());
        }
    }
}