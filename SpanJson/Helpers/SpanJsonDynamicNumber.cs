using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SpanJson.Helpers
{
    //[TypeConverter(typeof(DynamicTypeConverter))]
    public sealed class SpanJsonDynamicNumber : DynamicObject
    {
        private class DynamicTypeConverter : TypeConverter
        {
            private readonly SpanJsonDynamicNumber _dynamicNumber;
            private static readonly Dictionary<Type, ConvertDelegate> Converters = BuildConverters();

            private static Dictionary<Type, ConvertDelegate> BuildConverters()
            {
                var result = new Dictionary<Type, ConvertDelegate>();
                var staticMethods = typeof(NumberParser).GetMethods(BindingFlags.Public | BindingFlags.Static);
                foreach (var staticMethod in staticMethods.Where(a =>
                    a.Name.StartsWith("TryParse") && a.ReturnType == typeof(bool) && a.GetParameters().Length == 2))
                {
                    var parameters = staticMethod.GetParameters();
                    if (parameters[0].ParameterType == typeof(ReadOnlySpan<char>).MakeByRefType() && parameters[1].IsOut)
                    {
                        var spanExpression = Expression.Parameter(parameters[0].ParameterType, "span");
                        var tempExpression = Expression.Parameter(parameters[1].ParameterType.GetElementType(), "temp");
                        var outExpression = Expression.Parameter(typeof(object));
                        var callExpression = Expression.Call(null, staticMethod, spanExpression, tempExpression);
                        var resultExpression = Expression.Parameter(typeof(bool), "result");
                        var returnTarget = Expression.Label(resultExpression.Type);
                        var variables = new ParameterExpression[] {outExpression, tempExpression, resultExpression };
                        var block = Expression.Block(variables,
                            Expression.IfThenElse(
                                callExpression,
                                Expression.Block(variables,
                                    Expression.Assign(outExpression, Expression.Convert(tempExpression, typeof(object))),
                                    Expression.Assign(resultExpression, Expression.Constant(true))),
                                Expression.Block(variables,
                                    Expression.Assign(outExpression, Expression.Constant(null)),
                                    Expression.Assign(resultExpression, Expression.Constant(false)))),
                            Expression.Label(returnTarget, resultExpression)
                        );
                        var lambda = Expression.Lambda<ConvertDelegate>(block, spanExpression,
                            outExpression);
                        result.Add(tempExpression.Type, lambda.Compile());
                    }
                }
                return result;
            }

            private delegate bool ConvertDelegate(in ReadOnlySpan<char> span, out object value);

            public DynamicTypeConverter(SpanJsonDynamicNumber dynamicNumber)
            {
                _dynamicNumber = dynamicNumber;
            }

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
                return Converters.ContainsKey(destinationType);
            }

            public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (TryConvertTo(destinationType, out var temp))
                {
                    return temp;
                }

                throw new InvalidCastException();
            }

            public bool TryConvertTo(Type destinationType, out object value)
            {
                if (Converters.TryGetValue(destinationType, out var del))
                {
                    var result =  del(_dynamicNumber._chars, out value);
                    return result;
                }

                value = default;
                return false;
            }
        }


        private readonly char[] _chars;
        private readonly DynamicTypeConverter _typeConverter;

        public SpanJsonDynamicNumber(ReadOnlySpan<char> span)
        {
            _chars = span.ToArray();
            _typeConverter = new DynamicTypeConverter(this);
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            var returnType = Nullable.GetUnderlyingType(binder.ReturnType) ?? binder.ReturnType;
            return _typeConverter.TryConvertTo(returnType, out result);
        }
    }
}