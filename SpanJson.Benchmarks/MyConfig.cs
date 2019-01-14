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
            Add(Job.Default.WithUnrollFactor(2).With(BenchmarkDotNet.Toolchains.CsProj.CsProjCoreToolchain.NetCoreApp30));
            Add(MemoryDiagnoser.Default);
            Set(new DefaultOrderer(SummaryOrderPolicy.Default, MethodOrderPolicy.Alphabetical));
        }
    }
}