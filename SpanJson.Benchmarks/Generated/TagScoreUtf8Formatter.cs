using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagScoreUtf8Formatter : BaseGeneratedFormatter<TagScore, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<TagScore, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly TagScoreUtf8Formatter Default = new TagScoreUtf8Formatter();
        private readonly byte[] _post_countName = Encoding.UTF8.GetBytes("\"post_count\":");
        private readonly byte[] _scoreName = Encoding.UTF8.GetBytes("\"score\":");
        private readonly byte[] _userName = Encoding.UTF8.GetBytes("\"user\":");

        public TagScore Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new TagScore();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 8462091486479216496UL && ReadUInt16(ref b, 8) == 29806)
                {
                    result.post_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1919902579U && ReadByte(ref b, 4) == 101)
                {
                    result.score = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1919251317U)
                {
                    result.user = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, TagScore value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.user != null)
            {
                writer.WriteUtf8Verbatim(_userName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
                writeSeparator = true;
            }

            if (value.score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_scoreName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.score, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_count, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}