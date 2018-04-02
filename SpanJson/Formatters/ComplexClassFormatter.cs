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
    }

    public sealed class ComplexClassFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : class
    {
        public static readonly ComplexClassFormatter<T> Default = new ComplexClassFormatter<T>();
        private static readonly SerializationDelegate<T> Delegate = BuildDelegate<T>();


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            if (value != null)
            {
                writer.WriteObjectStart();
                Delegate(in writer, value, formatterResolver);
                writer.WriteObjectEnd();
            }
        }

        public T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class ComplexStructFormatter<T> : ComplexFormatter, IJsonFormatter<T> where T : struct
    {
        public static readonly ComplexStructFormatter<T> Default = new ComplexStructFormatter<T>();
        private static readonly SerializationDelegate<T> Delegate = BuildDelegate<T>();


        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            writer.WriteObjectStart();
            Delegate(in writer, value, formatterResolver);
            writer.WriteObjectEnd();
        }

        public T DeSerialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }
    }
}