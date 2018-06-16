using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class AccessTokenUtf8Formatter : BaseGeneratedFormatter<AccessToken, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<AccessToken, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly AccessTokenUtf8Formatter Default = new AccessTokenUtf8Formatter();
        private readonly byte[] _access_tokenName = Encoding.UTF8.GetBytes("\"access_token\":");
        private readonly byte[] _account_idName = Encoding.UTF8.GetBytes("\"account_id\":");
        private readonly byte[] _expires_on_dateName = Encoding.UTF8.GetBytes("\"expires_on_date\":");
        private readonly byte[] _scopeName = Encoding.UTF8.GetBytes("\"scope\":");

        public AccessToken Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new AccessToken();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 12 && ReadUInt64(ref b, 0) == 8385547970646598497UL && ReadUInt32(ref b, 8) == 1852140399U)
                {
                    result.access_token = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 6878243981560603489UL && ReadUInt16(ref b, 8) == 25705)
                {
                    result.account_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 6877952597994535013UL && ReadUInt32(ref b, 8) == 1683975791U && ReadUInt16(ref b, 12) == 29793 &&
                    ReadByte(ref b, 14) == 101)
                {
                    result.expires_on_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 5 && ReadUInt32(ref b, 0) == 1886348147U && ReadByte(ref b, 4) == 101)
                {
                    result.scope = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, AccessToken value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.access_token != null)
            {
                writer.WriteUtf8Verbatim(_access_tokenName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.access_token, nestingLimit);
                writeSeparator = true;
            }

            if (value.expires_on_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_expires_on_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.expires_on_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.account_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_account_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.account_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.scope != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_scopeName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.scope, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}