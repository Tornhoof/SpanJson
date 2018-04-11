using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Models;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}