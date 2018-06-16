using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagScoreUtf16Formatter : BaseGeneratedFormatter<TagScore, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<TagScore, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _userName = "\"user\":";
        private const string _scoreName = "\"score\":";
        private const string _post_countName = "\"post_count\":";
        public static readonly TagScoreUtf16Formatter Default = new TagScoreUtf16Formatter();

        public TagScore Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new TagScore();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 10 && ReadUInt64(ref b, 0) == 32651591226949744UL && ReadUInt64(ref b, 8) == 32933049023004767UL &&
                    ReadUInt32(ref b, 16) == 7602286U)
                {
                    result.post_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 32088624092872819UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32088581144248437UL)
                {
                    result.user = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, TagScore value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.user != null)
            {
                writer.WriteUtf16Verbatim(_userName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
                writeSeparator = true;
            }

            if (value.score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_scoreName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.score, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_post_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.post_count, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}