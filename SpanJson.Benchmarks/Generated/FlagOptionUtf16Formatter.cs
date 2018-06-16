using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class FlagOptionUtf16Formatter : BaseGeneratedFormatter<FlagOption, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<FlagOption, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _option_idName = "\"option_id\":";
        private const string _requires_commentName = "\"requires_comment\":";
        private const string _requires_siteName = "\"requires_site\":";
        private const string _requires_question_idName = "\"requires_question_id\":";
        private const string _titleName = "\"title\":";
        private const string _descriptionName = "\"description\":";
        private const string _sub_optionsName = "\"sub_options\":";
        private const string _has_flaggedName = "\"has_flagged\":";
        private const string _countName = "\"count\":";
        public static readonly FlagOptionUtf16Formatter Default = new FlagOptionUtf16Formatter();

        public FlagOption Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new FlagOption();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 32933057613070450UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 32370056120893545UL)
                    {
                        if (length == 13 && ReadUInt64(ref b, 16) == 32651548277538911UL && ReadUInt16(ref b, 24) == 101)
                        {
                            result.requires_site = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 16 && ReadUInt64(ref b, 16) == 30681249209319519UL && ReadUInt64(ref b, 24) == 32651569751457901UL)
                        {
                            result.requires_comment = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 20 && ReadUInt64(ref b, 16) == 28429475166355551UL && ReadUInt64(ref b, 24) == 31244173394051187UL &&
                            ReadUInt64(ref b, 32) == 28147948648857710UL)
                        {
                            result.requires_question_id = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30962749956620387UL && ReadUInt16(ref b, 8) == 116)
                {
                    result.count = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30399795707838580UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 29555370778165359UL && ReadUInt64(ref b, 8) == 29555280583721071UL && ReadUInt16(ref b, 16) == 100)
                {
                    result.option_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 27866516622213220UL && ReadUInt64(ref b, 8) == 32651578341654642UL &&
                    ReadUInt32(ref b, 16) == 7274601U && ReadUInt16(ref b, 20) == 110)
                {
                    result.description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 26740616715108456UL && ReadUInt64(ref b, 8) == 28992339220103270UL &&
                    ReadUInt32(ref b, 16) == 6619239U && ReadUInt16(ref b, 20) == 100)
                {
                    result.has_flagged = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 26740543701975155UL && ReadUInt64(ref b, 8) == 29555370778165359UL &&
                    ReadUInt32(ref b, 16) == 7209071U && ReadUInt16(ref b, 20) == 115)
                {
                    result.sub_options =
                        ListFormatter<List<FlagOption>, FlagOption, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, FlagOption value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.option_id != null)
            {
                writer.WriteUtf16Verbatim(_option_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.option_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_comment != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_requires_commentName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.requires_comment, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_requires_siteName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.requires_site, nestingLimit);
                writeSeparator = true;
            }

            if (value.requires_question_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_requires_question_idName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.requires_question_id, nestingLimit);
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

            if (value.sub_options != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_sub_optionsName);
                ListFormatter<List<FlagOption>, FlagOption, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.sub_options,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.has_flagged != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_has_flaggedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.has_flagged, nestingLimit);
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

            writer.WriteUtf16EndObject();
        }
    }
}