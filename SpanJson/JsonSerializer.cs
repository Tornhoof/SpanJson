using System;
using SpanJson.Formatters;

namespace SpanJson
{
    public static class JsonSerializer
    {
        public static string Serialize<T>(T input) where T: class
        {
            var complexClassFormatter = FormatterHelper.GetDefaultFormatter<T>();
            Span<char> span = stackalloc char[100];
            var jsonWriter = new JsonWriter(span);
            complexClassFormatter.Serialize(ref jsonWriter, input, null);
            var result =  jsonWriter.ToString();
            jsonWriter.Dispose();
            return result;
        }
    }
}