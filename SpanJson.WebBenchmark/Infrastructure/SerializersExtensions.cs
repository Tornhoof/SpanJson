using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public static class SerializersExtensions
    {
        public static IMvcCoreBuilder AddSerializers(this IMvcCoreBuilder builder)
        {
            builder.Services.Configure<MvcOptions>(config =>
            {
                config.InputFormatters.Clear();
                config.OutputFormatters.Clear();
                config.InputFormatters.Add(new JilInputFormatter());
                config.OutputFormatters.Add(new JilOutputFormatter());
                config.InputFormatters.Add(new SpanJsonInputFormatter());
                config.OutputFormatters.Add(new SpanJsonOutputFormatter());
                config.InputFormatters.Add(new Utf8JsonInputFormatter());
                config.OutputFormatters.Add(new Utf8JsonOutputFormatter());
            });
            return builder;
        }
    }
}