using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class SearchExcerptUtf8Formatter : BaseGeneratedFormatter<SearchExcerpt, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<SearchExcerpt, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly SearchExcerptUtf8Formatter Default = new SearchExcerptUtf8Formatter();
        private readonly byte[] _answer_countName = Encoding.UTF8.GetBytes("\"answer_count\":");
        private readonly byte[] _answer_idName = Encoding.UTF8.GetBytes("\"answer_id\":");
        private readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private readonly byte[] _closed_dateName = Encoding.UTF8.GetBytes("\"closed_date\":");
        private readonly byte[] _community_owned_dateName = Encoding.UTF8.GetBytes("\"community_owned_date\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _excerptName = Encoding.UTF8.GetBytes("\"excerpt\":");
        private readonly byte[] _is_acceptedName = Encoding.UTF8.GetBytes("\"is_accepted\":");
        private readonly byte[] _is_answeredName = Encoding.UTF8.GetBytes("\"is_answered\":");
        private readonly byte[] _item_typeName = Encoding.UTF8.GetBytes("\"item_type\":");
        private readonly byte[] _last_activity_dateName = Encoding.UTF8.GetBytes("\"last_activity_date\":");
        private readonly byte[] _last_activity_userName = Encoding.UTF8.GetBytes("\"last_activity_user\":");
        private readonly byte[] _locked_dateName = Encoding.UTF8.GetBytes("\"locked_date\":");
        private readonly byte[] _ownerName = Encoding.UTF8.GetBytes("\"owner\":");
        private readonly byte[] _question_idName = Encoding.UTF8.GetBytes("\"question_id\":");
        private readonly byte[] _scoreName = Encoding.UTF8.GetBytes("\"score\":");
        private readonly byte[] _tagsName = Encoding.UTF8.GetBytes("\"tags\":");
        private readonly byte[] _titleName = Encoding.UTF8.GetBytes("\"title\":");

        public SearchExcerpt Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new SearchExcerpt();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 20 && ReadUInt64(ref b, 0) == 8388357231580376931UL && ReadUInt64(ref b, 8) == 6873730456398815097UL &&
                    ReadUInt32(ref b, 16) == 1702125924U)
                {
                    result.community_owned_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 8 && ReadUInt64(ref b, 0) == 8386653993697501548UL)
                {
                    if (length == 18 && ReadUInt64(ref b, 8) == 8319660861885609577UL && ReadUInt16(ref b, 16) == 29285)
                    {
                        result.last_activity_user = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 18 && ReadUInt64(ref b, 8) == 7017839094598825577UL && ReadUInt16(ref b, 16) == 25972)
                    {
                        result.last_activity_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf8Segment();
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 8104636957635998825UL && ReadByte(ref b, 8) == 101)
                {
                    result.item_type = NullableFormatter<SearchExcerptItemType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 8098988783382262633UL && ReadUInt16(ref b, 8) == 25972 && ReadByte(ref b, 10) == 100)
                {
                    result.is_accepted = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7957695015460107633UL && ReadUInt16(ref b, 8) == 26975 && ReadByte(ref b, 10) == 100)
                {
                    result.question_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 7592913276891262561UL && ReadByte(ref b, 8) == 100)
                {
                    result.answer_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7311439437976531817UL && ReadUInt16(ref b, 8) == 25970 && ReadByte(ref b, 10) == 100)
                {
                    result.is_answered = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7232609913471462499UL && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                {
                    result.closed_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7232609913336459116UL && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                {
                    result.locked_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 7160567712663694945UL && ReadUInt32(ref b, 8) == 1953396079U)
                {
                    result.answer_count = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1936154996U)
                {
                    result.tags = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1919902579U && ReadByte(ref b, 4) == 101)
                {
                    result.score = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1819568500U && ReadByte(ref b, 4) == 101)
                {
                    result.title = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1701738351U && ReadByte(ref b, 4) == 114)
                {
                    result.owner = ShallowUserUtf8Formatter.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<byte> writer, SearchExcerpt value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.title != null)
            {
                writer.WriteUtf8Verbatim(_titleName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.title, nestingLimit);
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

            if (value.community_owned_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_community_owned_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.community_owned_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.locked_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_locked_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.locked_date, nestingLimit);
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

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_ownerName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.owner, nestingLimit);
                writeSeparator = true;
            }

            if (value.last_activity_user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_last_activity_userName);
                ShallowUserUtf8Formatter.Default.Serialize(ref writer, value.last_activity_user, nestingLimit);
                writeSeparator = true;
            }

            if (value.score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_scoreName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.score, nestingLimit);
                writeSeparator = true;
            }

            if (value.item_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_item_typeName);
                NullableFormatter<SearchExcerptItemType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.item_type,
                    nestingLimit);
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

            if (value.question_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_question_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.question_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_answered != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_answeredName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_answered, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_count != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answer_countName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answer_count, nestingLimit);
                writeSeparator = true;
            }

            if (value.tags != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_tagsName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.tags, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_closed_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.closed_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.answer_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_answer_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.answer_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_accepted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_acceptedName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_accepted, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}