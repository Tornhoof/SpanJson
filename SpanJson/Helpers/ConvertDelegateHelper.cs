using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SpanJson.Helpers
{
    public delegate bool ConvertDelegate(in ReadOnlySpan<char> span, out object value);
    public static class ConvertDelegateHelper
    {
        public static Dictionary<Type, ConvertDelegate> BuildConverters(Type parserType)
        {
            var result = new Dictionary<Type, ConvertDelegate>();
            var staticMethods = parserType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var staticMethod in staticMethods.Where(a =>
                a.Name.StartsWith("TryParse") && a.ReturnType == typeof(bool) && a.GetParameters().Length == 2))
            {
                var parameters = staticMethod.GetParameters();
                if (parameters[0].ParameterType == typeof(ReadOnlySpan<char>).MakeByRefType() && parameters[1].IsOut)
                {
                    var spanExpression = Expression.Parameter(parameters[0].ParameterType, "span");
                    var tempExpression = Expression.Parameter(parameters[1].ParameterType.GetElementType(), "temp");
                    var outExpression = Expression.Parameter(typeof(object).MakeByRefType());
                    var callExpression = Expression.Call(null, staticMethod, spanExpression, tempExpression);
                    var resultExpression = Expression.Parameter(typeof(bool), "result");
                    var returnTarget = Expression.Label(resultExpression.Type);
                    var variables = new ParameterExpression[] { resultExpression, tempExpression };
                    var block = Expression.Block(variables,
                        Expression.Assign(resultExpression, callExpression),
                        Expression.IfThenElse(resultExpression,
                            Expression.Assign(outExpression, Expression.Convert(tempExpression, typeof(object))),
                            Expression.Assign(outExpression, Expression.Constant(null))),
                        Expression.Label(returnTarget, resultExpression)
                    );
                    var lambda = Expression.Lambda<ConvertDelegate>(block, spanExpression,
                        outExpression);
                    result.Add(tempExpression.Type, lambda.Compile());
                }
            }
            return result;
        }     
    }
}
