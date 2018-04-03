using BenchmarkDotNet.Attributes;
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
            Add(Job.ShortRun);
            Add(CsvMeasurementsExporter.Default);
            Add(MemoryDiagnoser.Default);
        }
    }
}
