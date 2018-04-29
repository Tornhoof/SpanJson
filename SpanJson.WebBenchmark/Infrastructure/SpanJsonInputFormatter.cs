using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class SpanJsonInputFormatter : TextInputFormatter
    {
        private const string SpanJsonJsonMediaType = "application/spanjson+json";

        public SpanJsonInputFormatter()
        {
            SupportedMediaTypes.Add(SpanJsonJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            try
            {
                return await InputFormatterResult.SuccessAsync(await
                    JsonSerializer.NonGeneric.Utf8.DeserializeAsync(context.HttpContext.Request.Body, context.ModelType));
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError("JSON", ex.Message);
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}