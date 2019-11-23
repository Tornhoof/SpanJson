using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using SpanJson.Helpers;
using SpanJson.Resolvers;

namespace SpanJson
{
    /// <summary>
    ///     Main Type for SpanJson Serializer
    /// </summary>
    public static partial class JsonSerializer
    {
        /// <summary>
        ///     Generic part
        /// </summary>
        public static class Generic
        {
            /// <summary>
            ///     This method is used for the nongeneric deserialize calls.
            /// </summary>
            internal static T DeserializeInternal<T, TSymbol, TResolver>(in ReadOnlySpan<TSymbol> input)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerDeserialize(input);
            }


            private static class Inner<T, TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                private static readonly IJsonFormatter<T, TSymbol> Formatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static string InnerSerializeToString(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        return jsonWriter.ToString();
                    }
                    finally
                    {
                        jsonWriter.Dispose();
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ArraySegment<char> InnerSerializeToCharArrayPool(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        var data = Unsafe.As<char[]>(jsonWriter.Data);
                        return new ArraySegment<char>(data, 0, jsonWriter.Position);
                    }
                    catch
                    {
                        jsonWriter.Dispose();
                        throw;
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static byte[] InnerSerializeToByteArray(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        return jsonWriter.ToByteArray();
                    }
                    finally
                    {
                        jsonWriter.Dispose();
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ArraySegment<byte> InnerSerializeToByteArrayPool(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        var data = Unsafe.As<byte[]>(jsonWriter.Data);
                        return new ArraySegment<byte>(data, 0, jsonWriter.Position);
                    }
                    catch
                    {
                        jsonWriter.Dispose();
                        throw;
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ValueTask InnerSerializeAsync(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        var data = Unsafe.As<char[]>(jsonWriter.Data);
                        var result = writer.WriteAsync(data, 0, jsonWriter.Position);
                        if (result.IsCompletedSuccessfully)
                        {
                            return default;
                        }
                        return AwaitSerializeAsync(result);
                    }
                    finally
                    {
                        jsonWriter.Dispose();
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ValueTask InnerSerializeAsync(T input, Stream stream, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSizeEstimate);
                    try
                    {
                        Formatter.Serialize(ref jsonWriter, input);
                        _lastSerializationSizeEstimate = jsonWriter.Data.Length;
                        var data = Unsafe.As<byte[]>(jsonWriter.Data);
                        var result = stream.WriteAsync(data, 0, jsonWriter.Position, cancellationToken);
                        if (result.IsCompletedSuccessfully)
                        {
                            return default;
                        }

                        return AwaitSerializeAsync(result);
                    }
                    finally
                    {
                        jsonWriter.Dispose();
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static T InnerDeserialize(in ReadOnlySpan<TSymbol> input)
                {
                    _lastDeserializationSizeEstimate = input.Length;
                    var jsonReader = new JsonReader<TSymbol>(input);
                    return Formatter.Deserialize(ref jsonReader);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ValueTask<T> InnerDeserializeAsync(TextReader reader, CancellationToken cancellationToken = default)
                {
                    var input = reader.ReadToEndAsync();
                    if (input.IsCompletedSuccessfully)
                    {
                        return new ValueTask<T>(InnerDeserialize(MemoryMarshal.Cast<char, TSymbol>(input.Result)));
                    }

                    return AwaitDeserializeAsync(input);
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                public static ValueTask<T> InnerDeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
                {
                    if (stream is MemoryStream ms && ms.TryGetBuffer(out var buffer))
                    {
                        var span = new ReadOnlySpan<byte>(buffer.Array, buffer.Offset, buffer.Count);
                        return new ValueTask<T>(InnerDeserialize(MemoryMarshal.Cast<byte, TSymbol>(span)));
                    }

                    var input = stream.CanSeek
                        ? ReadStreamFullAsync(stream, cancellationToken)
                        : ReadStreamAsync(stream, _lastDeserializationSizeEstimate, cancellationToken);
                    if (input.IsCompletedSuccessfully)
                    {
                        var memory = input.Result;
                        return new ValueTask<T>(InnerDeserialize(memory));
                    }

                    return AwaitDeserializeAsync(input);
                }

                private static async ValueTask<Memory<byte>> ReadStreamFullAsync(Stream stream, CancellationToken cancellationToken = default)
                {
                    var buffer = ArrayPool<byte>.Shared.Rent((int) stream.Length);
                    try
                    {

                        var read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                        return new Memory<byte>(buffer, 0, read);
                    }
                    catch
                    {
                        if (buffer != null)
                        {
                            ArrayPool<byte>.Shared.Return(buffer);
                        }

                        throw;
                    }
                }

                private static T InnerDeserialize(Memory<byte> memory)
                {
                    try
                    {
                        return InnerDeserialize(MemoryMarshal.Cast<byte, TSymbol>(memory.Span));
                    }
                    finally
                    {
                        if (MemoryMarshal.TryGetArray<byte>(memory, out var segment))
                        {
                            ArrayPool<byte>.Shared.Return(segment.Array);
                        }
                    }
                }

                private static async ValueTask<Memory<byte>> ReadStreamAsync(Stream stream, int sizeHint, CancellationToken cancellationToken = default)
                {
                    var buffer = ArrayPool<byte>.Shared.Rent(sizeHint);
                    try
                    {
                        var totalSize = 0;
                        int read;
                        while ((read = await stream.ReadAsync(buffer, totalSize, buffer.Length - totalSize, cancellationToken).ConfigureAwait(false)) > 0)
                        {
                            if (totalSize + read == buffer.Length)
                            {
                                FormatterUtils.GrowArray(ref buffer);
                            }

                            totalSize += read;
                        }

                        return new Memory<byte>(buffer, 0, totalSize);
                    }
                    catch
                    {
                        if (buffer != null)
                        {
                            ArrayPool<byte>.Shared.Return(buffer);
                        }

                        throw;
                    }
                }

                private static async ValueTask AwaitSerializeAsync(Task result)
                {
                    await result.ConfigureAwait(false);
                }

                private static async ValueTask<T> AwaitDeserializeAsync(Task<string> task)
                {
                    var input = await task.ConfigureAwait(false);
                    return InnerDeserialize(MemoryMarshal.Cast<char, TSymbol>(input));
                }

                private static async ValueTask<T> AwaitDeserializeAsync(ValueTask<Memory<byte>> task)
                {
                    var input = await task.ConfigureAwait(false);
                    return InnerDeserialize(input);
                }

                // ReSharper disable StaticMemberInGenericType
                private static int _lastSerializationSizeEstimate = 256; // initial size, get's updated with each serialization

                private static int _lastDeserializationSizeEstimate = 256; // initial size, get's updated with each deserialization
                // ReSharper restore StaticMemberInGenericType
            }

            /// <summary>
            ///     Serialize/Deserialize to/from string et al.
            /// </summary>
            public static class Utf16
            {
                /// <summary>
                ///     Serialize to string.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>String</returns>
                public static string Serialize<T>(T input)
                {
                    return Serialize<T, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                /// <summary>
                ///     Serialize to char buffer from ArrayPool
                ///     The returned ArraySegment's Array needs to be returned to the ArrayPool
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Char array from ArrayPool</returns>
                public static ArraySegment<char> SerializeToArrayPool<T>(T input)
                {
                    return SerializeToArrayPool<T, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                /// <summary>
                ///     Serialize to string with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>String</returns>
                public static string Serialize<T, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerSerializeToString(input);
                }


                /// <summary>
                ///     Serialize to string with specific resolver.
                ///     The returned ArraySegment's Array needs to be returned to the ArrayPool
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>String</returns>
                public static ArraySegment<char> SerializeToArrayPool<T, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerSerializeToCharArrayPool(input);
                }

                /// <summary>
                ///     Serialize to TextWriter.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <param name="writer">Writer</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask SerializeAsync<T>(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<T, ExcludeNullsOriginalCaseResolver<char>>(input, writer, cancellationToken);
                }

                /// <summary>
                ///     Deserialize from string with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T, TResolver>(in ReadOnlySpan<char> input)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerDeserialize(input);
                }

                /// <summary>
                ///     Deserialize from string.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T>(in ReadOnlySpan<char> input)
                {
                    return Deserialize<T, ExcludeNullsOriginalCaseResolver<char>>(input);
                }


                /// <summary>
                ///     Deserialize from string with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T, TResolver>(string input)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerDeserialize(input);
                }

                /// <summary>
                ///     Deserialize from string.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T>(string input)
                {
                    return Deserialize<T, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                /// <summary>
                ///     Deserialize from TextReader.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="reader">TextReader</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Deserialized object</returns>
                public static ValueTask<T> DeserializeAsync<T>(TextReader reader, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<T, ExcludeNullsOriginalCaseResolver<char>>(reader, cancellationToken);
                }

                /// <summary>
                ///     Serialize to TextWriter with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <param name="writer">Writer</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask SerializeAsync<T, TResolver>(T input, TextWriter writer, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
                }

                /// <summary>
                ///     Deserialize from TextReader with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="reader">TextReader</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask<T> DeserializeAsync<T, TResolver>(TextReader reader, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<char, TResolver>, new()
                {
                    return Inner<T, char, TResolver>.InnerDeserializeAsync(reader, cancellationToken);
                }
            }

            /// <summary>
            ///     Serialize/Deserialize to/from byte array et al.
            /// </summary>
            public static class Utf8
            {
                /// <summary>
                ///     Serialize to byte array.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Byte array</returns>
                public static byte[] Serialize<T>(T input)
                {
                    return Serialize<T, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                /// <summary>
                ///     Serialize to byte array from ArrayPool.
                ///     The returned ArraySegment's Array needs to be returned to the ArrayPool
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Byte array from ArrayPool</returns>
                public static ArraySegment<byte> SerializeToArrayPool<T>(T input)
                {
                    return SerializeToArrayPool<T, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                /// <summary>
                ///     Deserialize from byte array.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T>(in ReadOnlySpan<byte> input)
                {
                    return Deserialize<T, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                /// <summary>
                ///     Deserialize from byte array with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T, TResolver>(in ReadOnlySpan<byte> input)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerDeserialize(input);
                }

                /// <summary>
                ///     Deserialize from byte array.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T>(byte[] input)
                {
                    return Deserialize<T, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                /// <summary>
                ///     Deserialize from byte array with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Deserialized object</returns>
                public static T Deserialize<T, TResolver>(byte[] input)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerDeserialize(input);
                }

                /// <summary>
                ///     Serialize to byte array with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Byte array</returns>
                public static byte[] Serialize<T, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerSerializeToByteArray(input);
                }

                /// <summary>
                ///     Serialize to byte array from array pool with specific resolver.
                ///     The returned ArraySegment's Array needs to be returned to the ArrayPool
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <returns>Byte array from ArrayPool</returns>
                public static ArraySegment<byte> SerializeToArrayPool<T, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerSerializeToByteArrayPool(input);
                }

                /// <summary>
                ///     Serialize to stream.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="input">Input</param>
                /// <param name="stream">Stream</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask SerializeAsync<T>(T input, Stream stream, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<T, ExcludeNullsOriginalCaseResolver<byte>>(input, stream, cancellationToken);
                }

                /// <summary>
                ///     Deserialize from stream.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <param name="stream">Stream</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<T, ExcludeNullsOriginalCaseResolver<byte>>(stream, cancellationToken);
                }

                /// <summary>
                ///     Serialize to stream with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="input">Input</param>
                /// <param name="stream">Stream</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask SerializeAsync<T, TResolver>(T input, Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerSerializeAsync(input, stream, cancellationToken);
                }

                /// <summary>
                ///     Deserialize from stream with specific resolver.
                /// </summary>
                /// <typeparam name="T">Type</typeparam>
                /// <typeparam name="TResolver">Resolver</typeparam>
                /// <param name="stream">Stream</param>
                /// <param name="cancellationToken">CancellationToken</param>
                /// <returns>Task</returns>
                public static ValueTask<T> DeserializeAsync<T, TResolver>(Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<byte, TResolver>, new()
                {
                    return Inner<T, byte, TResolver>.InnerDeserializeAsync(stream, cancellationToken);
                }
            }
        }
    }
}