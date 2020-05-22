using System.Buffers;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Serializers;

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

        private static readonly JsonMessage JsonMessageInput = new JsonMessage {message = Message};


        private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions();
        private static readonly ArrayBufferWriter<byte> Writer = new ArrayBufferWriter<byte>();


        [Benchmark]
        public byte[] SerializeUtf8()
        {
            var message = JsonMessageInput;
            return JsonSerializer.Generic.Utf8.Serialize(message);
        }

        [Benchmark]
        public void SerializeUtf8Unsafe()
        {
            var message = JsonMessageInput;
            var buffer = JsonSerializer.Generic.Utf8.SerializeToArrayPool(message);
            ArrayPool<byte>.Shared.Return(buffer.Array);
        }


        [Benchmark]
        public Task SerializeUtf8Async()
        {
            var message = JsonMessageInput;
            return JsonSerializer.Generic.Utf8.SerializeAsync(message, Stream.Null).AsTask();
        }

        [Benchmark]
        public Task SerializeUtf8JsonAsync()
        {
            var message = JsonMessageInput;
            return Utf8Json.JsonSerializer.SerializeAsync(Stream.Null, message);
        }

        [Benchmark]
        public void SerializeUtf8JsonUnsafe()
        {
            var message = JsonMessageInput;
            Utf8Json.JsonSerializer.SerializeUnsafe(message);
        }

        [Benchmark]
        public void SerializeUtf8Json()
        {
            var message = JsonMessageInput;
            Utf8Json.JsonSerializer.Serialize(message);
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
            ArrayPool<byte>.Shared.Return(buffer);
        }

        [Benchmark]
        public void SerializeSystemTextJson()
        {
            Writer.Clear();
            var message = JsonMessageInput;
            using (var utf8JsonWriter = new Utf8JsonWriter(Writer))
            {
                System.Text.Json.JsonSerializer.Serialize(utf8JsonWriter, message, SerializerOptions);
            }
        }

        [Benchmark]
        public void WriteeMessageDirectlySystemTextJson()
        {
            Writer.Clear();
            var message = JsonMessageInput;
            using (var utf8JsonWriter = new Utf8JsonWriter(Writer))
            {
                utf8JsonWriter.WriteStartObject();
                utf8JsonWriter.WriteString("message", message.message);
                utf8JsonWriter.WriteEndObject();
            }
        }
    }
}