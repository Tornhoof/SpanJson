using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace SpanJson.AspNetCore.Formatter
{
    public class SpanJsonInputFormatter<TResolver> : TextInputFormatter where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
    {
        public SpanJsonInputFormatter()
        {
            SupportedMediaTypes.Add("application/json");
            SupportedMediaTypes.Add("text/json");
            SupportedMediaTypes.Add("application/*+json");
            SupportedEncodings.Add(UTF8EncodingWithoutBOM);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
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