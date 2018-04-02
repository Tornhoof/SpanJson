using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Resolvers;

namespace SpanJson
{
    public static class JsonSerializer
    {
        public static class Generic
        {
            public static string Serialize<T>(T input)
            {
                var resolver = StandardResolvers.Default;
                var formatter = resolver.GetFormatter<T>();
                Span<char> span = stackalloc char[100];
                var jsonWriter = new JsonWriter(span);
                formatter.Serialize(ref jsonWriter, input, resolver);
                var result = jsonWriter.ToString();
                jsonWriter.Dispose();
                return result;
            }
        }

        public static class NonGeneric
        {
            private delegate string NonGenericInvoker(object input);

            private static readonly ConcurrentDictionary<Type, NonGenericInvoker> invokers =
                new ConcurrentDictionary<Type, NonGenericInvoker>();

            public static string Serialize(object input)
            {
                if (input == null)
                {
                    return null;
                }

                var invoker = invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                return invoker(input);
            }

            private static NonGenericInvoker BuildInvoker(Type type)
            {
                var inputParam = Expression.Parameter(typeof(object), "input");
                var typedInputParam = Expression.Convert(inputParam, type);
                var serializerMethodInfo = typeof(Generic)
                    .GetMethod(nameof(Generic.Serialize), BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(type);
                var lambdaExpression =
                    Expression.Lambda<NonGenericInvoker>(Expression.Call(null, serializerMethodInfo, typedInputParam),
                        inputParam);
                return lambdaExpression.Compile();
            }
        }
    }
}