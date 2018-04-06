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


        private static readonly MobileBadgeAward MobileBadgeAwardInput =
            ExpressionTreeFixture.Create<MobileBadgeAward>();

        private static readonly long? NullableLong = 5000;
        private static readonly string ListInput = "[\"Hello\",\"World\",\"Universe\"]";
        private static readonly byte[] ListInputArray = Encoding.UTF8.GetBytes(ListInput);

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

        ////[Benchmark]
        ////public byte[] SerializeWithUtf8JsonSerializer()
        ////{
        ////    return Utf8JsonSerializer.Serialize(NullableLong);
        ////}

        [Benchmark]
        public List<string> DeserializeWithJilDeserializer()
        {
            return JilSerializer.Deserialize<List<string>>(ListInput);
        }

        [Benchmark]
        public List<string> DeserializeSpanJsonDeserializer()
        {
            return SpanJsonSerializer.Deserialize<List<string>>(ListInput);
        }

        [Benchmark]
        public List<string> DeserializeWithUtf8JsonDeserializer()
        {
            return Utf8JsonSerializer.Deserialize<List<string>>(ListInputArray);
        }
    }
}