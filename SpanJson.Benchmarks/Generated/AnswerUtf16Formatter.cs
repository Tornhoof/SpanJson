using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class AnswerUtf16Formatter<TResolver> : BaseGeneratedFormatter<Answer, char, TResolver>, IJsonFormatter<Answer, char, TResolver>
        where TResolver : class, IJsonFormatterResolver<char, TResolver>, new()
    {
        private static readonly char[] _question_idName = "\"question_id\":".ToCharArray();
        private static readonly char[] _answer_idName = "\"answer_id\":".ToCharArray();
        private static readonly char[] _locked_dateName = "\"locked_date\":".ToCharArray();
        private static readonly char[] _creation_dateName = "\"creation_date\":".ToCharArray();
        private static readonly char[] _last_edit_dateName = "\"last_edit_date\":".ToCharArray();
        private static readonly char[] _last_activity_dateName = "\"last_activity_date\":".ToCharArray();
        private static readonly char[] _scoreName = "\"score\":".ToCharArray();
        private static readonly char[] _community_owned_dateName = "\"community_owned_date\":".ToCharArray();
        private static readonly char[] _is_acceptedName = "\"is_accepted\":".ToCharArray();
        private static readonly char[] _bodyName = "\"body\":".ToCharArray();
        private static readonly char[] _ownerName = "\"owner\":".ToCharArray();
        private static readonly char[] _titleName = "\"title\":".ToCharArray();
        private static readonly char[] _up_vote_countName = "\"up_vote_count\":".ToCharArray();
        private static readonly char[] _down_vote_countName = "\"down_vote_count\":".ToCharArray();
        private static readonly char[] _commentsName = "\"comments\":".ToCharArray();
        private static readonly char[] _linkName = "\"link\":".ToCharArray();
        private static readonly char[] _tagsName = "\"tags\":".ToCharArray();
        private static readonly char[] _upvotedName = "\"upvoted\":".ToCharArray();
        private static readonly char[] _downvotedName = "\"downvoted\":".ToCharArray();
        private static readonly char[] _acceptedName = "\"accepted\":".ToCharArray();
        private static readonly char[] _last_editorName = "\"last_editor\":".ToCharArray();
        private static readonly char[] _comment_countName = "\"comment_count\":".ToCharArray();
        private static readonly char[] _body_markdownName = "\"body_markdown\":".ToCharArray();
        private static readonly char[] _share_linkName = "\"share_link\":".ToCharArray();
        public static readonly AnswerUtf16Formatter<TResolver> Default = new AnswerUtf16Formatter<TResolver>();

        public Answer Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Answer();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var c = ref MemoryMarshal.GetReference(name);
                if (length >= 4 && ReadUInt64(ref c, 0) == 34058901685993570UL)
                {
                    if (length == 13 && ReadUInt64(ref c, 4) == 32088563963986015UL && ReadUInt64(ref c, 8) == 33495998976491627UL &&
                        ReadUInt16(ref c, 12) == 110)
                    {
                        result.body_markdown = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 4)
                    {
                        result.body = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 9 && ReadUInt64(ref c, 0) == 33496016157016161UL && ReadUInt64(ref c, 4) == 29555280583983205UL && ReadUInt16(ref c, 8) == 100)
                {
                    result.answer_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 33214455281090677UL && ReadUInt64(ref c, 4) == 26740556586811503UL &&
                    ReadUInt64(ref c, 8) == 30962749956620387UL && ReadUInt16(ref c, 12) == 116)
                {
                    result.up_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref c, 0) == 32651591226032236UL)
                {
                    if (length == 18 && ReadUInt64(ref c, 4) == 32651522506555487UL && ReadUInt64(ref c, 8) == 32651548277735529UL &&
                        ReadUInt64(ref c, 12) == 27303502243889273UL && ReadUInt32(ref c, 16) == 6619252U)
                    {
                        result.last_activity_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length >= 8 && ReadUInt64(ref c, 4) == 29555302057967711UL)
                    {
                        if (length == 14 && ReadUInt64(ref c, 8) == 27303502243889268UL && ReadUInt32(ref c, 12) == 6619252U)
                        {
                            result.last_edit_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 11 && ReadUInt32(ref c, 8) == 7274612U && ReadUInt16(ref c, 10) == 114)
                        {
                            result.last_editor = ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref c, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 32370056121090161UL && ReadUInt64(ref c, 4) == 30962724186423412UL &&
                    ReadUInt32(ref c, 8) == 6881375U && ReadUInt16(ref c, 10) == 100)
                {
                    result.question_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref c, 0) == 32088624092872819UL && ReadUInt16(ref c, 4) == 101)
                {
                    result.score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref c, 0) == 32088563963658355UL && ReadUInt64(ref c, 4) == 29555336417312869UL &&
                    ReadUInt32(ref c, 8) == 7012462U)
                {
                    result.share_link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref c, 0) == 31244229228363893UL && ReadUInt32(ref c, 4) == 6619252U && ReadUInt16(ref c, 6) == 100)
                {
                    result.upvoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref c, 0) == 30962758546554980UL)
                {
                    if (length == 15 && ReadUInt64(ref c, 4) == 32651574047539295UL && ReadUInt64(ref c, 8) == 31244147622871141UL &&
                        ReadUInt32(ref c, 12) == 7209077U && ReadUInt16(ref c, 14) == 116)
                    {
                        result.down_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 9 && ReadUInt64(ref c, 4) == 28429470871257206UL && ReadUInt16(ref c, 8) == 100)
                    {
                        result.downvoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref c, 0) == 30681240620171363UL)
                {
                    if (length == 20 && ReadUInt64(ref c, 4) == 32651548277211253UL && ReadUInt64(ref c, 8) == 33495998976163961UL &&
                        ReadUInt64(ref c, 12) == 26740552290861166UL && ReadUInt64(ref c, 16) == 28429470870339684UL)
                    {
                        result.community_owned_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 8 && ReadUInt64(ref c, 4) == 32370120545140837UL)
                    {
                        result.comments = ListFormatter<List<Comment>, Comment, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 13 && ReadUInt64(ref c, 4) == 26740621010927717UL && ReadUInt64(ref c, 8) == 30962749956620387UL &&
                        ReadUInt16(ref c, 12) == 116)
                    {
                        result.comment_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 5 && ReadUInt64(ref c, 0) == 30399795707838580UL && ReadUInt16(ref c, 4) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref c, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 30118247717077100UL && ReadUInt64(ref c, 4) == 28147905699512421UL &&
                    ReadUInt32(ref c, 8) == 7602273U && ReadUInt16(ref c, 10) == 101)
                {
                    result.locked_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref c, 0) == 28429445101977711UL && ReadUInt16(ref c, 4) == 114)
                {
                    result.owner = ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref c, 0) == 28429397856026721UL && ReadUInt64(ref c, 4) == 28147931470364784UL)
                {
                    result.accepted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 27303506540101731UL && ReadUInt64(ref c, 4) == 30962724186423412UL &&
                    ReadUInt64(ref c, 8) == 32651513916817503UL && ReadUInt16(ref c, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 27303480770363497UL && ReadUInt64(ref c, 4) == 31525631189778531UL &&
                    ReadUInt32(ref c, 8) == 6619252U && ReadUInt16(ref c, 10) == 100)
                {
                    result.is_accepted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Answer value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.question_id != null)
            {
                writer.WriteUtf16Verbatim(_question_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answer_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answer_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.locked_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_locked_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.locked_date, nestingLimit);
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

            if (value.last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_edit_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_edit_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_activity_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
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

            if (value.community_owned_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_community_owned_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.community_owned_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_acceptedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_accepted, nestingLimit);
                writeSeparator = true;
            }

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bodyName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body, nestingLimit);
                writeSeparator = true;
            }

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_ownerName);
                ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.owner, nestingLimit);
                writeSeparator = true;
            }

            if (value.title != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_titleName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.title, nestingLimit);
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

            if (value.comments != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_commentsName);
                ListFormatter<List<Comment>, Comment, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.comments, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_tagsName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tags, nestingLimit);
                writeSeparator = true;
            }

            if (value.upvoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_upvotedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.upvoted, nestingLimit);
                writeSeparator = true;
            }

            if (value.downvoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_downvotedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.downvoted, nestingLimit);
                writeSeparator = true;
            }

            if (value.accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_acceptedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.accepted, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_editorName);
                ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_editor, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_comment_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.comment_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.body_markdown != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_body_markdownName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body_markdown, nestingLimit);
                writeSeparator = true;
            }

            if (value.share_link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_share_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.share_link, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}