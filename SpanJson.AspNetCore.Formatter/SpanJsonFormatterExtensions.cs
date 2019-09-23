using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SpanJson.AspNetCore.Formatter
{
    /// <summary>
    /// Extensions to use SpanJson in UTF8 mode as the default serializer
    /// </summary>
    public static class SpanJsonFormatterExtensions
    {
        /// <summary>
        /// Uses SpanJson in UTF8 mode with custom formatter resolver
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonCustom<TResolver>(this IMvcCoreBuilder mvcCoreBuilder)
            where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            Configure<TResolver>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode with custom formatter resolver
        /// </summary>
        public static IMvcBuilder AddSpanJsonCustom<TResolver>(this IMvcBuilder mvcBuilder) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            Configure<TResolver>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode with ASP.NET Core 3.0 defaults (IncludeNull, CamelCase, Enum as Ints)
        /// </summary>
        public static IMvcCoreBuilder AddSpanJson(this IMvcCoreBuilder mvcBuilder)
        {
            Configure<AspNetCoreDefaultResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode with ASP.NET Core 3.0 defaults (IncludeNull, CamelCase, Enum as Ints)
        /// </summary>
        public static IMvcBuilder AddSpanJson(this IMvcBuilder mvcBuilder)
        {
            Configure<AspNetCoreDefaultResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        private static void Configure<TResolver>(IServiceCollection serviceCollection) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            serviceCollection.Configure<MvcOptions>(config =>
            {
                config.OutputFormatters.Clear();
                config.InputFormatters.Clear();
                //TODO config.OutputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonOutputFormatter>();
                //TODO config.InputFormatters.RemoveType<Microsoft.AspNetCore.Mvc.Formatters.SystemTextJsonInputFormatter>();
                config.InputFormatters.Add(new SpanJsonInputFormatter<TResolver>());
                config.OutputFormatters.Add(new SpanJsonOutputFormatter<TResolver>());
            });
        }
    }
}