using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public class HelloWorldMessageBenchmark
    {
        public struct JsonMessage
        {
            public string message { get; set; }
        }

        private static readonly JsonMessage Message = new JsonMessage {message = "Hello, World!"};


        [Benchmark]
        public string SerializeUtf16()
        {
            return JsonSerializer.Generic.Utf16.Serialize(Message);
        }

        [Benchmark]
        public byte[] SerializeUtf8()
        {
            return JsonSerializer.Generic.Utf8.Serialize(Message);
        }

        [Benchmark]
        public void SerializeUtf16Unsafe()
        {
            var buffer = JsonSerializer.Generic.Utf16.SerializeUnsafe(Message);
            ArrayPool<char>.Shared.Return(buffer.Array);
        }

        [Benchmark]
        public void SerializeUtf8Unsafe()
        {
            var buffer = JsonSerializer.Generic.Utf8.SerializeUnsafe(Message);
            ArrayPool<byte>.Shared.Return(buffer.Array);
        }

        [Benchmark]
        public ValueTask SerializeUtf16Async()
        {
            using (var tw = new StreamWriter(Stream.Null))
            {
                return JsonSerializer.Generic.Utf16.SerializeAsync(Message, tw);
            }
        }

        [Benchmark]
        public Task SerializeUtf8Async()
        {
            return JsonSerializer.Generic.Utf8.SerializeAsync(Message, Stream.Null).AsTask();
        }
    }
}
