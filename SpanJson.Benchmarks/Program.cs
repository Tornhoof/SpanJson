using System;
using SpanJson;
using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {

            var input = new Input {Text = "Hello World"};

            var serialized = JsonSerializer.Generic.Utf16.Serialize(input);

            var deserialized = JsonSerializer.Generic.Utf16.Deserialize<Input>(serialized);
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }

    public class Input
    {
        public string Text { get; set; }
    }
}