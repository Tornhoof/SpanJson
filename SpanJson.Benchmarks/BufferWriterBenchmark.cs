using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using SpanJson.Formatters;
using SpanJson.Resolvers;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class BufferWriterBenchmark
    {
        private const string Input = "Hello World";

        private static readonly ExpressionTreeFixture Fixture = new ExpressionTreeFixture();

        [Params(100, 1000, 10000)] public int Count;

        private List<string> _stringList;
        private List<Answer> _answerList;

        [GlobalSetup]
        public void Setup()
        {
            _stringList = Enumerable.Repeat(Input, Count).ToList();
            _answerList = Fixture.CreateMany<Answer>(Count).ToList();
        }



        //[Benchmark]
        //public void StringListUtf8()
        //{
        //    var writer = new JsonWriter<byte>(256);
        //    StringUtf8ListFormatter.Default.Serialize(ref writer, _stringList, 0);
        //    ArrayPool<byte>.Shared.Return(writer.Data);
        //}

        //[Benchmark]
        //public void StringListBufferWriter()
        //{
        //    var buffer = new BufferWriter<byte>(Stream.Null);
        //    var writer = new JsonWriter<byte>(buffer);
        //    StringUtf8ListFormatter.Default.Serialize(ref writer, _stringList, 0);
        //    var pos = writer.Position;
        //    buffer.Flush(ref pos);
        //    buffer.Dispose();
        //}

        [Benchmark]
        public void AnswerUtf8()
        {
            var writer = new JsonWriter<byte>(256);
            ListFormatter<List<Answer>, Answer, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _answerList, 0);
            ArrayPool<byte>.Shared.Return(writer.Data);
        }

        [Benchmark]
        public void AnswerUtf8BufferWriter()
        {
            var buffer = new BufferWriter<byte>(Stream.Null);
            var writer = new JsonWriter<byte>(buffer);
            ListFormatter<List<Answer>, Answer, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, _answerList, 0);
            var pos = writer.Position;
            buffer.Flush(ref pos);
            buffer.Dispose();
        }
    }
}
