using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SpanJson.Formatters
{
    public class EnumFormatter<T> : IJsonFormatter<T> where T : struct
    {
        private delegate void SerializeDelegate(ref JsonWriter writer, T value,
            IJsonFormatterResolver formatterResolver);

        private delegate T DeserializeDelegate(ref JsonReader reader, IJsonFormatterResolver formatterResolver);

        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate();
        private static readonly DeserializeDelegate Deserializer = BuildDeserializeDelegate();

        private static DeserializeDelegate BuildDeserializeDelegate()
        {
            var readerParameter = Expression.Parameter(typeof(JsonReader).MakeByRefType(), "reader");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var jsonValue = Expression.Variable(typeof(string), "jsonValue");
            var returnValue = Expression.Variable(typeof(T), "returnValue");
            var expressions = new List<Expression>
            {
                Expression.Assign(jsonValue,
                    Expression.Call(readerParameter, FindMethod(readerParameter.Type, nameof(JsonReader.ReadString))))
            };
            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var switchCase = Expression.SwitchCase(Expression.Assign(returnValue, Expression.Constant(value)),
                    Expression.Constant(value.ToString()));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(typeof(void), jsonValue,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), null, cases.ToArray());
            expressions.Add(switchExpression);
            var returnTarget = Expression.Label(returnValue.Type);
            var returnLabel = Expression.Label(returnTarget, returnValue);
            expressions.Add(returnLabel);
            var blockExpression = Expression.Block(new ParameterExpression[] {jsonValue, returnValue }, expressions);
            var lambdaExpression =
                Expression.Lambda<DeserializeDelegate>(blockExpression, readerParameter, resolverParameter);
            return lambdaExpression.Compile();
        }

        private static SerializeDelegate BuildSerializeDelegate()
        {
            var writerParameter = Expression.Parameter(typeof(JsonWriter).MakeByRefType(), "writer");
            var valueParameter = Expression.Parameter(typeof(T), "value");
            var resolverParameter = Expression.Parameter(typeof(IJsonFormatterResolver), "formatterResolver");
            var cases = new List<SwitchCase>();
            foreach (var value in Enum.GetValues(typeof(T)))
            {
                var switchCase =
                    Expression.SwitchCase(
                        Expression.Call(writerParameter,
                            FindMethod(writerParameter.Type, nameof(JsonWriter.WriteString)),
                            Expression.Constant(value.ToString())), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression = Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter,
                valueParameter, resolverParameter);
            return lambdaExpression.Compile();
        }

        private static MethodInfo FindMethod(Type type, string name)
        {
            return type.GetMethod(name);
        }

        public static readonly EnumFormatter<T> Default = new EnumFormatter<T>();


        public int AllocSize { get; } = 100;

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            return Deserializer(ref reader, formatterResolver);
        }

        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            Serializer(ref writer, value, formatterResolver);
        }
    }
}