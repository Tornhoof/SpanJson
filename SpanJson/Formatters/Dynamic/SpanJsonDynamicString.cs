using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace SpanJson.Formatters.Dynamic
{
    public sealed class SpanJsonDynamicString<TSymbol> : DynamicObject, ISpanJsonDynamicValue where TSymbol : struct 
    {
        private static readonly DynamicTypeConverter Converter = new DynamicTypeConverter();

        public SpanJsonDynamicString(ReadOnlySpan<char> span)
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

        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter<TSymbol>
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            public override bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value)
            {
                try
                {
                    var reader = new JsonReader<TSymbol>(span);
                    if (Converters.TryGetValue(destinationType, out var del))
                    {
                        value = del(reader);
                        return true;
                    }

                    if (destinationType == typeof(string))
                    {
                        value = reader.ReadUtf16String();
                        return true;
                    }

                    if (destinationType.IsEnum)
                    {
                        // TODO: Optimize
                        var data = reader.ReadUtf16String();
                        if (Enum.TryParse(destinationType, data, out var enumValue))
                        {
                            value = enumValue;
                            return true;
                        }
                    }
                }
                catch (Exception)
                {
                }

                value = default;
                return false;
            }

            public override bool IsSupported(Type type)
            {
                var fix = Converters.ContainsKey(type) || type == typeof(string) || type.IsEnum;
                if (!fix)
                {
                    var nullable = Nullable.GetUnderlyingType(type);
                    if (nullable != null)
                    {
                        fix |= IsSupported(nullable);
                    }
                }

                return fix;
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