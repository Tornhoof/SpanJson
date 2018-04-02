using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Resolvers;

namespace SpanJson.Formatters
{
    public abstract class ComplexFormatter
    {
        private static MethodInfo FindWriteMethod(string name)
        {
            return typeof(JsonWriter).GetMethod(name);
        }

        protected static SerializationDelegate<T> BuildDelegate<T>()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var propertyInfos = typeof(T).GetProperties();
            var expressions = new List<Expression>();
            var propertyNameWriterMethodInfo = FindWriteMethod(nameof(JsonWriter.WriteName));
            var seperatorWriteMethodInfo = FindWriteMethod(nameof(JsonWriter.WriteSeparator));
            for (int i = 0; i < propertyInfos.Length; i++)
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
                {
                    expressions.Add(Expression.Call(writerParameter, seperatorWriteMethodInfo));
                }
            }

            var blockExpression = Expression.Block(expressions);
            var lambda = Expression.Lambda<SerializationDelegate<T>>(blockExpression, writerParameter, valueParameter,
                resolverParameter);
            return lambda.Compile();
        }

        protected static int EstimateSize<T>()
        {
            Queue<Type> queue = new Queue<Type>();
            HashSet<Type> alreadyseen = new HashSet<Type>();
            queue.Enqueue(typeof(T));
            int result = 0;
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
    }
}