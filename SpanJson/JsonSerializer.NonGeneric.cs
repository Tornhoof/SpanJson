using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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

            public static ValueTask SerializeAsync(object input, TextWriter writer, CancellationToken cancellationToken = default)
            {
                return SerializeAsync<ExcludeNullsOriginalCaseResolver>(input, writer, cancellationToken);
            }

            public static ValueTask<object> DeserializeAsync(TextReader reader, Type type, CancellationToken cancellationToken = default)
            {
                return DeserializeAsync<ExcludeNullsOriginalCaseResolver>(reader, type, cancellationToken);
            }

            public static object Deserialize<TResolver>(ReadOnlySpan<char> input, Type type) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerDeserialize(input, type);
            }

            public static string Serialize<TResolver>(object input) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerSerialize(input);
            }

            public static ValueTask<object> DeserializeAsync<TResolver>(TextReader reader, Type type, CancellationToken cancellationToken = default) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerDeserializeAsync(reader, type, cancellationToken);
            }

            public static ValueTask SerializeAsync<TResolver>(object input, TextWriter writer, CancellationToken cancellationToken = default) where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
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

                public static ValueTask InnerSerializeAsync(object input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    if (input == null)
                    {
                        return new ValueTask(Task.CompletedTask);
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.SerializerAsync(input, writer, cancellationToken);
                }

                public static ValueTask<object> InnerDeserializeAsync(TextReader reader, Type type, CancellationToken cancellationToken = default)
                {
                    if (reader == null)
                    {
                        return new ValueTask<object>(null);
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(type, x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.DeserializerAsync(reader, cancellationToken);
                }

                private static Invoker BuildInvoker(Type type)
                {
                    return new Invoker(BuildSerializer(type), BuildDeserializer(type), BuildAsyncSerializer(type), BuildAsyncDeserializer(type));
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

                private static SerializeDelegateAsync BuildAsyncSerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var textWriterParam = Expression.Parameter(typeof(TextWriter), "tw");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    var lambdaExpression =
                        Expression.Lambda<SerializeDelegateAsync>(
                            Expression.Call(typeof(Generic), nameof(Generic.SerializeAsync),
                                new[] { type, typeof(TResolver) }, typedInputParam, textWriterParam, cancellationTokenParam),
                            inputParam, textWriterParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeDelegateAsync BuildAsyncDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(TextReader), "tr");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    Expression genericCall = Expression.Call(typeof(Inner<>).MakeGenericType(typeof(TResolver)), nameof(GenericObjectWrapper),
                        new[] {type}, inputParam, cancellationTokenParam);
                    var lambdaExpression = Expression.Lambda<DeserializeDelegateAsync>(genericCall, inputParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                /// <summary>
                /// This is necessary to convert ValueTask of T to ValueTask of object
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static async ValueTask<object> GenericObjectWrapper<T>(TextReader reader, CancellationToken cancellationToken = default)
                {
                    return await Generic.DeserializeAsync<T, TResolver>(reader, cancellationToken).ConfigureAwait(false);
                }

                private delegate object DeserializeDelegate(ReadOnlySpan<char> input);
                private delegate string SerializeDelegate(object input);

                private delegate ValueTask<object> DeserializeDelegateAsync(TextReader textReader, CancellationToken cancellationToken = default);
                private delegate ValueTask SerializeDelegateAsync(object input, TextWriter writer, CancellationToken cancellationToken = default);

                private readonly struct Invoker
                {
                    public Invoker(SerializeDelegate serializer, DeserializeDelegate deserializer, SerializeDelegateAsync serializeDelegateAsync, DeserializeDelegateAsync deserializeDelegateAsync)
                    {
                        Serializer = serializer;
                        SerializerAsync = serializeDelegateAsync;
                        Deserializer = deserializer;
                        DeserializerAsync = deserializeDelegateAsync;
                    }

                    public readonly SerializeDelegate Serializer;
                    public readonly DeserializeDelegate Deserializer;
                    public readonly SerializeDelegateAsync SerializerAsync;
                    public readonly DeserializeDelegateAsync DeserializerAsync;
                }
            }
        }
    }
}