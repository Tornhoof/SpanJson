using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagSynonymUtf16Formatter : BaseGeneratedFormatter<TagSynonym, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<TagSynonym, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _from_tagName = "\"from_tag\":";
        private const string _to_tagName = "\"to_tag\":";
        private const string _applied_countName = "\"applied_count\":";
        private const string _last_applied_dateName = "\"last_applied_date\":";
        private const string _creation_dateName = "\"creation_date\":";
        public static readonly TagSynonymUtf16Formatter Default = new TagSynonymUtf16Formatter();

        public TagSynonym Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new TagSynonym();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 17 && ReadUInt64(ref b, 0) == 32651591226032236UL && ReadUInt64(ref b, 8) == 31525678434287711UL &&
                    ReadUInt64(ref b, 16) == 28147931469643884UL && ReadUInt64(ref b, 24) == 32651513916817503UL && ReadUInt16(ref b, 32) == 101)
                {
                    result.last_applied_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 32651505327603828UL && ReadUInt32(ref b, 8) == 6750305U)
                {
                    result.to_tag = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 30681249210302566UL && ReadUInt64(ref b, 8) == 28992339220627551UL)
                {
                    result.from_tag = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 30399778528428129UL && ReadUInt64(ref b, 8) == 26740552290861161UL &&
                    ReadUInt64(ref b, 16) == 30962749956620387UL && ReadUInt16(ref b, 24) == 116)
                {
                    result.applied_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, TagSynonym value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.from_tag != null)
            {
                writer.WriteUtf16Verbatim(_from_tagName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.from_tag, nestingLimit);
                writeSeparator = true;
            }

            if (value.to_tag != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_to_tagName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.to_tag, nestingLimit);
                writeSeparator = true;
            }

            if (value.applied_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_applied_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.applied_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_applied_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_applied_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_applied_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_creation_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}