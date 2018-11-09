using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

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

        protected static string GetFormattedValue(object enumValue)
        {
            var name = enumValue.ToString();
            return typeof(T).GetMember(name)?.FirstOrDefault()?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? name;
        }

        protected delegate void SerializeDelegate(ref JsonWriter<TSymbol> writer, T value);
    }
}