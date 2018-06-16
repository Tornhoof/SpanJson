using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class MobileBannerAdUtf8Formatter : BaseGeneratedFormatter<MobileBannerAd, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileBannerAd, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileBannerAdUtf8Formatter Default = new MobileBannerAdUtf8Formatter();
        private readonly byte[] _added_dateName = Encoding.UTF8.GetBytes("\"added_date\":");
        private readonly byte[] _group_idName = Encoding.UTF8.GetBytes("\"group_id\":");
        private readonly byte[] _imagesName = Encoding.UTF8.GetBytes("\"images\":");
        private readonly byte[] _linkName = Encoding.UTF8.GetBytes("\"link\":");

        public MobileBannerAd Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileBannerAd();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 8 && ReadUInt64(ref b, 0) == 7235419212958626407UL)
                {
                    result.group_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7017839004152521825UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.added_date = NullableInt64Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1802398060U)
                {
                    result.link = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1734438249U && ReadUInt16(ref b, 4) == 29541)
                {
                    result.images = ListFormatter<List<MobileBannerAd.MobileBannerAdImage>, MobileBannerAd.MobileBannerAdImage, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default
                        .Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobileBannerAd value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.link != null)
            {
                writer.WriteUtf8Verbatim(_linkName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.images != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_imagesName);
                ListFormatter<List<MobileBannerAd.MobileBannerAdImage>, MobileBannerAd.MobileBannerAdImage, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer,
                    value.images, nestingLimit);
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