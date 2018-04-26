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
            public static object Deserialize<TSymbol, TResolver>(ReadOnlySpan<TSymbol> input, Type type)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<TSymbol, TResolver>.InnerDeserialize(input, type);
            }

            public static class Utf16
            {
                public static string Serialize(object input)
                {
                    return Serialize<char, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                public static ValueTask SerializeAsync(object input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<char, ExcludeNullsOriginalCaseResolver<char>>(input, writer, cancellationToken);
                }

                public static object Deserialize(ReadOnlySpan<char> input, Type type)
                {
                    return Deserialize<char, ExcludeNullsOriginalCaseResolver<char>>(input, type);
                }

                public static ValueTask<object> DeserializeAsync(TextReader reader, Type type, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<char, ExcludeNullsOriginalCaseResolver<char>>(reader, type, cancellationToken);
                }

                public static string Serialize<TSymbol, TResolver>(object input) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
                    where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerSerializeToString(input);
                }

                public static ValueTask<object> DeserializeAsync<TSymbol, TResolver>(TextReader reader, Type type,
                    CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerDeserializeAsync(reader, type, cancellationToken);
                }

                public static ValueTask SerializeAsync<TSymbol, TResolver>(object input, TextWriter writer, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
                }
            }

            public static class Utf8
            {
                public static byte[] Serialize(object input)
                {
                    return Serialize<byte, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                public static object Deserialize(ReadOnlySpan<byte> input, Type type)
                {
                    return Deserialize<byte, ExcludeNullsOriginalCaseResolver<byte>>(input, type);
                }

                public static ValueTask SerializeAsync(object input, Stream stream, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<byte, ExcludeNullsOriginalCaseResolver<byte>>(input, stream, cancellationToken);
                }

                public static ValueTask<object> DeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<byte, ExcludeNullsOriginalCaseResolver<byte>>(stream, type, cancellationToken);
                }

                public static ValueTask<object> DeserializeAsync<TSymbol, TResolver>(Stream stream, Type type,
                    CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerDeserializeAsync(stream, type, cancellationToken);
                }

                public static ValueTask SerializeAsync<TSymbol, TResolver>(object input, Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerSerializeAsync(input, stream, cancellationToken);
                }

                public static byte[] Serialize<TSymbol, TResolver>(object input) where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new()
                    where TSymbol : struct
                {
                    return Inner<TSymbol, TResolver>.InnerSerializeToByteArray(input);
                }
            }


            private static class Inner<TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                private static readonly ConcurrentDictionary<Type, Invoker> Invokers =
                    new ConcurrentDictionary<Type, Invoker>();


                public static string InnerSerializeToString(object input)
                {
                    if (input == null)
                    {
                        return null;
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.ToStringSerializer(input);
                }

                public static byte[] InnerSerializeToByteArray(object input)
                {
                    if (input == null)
                    {
                        return null;
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.ToByteArraySerializer(input);
                }

                public static object InnerDeserialize(ReadOnlySpan<TSymbol> input, Type type)
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
                    return invoker.ToTextWriterSerializerAsync(input, writer, cancellationToken);
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
                    return invoker.FromTextReaderDeserializerAsync(reader, cancellationToken);
                }

                public static ValueTask InnerSerializeAsync(object input, Stream stream, CancellationToken cancellationToken = default)
                {
                    if (input == null)
                    {
                        return new ValueTask(Task.CompletedTask);
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(input.GetType(), x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.ToStreamSerializerAsync(input, stream, cancellationToken);
                }

                public static ValueTask<object> InnerDeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken = default)
                {
                    if (stream == null)
                    {
                        return new ValueTask<object>(null);
                    }

                    // ReSharper disable ConvertClosureToMethodGroup
                    var invoker = Invokers.GetOrAdd(type, x => BuildInvoker(x));
                    // ReSharper restore ConvertClosureToMethodGroup
                    return invoker.FromStreamDeserializerAsync(stream, cancellationToken);
                }

                private static Invoker BuildInvoker(Type type)
                {
                    return new Invoker(BuildToStringSerializer(type), BuildToByteArraySerializer(type), BuildDeserializer(type),
                        BuildAsyncTextWriterSerializer(type),
                        BuildAsyncTextReaderDeserializer(type), BuildAsyncStreamSerializer(type), BuildAsyncStreamDeserializer(type));
                }

                private static SerializeToByteArrayDelegate BuildToByteArraySerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var lambdaExpression =
                        Expression.Lambda<SerializeToByteArrayDelegate>(
                            Expression.Call(typeof(Generic.Utf8), nameof(Generic.Utf8.Serialize),
                                new[] {type, typeof(TSymbol), typeof(TResolver)}, typedInputParam),
                            inputParam);
                    return lambdaExpression.Compile();
                }

                private static SerializeToStringDelegate BuildToStringSerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var lambdaExpression =
                        Expression.Lambda<SerializeToStringDelegate>(
                            Expression.Call(typeof(Generic.Utf16), nameof(Generic.Utf16.Serialize),
                                new[] {type, typeof(TSymbol), typeof(TResolver)}, typedInputParam),
                            inputParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeDelegate BuildDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(ReadOnlySpan<TSymbol>), "input");
                    Expression genericCall = Expression.Call(typeof(Generic), nameof(Generic.Deserialize),
                        new[] {type, typeof(TSymbol), typeof(TResolver)}, inputParam);
                    if (type.IsValueType)
                    {
                        genericCall = Expression.Convert(genericCall, typeof(object));
                    }

                    var lambdaExpression = Expression.Lambda<DeserializeDelegate>(genericCall, inputParam);
                    return lambdaExpression.Compile();
                }

                private static SerializeToTextWriterDelegateAsync BuildAsyncTextWriterSerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var textWriterParam = Expression.Parameter(typeof(TextWriter), "tw");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    var lambdaExpression =
                        Expression.Lambda<SerializeToTextWriterDelegateAsync>(
                            Expression.Call(typeof(Generic.Utf16), nameof(Generic.Utf16.SerializeAsync),
                                new[] {type, typeof(TSymbol), typeof(TResolver)}, typedInputParam, textWriterParam, cancellationTokenParam),
                            inputParam, textWriterParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static SerializeToStreamDelegateAsync BuildAsyncStreamSerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var textWriterParam = Expression.Parameter(typeof(Stream), "stream");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    var lambdaExpression =
                        Expression.Lambda<SerializeToStreamDelegateAsync>(
                            Expression.Call(typeof(Generic.Utf8), nameof(Generic.Utf8.SerializeAsync),
                                new[] { type, typeof(TSymbol), typeof(TResolver) }, typedInputParam, textWriterParam, cancellationTokenParam),
                            inputParam, textWriterParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeFromTextReaderDelegateAsync BuildAsyncTextReaderDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(TextReader), "tr");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    Expression genericCall = Expression.Call(typeof(Inner<,>).MakeGenericType(typeof(TSymbol), typeof(TResolver)), nameof(GenericTextReaderObjectWrapper),
                        new[] {type}, inputParam, cancellationTokenParam);
                    var lambdaExpression = Expression.Lambda<DeserializeFromTextReaderDelegateAsync>(genericCall, inputParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeFromStreamDelegateAsync BuildAsyncStreamDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(Stream), "stream");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    Expression genericCall = Expression.Call(typeof(Inner<,>).MakeGenericType(typeof(TSymbol), typeof(TResolver)),
                        nameof(GenericStreamObjectWrapper),
                        new[] {type}, inputParam, cancellationTokenParam);
                    var lambdaExpression = Expression.Lambda<DeserializeFromStreamDelegateAsync>(genericCall, inputParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }


                /// <summary>
                ///     This is necessary to convert ValueTask of T to ValueTask of object
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static async ValueTask<object> GenericTextReaderObjectWrapper<T>(TextReader reader, CancellationToken cancellationToken = default)
                {
                    return await Generic.Utf16.DeserializeAsync<T, TSymbol, TResolver>(reader, cancellationToken).ConfigureAwait(false);
                }

                /// <summary>
                ///     This is necessary to convert ValueTask of T to ValueTask of object
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static async ValueTask<object> GenericStreamObjectWrapper<T>(Stream stream, CancellationToken cancellationToken = default)
                {
                    return await Generic.Utf8.DeserializeAsync<T, TSymbol, TResolver>(stream, cancellationToken).ConfigureAwait(false);
                }

                private delegate object DeserializeDelegate(ReadOnlySpan<TSymbol> input);

                private delegate ValueTask<object> DeserializeFromTextReaderDelegateAsync(TextReader textReader, CancellationToken cancellationToken = default);
                private delegate ValueTask<object> DeserializeFromStreamDelegateAsync(Stream stream, CancellationToken cancellationToken = default);

                private readonly struct Invoker
                {
                    public Invoker(SerializeToStringDelegate toStringSerializer, SerializeToByteArrayDelegate toByteArraySerializer,
                        DeserializeDelegate deserializer, SerializeToTextWriterDelegateAsync serializeToTextWriterDelegateAsync, DeserializeFromTextReaderDelegateAsync deserializeFromTextReaderDelegateAsync, SerializeToStreamDelegateAsync toStreamSerializerAsync, DeserializeFromStreamDelegateAsync fromStreamDeserializerAsync)
                    {
                        ToByteArraySerializer = toByteArraySerializer;
                        ToStringSerializer = toStringSerializer;
                        ToTextWriterSerializerAsync = serializeToTextWriterDelegateAsync;
                        Deserializer = deserializer;
                        FromTextReaderDeserializerAsync = deserializeFromTextReaderDelegateAsync;
                        ToStreamSerializerAsync = toStreamSerializerAsync;
                        FromStreamDeserializerAsync = fromStreamDeserializerAsync;
                    }

                    public readonly SerializeToStringDelegate ToStringSerializer;
                    public readonly SerializeToByteArrayDelegate ToByteArraySerializer;
                    public readonly DeserializeDelegate Deserializer;
                    public readonly SerializeToTextWriterDelegateAsync ToTextWriterSerializerAsync;
                    public readonly DeserializeFromTextReaderDelegateAsync FromTextReaderDeserializerAsync;
                    public readonly SerializeToStreamDelegateAsync ToStreamSerializerAsync;
                    public readonly DeserializeFromStreamDelegateAsync FromStreamDeserializerAsync;
                }

                private delegate ValueTask SerializeToTextWriterDelegateAsync(object input, TextWriter writer, CancellationToken cancellationToken = default);
                private delegate ValueTask SerializeToStreamDelegateAsync(object input, Stream stream, CancellationToken cancellationToken = default);

                private delegate byte[] SerializeToByteArrayDelegate(object input);

                private delegate string SerializeToStringDelegate(object input);
            }
        }
    }
}