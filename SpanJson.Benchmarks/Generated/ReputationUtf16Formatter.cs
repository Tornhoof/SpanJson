using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class ReputationUtf16Formatter : BaseGeneratedFormatter<Reputation, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Reputation, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _user_idName = "\"user_id\":";
        private const string _post_idName = "\"post_id\":";
        private const string _post_typeName = "\"post_type\":";
        private const string _vote_typeName = "\"vote_type\":";
        private const string _titleName = "\"title\":";
        private const string _linkName = "\"link\":";
        private const string _reputation_changeName = "\"reputation_change\":";
        private const string _on_dateName = "\"on_date\":";
        public static readonly ReputationUtf16Formatter Default = new ReputationUtf16Formatter();

        public Reputation Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Reputation();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 17 && ReadUInt64(ref b, 0) == 32933053318103154UL && ReadUInt64(ref b, 8) == 29555370777182324UL &&
                    ReadUInt64(ref b, 16) == 27866430723457135UL && ReadUInt64(ref b, 24) == 28992395053957224UL && ReadUInt16(ref b, 32) == 101)
                {
                    result.reputation_change = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

                if (length == 7 && ReadUInt64(ref b, 0) == 32088581144248437UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt64(ref b, 0) == 30399795707838580UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.title = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 28429470871257206UL && ReadUInt64(ref b, 8) == 31525717090238559UL && ReadUInt16(ref b, 16) == 101)
                {
                    result.vote_type = NullableFormatter<VoteType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 28147905700167791UL && ReadUInt32(ref b, 8) == 7602273U && ReadUInt16(ref b, 12) == 101)
                {
                    result.on_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Reputation value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.user_id != null)
            {
                writer.WriteUtf16Verbatim(_user_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.user_id, nestingLimit);
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

            if (value.vote_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_vote_typeName);
                NullableFormatter<VoteType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.vote_type, nestingLimit);
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

            if (value.reputation_change != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_changeName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation_change, nestingLimit);
                writeSeparator = true;
            }

            if (value.on_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_on_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.on_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}