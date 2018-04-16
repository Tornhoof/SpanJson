using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;

namespace SpanJson.Helpers
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicString : DynamicObject
    {
        public sealed class DynamicTypeConverter : TypeConverter
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters =
                ConvertDelegateHelper.BuildConverters(typeof(StringParser));

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return false;
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return false;
            }

            public override bool IsValid(ITypeDescriptorContext context, object value)
            {
                return true;
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return IsSupported(destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                Type destinationType)
            {
                var input = (SpanJsonDynamicString) value;
                if (TryConvertTo(destinationType, input._chars, out var temp))
                {
                    return temp;
                }

                throw new InvalidCastException();
            }

            public bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value)
            {
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    return del(span, out value);
                }

                if (destinationType == typeof(string))
                {
                    value = new string(span);
                    return true;
                }

                if (destinationType.IsEnum)
                {
                    return StringParser.TryParseEnum(span, destinationType, out value);
                }

                value = default;
                return false;
            }

            public static bool IsSupported(Type type)
            {
                return Converters.ContainsKey(type) || type == typeof(string) ||
                       type.IsEnum;
            }
        }


        private readonly int _escapedChars;
        private readonly char[] _chars;

        private static readonly DynamicTypeConverter Converter =
            (DynamicTypeConverter) TypeDescriptor.GetConverter(typeof(SpanJsonDynamicString));

        public SpanJsonDynamicString(ReadOnlySpan<char> span, int escapedChars)
        {
            _chars = span.ToArray();
            _escapedChars = escapedChars;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = Nullable.GetUnderlyingType(binder.ReturnType) ?? binder.ReturnType;
            return Converter.TryConvertTo(returnType, _chars, out result);
        }

        public override string ToString()
        {
            return $"\"{new string(_chars)}\"";
        }
    }
}