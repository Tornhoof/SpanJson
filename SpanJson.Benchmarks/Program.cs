using System;
using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now.ToString("O"));
            Console.WriteLine(DateTime.UtcNow.ToString("O"));
            Console.WriteLine(DateTimeOffset.Now.ToString("O"));
            Console.WriteLine(DateTimeOffset.UtcNow.ToString("O"));
            Console.WriteLine(new DateTimeOffset(2017, 6, 12, 5, 30, 45, TimeSpan.Zero).ToString("O"));

            var sb = new SelectedBenchmarks();
            sb.SerializeDateTimeOffsetWithSpanJsonSerializer();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}