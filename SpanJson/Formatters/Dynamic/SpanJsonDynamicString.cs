using System;
using System.Collections.Generic;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class SpanJsonDynamicString<TSymbol> : SpanJsonDynamic<TSymbol> where TSymbol : struct
    {
        private static readonly DynamicTypeConverter DynamicConverter = new DynamicTypeConverter();

        protected SpanJsonDynamicString(in ReadOnlySpan<TSymbol> span) : base(span)
        {
        }

        protected override BaseDynamicTypeConverter<TSymbol> Converter => DynamicConverter;

        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter<TSymbol>
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            public override bool TryConvertTo(Type destinationType, ReadOnlySpan<TSymbol> span, out object value)
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
                        value = reader.ReadString();
                        return true;
                    }

                    if (destinationType.IsEnum)
                    {
                        var data = reader.ReadString();
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