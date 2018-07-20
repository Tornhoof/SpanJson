using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var bwb = new BufferWriterBenchmark();
            bwb.Count = 10000;
            bwb.Setup();
            bwb.StringListUtf8();
            bwb.StringListBufferWriter();
            // dotnet run -c Release -- --methods=ReadUtf8Char
            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            switcher.Run(args);
        }
    }
}