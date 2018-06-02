using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var sb = new SelectedBenchmarks();
            sb.ReadFalse();
            sb.ReadTrue();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}