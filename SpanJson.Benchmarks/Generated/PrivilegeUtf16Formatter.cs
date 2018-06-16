using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class PrivilegeUtf16Formatter : BaseGeneratedFormatter<Privilege, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Privilege, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _short_descriptionName = "\"short_description\":";
        private const string _descriptionName = "\"description\":";
        private const string _reputationName = "\"reputation\":";
        public static readonly PrivilegeUtf16Formatter Default = new PrivilegeUtf16Formatter();

        public Privilege Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Privilege();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 10 && ReadUInt64(ref b, 0) == 32933053318103154UL && ReadUInt64(ref b, 8) == 29555370777182324UL &&
                    ReadUInt32(ref b, 16) == 7209071U)
                {
                    result.reputation = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 32088624093200499UL && ReadUInt64(ref b, 8) == 28429402150731892UL &&
                    ReadUInt64(ref b, 16) == 29555362187378803UL && ReadUInt64(ref b, 24) == 31244173394051184UL && ReadUInt16(ref b, 32) == 110)
                {
                    result.short_description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 27866516622213220UL && ReadUInt64(ref b, 8) == 32651578341654642UL &&
                    ReadUInt32(ref b, 16) == 7274601U && ReadUInt16(ref b, 20) == 110)
                {
                    result.description = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Privilege value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.short_description != null)
            {
                writer.WriteUtf16Verbatim(_short_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.short_description, nestingLimit);
                writeSeparator = true;
            }

            if (value.description != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_descriptionName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.description, nestingLimit);
                writeSeparator = true;
            }

            if (value.reputation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_reputationName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.reputation, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}