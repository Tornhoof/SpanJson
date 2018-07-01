using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SpanJson.Resolvers;

namespace SpanJson.AspNetCore.Formatter
{
    /// <summary>
    /// Extensions to use SpanJson in UTF8 mode as the default serializer
    /// </summary>
    public static class SpanJsonFormatterExtensions
    {
        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and excluding null members
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonExcludeNullsOriginalCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<ExcludeNullsOriginalCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and excluding null members
        /// </summary>
        public static IMvcBuilder AddSpanJsonExcludeNullsOriginalCase(this IMvcBuilder mvcBuilder)
        {
            Configure<ExcludeNullsOriginalCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and including null members
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonIncludeNullsOriginalCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<IncludeNullsOriginalCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and including null members
        /// </summary>
        public static IMvcBuilder AddSpanJsonIncludeNullsOriginalCase(this IMvcBuilder mvcBuilder)
        {
            Configure<IncludeNullsOriginalCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and excluding null members
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonExcludeNullsCamelCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<ExcludeNullsCamelCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and excluding null members
        /// </summary>
        public static IMvcBuilder AddSpanJsonExcludeNullsCamelCase(this IMvcBuilder mvcBuilder)
        {
            Configure<ExcludeNullsCamelCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and including null members
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonIncludeNullsCamelCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<IncludeNullsCamelCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and including null members
        /// </summary>
        public static IMvcBuilder AddSpanJsonIncludeNullsCamelCase(this IMvcBuilder mvcBuilder)
        {
            Configure<IncludeNullsCamelCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode with custom formatter resolver
        /// </summary>
        public static IMvcCoreBuilder AddSpanJsonCustom<TResolver>(this IMvcCoreBuilder mvcCoreBuilder) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
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

        private static void Configure<TResolver>(IServiceCollection serviceCollection) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
        {
            serviceCollection.Configure<MvcOptions>(config =>
            {
                config.InputFormatters.Clear();
                config.OutputFormatters.Clear();
                config.InputFormatters.Add(new SpanJsonInputFormatter<TResolver>());
                config.OutputFormatters.Add(new SpanJsonOutputFormatter<TResolver>());
            });
        }

    }
}
