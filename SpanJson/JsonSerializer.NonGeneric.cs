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
            public static class Utf16
            {
                public static string Serialize(object input)
                {
                    return Serialize<ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                public static ValueTask SerializeAsync(object input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<ExcludeNullsOriginalCaseResolver<char>>(input, writer, cancellationToken);
                }

                public static object Deserialize<TResolver>(ReadOnlySpan<char> input, Type type)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<char, TResolver>.InnerDeserialize(input, type);
                }

                public static object Deserialize(ReadOnlySpan<char> input, Type type)
                {
                    return Deserialize<ExcludeNullsOriginalCaseResolver<char>>(input, type);
                }

                public static ValueTask<object> DeserializeAsync(TextReader reader, Type type, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<ExcludeNullsOriginalCaseResolver<char>>(reader, type, cancellationToken);
                }

                public static string Serialize<TResolver>(object input) where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<char, TResolver>.InnerSerializeToString(input);
                }

                public static ValueTask<object> DeserializeAsync<TResolver>(TextReader reader, Type type,
                    CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<char, TResolver>.InnerDeserializeAsync(reader, type, cancellationToken);
                }

                public static ValueTask SerializeAsync<TResolver>(object input, TextWriter writer, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<char, TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
                }

                /// <summary>
                ///     This is necessary to convert ValueTask of T to ValueTask of object
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static async ValueTask<object> GenericTextReaderObjectWrapper<T, TResolver>(TextReader reader,
                    CancellationToken cancellationToken = default) where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return await Generic.Utf16.DeserializeAsync<T, TResolver>(reader, cancellationToken).ConfigureAwait(false);
                }

            }

            public static class Utf8
            {
                public static byte[] Serialize(object input)
                {
                    return Serialize<ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                public static object Deserialize<TResolver>(ReadOnlySpan<byte> input, Type type)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<byte, TResolver>.InnerDeserialize(input, type);
                }

                public static object Deserialize(ReadOnlySpan<byte> input, Type type)
                {
                    return Deserialize<ExcludeNullsOriginalCaseResolver<byte>>(input, type);
                }

                public static ValueTask SerializeAsync(object input, Stream stream, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<ExcludeNullsOriginalCaseResolver<byte>>(input, stream, cancellationToken);
                }

                public static ValueTask<object> DeserializeAsync(Stream stream, Type type, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<ExcludeNullsOriginalCaseResolver<byte>>(stream, type, cancellationToken);
                }

                public static ValueTask<object> DeserializeAsync<TResolver>(Stream stream, Type type,
                    CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<byte, TResolver>.InnerDeserializeAsync(stream, type, cancellationToken);
                }

                public static ValueTask SerializeAsync<TResolver>(object input, Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<byte, TResolver>.InnerSerializeAsync(input, stream, cancellationToken);
                }

                public static byte[] Serialize<TResolver>(object input) where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<byte, TResolver>.InnerSerializeToByteArray(input);
                }

                /// <summary>
                ///     This is necessary to convert ValueTask of T to ValueTask of object
                /// </summary>
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                internal static async ValueTask<object> GenericStreamObjectWrapper<T, TResolver>(Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return await Generic.Utf8.DeserializeAsync<T, TResolver>(stream, cancellationToken).ConfigureAwait(false);
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

                /// <summary>
                /// Build only the delegates which are actually required
                /// </summary>
                private static Invoker BuildInvoker(Type type)
                {
                    if (typeof(TSymbol) == typeof(char))
                    {
                        return new Invoker(BuildToStringSerializer(type), null, BuildDeserializer(type),
                            BuildAsyncTextWriterSerializer(type),
                            BuildAsyncTextReaderDeserializer(type), null, null);
                    }

                    if (typeof(TSymbol) == typeof(byte))
                    {
                        return new Invoker(null, BuildToByteArraySerializer(type), BuildDeserializer(type),
                            null, null, BuildAsyncStreamSerializer(type), BuildAsyncStreamDeserializer(type));
                    }

                    throw new NotSupportedException();

                }

                private static SerializeToByteArrayDelegate BuildToByteArraySerializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(object), "input");
                    var typedInputParam = Expression.Convert(inputParam, type);
                    var lambdaExpression =
                        Expression.Lambda<SerializeToByteArrayDelegate>(
                            Expression.Call(typeof(Generic.Utf8), nameof(Generic.Utf8.Serialize),
                                new[] {type, typeof(TResolver)}, typedInputParam),
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
                                new[] {type, typeof(TResolver)}, typedInputParam),
                            inputParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeDelegate BuildDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(ReadOnlySpan<TSymbol>), "input");
                    Expression genericCall = Expression.Call(typeof(Generic), nameof(Generic.DeserializeInternal),
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
                                new[] {type, typeof(TResolver)}, typedInputParam, textWriterParam, cancellationTokenParam),
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
                                new[] {type, typeof(TResolver)}, typedInputParam, textWriterParam, cancellationTokenParam),
                            inputParam, textWriterParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeFromTextReaderDelegateAsync BuildAsyncTextReaderDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(TextReader), "tr");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    Expression genericCall = Expression.Call(typeof(Utf16), nameof(Utf16.GenericTextReaderObjectWrapper),
                        new[] { type, typeof(TResolver)}, inputParam, cancellationTokenParam);
                    var lambdaExpression = Expression.Lambda<DeserializeFromTextReaderDelegateAsync>(genericCall, inputParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }

                private static DeserializeFromStreamDelegateAsync BuildAsyncStreamDeserializer(Type type)
                {
                    var inputParam = Expression.Parameter(typeof(Stream), "stream");
                    var cancellationTokenParam = Expression.Parameter(typeof(CancellationToken), "cancellationToken");
                    Expression genericCall = Expression.Call(typeof(Utf8), nameof(Utf8.GenericStreamObjectWrapper),
                        new[] {type, typeof(TResolver)}, inputParam, cancellationTokenParam);
                    var lambdaExpression = Expression.Lambda<DeserializeFromStreamDelegateAsync>(genericCall, inputParam, cancellationTokenParam);
                    return lambdaExpression.Compile();
                }


                private delegate object DeserializeDelegate(ReadOnlySpan<TSymbol> input);

                private delegate ValueTask<object> DeserializeFromTextReaderDelegateAsync(TextReader textReader, CancellationToken cancellationToken = default);

                private delegate ValueTask<object> DeserializeFromStreamDelegateAsync(Stream stream, CancellationToken cancellationToken = default);

                private readonly struct Invoker
                {
                    public Invoker(SerializeToStringDelegate toStringSerializer, SerializeToByteArrayDelegate toByteArraySerializer,
                        DeserializeDelegate deserializer, SerializeToTextWriterDelegateAsync serializeToTextWriterDelegateAsync,
                        DeserializeFromTextReaderDelegateAsync deserializeFromTextReaderDelegateAsync, SerializeToStreamDelegateAsync toStreamSerializerAsync,
                        DeserializeFromStreamDelegateAsync fromStreamDeserializerAsync)
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