using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Running;
using Perfolizer.Mathematics.Histograms;
using SpanJson.Resolvers;
using SpanJson.Shared.Models;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //// dotnet run -c Release -- --methods=ReadUtf8Char
            var switcher = new BenchmarkSwitcher(typeof(Program).Assembly);
            switcher.Run(args);
        }
    }
}