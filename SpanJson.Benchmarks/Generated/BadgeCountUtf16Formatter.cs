using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class BadgeCountUtf16Formatter<TResolver> : BaseGeneratedFormatter<User.BadgeCount, char, TResolver>,
        IJsonFormatter<User.BadgeCount, char, TResolver> where TResolver : class, IJsonFormatterResolver<char, TResolver>, new()
    {
        private static readonly char[] _goldName = "\"gold\":".ToCharArray();
        private static readonly char[] _silverName = "\"silver\":".ToCharArray();
        private static readonly char[] _bronzeName = "\"bronze\":".ToCharArray();
        public static readonly BadgeCountUtf16Formatter<TResolver> Default = new BadgeCountUtf16Formatter<TResolver>();

        public User.BadgeCount Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new User.BadgeCount();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var c = ref MemoryMarshal.GetReference(name);
                if (length == 6 && ReadUInt64(ref c, 0) == 33214511115206771UL && ReadUInt32(ref c, 4) == 7471205U)
                {
                    result.silver = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 6 && ReadUInt64(ref c, 0) == 30962724187013218UL && ReadUInt32(ref c, 4) == 6619258U)
                {
                    result.bronze = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref c, 0) == 28147961534808167UL)
                {
                    result.gold = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, User.BadgeCount value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.gold != null)
            {
                writer.WriteUtf16Verbatim(_goldName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.gold, nestingLimit);
                writeSeparator = true;
            }

            if (value.silver != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_silverName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.silver, nestingLimit);
                writeSeparator = true;
            }

            if (value.bronze != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bronzeName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.bronze, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}