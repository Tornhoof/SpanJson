using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;

namespace SpanJson.Formatters.Dynamic
{
    public abstract class BaseDynamicTypeConverter : TypeConverter
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
            var input = (ISpanJsonDynamicValue) value;
            if (TryConvertTo(destinationType, input.Chars, out var temp))
            {
                return temp;
            }

            throw new InvalidCastException();
        }


        public abstract bool TryConvertTo(Type destinationType, in ReadOnlySpan<char> span, out object value);

        public abstract bool IsSupported(Type destinationType);

        protected static Dictionary<Type, ConvertDelegate> BuildDelegates(Type[] allowedTypes)
        {
            var result = new Dictionary<Type, ConvertDelegate>();
            foreach (var allowedType in allowedTypes)
            {
                var method = typeof(JsonParser).GetMethod($"Read{allowedType.Name}");
                if (method != null)
                {
                    var parameter = Expression.Parameter(typeof(JsonParser).MakeByRefType(), "reader");
                    var lambda = Expression.Lambda<ConvertDelegate>(
                        Expression.Convert(Expression.Call(parameter, method), typeof(object)), parameter);
                    result.Add(allowedType, lambda.Compile());
                }
            }

            return result;
        }

        protected delegate object ConvertDelegate(in JsonParser parser);
    }

    public interface ISpanJsonDynamicValue
    {
        char[] Chars { get; }
    }
}