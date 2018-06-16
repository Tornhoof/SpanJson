using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class NotificationUtf8Formatter : BaseGeneratedFormatter<Notification, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Notification, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly NotificationUtf8Formatter Default = new NotificationUtf8Formatter();
        private readonly byte[] _bodyName = Encoding.UTF8.GetBytes("\"body\":");
        private readonly byte[] _creation_dateName = Encoding.UTF8.GetBytes("\"creation_date\":");
        private readonly byte[] _is_unreadName = Encoding.UTF8.GetBytes("\"is_unread\":");
        private readonly byte[] _notification_typeName = Encoding.UTF8.GetBytes("\"notification_type\":");
        private readonly byte[] _post_idName = Encoding.UTF8.GetBytes("\"post_id\":");
        private readonly byte[] _siteName = Encoding.UTF8.GetBytes("\"site\":");

        public Notification Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Notification();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 13 && ReadUInt64(ref b, 0) == 7957695015158116963UL && ReadUInt32(ref b, 8) == 1952539743U && ReadByte(ref b, 12) == 101)
                {
                    result.creation_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 7018141413067944809UL && ReadByte(ref b, 8) == 100)
                {
                    result.is_unread = NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 17 && ReadUInt64(ref b, 0) == 7017568532950183790UL && ReadUInt64(ref b, 8) == 8104636957653428596UL &&
                    ReadByte(ref b, 16) == 101)
                {
                    result.notification_type =
                        NullableFormatter<NotificationType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 2036625250U)
                {
                    result.body = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1953722224U && ReadUInt16(ref b, 4) == 26975 && ReadByte(ref b, 6) == 100)
                {
                    result.post_id = NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1702127987U)
                {
                    result.site = SiteUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Notification value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.notification_type != null)
            {
                writer.WriteUtf8Verbatim(_notification_typeName);
                NullableFormatter<NotificationType, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.notification_type,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.site != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_siteName);
                SiteUtf8Formatter.Default.Serialize(ref writer, value.site, nestingLimit);
                writeSeparator = true;
            }

            if (value.creation_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_creation_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.creation_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.body != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_bodyName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.body, nestingLimit);
                writeSeparator = true;
            }

            if (value.post_id != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_post_idName);
                NullableInt32Utf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.post_id, nestingLimit);
                writeSeparator = true;
            }

            if (value.is_unread != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_is_unreadName);
                NullableBooleanUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.is_unread, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}