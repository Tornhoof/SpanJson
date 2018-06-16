using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class InfoUtf8Formatter : BaseGeneratedFormatter<Info, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Info, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly InfoUtf8Formatter Default = new InfoUtf8Formatter();
        private readonly byte[] _answers_per_minuteName = Encoding.UTF8.GetBytes("\"answers_per_minute\":");
        private readonly byte[] _api_revisionName = Encoding.UTF8.GetBytes("\"api_revision\":");
        private readonly byte[] _badges_per_minuteName = Encoding.UTF8.GetBytes("\"badges_per_minute\":");
        private readonly byte[] _new_active_usersName = Encoding.UTF8.GetBytes("\"new_active_users\":");
        private readonly byte[] _questions_per_minuteName = Encoding.UTF8.GetBytes("\"questions_per_minute\":");
        private readonly byte[] _siteName = Encoding.UTF8.GetBytes("\"site\":");
        private readonly byte[] _total_acceptedName = Encoding.UTF8.GetBytes("\"total_accepted\":");
        private readonly byte[] _total_answersName = Encoding.UTF8.GetBytes("\"total_answers\":");
        private readonly byte[] _total_badgesName = Encoding.UTF8.GetBytes("\"total_badges\":");
        private readonly byte[] _total_commentsName = Encoding.UTF8.GetBytes("\"total_comments\":");
        private readonly byte[] _total_questionsName = Encoding.UTF8.GetBytes("\"total_questions\":");
        private readonly byte[] _total_unansweredName = Encoding.UTF8.GetBytes("\"total_unanswered\":");
        private readonly byte[] _total_usersName = Encoding.UTF8.GetBytes("\"total_users\":");
        private readonly byte[] _total_votesName = Encoding.UTF8.GetBytes("\"total_votes\":");

        public Info Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Info();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 15 && ReadUInt64(ref b, 0) == 8462650093901999988UL && ReadUInt32(ref b, 8) == 1769239397U && ReadUInt16(ref b, 12) == 28271 &&
                    ReadByte(ref b, 14) == 115)
                {
                    result.total_questions = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8319660805732986740UL && ReadUInt16(ref b, 8) == 29285 && ReadByte(ref b, 10) == 115)
                {
                    result.total_users = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 8097317534398964066UL && ReadUInt64(ref b, 8) == 8391734879760380517UL &&
                    ReadByte(ref b, 16) == 101)
                {
                    result.badges_per_minute = NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8031711904557985652UL && ReadUInt16(ref b, 8) == 25972 && ReadByte(ref b, 10) == 115)
                {
                    result.total_votes = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 8026363880000483188UL && ReadUInt32(ref b, 8) == 1852140909U && ReadUInt16(ref b, 12) == 29556)
                {
                    result.total_comments = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 7959372835543347060UL && ReadUInt64(ref b, 8) == 7234314156561886817UL)
                {
                    result.total_unanswered = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt64(ref b, 8) == 7596833091376668531UL &&
                    ReadUInt32(ref b, 16) == 1702131054U)
                {
                    result.questions_per_minute = NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7953743336009133940UL && ReadUInt32(ref b, 8) == 1919252339U && ReadByte(ref b, 12) == 115)
                {
                    result.total_answers = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7599372963135713377UL && ReadUInt32(ref b, 8) == 1852795251U)
                {
                    result.api_revision = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 7598807741145507182UL && ReadUInt64(ref b, 8) == 8318823008271558006UL)
                {
                    result.new_active_users = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 7161109801591926644UL && ReadUInt32(ref b, 8) == 1953523043U && ReadUInt16(ref b, 12) == 25701)
                {
                    result.total_accepted = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7017276088492781428UL && ReadUInt32(ref b, 8) == 1936025444U)
                {
                    result.total_badges = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 6877966836046196321UL && ReadUInt64(ref b, 8) == 8461816668349425008UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.answers_per_minute = NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1702127987U)
                {
                    result.site = SiteUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Info value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.total_questions != null)
            {
                writer.WriteUtf8Verbatim(_total_questionsName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_unanswered != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_unansweredName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_unanswered, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_acceptedName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_accepted, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_answers != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_answersName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_answers, nestingLimit);
                writeSeparator = true;
            }

            if (value.questions_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_questions_per_minuteName);
                NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.questions_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.answers_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answers_per_minuteName);
                NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answers_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_comments != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_commentsName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_comments, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_votes != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_votesName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_votes, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_badges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_badgesName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_badges, nestingLimit);
                writeSeparator = true;
            }

            if (value.badges_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badges_per_minuteName);
                NullableDecimalUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badges_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_total_usersName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.total_users, nestingLimit);
                writeSeparator = true;
            }

            if (value.new_active_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_new_active_usersName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.new_active_users, nestingLimit);
                writeSeparator = true;
            }

            if (value.api_revision != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_api_revisionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.api_revision, nestingLimit);
                writeSeparator = true;
            }

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_siteName);
                SiteUtf8Formatter.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}