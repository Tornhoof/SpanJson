using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;
using SpanJson.Helpers;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class BaseDynamicTypeConverter<TSymbol> : TypeConverter where TSymbol : struct
    {
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
            if (value == null)
            {
                if (destinationType.IsNullable())
                {
                    return null;
                }
            }
            else
            {
                destinationType = Nullable.GetUnderlyingType(destinationType) ?? destinationType;
                var input = (ISpanJsonDynamicValue<TSymbol>) value;
                if (TryConvertTo(destinationType, input.Symbols, out var temp))
                {
                    return temp;
                }
            }

            throw new InvalidCastException();
        }

        public abstract bool TryConvertTo(Type destinationType, ReadOnlySpan<TSymbol> span, out object value);

        public abstract bool IsSupported(Type destinationType);

        protected static Dictionary<Type, ConvertDelegate> BuildDelegates(Type[] allowedTypes)
        {
            var result = new Dictionary<Type, ConvertDelegate>();
            string utfType;
            if (typeof(TSymbol) == typeof(char))
            {
                utfType = "Utf16";
            }

            else if (typeof(TSymbol) == typeof(byte))
            {
                utfType = "Utf8";
            }
            else
            {
                throw new NotSupportedException();
            }

            foreach (var allowedType in allowedTypes)
            {
                var method = typeof(JsonReader<TSymbol>).GetMethod($"Read{utfType}{allowedType.Name}");
                if (method != null)
                {
                    var parameter = Expression.Parameter(typeof(JsonReader<TSymbol>).MakeByRefType(), "reader");
                    var lambda = Expression.Lambda<ConvertDelegate>(
                        Expression.Convert(Expression.Call(parameter, method), typeof(object)), parameter);
                    result.Add(allowedType, lambda.Compile());

                    if (allowedType.IsValueType)
                    {
                        var methodIsNull = typeof(JsonReader<TSymbol>).GetMethod($"Read{utfType}IsNull");
                        var conditionExpression = Expression.Condition(Expression.IsTrue(Expression.Call(parameter, methodIsNull)),
                            Expression.Constant(null),
                            Expression.Convert(Expression.Call(parameter, method), typeof(object)));
                        lambda = Expression.Lambda<ConvertDelegate>(conditionExpression, parameter);
                        result.Add(typeof(Nullable<>).MakeGenericType(allowedType), lambda.Compile());
                    }
                }

            }

            return result;
        }

        protected delegate object ConvertDelegate(ref JsonReader<TSymbol> reader);
    }
}