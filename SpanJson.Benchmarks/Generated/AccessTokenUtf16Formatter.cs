using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class AccessTokenUtf16Formatter : BaseGeneratedFormatter<AccessToken, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<AccessToken, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _access_tokenName = "\"access_token\":";
        private const string _expires_on_dateName = "\"expires_on_date\":";
        private const string _account_idName = "\"account_id\":";
        private const string _scopeName = "\"scope\":";
        public static readonly AccessTokenUtf16Formatter Default = new AccessTokenUtf16Formatter();

        public AccessToken Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new AccessToken();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 5 && ReadUInt64(ref b, 0) == 31525674139451507UL && ReadUInt16(ref b, 8) == 101)
                {
                    result.scope = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 31244147623133281UL && ReadUInt64(ref b, 8) == 26740621010927733UL &&
                    ReadUInt32(ref b, 16) == 6553705U)
                {
                    result.account_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 29555353598820453UL && ReadUInt64(ref b, 8) == 26740616715370610UL &&
                    ReadUInt64(ref b, 16) == 28147905700167791UL && ReadUInt32(ref b, 24) == 7602273U && ReadUInt16(ref b, 28) == 101)
                {
                    result.expires_on_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 12 && ReadUInt64(ref b, 0) == 28429397856026721UL && ReadUInt64(ref b, 8) == 32651505327865971UL &&
                    ReadUInt64(ref b, 16) == 30962681236881519UL)
                {
                    result.access_token = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, AccessToken value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.access_token != null)
            {
                writer.WriteUtf16Verbatim(_access_tokenName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.access_token, nestingLimit);
                writeSeparator = true;
            }

            if (value.expires_on_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_expires_on_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.expires_on_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_account_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.scope != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_scopeName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.scope, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}