using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.AspNetCore.Formatter
{
    public class SpanJsonOutputFormatter<TResolver> : TextOutputFormatter where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
    {
        public SpanJsonOutputFormatter()
        {
            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/json");
            SupportedMediaTypes.Add("application/*+json");
            SupportedEncodings.Add(Encoding.UTF8);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding encoding)
        {
            if (context.Object != null)
            {
                var valueTask = JsonSerializer.NonGeneric.Utf8.SerializeAsync<TResolver>(context.Object, context.HttpContext.Response.Body);
                return valueTask.IsCompletedSuccessfully ? Task.CompletedTask : valueTask.AsTask();
            }

            return Task.CompletedTask;
        }
    }
}