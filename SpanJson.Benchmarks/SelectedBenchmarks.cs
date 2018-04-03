using System;
using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly AccessToken AccessTokenInput = ExpressionTreeFixture.Create<AccessToken>();
        private static readonly ShallowUser ShallowUserInput = ExpressionTreeFixture.Create<ShallowUser>();

        //[Benchmark]
        //public string SerializeShallowUserWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(ShallowUserInput);
        //}

        [Benchmark]
        public string SerializeShallowUserSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ShallowUserInput);
        }

        //[Benchmark]
        //public byte[] SerializeShallowUserWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(ShallowUserInput);
        //}
    }
}
