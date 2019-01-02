using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class BaseEnumStringFormatter<T, TSymbol> : BaseFormatter where T : struct, Enum
        where TSymbol : struct
    {
        protected static SerializeDelegate BuildSerializeDelegate(Func<string, string> escapeFunctor)
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter<TSymbol>).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            MethodInfo writerMethodInfo;
            if (typeof(TSymbol) == typeof(char))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf16Verbatim), typeof(string));
            }
            else if (typeof(TSymbol) == typeof(byte))
            {
                writerMethodInfo = FindPublicInstanceMethod(writerParameter.Type, nameof(JsonWriter<TSymbol>.WriteUtf8Verbatim), typeof(byte[]));
            }
            else
            {
                throw new NotSupportedException();
            }

            var cases = new List<SwitchCase>();
            foreach (var name in Enum.GetNames(typeof(T)))
            {
                Expression valueConstant;
                var formattedValue = escapeFunctor(GetFormattedValue(name));
                if (typeof(TSymbol) == typeof(char))
                {
                    valueConstant = Expression.Constant(formattedValue);
                }
                else if (typeof(TSymbol) == typeof(byte))
                {
                    valueConstant = Expression.Constant(Encoding.UTF8.GetBytes(formattedValue));
                }
                else
                {
                    throw new NotSupportedException();
                }

                var value = Enum.Parse(typeof(T), name);
                var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, writerMethodInfo, valueConstant), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression =
                Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter, valueParameter);
            return lambdaExpression.Compile();
        }

        private static string GetFormattedValue(string enumValue)
        {
           return typeof(T).GetMember(enumValue)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? enumValue;
        }

        protected static TDelegate BuildDeserializeDelegateExpressions<TDelegate, TReturn>(ParameterExpression inputExpression, Expression nameSpanExpression)
        {
            var nameSpan = Expression.Variable(typeof(ReadOnlySpan<TSymbol>), "nameSpan");
            var returnValue = Expression.Variable(typeof(TReturn), "returnValue");
            var lengthParameter = Expression.Variable(typeof(int), "length");
            var endOfBlockLabel = Expression.Label();
            var assignNameSpan = Expression.Assign(nameSpan, nameSpanExpression);
            var lengthExpression = Expression.Assign(lengthParameter, Expression.PropertyOrField(nameSpan, "Length"));
            var byteNameSpan = Expression.Variable(typeof(ReadOnlySpan<byte>), "byteNameSpan");
            var parameters = new List<ParameterExpression> { nameSpan, lengthParameter, returnValue };
            if (typeof(TSymbol) == typeof(char))
            {
                var asBytesMethodInfo = FindGenericMethod(typeof(MemoryMarshal), nameof(MemoryMarshal.AsBytes), BindingFlags.Public | BindingFlags.Static,
                    typeof(char), typeof(ReadOnlySpan<>));
                nameSpanExpression = Expression.Call(null, asBytesMethodInfo, assignNameSpan);
                assignNameSpan = Expression.Assign(byteNameSpan, nameSpanExpression);
                parameters.Add(byteNameSpan);
            }
            else
            {
                byteNameSpan = nameSpan;
            }

            var memberInfos = new List<JsonMemberInfo>();
            var dict = new Dictionary<string, TReturn>();
            foreach (var name in Enum.GetNames(typeof(T)))
            {
                var formattedValue = GetFormattedValue(name);
                memberInfos.Add(new JsonMemberInfo(name, typeof(T), null, formattedValue, false, true, false, null, null));
                var value = Enum.Parse(typeof(T), name);
                dict.Add(name, (TReturn) Convert.ChangeType(value, typeof(TReturn)));
            }

            Expression MatchExpressionFunctor(JsonMemberInfo memberInfo)
            {
                var enumValue = dict[memberInfo.MemberName];
                return Expression.Assign(returnValue, Expression.Constant(enumValue));
            }

            var returnTarget = Expression.Label(returnValue.Type);
            var returnLabel = Expression.Label(returnTarget, returnValue);
            var expressions = new List<Expression>
            {
                assignNameSpan,
                lengthExpression,
                MemberComparisonBuilder.Build<TSymbol>(memberInfos, 0, lengthParameter, byteNameSpan, endOfBlockLabel, MatchExpressionFunctor),
                Expression.Throw(Expression.Constant(new InvalidOperationException())),
                Expression.Label(endOfBlockLabel),
                returnLabel
            };
            var blockExpression = Expression.Block(parameters, expressions);
            var lambdaExpression =
                Expression.Lambda<TDelegate>(blockExpression, inputExpression);
            return lambdaExpression.Compile();
        }



        protected delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);
    }
}