using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class PrivilegeUtf8Formatter : BaseGeneratedFormatter<Privilege, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Privilege, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly PrivilegeUtf8Formatter Default = new PrivilegeUtf8Formatter();
        private readonly byte[] _descriptionName = Encoding.UTF8.GetBytes("\"description\":");
        private readonly byte[] _reputationName = Encoding.UTF8.GetBytes("\"reputation\":");
        private readonly byte[] _short_descriptionName = Encoding.UTF8.GetBytes("\"short_description\":");

        public Privilege Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Privilege();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 11 && ReadUInt64(ref b, 0) == 8390322045806929252UL && ReadUInt16(ref b, 8) == 28521 && ReadByte(ref b, 10) == 110)
                {
                    result.description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7598805624095270258UL && ReadUInt16(ref b, 8) == 28271)
                {
                    result.reputation = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7306069449242536051UL && ReadUInt64(ref b, 8) == 8028075836918883187UL &&
                    ReadByte(ref b, 16) == 110)
                {
                    result.short_description = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Privilege value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.short_description != null)
            {
                writer.WriteUtf8Verbatim(_short_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.short_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_descriptionName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_reputationName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.reputation, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}