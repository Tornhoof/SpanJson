using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace SpanJson
{
    public struct TypedSerializer<T>
    {
        internal delegate void SerializeDelegate(in ValueStringBuilder builder, T value);

        internal static readonly SerializeDelegate Serializer = BuildSerializeDelegate();

        public static string Serialize(T input)
        {
            Span<char> span = stackalloc char[100];
            ValueStringBuilder vsb = new ValueStringBuilder(span);
            vsb.Append('{');
            Serializer(in vsb, input);
            vsb.Append('}');
            return vsb.ToString();
        }

        private static MethodInfo FindAppendMethod(Type type)
        {
            return typeof(ValueStringBuilder).GetMethod("Append", new Type[] {type});
        }

        private static bool IsListOrArray(Type type, out Type elementType)
        {
            if (type.IsArray)
            {
                elementType = type.GetElementType();
                return true;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            elementType = null;
            return false;
        }

        private static Expression ForEach(Expression collection, ParameterExpression loopVar, Expression loopContent)
        {
            var elementType = loopVar.Type;
            var enumerableType = typeof(IEnumerable<>).MakeGenericType(elementType);
            var enumeratorType = typeof(IEnumerator<>).MakeGenericType(elementType);

            var enumeratorVar = Expression.Variable(enumeratorType, "enumerator");
            var getEnumeratorCall = Expression.Call(collection, enumerableType.GetMethod("GetEnumerator"));
            var enumeratorAssign = Expression.Assign(enumeratorVar, getEnumeratorCall);

            // The MoveNext method's actually on IEnumerator, not IEnumerator<T>
            var moveNextCall = Expression.Call(enumeratorVar, typeof(IEnumerator).GetMethod("MoveNext"));

            var breakLabel = Expression.Label("LoopBreak");

            var loop = Expression.Block(new[] {enumeratorVar},
                enumeratorAssign,
                Expression.Loop(
                    Expression.IfThenElse(
                        Expression.Equal(moveNextCall, Expression.Constant(true)),
                        Expression.Block(new[] {loopVar},
                            Expression.Assign(loopVar, Expression.Property(enumeratorVar, "Current")),
                            loopContent
                        ),
                        Expression.Break(breakLabel)
                    ),
                    breakLabel)
            );

            return loop;
        }

        private static SerializeDelegate BuildSerializeDelegate()
        {
            var builderParameter = Expression.Parameter(typeof(ValueStringBuilder).MakeByRefType(), "builder");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var props = typeof(T).GetProperties();
            var expressions = new List<Expression>();
            int counter = 0;
            foreach (var propertyInfo in props)
            {
                var propertyExpression = Expression.Property(valueParameter, propertyInfo);
                var propertType = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
                if (propertType != propertyInfo.PropertyType)
                {
                    propertyExpression = Expression.Property(propertyExpression, "Value");

                }

                var writeJsonBlockExpressions = new List<Expression>();
                var isArray = IsListOrArray(propertType, out var elementType);
                if (isArray)
                {
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(string)),
                        Expression.Constant(propertyInfo.Name)));
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                        Expression.Constant(':')));
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                        Expression.Constant('[')));

                    var paramExpression = Expression.Parameter(elementType, "loopVar");
                    var loopExpressions = new List<Expression>
                    {
                        Expression.Call(builderParameter, FindAppendMethod(elementType), paramExpression),
                        Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                            Expression.Constant(','))
                    };
                    writeJsonBlockExpressions.Add(ForEach(propertyExpression, paramExpression,
                        Expression.Block(loopExpressions)));
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                        Expression.Constant(']')));
                }
                else
                {
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(string)),
                        Expression.Constant(propertyInfo.Name)));
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                        Expression.Constant(':')));
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter,
                        FindAppendMethod(propertType), propertyExpression));
                }

                if (counter++ < props.Length - 1)
                {
                    writeJsonBlockExpressions.Add(Expression.Call(builderParameter, FindAppendMethod(typeof(char)),
                        Expression.Constant(',')));
                }

                if (propertType.IsNullable())
                {
                    expressions.Add(Expression.IfThen(
                        Expression.ReferenceNotEqual(propertyExpression, Expression.Constant(null)),
                        Expression.Block(writeJsonBlockExpressions)));
                }
                else
                {
                    expressions.AddRange(writeJsonBlockExpressions);
                }
            }

            var blockExpression = Expression.Block(expressions);
            var lambda = Expression.Lambda<SerializeDelegate>(blockExpression, builderParameter, valueParameter);
            return lambda.Compile();
        }
    }

    public static class TypeExtensions
    {
        public static bool IsNullable(this Type type)
        {
            return type.IsClass || Nullable.GetUnderlyingType(type) != null;
        }
    }
}