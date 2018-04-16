using System;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;

namespace SpanJson.Helpers
{
    public sealed class SpanJsonDynamicString : DynamicObject
    {
        class DynamicTypeConverter : TypeConverter
        {
            private readonly SpanJsonDynamicString _dynamicString;

            public DynamicTypeConverter(SpanJsonDynamicString dynamicString)
            {
                _dynamicString = dynamicString;
            }

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
                return true;
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                return null;
            }
        }


        private readonly int _escapedChars;
        private readonly char[] _chars;

        public SpanJsonDynamicString(ReadOnlySpan<char> span, int escapedChars)
        {
            _chars = span.ToArray();
            _escapedChars = escapedChars;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = Nullable.GetUnderlyingType(binder.ReturnType) ?? binder.ReturnType;
            if (returnType == typeof(DateTime))
            {
                if (DateTimeParser.TryParseDateTime(_chars, out var dt, out _))
                {
                    result = dt;
                    return true;
                }
            }
            else if (returnType == typeof(DateTimeOffset))
            {
                if (DateTimeParser.TryParseDateTimeOffset(_chars, out var dto, out _))
                {
                    result = dto;
                    return true;
                }
            }
            else if (returnType == typeof(TimeSpan))
            {
                if (TimeSpan.TryParse(_chars, out var ts))
                {
                    result = ts;
                    return true;
                }
            }
            else if (returnType == typeof(Guid))
            {
                if (Guid.TryParse(_chars, out var guid))
                {
                    result = guid;
                    return true;
                }
            }
            else if (returnType == typeof(string))
            {
                result = new string(_chars);
                return true;
            }
            else if (returnType == typeof(Version))
            {
                if (Version.TryParse(_chars, out var version))
                {
                    result = version;
                    return true;
                }
            }
            else if (returnType == typeof(Uri))
            {
                var data = new string(_chars);
                if (Uri.TryCreate(data, UriKind.RelativeOrAbsolute, out var uri))
                {
                    result = uri;
                    return true;
                }
            }
            else if (returnType.IsEnum)
            {
                var data = new string(_chars);
                if (Enum.TryParse(returnType, data, out var enumValue))
                {
                    result = enumValue;
                    return true;
                }
            }

            return base.TryConvert(binder, out result);
        }
    }
}