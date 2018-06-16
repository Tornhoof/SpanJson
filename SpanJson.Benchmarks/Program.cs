using System;
using System.IO;
using System.Linq;
using System.Reflection;
using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //var types = Assembly.GetExecutingAssembly().GetTypes().Where(a => a.IsClass && !a.IsAbstract && a.Namespace.StartsWith("SpanJson.Benchmarks.Models")).ToArray();
            //var utf16Output = CodeGenerator<char, ExcludeNullsOriginalCaseResolver<char>>.Generate(types);
            //File.WriteAllText("utf16.cs", utf16Output);
            //var utf8Output = CodeGenerator<Byte, ExcludeNullsOriginalCaseResolver<Byte>>.Generate(types);
            //File.WriteAllText("utf8.cs", utf8Output);
            InitSpecial.Init();
            //  var sb = new SelectedBenchmarks();
            //  sb.DeserializeAnswerWithSpanJsonSerializer();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}