using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class Utf8JsonOutputFormatter : TextOutputFormatter
    {
        private const string Utf8JsonJsonMediaType = "application/Utf8json+json";

        public Utf8JsonOutputFormatter()
        {
            SupportedMediaTypes.Add(Utf8JsonJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }


        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context.Object != null)
            {
                return Utf8Json.JsonSerializer.NonGeneric.SerializeAsync(context.HttpContext.Response.Body, context.Object);
            }

            return Task.CompletedTask;
        }
    }
}