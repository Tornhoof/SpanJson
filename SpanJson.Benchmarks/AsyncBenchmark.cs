using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public class AsyncBenchmark
    {
        private static ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        private static List<Answer> Answers;

        [Params(10, 100, 1000)] public int Count;

        [GlobalSetup]
        public void Setup()
        {
            Answers = Fixture.CreateMany<Answer>(Count).ToList();
            var output = JsonSerializer.Generic.Utf8.Serialize(Answers);
        }

        [Benchmark]
        public async Task SerializeAnswerList()
        {
            await JsonSerializer.Generic.Utf8.SerializeAsync(Answers, Stream.Null).ConfigureAwait(false);
        }
    }
}
