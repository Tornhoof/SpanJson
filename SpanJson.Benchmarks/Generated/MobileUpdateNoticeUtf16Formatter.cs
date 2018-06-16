using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MobileUpdateNoticeUtf16Formatter : BaseGeneratedFormatter<MobileUpdateNotice, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<MobileUpdateNotice, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _should_updateName = "\"should_update\":";
        private const string _messageName = "\"message\":";
        private const string _minimum_supported_versionName = "\"minimum_supported_version\":";
        public static readonly MobileUpdateNoticeUtf16Formatter Default = new MobileUpdateNoticeUtf16Formatter();

        public MobileUpdateNotice Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new MobileUpdateNotice();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 13 && ReadUInt64(ref b, 0) == 32933049023332467UL && ReadUInt64(ref b, 8) == 32932980303593580UL &&
                    ReadUInt64(ref b, 16) == 32651513916817520UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.should_update = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32370116249583725UL && ReadUInt32(ref b, 8) == 6750305U && ReadUInt16(ref b, 12) == 101)
                {
                    result.message = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 25 && ReadUInt64(ref b, 0) == 29555345007902829UL && ReadUInt64(ref b, 8) == 26740590946615405UL &&
                    ReadUInt64(ref b, 16) == 31525678435598451UL && ReadUInt64(ref b, 24) == 28429470871453807UL &&
                    ReadUInt64(ref b, 32) == 28429479460143204UL && ReadUInt64(ref b, 40) == 31244173393985650UL && ReadUInt16(ref b, 48) == 110)
                {
                    result.minimum_supported_version = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, MobileUpdateNotice value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.should_update != null)
            {
                writer.WriteUtf16Verbatim(_should_updateName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.should_update, nestingLimit);
                writeSeparator = true;
            }

            if (value.message != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_messageName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.message, nestingLimit);
                writeSeparator = true;
            }

            if (value.minimum_supported_version != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_minimum_supported_versionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.minimum_supported_version, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}