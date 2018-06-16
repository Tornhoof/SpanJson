using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileQuestionUtf16Formatter : BaseGeneratedFormatter<MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileQuestion, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _question_idName = "\"question_id\":";
        private const string _question_creation_dateName = "\"question_creation_date\":";
        private const string _titleName = "\"title\":";
        private const string _last_activity_dateName = "\"last_activity_date\":";
        private const string _tagsName = "\"tags\":";
        private const string _siteName = "\"site\":";
        private const string _is_deletedName = "\"is_deleted\":";
        private const string _has_accepted_answerName = "\"has_accepted_answer\":";
        private const string _answer_countName = "\"answer_count\":";
        private const string _group_idName = "\"group_id\":";
        private const string _added_dateName = "\"added_date\":";
        public static readonly MobileQuestionUtf16Formatter Default = new MobileQuestionUtf16Formatter();

        public MobileQuestion Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileQuestion();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 12 && ReadUInt64(ref b, 0) == 33496016157016161UL && ReadUInt64(ref b, 8) == 27866430723719269UL &&
                    ReadUInt64(ref b, 16) == 32651569752506479UL)
                {
                    result.answer_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 32933049023987815UL && ReadUInt64(ref b, 8) == 28147948648857712UL)
                {
                    result.group_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 32651591226032236UL && ReadUInt64(ref b, 8) == 32651522506555487UL &&
                    ReadUInt64(ref b, 16) == 32651548277735529UL && ReadUInt64(ref b, 24) == 27303502243889273UL && ReadUInt32(ref b, 32) == 6619252U)
                {
                    result.last_activity_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32370056121090161UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 30962724186423412UL)
                    {
                        if (length == 22 && ReadUInt64(ref b, 16) == 28429462280536159UL && ReadUInt64(ref b, 24) == 31244173394051169UL &&
                            ReadUInt64(ref b, 32) == 27303502243889262UL && ReadUInt32(ref b, 40) == 6619252U)
                        {
                            result.question_creation_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 11 && ReadUInt32(ref b, 16) == 6881375U && ReadUInt16(ref b, 20) == 100)
                        {
                            result.question_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
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

                if (length == 4 && ReadUInt64(ref b, 0) == 28429470870863987UL)
                {
                    result.site = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 28429402151059553UL && ReadUInt64(ref b, 8) == 27303502243889252UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.added_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 28147905700495465UL && ReadUInt64(ref b, 8) == 32651531097210981UL &&
                    ReadUInt32(ref b, 16) == 6553701U)
                {
                    result.is_deleted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 26740616715108456UL && ReadUInt64(ref b, 8) == 28429397856026721UL &&
                    ReadUInt64(ref b, 16) == 28147931470364784UL && ReadUInt64(ref b, 24) == 32370094774485087UL && ReadUInt32(ref b, 32) == 6619255U &&
                    ReadUInt16(ref b, 36) == 114)
                {
                    result.has_accepted_answer = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileQuestion value, int nestingLimit)
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

            if (value.question_creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_question_creation_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_creation_date, nestingLimit);
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

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_activity_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
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

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_siteName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_deleted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_deletedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_deleted, nestingLimit);
                writeSeparator = true;
            }

            if (value.has_accepted_answer != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_has_accepted_answerName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.has_accepted_answer, nestingLimit);
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

            if (value.group_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_group_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.group_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.added_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_added_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.added_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}