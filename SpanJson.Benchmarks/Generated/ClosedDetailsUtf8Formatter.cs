using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class ClosedDetailsUtf8Formatter : BaseGeneratedFormatter<Question.ClosedDetails, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Question.ClosedDetails, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly ClosedDetailsUtf8Formatter Default = new ClosedDetailsUtf8Formatter();
        private readonly byte[] _by_usersName = Encoding.UTF8.GetBytes("\"by_users\":");
        private readonly byte[] _descriptionName = Encoding.UTF8.GetBytes("\"description\":");
        private readonly byte[] _on_holdName = Encoding.UTF8.GetBytes("\"on_hold\":");
        private readonly byte[] _original_questionsName = Encoding.UTF8.GetBytes("\"original_questions\":");
        private readonly byte[] _reasonName = Encoding.UTF8.GetBytes("\"reason\":");

        public Question.ClosedDetails Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Question.ClosedDetails();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref b, 0) == 8390322045806929252UL && ReadUInt16(ref b, 8) == 28521 && ReadByte(ref b, 10) == 110)
                {
                    result.description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 8318823008271563106UL)
                {
                    result.by_users =
                        ListFormatter<List<ShallowUser>, ShallowUser, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7809644627822735983UL && ReadUInt64(ref b, 8) == 8028075849736876383UL &&
                    ReadUInt16(ref b, 16) == 29550)
                {
                    result.original_questions =
                        ListFormatter<List<Question.ClosedDetails.OriginalQuestion>, Question.ClosedDetails.OriginalQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1935762802U && ReadUInt16(ref b, 4) == 28271)
                {
                    result.reason = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1751084655U && ReadUInt16(ref b, 4) == 27759 && ReadByte(ref b, 6) == 100)
                {
                    result.on_hold = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Question.ClosedDetails value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.on_hold != null)
            {
                writer.WriteUtf8Verbatim(_on_holdName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.on_hold, nestingLimit);
                writeSeparator = true;
            }

            if (value.reason != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reasonName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reason, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            if (value.by_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_by_usersName);
                ListFormatter<List<ShallowUser>, ShallowUser, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.by_users,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.original_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_original_questionsName);
                ListFormatter<List<Question.ClosedDetails.OriginalQuestion>, Question.ClosedDetails.OriginalQuestion, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.original_questions, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}