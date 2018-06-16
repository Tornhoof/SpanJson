using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class QuestionTimelineUtf16Formatter : BaseGeneratedFormatter<QuestionTimeline, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<QuestionTimeline, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _timeline_typeName = "\"timeline_type\":";
        private const string _question_idName = "\"question_id\":";
        private const string _post_idName = "\"post_id\":";
        private const string _comment_idName = "\"comment_id\":";
        private const string _revision_guidName = "\"revision_guid\":";
        private const string _up_vote_countName = "\"up_vote_count\":";
        private const string _down_vote_countName = "\"down_vote_count\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _userName = "\"user\":";
        private const string _ownerName = "\"owner\":";
        public static readonly QuestionTimelineUtf16Formatter Default = new QuestionTimelineUtf16Formatter();

        public QuestionTimeline Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new QuestionTimeline();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 13 && ReadUInt64(ref b, 0) == 33214455281090677UL && ReadUInt64(ref b, 8) == 26740556586811503UL &&
                    ReadUInt64(ref b, 16) == 30962749956620387UL && ReadUInt16(ref b, 24) == 116)
                {
                    result.up_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32651591226949744UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.post_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32370056121090161UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt32(ref b, 16) == 6881375U && ReadUInt16(ref b, 20) == 100)
                {
                    result.question_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32088581144248437UL)
                {
                    result.user = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 30962758546554980UL && ReadUInt64(ref b, 8) == 32651574047539295UL &&
                    ReadUInt64(ref b, 16) == 31244147622871141UL && ReadUInt32(ref b, 24) == 7209077U && ReadUInt16(ref b, 28) == 116)
                {
                    result.down_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 30681240620171363UL && ReadUInt64(ref b, 8) == 26740621010927717UL &&
                    ReadUInt32(ref b, 16) == 6553705U)
                {
                    result.comment_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 29555379367379058UL && ReadUInt64(ref b, 8) == 30962724186423411UL &&
                    ReadUInt64(ref b, 16) == 29555375072542815UL && ReadUInt16(ref b, 24) == 100)
                {
                    result.revision_guid = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 28429445101977711UL && ReadUInt16(ref b, 8) == 114)
                {
                    result.owner = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 28429440806092916UL && ReadUInt64(ref b, 8) == 28429445101060204UL &&
                    ReadUInt64(ref b, 16) == 31525717090238559UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.timeline_type =
                        NullableFormatter<QuestionTimelineAction, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, QuestionTimeline value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.timeline_type != null)
            {
                writer.WriteUtf16Verbatim(_timeline_typeName);
                NullableFormatter<QuestionTimelineAction, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.timeline_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.question_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_question_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_post_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_comment_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.comment_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.revision_guid != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_revision_guidName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.revision_guid, nestingLimit);
                writeSeparator = true;
            }

            if (value.up_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_up_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.up_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.down_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_down_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.down_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_creation_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_userName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
                writeSeparator = true;
            }

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_ownerName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.owner, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}