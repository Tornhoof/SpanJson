using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    //[ShortRunJob]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly string AnswerSerializedString =
            SpanJsonSerializer.Serialize(ExpressionTreeFixture.Create<Answer>());

        private static readonly byte[] AnswerSerializedByteArray =
            Encoding.UTF8.GetBytes(AnswerSerializedString);


        private static readonly JilSerializer JilSerializer = new JilSerializer();


        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerSerializedString);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithJilSerializer()
        {
            return JilSerializer.Deserialize<Answer>(AnswerSerializedString);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Answer>(AnswerSerializedByteArray);
        }
    }
}