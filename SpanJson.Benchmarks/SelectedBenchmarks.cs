using System.Runtime.InteropServices.ComTypes;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    [DisassemblyDiagnoser(printIL: true, recursiveDepth: 2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
        private static readonly AccessToken AccessToken = ExpressionTreeFixture.Create<AccessToken>();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();
        private static readonly SpanJsonUtf8Serializer SpanJsonUtf8Serializer = new SpanJsonUtf8Serializer();

        private static readonly string AccessTokenSerializedString =
            SpanJsonSerializer.Serialize(AccessToken);

        private static readonly byte[] AccessTokenSerializedByteArray =
            Encoding.UTF8.GetBytes(AccessTokenSerializedString);


        private static readonly Answer Answer = ExpressionTreeFixture.Create<Answer>();

        private static readonly string AnswerSerializedString =
            SpanJsonSerializer.Serialize(Answer);

        private static readonly byte[] AnswerSerializedByteArray =
            Encoding.UTF8.GetBytes(AnswerSerializedString);


        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly StringBuilder StringBuilder = new StringBuilder();


        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();

        //[Benchmark]
        //public Answer DeserializeAnswerWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<Answer>(AnswerSerializedString);
        //}

        //[Benchmark]
        //public Answer DeserializeAnswerWithSpanJsonSerializerUtf8()
        //{
        //    return JsonSerializer.Generic.Deserialize<Answer>(AnswerSerializedByteArray);
        //}

        //[Benchmark]
        //public async ValueTask<Answer> DeserializeAnswerWithSpanJsonSerializerAsync()
        //{
        //    using (var tr = new StringReader(AnswerSerializedString))
        //    {
        //        return await JsonSerializer.Generic.DeserializeAsync<Answer>(tr);
        //    }
        //}

        //[Benchmark]
        //public object DeserializeDynamicAnswerWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<dynamic>(AnswerSerializedString);
        //}

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

        //[Benchmark]
        //public string SerializeAnswerWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(Answer);
        //}

        //[Benchmark]
        //public byte[] SerializeAnswerWithSpanJsonSerializerUtf8()
        //{
        //    return JsonSerializer.Generic.SerializeToByteArray(Answer);
        //}


        //[Benchmark]
        //public async ValueTask<string> SerializeAnswerWithSpanJsonSerializerAsync()
        //{
        //    StringBuilder.Clear();
        //    using (var tw = new StringWriter(StringBuilder))
        //    {
        //        await JsonSerializer.Generic.SerializeAsync(Answer, tw);

        //    }
        //    return StringBuilder.ToString();
        //}

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

        //private static readonly string TestString = "Hello  World and the Univer and Other Stuff";

        //[Benchmark]
        //public string JsonTestChar()
        //{
        //    var jsonWriter = new JsonWriter<char>(100);
        //    jsonWriter.WriteUtf16Boolean(false);
        //    return jsonWriter.ToString();
        //}

        //[Benchmark]
        //public byte[] JsonTestByte()
        //{
        //    var jsonWriter = new JsonWriter<byte>(100);
        //    jsonWriter.WriteUtf8Boolean(false);
        //    return jsonWriter.ToByteArray();
        //}


        //[Benchmark]
        //public char[] WriteStringUtf16()
        //{
        //    var writer = new JsonWriter<char>(20);
        //    writer.WriteUtf16String("Hello World");
        //    return writer.Data;
        //}

        //[Benchmark]
        //public byte[] WriteStringUtf8()
        //{
        //    var writer = new JsonWriter<byte>(20);
        //    writer.WriteUtf8String("Hello World");
        //    return writer.Data;
        //}

        //[Benchmark]
        //public ReadOnlySpan<char> JsonTestCharDirect()
        //{
        //    var jsonWriter = new JsonReader<char>("\"Hello World\"");
        //    return jsonWriter.ReadUtf16StringSpan();
        //}

        //[Benchmark]
        //public ReadOnlySpan<byte> JsonTestByteDirect()
        //{
        //    var jsonWriter = new JsonReader<byte>(Encoding.UTF8.GetBytes("\"Hello World\""));
        //    return jsonWriter.ReadUtf8StringSpan();
        //}

        //[Benchmark]
        //public ReadOnlySpan<char> JsonTestCharInDirect()
        //{
        //    var jsonWriter = new JsonReader<char>("\"Hello World\"");
        //    return jsonWriter.ReadStringSpan();
        //}

        //[Benchmark]
        //public ReadOnlySpan<byte> JsonTestByteInDirect()
        //{
        //    var jsonWriter = new JsonReader<byte>(Encoding.UTF8.GetBytes("\"Hello World\""));
        //    return jsonWriter.ReadStringSpan();
        //}

        private static readonly char UInt64Input = ExpressionTreeFixture.Create<char>();

        [Benchmark]
        public System.String SerializeUInt64WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt64Input);
        }


        [Benchmark]
        public System.Byte[] SerializeUInt64WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UInt64Input);
        }
    }
}