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
            private static class Inner<T>
            {
                public delegate string SerializeDelegate(T input, IJsonFormatterResolver formatterResolver);
                public static readonly SerializeDelegate InnerSerialize = BuildSerializeDelegate();
                public delegate T DeserializeDelegate(string input, IJsonFormatterResolver formatterResolver);
                public static readonly DeserializeDelegate InnerDeserialize = BuildDeserializeDelegate();
                /// <summary>
                /// This gets us around the runtime decision of allocSize, we know it after init of the formatter
                /// A delegate to a local method
                /// </summary>
                private static SerializeDelegate BuildSerializeDelegate()
                {
                    var resolver = StandardResolvers.Default;
                    var formatter = resolver.GetFormatter<T>();
                    if (formatter.AllocSize <= 256) // todo find better values
                    {
                        string Serialize(T input, IJsonFormatterResolver formatterResolver)
                        {
                            Span<char> span = stackalloc char[formatter.AllocSize];
                            var jsonWriter = new JsonWriter(span);
                            formatter.Serialize(ref jsonWriter, input, resolver);
                            return jsonWriter.ToString(); // includes Dispose
                        }

                        return Serialize;
                    }
                    else
                    {
                        string Serialize(T input, IJsonFormatterResolver formatterResolver)
                        {
                            var jsonWriter = new JsonWriter(formatter.AllocSize);
                            formatter.Serialize(ref jsonWriter, input, resolver);
                            var result = jsonWriter.ToString(); // includes Dispose
                            return result;
                        }

                        return Serialize;
                    }
                }

                private static DeserializeDelegate BuildDeserializeDelegate()
                {
                    var resolver = StandardResolvers.Default;
                    var formatter = resolver.GetFormatter<T>();

                    T Deserialize(string input, IJsonFormatterResolver formatterResolver)
                    {
                        var jsonReader = new JsonReader(input);
                        return formatter.Deserialize(ref jsonReader, resolver);
                    }

                    return Deserialize;
                }
            }
            public static string Serialize<T>(T input)
            {
                return Inner<T>.InnerSerialize(input, StandardResolvers.Default);
            }

            public static T Deserialize<T>(string input)
            {
                return Inner<T>.InnerDeserialize(input, StandardResolvers.Default);
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