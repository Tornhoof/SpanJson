using System;
using System.ComponentModel;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicUtf16String : SpanJsonDynamicString<char>
    {
        public SpanJsonDynamicUtf16String(in ReadOnlySpan<char> span) : base(span)
        {
        }
    }
}