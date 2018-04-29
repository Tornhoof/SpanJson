using System;
using System.Text;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class JilInputFormatter : TextInputFormatter
    {
        private const string JilJsonMediaType = "application/jil+json";

        public JilInputFormatter()
        {
            SupportedMediaTypes.Add(JilJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            using (var reader = context.ReaderFactory(context.HttpContext.Request.Body, encoding))
            {
                try
                {
                    return await InputFormatterResult.SuccessAsync(JSON.Deserialize(reader, context.ModelType, Options.ISO8601ExcludeNullsIncludeInherited));
                }
                catch (Exception ex)
                {
                    context.ModelState.AddModelError("JSON", ex.Message);
                    return await InputFormatterResult.FailureAsync();
                }
            }
        }
    }
}