using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using SpanJson.Helpers;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicString : DynamicObject
    {
        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            private static Dictionary<Type, ConvertDelegate> BuildDelegates()
            {
                var allowedTypes = new Type[] {
                    typeof(char),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid),
                    typeof(string),
                    typeof(Version),
                    typeof(Uri)
                };
                return BuildDelegates(allowedTypes);
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
                return IsSupported(destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                Type destinationType)
            {
                var input = (SpanJsonDynamicString)value;
                if (TryConvertTo(destinationType, input._chars, out var temp))
                {
                    return temp;
                }

                throw new InvalidCastException();
            }

            public bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value)
            {
                var reader = new JsonReader(span);
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    value = del(reader);
                    return true;
                }
                else if (destinationType == typeof(string))
                {
                    value = reader.ReadString();
                    return true;
                }
                else if (destinationType.IsEnum)
                {
                    // TODO: Optimize
                    var data = reader.ReadString();
                    if (Enum.TryParse(destinationType, data, out var enumValue))
                    {
                        value = enumValue;
                        return true;
                    }

                    value = default;
                    return false;
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
            (DynamicTypeConverter)TypeDescriptor.GetConverter(typeof(SpanJsonDynamicString));

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
            return new string(_chars);
        }
    }
}