using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace SpanJson.Benchmarks
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core50));
            AddJob(Job.ShortRun.WithRuntime(CoreRuntime.Core60));
            AddDiagnoser(MemoryDiagnoser.Default);
            Orderer = new DefaultOrderer(SummaryOrderPolicy.Default, MethodOrderPolicy.Alphabetical);
        }
    }
}