using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class NotificationUtf16Formatter : BaseGeneratedFormatter<Notification, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Notification, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _notification_typeName = "\"notification_type\":";
        private const string _siteName = "\"site\":";
        private const string _creation_dateName = "\"creation_date\":";
        private const string _bodyName = "\"body\":";
        private const string _post_idName = "\"post_id\":";
        private const string _is_unreadName = "\"is_unread\":";
        public static readonly NotificationUtf16Formatter Default = new NotificationUtf16Formatter();

        public Notification Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Notification();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 4 && ReadUInt64(ref b, 0) == 34058901685993570UL)
                {
                    result.body = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 32932980304576617UL && ReadUInt64(ref b, 8) == 27303506540101742UL && ReadUInt16(ref b, 16) == 100)
                {
                    result.is_unread = NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 32651591226949744UL && ReadUInt32(ref b, 8) == 6881375U && ReadUInt16(ref b, 12) == 100)
                {
                    result.post_id = NullableInt32Utf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 29555370778099822UL && ReadUInt64(ref b, 8) == 27303497949577318UL &&
                    ReadUInt64(ref b, 16) == 30962724186423412UL && ReadUInt64(ref b, 24) == 31525717090238559UL && ReadUInt16(ref b, 32) == 101)
                {
                    result.notification_type =
                        NullableFormatter<NotificationType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429470870863987UL)
                {
                    result.site = SiteUtf16Formatter.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<char> writer, Notification value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.notification_type != null)
            {
                writer.WriteUtf16Verbatim(_notification_typeName);
                NullableFormatter<NotificationType, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.notification_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_siteName);
                SiteUtf16Formatter.Default.Serialize(ref writer, value.site, nestingLimit);
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

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_bodyName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.body, nestingLimit);
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

            if (value.is_unread != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_is_unreadName);
                NullableBooleanUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.is_unread, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}