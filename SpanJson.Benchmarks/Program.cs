using System;
using BenchmarkDotNet.Running;
using SpanJson.Helpers;

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