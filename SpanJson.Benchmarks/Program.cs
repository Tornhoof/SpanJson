using BenchmarkDotNet.Running;

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