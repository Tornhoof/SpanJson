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
        public static IMvcCoreBuilder UseSpanJsonExcludeNullsOriginalCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<ExcludeNullsOriginalCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and excluding null members
        /// </summary>
        public static IMvcBuilder UseSpanJsonExcludeNullsOriginalCase(this IMvcBuilder mvcBuilder)
        {
            Configure<ExcludeNullsOriginalCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and including null members
        /// </summary>
        public static IMvcCoreBuilder UseSpanJsonIncludeNullsOriginalCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<IncludeNullsOriginalCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with original case and including null members
        /// </summary>
        public static IMvcBuilder UseSpanJsonIncludeNullsOriginalCase(this IMvcBuilder mvcBuilder)
        {
            Configure<IncludeNullsOriginalCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and excluding null members
        /// </summary>
        public static IMvcCoreBuilder UseSpanJsonExcludeNullsCamelCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<ExcludeNullsCamelCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and excluding null members
        /// </summary>
        public static IMvcBuilder UseSpanJsonExcludeNullsCamelCase(this IMvcBuilder mvcBuilder)
        {
            Configure<ExcludeNullsCamelCaseResolver<byte>>(mvcBuilder.Services);
            return mvcBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and including null members
        /// </summary>
        public static IMvcCoreBuilder UseSpanJsonIncludeNullsCamelCase(this IMvcCoreBuilder mvcCoreBuilder)
        {
            Configure<IncludeNullsCamelCaseResolver<byte>>(mvcCoreBuilder.Services);
            return mvcCoreBuilder;
        }

        /// <summary>
        /// Uses SpanJson in UTF8 mode to format with Camel case and including null members
        /// </summary>
        public static IMvcBuilder UseSpanJsonIncludeNullsCamelCase(this IMvcBuilder mvcBuilder)
        {
            Configure<IncludeNullsCamelCaseResolver<byte>>(mvcBuilder.Services);
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
