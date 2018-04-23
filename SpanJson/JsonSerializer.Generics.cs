using System;
using System.Buffers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using SpanJson.Resolvers;

namespace SpanJson
{
    public static partial class JsonSerializer
    {
        public static class Generic
        {
            public static string Serialize<T>(T input)
            {
                return Serialize<T, ExcludeNullsOriginalCaseResolver>(input);
            }

            public static Task SerializeAsync<T>(T input, TextWriter writer, CancellationToken cancellationToken = default)
            {
                return SerializeAsync<T, ExcludeNullsOriginalCaseResolver>(input, writer, cancellationToken);
            }

            public static T Deserialize<T>(ReadOnlySpan<char> input)
            {
                return Deserialize<T, ExcludeNullsOriginalCaseResolver>(input);
            }

            public static string Serialize<T, TResolver>(T input)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerSerialize(input);
            }

            public static Task<T> DeserializeAsync<T>(TextReader reader, CancellationToken cancellationToken = default)
            {
                return DeserializeAsync<T, ExcludeNullsOriginalCaseResolver>(reader, cancellationToken);
            }

            public static Task SerializeAsync<T, TResolver>(T input, TextWriter writer, CancellationToken cancellationToken = default)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerSerializeAsync(input, writer, cancellationToken);
            }

            public static T Deserialize<T, TResolver>(ReadOnlySpan<char> input)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerDeserialize(input);
            }

            public static Task<T> DeserializeAsync<T, TResolver>(TextReader reader, CancellationToken cancellationToken = default)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerDeserializeAsync(reader, cancellationToken);
            }

            private static class Inner<T, TResolver> where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                // ReSharper disable StaticMemberInGenericType
                private static int _lastSerializationSize = 256; // initial size, get's updated with each serialization
                // ReSharper restore StaticMemberInGenericType

                private static readonly IJsonFormatter<T, TResolver> Formatter = StandardResolvers.GetResolver<TResolver>().GetFormatter<T>();

                public static string InnerSerialize(T input)
                {
                    var jsonWriter = new JsonWriter(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input);
                    _lastSerializationSize = jsonWriter.Position;
                    var result = jsonWriter.ToString(); // includes Dispose
                    return result;
                }

                public static Task InnerSerializeAsync(T input, TextWriter writer, CancellationToken cancellationToken = default)
                {
                    var jsonWriter = new JsonWriter(_lastSerializationSize);
                    Formatter.Serialize(ref jsonWriter, input);
                    _lastSerializationSize = jsonWriter.Position;
                    var data = jsonWriter.Data;
                    // This is a bit ugly, as we use the arraypool outside of the jsonwriter, but ref can't be use in async
                    var result = writer.WriteAsync(data, 0, _lastSerializationSize);
                    if (result.IsCompletedSuccessfully)
                    {
                        ArrayPool<char>.Shared.Return(data);
                        return Task.CompletedTask;
                    }

                    return result.ContinueWith(_ => ArrayPool<char>.Shared.Return(data), cancellationToken);
                }

                public static T InnerDeserialize(ReadOnlySpan<char> input)
                {
                    var jsonReader = new JsonReader(input);
                    return Formatter.Deserialize(ref jsonReader);
                }

                public static Task<T> InnerDeserializeAsync(TextReader reader, CancellationToken cancellationToken = default)
                {
                    var input = reader.ReadToEndAsync();
                    if (input.IsCompletedSuccessfully)
                    {
                        return Task.FromResult(InnerDeserialize(input.Result));
                    }

                    return input.ContinueWith(task => InnerDeserialize(task.Result), cancellationToken);
                }
            }
        }
    }
}