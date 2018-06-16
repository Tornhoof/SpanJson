using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagWikiUtf16Formatter : BaseGeneratedFormatter<TagWiki, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<TagWiki, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _tag_nameName = "\"tag_name\":";
        private const string _bodyName = "\"body\":";
        private const string _excerptName = "\"excerpt\":";
        private const string _body_last_edit_dateName = "\"body_last_edit_date\":";
        private const string _excerpt_last_edit_dateName = "\"excerpt_last_edit_date\":";
        private const string _last_body_editorName = "\"last_body_editor\":";
        private const string _last_excerpt_editorName = "\"last_excerpt_editor\":";
        public static readonly TagWikiUtf16Formatter Default = new TagWikiUtf16Formatter();

        public TagWiki Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new TagWiki();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 34058901685993570UL)
                {
                    if (length == 19 && ReadUInt64(ref b, 8) == 32370038940631135UL && ReadUInt64(ref b, 16) == 28147931468988532UL &&
                        ReadUInt64(ref b, 24) == 28147905700561001UL && ReadUInt32(ref b, 32) == 7602273U && ReadUInt16(ref b, 36) == 101)
                    {
                        result.body_last_edit_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 4)
                    {
                        result.body = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32651591226032236UL)
                {
                    if (length == 16 && ReadUInt64(ref b, 8) == 28147974418858079UL && ReadUInt64(ref b, 16) == 28147931468988537UL &&
                        ReadUInt64(ref b, 24) == 32088624093986921UL)
                    {
                        result.last_body_editor = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 19 && ReadUInt64(ref b, 8) == 27866538097049695UL && ReadUInt64(ref b, 16) == 32651578342244453UL &&
                        ReadUInt64(ref b, 24) == 29555302057967711UL && ReadUInt32(ref b, 32) == 7274612U && ReadUInt16(ref b, 36) == 114)
                    {
                        result.last_excerpt_editor = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 28429397857402981UL)
                {
                    if (length == 22 && ReadUInt64(ref b, 8) == 26740621011058802UL && ReadUInt64(ref b, 16) == 32651591226032236UL &&
                        ReadUInt64(ref b, 24) == 29555302057967711UL && ReadUInt64(ref b, 32) == 27303502243889268UL && ReadUInt32(ref b, 40) == 6619252U)
                    {
                        result.excerpt_last_edit_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref b, 8) == 7340146U && ReadUInt16(ref b, 12) == 116)
                    {
                        result.excerpt = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 26740565175500916UL && ReadUInt64(ref b, 8) == 28429440805568622UL)
                {
                    result.tag_name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, TagWiki value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.tag_name != null)
            {
                writer.WriteUtf16Verbatim(_tag_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tag_name, nestingLimit);
                writeSeparator = true;
            }

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bodyName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body, nestingLimit);
                writeSeparator = true;
            }

            if (value.excerpt != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_excerptName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.excerpt, nestingLimit);
                writeSeparator = true;
            }

            if (value.body_last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_body_last_edit_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body_last_edit_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.excerpt_last_edit_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_excerpt_last_edit_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.excerpt_last_edit_date,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.last_body_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_body_editorName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.last_body_editor, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_excerpt_editor != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_excerpt_editorName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.last_excerpt_editor, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}