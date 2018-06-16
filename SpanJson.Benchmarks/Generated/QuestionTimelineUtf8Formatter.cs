using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class QuestionTimelineUtf8Formatter : BaseGeneratedFormatter<QuestionTimeline, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<QuestionTimeline, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly QuestionTimelineUtf8Formatter Default = new QuestionTimelineUtf8Formatter();
        private readonly byte[] _comment_idName = Encoding.UTF8.GetBytes("\"comment_id\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _down_vote_countName = Encoding.UTF8.GetBytes("\"down_vote_count\":");
        private readonly byte[] _ownerName = Encoding.UTF8.GetBytes("\"owner\":");
        private readonly byte[] _post_idName = Encoding.UTF8.GetBytes("\"post_id\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private readonly byte[] _revision_guidName = Encoding.UTF8.GetBytes("\"revision_guid\":");
        private readonly byte[] _timeline_typeName = Encoding.UTF8.GetBytes("\"timeline_type\":");
        private readonly byte[] _up_vote_countName = Encoding.UTF8.GetBytes("\"up_vote_count\":");
        private readonly byte[] _userName = Encoding.UTF8.GetBytes("\"user\":");

        public QuestionTimeline Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new QuestionTimeline();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 15 && ReadUInt64(ref b, 0) == 8390054783061815140UL && ReadUInt32(ref b, 8) == 1868783461U && ReadUInt16(ref b, 12) == 28277 &&
                    ReadByte(ref b, 14) == 116)
                {
                    result.down_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt16(ref b, 8) == 26975 && ReadByte(ref b, 10) == 100)
                {
                    result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695010998478194UL && ReadUInt32(ref b, 8) == 1769301855U && ReadByte(ref b, 12) == 100)
                {
                    result.revision_guid = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7308895159547947380UL && ReadUInt32(ref b, 8) == 1887007839U && ReadByte(ref b, 12) == 101)
                {
                    result.timeline_type =
                        NullableFormatter<QuestionTimelineAction, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243912808230755UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.comment_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6874028428327088245UL && ReadUInt32(ref b, 8) == 1853189987U && ReadByte(ref b, 12) == 116)
                {
                    result.up_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1953722224U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.post_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1919251317U)
                {
                    result.user = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1701738351U && ReadByte(ref b, 4) == 114)
                {
                    result.owner = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, QuestionTimeline value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.timeline_type != null)
            {
                writer.WriteUtf8Verbatim(_timeline_typeName);
                NullableFormatter<QuestionTimelineAction, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.timeline_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.question_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_question_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_comment_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.comment_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.revision_guid != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_revision_guidName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.revision_guid, nestingLimit);
                writeSeparator = true;
            }

            if (value.up_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_up_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.up_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.down_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_down_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.down_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_creation_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_userName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
                writeSeparator = true;
            }

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_ownerName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.owner, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}