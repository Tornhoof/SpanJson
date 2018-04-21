using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using SpanJson.Resolvers;

namespace SpanJson
{
    public static partial class JsonSerializer
    {
        public static class NonGeneric
        {
            public static string Serialize(object input)
            {
                return Serialize<ExcludeNullsOriginalCaseResolver>(input);
            }


            public static object Deserialize(ReadOnlySpan<char> input, Type type)
            {
                return Deserialize<ExcludeNullsOriginalCaseResolver>(input, type);
            }

            public static object Deserialize<TResolver>(ReadOnlySpan<char> input, Type type) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerDeserialize(input, type);
            }

            public static string Serialize<TResolver>(object input) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerSerialize(input);
            }


            private static class Inner<TResolver> where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                private static readonly ConcurrentDictionary<Type, Invoker> Invokers =
                    new ConcurrentDictionary<Type, Invoker>();


                public static string InnerSerialize(object input)
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

                public static object InnerDeserialize(ReadOnlySpan<char> input, Type type)
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
                    var lambdaExpression =
                        Expression.Lambda<SerializeDelegate>(
                            Expression.Call(typeof(Generic), nameof(Generic.Serialize),
                                new[] {type, typeof(TResolver)}, typedInputParam),
                            inputParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeDelegate BuildDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(ReadOnlySpan<char>), "input");
                    Expression genericCall = Expression.Call(typeof(Generic), nameof(Generic.Deserialize),
                        new[] {type, typeof(TResolver)}, inputParam);
                    if (type.IsValueType)
                    {
                        genericCall = Expression.Convert(genericCall, typeof(object));
                    }

                    var lambdaExpression = Expression.Lambda<DeserializeDelegate>(genericCall, inputParam);
                    return lambdaExpression.Compile();
                }

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

                private delegate string SerializeDelegate(object input);
            }
        }
    }
}
