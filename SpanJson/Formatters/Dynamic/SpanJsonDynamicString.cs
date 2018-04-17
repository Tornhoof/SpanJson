using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicString : DynamicObject, ISpanJsonDynamicValue
    {
        private static readonly DynamicTypeConverter Converter =
            (DynamicTypeConverter) TypeDescriptor.GetConverter(typeof(SpanJsonDynamicString));


        private readonly int _escapedChars;

        public SpanJsonDynamicString(ReadOnlySpan<char> span, int escapedChars)
        {
            Chars = span.ToArray();
            _escapedChars = escapedChars;
        }

        public char[] Chars { get; }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = Nullable.GetUnderlyingType(binder.ReturnType) ?? binder.ReturnType;
            return Converter.TryConvertTo(returnType, Chars, out result);
        }

        public override string ToString()
        {
            return new string(Chars);
        }

        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            public override bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value)
            {
                var reader = new JsonParser(span);
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    value = del(reader);
                    return true;
                }

                if (destinationType == typeof(string))
                {
                    value = reader.ReadString();
                    return true;
                }

                if (destinationType.IsEnum)
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

            public override bool IsSupported(Type type)
            {
                return Converters.ContainsKey(type) || type == typeof(string) ||
                       type.IsEnum;
            }

            private static Dictionary<Type, ConvertDelegate> BuildDelegates()
            {
                var allowedTypes = new[]
                {
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
        }
    }
}