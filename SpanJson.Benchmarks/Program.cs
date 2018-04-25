using System.Text;
using BenchmarkDotNet.Running;

namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
var sb = new SelectedBenchmarks();
            sb.SerializeUInt64WithSpanJsonSerializer();
            BenchmarkRunner.Run<SelectedBenchmarks>();
        }
    }
}