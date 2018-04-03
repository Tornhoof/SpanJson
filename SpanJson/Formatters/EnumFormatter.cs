using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace SpanJson.Formatters
{
    public class EnumFormatter<T> : IJsonFormatter<T> where T : struct
    {
        private delegate void SerializeDelegate(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver);

        private static readonly SerializeDelegate Serializer = BuildSerializeDelegate();

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
                        Expression.Call(writerParameter, FindWriteMethod("WriteString"),
                            Expression.Constant(value.ToString())), Expression.Constant(value));
                cases.Add(switchCase);
            }

            var switchExpression = Expression.Switch(valueParameter,
                Expression.Throw(Expression.Constant(new InvalidOperationException())), cases.ToArray());

            var lambdaExpression = Expression.Lambda<SerializeDelegate>(switchExpression, writerParameter,
                valueParameter, resolverParameter);
            return lambdaExpression.Compile();
        }

        private static MethodInfo FindWriteMethod(string name)
        {
            return typeof(JsonWriter).GetMethod(name);
        }

        public static readonly EnumFormatter<T> Default = new EnumFormatter<T>();


        public int AllocSize { get; } = 100;

        public T Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref JsonWriter writer, T value, IJsonFormatterResolver formatterResolver)
        {
            Serializer(ref writer, value, formatterResolver);
        }

        private static Dictionary<T, string> BuildEnumDictionary()
        {
            var result = new Dictionary<T, string>();
            var values = Enum.GetValues(typeof(T));
            foreach (T value in values)
            {
                result.Add(value, value.ToString());
            }

            return result;
        }
    }
}