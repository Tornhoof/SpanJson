using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class SpanJsonOutputFormatter : OutputFormatter
    {
        private const string SpanJsonJsonMediaType = "application/spanjson+json";

        public SpanJsonOutputFormatter()
        {
            SupportedMediaTypes.Add(SpanJsonJsonMediaType);
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context.Object != null)
            {
                await JsonSerializer.NonGeneric.Utf8.SerializeAsync(context.Object, context.HttpContext.Response.Body).ConfigureAwait(false);
            }
        }
    }
}