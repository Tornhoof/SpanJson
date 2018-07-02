using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.AspNetCore.Formatter
{
    public class SpanJsonOutputFormatter<TResolver> : OutputFormatter where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
    {
        public SpanJsonOutputFormatter()
        {
            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/json");
            SupportedMediaTypes.Add("application/*+json");
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context.Object != null)
            {
                return JsonSerializer.NonGeneric.Utf8.SerializeAsync<TResolver>(context.Object, context.HttpContext.Response.Body).AsTask();
            }

            return Task.CompletedTask;
        }
    }
}