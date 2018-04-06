using System.Collections.Generic;
using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    //[ShortRunJob]
    [DisassemblyDiagnoser(true, recursiveDepth: 2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly string HelloWorldSerializedString =
            SpanJsonSerializer.Serialize(ExpressionTreeFixture.Create<HelloWorld>());

        private static readonly byte[] HelloWorldSerializedByteArray = Encoding.UTF8.GetBytes(HelloWorldSerializedString);

        [Benchmark]
        public HelloWorld DeserializeHelloWorldWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<HelloWorld>(HelloWorldSerializedString);
        }

        [Benchmark]
        public HelloWorld DeserializeHelloWorldWithJilSerializer()
        {
            return JilSerializer.Deserialize<HelloWorld>(HelloWorldSerializedString);
        }

        [Benchmark]
        public HelloWorld DeserializeHelloWorldWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<HelloWorld>(HelloWorldSerializedByteArray);
        }
    }

    public class HelloWorld
    {
        public string Value { get; set; }
    }
}