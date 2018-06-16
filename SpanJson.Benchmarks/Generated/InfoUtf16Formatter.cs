using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class InfoUtf16Formatter : BaseGeneratedFormatter<Info, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Info, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _total_questionsName = "\"total_questions\":";
        private const string _total_unansweredName = "\"total_unanswered\":";
        private const string _total_acceptedName = "\"total_accepted\":";
        private const string _total_answersName = "\"total_answers\":";
        private const string _questions_per_minuteName = "\"questions_per_minute\":";
        private const string _answers_per_minuteName = "\"answers_per_minute\":";
        private const string _total_commentsName = "\"total_comments\":";
        private const string _total_votesName = "\"total_votes\":";
        private const string _total_badgesName = "\"total_badges\":";
        private const string _badges_per_minuteName = "\"badges_per_minute\":";
        private const string _total_usersName = "\"total_users\":";
        private const string _new_active_usersName = "\"new_active_users\":";
        private const string _api_revisionName = "\"api_revision\":";
        private const string _siteName = "\"site\":";
        public static readonly InfoUtf16Formatter Default = new InfoUtf16Formatter();

        public Info Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Info();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 18 && ReadUInt64(ref b, 0) == 33496016157016161UL && ReadUInt64(ref b, 8) == 26740616716222565UL &&
                    ReadUInt64(ref b, 16) == 26740612420403312UL && ReadUInt64(ref b, 24) == 32933044728430701UL && ReadUInt32(ref b, 32) == 6619252U)
                {
                    result.answers_per_minute = NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref b, 0) == 32370056121090161UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 28429453690339443UL && ReadUInt64(ref b, 24) == 29555340712280178UL &&
                    ReadUInt64(ref b, 32) == 28429470871650414UL)
                {
                    result.questions_per_minute = NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 28992352104284258UL && ReadUInt64(ref b, 8) == 31525605421023333UL &&
                    ReadUInt64(ref b, 16) == 30681180490825829UL && ReadUInt64(ref b, 24) == 32651599816818793UL && ReadUInt16(ref b, 32) == 101)
                {
                    result.badges_per_minute = NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429470870863987UL)
                {
                    result.site = SiteUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 27303570964414580UL)
                {
                    if (length == 15 && ReadUInt64(ref b, 8) == 32933057612677228UL && ReadUInt64(ref b, 16) == 29555370778361957UL &&
                        ReadUInt32(ref b, 24) == 7209071U && ReadUInt16(ref b, 28) == 115)
                    {
                        result.total_questions = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt64(ref b, 8) == 32370124839125100UL && ReadUInt32(ref b, 16) == 7471205U && ReadUInt16(ref b, 20) == 115)
                    {
                        result.total_users = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt64(ref b, 8) == 31244229227249772UL && ReadUInt32(ref b, 16) == 6619252U && ReadUInt16(ref b, 20) == 115)
                    {
                        result.total_votes = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 14 && ReadUInt64(ref b, 8) == 31244147622871148UL && ReadUInt64(ref b, 16) == 30962681237012589UL &&
                        ReadUInt32(ref b, 24) == 7536756U)
                    {
                        result.total_comments = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 16 && ReadUInt64(ref b, 8) == 30962749955571820UL && ReadUInt64(ref b, 16) == 33496016157016161UL &&
                        ReadUInt64(ref b, 24) == 28147931470233701UL)
                    {
                        result.total_unanswered = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 13 && ReadUInt64(ref b, 8) == 30962664056225900UL && ReadUInt64(ref b, 16) == 32088581144510579UL &&
                        ReadUInt16(ref b, 24) == 115)
                    {
                        result.total_answers = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 14 && ReadUInt64(ref b, 8) == 27866439312408684UL && ReadUInt64(ref b, 16) == 32651578341392483UL &&
                        ReadUInt32(ref b, 24) == 6553701U)
                    {
                        result.total_accepted = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 12 && ReadUInt64(ref b, 8) == 27303493653954668UL && ReadUInt64(ref b, 16) == 32370056120172644UL)
                    {
                        result.total_badges = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 26740633895239790UL && ReadUInt64(ref b, 8) == 29555370777313377UL &&
                    ReadUInt64(ref b, 16) == 32932980303659126UL && ReadUInt64(ref b, 24) == 32370111954616435UL)
                {
                    result.new_active_users = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 26740573766418529UL && ReadUInt64(ref b, 8) == 29555379367379058UL &&
                    ReadUInt64(ref b, 16) == 30962724186423411UL)
                {
                    result.api_revision = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Info value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.total_questions != null)
            {
                writer.WriteUtf16Verbatim(_total_questionsName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_questions, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_unanswered != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_unansweredName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_unanswered, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_acceptedName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_accepted, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_answers != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_answersName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_answers, nestingLimit);
                writeSeparator = true;
            }

            if (value.questions_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_questions_per_minuteName);
                NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.questions_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.answers_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answers_per_minuteName);
                NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answers_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_comments != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_commentsName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_comments, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_votes != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_votesName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_votes, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_badges != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_badgesName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_badges, nestingLimit);
                writeSeparator = true;
            }

            if (value.badges_per_minute != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_badges_per_minuteName);
                NullableDecimalUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.badges_per_minute, nestingLimit);
                writeSeparator = true;
            }

            if (value.total_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_total_usersName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.total_users, nestingLimit);
                writeSeparator = true;
            }

            if (value.new_active_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_new_active_usersName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.new_active_users, nestingLimit);
                writeSeparator = true;
            }

            if (value.api_revision != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_api_revisionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.api_revision, nestingLimit);
                writeSeparator = true;
            }

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_siteName);
                SiteUtf16Formatter.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}