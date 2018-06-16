using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class CommentUtf16Formatter<TResolver> : BaseGeneratedFormatter<Comment, char, TResolver>, IJsonFormatter<Comment, char, TResolver>
        where TResolver : class, IJsonFormatterResolver<char, TResolver>, new()
    {
        private readonly char[] _comment_idName = "\"comment_id\":".ToCharArray();
        private readonly char[] _post_idName = "\"post_id\":".ToCharArray();
        private readonly char[] _creation_dateName = "\"creation_date\":".ToCharArray();
        private readonly char[] _post_typeName = "\"post_type\":".ToCharArray();
        private readonly char[] _scoreName = "\"score\":".ToCharArray();
        private readonly char[] _editedName = "\"edited\":".ToCharArray();
        private readonly char[] _bodyName = "\"body\":".ToCharArray();
        private readonly char[] _ownerName = "\"owner\":".ToCharArray();
        private readonly char[] _reply_to_userName = "\"reply_to_user\":".ToCharArray();
        private readonly char[] _linkName = "\"link\":".ToCharArray();
        private readonly char[] _body_markdownName = "\"body_markdown\":".ToCharArray();
        private readonly char[] _upvotedName = "\"upvoted\":".ToCharArray();
        public static readonly CommentUtf16Formatter<TResolver> Default = new CommentUtf16Formatter<TResolver>();

        public Comment Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Comment();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var c = ref MemoryMarshal.GetReference(name);
                if (length >= 4 && ReadUInt64(ref c, 0) == 34058901685993570UL)
                {
                    if (length == 13 && ReadUInt64(ref c, 4) == 32088563963986015UL && ReadUInt64(ref c, 8) == 33495998976491627UL &&
                        ReadUInt16(ref c, 12) == 110)
                    {
                        result.body_markdown = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

                if (length >= 4 && ReadUInt64(ref c, 0) == 32651591226949744UL)
                {
                    if (length == 9 && ReadUInt64(ref c, 4) == 31525717090238559UL && ReadUInt16(ref c, 8) == 101)
                    {
                        result.post_type = NullableFormatter<PostType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 7 && ReadUInt32(ref c, 4) == 6881375U && ReadUInt16(ref c, 6) == 100)
                    {
                        result.post_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 6 && ReadUInt64(ref c, 0) == 32651548276555877UL && ReadUInt32(ref c, 4) == 6553701U)
                {
                    result.edited = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref c, 0) == 32088624092872819UL && ReadUInt16(ref c, 4) == 101)
                {
                    result.score = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref c, 0) == 31244229228363893UL && ReadUInt32(ref c, 4) == 6619252U && ReadUInt16(ref c, 6) == 100)
                {
                    result.upvoted = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref c, 0) == 30681240620171363UL && ReadUInt64(ref c, 4) == 26740621010927717UL &&
                    ReadUInt32(ref c, 8) == 6553705U)
                {
                    result.comment_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 30399778527707250UL && ReadUInt64(ref c, 4) == 31244220637315193UL &&
                    ReadUInt64(ref c, 8) == 28429466576683103UL && ReadUInt16(ref c, 12) == 114)
                {
                    result.reply_to_user = ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref c, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref c, 0) == 28429445101977711UL && ReadUInt16(ref c, 4) == 114)
                {
                    result.owner = ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref c, 0) == 27303506540101731UL && ReadUInt64(ref c, 4) == 30962724186423412UL &&
                    ReadUInt64(ref c, 8) == 32651513916817503UL && ReadUInt16(ref c, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Comment value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.comment_id != null)
            {
                writer.WriteUtf16Verbatim(_comment_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.comment_id, nestingLimit);
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

            if (value.score != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_scoreName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.score, nestingLimit);
                writeSeparator = true;
            }

            if (value.edited != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_editedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.edited, nestingLimit);
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

            if (value.owner != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_ownerName);
                ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.owner, nestingLimit);
                writeSeparator = true;
            }

            if (value.reply_to_user != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reply_to_userName);
                ShallowUserUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reply_to_user, nestingLimit);
                writeSeparator = true;
            }

            if (value.link != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_linkName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.link, nestingLimit);
                writeSeparator = true;
            }

            if (value.body_markdown != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_body_markdownName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body_markdown, nestingLimit);
                writeSeparator = true;
            }

            if (value.upvoted != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_upvotedName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.upvoted, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}