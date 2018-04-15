
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace SpanJson.Helpers
{
    public sealed class SpanJsonDynamicObject : DynamicObject
    {
        private readonly Dictionary<string, object> _dictionary;

        internal SpanJsonDynamicObject(Dictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _dictionary.TryGetValue(binder.Name, out result);
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys;
        }
    }

    public sealed class SpanJsonDynamicArray : DynamicObject
    {
        private readonly object[] _input;

        internal SpanJsonDynamicArray(object[] input)
        {
            _input = input;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (binder.Name == "Length")
            {
                result = _input.Length;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.ReturnType == typeof(IEnumerable))
            {
                result = _input;
                return true;
            }
            return base.TryConvert(binder, out result);
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            return base.TryGetIndex(binder, indexes, out result);
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            return base.TrySetIndex(binder, indexes, value);
        }
    }

    public sealed class SpanJsonDynamicString : DynamicObject
    {
        private int _escapedChars;
        private char[] _chars;

        public SpanJsonDynamicString(ReadOnlySpan<char> span, int escapedChars)
        {
            _chars = span.ToArray();
            _escapedChars = escapedChars;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.ReturnType == typeof(DateTime?))
            {
                if (DateTimeParser.TryParseDateTime(_chars, out var dt, out var charsConsumed))
                {
                    result = dt;
                    return true;
                }
            }
            return base.TryConvert(binder, out result);
        }
    }

    public sealed class SpanJsonDynamicNumber : DynamicObject
    {
        private char[] _chars;

        public SpanJsonDynamicNumber(ReadOnlySpan<char> span)
        {
            _chars = span.ToArray();
        }
    }
}
