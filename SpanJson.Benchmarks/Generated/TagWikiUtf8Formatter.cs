using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagWikiUtf8Formatter : BaseGeneratedFormatter<TagWiki, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<TagWiki, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly TagWikiUtf8Formatter Default = new TagWikiUtf8Formatter();
        private readonly byte[] _body_last_edit_dateName = Encoding.UTF8.GetBytes("\"body_last_edit_date\":");
        private readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private readonly byte[] _excerpt_last_edit_dateName = Encoding.UTF8.GetBytes("\"excerpt_last_edit_date\":");
        private readonly byte[] _excerptName = Encoding.UTF8.GetBytes("\"excerpt\":");
        private readonly byte[] _last_body_editorName = Encoding.UTF8.GetBytes("\"last_body_editor\":");
        private readonly byte[] _last_excerpt_editorName = Encoding.UTF8.GetBytes("\"last_excerpt_editor\":");
        private readonly byte[] _tag_nameName = Encoding.UTF8.GetBytes("\"tag_name\":");

        public TagWiki Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new TagWiki();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 19 && ReadUInt64(ref b, 0) == 8314045544416964450UL && ReadUInt64(ref b, 8) == 7232627522585059188UL &&
                    ReadUInt16(ref b, 16) == 29793 && ReadByte(ref b, 18) == 101)
                {
                    result.body_last_edit_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7308604896967090548UL)
                {
                    result.tag_name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 7237111288322810220UL && ReadUInt64(ref b, 8) == 8245937438743420793UL)
                {
                    result.last_body_editor = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 7167590267610161516UL && ReadUInt64(ref b, 8) == 7594306332303323749UL &&
                    ReadUInt16(ref b, 16) == 28532 && ReadByte(ref b, 18) == 114)
                {
                    result.last_excerpt_editor = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 22 && ReadUInt64(ref b, 0) == 6878246167531190373UL && ReadUInt64(ref b, 8) == 7594306332303516012UL &&
                    ReadUInt32(ref b, 16) == 1633967988U && ReadUInt16(ref b, 20) == 25972)
                {
                    result.excerpt_last_edit_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1701017701U && ReadUInt16(ref b, 4) == 28786 && ReadByte(ref b, 6) == 116)
                {
                    result.excerpt = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, TagWiki value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.tag_name != null)
            {
                writer.WriteUtf8Verbatim(_tag_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tag_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bodyName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body, nestingLimit);
                writeSeparator = true;
            }

            if (value.excerpt != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_excerptName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.excerpt, nestingLimit);
                writeSeparator = true;
            }

            if (value.body_last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_body_last_edit_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body_last_edit_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.excerpt_last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_excerpt_last_edit_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.excerpt_last_edit_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_body_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_body_editorName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.last_body_editor, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_excerpt_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_excerpt_editorName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.last_excerpt_editor, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}