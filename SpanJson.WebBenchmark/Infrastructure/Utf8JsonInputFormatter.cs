using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Utf8Json.Resolvers;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class Utf8JsonInputFormatter : TextInputFormatter
    {
        private const string Utf8JsonJsonMediaType = "application/utf8json+json";

        public Utf8JsonInputFormatter()
        {
            SupportedMediaTypes.Add(Utf8JsonJsonMediaType);
            SupportedEncodings.Add(new UTF8Encoding(false, true));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            try
            {
                return await InputFormatterResult.SuccessAsync(await Utf8Json.JsonSerializer.NonGeneric.DeserializeAsync(context.ModelType,
                    context.HttpContext.Request.Body, StandardResolver.ExcludeNull));
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError("JSON", ex.Message);
                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}