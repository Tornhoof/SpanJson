using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MobileBannerAdImageUtf8Formatter :
        BaseGeneratedFormatter<MobileBannerAd.MobileBannerAdImage, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileBannerAd.MobileBannerAdImage, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileBannerAdImageUtf8Formatter Default = new MobileBannerAdImageUtf8Formatter();
        private readonly byte[] _heightName = Encoding.UTF8.GetBytes("\"height\":");
        private readonly byte[] _image_urlName = Encoding.UTF8.GetBytes("\"image_url\":");
        private readonly byte[] _widthName = Encoding.UTF8.GetBytes("\"width\":");

        public MobileBannerAd.MobileBannerAdImage Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileBannerAd.MobileBannerAdImage();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 9 && ReadUInt64(ref b, 0) == 8247603181729705321UL && ReadByte(ref b, 8) == 108)
                {
                    result.image_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1952737655U && ReadByte(ref b, 4) == 104)
                {
                    result.width = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1734960488U && ReadUInt16(ref b, 4) == 29800)
                {
                    result.height = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobileBannerAd.MobileBannerAdImage value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.image_url != null)
            {
                writer.WriteUtf8Verbatim(_image_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.image_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.width != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_widthName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.width, nestingLimit);
                writeSeparator = true;
            }

            if (value.height != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_heightName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.height, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}