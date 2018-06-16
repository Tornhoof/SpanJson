using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class QuestionUtf8Formatter : BaseGeneratedFormatter<Question, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Question, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly QuestionUtf8Formatter Default = new QuestionUtf8Formatter();
        private readonly byte[] _accepted_answer_idName = Encoding.UTF8.GetBytes("\"accepted_answer_id\":");
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _answersName = Encoding.UTF8.GetBytes("\"answers\":");
        private readonly byte[] _body_markdownName = Encoding.UTF8.GetBytes("\"body_markdown\":");
        private readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private readonly byte[] _bounty_amountName = Encoding.UTF8.GetBytes("\"bounty_amount\":");
        private readonly byte[] _bounty_closes_dateName = Encoding.UTF8.GetBytes("\"bounty_closes_date\":");
        private readonly byte[] _close_vote_countName = Encoding.UTF8.GetBytes("\"close_vote_count\":");
        private readonly byte[] _closed_dateName = Encoding.UTF8.GetBytes("\"closed_date\":");
        private readonly byte[] _closed_detailsName = Encoding.UTF8.GetBytes("\"closed_details\":");
        private readonly byte[] _closed_reasonName = Encoding.UTF8.GetBytes("\"closed_reason\":");
        private readonly byte[] _comment_countName = Encoding.UTF8.GetBytes("\"comment_count\":");
        private readonly byte[] _commentsName = Encoding.UTF8.GetBytes("\"comments\":");
        private readonly byte[] _community_owned_dateName = Encoding.UTF8.GetBytes("\"community_owned_date\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _delete_vote_countName = Encoding.UTF8.GetBytes("\"delete_vote_count\":");
        private readonly byte[] _down_vote_countName = Encoding.UTF8.GetBytes("\"down_vote_count\":");
        private readonly byte[] _downvotedName = Encoding.UTF8.GetBytes("\"downvoted\":");
        private readonly byte[] _favorite_countName = Encoding.UTF8.GetBytes("\"favorite_count\":");
        private readonly byte[] _favoritedName = Encoding.UTF8.GetBytes("\"favorited\":");
        private readonly byte[] _is_answeredName = Encoding.UTF8.GetBytes("\"is_answered\":");
        private readonly byte[] _last_activity_dateName = Encoding.UTF8.GetBytes("\"last_activity_date\":");
        private readonly byte[] _last_edit_dateName = Encoding.UTF8.GetBytes("\"last_edit_date\":");
        private readonly byte[] _last_editorName = Encoding.UTF8.GetBytes("\"last_editor\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _locked_dateName = Encoding.UTF8.GetBytes("\"locked_date\":");
        private readonly byte[] _migrated_fromName = Encoding.UTF8.GetBytes("\"migrated_from\":");
        private readonly byte[] _migrated_toName = Encoding.UTF8.GetBytes("\"migrated_to\":");
        private readonly byte[] _noticeName = Encoding.UTF8.GetBytes("\"notice\":");
        private readonly byte[] _ownerName = Encoding.UTF8.GetBytes("\"owner\":");
        private readonly byte[] _protected_dateName = Encoding.UTF8.GetBytes("\"protected_date\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private readonly byte[] _reopen_vote_countName = Encoding.UTF8.GetBytes("\"reopen_vote_count\":");
        private readonly byte[] _scoreName = Encoding.UTF8.GetBytes("\"score\":");
        private readonly byte[] _share_linkName = Encoding.UTF8.GetBytes("\"share_link\":");
        private readonly byte[] _tagsName = Encoding.UTF8.GetBytes("\"tags\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");
        private readonly byte[] _up_vote_countName = Encoding.UTF8.GetBytes("\"up_vote_count\":");
        private readonly byte[] _upvotedName = Encoding.UTF8.GetBytes("\"upvoted\":");
        private readonly byte[] _view_countName = Encoding.UTF8.GetBytes("\"view_count\":");

        public Question Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Question();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 17 && ReadUInt64(ref b, 0) == 8529657601220109682UL && ReadUInt64(ref b, 8) == 7959390389040149615UL &&
                    ReadByte(ref b, 16) == 116)
                {
                    result.reopen_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 8529647769855223140UL && ReadUInt64(ref b, 8) == 7959390389040149615UL &&
                    ReadByte(ref b, 16) == 116)
                {
                    result.delete_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 8462091486528629110UL && ReadUInt16(ref b, 8) == 29806)
                {
                    result.view_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 8390054783061815140UL && ReadUInt32(ref b, 8) == 1868783461U && ReadUInt16(ref b, 12) == 28277 &&
                    ReadByte(ref b, 14) == 116)
                {
                    result.down_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref b, 0) == 8388357231580376931UL && ReadUInt64(ref b, 8) == 6873730456398815097UL &&
                    ReadUInt32(ref b, 16) == 1702125924U)
                {
                    result.community_owned_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 8386653993697501548UL && ReadUInt64(ref b, 8) == 7017839094598825577UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.last_activity_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 8319395793566789475UL)
                {
                    result.comments = ListFormatter<List<Comment>, Comment, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 8241989049890664290UL && ReadUInt32(ref b, 8) == 2003788907U && ReadByte(ref b, 12) == 110)
                {
                    result.body_markdown = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 8241416230002453603UL && ReadUInt32(ref b, 8) == 1869832549U && ReadByte(ref b, 12) == 110)
                {
                    result.closed_reason = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 8031711874794876003UL && ReadUInt64(ref b, 8) == 8389772277106828660UL)
                {
                    result.close_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

                if (length == 10 && ReadUInt64(ref b, 0) == 7596551560782506099UL && ReadUInt16(ref b, 8) == 27502)
                {
                    result.share_link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7594306332303516012UL)
                {
                    if (length == 14 && ReadUInt32(ref b, 8) == 1633967988U && ReadUInt16(ref b, 12) == 25972)
                    {
                        result.last_edit_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt16(ref b, 8) == 28532 && ReadByte(ref b, 10) == 114)
                    {
                        result.last_editor = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7311439437976531817UL && ReadUInt16(ref b, 8) == 25970 && ReadByte(ref b, 10) == 100)
                {
                    result.is_answered = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 7310590649579302756UL && ReadByte(ref b, 8) == 100)
                {
                    result.downvoted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7310584035346375014UL)
                {
                    if (length == 14 && ReadUInt32(ref b, 8) == 1970234207U && ReadUInt16(ref b, 12) == 29806)
                    {
                        result.favorite_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 9 && ReadByte(ref b, 8) == 100)
                    {
                        result.favorited = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 7310577382525465200UL && ReadUInt32(ref b, 8) == 1633967972U && ReadUInt16(ref b, 12) == 25972)
                {
                    result.protected_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7234316402526741345UL && ReadUInt64(ref b, 8) == 6877671144660296031UL &&
                    ReadUInt16(ref b, 16) == 25705)
                {
                    result.accepted_answer_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7234316338320599405UL)
                {
                    if (length == 13 && ReadUInt32(ref b, 8) == 1869768287U && ReadByte(ref b, 12) == 109)
                    {
                        result.migrated_from = MigrationInfoUtf8Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt16(ref b, 8) == 29791 && ReadByte(ref b, 10) == 111)
                    {
                        result.migrated_to = MigrationInfoUtf8Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7232609913471462499UL)
                {
                    if (length == 14 && ReadUInt32(ref b, 8) == 1767994469U && ReadUInt16(ref b, 12) == 29548)
                    {
                        result.closed_details = ClosedDetailsUtf8Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                    {
                        result.closed_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7232609913336459116UL && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                {
                    result.locked_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7160575473518735202UL && ReadUInt64(ref b, 8) == 7017839068578017132UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.bounty_closes_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7160567712663694945UL && ReadUInt32(ref b, 8) == 1953396079U)
                {
                    result.answer_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7016460285442879330UL && ReadUInt32(ref b, 8) == 1853189997U && ReadByte(ref b, 12) == 116)
                {
                    result.bounty_amount = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6878243912808230755UL && ReadUInt32(ref b, 8) == 1853189987U && ReadByte(ref b, 12) == 116)
                {
                    result.comment_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6874028428327088245UL && ReadUInt32(ref b, 8) == 1853189987U && ReadByte(ref b, 12) == 116)
                {
                    result.up_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 2004053601U && ReadUInt16(ref b, 4) == 29285 && ReadByte(ref b, 6) == 115)
                {
                    result.answers = ListFormatter<List<Answer>, Answer, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1936154996U)
                {
                    result.tags = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1919902579U && ReadByte(ref b, 4) == 101)
                {
                    result.score = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1870033013U && ReadUInt16(ref b, 4) == 25972 && ReadByte(ref b, 6) == 100)
                {
                    result.upvoted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1769238382U && ReadUInt16(ref b, 4) == 25955)
                {
                    result.notice = NoticeUtf8Formatter.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<byte> writer, Question value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.question_id != null)
            {
                writer.WriteUtf8Verbatim(_question_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_edit_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_edit_date, nestingLimit);
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

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_activity_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.locked_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_locked_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.locked_date, nestingLimit);
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

            if (value.community_owned_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_community_owned_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.community_owned_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answer_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answer_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.accepted_answer_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_accepted_answer_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.accepted_answer_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.migrated_to != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_migrated_toName);
                MigrationInfoUtf8Formatter.Default.Serialize(ref writer, value.migrated_to, nestingLimit);
                writeSeparator = true;
            }

            if (value.migrated_from != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_migrated_fromName);
                MigrationInfoUtf8Formatter.Default.Serialize(ref writer, value.migrated_from, nestingLimit);
                writeSeparator = true;
            }

            if (value.bounty_closes_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bounty_closes_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.bounty_closes_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.bounty_amount != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bounty_amountName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.bounty_amount, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_closed_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.closed_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.protected_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_protected_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.protected_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bodyName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body, nestingLimit);
                writeSeparator = true;
            }

            if (value.title != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_titleName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.title, nestingLimit);
                writeSeparator = true;
            }

            if (value.tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_tagsName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tags, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_reason != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_closed_reasonName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.closed_reason, nestingLimit);
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

            if (value.favorite_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_favorite_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.favorite_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.view_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_view_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.view_count, nestingLimit);
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

            if (value.comments != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_commentsName);
                ListFormatter<List<Comment>, Comment, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.comments, nestingLimit);
                writeSeparator = true;
            }

            if (value.answers != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answersName);
                ListFormatter<List<Answer>, Answer, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answers, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_linkName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_answered != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_answeredName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_answered, nestingLimit);
                writeSeparator = true;
            }

            if (value.close_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_close_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.close_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.reopen_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reopen_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reopen_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.delete_vote_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_delete_vote_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.delete_vote_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.notice != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_noticeName);
                NoticeUtf8Formatter.Default.Serialize(ref writer, value.notice, nestingLimit);
                writeSeparator = true;
            }

            if (value.upvoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_upvotedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.upvoted, nestingLimit);
                writeSeparator = true;
            }

            if (value.downvoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_downvotedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.downvoted, nestingLimit);
                writeSeparator = true;
            }

            if (value.favorited != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_favoritedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.favorited, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_editorName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.last_editor, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_comment_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.comment_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.body_markdown != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_body_markdownName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body_markdown, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_details != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_closed_detailsName);
                ClosedDetailsUtf8Formatter.Default.Serialize(ref writer, value.closed_details, nestingLimit);
                writeSeparator = true;
            }

            if (value.share_link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_share_linkName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.share_link, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}