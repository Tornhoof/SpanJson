using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class BadgeUtf8Formatter : BaseGeneratedFormatter<Badge, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Badge, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly BadgeUtf8Formatter Default = new BadgeUtf8Formatter();
        private readonly byte[] _award_countName = Encoding.UTF8.GetBytes("\"award_count\":");
        private readonly byte[] _badge_idName = Encoding.UTF8.GetBytes("\"badge_id\":");
        private readonly byte[] _badge_typeName = Encoding.UTF8.GetBytes("\"badge_type\":");
        private readonly byte[] _descriptionName = Encoding.UTF8.GetBytes("\"description\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");
        private readonly byte[] _nameName = Encoding.UTF8.GetBytes("\"name\":");
        private readonly byte[] _rankName = Encoding.UTF8.GetBytes("\"rank\":");
        private readonly byte[] _userName = Encoding.UTF8.GetBytes("\"user\":");

        public Badge Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Badge();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 8751724865018683746UL && ReadUInt16(ref b, 8) == 25968)
                {
                    result.badge_type = NullableFormatter<BadgeType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8390322045806929252UL && ReadUInt16(ref b, 8) == 28521 && ReadByte(ref b, 10) == 110)
                {
                    result.description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8026363845924714337UL && ReadUInt16(ref b, 8) == 28277 && ReadByte(ref b, 10) == 116)
                {
                    result.award_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7235419165478379874UL)
                {
                    result.badge_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1919251317U)
                {
                    result.user = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802396018U)
                {
                    result.rank = NullableFormatter<BadgeRank, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1701667182U)
                {
                    result.name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Badge value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.badge_id != null)
            {
                writer.WriteUtf8Verbatim(_badge_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.rank != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_rankName);
                NullableFormatter<BadgeRank, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.rank, nestingLimit);
                writeSeparator = true;
            }

            if (value.name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.name, nestingLimit);
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

            if (value.award_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_award_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.award_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_badge_typeName);
                NullableFormatter<BadgeType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.badge_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_userName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
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

            writer.WriteUtf8EndObject();
        }
    }
}