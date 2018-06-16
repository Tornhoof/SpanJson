using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class TagUtf16Formatter : BaseGeneratedFormatter<Tag, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Tag, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _nameName = "\"name\":";
        private const string _countName = "\"count\":";
        private const string _is_requiredName = "\"is_required\":";
        private const string _is_moderator_onlyName = "\"is_moderator_only\":";
        private const string _user_idName = "\"user_id\":";
        private const string _has_synonymsName = "\"has_synonyms\":";
        private const string _last_activity_dateName = "\"last_activity_date\":";
        private const string _synonymsName = "\"synonyms\":";
        public static readonly TagUtf16Formatter Default = new TagUtf16Formatter();

        public Tag Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Tag();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 18 && ReadUInt64(ref b, 0) == 32651591226032236UL && ReadUInt64(ref b, 8) == 32651522506555487UL &&
                    ReadUInt64(ref b, 16) == 32651548277735529UL && ReadUInt64(ref b, 24) == 27303502243889273UL && ReadUInt32(ref b, 32) == 6619252U)
                {
                    result.last_activity_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32088581144248437UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32088555374444649UL && ReadUInt64(ref b, 8) == 29555375073198181UL &&
                    ReadUInt32(ref b, 16) == 6619250U && ReadUInt16(ref b, 20) == 100)
                {
                    result.is_required = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 31244194869215347UL && ReadUInt64(ref b, 8) == 32370090481090670UL)
                {
                    result.synonyms = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30962749956620387UL && ReadUInt16(ref b, 8) == 116)
                {
                    result.count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 30681180490891369UL && ReadUInt64(ref b, 8) == 32088581143265391UL &&
                    ReadUInt64(ref b, 16) == 32088624093986913UL && ReadUInt64(ref b, 24) == 30399769938427999UL && ReadUInt16(ref b, 32) == 121)
                {
                    result.is_moderator_only = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429440805568622UL)
                {
                    result.name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 26740616715108456UL && ReadUInt64(ref b, 8) == 31244194869215347UL &&
                    ReadUInt64(ref b, 16) == 32370090481090670UL)
                {
                    result.has_synonyms = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Tag value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.name != null)
            {
                writer.WriteUtf16Verbatim(_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.name, nestingLimit);
                writeSeparator = true;
            }

            if (value.count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.count, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_required != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_requiredName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_required, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_moderator_only != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_moderator_onlyName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_moderator_only, nestingLimit);
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

            if (value.has_synonyms != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_has_synonymsName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.has_synonyms, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_activity_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.synonyms != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_synonymsName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.synonyms, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}