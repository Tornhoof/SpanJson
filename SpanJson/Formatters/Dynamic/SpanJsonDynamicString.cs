using System;
using System.Collections.Generic;
namespace SpanJson.Formatters.Dynamic
{
    /// <summary>
    /// We should autogenerate the conversions etc.
    /// </summary>
    public abstract class SpanJsonDynamicString<TSymbol> : SpanJsonDynamic<TSymbol> where TSymbol : struct
    {
        private static readonly DynamicTypeConverter DynamicConverter = new DynamicTypeConverter();

        protected SpanJsonDynamicString(ReadOnlySpan<TSymbol> span) : base(span)
        {
        }

        protected override BaseDynamicTypeConverter<TSymbol> Converter => DynamicConverter;

        public sealed class DynamicTypeConverter : BaseDynamicTypeConverter<TSymbol>
        {
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildDelegates();

            public override bool TryConvertTo(Type destinationType, in ReadOnlySpan<TSymbol> span, out object value)
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

        // ReSharper disable PossibleNullReferenceException
        public static implicit operator char(SpanJsonDynamicString<TSymbol> input)
        {
            return (char) DynamicConverter.ConvertTo(input, typeof(char));
        }

        public static implicit operator DateTime(SpanJsonDynamicString<TSymbol> input)
        {
            return (DateTime) DynamicConverter.ConvertTo(input, typeof(DateTime));
        }

        public static implicit operator DateTimeOffset(SpanJsonDynamicString<TSymbol> input)
        {
            return (DateTimeOffset) DynamicConverter.ConvertTo(input, typeof(DateTimeOffset));
        }

        public static implicit operator TimeSpan(SpanJsonDynamicString<TSymbol> input)
        {
            return (TimeSpan) DynamicConverter.ConvertTo(input, typeof(TimeSpan));
        }

        public static implicit operator Guid(SpanJsonDynamicString<TSymbol> input)
        {
            return (Guid) DynamicConverter.ConvertTo(input, typeof(Guid));
        }

        public static implicit operator string(SpanJsonDynamicString<TSymbol> input)
        {
            return (string) DynamicConverter.ConvertTo(input, typeof(string));
        }

        public static implicit operator Version(SpanJsonDynamicString<TSymbol> input)
        {
            return (Version) DynamicConverter.ConvertTo(input, typeof(Version));
        }

        public static implicit operator Uri(SpanJsonDynamicString<TSymbol> input)
        {
            return (Uri) DynamicConverter.ConvertTo(input, typeof(Uri));
        }
        // ReSharper restore PossibleNullReferenceException
    }
}