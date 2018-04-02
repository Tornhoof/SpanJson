using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;

namespace SpanJson.Benchmarks
{
    public class MyConfig : ManualConfig
    {
        public MyConfig()
        {
            Add(Job.Default);
            Add(CsvMeasurementsExporter.Default);
            Add(MemoryDiagnoser.Default);
        }
    }
}
