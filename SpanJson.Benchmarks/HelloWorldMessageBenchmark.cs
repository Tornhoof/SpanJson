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


        //[Benchmark]
        //public string SerializeUtf16()
        //{
        //    return JsonSerializer.Generic.Utf16.Serialize(Message);
        //}

        //[Benchmark]
        //public byte[] SerializeUtf8()
        //{
        //    return JsonSerializer.Generic.Utf8.Serialize(Message);
        //}

        //[Benchmark]
        //public void SerializeUtf16Unsafe()
        //{
        //    var buffer = JsonSerializer.Generic.Utf16.SerializeToArrayPool(Message);
        //    ArrayPool<char>.Shared.Return(buffer.Array);
        //}

        //[Benchmark]
        //public void SerializeUtf8Unsafe()
        //{
        //    var buffer = JsonSerializer.Generic.Utf8.SerializeToArrayPool(Message);
        //    ArrayPool<byte>.Shared.Return(buffer.Array);
        //}

        //[Benchmark]
        //public ValueTask SerializeUtf16Async()
        //{
        //    using (var tw = new StreamWriter(Stream.Null))
        //    {
        //        return JsonSerializer.Generic.Utf16.SerializeAsync(Message, tw);
        //    }
        //}

        //[Benchmark]
        //public Task SerializeUtf8Async()
        //{
        //    return JsonSerializer.Generic.Utf8.SerializeAsync(Message, Stream.Null).AsTask();
        //}

        //[Benchmark]
        //public Task SerializeUtf8JsonAsync()
        //{
        //    return Utf8Json.JsonSerializer.SerializeAsync(Stream.Null, Message);
        //}

        //[Benchmark]
        //public void SerializeUtf8JsonUnsafe()
        //{
        //    Utf8Json.JsonSerializer.SerializeUnsafe(Message);
        //}

        //[Benchmark]
        //public void SerializeUtf8Json()
        //{
        //    Utf8Json.JsonSerializer.Serialize(Message);
        //}

        //[Benchmark]
        //public void WriteMessageDirectlyUtf16()
        //{
        //    var jsonWriter = new JsonWriter<char>(64);
        //    jsonWriter.WriteUtf16BeginObject();
        //    jsonWriter.WriteUtf16Verbatim("\"message\":");
        //    jsonWriter.WriteUtf16String("Hello, World!");
        //    jsonWriter.WriteUtf16EndObject();            
        //}

        private static readonly byte[] NameByteArray = Encoding.UTF8.GetBytes("\"message\":");

        [Benchmark]
        public void WriteMessageDirectlyUtf8()
        {
            var jsonWriter = new JsonWriter<byte>(64);
            jsonWriter.WriteUtf8BeginObject();
            jsonWriter.WriteUtf8Verbatim(NameByteArray);
            jsonWriter.WriteUtf8String("Hello, World!");
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