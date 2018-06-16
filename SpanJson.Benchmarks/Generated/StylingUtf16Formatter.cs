using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class StylingUtf16Formatter : BaseGeneratedFormatter<Info.Site.Styling, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Info.Site.Styling, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _link_colorName = "\"link_color\":";
        private const string _tag_foreground_colorName = "\"tag_foreground_color\":";
        private const string _tag_background_colorName = "\"tag_background_color\":";
        public static readonly StylingUtf16Formatter Default = new StylingUtf16Formatter();

        public Info.Site.Styling Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Info.Site.Styling();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 10 && ReadUInt64(ref b, 0) == 30118294961324140UL && ReadUInt64(ref b, 8) == 30399774232608863UL &&
                    ReadUInt32(ref b, 16) == 7471215U)
                {
                    result.link_color = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 26740565175500916UL)
                {
                    if (length == 20 && ReadUInt64(ref b, 8) == 30118247716159586UL && ReadUInt64(ref b, 16) == 32933049023987815UL &&
                        ReadUInt64(ref b, 24) == 27866430722801774UL && ReadUInt64(ref b, 32) == 32088624093462639UL)
                    {
                        result.tag_background_color = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 20 && ReadUInt64(ref b, 8) == 28429462281322598UL && ReadUInt64(ref b, 16) == 32933049023987815UL &&
                        ReadUInt64(ref b, 24) == 27866430722801774UL && ReadUInt64(ref b, 32) == 32088624093462639UL)
                    {
                        result.tag_foreground_color = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Info.Site.Styling value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.link_color != null)
            {
                writer.WriteUtf16Verbatim(_link_colorName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link_color, nestingLimit);
                writeSeparator = true;
            }

            if (value.tag_foreground_color != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_tag_foreground_colorName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tag_foreground_color, nestingLimit);
                writeSeparator = true;
            }

            if (value.tag_background_color != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_tag_background_colorName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tag_background_color, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}