using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    //[ShortRunJob]
    [DisassemblyDiagnoser(true, recursiveDepth:2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly MobileBadgeAward MobileBadgeAwardInput = ExpressionTreeFixture.Create<MobileBadgeAward>();

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

        const int Int32Value = 12345678;

        [Benchmark]
        public string SerializeNumberFormatInt()
        {
            Span<char> span = stackalloc char[20];
            var writer = new JsonWriter(span);
            writer.WriteInt32Old(Int32Value);
            return writer.ToString();
        }

        [Benchmark]
        public string SerializeUtf8JsonInt()
        {
            Span<char> span = stackalloc char[20];
            var writer = new JsonWriter(span);
            writer.MyWriteInt32(Int32Value);
            return writer.ToString();
        }
    }
}
