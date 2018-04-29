using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class SpanJsonOutputFormatter : TextOutputFormatter
    {
        private const string SpanJsonJsonMediaType = "application/spanjson+json";

        public SpanJsonOutputFormatter()
        {
            SupportedMediaTypes.Add(SpanJsonJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }


        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context.Object != null)
            {
                await JsonSerializer.NonGeneric.Utf8.SerializeAsync(context.Object, context.HttpContext.Response.Body);
            }
        }
    }
}