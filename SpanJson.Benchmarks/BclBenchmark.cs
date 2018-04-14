using System;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public class BclBenchmark
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly JilSerializer JilSerializer = new JilSerializer();

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();


        private static readonly sbyte SByteInput = ExpressionTreeFixture.Create<sbyte>();

        private static readonly short Int16Input = ExpressionTreeFixture.Create<short>();

        private static readonly int Int32Input = ExpressionTreeFixture.Create<int>();

        private static readonly long Int64Input = ExpressionTreeFixture.Create<long>();

        private static readonly byte ByteInput = ExpressionTreeFixture.Create<byte>();

        private static readonly ushort UInt16Input = ExpressionTreeFixture.Create<ushort>();

        private static readonly uint UInt32Input = ExpressionTreeFixture.Create<uint>();

        private static readonly ulong UInt64Input = ExpressionTreeFixture.Create<ulong>();

        private static readonly float SingleInput = ExpressionTreeFixture.Create<float>();

        private static readonly double DoubleInput = ExpressionTreeFixture.Create<double>();

        private static readonly bool BooleanInput = ExpressionTreeFixture.Create<bool>();

        private static readonly char CharInput = ExpressionTreeFixture.Create<char>();

        private static readonly DateTime DateTimeInput = ExpressionTreeFixture.Create<DateTime>();

        private static readonly DateTimeOffset DateTimeOffsetInput = ExpressionTreeFixture.Create<DateTimeOffset>();

        private static readonly TimeSpan TimeSpanInput = ExpressionTreeFixture.Create<TimeSpan>();

        private static readonly Guid GuidInput = ExpressionTreeFixture.Create<Guid>();

        private static readonly string StringInput = ExpressionTreeFixture.Create<string>();

        private static readonly decimal DecimalInput = ExpressionTreeFixture.Create<decimal>();

        private static readonly Version VersionInput = ExpressionTreeFixture.Create<Version>();

        private static readonly Uri UriInput = ExpressionTreeFixture.Create<Uri>();


        private static readonly string SByteOutputOfJilSerializer = JilSerializer.Serialize(SByteInput);

        private static readonly string SByteOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(SByteInput);

        private static readonly byte[] SByteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SByteInput);

        private static readonly string Int16OutputOfJilSerializer = JilSerializer.Serialize(Int16Input);

        private static readonly string Int16OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(Int16Input);

        private static readonly byte[] Int16OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int16Input);

        private static readonly string Int32OutputOfJilSerializer = JilSerializer.Serialize(Int32Input);

        private static readonly string Int32OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(Int32Input);

        private static readonly byte[] Int32OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int32Input);

        private static readonly string Int64OutputOfJilSerializer = JilSerializer.Serialize(Int64Input);

        private static readonly string Int64OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(Int64Input);

        private static readonly byte[] Int64OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int64Input);

        private static readonly string ByteOutputOfJilSerializer = JilSerializer.Serialize(ByteInput);

        private static readonly string ByteOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(ByteInput);

        private static readonly byte[] ByteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ByteInput);

        private static readonly string UInt16OutputOfJilSerializer = JilSerializer.Serialize(UInt16Input);

        private static readonly string UInt16OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(UInt16Input);

        private static readonly byte[] UInt16OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt16Input);

        private static readonly string UInt32OutputOfJilSerializer = JilSerializer.Serialize(UInt32Input);

        private static readonly string UInt32OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(UInt32Input);

        private static readonly byte[] UInt32OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt32Input);

        private static readonly string UInt64OutputOfJilSerializer = JilSerializer.Serialize(UInt64Input);

        private static readonly string UInt64OutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(UInt64Input);

        private static readonly byte[] UInt64OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt64Input);

        private static readonly string SingleOutputOfJilSerializer = JilSerializer.Serialize(SingleInput);

        private static readonly string SingleOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(SingleInput);

        private static readonly byte[] SingleOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SingleInput);

        private static readonly string DoubleOutputOfJilSerializer = JilSerializer.Serialize(DoubleInput);

        private static readonly string DoubleOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(DoubleInput);

        private static readonly byte[] DoubleOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DoubleInput);

        private static readonly string BooleanOutputOfJilSerializer = JilSerializer.Serialize(BooleanInput);

        private static readonly string BooleanOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(BooleanInput);

        private static readonly byte[] BooleanOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BooleanInput);

        private static readonly string CharOutputOfJilSerializer = JilSerializer.Serialize(CharInput);

        private static readonly string CharOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(CharInput);

        private static readonly byte[] CharOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(CharInput);

        private static readonly string DateTimeOutputOfJilSerializer = JilSerializer.Serialize(DateTimeInput);

        private static readonly string DateTimeOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(DateTimeInput);

        private static readonly byte[] DateTimeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DateTimeInput);

        private static readonly string DateTimeOffsetOutputOfJilSerializer =
            JilSerializer.Serialize(DateTimeOffsetInput);

        private static readonly string DateTimeOffsetOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(DateTimeOffsetInput);

        private static readonly byte[] DateTimeOffsetOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(DateTimeOffsetInput);

        private static readonly string TimeSpanOutputOfJilSerializer = JilSerializer.Serialize(TimeSpanInput);

        private static readonly string TimeSpanOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(TimeSpanInput);

        private static readonly byte[] TimeSpanOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TimeSpanInput);

        private static readonly string GuidOutputOfJilSerializer = JilSerializer.Serialize(GuidInput);

        private static readonly string GuidOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(GuidInput);

        private static readonly byte[] GuidOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(GuidInput);

        private static readonly string StringOutputOfJilSerializer = JilSerializer.Serialize(StringInput);

        private static readonly string StringOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(StringInput);

        private static readonly byte[] StringOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StringInput);

        private static readonly string DecimalOutputOfJilSerializer = JilSerializer.Serialize(DecimalInput);

        private static readonly string DecimalOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(DecimalInput);

        private static readonly byte[] DecimalOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DecimalInput);

        private static readonly string VersionOutputOfJilSerializer = JilSerializer.Serialize(VersionInput);

        private static readonly string VersionOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(VersionInput);

        private static readonly byte[] VersionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(VersionInput);

        private static readonly string UriOutputOfJilSerializer = JilSerializer.Serialize(UriInput);

        private static readonly string UriOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(UriInput);

        private static readonly byte[] UriOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UriInput);


        [Benchmark]
        public string SerializeSByteWithJilSerializer()
        {
            return JilSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public string SerializeSByteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public byte[] SerializeSByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public string SerializeInt16WithJilSerializer()
        {
            return JilSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public string SerializeInt16WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public byte[] SerializeInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public string SerializeInt32WithJilSerializer()
        {
            return JilSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public string SerializeInt32WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public byte[] SerializeInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public string SerializeInt64WithJilSerializer()
        {
            return JilSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public string SerializeInt64WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public byte[] SerializeInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public string SerializeByteWithJilSerializer()
        {
            return JilSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public string SerializeByteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public byte[] SerializeByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public string SerializeUInt16WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public string SerializeUInt16WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public byte[] SerializeUInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public string SerializeUInt32WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public string SerializeUInt32WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public byte[] SerializeUInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public string SerializeUInt64WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt64Input);
        }


        [Benchmark]
        public string SerializeUInt64WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt64Input);
        }


        [Benchmark]
        public byte[] SerializeUInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt64Input);
        }


        [Benchmark]
        public string SerializeSingleWithJilSerializer()
        {
            return JilSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public string SerializeSingleWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public byte[] SerializeSingleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public string SerializeDoubleWithJilSerializer()
        {
            return JilSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public string SerializeDoubleWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public byte[] SerializeDoubleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public string SerializeBooleanWithJilSerializer()
        {
            return JilSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public string SerializeBooleanWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public byte[] SerializeBooleanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public string SerializeCharWithJilSerializer()
        {
            return JilSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public string SerializeCharWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public byte[] SerializeCharWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public string SerializeDateTimeWithJilSerializer()
        {
            return JilSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public string SerializeDateTimeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public byte[] SerializeDateTimeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public string SerializeDateTimeOffsetWithJilSerializer()
        {
            return JilSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public string SerializeDateTimeOffsetWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public byte[] SerializeDateTimeOffsetWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public string SerializeTimeSpanWithJilSerializer()
        {
            return JilSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public string SerializeTimeSpanWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public byte[] SerializeTimeSpanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public string SerializeGuidWithJilSerializer()
        {
            return JilSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public string SerializeGuidWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public byte[] SerializeGuidWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public string SerializeStringWithJilSerializer()
        {
            return JilSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public string SerializeStringWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public byte[] SerializeStringWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public string SerializeDecimalWithJilSerializer()
        {
            return JilSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public string SerializeDecimalWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public byte[] SerializeDecimalWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public string SerializeVersionWithJilSerializer()
        {
            return JilSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public string SerializeVersionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public byte[] SerializeVersionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public string SerializeUriWithJilSerializer()
        {
            return JilSerializer.Serialize(UriInput);
        }


        [Benchmark]
        public string SerializeUriWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UriInput);
        }


        [Benchmark]
        public byte[] SerializeUriWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UriInput);
        }

        [Benchmark]
        public sbyte DeserializeSByteWithJilSerializer()
        {
            return JilSerializer.Deserialize<sbyte>(SByteOutputOfJilSerializer);
        }

        [Benchmark]
        public sbyte DeserializeSByteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<sbyte>(SByteOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public sbyte DeserializeSByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<sbyte>(SByteOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public short DeserializeInt16WithJilSerializer()
        {
            return JilSerializer.Deserialize<short>(Int16OutputOfJilSerializer);
        }

        [Benchmark]
        public short DeserializeInt16WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<short>(Int16OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public short DeserializeInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<short>(Int16OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public int DeserializeInt32WithJilSerializer()
        {
            return JilSerializer.Deserialize<int>(Int32OutputOfJilSerializer);
        }

        [Benchmark]
        public int DeserializeInt32WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<int>(Int32OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public int DeserializeInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<int>(Int32OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public long DeserializeInt64WithJilSerializer()
        {
            return JilSerializer.Deserialize<long>(Int64OutputOfJilSerializer);
        }

        [Benchmark]
        public long DeserializeInt64WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<long>(Int64OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public long DeserializeInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<long>(Int64OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public byte DeserializeByteWithJilSerializer()
        {
            return JilSerializer.Deserialize<byte>(ByteOutputOfJilSerializer);
        }

        [Benchmark]
        public byte DeserializeByteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<byte>(ByteOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public byte DeserializeByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<byte>(ByteOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public ushort DeserializeUInt16WithJilSerializer()
        {
            return JilSerializer.Deserialize<ushort>(UInt16OutputOfJilSerializer);
        }

        [Benchmark]
        public ushort DeserializeUInt16WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<ushort>(UInt16OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public ushort DeserializeUInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<ushort>(UInt16OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public uint DeserializeUInt32WithJilSerializer()
        {
            return JilSerializer.Deserialize<uint>(UInt32OutputOfJilSerializer);
        }

        [Benchmark]
        public uint DeserializeUInt32WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<uint>(UInt32OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public uint DeserializeUInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<uint>(UInt32OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public ulong DeserializeUInt64WithJilSerializer()
        {
            return JilSerializer.Deserialize<ulong>(UInt64OutputOfJilSerializer);
        }

        [Benchmark]
        public ulong DeserializeUInt64WithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<ulong>(UInt64OutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public ulong DeserializeUInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<ulong>(UInt64OutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public float DeserializeSingleWithJilSerializer()
        {
            return JilSerializer.Deserialize<float>(SingleOutputOfJilSerializer);
        }

        [Benchmark]
        public float DeserializeSingleWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<float>(SingleOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public float DeserializeSingleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<float>(SingleOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public double DeserializeDoubleWithJilSerializer()
        {
            return JilSerializer.Deserialize<double>(DoubleOutputOfJilSerializer);
        }

        [Benchmark]
        public double DeserializeDoubleWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<double>(DoubleOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public double DeserializeDoubleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<double>(DoubleOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public bool DeserializeBooleanWithJilSerializer()
        {
            return JilSerializer.Deserialize<bool>(BooleanOutputOfJilSerializer);
        }

        [Benchmark]
        public bool DeserializeBooleanWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<bool>(BooleanOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public bool DeserializeBooleanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<bool>(BooleanOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public char DeserializeCharWithJilSerializer()
        {
            return JilSerializer.Deserialize<char>(CharOutputOfJilSerializer);
        }

        [Benchmark]
        public char DeserializeCharWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<char>(CharOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public char DeserializeCharWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<char>(CharOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public DateTime DeserializeDateTimeWithJilSerializer()
        {
            return JilSerializer.Deserialize<DateTime>(DateTimeOutputOfJilSerializer);
        }

        [Benchmark]
        public DateTime DeserializeDateTimeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<DateTime>(DateTimeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public DateTime DeserializeDateTimeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<DateTime>(DateTimeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public DateTimeOffset DeserializeDateTimeOffsetWithJilSerializer()
        {
            return JilSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfJilSerializer);
        }

        [Benchmark]
        public DateTimeOffset DeserializeDateTimeOffsetWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public DateTimeOffset DeserializeDateTimeOffsetWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<DateTimeOffset>(DateTimeOffsetOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public TimeSpan DeserializeTimeSpanWithJilSerializer()
        {
            return JilSerializer.Deserialize<TimeSpan>(TimeSpanOutputOfJilSerializer);
        }

        [Benchmark]
        public TimeSpan DeserializeTimeSpanWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<TimeSpan>(TimeSpanOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public TimeSpan DeserializeTimeSpanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<TimeSpan>(TimeSpanOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Guid DeserializeGuidWithJilSerializer()
        {
            return JilSerializer.Deserialize<Guid>(GuidOutputOfJilSerializer);
        }

        [Benchmark]
        public Guid DeserializeGuidWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Guid>(GuidOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Guid DeserializeGuidWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Guid>(GuidOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public string DeserializeStringWithJilSerializer()
        {
            return JilSerializer.Deserialize<string>(StringOutputOfJilSerializer);
        }

        [Benchmark]
        public string DeserializeStringWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<string>(StringOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public string DeserializeStringWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<string>(StringOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public decimal DeserializeDecimalWithJilSerializer()
        {
            return JilSerializer.Deserialize<decimal>(DecimalOutputOfJilSerializer);
        }

        [Benchmark]
        public decimal DeserializeDecimalWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<decimal>(DecimalOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public decimal DeserializeDecimalWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<decimal>(DecimalOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Version DeserializeVersionWithJilSerializer()
        {
            return JilSerializer.Deserialize<Version>(VersionOutputOfJilSerializer);
        }

        [Benchmark]
        public Version DeserializeVersionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Version>(VersionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Version DeserializeVersionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Version>(VersionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Uri DeserializeUriWithJilSerializer()
        {
            return JilSerializer.Deserialize<Uri>(UriOutputOfJilSerializer);
        }

        [Benchmark]
        public Uri DeserializeUriWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Uri>(UriOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Uri DeserializeUriWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Uri>(UriOutputOfUtf8JsonSerializer);
        }
    }
}