using System.Text;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Serializers;
using SpanJson.Shared.Fixture;
using SpanJson.Shared.Models;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    [DisassemblyDiagnoser(recursiveDepth: 2)]
    public class SelectedBenchmarks
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture(12345);
        //private static readonly AccessToken AccessToken = ExpressionTreeFixture.Create<AccessToken>();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();
        private static readonly SpanJsonUtf8Serializer SpanJsonUtf8Serializer = new SpanJsonUtf8Serializer();

        //private static readonly string AccessTokenSerializedString =
        //    SpanJsonSerializer.Serialize(AccessToken);

        //private static readonly byte[] AccessTokenSerializedByteArray =
        //    Encoding.UTF8.GetBytes(AccessTokenSerializedString);


        private static readonly Answer Answer = ExpressionTreeFixture.Create<Answer>();

        private static readonly string AnswerSerializedString = SpanJsonSerializer.Serialize(Answer);

        private static readonly byte[] AnswerSerializedByteArray = Encoding.UTF8.GetBytes(AnswerSerializedString);


        //        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly StringBuilder StringBuilder = new StringBuilder();


        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();

        private static readonly MobileBadgeAward MobileBadgeAwardInput = ExpressionTreeFixture.Create<MobileBadgeAward>();

        private static readonly string MobileBadgeAwardSerializedString = JsonSerializer.Generic.Utf16.Serialize(MobileBadgeAwardInput);
        private static readonly byte[] MobileBadgeAwardSerializedByteArray = JsonSerializer.Generic.Utf8.Serialize(MobileBadgeAwardInput);

        //[Benchmark]
        //public string SerializeMobileBadgeAwardWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(MobileBadgeAwardInput);
        //}

        //[Benchmark]
        //public byte[] SerializeMobileBadgeAwardWithSpanJsonSerializerUtf8()
        //{
        //    return JsonSerializer.Generic.Utf8.Serialize(MobileBadgeAwardInput);
        //}

        //[Benchmark]
        //public MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardSerializedString);
        //}

        //[Benchmark]
        //public MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonSerializerUtf8()
        //{
        //    return JsonSerializer.Generic.Utf8.Deserialize<MobileBadgeAward>(MobileBadgeAwardSerializedByteArray);
        //}

        [Benchmark]
        public string SerializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Answer);
        }

        [Benchmark]
        public byte[] SerializeAnswerWithSpanJsonSerializerUtf8()
        {
            return JsonSerializer.Generic.Utf8.Serialize(Answer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerSerializedString);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonSerializerUtf8()
        {
            return JsonSerializer.Generic.Utf8.Deserialize<Answer>(AnswerSerializedByteArray);
        }


        //[Benchmark]
        //public BadgeRank DeserializeBadgeRankWithSpanJsonSerializerUtf16()
        //{
        //    return SpanJsonSerializer.Deserialize<BadgeRank>("\"bronze\"");
        //}

        //private static readonly byte[] bronze = Encoding.UTF8.GetBytes("\"bronze\"");

        //[Benchmark]
        //public BadgeRank DeserializeBadgeRankWithSpanJsonSerializerUtf8()
        //{
        //    return SpanJsonUtf8Serializer.Deserialize<BadgeRank>(bronze);
        //}

        //[Benchmark]
        //public async ValueTask<Answer> DeserializeAnswerWithSpanJsonSerializerAsyncUtf8()
        //{
        //    using (var ms = new MemoryStream(AnswerSerializedByteArray, 0, AnswerSerializedByteArray.Length, false, false))
        //    {
        //        return await JsonSerializer.Generic.Utf8.DeserializeAsync<Answer>(ms);
        //    }
        //}

        //[Benchmark]
        //public async ValueTask<byte[]> SerializeAnswerWithSpanJsonSerializerAsyncUtf8()
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        await JsonSerializer.Generic.Utf8.SerializeAsync(Answer, ms);
        //        return ms.ToArray();
        //    }
        //}

        //[Benchmark]
        //public async ValueTask<Answer> DeserializeAnswerWithJilSerializerAsync()
        //{
        //    using (var tr = new StringReader(AnswerSerializedString))
        //    {
        //        return JSON.Deserialize<Answer>(tr, Options.ISO8601ExcludeNullsIncludeInherited);
        //    }
        //}


        //[Benchmark]
        //public async ValueTask<string> SerializeAnswerWithJilSerializerAsync()
        //{
        //    StringBuilder.Clear();
        //    using (var tw = new StringWriter(StringBuilder))
        //    {
        //       JSON.Serialize(Answer, tw, Options.ISO8601ExcludeNullsIncludeInherited);
        //    }
        //    return StringBuilder.ToString();
        //}

        //[Benchmark]
        //public async ValueTask<Answer> DeserializeAnswerWithSpanJsonSerializerAsync()
        //{
        //    using (var tr = new StringReader(AnswerSerializedString))
        //    {
        //        return await JsonSerializer.Generic.Utf16.DeserializeAsync<Answer>(tr);
        //    }
        //}


        //[Benchmark]
        //public async ValueTask<string> SerializeAnswerWithSpanJsonSerializerAsync()
        //{
        //    StringBuilder.Clear();
        //    using (var tw = new StringWriter(StringBuilder))
        //    {
        //        await JsonSerializer.Generic.Utf16.SerializeAsync(Answer, tw);

        //    }
        //    return StringBuilder.ToString();
        //}


        //[Benchmark]
        //public string SerializeAnswerWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(Answer);
        //}

        //[Benchmark]
        //public byte[] SerializeAnswerWithSpanJsonSerializerUtf8()
        //{
        //    return JsonSerializer.Generic.Utf8.Serialize(Answer);
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
        //    return JsonSerializer.Generic.Serialize(Answer);
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

        //private static readonly string ReadInput = "\"Hello World\"";
        //private static readonly byte[] ReadInputBytes = Encoding.UTF8.GetBytes(ReadInput);

        //[Benchmark]
        //public string ReadStringUtf16()
        //{
        //    var reader = new JsonReader<char>(ReadInput);
        //    return reader.ReadUtf16String();
        //}

        //[Benchmark]
        //public string ReadStringUtf8()
        //{
        //    var reader = new JsonReader<byte>(ReadInputBytes);
        //    return reader.ReadUtf8String();
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

        //private static readonly char UInt64Input = ExpressionTreeFixture.Create<char>();

        //[Benchmark]
        //public System.String SerializeUInt64WithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(UInt64Input);
        //}


        //[Benchmark]
        //public System.Byte[] SerializeUInt64WithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(UInt64Input);
        //}

        //[Benchmark]
        //public System.String SerializeStringWithJilSerializer()
        //{
        //    return JilSerializer.Serialize(StringInput);
        //}

        //private static string StringInput = "Hello\nWorld 😁";
        ////        private static string StringInput = "Hello World";
        ////private static string StringInput = "Hello😁World";

        //[Benchmark]
        //public System.String SerializeStringWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(StringInput);
        //}


        //[Benchmark]
        //public System.Byte[] SerializeStringWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(StringInput);
        //}


        //[Benchmark]
        //public System.Byte[] SerializeStringWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Serialize(StringInput);
        //}

        //private static readonly String StringOutputOfJilSerializer = JilSerializer.Serialize(StringInput);
        //[Benchmark]
        //public System.String DeserializeStringWithJilSerializer()
        //{
        //    return JilSerializer.Deserialize<System.String>(StringOutputOfJilSerializer);
        //}

        //private static readonly String StringOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(StringInput);
        //[Benchmark]
        //public System.String DeserializeStringWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<System.String>(StringOutputOfSpanJsonSerializer);
        //}

        //private static readonly Byte[] StringOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(StringInput);
        //[Benchmark]
        //public System.String DeserializeStringWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Deserialize<System.String>(StringOutputOfSpanJsonUtf8Serializer);
        //}

        //private static readonly Byte[] StringOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StringInput);
        //[Benchmark]
        //public System.String DeserializeStringWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Deserialize<System.String>(StringOutputOfUtf8JsonSerializer);
        //}

        //private static readonly System.Single SingleInput = ExpressionTreeFixture.Create<System.Single>();

        //private static readonly System.Double DoubleInput = ExpressionTreeFixture.Create<System.Double>();

        //[Benchmark]
        //public System.String SerializeDoubleWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(DoubleInput);
        //}

        //[Benchmark]
        //public System.Byte[] SerializeDoubleWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(DoubleInput);
        //}

        //[Benchmark]
        //public System.String SerializeSingleWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(SingleInput);
        //}

        //[Benchmark]
        //public System.Byte[] SerializeSingleWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(SingleInput);
        //}

        //private static readonly System.DateTime DateTimeInput = ExpressionTreeFixture.Create<System.DateTime>();

        //private static readonly System.DateTimeOffset DateTimeOffsetInput = ExpressionTreeFixture.Create<System.DateTimeOffset>();

        //[Benchmark]
        //public System.String SerializeDateTimeWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(DateTimeInput);
        //}


        //[Benchmark]
        //public System.Byte[] SerializeDateTimeWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(DateTimeInput);
        //}

        //[Benchmark]
        //public System.String SerializeDateTimeOffsetWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Serialize(DateTimeOffsetInput);
        //}


        //[Benchmark]
        //public System.Byte[] SerializeDateTimeOffsetWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Serialize(DateTimeOffsetInput);
        //}

        //private static readonly Byte[] DateTimeOffsetOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DateTimeOffsetInput);
        //[Benchmark]
        //public System.DateTimeOffset DeserializeDateTimeOffsetWithSpanJsonUtf8Serializer()
        //{
        //    return SpanJsonUtf8Serializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfSpanJsonUtf8Serializer);
        //}

        //private static readonly string DateTimeOffsetOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DateTimeOffsetInput);
        //[Benchmark]
        //public System.DateTimeOffset DeserializeDateTimeOffsetWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfSpanJsonSerializer);
        //}

        //private static readonly byte[] DateTimeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DateTimeInput);
        //[Benchmark]
        //public System.DateTimeOffset DeserializeDateTimetWithSpanUtf8JsonSerializer()
        //{
        //    return SpanJsonUtf8Serializer.Deserialize<System.DateTime>(DateTimeOutputOfSpanJsonUtf8Serializer);
        //}

        //private static readonly string DateTimeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DateTimeInput);
        //[Benchmark]
        //public System.DateTime DeserializeDateTimeWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<System.DateTime>(DateTimeOutputOfSpanJsonSerializer);
        //}

        //private static readonly string Int64Value = ExpressionTreeFixture.Create<long>().ToString();
        //private static readonly byte[] Int64ValueBytes = Encoding.UTF8.GetBytes(Int64Value);

        //[Benchmark]
        //public long DeserializeInt64Utf8()
        //{
        //    var reader = new JsonReader<byte>(Int64ValueBytes);
        //    return reader.ReadUtf8Int64();
        //}

        //[Benchmark]
        //public long DeserializeInt64Utf16()
        //{
        //    var reader = new JsonReader<char>(Int64Value);
        //    return reader.ReadUtf16Int64();
        //}

        //private static readonly string UInt64Value = ExpressionTreeFixture.Create<ulong>().ToString();
        //private static readonly byte[] UInt64ValueBytes = Encoding.UTF8.GetBytes(UInt64Value);

        //[Benchmark]
        //public ulong DeserializeUInt64Utf8()
        //{
        //    var reader = new JsonReader<byte>(UInt64ValueBytes);
        //    return reader.ReadUtf8UInt64();
        //}

        //[Benchmark]
        //public ulong DeserializeUInt64Utf16()
        //{
        //    var reader = new JsonReader<char>(UInt64Value);
        //    return reader.ReadUtf16UInt64();
        //}

        //private static readonly byte[] NullBytes = Encoding.UTF8.GetBytes("null");
        //[Benchmark]
        //public bool ReadIsNull()
        //{
        //    var reader = new JsonReader<char>("null");
        //    return reader.ReadUtf16IsNull();
        //}

        //private static readonly byte[] FalseBytes = Encoding.UTF8.GetBytes("false");
        //[Benchmark]
        //public bool ReadFalse()
        //{
        //    var reader = new JsonReader<char>("false");
        //    return reader.ReadUtf16Boolean();
        //}

        //private static readonly byte[] TrueBytes = Encoding.UTF8.GetBytes("true");
        //[Benchmark]
        //public bool ReadTrue()
        //{
        //    var reader = new JsonReader<char>("true");
        //    return reader.ReadUtf16Boolean();
        //}

        //[Benchmark]
        //public void WriteUtf16Null()
        //{
        //    var writer = new JsonWriter<Char>(32);
        //    writer.WriteUtf16Null();
        //}

        //[Benchmark]
        //public void WriteUtf8Null()
        //{
        //    var writer = new JsonWriter<byte>(32);
        //    writer.WriteUtf8Null();
        //}

        //[Benchmark]
        //public void WriteUtf8Boolean()
        //{
        //    var writer = new JsonWriter<Byte>(32);
        //    writer.WriteUtf8Boolean(true);
        //}

        //[Benchmark]
        //public void WriteUtf8String()
        //{
        //    var writer = new JsonWriter<byte>(64);
        //    writer.WriteUtf8String("This is a Test Value String");
        //}
        //private static readonly byte[] buffer = new byte[64];
        //[Benchmark]
        //public void WriteUtf8StringUtf8Json()
        //{
        //    var writer = new Utf8Json.JsonWriter(buffer);
        //    writer.WriteString("This is a Test Value String");
        //}
    }
}