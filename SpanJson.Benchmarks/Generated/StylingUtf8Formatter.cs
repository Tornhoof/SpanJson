using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class StylingUtf8Formatter : BaseGeneratedFormatter<Info.Site.Styling, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Info.Site.Styling, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly StylingUtf8Formatter Default = new StylingUtf8Formatter();
        private readonly byte[] _link_colorName = Encoding.UTF8.GetBytes("\"link_color\":");
        private readonly byte[] _tag_background_colorName = Encoding.UTF8.GetBytes("\"tag_background_color\":");
        private readonly byte[] _tag_foreground_colorName = Encoding.UTF8.GetBytes("\"tag_foreground_color\":");

        public Info.Site.Styling Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Info.Site.Styling();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 10 && ReadUInt64(ref b, 0) == 7813573139986540908UL && ReadUInt16(ref b, 8) == 29295)
                {
                    result.link_color = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref b, 0) == 7738135659887944052UL && ReadUInt64(ref b, 8) == 7160552358121796199UL &&
                    ReadUInt32(ref b, 16) == 1919904879U)
                {
                    result.tag_background_color = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 20 && ReadUInt64(ref b, 0) == 7310027630653694324UL && ReadUInt64(ref b, 8) == 7160552358121796199UL &&
                    ReadUInt32(ref b, 16) == 1919904879U)
                {
                    result.tag_foreground_color = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Info.Site.Styling value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.link_color != null)
            {
                writer.WriteUtf8Verbatim(_link_colorName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.link_color, nestingLimit);
                writeSeparator = true;
            }

            if (value.tag_foreground_color != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_tag_foreground_colorName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tag_foreground_color, nestingLimit);
                writeSeparator = true;
            }

            if (value.tag_background_color != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_tag_background_colorName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tag_background_color, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}