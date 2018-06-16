using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class FlagOptionUtf8Formatter : BaseGeneratedFormatter<FlagOption, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<FlagOption, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly FlagOptionUtf8Formatter Default = new FlagOptionUtf8Formatter();
        private readonly byte[] _countName = Encoding.UTF8.GetBytes("\"count\":");
        private readonly byte[] _descriptionName = Encoding.UTF8.GetBytes("\"description\":");
        private readonly byte[] _has_flaggedName = Encoding.UTF8.GetBytes("\"has_flagged\":");
        private readonly byte[] _option_idName = Encoding.UTF8.GetBytes("\"option_id\":");
        private readonly byte[] _requires_commentName = Encoding.UTF8.GetBytes("\"requires_comment\":");
        private readonly byte[] _requires_question_idName = Encoding.UTF8.GetBytes("\"requires_question_id\":");
        private readonly byte[] _requires_siteName = Encoding.UTF8.GetBytes("\"requires_site\":");
        private readonly byte[] _sub_optionsName = Encoding.UTF8.GetBytes("\"sub_options\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");

        public FlagOption Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new FlagOption();
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

                if (length >= 8 && ReadUInt64(ref b, 0) == 8315178084276987250UL)
                {
                    if (length == 16 && ReadUInt64(ref b, 8) == 8389754676499669855UL)
                    {
                        result.requires_comment = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 20 && ReadUInt64(ref b, 8) == 8028075849736876383UL && ReadUInt32(ref b, 16) == 1684627310U)
                    {
                        result.requires_question_id = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 13 && ReadUInt32(ref b, 8) == 1953067871U && ReadByte(ref b, 12) == 101)
                    {
                        result.requires_site = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7598822094924838259UL && ReadUInt16(ref b, 8) == 28271 && ReadByte(ref b, 10) == 115)
                {
                    result.sub_options =
                        ListFormatter<List<FlagOption>, FlagOption, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 7592908921559609455UL && ReadByte(ref b, 8) == 100)
                {
                    result.option_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7449354445591372136UL && ReadUInt16(ref b, 8) == 25959 && ReadByte(ref b, 10) == 100)
                {
                    result.has_flagged = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1853189987U && ReadByte(ref b, 4) == 116)
                {
                    result.count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<byte> writer, FlagOption value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.option_id != null)
            {
                writer.WriteUtf8Verbatim(_option_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.option_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_comment != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_requires_commentName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.requires_comment, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_requires_siteName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.requires_site, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_question_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_requires_question_idName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.requires_question_id, nestingLimit);
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

            if (value.sub_options != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_sub_optionsName);
                ListFormatter<List<FlagOption>, FlagOption, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.sub_options,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.has_flagged != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_has_flaggedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.has_flagged, nestingLimit);
                writeSeparator = true;
            }

            if (value.count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.count, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}