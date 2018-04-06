using System.Collections.Generic;
using System.Text;
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


        private static readonly string AccessTokenSerializedString =
            SpanJsonSerializer.Serialize(ExpressionTreeFixture.Create<AccessToken>());

        private static readonly byte[] AccessTokenSerializedByteArray = Encoding.UTF8.GetBytes(AccessTokenSerializedString);

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<AccessToken>(AccessTokenSerializedString);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithJilSerializer()
        {
            return JilSerializer.Deserialize<AccessToken>(AccessTokenSerializedString);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<AccessToken>(AccessTokenSerializedByteArray);
        }
    }
}