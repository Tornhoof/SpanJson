using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class SpanJsonDynamicString<TSymbol> : DynamicObject, ISpanJsonDynamicValue<TSymbol> where TSymbol : struct
    {
        private static readonly DynamicTypeConverter Converter = new DynamicTypeConverter();

        protected SpanJsonDynamicString(ReadOnlySpan<TSymbol> span)
        {
            Symbols = span.ToArray();
        }

        public TSymbol[] Symbols { get; }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = Nullable.GetUnderlyingType(binder.ReturnType) ?? binder.ReturnType;
            return Converter.TryConvertTo(returnType, Symbols, out result);
        }

        public override string ToString()
        {
            if (typeof(TSymbol) == typeof(char))
            {
                var temp = Symbols;
                var chars = Unsafe.As<TSymbol[], char[]>(ref temp);
                return new string(chars);
            }

            if (typeof(TSymbol) == typeof(byte))
            {
                var temp = Symbols;
                var bytes = Unsafe.As<TSymbol[], byte[]>(ref temp);
                return Encoding.UTF8.GetString(bytes);
            }

            throw new NotSupportedException();
        }

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
    }
}