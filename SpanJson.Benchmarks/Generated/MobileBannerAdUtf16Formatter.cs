using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileBannerAdUtf16Formatter : BaseGeneratedFormatter<MobileBannerAd, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileBannerAd, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _linkName = "\"link\":";
        private const string _imagesName = "\"images\":";
        private const string _group_idName = "\"group_id\":";
        private const string _added_dateName = "\"added_date\":";
        public static readonly MobileBannerAdUtf16Formatter Default = new MobileBannerAdUtf16Formatter();

        public MobileBannerAd Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileBannerAd();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 8 && ReadUInt64(ref b, 0) == 32933049023987815UL && ReadUInt64(ref b, 8) == 28147948648857712UL)
                {
                    result.group_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 28992339220168809UL && ReadUInt32(ref b, 8) == 7536741U)
                {
                    result.images = ListFormatter<List<MobileBannerAd.MobileBannerAdImage>, MobileBannerAd.MobileBannerAdImage, char, ExcludeNullsOriginalCaseResolver<char>>.Default
                        .Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 28429402151059553UL && ReadUInt64(ref b, 8) == 27303502243889252UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.added_date = NullableInt64Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileBannerAd value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.link != null)
            {
                writer.WriteUtf16Verbatim(_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.images != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_imagesName);
                ListFormatter<List<MobileBannerAd.MobileBannerAdImage>, MobileBannerAd.MobileBannerAdImage, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.images, nestingLimit);
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