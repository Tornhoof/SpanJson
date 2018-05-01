using System;
using System.Buffers;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using SpanJson.Resolvers;

namespace SpanJson
{
    public static partial class JsonSerializer
    {
        public static class Generic
        {
            /// <summary>
            /// This method is not encoding specific, but for symmetry reasons the public ones are in the respective encoding classes
            /// </summary>
            internal static T DeserializeInternal<T, TSymbol, TResolver>(ReadOnlySpan<TSymbol> input)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerDeserialize(input);
            }

            public static class Utf16
            {
                public static string Serialize<T>(T input)
                {
                    return Serialize<T, char, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                public static string Serialize<T, TSymbol, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerSerializeToString(input);
                }

                public static ValueTask SerializeAsync<T>(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<T, char, ExcludeNullsOriginalCaseResolver<char>>(input, writer, cancellationToken);
                }

                public static T Deserialize<T, TSymbol, TResolver>(ReadOnlySpan<TSymbol> input)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return DeserializeInternal<T, TSymbol, TResolver>(input);
                }

                public static T Deserialize<T>(ReadOnlySpan<char> input)
                {
                    return Deserialize<T, char, ExcludeNullsOriginalCaseResolver<char>>(input);
                }

                public static ValueTask<T> DeserializeAsync<T>(TextReader reader, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<T, char, ExcludeNullsOriginalCaseResolver<char>>(reader, cancellationToken);
                }

                public static ValueTask SerializeAsync<T, TSymbol, TResolver>(T input, TextWriter writer, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
                }

                public static ValueTask<T> DeserializeAsync<T, TSymbol, TResolver>(TextReader reader, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerDeserializeAsync(reader, cancellationToken);
                }
            }

            public static class Utf8
            {
                public static byte[] Serialize<T>(T input)
                {
                    return Serialize<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                public static T Deserialize<T>(ReadOnlySpan<byte> input)
                {
                    return Deserialize<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(input);
                }

                public static T Deserialize<T, TSymbol, TResolver>(ReadOnlySpan<TSymbol> input)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return DeserializeInternal<T, TSymbol, TResolver>(input);
                }

                public static byte[] Serialize<T, TSymbol, TResolver>(T input)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerSerializeToByteArray(input);
                }

                public static ValueTask SerializeAsync<T>(T input, Stream stream, CancellationToken cancellationToken = default)
                {
                    return SerializeAsync<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(input, stream, cancellationToken);
                }

                public static ValueTask<T> DeserializeAsync<T>(Stream stream, CancellationToken cancellationToken = default)
                {
                    return DeserializeAsync<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(stream, cancellationToken);
                }

                public static ValueTask SerializeAsync<T, TSymbol, TResolver>(T input, Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerSerializeAsync(input, stream, cancellationToken);
                }

