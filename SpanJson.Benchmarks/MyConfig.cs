using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace SpanJson.Benchmarks
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Add(Job.Default);
            Add(MemoryDiagnoser.Default);
            Set(new DefaultOrderProvider(SummaryOrderPolicy.Default, MethodOrderPolicy.Alphabetical));
        }
    }
}