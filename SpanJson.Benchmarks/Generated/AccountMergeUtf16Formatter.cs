using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class AccountMergeUtf16Formatter : BaseGeneratedFormatter<AccountMerge, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<AccountMerge, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _old_account_idName = "\"old_account_id\":";
        private const string _new_account_idName = "\"new_account_id\":";
        private const string _merge_dateName = "\"merge_date\":";
        public static readonly AccountMergeUtf16Formatter Default = new AccountMergeUtf16Formatter();

        public AccountMerge Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new AccountMerge();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 10 && ReadUInt64(ref b, 0) == 28992412234088557UL && ReadUInt64(ref b, 8) == 27303502243889253UL &&
                    ReadUInt32(ref b, 16) == 6619252U)
                {
                    result.merge_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 26740633895239790UL && ReadUInt64(ref b, 8) == 31244147623133281UL &&
                    ReadUInt64(ref b, 16) == 26740621010927733UL && ReadUInt32(ref b, 24) == 6553705U)
                {
                    result.new_account_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 26740552291319919UL && ReadUInt64(ref b, 8) == 31244147623133281UL &&
                    ReadUInt64(ref b, 16) == 26740621010927733UL && ReadUInt32(ref b, 24) == 6553705U)
                {
                    result.old_account_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, AccountMerge value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.old_account_id != null)
            {
                writer.WriteUtf16Verbatim(_old_account_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.old_account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.new_account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_new_account_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.new_account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.merge_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_merge_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.merge_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}