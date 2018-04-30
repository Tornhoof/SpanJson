using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;
using Utf8Json.Resolvers;

namespace SpanJson.WebBenchmark.Infrastructure
{
    public class Utf8JsonInputFormatter : InputFormatter
    {
        private const string Utf8JsonJsonMediaType = "application/utf8json+json";

        public Utf8JsonInputFormatter()
        {
            SupportedMediaTypes.Add(Utf8JsonJsonMediaType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
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