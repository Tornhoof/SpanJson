using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class ClosedDetailsUtf16Formatter : BaseGeneratedFormatter<Question.ClosedDetails, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Question.ClosedDetails, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _on_holdName = "\"on_hold\":";
        private const string _reasonName = "\"reason\":";
        private const string _descriptionName = "\"description\":";
        private const string _by_usersName = "\"by_users\":";
        private const string _original_questionsName = "\"original_questions\":";
        public static readonly ClosedDetailsUtf16Formatter Default = new ClosedDetailsUtf16Formatter();

        public Question.ClosedDetails Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Question.ClosedDetails();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 8 && ReadUInt64(ref b, 0) == 32932980304969826UL && ReadUInt64(ref b, 8) == 32370111954616435UL)
                {
                    result.by_users =
                        ListFormatter<List<ShallowUser>, ShallowUser, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 32370038940172402UL && ReadUInt32(ref b, 8) == 7209071U)
                {
                    result.reason = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 29273805607010415UL && ReadUInt32(ref b, 8) == 7077999U && ReadUInt16(ref b, 12) == 100)
                {
                    result.on_hold = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 28992373580234863UL && ReadUInt64(ref b, 8) == 30399714103787625UL &&
                    ReadUInt64(ref b, 16) == 28429475166355551UL && ReadUInt64(ref b, 24) == 31244173394051187UL && ReadUInt32(ref b, 32) == 7536750U)
                {
                    result.original_questions =
                        ListFormatter<List<Question.ClosedDetails.OriginalQuestion>, Question.ClosedDetails.OriginalQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 27866516622213220UL && ReadUInt64(ref b, 8) == 32651578341654642UL &&
                    ReadUInt32(ref b, 16) == 7274601U && ReadUInt16(ref b, 20) == 110)
                {
                    result.description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Question.ClosedDetails value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.on_hold != null)
            {
                writer.WriteUtf16Verbatim(_on_holdName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.on_hold, nestingLimit);
                writeSeparator = true;
            }

            if (value.reason != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reasonName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reason, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            if (value.by_users != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_by_usersName);
                ListFormatter<List<ShallowUser>, ShallowUser, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.by_users,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.original_questions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_original_questionsName);
                ListFormatter<List<Question.ClosedDetails.OriginalQuestion>, Question.ClosedDetails.OriginalQuestion, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.original_questions, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}