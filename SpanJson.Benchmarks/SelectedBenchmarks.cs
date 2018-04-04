using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    //[ShortRunJob]
    [DisassemblyDiagnoser(true, recursiveDepth: 2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly MobileBadgeAward MobileBadgeAwardInput =
            ExpressionTreeFixture.Create<MobileBadgeAward>();

        private static readonly long? NullableLong = 5000;

        //[Benchmark]
        //public string SerializeWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(NullableLong);
        //}

        //[Benchmark]
        //public string SerializeSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(NullableLong);
        //}

        //[Benchmark]
        //public byte[] SerializeWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(NullableLong);
        //}
    }
}