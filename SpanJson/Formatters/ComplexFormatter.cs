using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter
    {
        protected delegate void SerializeDelegate<in T>(ref JsonWriter writer, T value,
            IJsonFormatterResolver formatterResolver);

        protected delegate T DeserializeDelegate<out T>(ref JsonReader reader, IJsonFormatterResolver formatterResolver);

        /// <summary>
        /// if the propertyType is object, we need to do it during runtime
        /// if the type is RuntimeHelpers.IsReferenceOrContainsReferences -> false, we can do everything statically (struct and struct children case) 
        /// if the type is sealed or struct, then the type during generation is the type we can use for static lookup
        /// else we need to do runtime lookup
        /// </summary>
        protected static SerializeDelegate<T> BuildSerializeDelegate<T>()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var propertyInfos = typeof(T).GetProperties();
            var expressions = new List<Expression>();
            var propertyNameWriterMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteName));
            var seperatorWriteMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteSeparator));
            expressions.Add(Expression.Call(writerParameter, FindMethod(writerParameter.Type, nameof(JsonWriter.WriteObjectStart))));
            var isNotReferenceOrContainsReference = !RuntimeHelpers.IsReferenceOrContainsReferences<T>();
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfo = propertyInfos[i];
                Expression formatterExpression;
                MethodInfo serializeMethodInfo;
                if (isNotReferenceOrContainsReference || IsSealedOrStruct(propertyInfo.PropertyType))
                {
                    var formatter = DefaultResolver.Default.GetFormatter(propertyInfo.PropertyType);
                    formatterExpression = Expression.Constant(formatter);
                    serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
                }
                else
                {
                    var formatter = RuntimeFormatter.Default;
                    formatterExpression = Expression.Constant(formatter);
                    serializeMethodInfo = formatter.GetType().GetMethod("Serialize");
                }
                expressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo,
                    Expression.Constant(propertyInfo.Name)));
                expressions.Add(Expression.Call(formatterExpression,
                    serializeMethodInfo, writerParameter,
                    Expression.Property(valueParameter, propertyInfo),
                    resolverParameter));
                if (i != propertyInfos.Length - 1)
                {
                    expressions.Add(Expression.Call(writerParameter, seperatorWriteMethodInfo));
                }
            }

            expressions.Add(Expression.Call(writerParameter, FindMethod(writerParameter.Type, nameof(JsonWriter.WriteObjectEnd))));
            var blockExpression = Expression.Block(expressions);
            var lambda = Expression.Lambda<SerializeDelegate<T>>(blockExpression, writerParameter, valueParameter,
                resolverParameter);
            return lambda.Compile();
        }

        private static bool IsSealedOrStruct(Type type)
        {
            return type.IsValueType || type.IsSealed;
        }

        protected static int EstimateSize<T>()
        {
            var queue = new Queue<Type>();
            var alreadyseen = new HashSet<Type>();
            queue.Enqueue(typeof(T));
            var result = 0;
            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (alreadyseen.Add(current))
                {
                    var propertyInfos = current.GetProperties();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        result += propertyInfo.Name.Length + 3; // two quotes + :
                        result += 20; // find better estimation
                        if (propertyInfo.PropertyType.IsClass)
                        {
                            result += 4;
                            queue.Enqueue(propertyInfo.PropertyType);
                        }
                    }
                }
            }

            return result;
        }

        private static MethodInfo FindMethod(Type type, string name)
        {
            return type.GetMethod(name);
        }


        protected static DeserializeDelegate<T> BuildDeserializeDelegate<T>()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var propertyInfos = typeof(T).GetProperties();
            var cases = new List<SwitchCase>();
            var returnValue = Expression.Variable(typeof(T), "result");
            foreach (var propertyInfo in propertyInfos.Where(p => p.CanWrite))
            {
                var formatter = DefaultResolver.Default.GetFormatter(propertyInfo.PropertyType);
                var switchCase = Expression.SwitchCase(
                    Expression.Assign(Expression.Property(returnValue, propertyInfo.Name),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"),
                            readerParameter, resolverParameter)),
                    Expression.Constant(propertyInfo.Name));
                cases.Add(switchCase);
            }

            var equalityMethod =
                typeof(ComplexFormatter).GetMethod(nameof(IsEqual), BindingFlags.NonPublic | BindingFlags.Static);
            var switchExpression = Expression.Switch(typeof(void),
                Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.ReadNameSpan))), null,
                equalityMethod, cases.ToArray());
            var countExpression = Expression.Parameter(typeof(int), "count");
            var abortExpression = Expression.IsTrue(Expression.Call(readerParameter,
                readerParameter.Type.GetMethod(nameof(JsonReader.TryReadIsEndObjectOrValueSeparator)),
                countExpression));
            var readBeginObject = Expression.Call(readerParameter,
                FindMethod(readerParameter.Type, nameof(JsonReader.ReadBeginObjectOrThrow)));
            var loopAbort = Expression.Label(typeof(void));
            var returnTarget = Expression.Label(returnValue.Type);
            var block = Expression.Block(new ParameterExpression[] {returnValue, countExpression}, readBeginObject,
                Expression.Assign(returnValue, Expression.New(returnValue.Type)),
                Expression.Loop(
                    Expression.IfThenElse(abortExpression, Expression.Break(loopAbort),
                        switchExpression), loopAbort
                ),
                Expression.Label(returnTarget, returnValue)
            );

            var lambda = Expression.Lambda<DeserializeDelegate<T>>(block, readerParameter,
                resolverParameter);
            return lambda.Compile();
        }        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsEqual(ReadOnlySpan<char> span, string comparison)
        {
            //return span.Equals(comparison.AsSpan(), StringComparison.Ordinal);
            return span.SequenceEqual(comparison.AsSpan());
        }
    }
}