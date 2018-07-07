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
        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();
        private List<Answer> _answers;
        private byte[] _data;

        [Params(10, 100, 1000, 10000)] public int Count;

        [GlobalSetup]
        public void Setup()
        {
            _answers = Fixture.CreateMany<Answer>(Count).ToList();
            _data = JsonSerializer.Generic.Utf8.Serialize(_answers);
            DeserializeAnswerListAsync().GetAwaiter().GetResult();
        }


        [Benchmark]
        public async Task SerializeAnswerListAsync()
        {
            await JsonSerializer.Generic.Utf8.SerializeAsync(_answers, Stream.Null).ConfigureAwait(false);
        }

        [Benchmark]
        public void SerializeAnswerList()
        {
            JsonSerializer.Generic.Utf8.Serialize(_answers);
        }

        [Benchmark]
        public async Task DeserializeAnswerListAsync()
        {
            using (var ms = new MemoryStream(_data, false))
            {
                using (var tr = new StreamReader(ms, Encoding.UTF8))
                {

                    var result = await JsonSerializer.Generic.Utf16.DeserializeAsync<List<Answer>>(tr).ConfigureAwait(false);
                }
            }
        }

        [Benchmark]
        public void DeserializeAnswerList()
        {
            JsonSerializer.Generic.Utf8.Serialize(_answers);
        }
    }
}
