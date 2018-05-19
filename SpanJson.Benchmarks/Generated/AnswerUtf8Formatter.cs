using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class AnswerUtf8Formatter<TResolver> : BaseGeneratedFormatter<Answer, byte, TResolver>, IJsonFormatter<Answer, byte, TResolver>
        where TResolver : class, IJsonFormatterResolver<byte, TResolver>, new()
    {
        private static readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private static readonly byte[] _answer_idName = Encoding.UTF8.GetBytes("\"answer_id\":");
        private static readonly byte[] _locked_dateName = Encoding.UTF8.GetBytes("\"locked_date\":");
        private static readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private static readonly byte[] _last_edit_dateName = Encoding.UTF8.GetBytes("\"last_edit_date\":");
        private static readonly byte[] _last_activity_dateName = Encoding.UTF8.GetBytes("\"last_activity_date\":");
        private static readonly byte[] _scoreName = Encoding.UTF8.GetBytes("\"score\":");
        private static readonly byte[] _community_owned_dateName = Encoding.UTF8.GetBytes("\"community_owned_date\":");
        private static readonly byte[] _is_acceptedName = Encoding.UTF8.GetBytes("\"is_accepted\":");
        private static readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private static readonly byte[] _ownerName = Encoding.UTF8.GetBytes("\"owner\":");
        private static readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");
        private static readonly byte[] _up_vote_countName = Encoding.UTF8.GetBytes("\"up_vote_count\":");
        private static readonly byte[] _down_vote_countName = Encoding.UTF8.GetBytes("\"down_vote_count\":");
        private static readonly byte[] _commentsName = Encoding.UTF8.GetBytes("\"comments\":");
        private static readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private static readonly byte[] _tagsName = Encoding.UTF8.GetBytes("\"tags\":");
        private static readonly byte[] _upvotedName = Encoding.UTF8.GetBytes("\"upvoted\":");
        private static readonly byte[] _downvotedName = Encoding.UTF8.GetBytes("\"downvoted\":");
        private static readonly byte[] _acceptedName = Encoding.UTF8.GetBytes("\"accepted\":");
        private static readonly byte[] _last_editorName = Encoding.UTF8.GetBytes("\"last_editor\":");
        private static readonly byte[] _comment_countName = Encoding.UTF8.GetBytes("\"comment_count\":");
        private static readonly byte[] _body_markdownName = Encoding.UTF8.GetBytes("\"body_markdown\":");
        private static readonly byte[] _share_linkName = Encoding.UTF8.GetBytes("\"share_link\":");
        public static readonly AnswerUtf8Formatter<TResolver> Default = new AnswerUtf8Formatter<TResolver>();

        public Answer Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Answer();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var c = ref MemoryMarshal.GetReference(name);
                if (length == 15 && ReadUInt64(ref c, 0) == 8390054783061815140UL && ReadUInt32(ref c, 8) == 1868783461U && ReadUInt16(ref c, 12) == 28277 &&
                    ReadByte(ref c, 14) == 116)
                {
                    result.down_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref c, 0) == 8388357231580376931UL && ReadUInt64(ref c, 8) == 6873730456398815097UL &&
                    ReadUInt32(ref c, 16) == 1702125924U)
                {
                    result.community_owned_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref c, 0) == 8386653993697501548UL && ReadUInt64(ref c, 8) == 7017839094598825577UL &&
                    ReadUInt16(ref c, 16) == 25972)
                {
                    result.last_activity_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref c, 0) == 8319395793566789475UL)
                {
                    result.comments = ListFormatter<List<Comment>, Comment, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 8241989049890664290UL && ReadUInt32(ref c, 8) == 2003788907U && ReadByte(ref c, 12) == 110)
                {
                    result.body_markdown = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 8098988783382262633UL && ReadUInt16(ref c, 8) == 25972 && ReadByte(ref c, 10) == 100)
                {
                    result.is_accepted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 7957695015460107633UL && ReadUInt16(ref c, 8) == 26975 && ReadByte(ref c, 10) == 100)
                {
                    result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 7957695015158116963UL && ReadUInt32(ref c, 8) == 1952539743U && ReadByte(ref c, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref c, 0) == 7596551560782506099UL && ReadUInt16(ref c, 8) == 27502)
                {
                    result.share_link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref c, 0) == 7594306332303516012UL)
                {
                    if (length == 14 && ReadUInt32(ref c, 8) == 1633967988U && ReadUInt16(ref c, 12) == 25972)
                    {
                        result.last_edit_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt16(ref c, 8) == 28532 && ReadByte(ref c, 10) == 114)
                    {
                        result.last_editor = ShallowUserUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 9 && ReadUInt64(ref c, 0) == 7592913276891262561UL && ReadByte(ref c, 8) == 100)
                {
                    result.answer_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref c, 0) == 7310590649579302756UL && ReadByte(ref c, 8) == 100)
                {
                    result.downvoted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref c, 0) == 7234316402526741345UL)
                {
                    result.accepted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref c, 0) == 7232609913336459116UL && ReadUInt16(ref c, 8) == 29793 && ReadByte(ref c, 10) == 101)
                {
                    result.locked_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 6878243912808230755UL && ReadUInt32(ref c, 8) == 1853189987U && ReadByte(ref c, 12) == 116)
                {
                    result.comment_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 6874028428327088245UL && ReadUInt32(ref c, 8) == 1853189987U && ReadByte(ref c, 12) == 116)
                {
                    result.up_vote_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref c, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref c, 0) == 1936154996U)
                {
                    result.tags = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref c, 0) == 1919902579U && ReadByte(ref c, 4) == 101)
                {
                    result.score = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref c, 0) == 1870033013U && ReadUInt16(ref c, 4) == 25972 && ReadByte(ref c, 6) == 100)
                {
                    result.upvoted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref c, 0) == 1819568500U && ReadByte(ref c, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref c, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref c, 0) == 1701738351U && ReadByte(ref c, 4) == 114)
                {
                    result.owner = ShallowUserUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Answer value, int nestingLimit)
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

            if (value.answer_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answer_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answer_id, nestingLimit);
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

            if (value.is_accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_acceptedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_accepted, nestingLimit);
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

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_ownerName);
                ShallowUserUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.owner, nestingLimit);
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

            if (value.accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_acceptedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.accepted, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_editorName);
                ShallowUserUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_editor, nestingLimit);
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