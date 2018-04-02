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
                if (formatter.AllocSize <= 256) // todo find better values
                {
                    Span<char> span = stackalloc char[formatter.AllocSize];
                    var jsonWriter = new JsonWriter(span);
                    formatter.Serialize(ref jsonWriter, input, resolver);
                    return jsonWriter.ToString(); // includes Dispose
                }
                else
                {
                    var jsonWriter = new JsonWriter(formatter.AllocSize);
                    formatter.Serialize(ref jsonWriter, input, resolver);
                    var result = jsonWriter.ToString(); // includes Dispose
                    return result;
                }
            }
        }

        public static class NonGeneric
        {
            private static readonly ConcurrentDictionary<Type, NonGenericInvoker> Invokers =
                new ConcurrentDictionary<Type, NonGenericInvoker>();

            public static string Serialize(object input)
            {
                if (input == null)
                {
                    return null;
                }

                // ReSharper disable ConvertClosureToMethodGroup
                var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                // ReSharper restore ConvertClosureToMethodGroup
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

            private delegate string NonGenericInvoker(object input);
        }
    }
}