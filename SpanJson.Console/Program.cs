using SpanJson.Shared.Models;
using System;
using System.IO;
using System.Reflection;

namespace SpanJson.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            var serialized = GetPayload();
            System.Console.WriteLine(serialized.Length);
            var deserialized = SpanJson.JsonSerializer.Generic.Utf16.Deserialize<Answer>(serialized);
        }

        private static string GetPayload()
        {
            using (var stream = Assembly.GetAssembly(typeof(Program)).GetManifestResourceStream(typeof(Program), "Payload.json"))
            {
                using (var tr = new StreamReader(stream))
                {
                    return tr.ReadToEnd();
                }
            }
        }
    }
}
