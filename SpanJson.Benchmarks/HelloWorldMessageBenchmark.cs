using System;
using System.Buffers;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using SpanJson.Formatters;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    [DisassemblyDiagnoser()]
    public class HelloWorldMessageBenchmark
    {
        public struct JsonMessage
        {
            public string message { get; set; }
        }

        private const string Message = "Hello, World!";



        [Benchmark]
        public byte[] SerializeUtf8()
        {
            return JsonSerializer.Generic.Utf8.Serialize(Message);
        }

        [Benchmark]
        public void SerializeUtf8Unsafe()
        {
            var buffer = JsonSerializer.Generic.Utf8.SerializeToArrayPool(Message);
            ArrayPool<byte>.Shared.Return(buffer.Array);
        }


        [Benchmark]
        public Task SerializeUtf8Async()
        {
            return JsonSerializer.Generic.Utf8.SerializeAsync(Message, Stream.Null).AsTask();
        }

        [Benchmark]
        public Task SerializeUtf8JsonAsync()
        {
            return Utf8Json.JsonSerializer.SerializeAsync(Stream.Null, Message);
        }

        [Benchmark]
        public void SerializeUtf8JsonUnsafe()
        {
            Utf8Json.JsonSerializer.SerializeUnsafe(Message);
        }

        [Benchmark]
        public void SerializeUtf8Json()
        {
            Utf8Json.JsonSerializer.Serialize(Message);
        }


        private static readonly byte[] NameByteArray = Encoding.UTF8.GetBytes("\"message\":");

        [Benchmark]
        public void WriteMessageDirectlyUtf8()
        {
            var jsonWriter = new JsonWriter<byte>(64);
            jsonWriter.WriteUtf8BeginObject();
            jsonWriter.WriteUtf8Verbatim(7306916068917079330UL, 14882);
            jsonWriter.WriteUtf8String(Message);
            jsonWriter.WriteUtf8EndObject();
        }

        [Benchmark]
        public void WriteMessageDirectlyUtf8JsonUtf8()
        {
            var buffer = ArrayPool<byte>.Shared.Rent(64);
            var jsonWriter = new Utf8Json.JsonWriter(buffer);
            jsonWriter.WriteBeginObject();
            jsonWriter.WriteRaw(NameByteArray);
            jsonWriter.WriteString("Hello, World!");
            jsonWriter.WriteEndObject();
        }
    }
}