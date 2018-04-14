using System;
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


        //private static readonly string AccessTokenSerializedString =
        //    SpanJsonSerializer.Serialize(ExpressionTreeFixture.Create<AccessToken>());

        //private static readonly byte[] AccessTokenSerializedByteArray =
        //    Encoding.UTF8.GetBytes(AccessTokenSerializedString);

        //[Benchmark]
        //public AccessToken DeserializeAccessTokenWithSpanJsonSerializer()
        //{
        //    return SpanJsonSerializer.Deserialize<AccessToken>(AccessTokenSerializedString);
        //}

        //[Benchmark]
        //public AccessToken DeserializeAccessTokenWithJilSerializer()
        //{
        //    return JilSerializer.Deserialize<AccessToken>(AccessTokenSerializedString);
        //}

        //[Benchmark]
        //public AccessToken DeserializeAccessTokenWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Deserialize<AccessToken>(AccessTokenSerializedByteArray);
        //}



        private static readonly DateTime DateTimeInput = ExpressionTreeFixture.Create<DateTime>();

        private static readonly DateTimeOffset DateTimeOffsetInput = ExpressionTreeFixture.Create<DateTimeOffset>();

        private static readonly string DateTimeOutputOfJilSerializer = JilSerializer.Serialize(DateTimeInput);

        private static readonly string DateTimeOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(DateTimeInput);

        private static readonly byte[] DateTimeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DateTimeInput);

        private static readonly string DateTimeOffsetOutputOfJilSerializer =
            JilSerializer.Serialize(DateTimeOffsetInput);

        private static readonly string DateTimeOffsetOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(DateTimeOffsetInput);

        private static readonly byte[] DateTimeOffsetOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(DateTimeOffsetInput);

        //[Benchmark]
        //public DateTime DeserializeDateTimeWithJilSerializer()
        //{
        //    return JilSerializer.Deserialize<DateTime>(DateTimeOutputOfJilSerializer);
        //}

        [Benchmark]
        public DateTime DeserializeDateTimeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<DateTime>(DateTimeOutputOfJsonSpanSerializer);
        }

        //[Benchmark]
        //public DateTime DeserializeDateTimeWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Deserialize<DateTime>(DateTimeOutputOfUtf8JsonSerializer);
        //}

        //[Benchmark]
        //public DateTimeOffset DeserializeDateTimeOffsetWithJilSerializer()
        //{
        //    return JilSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfJilSerializer);
        //}

        [Benchmark]
        public DateTimeOffset DeserializeDateTimeOffsetWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfJsonSpanSerializer);
        }

        //[Benchmark]
        //public DateTimeOffset DeserializeDateTimeOffsetWithUtf8JsonSerializer()
        //{
        //    return Utf8JsonSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfUtf8JsonSerializer);
        //}

    }

    //public sealed class AccessTokenFormatter : ComplexFormatter, IJsonFormatter<AccessToken>
    //{
    //    public static readonly AccessTokenFormatter Default = new AccessTokenFormatter();
    //    private static readonly SerializeDelegate<AccessToken> Serializer = BuildSerializeDelegate<AccessToken>();
    //    private static readonly DeserializeDelegate<AccessToken> Deserializer = BuildDeserializeDelegate<AccessToken>();

    //    public int AllocSize { get; } = EstimateSize<AccessToken>();

    //    public AccessToken Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
    //    {
    //        if (reader.ReadIsNull())
    //        {
    //            return null;
    //        }

    //        reader.ReadBeginObjectOrThrow();
    //        int count = 0;
    //        var result = new AccessToken();
    //        while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
    //        {
    //            var nameSpan = reader.ReadNameSpan();
    //            switch (nameSpan[0])
    //            {
    //                case 'a':
    //                    switch (nameSpan[1])
    //                    {
    //                        case 'c':
    //                            switch (nameSpan[2])
    //                            {
    //                                case 'c':
    //                                    switch (nameSpan[3])
    //                                    {
    //                                        case 'e':
    //                                            if (nameSpan.Slice(4).SequenceEqual("ss_token".AsSpan()))
    //                                            {
    //                                                result.access_token =
    //                                                    StringFormatter.Default.Deserialize(ref reader,
    //                                                        formatterResolver);
    //                                            }

    //                                            break;
    //                                        case 'o':
    //                                            if (nameSpan.Slice(4).SequenceEqual("unt_id".AsSpan()))
    //                                            {
    //                                                result.account_id =
    //                                                    NullableInt32Formatter.Default.Deserialize(ref reader,
    //                                                        formatterResolver);
    //                                            }

    //                                            break;
    //                                    }

    //                                    break;
    //                            }
    //                            break;

    //                    }
    //                    break;
    //                case 'e':
    //                    if (nameSpan.Slice(1).SequenceEqual("xpires_on_date".AsSpan()))
    //                    {
    //                        //reader.ReadStringSpanInternal();
    //                        result.expires_on_date = NullableDateTimeFormatter.Default.Deserialize(ref reader, formatterResolver);
    //                    }
    //                    break;
    //                case 's':
    //                    if (nameSpan.Slice(1).SequenceEqual("cope".AsSpan()))
    //                    {
    //                        result.scope = StringListFormatter.Default.Deserialize(ref reader, formatterResolver);
    //                    }
    //                    break;
    //            }
    //        }

    //        return result;
    //    }


    //    public void Serialize(ref JsonWriter writer, AccessToken value, IJsonFormatterResolver formatterResolver)
    //    {
    //        if (value == null)
    //        {
    //            writer.WriteNull();
    //            return;
    //        }

    //        Serializer(ref writer, value, formatterResolver);
    //    }
    //}
}