using System.IO;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Answer Answer = ExpressionTreeFixture.Create<Answer>();
        private static readonly string AnswerSerializedString =            
            SpanJsonSerializer.Serialize(Answer);

        private static readonly byte[] AnswerSerializedByteArray =
            Encoding.UTF8.GetBytes(AnswerSerializedString);


        private static readonly JilSerializer JilSerializer = new JilSerializer();


        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();
        private static readonly StringBuilder StringBuilder = new StringBuilder();

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerSerializedString);
        }

        [Benchmark]
        public async ValueTask<Answer> DeserializeAnswerWithSpanJsonSerializerAsync()
        {
            using (var tr = new StringReader(AnswerSerializedString))
            {
                return await JsonSerializer.Generic.DeserializeAsync<Answer>(tr);
            }
        }

        //[Benchmark]
        //public Answer DeserializeAnswerWithJilSerializer()
        //{
        //    return JilSerializer.Deserialize<Answer>(AnswerSerializedString);
        //}

        //[Benchmark]
        //public Answer DeserializeAnswerWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Deserialize<Answer>(AnswerSerializedByteArray);
        //}

        [Benchmark]
        public string SerializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Answer);
        }

        [Benchmark]
        public async ValueTask<string> SerializeAnswerWithSpanJsonSerializerAsync()
        {
            StringBuilder.Clear();
            using (var tw = new StringWriter(StringBuilder))
            {
                await JsonSerializer.Generic.SerializeAsync(Answer, tw);

            }
            return StringBuilder.ToString();
        }

        //[Benchmark]
        //public string SerializeAnswerWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(Answer);
        //}

        //[Benchmark]
        //public byte[] SerializeAnswerWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(Answer);
        //}
    }
}