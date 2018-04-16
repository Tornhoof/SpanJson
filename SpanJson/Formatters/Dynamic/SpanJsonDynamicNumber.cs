using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using SpanJson.Helpers;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicNumber : DynamicObject
    {
        public sealed class DynamicTypeConverter : TypeConverter
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters =
                ConvertDelegateHelper.BuildConverters(typeof(NumberParser));

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return false;
            }

            public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            {
                return false;
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return IsSupported(destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
                Type destinationType)
            {
                var input = (SpanJsonDynamicNumber) value;
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
                    var result = del(span, out value);
                    return result;
                }

                value = default;
                return false;
            }

            public static bool IsSupported(Type type) => Converters.ContainsKey(type);
        }


        private readonly char[] _chars;

        private static readonly DynamicTypeConverter Converter =
            (DynamicTypeConverter) TypeDescriptor.GetConverter(typeof(SpanJsonDynamicNumber));

        public SpanJsonDynamicNumber(ReadOnlySpan<char> span)
        {
            _chars = span.ToArray();
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