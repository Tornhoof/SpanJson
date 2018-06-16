using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileBadgeAwardUtf8Formatter : BaseGeneratedFormatter<MobileBadgeAward, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileBadgeAward, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileBadgeAwardUtf8Formatter Default = new MobileBadgeAwardUtf8Formatter();
        private readonly byte[] _added_dateName = Encoding.UTF8.GetBytes("\"added_date\":");
        private readonly byte[] _badge_descriptionName = Encoding.UTF8.GetBytes("\"badge_description\":");
        private readonly byte[] _badge_idName = Encoding.UTF8.GetBytes("\"badge_id\":");
        private readonly byte[] _badge_nameName = Encoding.UTF8.GetBytes("\"badge_name\":");
        private readonly byte[] _badge_typeName = Encoding.UTF8.GetBytes("\"badge_type\":");
        private readonly byte[] _group_idName = Encoding.UTF8.GetBytes("\"group_id\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _post_idName = Encoding.UTF8.GetBytes("\"post_id\":");
        private readonly byte[] _rankName = Encoding.UTF8.GetBytes("\"rank\":");
        private readonly byte[] _siteName = Encoding.UTF8.GetBytes("\"site\":");

        public MobileBadgeAward Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileBadgeAward();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 8751724865018683746UL && ReadUInt16(ref b, 8) == 25968)
                {
                    result.badge_type = NullableFormatter<MobileBadgeAward.BadgeType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7306069384632754530UL && ReadUInt64(ref b, 8) == 8028075836918883187UL &&
                    ReadByte(ref b, 16) == 110)
                {
                    result.badge_description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419212958626407UL)
                {
                    result.group_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419165478379874UL)
                {
                    result.badge_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7020653758248149346UL && ReadUInt16(ref b, 8) == 25965)
                {
                    result.badge_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7017839004152521825UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.added_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1953722224U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.post_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802396018U)
                {
                    result.rank = NullableFormatter<MobileBadgeAward.BadgeRank, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<byte> writer, MobileBadgeAward value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.site != null)
            {
                writer.WriteUtf8Verbatim(_siteName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_linkName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.rank != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_rankName);
                NullableFormatter<MobileBadgeAward.BadgeRank, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.rank, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_typeName);
                NullableFormatter<MobileBadgeAward.BadgeType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_type, nestingLimit);
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