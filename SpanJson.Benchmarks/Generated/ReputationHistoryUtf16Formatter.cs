using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class ReputationHistoryUtf16Formatter : BaseGeneratedFormatter<ReputationHistory, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<ReputationHistory, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _user_idName = "\"user_id\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _post_idName = "\"post_id\":";
        private const string _reputation_changeName = "\"reputation_change\":";
        private const string _reputation_history_typeName = "\"reputation_history_type\":";
        public static readonly ReputationHistoryUtf16Formatter Default = new ReputationHistoryUtf16Formatter();

        public ReputationHistory Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new ReputationHistory();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 32933053318103154UL)
                {
                    if (length >= 8 && ReadUInt64(ref b, 8) == 29555370777182324UL)
                    {
                        if (length == 23 && ReadUInt64(ref b, 16) == 29273805607010415UL && ReadUInt64(ref b, 24) == 31244220638625897UL &&
                            ReadUInt64(ref b, 32) == 32651505328259186UL && ReadUInt32(ref b, 40) == 7340153U && ReadUInt16(ref b, 44) == 101)
                        {
                            result.reputation_history_type =
                                NullableFormatter<ReputationHistory.ReputationHistoryType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        if (length == 17 && ReadUInt64(ref b, 16) == 27866430723457135UL && ReadUInt64(ref b, 24) == 28992395053957224UL &&
                            ReadUInt16(ref b, 32) == 101)
                        {
                            result.reputation_change = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                            continue;
                        }

                        reader.SkipNextUtf16Segment();
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32651591226949744UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.post_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32088581144248437UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.user_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<char> writer, ReputationHistory value, int nestingLimit)
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

            if (value.reputation_history_type != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputation_history_typeName);
                NullableFormatter<ReputationHistory.ReputationHistoryType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer,
                    value.reputation_history_type, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}