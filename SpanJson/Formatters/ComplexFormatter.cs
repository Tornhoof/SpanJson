using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter
    {
        protected delegate void SerializeDelegate<in T>(ref JsonWriter writer, T value,
            IJsonFormatterResolver formatterResolver);

        protected static SerializeDelegate<T> BuildSerializeDelegate<T>()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var propertyInfos = typeof(T).GetProperties();
            var expressions = new List<Expression>();
            var propertyNameWriterMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteName));
            var seperatorWriteMethodInfo = FindMethod(typeof(JsonWriter), nameof(JsonWriter.WriteSeparator));
            for (var i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfo = propertyInfos[i];
                var formatter = DefaultResolver.Default.GetFormatter(propertyInfo.PropertyType);
                expressions.Add(Expression.Call(writerParameter, propertyNameWriterMethodInfo,
                    Expression.Constant(propertyInfo.Name)));
                expressions.Add(Expression.Call(Expression.Constant(formatter),
                    formatter.GetType().GetMethod("Serialize"), writerParameter,
                    Expression.Property(valueParameter, propertyInfo),
                    resolverParameter));
                if (i != propertyInfos.Length - 1)
                    expressions.Add(Expression.Call(writerParameter, seperatorWriteMethodInfo));
            }

            var blockExpression = Expression.Block(expressions);
            var lambda = Expression.Lambda<SerializeDelegate<T>>(blockExpression, writerParameter, valueParameter,
                resolverParameter);
            return lambda.Compile();
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
    }

    public abstract class ComplexFormatter<T> : ComplexFormatter where T : new()
    {
        private delegate void AssignDelegate(ref JsonReader reader, T result,
            IJsonFormatterResolver formatterResolver);

        private static readonly AssignDelegate Assigner = BuildAssigner();

        protected T DeserializeInternal(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            reader.ReadBeginObjectOrThrow();
            int count = 0;
            var result = new T();
            while (!reader.TryReadIsEndObjectOrValueSeparator(ref count))
            {
                Assigner(ref reader, result, formatterResolver);
            }
            return result;
        }


        private static AssignDelegate BuildAssigner()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");
            var resultParameter = Expression.Parameter(typeof(T), "result");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var propertyInfos = typeof(T).GetProperties();
            var cases = new List<SwitchCase>();
            foreach (var propertyInfo in propertyInfos.Where(p => p.CanWrite))
            {
                var formatter = DefaultResolver.Default.GetFormatter(propertyInfo.PropertyType);
                var switchCase = Expression.SwitchCase(
                    Expression.Assign(Expression.Property(resultParameter, propertyInfo.Name),
                        Expression.Call(Expression.Constant(formatter), formatter.GetType().GetMethod("Deserialize"),
                            readerParameter, resolverParameter)),
                    Expression.Constant(propertyInfo.Name));
                cases.Add(switchCase);
            }

            var equalityMethod =
                typeof(ComplexFormatter<>).MakeGenericType(typeof(T)).GetMethod(nameof(IsEqual), BindingFlags.NonPublic | BindingFlags.Static);
            var switchExpression = Expression.Switch(typeof(void),
                Expression.Call(readerParameter, readerParameter.Type.GetMethod(nameof(JsonReader.ReadNameSpan))), null,
                equalityMethod, cases.ToArray());
            var lambda = Expression.Lambda<AssignDelegate>(switchExpression, readerParameter, resultParameter,
                resolverParameter);
            return lambda.Compile();
        }

        private static bool IsEqual(ReadOnlySpan<char> span, string comparison)
        {
            return span.Equals(comparison.AsSpan(), StringComparison.Ordinal);
        }
    }
}