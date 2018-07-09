using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {

            var alb = new AsyncListBenchmark();
            alb.Count = 10000;
            alb.Setup();
            alb.AsyncStringListFileStream().GetAwaiter().GetResult();

            // dotnet run -c Release -- --methods=ReadUtf8Char
            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            switcher.Run(args);
        }
    }
}