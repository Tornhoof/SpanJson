using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class BaseEnumStringFormatterr<T, TSymbol> : BaseFormatter where T : Enum
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
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                Expression valueConstant;
                var formattedValue = escapeFunctor(GetFormattedValue(value));
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

                var switchCase = Expression.SwitchCase(Expression.Call(writerParameter, writerMethodInfo, valueConstant), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression =
                Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter, valueParameter);
            return lambdaExpression.Compile();
        }

        private static string GetFormattedValue(object enumValue)
        {
            var name = enumValue.ToString();
            return typeof(T).GetMember(name)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
        }

        protected static TDelegate BuildDeserializeDelegateExpressions<TDelegate>(ParameterExpression returnValue, BinaryExpression assignNameSpan,
            BinaryExpression lengthExpression, ParameterExpression lengthParameter, ParameterExpression byteNameSpan, List<ParameterExpression> parameters,
            ParameterExpression inputParameter)
        {
            var memberInfos = new List<JsonMemberInfo>();
            var dict = new Dictionary<string, T>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var formattedValue = GetFormattedValue(value);
                memberInfos.Add(new JsonMemberInfo(value.ToString(), typeof(T), null, formattedValue, false, true, false, null));
                dict.Add(value.ToString(), (T)value);
            }

            Expression MatchExpressionFunctor(JsonMemberInfo memberInfo)
            {
                var enumValue = dict[memberInfo.MemberName];
                return Expression.Assign(returnValue, Expression.Constant(enumValue));
            }
            var endOfBlockLabel = Expression.Label();
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
                Expression.Lambda<TDelegate>(blockExpression, inputParameter);
            return lambdaExpression.Compile();
        }



        protected delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);
    }
}