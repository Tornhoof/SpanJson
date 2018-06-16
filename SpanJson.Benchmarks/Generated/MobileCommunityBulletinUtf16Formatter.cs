using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileCommunityBulletinUtf16Formatter : BaseGeneratedFormatter<MobileCommunityBulletin, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileCommunityBulletin, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _siteName = "\"site\":";
        private const string _titleName = "\"title\":";
        private const string _linkName = "\"link\":";
        private const string _bulletin_typeName = "\"bulletin_type\":";
        private const string _begin_dateName = "\"begin_date\":";
        private const string _end_dateName = "\"end_date\":";
        private const string _custom_date_stringName = "\"custom_date_string\":";
        private const string _tagsName = "\"tags\":";
        private const string _is_deletedName = "\"is_deleted\":";
        private const string _has_accepted_answerName = "\"has_accepted_answer\":";
        private const string _answer_countName = "\"answer_count\":";
        private const string _is_promotedName = "\"is_promoted\":";
        private const string _group_idName = "\"group_id\":";
        private const string _added_dateName = "\"added_date\":";
        public static readonly MobileCommunityBulletinUtf16Formatter Default = new MobileCommunityBulletinUtf16Formatter();

        public MobileCommunityBulletin Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileCommunityBulletin();
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

                if (length == 18 && ReadUInt64(ref b, 0) == 32651591227342947UL && ReadUInt64(ref b, 8) == 28147905700102255UL &&
                    ReadUInt64(ref b, 16) == 26740556586811489UL && ReadUInt64(ref b, 24) == 29555362188492915UL && ReadUInt32(ref b, 32) == 6750318U)
                {
                    result.custom_date_string = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 31525605421023337UL && ReadUInt64(ref b, 8) == 31244190573592690UL &&
                    ReadUInt32(ref b, 16) == 6619252U && ReadUInt16(ref b, 20) == 100)
                {
                    result.is_promoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30399795707838580UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 30399761348886626UL && ReadUInt64(ref b, 8) == 30962698417340517UL &&
                    ReadUInt64(ref b, 16) == 31525717090238559UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.bulletin_type =
                        NullableFormatter<MobileCommunityBulletin.CommunityBulletinType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 29555314942869602UL && ReadUInt64(ref b, 8) == 27303502243889262UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.begin_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

                if (length == 8 && ReadUInt64(ref b, 0) == 26740552291450981UL && ReadUInt64(ref b, 8) == 28429470870339684UL)
                {
                    result.end_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileCommunityBulletin value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.site != null)
            {
                writer.WriteUtf16Verbatim(_siteName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site, nestingLimit);
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

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.bulletin_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bulletin_typeName);
                NullableFormatter<MobileCommunityBulletin.CommunityBulletinType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.bulletin_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.begin_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_begin_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.begin_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.end_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_end_dateName);
                NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.end_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.custom_date_string != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_custom_date_stringName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.custom_date_string, nestingLimit);
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

            if (value.is_promoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_promotedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_promoted, nestingLimit);
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