                public static ValueTask<T> DeserializeAsync<T, TSymbol, TResolver>(Stream stream, CancellationToken cancellationToken = default)
                    where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
                {
                    return Inner<T, TSymbol, TResolver>.InnerDeserializeAsync(stream, cancellationToken);
                }
            }


            private static class Inner<T, TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                // ReSharper disable StaticMemberInGenericType
                private static int _lastSerializationSize = 256; // initial size, get's updated with each serialization
                private static int _lastDeserializationSize = 256; // initial size, get's updated with each deserialization
                // ReSharper restore StaticMemberInGenericType

                private static readonly IJsonFormatter<T, TSymbol, TResolver> Formatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

                public static string InnerSerializeToString(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input, 0);
                    _lastSerializationSize = jsonWriter.Position;
                    var result = jsonWriter.ToString(); // includes Dispose
                    return result;
                }

                public static byte[] InnerSerializeToByteArray(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input, 0);
                    _lastSerializationSize = jsonWriter.Position;
                    var result = jsonWriter.ToByteArray();
                    return result;
                }

                public static ValueTask InnerSerializeAsync(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input, 0);
                    _lastSerializationSize = jsonWriter.Position;
                    var temp = jsonWriter.Data;
                    var data = Unsafe.As<TSymbol[], char[]>(ref temp);
                    var result = writer.WriteAsync(data, 0, _lastSerializationSize);
                    if (result.IsCompletedSuccessfully)
                    {
                        // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                        ArrayPool<char>.Shared.Return(data);
                        return new ValueTask();
                    }

                    return AwaitSerializeAsync(result, data);
                }

                public static ValueTask InnerSerializeAsync(T input, Stream stream, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input, 0);
                    _lastSerializationSize = jsonWriter.Position;
                    var temp = jsonWriter.Data;
                    var data = Unsafe.As<TSymbol[], byte[]>(ref temp);
                    var result = stream.WriteAsync(data, 0, _lastSerializationSize, cancellationToken);
                    if (result.IsCompletedSuccessfully)
                    {
                        // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                        ArrayPool<byte>.Shared.Return(data);
                        return new ValueTask();
                    }

                    return AwaitSerializeAsync(result, data);
                }

                public static T InnerDeserialize(ReadOnlySpan<TSymbol> input)
                {
                    _lastDeserializationSize = input.Length;
                    var jsonReader = new JsonReader<TSymbol>(input);
                    return Formatter.Deserialize(ref jsonReader);
                }

                public static ValueTask<T> InnerDeserializeAsync(TextReader reader, CancellationToken cancellationToken = default)
                {
                    var input = reader.ReadToEndAsync();
                    if (input.IsCompletedSuccessfully)
                    {
                        return new ValueTask<T>(InnerDeserialize(MemoryMarshal.Cast<char, TSymbol>(input.Result)));
                    }

                    return AwaitDeserializeAsync(input);
                }

                public static ValueTask<T> InnerDeserializeAsync(Stream stream, CancellationToken cancellationToken = default)
                {
                    if (stream is MemoryStream ms && ms.TryGetBuffer(out var buffer))
                    {
                        var span = new ReadOnlySpan<byte>(buffer.Array, buffer.Offset, buffer.Count);
                        return new ValueTask<T>(InnerDeserialize(MemoryMarshal.Cast<byte, TSymbol>(span)));
                    }

                    var input = stream.CanSeek ? ReadStreamFullAsync(stream, cancellationToken) : ReadStreamAsync(stream, _lastDeserializationSize, cancellationToken);
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
                    var read = await stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
                    return new Memory<byte>(buffer, 0, read);
                }

                private static T InnerDeserialize(Memory<byte> memory)
                {
                    var result = InnerDeserialize(MemoryMarshal.Cast<byte, TSymbol>(memory.Span));
                    if (MemoryMarshal.TryGetArray<byte>(memory, out var segment))
                    {
                        ArrayPool<byte>.Shared.Return(segment.Array);
                    }

                    return result;
                }

                private static async ValueTask<Memory<byte>> ReadStreamAsync(Stream stream, int sizeHint, CancellationToken cancellationToken = default)
                {
                    var totalSize = 0;
                    var buffer = ArrayPool<byte>.Shared.Rent(sizeHint);
                    int read;
                    while ((read = await stream.ReadAsync(buffer, totalSize, buffer.Length - totalSize, cancellationToken).ConfigureAwait(false)) > 0)
                    {
                        if (totalSize + read == buffer.Length)
                        {
                            Grow(ref buffer);
                        }

                        totalSize += read;
                    }

                    return new Memory<byte>(buffer, 0, totalSize);
                }

                private static void Grow(ref byte[] array)
                {
                    var backup = array;
                    array = ArrayPool<byte>.Shared.Rent(backup.Length * 2);
                    backup.CopyTo(array, 0);
                    ArrayPool<byte>.Shared.Return(backup);
                }

                // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                private static async ValueTask AwaitSerializeAsync(Task result, char[] data)
                {
                    await result.ConfigureAwait(false);
                    ArrayPool<char>.Shared.Return(data);
                }

                // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                private static async ValueTask AwaitSerializeAsync(Task result, byte[] data)
                {
                    await result.ConfigureAwait(false);
                    ArrayPool<byte>.Shared.Return(data);
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
            }
        }
    }
}