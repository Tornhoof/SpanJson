using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class BadgeCountUtf8Formatter : BaseGeneratedFormatter<User.BadgeCount, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<User.BadgeCount, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly BadgeCountUtf8Formatter Default = new BadgeCountUtf8Formatter();
        private readonly byte[] _bronzeName = Encoding.UTF8.GetBytes("\"bronze\":");
        private readonly byte[] _goldName = Encoding.UTF8.GetBytes("\"gold\":");
        private readonly byte[] _silverName = Encoding.UTF8.GetBytes("\"silver\":");

        public User.BadgeCount Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new User.BadgeCount();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 6 && ReadUInt32(ref b, 0) == 1986816371U && ReadUInt16(ref b, 4) == 29285)
                {
                    result.silver = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt32(ref b, 0) == 1852797538U && ReadUInt16(ref b, 4) == 25978)
                {
                    result.bronze = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1684828007U)
                {
                    result.gold = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, User.BadgeCount value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.gold != null)
            {
                writer.WriteUtf8Verbatim(_goldName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.gold, nestingLimit);
                writeSeparator = true;
            }

            if (value.silver != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_silverName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.silver, nestingLimit);
                writeSeparator = true;
            }

            if (value.bronze != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bronzeName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.bronze, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}