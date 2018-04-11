using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson
{
    public static class JsonSerializer
    {
        public static class Generic
        {
            private static class Inner<T, TResolver> where TResolver : IJsonFormatterResolver, new()
            {
                public delegate string SerializeDelegate(T input);

                public static readonly SerializeDelegate InnerSerialize = BuildSerializeDelegate();

                public delegate T DeserializeDelegate(ReadOnlySpan<char> input);

                public static readonly DeserializeDelegate InnerDeserialize = BuildDeserializeDelegate();


                /// <summary>
                /// This gets us around the runtime decision of allocSize, we know it after init of the formatter
                /// A delegate to a local method
                /// </summary>
                private static SerializeDelegate BuildSerializeDelegate()
                {
                    var resolver = StandardResolvers.GetResolver<TResolver>();
                    var formatter = resolver.GetFormatter<T, TResolver>();
                    if (formatter.AllocSize <= 256) // todo find better values
                    {
                        string Serialize(T input)
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
                        string Serialize(T input)
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
                    var resolver = StandardResolvers.GetResolver<TResolver>();
                    var formatter = resolver.GetFormatter<T, TResolver>();
                    T Deserialize(ReadOnlySpan<char> input)
                    {
                        var jsonReader = new JsonReader(input);
                        return formatter.Deserialize(ref jsonReader, resolver);
                    }

                    return Deserialize;
                }
            }

            public static string Serialize<T>(T input)
            {
                return Serialize<T, DefaultResolver>(input);
            }

            public static T Deserialize<T>(ReadOnlySpan<char> input)
            {
                return Deserialize<T, DefaultResolver>(input);
            }

            public static string Serialize<T, TResolver>(T input)
                where TResolver : IJsonFormatterResolver, new()
            {
                return Inner<T, TResolver>.InnerSerialize(input);
            }

            public static T Deserialize<T, TResolver>(ReadOnlySpan<char> input)
                where TResolver : IJsonFormatterResolver, new()
            {
                return Inner<T, TResolver>.InnerDeserialize(input);
            }
        }

        public static class NonGeneric
        {
            private static readonly ConcurrentDictionary<Type, Invoker> Invokers =
                new ConcurrentDictionary<Type, Invoker>();

            public static string Serialize(object input)
            {
                if (input == null)
                {
                    return null;
                }

                // ReSharper disable ConvertClosureToMethodGroup
                var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                // ReSharper restore ConvertClosureToMethodGroup
                return invoker.Serializer(input);
            }

            public static object Deserialize(ReadOnlySpan<char> input, Type type)
            {
                if (input == null)
                {
                    return null;
                }

                // ReSharper disable ConvertClosureToMethodGroup
                var invoker = Invokers.GetOrAdd(type, x => BuildInvoker(x));
                // ReSharper restore ConvertClosureToMethodGroup
                return invoker.Deserializer(input);
            }

            private static Invoker BuildInvoker(Type type)
            {
                return new Invoker(BuildSerializer(type), BuildDeserializer(type));
            }

            private static SerializeDelegate BuildSerializer(Type type)
            {
                var inputParam = Expression.Parameter(typeof(object), "input");
                var typedInputParam = Expression.Convert(inputParam, type);
                var serializerMethodInfo = typeof(Generic)
                    .GetMethod(nameof(Generic.Serialize), BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(type);
                var lambdaExpression =
                    Expression.Lambda<SerializeDelegate>(Expression.Call(null, serializerMethodInfo, typedInputParam),
                        inputParam);
                return lambdaExpression.Compile();
            }

            private static DeserializeDelegate BuildDeserializer(Type type)
            {
                var inputParam = Expression.Parameter(typeof(ReadOnlySpan<char>), "input");
                var deserializeMethod = typeof(Generic)
                    .GetMethod(nameof(Generic.Deserialize), BindingFlags.Public | BindingFlags.Static)
                    .MakeGenericMethod(type);
                var lambdaExpression =
                    Expression.Lambda<DeserializeDelegate>(Expression.Call(null, deserializeMethod, inputParam),
                        inputParam);
                return lambdaExpression.Compile();
            }

            private delegate string SerializeDelegate(object input);

            private delegate object DeserializeDelegate(ReadOnlySpan<char> input);

            private readonly struct Invoker
            {
                public Invoker(SerializeDelegate serializer, DeserializeDelegate deserializer)
                {
                    Serializer = serializer;
                    Deserializer = deserializer;
                }

                public readonly SerializeDelegate Serializer;
                public readonly DeserializeDelegate Deserializer;
            }
        }
    }
}