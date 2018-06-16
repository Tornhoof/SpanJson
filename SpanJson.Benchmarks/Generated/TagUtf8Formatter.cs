using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class TagUtf8Formatter : BaseGeneratedFormatter<Tag, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Tag, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly TagUtf8Formatter Default = new TagUtf8Formatter();
        private readonly byte[] _countName = Encoding.UTF8.GetBytes("\"count\":");
        private readonly byte[] _has_synonymsName = Encoding.UTF8.GetBytes("\"has_synonyms\":");
        private readonly byte[] _is_moderator_onlyName = Encoding.UTF8.GetBytes("\"is_moderator_only\":");
        private readonly byte[] _is_requiredName = Encoding.UTF8.GetBytes("\"is_required\":");
        private readonly byte[] _last_activity_dateName = Encoding.UTF8.GetBytes("\"last_activity_date\":");
        private readonly byte[] _nameName = Encoding.UTF8.GetBytes("\"name\":");
        private readonly byte[] _synonymsName = Encoding.UTF8.GetBytes("\"synonyms\":");
        private readonly byte[] _user_idName = Encoding.UTF8.GetBytes("\"user_id\":");

        public Tag Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Tag();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 18 && ReadUInt64(ref b, 0) == 8386653993697501548UL && ReadUInt64(ref b, 8) == 7017839094598825577UL &&
                    ReadUInt16(ref b, 16) == 25972)
                {
                    result.last_activity_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 8317437602046048627UL)
                {
                    result.synonyms = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 8243105122710680425UL && ReadUInt64(ref b, 8) == 7813304859266872417UL &&
                    ReadByte(ref b, 16) == 121)
                {
                    result.is_moderator_only = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 8029488722077770088UL && ReadUInt32(ref b, 8) == 1936554350U)
                {
                    result.has_synonyms = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7599104626782073705UL && ReadUInt16(ref b, 8) == 25970 && ReadByte(ref b, 10) == 100)
                {
                    result.is_required = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1919251317U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.user_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1853189987U && ReadByte(ref b, 4) == 116)
                {
                    result.count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1701667182U)
                {
                    result.name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Tag value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.name != null)
            {
                writer.WriteUtf8Verbatim(_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.name, nestingLimit);
                writeSeparator = true;
            }

            if (value.count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.count, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_required != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_requiredName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_required, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_moderator_only != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_moderator_onlyName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_moderator_only, nestingLimit);
                writeSeparator = true;
            }

            if (value.user_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_user_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.has_synonyms != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_has_synonymsName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.has_synonyms, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_activity_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_activity_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.last_activity_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.synonyms != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_synonymsName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.synonyms, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}