using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TopTagUtf16Formatter : BaseGeneratedFormatter<TopTag, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<TopTag, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _tag_nameName = "\"tag_name\":";
        private const string _question_scoreName = "\"question_score\":";
        private const string _question_countName = "\"question_count\":";
        private const string _answer_scoreName = "\"answer_score\":";
        private const string _answer_countName = "\"answer_count\":";
        private const string _user_idName = "\"user_id\":";
        public static readonly TopTagUtf16Formatter Default = new TopTagUtf16Formatter();

        public TopTag Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new TopTag();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 33496016157016161UL)
                {
                    if (length == 12 && ReadUInt64(ref b, 8) == 32370030351089765UL && ReadUInt64(ref b, 16) == 28429462281322595UL)
                    {
                        result.answer_score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 12 && ReadUInt64(ref b, 8) == 27866430723719269UL && ReadUInt64(ref b, 16) == 32651569752506479UL)
                    {
                        result.answer_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32370056121090161UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 30962724186423412UL)
                    {
                        if (length == 14 && ReadUInt64(ref b, 16) == 32933049023004767UL && ReadUInt32(ref b, 24) == 7602286U)
                        {
                            result.question_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 14 && ReadUInt64(ref b, 16) == 31244147624181855UL && ReadUInt32(ref b, 24) == 6619250U)
                        {
                            result.question_score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32088581144248437UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 26740565175500916UL && ReadUInt64(ref b, 8) == 28429440805568622UL)
                {
                    result.tag_name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, TopTag value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.tag_name != null)
            {
                writer.WriteUtf16Verbatim(_tag_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tag_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.question_score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_question_scoreName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_score, nestingLimit);
                writeSeparator = true;
            }

            if (value.question_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_question_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.question_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_answer_scoreName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.answer_score, nestingLimit);
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

            if (value.user_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_user_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}