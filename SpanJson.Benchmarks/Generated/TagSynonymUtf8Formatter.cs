using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class TagSynonymUtf8Formatter : BaseGeneratedFormatter<TagSynonym, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<TagSynonym, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly TagSynonymUtf8Formatter Default = new TagSynonymUtf8Formatter();
        private readonly byte[] _applied_countName = Encoding.UTF8.GetBytes("\"applied_count\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _from_tagName = Encoding.UTF8.GetBytes("\"from_tag\":");
        private readonly byte[] _last_applied_dateName = Encoding.UTF8.GetBytes("\"last_applied_date\":");
        private readonly byte[] _to_tagName = Encoding.UTF8.GetBytes("\"to_tag\":");

        public TagSynonym Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new TagSynonym();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 17 && ReadUInt64(ref b, 0) == 8102082792243028332UL && ReadUInt64(ref b, 8) == 8386094342009612652UL &&
                    ReadByte(ref b, 16) == 101)
                {
                    result.last_applied_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7449363211854246502UL)
                {
                    result.from_tag = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6873730434739499105UL && ReadUInt32(ref b, 8) == 1853189987U && ReadByte(ref b, 12) == 116)
                {
                    result.applied_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1952411508U && ReadUInt16(ref b, 4) == 26465)
                {
                    result.to_tag = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, TagSynonym value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.from_tag != null)
            {
                writer.WriteUtf8Verbatim(_from_tagName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.from_tag, nestingLimit);
                writeSeparator = true;
            }

            if (value.to_tag != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_to_tagName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.to_tag, nestingLimit);
                writeSeparator = true;
            }

            if (value.applied_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_applied_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.applied_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_applied_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_applied_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_applied_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_creation_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}