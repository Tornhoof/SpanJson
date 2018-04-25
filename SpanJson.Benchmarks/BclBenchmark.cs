using System;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public partial class BclBenchmark
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly Serializers.JilSerializer JilSerializer = new Serializers.JilSerializer();

        private static readonly Serializers.SpanJsonSerializer SpanJsonSerializer = new Serializers.SpanJsonSerializer();

        private static readonly Serializers.SpanJsonUtf8Serializer SpanJsonUtf8Serializer = new Serializers.SpanJsonUtf8Serializer();

        private static readonly Serializers.Utf8JsonSerializer Utf8JsonSerializer = new Serializers.Utf8JsonSerializer();


        private static readonly System.SByte SByteInput = ExpressionTreeFixture.Create<System.SByte>();

        private static readonly System.Int16 Int16Input = ExpressionTreeFixture.Create<System.Int16>();

        private static readonly System.Int32 Int32Input = ExpressionTreeFixture.Create<System.Int32>();

        private static readonly System.Int64 Int64Input = ExpressionTreeFixture.Create<System.Int64>();

        private static readonly System.Byte ByteInput = ExpressionTreeFixture.Create<System.Byte>();

        private static readonly System.UInt16 UInt16Input = ExpressionTreeFixture.Create<System.UInt16>();

        private static readonly System.UInt32 UInt32Input = ExpressionTreeFixture.Create<System.UInt32>();

        private static readonly System.UInt64 UInt64Input = ExpressionTreeFixture.Create<System.UInt64>();

        private static readonly System.Single SingleInput = ExpressionTreeFixture.Create<System.Single>();

        private static readonly System.Double DoubleInput = ExpressionTreeFixture.Create<System.Double>();

        private static readonly System.Boolean BooleanInput = ExpressionTreeFixture.Create<System.Boolean>();

        private static readonly System.Char CharInput = ExpressionTreeFixture.Create<System.Char>();

        private static readonly System.DateTime DateTimeInput = ExpressionTreeFixture.Create<System.DateTime>();

        private static readonly System.DateTimeOffset DateTimeOffsetInput = ExpressionTreeFixture.Create<System.DateTimeOffset>();

        private static readonly System.TimeSpan TimeSpanInput = ExpressionTreeFixture.Create<System.TimeSpan>();

        private static readonly System.Guid GuidInput = ExpressionTreeFixture.Create<System.Guid>();

        private static readonly System.String StringInput = ExpressionTreeFixture.Create<System.String>();

        private static readonly System.Decimal DecimalInput = ExpressionTreeFixture.Create<System.Decimal>();

        private static readonly System.Version VersionInput = ExpressionTreeFixture.Create<System.Version>();

        private static readonly System.Uri UriInput = ExpressionTreeFixture.Create<System.Uri>();



        [Benchmark]
        public System.String SerializeSByteWithJilSerializer()
        {
            return JilSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public System.String SerializeSByteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public System.Byte[] SerializeSByteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(SByteInput);
        }


        [Benchmark]
        public System.Byte[] SerializeSByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SByteInput);
        }


        [Benchmark]
        public System.String SerializeInt16WithJilSerializer()
        {
            return JilSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public System.String SerializeInt16WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt16WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(Int16Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int16Input);
        }


        [Benchmark]
        public System.String SerializeInt32WithJilSerializer()
        {
            return JilSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public System.String SerializeInt32WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt32WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(Int32Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int32Input);
        }


        [Benchmark]
        public System.String SerializeInt64WithJilSerializer()
        {
            return JilSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public System.String SerializeInt64WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt64WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(Int64Input);
        }


        [Benchmark]
        public System.Byte[] SerializeInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(Int64Input);
        }


        [Benchmark]
        public System.String SerializeByteWithJilSerializer()
        {
            return JilSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public System.String SerializeByteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public System.Byte[] SerializeByteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ByteInput);
        }


        [Benchmark]
        public System.Byte[] SerializeByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ByteInput);
        }


        [Benchmark]
        public System.String SerializeUInt16WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public System.String SerializeUInt16WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public System.Byte[] SerializeUInt16WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public System.Byte[] SerializeUInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt16Input);
        }


        [Benchmark]
        public System.String SerializeUInt32WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public System.String SerializeUInt32WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public System.Byte[] SerializeUInt32WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public System.Byte[] SerializeUInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt32Input);
        }


        [Benchmark]
        public System.String SerializeUInt64WithJilSerializer()
        {
            return JilSerializer.Serialize(UInt64Input);
        }


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


        [Benchmark]
        public System.Byte[] SerializeUInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UInt64Input);
        }


        [Benchmark]
        public System.String SerializeSingleWithJilSerializer()
        {
            return JilSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public System.String SerializeSingleWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public System.Byte[] SerializeSingleWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(SingleInput);
        }


        [Benchmark]
        public System.Byte[] SerializeSingleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SingleInput);
        }


        [Benchmark]
        public System.String SerializeDoubleWithJilSerializer()
        {
            return JilSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public System.String SerializeDoubleWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDoubleWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDoubleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DoubleInput);
        }


        [Benchmark]
        public System.String SerializeBooleanWithJilSerializer()
        {
            return JilSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public System.String SerializeBooleanWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public System.Byte[] SerializeBooleanWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public System.Byte[] SerializeBooleanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(BooleanInput);
        }


        [Benchmark]
        public System.String SerializeCharWithJilSerializer()
        {
            return JilSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public System.String SerializeCharWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public System.Byte[] SerializeCharWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(CharInput);
        }


        [Benchmark]
        public System.Byte[] SerializeCharWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(CharInput);
        }


        [Benchmark]
        public System.String SerializeDateTimeWithJilSerializer()
        {
            return JilSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public System.String SerializeDateTimeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDateTimeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDateTimeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DateTimeInput);
        }


        [Benchmark]
        public System.String SerializeDateTimeOffsetWithJilSerializer()
        {
            return JilSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public System.String SerializeDateTimeOffsetWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDateTimeOffsetWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDateTimeOffsetWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DateTimeOffsetInput);
        }


        [Benchmark]
        public System.String SerializeTimeSpanWithJilSerializer()
        {
            return JilSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public System.String SerializeTimeSpanWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public System.Byte[] SerializeTimeSpanWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public System.Byte[] SerializeTimeSpanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TimeSpanInput);
        }


        [Benchmark]
        public System.String SerializeGuidWithJilSerializer()
        {
            return JilSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public System.String SerializeGuidWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public System.Byte[] SerializeGuidWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(GuidInput);
        }


        [Benchmark]
        public System.Byte[] SerializeGuidWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(GuidInput);
        }


        [Benchmark]
        public System.String SerializeStringWithJilSerializer()
        {
            return JilSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public System.String SerializeStringWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public System.Byte[] SerializeStringWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(StringInput);
        }


        [Benchmark]
        public System.Byte[] SerializeStringWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(StringInput);
        }


        [Benchmark]
        public System.String SerializeDecimalWithJilSerializer()
        {
            return JilSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public System.String SerializeDecimalWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDecimalWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public System.Byte[] SerializeDecimalWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(DecimalInput);
        }


        [Benchmark]
        public System.String SerializeVersionWithJilSerializer()
        {
            return JilSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public System.String SerializeVersionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public System.Byte[] SerializeVersionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(VersionInput);
        }


        [Benchmark]
        public System.Byte[] SerializeVersionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(VersionInput);
        }


        [Benchmark]
        public System.String SerializeUriWithJilSerializer()
        {
            return JilSerializer.Serialize(UriInput);
        }


        [Benchmark]
        public System.String SerializeUriWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UriInput);
        }


        [Benchmark]
        public System.Byte[] SerializeUriWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UriInput);
        }


        [Benchmark]
        public System.Byte[] SerializeUriWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UriInput);
        }


        private static readonly String SByteOutputOfJilSerializer = JilSerializer.Serialize(SByteInput);
        [Benchmark]
        public System.SByte DeserializeSByteWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.SByte>(SByteOutputOfJilSerializer);
        }

        private static readonly String SByteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SByteInput);
        [Benchmark]
        public System.SByte DeserializeSByteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.SByte>(SByteOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] SByteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SByteInput);
        [Benchmark]
        public System.SByte DeserializeSByteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.SByte>(SByteOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] SByteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SByteInput);
        [Benchmark]
        public System.SByte DeserializeSByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.SByte>(SByteOutputOfUtf8JsonSerializer);
        }

        private static readonly String Int16OutputOfJilSerializer = JilSerializer.Serialize(Int16Input);
        [Benchmark]
        public System.Int16 DeserializeInt16WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Int16>(Int16OutputOfJilSerializer);
        }

        private static readonly String Int16OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(Int16Input);
        [Benchmark]
        public System.Int16 DeserializeInt16WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Int16>(Int16OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] Int16OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(Int16Input);
        [Benchmark]
        public System.Int16 DeserializeInt16WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Int16>(Int16OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] Int16OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int16Input);
        [Benchmark]
        public System.Int16 DeserializeInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Int16>(Int16OutputOfUtf8JsonSerializer);
        }

        private static readonly String Int32OutputOfJilSerializer = JilSerializer.Serialize(Int32Input);
        [Benchmark]
        public System.Int32 DeserializeInt32WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Int32>(Int32OutputOfJilSerializer);
        }

        private static readonly String Int32OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(Int32Input);
        [Benchmark]
        public System.Int32 DeserializeInt32WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Int32>(Int32OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] Int32OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(Int32Input);
        [Benchmark]
        public System.Int32 DeserializeInt32WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Int32>(Int32OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] Int32OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int32Input);
        [Benchmark]
        public System.Int32 DeserializeInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Int32>(Int32OutputOfUtf8JsonSerializer);
        }

        private static readonly String Int64OutputOfJilSerializer = JilSerializer.Serialize(Int64Input);
        [Benchmark]
        public System.Int64 DeserializeInt64WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Int64>(Int64OutputOfJilSerializer);
        }

        private static readonly String Int64OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(Int64Input);
        [Benchmark]
        public System.Int64 DeserializeInt64WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Int64>(Int64OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] Int64OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(Int64Input);
        [Benchmark]
        public System.Int64 DeserializeInt64WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Int64>(Int64OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] Int64OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(Int64Input);
        [Benchmark]
        public System.Int64 DeserializeInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Int64>(Int64OutputOfUtf8JsonSerializer);
        }

        private static readonly String ByteOutputOfJilSerializer = JilSerializer.Serialize(ByteInput);
        [Benchmark]
        public System.Byte DeserializeByteWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Byte>(ByteOutputOfJilSerializer);
        }

        private static readonly String ByteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ByteInput);
        [Benchmark]
        public System.Byte DeserializeByteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Byte>(ByteOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] ByteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ByteInput);
        [Benchmark]
        public System.Byte DeserializeByteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Byte>(ByteOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] ByteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ByteInput);
        [Benchmark]
        public System.Byte DeserializeByteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Byte>(ByteOutputOfUtf8JsonSerializer);
        }

        private static readonly String UInt16OutputOfJilSerializer = JilSerializer.Serialize(UInt16Input);
        [Benchmark]
        public System.UInt16 DeserializeUInt16WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.UInt16>(UInt16OutputOfJilSerializer);
        }

        private static readonly String UInt16OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UInt16Input);
        [Benchmark]
        public System.UInt16 DeserializeUInt16WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.UInt16>(UInt16OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] UInt16OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UInt16Input);
        [Benchmark]
        public System.UInt16 DeserializeUInt16WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.UInt16>(UInt16OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] UInt16OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt16Input);
        [Benchmark]
        public System.UInt16 DeserializeUInt16WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.UInt16>(UInt16OutputOfUtf8JsonSerializer);
        }

        private static readonly String UInt32OutputOfJilSerializer = JilSerializer.Serialize(UInt32Input);
        [Benchmark]
        public System.UInt32 DeserializeUInt32WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.UInt32>(UInt32OutputOfJilSerializer);
        }

        private static readonly String UInt32OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UInt32Input);
        [Benchmark]
        public System.UInt32 DeserializeUInt32WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.UInt32>(UInt32OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] UInt32OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UInt32Input);
        [Benchmark]
        public System.UInt32 DeserializeUInt32WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.UInt32>(UInt32OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] UInt32OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt32Input);
        [Benchmark]
        public System.UInt32 DeserializeUInt32WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.UInt32>(UInt32OutputOfUtf8JsonSerializer);
        }

        private static readonly String UInt64OutputOfJilSerializer = JilSerializer.Serialize(UInt64Input);
        [Benchmark]
        public System.UInt64 DeserializeUInt64WithJilSerializer()
        {
            return JilSerializer.Deserialize<System.UInt64>(UInt64OutputOfJilSerializer);
        }

        private static readonly String UInt64OutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UInt64Input);
        [Benchmark]
        public System.UInt64 DeserializeUInt64WithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.UInt64>(UInt64OutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] UInt64OutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UInt64Input);
        [Benchmark]
        public System.UInt64 DeserializeUInt64WithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.UInt64>(UInt64OutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] UInt64OutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UInt64Input);
        [Benchmark]
        public System.UInt64 DeserializeUInt64WithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.UInt64>(UInt64OutputOfUtf8JsonSerializer);
        }

        private static readonly String SingleOutputOfJilSerializer = JilSerializer.Serialize(SingleInput);
        [Benchmark]
        public System.Single DeserializeSingleWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Single>(SingleOutputOfJilSerializer);
        }

        private static readonly String SingleOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SingleInput);
        [Benchmark]
        public System.Single DeserializeSingleWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Single>(SingleOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] SingleOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SingleInput);
        [Benchmark]
        public System.Single DeserializeSingleWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Single>(SingleOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] SingleOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SingleInput);
        [Benchmark]
        public System.Single DeserializeSingleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Single>(SingleOutputOfUtf8JsonSerializer);
        }

        private static readonly String DoubleOutputOfJilSerializer = JilSerializer.Serialize(DoubleInput);
        [Benchmark]
        public System.Double DeserializeDoubleWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Double>(DoubleOutputOfJilSerializer);
        }

        private static readonly String DoubleOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DoubleInput);
        [Benchmark]
        public System.Double DeserializeDoubleWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Double>(DoubleOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] DoubleOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DoubleInput);
        [Benchmark]
        public System.Double DeserializeDoubleWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Double>(DoubleOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] DoubleOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DoubleInput);
        [Benchmark]
        public System.Double DeserializeDoubleWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Double>(DoubleOutputOfUtf8JsonSerializer);
        }

        private static readonly String BooleanOutputOfJilSerializer = JilSerializer.Serialize(BooleanInput);
        [Benchmark]
        public System.Boolean DeserializeBooleanWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Boolean>(BooleanOutputOfJilSerializer);
        }

        private static readonly String BooleanOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(BooleanInput);
        [Benchmark]
        public System.Boolean DeserializeBooleanWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Boolean>(BooleanOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] BooleanOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(BooleanInput);
        [Benchmark]
        public System.Boolean DeserializeBooleanWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Boolean>(BooleanOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] BooleanOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BooleanInput);
        [Benchmark]
        public System.Boolean DeserializeBooleanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Boolean>(BooleanOutputOfUtf8JsonSerializer);
        }

        private static readonly String CharOutputOfJilSerializer = JilSerializer.Serialize(CharInput);
        [Benchmark]
        public System.Char DeserializeCharWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Char>(CharOutputOfJilSerializer);
        }

        private static readonly String CharOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(CharInput);
        [Benchmark]
        public System.Char DeserializeCharWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Char>(CharOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] CharOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(CharInput);
        [Benchmark]
        public System.Char DeserializeCharWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Char>(CharOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] CharOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(CharInput);
        [Benchmark]
        public System.Char DeserializeCharWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Char>(CharOutputOfUtf8JsonSerializer);
        }

        private static readonly String DateTimeOutputOfJilSerializer = JilSerializer.Serialize(DateTimeInput);
        [Benchmark]
        public System.DateTime DeserializeDateTimeWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.DateTime>(DateTimeOutputOfJilSerializer);
        }

        private static readonly String DateTimeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DateTimeInput);
        [Benchmark]
        public System.DateTime DeserializeDateTimeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.DateTime>(DateTimeOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] DateTimeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DateTimeInput);
        [Benchmark]
        public System.DateTime DeserializeDateTimeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.DateTime>(DateTimeOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] DateTimeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DateTimeInput);
        [Benchmark]
        public System.DateTime DeserializeDateTimeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.DateTime>(DateTimeOutputOfUtf8JsonSerializer);
        }

        private static readonly String DateTimeOffsetOutputOfJilSerializer = JilSerializer.Serialize(DateTimeOffsetInput);
        [Benchmark]
        public System.DateTimeOffset DeserializeDateTimeOffsetWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfJilSerializer);
        }

        private static readonly String DateTimeOffsetOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DateTimeOffsetInput);
        [Benchmark]
        public System.DateTimeOffset DeserializeDateTimeOffsetWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] DateTimeOffsetOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DateTimeOffsetInput);
        [Benchmark]
        public System.DateTimeOffset DeserializeDateTimeOffsetWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] DateTimeOffsetOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DateTimeOffsetInput);
        [Benchmark]
        public System.DateTimeOffset DeserializeDateTimeOffsetWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.DateTimeOffset>(DateTimeOffsetOutputOfUtf8JsonSerializer);
        }

        private static readonly String TimeSpanOutputOfJilSerializer = JilSerializer.Serialize(TimeSpanInput);
        [Benchmark]
        public System.TimeSpan DeserializeTimeSpanWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.TimeSpan>(TimeSpanOutputOfJilSerializer);
        }

        private static readonly String TimeSpanOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TimeSpanInput);
        [Benchmark]
        public System.TimeSpan DeserializeTimeSpanWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.TimeSpan>(TimeSpanOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] TimeSpanOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TimeSpanInput);
        [Benchmark]
        public System.TimeSpan DeserializeTimeSpanWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.TimeSpan>(TimeSpanOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] TimeSpanOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TimeSpanInput);
        [Benchmark]
        public System.TimeSpan DeserializeTimeSpanWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.TimeSpan>(TimeSpanOutputOfUtf8JsonSerializer);
        }

        private static readonly String GuidOutputOfJilSerializer = JilSerializer.Serialize(GuidInput);
        [Benchmark]
        public System.Guid DeserializeGuidWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Guid>(GuidOutputOfJilSerializer);
        }

        private static readonly String GuidOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(GuidInput);
        [Benchmark]
        public System.Guid DeserializeGuidWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Guid>(GuidOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] GuidOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(GuidInput);
        [Benchmark]
        public System.Guid DeserializeGuidWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Guid>(GuidOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] GuidOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(GuidInput);
        [Benchmark]
        public System.Guid DeserializeGuidWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Guid>(GuidOutputOfUtf8JsonSerializer);
        }

        private static readonly String StringOutputOfJilSerializer = JilSerializer.Serialize(StringInput);
        [Benchmark]
        public System.String DeserializeStringWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.String>(StringOutputOfJilSerializer);
        }

        private static readonly String StringOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(StringInput);
        [Benchmark]
        public System.String DeserializeStringWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.String>(StringOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] StringOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(StringInput);
        [Benchmark]
        public System.String DeserializeStringWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.String>(StringOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] StringOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StringInput);
        [Benchmark]
        public System.String DeserializeStringWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.String>(StringOutputOfUtf8JsonSerializer);
        }

        private static readonly String DecimalOutputOfJilSerializer = JilSerializer.Serialize(DecimalInput);
        [Benchmark]
        public System.Decimal DeserializeDecimalWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Decimal>(DecimalOutputOfJilSerializer);
        }

        private static readonly String DecimalOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(DecimalInput);
        [Benchmark]
        public System.Decimal DeserializeDecimalWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Decimal>(DecimalOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] DecimalOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(DecimalInput);
        [Benchmark]
        public System.Decimal DeserializeDecimalWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Decimal>(DecimalOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] DecimalOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(DecimalInput);
        [Benchmark]
        public System.Decimal DeserializeDecimalWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Decimal>(DecimalOutputOfUtf8JsonSerializer);
        }

        private static readonly String VersionOutputOfJilSerializer = JilSerializer.Serialize(VersionInput);
        [Benchmark]
        public System.Version DeserializeVersionWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Version>(VersionOutputOfJilSerializer);
        }

        private static readonly String VersionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(VersionInput);
        [Benchmark]
        public System.Version DeserializeVersionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Version>(VersionOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] VersionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(VersionInput);
        [Benchmark]
        public System.Version DeserializeVersionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Version>(VersionOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] VersionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(VersionInput);
        [Benchmark]
        public System.Version DeserializeVersionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Version>(VersionOutputOfUtf8JsonSerializer);
        }

        private static readonly String UriOutputOfJilSerializer = JilSerializer.Serialize(UriInput);
        [Benchmark]
        public System.Uri DeserializeUriWithJilSerializer()
        {
            return JilSerializer.Deserialize<System.Uri>(UriOutputOfJilSerializer);
        }

        private static readonly String UriOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UriInput);
        [Benchmark]
        public System.Uri DeserializeUriWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<System.Uri>(UriOutputOfSpanJsonSerializer);
        }

        private static readonly Byte[] UriOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UriInput);
        [Benchmark]
        public System.Uri DeserializeUriWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<System.Uri>(UriOutputOfSpanJsonUtf8Serializer);
        }

        private static readonly Byte[] UriOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UriInput);
        [Benchmark]
        public System.Uri DeserializeUriWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<System.Uri>(UriOutputOfUtf8JsonSerializer);
        }

    }
}
