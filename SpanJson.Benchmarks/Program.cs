using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Models;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var b = new SelectedBenchmarks();
            var x = b.DeserializeAccessTokenWithSpanJsonSerializer();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}