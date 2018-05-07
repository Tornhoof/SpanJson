using System;
using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var sb = new SelectedBenchmarks();
            sb.ReadStringUtf8();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}