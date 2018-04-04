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

        const float FloatValue = 1.23456f;
        const double DoubleValue = 1.23456d;
        const decimal DecimalValue = 1.23456m;

        [Benchmark]
        public string SerializeNumberFormatDoubleSpanJson()
        {
            return SpanJsonSerializer.Serialize(DoubleValue);
        }

        [Benchmark]
        public byte[] SerializeNumberFormatDoubleUtf8Json()
        {
            return Utf8JsonSerializer.Serialize(DoubleValue);
        }

        [Benchmark]
        public string SerializeNumberFormatFloatSpanJson()
        {
            return SpanJsonSerializer.Serialize(FloatValue);
        }

        [Benchmark]
        public byte[] SerializeNumberFormatFloatUtf8Json()
        {
            return Utf8JsonSerializer.Serialize(FloatValue);
        }

        [Benchmark]
        public string SerializeNumberFormatDecimalSpanJson()
        {
            return SpanJsonSerializer.Serialize(DecimalValue);
        }

        [Benchmark]
        public byte[] SerializeNumberFormatDecimalUtf8Json()
        {
            return Utf8JsonSerializer.Serialize(DecimalValue);
        }
    }
}
