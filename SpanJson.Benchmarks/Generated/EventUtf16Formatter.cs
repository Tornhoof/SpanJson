using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class EventUtf16Formatter : BaseGeneratedFormatter<Event, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Event, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _event_typeName = "\"event_type\":";
        private const string _event_idName = "\"event_id\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _linkName = "\"link\":";
        private const string _excerptName = "\"excerpt\":";
        public static readonly EventUtf16Formatter Default = new EventUtf16Formatter();

        public Event Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Event();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length >= 4 && ReadUInt64(ref b, 0) == 30962681237602405UL)
                {
                    if (length == 10 && ReadUInt64(ref b, 8) == 34058970404421748UL && ReadUInt32(ref b, 16) == 6619248U)
                    {
                        result.event_type = NullableFormatter<EventType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 8 && ReadUInt64(ref b, 8) == 28147948648857716UL)
                    {
                        result.event_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 30118294961324140UL)
                {
                    result.link = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 28429397857402981UL && ReadUInt32(ref b, 8) == 7340146U && ReadUInt16(ref b, 12) == 116)
                {
                    result.excerpt = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<char> writer, Event value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.event_type != null)
            {
                writer.WriteUtf16Verbatim(_event_typeName);
                NullableFormatter<EventType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.event_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.event_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_event_idName);
                NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.event_id, nestingLimit);
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

            if (value.excerpt != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_excerptName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.excerpt, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}