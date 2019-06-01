using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract partial class ComplexFormatter : BaseFormatter
    {
        /// <summary>
        /// Creates the serializer for both utf8 and utf16
        /// There should not be a large difference between utf8 and utf16 besides member names
        /// </summary>
        protected static SerializeAsyncDelegate<T, TSymbol> BuildSerializeAsyncDelegate<T, TSymbol, TResolver>()
            where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
        {
            var resolver = StandardResolvers.GetResolver<TSymbol, TResolver>();
            var objectDescription = resolver.GetObjectDescription<T>();
            var memberInfos = objectDescription.Where(a => a.CanRead).ToList();
            var writerParameter = Expression.Parameter(typeof(AsyncWriter<TSymbol>), "asyncWriter");
            var stateParameter = Expression.Parameter(typeof(int).MakeByRefType(), "state");
            var cancellationTokenParameter = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var expressions = new List<Expression>();
            var returnLabel = Expression.Label(typeof(ValueTask), "result");
            var labels = new LabelTarget[memberInfos.Count];
            var cases = new List<SwitchCase>();
            for (var i = 0; i < memberInfos.Count; i++)
            {
                labels[i] = Expression.Label(i.ToString());
                var switchCase = Expression.SwitchCase(Expression.Goto(labels[i]), Expression.Constant(i));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(stateParameter, cases.ToArray());
            expressions.Add(switchExpression);
            var vTaskVariable = Expression.Variable(typeof(ValueTask));
            for (var i = 0; i < memberInfos.Count; i++)
            {
                var memberInfo = memberInfos[i];
                var formatterType = resolver.GetFormatter(memberInfo).GetType();
                Expression serializerInstance = null;
                Expression memberExpression = Expression.PropertyOrField(valueParameter, memberInfo.MemberName);
                var parameterExpressions = new List<Expression> {writerParameter, memberExpression};
                var fieldInfo = formatterType.GetField("Default", BindingFlags.Static | BindingFlags.Public);
                var serializeMethodInfo = FindPublicInstanceMethod(formatterType, "SerializeAsync", writerParameter.Type, memberInfo.MemberType,
                    cancellationTokenParameter.Type);
                if (serializeMethodInfo != null)
                {
                    serializerInstance = Expression.Field(null, fieldInfo);
                }
                else
                {
                    serializeMethodInfo = typeof(BaseFormatter)
                        .GetMethod(nameof(SerializeFallbackAsync), BindingFlags.NonPublic | BindingFlags.Static)
                        .MakeGenericMethod(memberInfo.MemberType, typeof(TSymbol), typeof(TResolver));
                    parameterExpressions.Add(Expression.Field(null, fieldInfo));
                }
                parameterExpressions.Add(cancellationTokenParameter);
                expressions.Add(Expression.Assign(stateParameter, Expression.Constant(i)));
                expressions.Add(Expression.Assign(vTaskVariable, Expression.Call(serializerInstance, serializeMethodInfo, parameterExpressions)));
                var needsAwait = Expression.IfThen(Expression.IsFalse(Expression.Property(vTaskVariable, nameof(ValueTask.IsCompletedSuccessfully))),
                    Expression.Goto(returnLabel, vTaskVariable));
                expressions.Add(needsAwait);
                expressions.Add(Expression.Label(labels[i]));
            }
            expressions.Add(Expression.Assign(stateParameter, Expression.Constant(-1)));
            expressions.Add(Expression.Label(returnLabel, Expression.Default(typeof(ValueTask))));
            var blockExpression = Expression.Block(new[] {vTaskVariable}, expressions);
            var lambda =
                Expression.Lambda<SerializeAsyncDelegate<T, TSymbol>>(blockExpression, writerParameter, stateParameter, valueParameter,
                    cancellationTokenParameter);
            return lambda.Compile();
        }
    }
}
