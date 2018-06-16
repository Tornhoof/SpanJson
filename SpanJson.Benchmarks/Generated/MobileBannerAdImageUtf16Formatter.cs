using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MobileBannerAdImageUtf16Formatter :
        BaseGeneratedFormatter<MobileBannerAd.MobileBannerAdImage, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileBannerAd.MobileBannerAdImage, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _image_urlName = "\"image_url\":";
        private const string _widthName = "\"width\":";
        private const string _heightName = "\"height\":";
        public static readonly MobileBannerAdImageUtf16Formatter Default = new MobileBannerAdImageUtf16Formatter();

        public MobileBannerAd.MobileBannerAdImage Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileBannerAd.MobileBannerAdImage();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 5 && ReadUInt64(ref b, 0) == 32651526802047095UL && ReadUInt16(ref b, 8) == 104)
                {
                    result.width = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref b, 0) == 28992373579382888UL && ReadUInt32(ref b, 8) == 7602280U)
                {
                    result.height = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 28992339220168809UL && ReadUInt64(ref b, 8) == 32088649862414437UL && ReadUInt16(ref b, 16) == 108)
                {
                    result.image_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileBannerAd.MobileBannerAdImage value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.image_url != null)
            {
                writer.WriteUtf16Verbatim(_image_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.image_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.width != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_widthName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.width, nestingLimit);
                writeSeparator = true;
            }

            if (value.height != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_heightName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.height, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}