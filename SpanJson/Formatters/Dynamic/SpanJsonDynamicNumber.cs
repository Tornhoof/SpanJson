using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace SpanJson.Formatters.Dynamic
{
    [TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicNumber : DynamicObject, ISpanJsonDynamicValue
    {
        private static readonly DynamicTypeConverter Converter =
            (DynamicTypeConverter) TypeDescriptor.GetConverter(typeof(SpanJsonDynamicNumber));

        public SpanJsonDynamicNumber(ReadOnlySpan<char> span)
        {
            Chars = span.ToArray();
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


            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return IsSupported(destinationType);
            }

            public override bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value)
            {
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    var reader = new JsonParser(span);
                    value = del(reader);
                    return true;
                }

                value = default;
                return false;
            }

            public override bool IsSupported(Type type)
            {
                return Converters.ContainsKey(type);
            }

            private static Dictionary<Type, ConvertDelegate> BuildDelegates()
            {
                var allowedTypes = new[]
                {
                    typeof(sbyte),
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(byte),
                    typeof(ushort),
                    typeof(uint),
                    typeof(ulong),
                    typeof(float),
                    typeof(double),
                    typeof(decimal)
                };
                return BuildDelegates(allowedTypes);
            }
        }
    }
}