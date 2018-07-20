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

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    public class BufferWriterBenchmark
    {
        private const string Input = "Hello World";

        [Params(1000, 10000, 100000)] public int Count;

        private List<string> _list;

        [GlobalSetup]
        public void Setup()
        {
            _list = Enumerable.Repeat(Input, Count).ToList();
        }



        [Benchmark]
        public void StringListUtf8()
        {
            var writer = new JsonWriter<byte>(256);
            StringUtf8ListFormatter.Default.Serialize(ref writer, _list, 0);
            ArrayPool<byte>.Shared.Return(writer.Data);
        }

        [Benchmark]
        public void StringListBufferWriter()
        {
            var buffer = new BufferWriter<byte>(Stream.Null);
            var writer = new JsonWriter<byte>(buffer);
            StringUtf8ListFormatter.Default.Serialize(ref writer, _list, 0);
            var pos = writer.Position;
            buffer.Flush(ref pos);
            buffer.Dispose();
        }
    }
}
