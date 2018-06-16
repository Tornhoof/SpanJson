using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class SuggestedEditUtf16Formatter : BaseGeneratedFormatter<SuggestedEdit, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<SuggestedEdit, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _suggested_edit_idName = "\"suggested_edit_id\":";
        private const string _post_idName = "\"post_id\":";
        private const string _post_typeName = "\"post_type\":";
        private const string _bodyName = "\"body\":";
        private const string _titleName = "\"title\":";
        private const string _tagsName = "\"tags\":";
        private const string _commentName = "\"comment\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _approval_dateName = "\"approval_date\":";
        private const string _rejection_dateName = "\"rejection_date\":";
        private const string _proposing_userName = "\"proposing_user\":";
        public static readonly SuggestedEditUtf16Formatter Default = new SuggestedEditUtf16Formatter();

        public SuggestedEdit Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new SuggestedEdit();
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

                if (length == 4 && ReadUInt64(ref b, 0) == 32370064709714036UL)
                {
                    result.tags = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 32088628388692065UL && ReadUInt64(ref b, 8) == 30399714104311919UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.approval_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 31525674140434544UL && ReadUInt64(ref b, 8) == 30962698417274991UL &&
                    ReadUInt64(ref b, 16) == 32370124839125095UL && ReadUInt32(ref b, 24) == 7471205U)
                {
                    result.proposing_user = ShallowUserUtf16Formatter.Default.Deserialize(ref reader);
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

                if (length == 17 && ReadUInt64(ref b, 0) == 28992364990496883UL && ReadUInt64(ref b, 8) == 28429470871519333UL &&
                    ReadUInt64(ref b, 16) == 28147931468988516UL && ReadUInt64(ref b, 24) == 29555280584114281UL && ReadUInt16(ref b, 32) == 100)
                {
                    result.suggested_edit_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 28429427920928882UL && ReadUInt64(ref b, 8) == 31244173394051171UL &&
                    ReadUInt64(ref b, 16) == 27303502243889262UL && ReadUInt32(ref b, 24) == 6619252U)
                {
                    result.rejection_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303506540101731UL && ReadUInt64(ref b, 8) == 30962724186423412UL &&
                    ReadUInt64(ref b, 16) == 32651513916817503UL && ReadUInt16(ref b, 24) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, SuggestedEdit value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.suggested_edit_id != null)
            {
                writer.WriteUtf16Verbatim(_suggested_edit_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.suggested_edit_id, nestingLimit);
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

            if (value.approval_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_approval_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.approval_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.rejection_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_rejection_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.rejection_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.proposing_user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_proposing_userName);
                ShallowUserUtf16Formatter.Default.Serialize(ref writer, value.proposing_user, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}