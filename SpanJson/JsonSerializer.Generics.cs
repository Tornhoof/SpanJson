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
            public static string SerializeToString<T>(T input)
            {
                return SerializeToString<T, char, ExcludeNullsOriginalCaseResolver<char>>(input);
            }

            public static byte[] SerializeToByteArray<T>(T input)
            {
                return SerializeToByteArray<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(input);
            }

            public static ValueTask SerializeAsync<T>(T input, TextWriter writer, CancellationToken cancellationToken = default)
            {
                return SerializeAsync<T, char, ExcludeNullsOriginalCaseResolver<char>>(input, writer, cancellationToken);
            }

            public static T Deserialize<T>(ReadOnlySpan<char> input)
            {
                return Deserialize<T, char, ExcludeNullsOriginalCaseResolver<char>>(input);
            }

            public static T Deserialize<T>(ReadOnlySpan<byte> input)
            {
                return Deserialize<T, byte, ExcludeNullsOriginalCaseResolver<byte>>(input);
            }

            public static string SerializeToString<T, TSymbol, TResolver>(T input)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerSerializeToString(input);
            }

            public static byte[] SerializeToByteArray<T, TSymbol, TResolver>(T input)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerSerializeToByteArray(input);
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

            public static T Deserialize<T, TSymbol, TResolver>(ReadOnlySpan<TSymbol> input)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerDeserialize(input);
            }

            public static ValueTask<T> DeserializeAsync<T, TSymbol, TResolver>(TextReader reader, CancellationToken cancellationToken = default)
                where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                return Inner<T, TSymbol, TResolver>.InnerDeserializeAsync(reader, cancellationToken);
            }

            private static class Inner<T, TSymbol, TResolver> where TResolver : IJsonFormatterResolver<TSymbol, TResolver>, new() where TSymbol : struct
            {
                // ReSharper disable StaticMemberInGenericType
                private static int _lastSerializationSize = 256; // initial size, get's updated with each serialization
                // ReSharper restore StaticMemberInGenericType

                private static readonly IJsonFormatter<T, TSymbol, TResolver> Formatter = StandardResolvers.GetResolver<TSymbol, TResolver>().GetFormatter<T>();

                public static string InnerSerializeToString(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input);
                    _lastSerializationSize = jsonWriter.Position;
                    var result = jsonWriter.ToString(); // includes Dispose
                    return result;
                }

                public static byte[] InnerSerializeToByteArray(T input)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input);
                    _lastSerializationSize = jsonWriter.Position;
                    var result = jsonWriter.ToByteArray();
                    return result;
                }

                public static ValueTask InnerSerializeAsync(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter<TSymbol>(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input);
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

                // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                private static async ValueTask AwaitSerializeAsync(Task result, char[] data)
                {
                    await result.ConfigureAwait(false);
                    ArrayPool<char>.Shared.Return(data);
                }

                public static T InnerDeserialize(ReadOnlySpan<TSymbol> input)
                {
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

                    return AwaitDeSerializeAsync(input);
                }

                private static async ValueTask<T> AwaitDeSerializeAsync(Task<string> task)
                {
                    var input = await task.ConfigureAwait(false);
                    return InnerDeserialize(MemoryMarshal.Cast<char, TSymbol>(input));
                }
            }
        }
    }
}