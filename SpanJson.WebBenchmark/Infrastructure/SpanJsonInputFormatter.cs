using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class SpanJsonInputFormatter : InputFormatter
    {
        private const string SpanJsonJsonMediaType = "application/spanjson+json";

        public SpanJsonInputFormatter()
        {
            SupportedMediaTypes.Add(SpanJsonJsonMediaType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                return InputFormatterResult.Success(await
                    JsonSerializer.NonGeneric.Utf8.DeserializeAsync(context.HttpContext.Request.Body, context.ModelType).ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError("JSON", ex.Message);
                return InputFormatterResult.Failure();
            }
        }
    }
}