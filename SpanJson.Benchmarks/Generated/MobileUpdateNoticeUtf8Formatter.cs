using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class MobileUpdateNoticeUtf8Formatter : BaseGeneratedFormatter<MobileUpdateNotice, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<MobileUpdateNotice, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly MobileUpdateNoticeUtf8Formatter Default = new MobileUpdateNoticeUtf8Formatter();
        private readonly byte[] _messageName = Encoding.UTF8.GetBytes("\"message\":");
        private readonly byte[] _minimum_supported_versionName = Encoding.UTF8.GetBytes("\"minimum_supported_version\":");
        private readonly byte[] _should_updateName = Encoding.UTF8.GetBytes("\"should_update\":");

        public MobileUpdateNotice Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new MobileUpdateNotice();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 13 && ReadUInt64(ref b, 0) == 8457589042214561907UL && ReadUInt32(ref b, 8) == 1952539760U && ReadByte(ref b, 12) == 101)
                {
                    result.should_update = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 25 && ReadUInt64(ref b, 0) == 6876281318845344109UL && ReadUInt64(ref b, 8) == 7310593918082512243UL &&
                    ReadUInt64(ref b, 16) == 8028074745930342244UL && ReadByte(ref b, 24) == 110)
                {
                    result.minimum_supported_version = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1936942445U && ReadUInt16(ref b, 4) == 26465 && ReadByte(ref b, 6) == 101)
                {
                    result.message = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, MobileUpdateNotice value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.should_update != null)
            {
                writer.WriteUtf8Verbatim(_should_updateName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.should_update, nestingLimit);
                writeSeparator = true;
            }

            if (value.message != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_messageName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.message, nestingLimit);
                writeSeparator = true;
            }

            if (value.minimum_supported_version != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_minimum_supported_versionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.minimum_supported_version, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}