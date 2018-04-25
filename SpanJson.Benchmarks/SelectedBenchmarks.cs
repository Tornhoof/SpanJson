using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Validators;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [MemoryDiagnoser]
    [ShortRunJob]
    [DisassemblyDiagnoser(printIL: true, recursiveDepth:2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Answer Answer = ExpressionTreeFixture.Create<Answer>();
        private static readonly string AnswerSerializedString =            
            SpanJsonSerializer.Serialize(Answer);

        private static readonly byte[] AnswerSerializedByteArray =
            Encoding.UTF8.GetBytes(AnswerSerializedString);

        private static readonly AccessToken AccessToken = ExpressionTreeFixture.Create<AccessToken>();
        private static readonly string AccessTokenSerializedString =
            SpanJsonSerializer.Serialize(AccessToken);

        private static readonly byte[] AccessTokenSerializedByteArray =
            Encoding.UTF8.GetBytes(AccessTokenSerializedString);

        private static readonly JilSerializer JilSerializer = new JilSerializer();


        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();
        private static readonly StringBuilder StringBuilder = new StringBuilder();

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

        private static readonly BadgeType BadgeType = BadgeType.named;

        private static readonly string BageTypeSerializedString = JsonSerializer.Generic.SerializeToString(BadgeType);
        private static readonly byte[] BageTypeSerializedByteArray = JsonSerializer.Generic.SerializeToByteArray(BadgeType);

        [Benchmark]
        public BadgeType JsonTestChar()
        {
            return JsonSerializer.Generic.Deserialize<BadgeType>(BageTypeSerializedString);
        }

        [Benchmark]
        public BadgeType JsonTestByte()
        {
            return JsonSerializer.Generic.Deserialize<BadgeType>(BageTypeSerializedString);
        }


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
    }
}