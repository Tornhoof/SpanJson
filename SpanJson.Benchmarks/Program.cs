namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<ModelBenchmark>();
        }
    }
}