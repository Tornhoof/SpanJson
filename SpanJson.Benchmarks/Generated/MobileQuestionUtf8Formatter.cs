using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileQuestionUtf8Formatter : BaseGeneratedFormatter<MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileQuestionUtf8Formatter Default = new MobileQuestionUtf8Formatter();
        private readonly byte[] _added_dateName = Encoding.UTF8.GetBytes("\"added_date\":");
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _group_idName = Encoding.UTF8.GetBytes("\"group_id\":");
        private readonly byte[] _has_accepted_answerName = Encoding.UTF8.GetBytes("\"has_accepted_answer\":");
        private readonly byte[] _is_deletedName = Encoding.UTF8.GetBytes("\"is_deleted\":");
        private readonly byte[] _last_activity_dateName = Encoding.UTF8.GetBytes("\"last_activity_date\":");
        private readonly byte[] _question_creation_dateName = Encoding.UTF8.GetBytes("\"question_creation_date\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private readonly byte[] _siteName = Encoding.UTF8.GetBytes("\"site\":");
        private readonly byte[] _tagsName = Encoding.UTF8.GetBytes("\"tags\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");

        public MobileQuestion Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileQuestion();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 8387229063778890601UL && ReadUInt16(ref b, 8) == 25701)
                {
                    result.is_deleted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 8386653993697501548UL && ReadUInt64(ref b, 8) == 7017839094598825577UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.last_activity_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 7957695015460107633UL)
                {
                    if (length == 22 && ReadUInt64(ref b, 8) == 8028075772427264863UL && ReadUInt32(ref b, 16) == 1633967982U && ReadUInt16(ref b, 20) == 25972)
                    {
                        result.question_creation_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 11 && ReadUInt16(ref b, 8) == 26975 && ReadByte(ref b, 10) == 100)
                    {
                        result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 7305792290389451112UL && ReadUInt64(ref b, 8) == 8317692624134042736UL &&
                    ReadUInt16(ref b, 16) == 25975 && ReadByte(ref b, 18) == 114)
                {
                    result.has_accepted_answer = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419212958626407UL)
                {
                    result.group_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7160567712663694945UL && ReadUInt32(ref b, 8) == 1953396079U)
                {
                    result.answer_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7017839004152521825UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.added_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1936154996U)
                {
                    result.tags = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1702127987U)
                {
                    result.site = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobileQuestion value, int nestingLimit)
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

            if (value.question_creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_question_creation_dateName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_creation_date, nestingLimit);
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

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_activity_dateName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
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

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_siteName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_deleted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_deletedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_deleted, nestingLimit);
                writeSeparator = true;
            }

            if (value.has_accepted_answer != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_has_accepted_answerName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.has_accepted_answer, nestingLimit);
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

            if (value.group_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_group_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.group_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.added_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_added_dateName);
                NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.added_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}