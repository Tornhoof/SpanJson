using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class BadgeUtf16Formatter : BaseGeneratedFormatter<Badge, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Badge, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _badge_idName = "\"badge_id\":";
        private const string _rankName = "\"rank\":";
        private const string _nameName = "\"name\":";
        private const string _descriptionName = "\"description\":";
        private const string _award_countName = "\"award_count\":";
        private const string _badge_typeName = "\"badge_type\":";
        private const string _userName = "\"user\":";
        private const string _linkName = "\"link\":";
        public static readonly BadgeUtf16Formatter Default = new BadgeUtf16Formatter();

        public Badge Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Badge();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 4 && ReadUInt64(ref b, 0) == 32088581144248437UL)
                {
                    result.user = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32088563964641377UL && ReadUInt64(ref b, 8) == 31244147622871140UL &&
                    ReadUInt32(ref b, 16) == 7209077U && ReadUInt16(ref b, 20) == 116)
                {
                    result.award_count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294960799858UL)
                {
                    result.rank = NullableFormatter<BadgeRank, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 28992352104284258UL)
                {
                    if (length == 10 && ReadUInt64(ref b, 8) == 34058970404421733UL && ReadUInt32(ref b, 16) == 6619248U)
                    {
                        result.badge_type = NullableFormatter<BadgeType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 8 && ReadUInt64(ref b, 8) == 28147948648857701UL)
                    {
                        result.badge_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429440805568622UL)
                {
                    result.name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<char> writer, Badge value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.badge_id != null)
            {
                writer.WriteUtf16Verbatim(_badge_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.badge_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.rank != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_rankName);
                NullableFormatter<BadgeRank, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.rank, nestingLimit);
                writeSeparator = true;
            }

            if (value.name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.name, nestingLimit);
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

            if (value.award_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_award_countName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.award_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.badge_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_badge_typeName);
                NullableFormatter<BadgeType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.badge_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_userName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
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

            writer.WriteUtf16EndObject();
        }
    }
}