using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class AccountMergeUtf8Formatter : BaseGeneratedFormatter<AccountMerge, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<AccountMerge, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly AccountMergeUtf8Formatter Default = new AccountMergeUtf8Formatter();
        private readonly byte[] _merge_dateName = Encoding.UTF8.GetBytes("\"merge_date\":");
        private readonly byte[] _new_account_idName = Encoding.UTF8.GetBytes("\"new_account_id\":");
        private readonly byte[] _old_account_idName = Encoding.UTF8.GetBytes("\"old_account_id\":");

        public AccountMerge Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new AccountMerge();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 14 && ReadUInt64(ref b, 0) == 8026368230768993646UL && ReadUInt32(ref b, 8) == 1601465973U && ReadUInt16(ref b, 12) == 25705)
                {
                    result.new_account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 8026368230767750255UL && ReadUInt32(ref b, 8) == 1601465973U && ReadUInt16(ref b, 12) == 25705)
                {
                    result.old_account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7017839008481961325UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.merge_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, AccountMerge value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.old_account_id != null)
            {
                writer.WriteUtf8Verbatim(_old_account_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.old_account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.new_account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_new_account_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.new_account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.merge_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_merge_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.merge_date, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}