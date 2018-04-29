using System.Text;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class JilOutputFormatter : TextOutputFormatter
    {
        private const string JilJsonMediaType = "application/jil+json";

        public JilOutputFormatter()
        {
            SupportedMediaTypes.Add(JilJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }


        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context.Object != null)
            {
                using (var textWriter = context.WriterFactory(context.HttpContext.Response.Body, selectedEncoding))
                {
                    JSON.SerializeDynamic(context.Object, textWriter, Options.ISO8601ExcludeNullsIncludeInherited);
                }
            }

            return Task.CompletedTask;
        }
    }
}