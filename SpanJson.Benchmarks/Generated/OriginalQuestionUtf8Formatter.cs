using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class OriginalQuestionUtf8Formatter :
        BaseGeneratedFormatter<Question.ClosedDetails.OriginalQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Question.ClosedDetails.OriginalQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly OriginalQuestionUtf8Formatter Default = new OriginalQuestionUtf8Formatter();
        private readonly byte[] _accepted_answer_idName = Encoding.UTF8.GetBytes("\"accepted_answer_id\":");
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");

        public Question.ClosedDetails.OriginalQuestion Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Question.ClosedDetails.OriginalQuestion();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt16(ref b, 8) == 26975 && ReadByte(ref b, 10) == 100)
                {
                    result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7234316402526741345UL && ReadUInt64(ref b, 8) == 6877671144660296031UL &&
                    ReadUInt16(ref b, 16) == 25705)
                {
                    result.accepted_answer_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7160567712663694945UL && ReadUInt32(ref b, 8) == 1953396079U)
                {
                    result.answer_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Question.ClosedDetails.OriginalQuestion value, int nestingLimit)
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

            writer.WriteUtf8EndObject();
        }
    }
}