using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.AspNetCore.Formatter
{
    public class SpanJsonInputFormatter<TResolver> : InputFormatter where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
    {
        public SpanJsonInputFormatter()
        {
            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/json");
            SupportedMediaTypes.Add("application/*+json");
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                var model = await JsonSerializer.NonGeneric.Utf8.DeserializeAsync<TResolver>(context.HttpContext.Request.Body, context.ModelType)
                    .ConfigureAwait(false);
                if (model == null && !context.TreatEmptyInputAsDefaultValue)
                {
                    return InputFormatterResult.NoValue();
                }

                return InputFormatterResult.Success(model);
            }
            catch (Exception ex)
            {
                context.ModelState.AddModelError("JSON", ex.Message);
                return InputFormatterResult.Failure();
            }
        }
    }
}