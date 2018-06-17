using System;
using System.ComponentModel;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicUtf8Number : SpanJsonDynamicNumber<byte>
    {
        public SpanJsonDynamicUtf8Number(in ReadOnlySpan<byte> span) : base(span)
        {
        }
    }
}