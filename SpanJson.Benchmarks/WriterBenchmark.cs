﻿  
  
  
  
  
  
  
 
using System;
using BenchmarkDotNet.Attributes;
using SpanJson.Shared.Fixture;

namespace SpanJson.Benchmarks
{
    // Autogenerated
    // ReSharper disable BuiltInTypeReferenceStyle
    [MemoryDiagnoser]
    [DisassemblyDiagnoser]
    public class WriterBenchmark
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();

        private static readonly SByte SByteInput = ExpressionTreeFixture.Create<SByte>();

        private static readonly Int16 Int16Input = ExpressionTreeFixture.Create<Int16>();

        private static readonly Int32 Int32Input = ExpressionTreeFixture.Create<Int32>();

        private static readonly Int64 Int64Input = ExpressionTreeFixture.Create<Int64>();

        private static readonly Byte ByteInput = ExpressionTreeFixture.Create<Byte>();

        private static readonly UInt16 UInt16Input = ExpressionTreeFixture.Create<UInt16>();

        private static readonly UInt32 UInt32Input = ExpressionTreeFixture.Create<UInt32>();

        private static readonly UInt64 UInt64Input = ExpressionTreeFixture.Create<UInt64>();

        private static readonly Single SingleInput = ExpressionTreeFixture.Create<Single>();

        private static readonly Double DoubleInput = ExpressionTreeFixture.Create<Double>();

        private static readonly Boolean BooleanInput = ExpressionTreeFixture.Create<Boolean>();

        private static readonly Char CharInput = ExpressionTreeFixture.Create<Char>();

        private static readonly DateTime DateTimeInput = ExpressionTreeFixture.Create<DateTime>();

        private static readonly DateTimeOffset DateTimeOffsetInput = ExpressionTreeFixture.Create<DateTimeOffset>();

        private static readonly TimeSpan TimeSpanInput = ExpressionTreeFixture.Create<TimeSpan>();

        private static readonly Guid GuidInput = ExpressionTreeFixture.Create<Guid>();

        private static readonly String StringInput = ExpressionTreeFixture.Create<String>();

        private static readonly Decimal DecimalInput = ExpressionTreeFixture.Create<Decimal>();

        private static readonly Version VersionInput = ExpressionTreeFixture.Create<Version>();

        private static readonly Uri UriInput = ExpressionTreeFixture.Create<Uri>();

        [Benchmark]
        public void WriteUtf8SByte()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8SByte(SByteInput);
        }

        [Benchmark]
        public void WriteUtf16SByte()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16SByte(SByteInput);
        }

        [Benchmark]
        public void WriteUtf8Int16()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Int16(Int16Input);
        }

        [Benchmark]
        public void WriteUtf16Int16()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Int16(Int16Input);
        }

        [Benchmark]
        public void WriteUtf8Int32()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Int32(Int32Input);
        }

        [Benchmark]
        public void WriteUtf16Int32()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Int32(Int32Input);
        }

        [Benchmark]
        public void WriteUtf8Int64()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Int64(Int64Input);
        }

        [Benchmark]
        public void WriteUtf16Int64()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Int64(Int64Input);
        }

        [Benchmark]
        public void WriteUtf8Byte()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Byte(ByteInput);
        }

        [Benchmark]
        public void WriteUtf16Byte()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Byte(ByteInput);
        }

        [Benchmark]
        public void WriteUtf8UInt16()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8UInt16(UInt16Input);
        }

        [Benchmark]
        public void WriteUtf16UInt16()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16UInt16(UInt16Input);
        }

        [Benchmark]
        public void WriteUtf8UInt32()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8UInt32(UInt32Input);
        }

        [Benchmark]
        public void WriteUtf16UInt32()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16UInt32(UInt32Input);
        }

        [Benchmark]
        public void WriteUtf8UInt64()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8UInt64(UInt64Input);
        }

        [Benchmark]
        public void WriteUtf16UInt64()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16UInt64(UInt64Input);
        }

        [Benchmark]
        public void WriteUtf8Single()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Single(SingleInput);
        }

        [Benchmark]
        public void WriteUtf16Single()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Single(SingleInput);
        }

        [Benchmark]
        public void WriteUtf8Double()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Double(DoubleInput);
        }

        [Benchmark]
        public void WriteUtf16Double()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Double(DoubleInput);
        }

        [Benchmark]
        public void WriteUtf8Boolean()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Boolean(BooleanInput);
        }

        [Benchmark]
        public void WriteUtf16Boolean()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Boolean(BooleanInput);
        }

        [Benchmark]
        public void WriteUtf8Char()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Char(CharInput);
        }

        [Benchmark]
        public void WriteUtf16Char()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Char(CharInput);
        }

        [Benchmark]
        public void WriteUtf8DateTime()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8DateTime(DateTimeInput);
        }

        [Benchmark]
        public void WriteUtf16DateTime()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16DateTime(DateTimeInput);
        }

        [Benchmark]
        public void WriteUtf8DateTimeOffset()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8DateTimeOffset(DateTimeOffsetInput);
        }

        [Benchmark]
        public void WriteUtf16DateTimeOffset()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16DateTimeOffset(DateTimeOffsetInput);
        }

        [Benchmark]
        public void WriteUtf8TimeSpan()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8TimeSpan(TimeSpanInput);
        }

        [Benchmark]
        public void WriteUtf16TimeSpan()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16TimeSpan(TimeSpanInput);
        }

        [Benchmark]
        public void WriteUtf8Guid()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Guid(GuidInput);
        }

        [Benchmark]
        public void WriteUtf16Guid()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Guid(GuidInput);
        }

        [Benchmark]
        public void WriteUtf8String()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8String(StringInput);
        }

        [Benchmark]
        public void WriteUtf16String()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16String(StringInput);
        }

        [Benchmark]
        public void WriteUtf8Decimal()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Decimal(DecimalInput);
        }

        [Benchmark]
        public void WriteUtf16Decimal()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Decimal(DecimalInput);
        }

        [Benchmark]
        public void WriteUtf8Version()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Version(VersionInput);
        }

        [Benchmark]
        public void WriteUtf16Version()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Version(VersionInput);
        }

        [Benchmark]
        public void WriteUtf8Uri()
        {
            var writer = new JsonWriter<Byte>(32);
            writer.WriteUtf8Uri(UriInput);
        }

        [Benchmark]
        public void WriteUtf16Uri()
        {
            var writer = new JsonWriter<Char>(32);
            writer.WriteUtf16Uri(UriInput);
        }

    }
}
  