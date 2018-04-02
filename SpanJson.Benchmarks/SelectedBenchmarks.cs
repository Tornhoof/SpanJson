using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [DisassemblyDiagnoser(true, recursiveDepth: 4)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly AccessToken AccessTokenInput = ExpressionTreeFixture.Create<AccessToken>();

        //[Benchmark]
        //public string SerializeAccessTokenWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(AccessTokenInput);
        //}

        [Benchmark]
        public string SerializeAccessTokenWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(AccessTokenInput);
        }

        //[Benchmark]
        //public byte[] SerializeAccessTokenWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(AccessTokenInput);
        //}
    }
}
