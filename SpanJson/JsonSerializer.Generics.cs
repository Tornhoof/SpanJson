using System;
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

                public static T InnerDeserialize(ReadOnlySpan<char> input)
                {
                    var jsonReader = new JsonReader(input);
                    return Formatter.Deserialize(ref jsonReader);
                }
            }
        }
    }
}