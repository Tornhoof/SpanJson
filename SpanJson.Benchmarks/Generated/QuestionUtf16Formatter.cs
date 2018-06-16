using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class QuestionUtf16Formatter : BaseGeneratedFormatter<Question, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Question, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _question_idName = "\"question_id\":";
        private const string _last_edit_dateName = "\"last_edit_date\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _last_activity_dateName = "\"last_activity_date\":";
        private const string _locked_dateName = "\"locked_date\":";
        private const string _scoreName = "\"score\":";
        private const string _community_owned_dateName = "\"community_owned_date\":";
        private const string _answer_countName = "\"answer_count\":";
        private const string _accepted_answer_idName = "\"accepted_answer_id\":";
        private const string _migrated_toName = "\"migrated_to\":";
        private const string _migrated_fromName = "\"migrated_from\":";
        private const string _bounty_closes_dateName = "\"bounty_closes_date\":";
        private const string _bounty_amountName = "\"bounty_amount\":";
        private const string _closed_dateName = "\"closed_date\":";
        private const string _protected_dateName = "\"protected_date\":";
        private const string _bodyName = "\"body\":";
        private const string _titleName = "\"title\":";
        private const string _tagsName = "\"tags\":";
        private const string _closed_reasonName = "\"closed_reason\":";
        private const string _up_vote_countName = "\"up_vote_count\":";
        private const string _down_vote_countName = "\"down_vote_count\":";
        private const string _favorite_countName = "\"favorite_count\":";
        private const string _view_countName = "\"view_count\":";
        private const string _ownerName = "\"owner\":";
        private const string _commentsName = "\"comments\":";
        private const string _answersName = "\"answers\":";
        private const string _linkName = "\"link\":";
        private const string _is_answeredName = "\"is_answered\":";
        private const string _close_vote_countName = "\"close_vote_count\":";
        private const string _reopen_vote_countName = "\"reopen_vote_count\":";
        private const string _delete_vote_countName = "\"delete_vote_count\":";
        private const string _noticeName = "\"notice\":";
        private const string _upvotedName = "\"upvoted\":";
        private const string _downvotedName = "\"downvoted\":";
        private const string _favoritedName = "\"favorited\":";
        private const string _last_editorName = "\"last_editor\":";
        private const string _comment_countName = "\"comment_count\":";
        private const string _body_markdownName = "\"body_markdown\":";
        private const string _closed_detailsName = "\"closed_details\":";
        private const string _share_linkName = "\"share_link\":";
        public static readonly QuestionUtf16Formatter Default = new QuestionUtf16Formatter();

        public Question Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Question();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 34058901685993570UL)
                {
                    if (length == 13 && ReadUInt64(ref b, 8) == 32088563963986015UL && ReadUInt64(ref b, 16) == 33495998976491627UL &&
                        ReadUInt16(ref b, 24) == 110)
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

                if (length >= 4 && ReadUInt64(ref b, 0) == 33496016157016161UL)
                {
                    if (length == 12 && ReadUInt64(ref b, 8) == 27866430723719269UL && ReadUInt64(ref b, 16) == 32651569752506479UL)
                    {
                        result.answer_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref b, 8) == 7471205U && ReadUInt16(ref b, 12) == 115)
                    {
                        result.answers = ListFormatter<List<Answer>, Answer, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 33495956027146358UL && ReadUInt64(ref b, 8) == 32933049023004767UL &&
                    ReadUInt32(ref b, 16) == 7602286U)
                {
                    result.view_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 33214455281090677UL && ReadUInt64(ref b, 8) == 26740556586811503UL &&
                    ReadUInt64(ref b, 16) == 30962749956620387UL && ReadUInt16(ref b, 24) == 116)
                {
                    result.up_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32651591226032236UL)
                {
                    if (length == 18 && ReadUInt64(ref b, 8) == 32651522506555487UL && ReadUInt64(ref b, 16) == 32651548277735529UL &&
                        ReadUInt64(ref b, 24) == 27303502243889273UL && ReadUInt32(ref b, 32) == 6619252U)
                    {
                        result.last_activity_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length >= 8 && ReadUInt64(ref b, 8) == 29555302057967711UL)
                    {
                        if (length == 14 && ReadUInt64(ref b, 16) == 27303502243889268UL && ReadUInt32(ref b, 24) == 6619252U)
                        {
                            result.last_edit_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 11 && ReadUInt32(ref b, 16) == 7274612U && ReadUInt16(ref b, 20) == 114)
                        {
                            result.last_editor = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 32651574047277168UL && ReadUInt64(ref b, 8) == 28429470870470757UL &&
                    ReadUInt64(ref b, 16) == 27303502243889252UL && ReadUInt32(ref b, 24) == 6619252U)
                {
                    result.protected_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32370099070173283UL)
                {
                    if (length == 13 && ReadUInt64(ref b, 8) == 32088555373461605UL && ReadUInt64(ref b, 16) == 31244216342478949UL &&
                        ReadUInt16(ref b, 24) == 110)
                    {
                        result.closed_reason = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 16 && ReadUInt64(ref b, 8) == 31244229227249765UL && ReadUInt64(ref b, 16) == 27866430722867316UL &&
                        ReadUInt64(ref b, 24) == 32651569752506479UL)
                    {
                        result.close_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length >= 8 && ReadUInt64(ref b, 8) == 28147905699512421UL)
                    {
                        if (length == 14 && ReadUInt64(ref b, 16) == 29555289174048869UL && ReadUInt32(ref b, 24) == 7536748U)
                        {
                            result.closed_details = ClosedDetailsUtf16Formatter.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 11 && ReadUInt32(ref b, 16) == 7602273U && ReadUInt16(ref b, 20) == 101)
                        {
                            result.closed_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32370056121090161UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt32(ref b, 16) == 6881375U && ReadUInt16(ref b, 20) == 100)
                {
                    result.question_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 32088624092872819UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32088589733527661UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 28147931470364769UL)
                    {
                        if (length == 13 && ReadUInt64(ref b, 16) == 31244212047839327UL && ReadUInt16(ref b, 24) == 109)
                        {
                            result.migrated_from = MigrationInfoUtf16Formatter.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 11 && ReadUInt32(ref b, 16) == 7602271U && ReadUInt16(ref b, 20) == 111)
                        {
                            result.migrated_to = MigrationInfoUtf16Formatter.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 32088563963658355UL && ReadUInt64(ref b, 8) == 29555336417312869UL &&
                    ReadUInt32(ref b, 16) == 7012462U)
                {
                    result.share_link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 31525674139582578UL && ReadUInt64(ref b, 8) == 33214455280959589UL &&
                    ReadUInt64(ref b, 16) == 26740556586811503UL && ReadUInt64(ref b, 24) == 30962749956620387UL && ReadUInt16(ref b, 32) == 116)
                {
                    result.reopen_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 31244229228363893UL && ReadUInt32(ref b, 8) == 6619252U && ReadUInt16(ref b, 12) == 100)
                {
                    result.upvoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 31244229227380838UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 28429470870863986UL)
                    {
                        if (length == 14 && ReadUInt64(ref b, 16) == 32933049023004767UL && ReadUInt32(ref b, 24) == 7602286U)
                        {
                            result.favorite_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 9 && ReadUInt16(ref b, 16) == 100)
                        {
                            result.favorited = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 30962758546554980UL)
                {
                    if (length == 15 && ReadUInt64(ref b, 8) == 32651574047539295UL && ReadUInt64(ref b, 16) == 31244147622871141UL &&
                        ReadUInt32(ref b, 24) == 7209077U && ReadUInt16(ref b, 28) == 116)
                    {
                        result.down_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 9 && ReadUInt64(ref b, 8) == 28429470871257206UL && ReadUInt16(ref b, 16) == 100)
                    {
                        result.downvoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 30962749956620386UL)
                {
                    if (length == 18 && ReadUInt64(ref b, 8) == 27866430724178036UL && ReadUInt64(ref b, 16) == 28429466576289900UL &&
                        ReadUInt64(ref b, 24) == 27303502243889267UL && ReadUInt32(ref b, 32) == 6619252U)
                    {
                        result.bounty_closes_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 13 && ReadUInt64(ref b, 8) == 27303480770756724UL && ReadUInt64(ref b, 16) == 30962749956620397UL &&
                        ReadUInt16(ref b, 24) == 116)
                    {
                        result.bounty_amount = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 30681240620171363UL)
                {
                    if (length == 20 && ReadUInt64(ref b, 8) == 32651548277211253UL && ReadUInt64(ref b, 16) == 33495998976163961UL &&
                        ReadUInt64(ref b, 24) == 26740552290861166UL && ReadUInt64(ref b, 32) == 28429470870339684UL)
                    {
                        result.community_owned_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 8 && ReadUInt64(ref b, 8) == 32370120545140837UL)
                    {
                        result.comments = ListFormatter<List<Comment>, Comment, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 13 && ReadUInt64(ref b, 8) == 26740621010927717UL && ReadUInt64(ref b, 16) == 30962749956620387UL &&
                        ReadUInt16(ref b, 24) == 116)
                    {
                        result.comment_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30399795707838580UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 30118247717077100UL && ReadUInt64(ref b, 8) == 28147905699512421UL &&
                    ReadUInt32(ref b, 16) == 7602273U && ReadUInt16(ref b, 20) == 101)
                {
                    result.locked_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 29555370778099822UL && ReadUInt32(ref b, 8) == 6619235U)
                {
                    result.notice = NoticeUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 28429445101977711UL && ReadUInt16(ref b, 8) == 114)
                {
                    result.owner = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 28429436510863460UL && ReadUInt64(ref b, 8) == 33214455280369780UL &&
                    ReadUInt64(ref b, 16) == 26740556586811503UL && ReadUInt64(ref b, 24) == 30962749956620387UL && ReadUInt16(ref b, 32) == 116)
                {
                    result.delete_vote_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 28429397856026721UL && ReadUInt64(ref b, 8) == 28147931470364784UL &&
                    ReadUInt64(ref b, 16) == 32370094774485087UL && ReadUInt64(ref b, 24) == 26740612420403319UL && ReadUInt32(ref b, 32) == 6553705U)
                {
                    result.accepted_answer_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 27303480770363497UL && ReadUInt64(ref b, 8) == 28429483756421230UL &&
                    ReadUInt32(ref b, 16) == 6619250U && ReadUInt16(ref b, 20) == 100)
                {
                    result.is_answered = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Question value, int nestingLimit)
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

            if (value.answer_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answer_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answer_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.accepted_answer_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_accepted_answer_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.accepted_answer_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.migrated_to != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_migrated_toName);
                MigrationInfoUtf16Formatter.Default.Serialize(ref writer, value.migrated_to, nestingLimit);
                writeSeparator = true;
            }

            if (value.migrated_from != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_migrated_fromName);
                MigrationInfoUtf16Formatter.Default.Serialize(ref writer, value.migrated_from, nestingLimit);
                writeSeparator = true;
            }

            if (value.bounty_closes_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bounty_closes_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.bounty_closes_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.bounty_amount != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bounty_amountName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.bounty_amount, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_closed_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.closed_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.protected_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_protected_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.protected_date, nestingLimit);
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

            if (value.closed_reason != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_closed_reasonName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.closed_reason, nestingLimit);
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

            if (value.favorite_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_favorite_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.favorite_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.view_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_view_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.view_count, nestingLimit);
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

            if (value.answers != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answersName);
                ListFormatter<List<Answer>, Answer, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answers, nestingLimit);
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

            if (value.is_answered != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_answeredName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_answered, nestingLimit);
                writeSeparator = true;
            }

            if (value.close_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_close_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.close_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.reopen_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reopen_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reopen_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.delete_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_delete_vote_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.delete_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.notice != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_noticeName);
                NoticeUtf16Formatter.Default.Serialize(ref writer, value.notice, nestingLimit);
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

            if (value.favorited != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_favoritedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.favorited, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_editorName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.last_editor, nestingLimit);
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

            if (value.closed_details != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_closed_detailsName);
                ClosedDetailsUtf16Formatter.Default.Serialize(ref writer, value.closed_details, nestingLimit);
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