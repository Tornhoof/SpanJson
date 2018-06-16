using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class RevisionUtf16Formatter : BaseGeneratedFormatter<Revision, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Revision, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _revision_guidName = "\"revision_guid\":";
        private const string _revision_numberName = "\"revision_number\":";
        private const string _revision_typeName = "\"revision_type\":";
        private const string _post_typeName = "\"post_type\":";
        private const string _post_idName = "\"post_id\":";
        private const string _commentName = "\"comment\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _is_rollbackName = "\"is_rollback\":";
        private const string _last_bodyName = "\"last_body\":";
        private const string _last_titleName = "\"last_title\":";
        private const string _last_tagsName = "\"last_tags\":";
        private const string _bodyName = "\"body\":";
        private const string _titleName = "\"title\":";
        private const string _tagsName = "\"tags\":";
        private const string _set_community_wikiName = "\"set_community_wiki\":";
        private const string _userName = "\"user\":";
        public static readonly RevisionUtf16Formatter Default = new RevisionUtf16Formatter();

        public Revision Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Revision();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 4 && ReadUInt64(ref b, 0) == 34058901685993570UL)
                {
                    result.body = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32651591226949744UL)
                {
                    if (length == 9 && ReadUInt64(ref b, 8) == 31525717090238559UL && ReadUInt16(ref b, 16) == 101)
                    {
                        result.post_type = NullableFormatter<PostType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                    {
                        result.post_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 32651591226032236UL)
                {
                    if (length == 10 && ReadUInt64(ref b, 8) == 32651548277604447UL && ReadUInt32(ref b, 16) == 6619244U)
                    {
                        result.last_title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 9 && ReadUInt64(ref b, 8) == 28992339220627551UL && ReadUInt16(ref b, 16) == 115)
                    {
                        result.last_tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 9 && ReadUInt64(ref b, 8) == 28147974418858079UL && ReadUInt16(ref b, 16) == 121)
                    {
                        result.last_body = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 32088581144248437UL)
                {
                    result.user = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 32088555374444649UL && ReadUInt64(ref b, 8) == 27585011581190255UL &&
                    ReadUInt32(ref b, 16) == 6488161U && ReadUInt16(ref b, 20) == 107)
                {
                    result.is_rollback = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 30681240620171363UL && ReadUInt32(ref b, 8) == 7209061U && ReadUInt16(ref b, 12) == 116)
                {
                    result.comment = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30399795707838580UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 29555379367379058UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 30962724186423411UL)
                    {
                        if (length == 13 && ReadUInt64(ref b, 16) == 31525717090238559UL && ReadUInt16(ref b, 24) == 101)
                        {
                            result.revision_type =
                                NullableFormatter<RevisionType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 15 && ReadUInt64(ref b, 16) == 30681274979844191UL && ReadUInt32(ref b, 24) == 6619234U && ReadUInt16(ref b, 28) == 114)
                        {
                            result.revision_number = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 13 && ReadUInt64(ref b, 16) == 29555375072542815UL && ReadUInt16(ref b, 24) == 100)
                        {
                            result.revision_guid = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 26740621010337907UL && ReadUInt64(ref b, 8) == 30681240620171363UL &&
                    ReadUInt64(ref b, 16) == 32651548277211253UL && ReadUInt64(ref b, 24) == 29555383661953145UL && ReadUInt32(ref b, 32) == 6881387U)
                {
                    result.set_community_wiki = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Revision value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.revision_guid != null)
            {
                writer.WriteUtf16Verbatim(_revision_guidName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.revision_guid, nestingLimit);
                writeSeparator = true;
            }

            if (value.revision_number != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_revision_numberName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.revision_number, nestingLimit);
                writeSeparator = true;
            }

            if (value.revision_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_revision_typeName);
                NullableFormatter<RevisionType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.revision_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_post_typeName);
                NullableFormatter<PostType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.post_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_post_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.comment != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_commentName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.comment, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_creation_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_rollback != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_rollbackName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_rollback, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_bodyName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_body, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_title != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_titleName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_title, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_last_tagsName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.last_tags, nestingLimit);
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

            if (value.title != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_titleName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.title, nestingLimit);
                writeSeparator = true;
            }

            if (value.tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_tagsName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.tags, nestingLimit);
                writeSeparator = true;
            }

            if (value.set_community_wiki != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_set_community_wikiName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.set_community_wiki, nestingLimit);
                writeSeparator = true;
            }

            if (value.user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_userName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.user, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}