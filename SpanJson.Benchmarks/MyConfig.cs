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
            AddJob(Job.Default.WithUnrollFactor(2));
            AddDiagnoser(MemoryDiagnoser.Default);
            Orderer = new DefaultOrderer(SummaryOrderPolicy.Default, MethodOrderPolicy.Alphabetical);
        }
    }
}