using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class SpanJsonDynamicNumber<TSymbol> : SpanJsonDynamic<TSymbol> where TSymbol : struct
    {
        private static readonly DynamicTypeConverter DynamicConverter = new DynamicTypeConverter();

        protected SpanJsonDynamicNumber(ReadOnlySpan<TSymbol> span) : base(span)
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
                    if (Converters.TryGetValue(destinationType, out var del))
                    {
                        var reader = new JsonReader<TSymbol>(span);
                        value = del(reader);
                        return true;
                    }
                }
                catch
                {
                }

                value = default;
                return false;
            }

            public override bool IsSupported(Type type)
            {
                var fix = Converters.ContainsKey(type);
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

        // ReSharper disable PossibleNullReferenceException
        public static implicit operator sbyte(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (sbyte) DynamicConverter.ConvertTo(input, typeof(sbyte));
        }

        public static implicit operator short(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (short) DynamicConverter.ConvertTo(input, typeof(short));
        }

        public static implicit operator int(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (int) DynamicConverter.ConvertTo(input, typeof(int));
        }

        public static implicit operator long(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (long) DynamicConverter.ConvertTo(input, typeof(long));
        }

        public static implicit operator byte(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (byte) DynamicConverter.ConvertTo(input, typeof(byte));
        }

        public static implicit operator ushort(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (ushort) DynamicConverter.ConvertTo(input, typeof(ushort));
        }

        public static implicit operator uint(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (uint) DynamicConverter.ConvertTo(input, typeof(uint));
        }

        public static implicit operator ulong(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (ulong) DynamicConverter.ConvertTo(input, typeof(ulong));
        }

        public static implicit operator float(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (float) DynamicConverter.ConvertTo(input, typeof(float));
        }

        public static implicit operator double(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (double) DynamicConverter.ConvertTo(input, typeof(double));
        }

        public static implicit operator decimal(SpanJsonDynamicNumber<TSymbol> input)
        {
            return (decimal) DynamicConverter.ConvertTo(input, typeof(decimal));
        }
        // ReSharper restore PossibleNullReferenceException
    }
}