using System;
using System.Collections.Generic;
using System.Text;
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

            public static T Deserialize<T>(ReadOnlySpan<char> input)
            {
                return Deserialize<T, ExcludeNullsOriginalCaseResolver>(input);
            }

            public static string Serialize<T, TResolver>(T input)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerSerialize(input);
            }

            public static T Deserialize<T, TResolver>(ReadOnlySpan<char> input)
                where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                return Inner<T, TResolver>.InnerDeserialize(input);
            }

            private static class Inner<T, TResolver> where TResolver : IJsonFormatterResolver<TResolver>, new()
            {
                internal static readonly SerializeDelegate InnerSerialize = BuildSerializeDelegate();
                internal static readonly DeserializeDelegate InnerDeserialize = BuildDeserializeDelegate();
                // ReSharper disable StaticMemberInGenericType
                private static int LastSize = 512;
                // ReSharper restore StaticMemberInGenericType

                /// <summary>
                ///     This gets us around the runtime decision of allocSize, we know it after init of the formatter
                ///     A delegate to a local method
                /// </summary>
                private static SerializeDelegate BuildSerializeDelegate()
                {
                    var resolver = StandardResolvers.GetResolver<TResolver>();
                    var formatter = resolver.GetFormatter<T>();
                    if (formatter.AllocSize <= 256) // todo find better values
                    {
                        string Serialize(T input)
                        {
                            Span<char> span = stackalloc char[formatter.AllocSize];
                            var jsonWriter = new JsonWriter(span);
                            formatter.Serialize(ref jsonWriter, input);
                            return jsonWriter.ToString(); // includes Dispose
                        }

                        return Serialize;
                    }
                    else
                    {
                        string Serialize(T input)
                        {
                            var jsonWriter = new JsonWriter(formatter.AllocSize);
                            formatter.Serialize(ref jsonWriter, input);
                            var result = jsonWriter.ToString(); // includes Dispose
                            return result;
                        }

                        return Serialize;
                    }
                }

                private static DeserializeDelegate BuildDeserializeDelegate()
                {
                    var resolver = StandardResolvers.GetResolver<TResolver>();
                    var formatter = resolver.GetFormatter<T>();

                    T Deserialize(ReadOnlySpan<char> input)
                    {
                        var jsonReader = new JsonReader(input);
                        return formatter.Deserialize(ref jsonReader);
                    }

                    return Deserialize;
                }

                internal delegate T DeserializeDelegate(ReadOnlySpan<char> input);

                internal delegate string SerializeDelegate(T input);
            }
        }
    }
}
