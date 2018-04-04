using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var formatter = new NumberFormatter();
            //Span<char> span = stackalloc char[100];
            //int pos = 0;
            //formatter.WriteUInt64Next(span, ref pos, 123456789);
            BenchmarkRunner.Run<ModelBenchmark>();
        }
    }
}