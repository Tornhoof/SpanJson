using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class SiteUtf8Formatter : BaseGeneratedFormatter<Info.Site, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Info.Site, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly SiteUtf8Formatter Default = new SiteUtf8Formatter();
        private readonly byte[] _aliasesName = Encoding.UTF8.GetBytes("\"aliases\":");
        private readonly byte[] _api_site_parameterName = Encoding.UTF8.GetBytes("\"api_site_parameter\":");
        private readonly byte[] _audienceName = Encoding.UTF8.GetBytes("\"audience\":");
        private readonly byte[] _closed_beta_dateName = Encoding.UTF8.GetBytes("\"closed_beta_date\":");
        private readonly byte[] _favicon_urlName = Encoding.UTF8.GetBytes("\"favicon_url\":");
        private readonly byte[] _high_resolution_icon_urlName = Encoding.UTF8.GetBytes("\"high_resolution_icon_url\":");
        private readonly byte[] _icon_urlName = Encoding.UTF8.GetBytes("\"icon_url\":");
        private readonly byte[] _launch_dateName = Encoding.UTF8.GetBytes("\"launch_date\":");
        private readonly byte[] _logo_urlName = Encoding.UTF8.GetBytes("\"logo_url\":");
        private readonly byte[] _markdown_extensionsName = Encoding.UTF8.GetBytes("\"markdown_extensions\":");
        private readonly byte[] _nameName = Encoding.UTF8.GetBytes("\"name\":");
        private readonly byte[] _open_beta_dateName = Encoding.UTF8.GetBytes("\"open_beta_date\":");
        private readonly byte[] _related_sitesName = Encoding.UTF8.GetBytes("\"related_sites\":");
        private readonly byte[] _site_stateName = Encoding.UTF8.GetBytes("\"site_state\":");
        private readonly byte[] _site_typeName = Encoding.UTF8.GetBytes("\"site_type\":");
        private readonly byte[] _site_urlName = Encoding.UTF8.GetBytes("\"site_url\":");
        private readonly byte[] _stylingName = Encoding.UTF8.GetBytes("\"styling\":");
        private readonly byte[] _twitter_accountName = Encoding.UTF8.GetBytes("\"twitter_account\":");

        public Info.Site Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Info.Site();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 14 && ReadUInt64(ref b, 0) == 8387218043060973679UL && ReadUInt32(ref b, 8) == 1633967969U && ReadUInt16(ref b, 12) == 25972)
                {
                    result.open_beta_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 24 && ReadUInt64(ref b, 0) == 8315178041108556136UL && ReadUInt64(ref b, 8) == 6876556179757427823UL &&
                    ReadUInt64(ref b, 16) == 7814437356176368489UL)
                {
                    result.high_resolution_icon_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 9 && ReadUInt64(ref b, 0) == 8104636957502761331UL && ReadByte(ref b, 8) == 101)
                {
                    result.site_type = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 7959953343490711917UL && ReadUInt64(ref b, 8) == 7598538378328958303UL &&
                    ReadUInt16(ref b, 16) == 28271 && ReadByte(ref b, 18) == 115)
                {
                    result.markdown_extensions = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7814437356192624492UL)
                {
                    result.logo_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7814437356176368489UL)
                {
                    result.icon_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7814437356025702771UL)
                {
                    result.site_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 7310584039372058721UL && ReadUInt64(ref b, 8) == 8387230146345660511UL &&
                    ReadUInt16(ref b, 16) == 29285)
                {
                    result.api_site_parameter = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 7305804402364020065UL)
                {
                    result.audience = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 7232614302844543340UL && ReadUInt16(ref b, 8) == 29793 && ReadByte(ref b, 10) == 101)
                {
                    result.launch_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 7088494725395606627UL && ReadUInt64(ref b, 8) == 7310575178854003813UL)
                {
                    result.closed_beta_date = NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 10 && ReadUInt64(ref b, 0) == 7022364572538661235UL && ReadUInt16(ref b, 8) == 25972)
                {
                    result.site_state = NullableFormatter<Info.Site.SiteState, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 15 && ReadUInt64(ref b, 0) == 6877671131791849332UL && ReadUInt32(ref b, 8) == 1868784481U && ReadUInt16(ref b, 12) == 28277 &&
                    ReadByte(ref b, 14) == 116)
                {
                    result.twitter_account = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 6876556153803137382UL && ReadUInt16(ref b, 8) == 29301 && ReadByte(ref b, 10) == 108)
                {
                    result.favicon_url = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 6873730481799325042UL && ReadUInt32(ref b, 8) == 1702127987U && ReadByte(ref b, 12) == 115)
                {
                    result.related_sites =
                        ListFormatter<List<Info.RelatedSite>, Info.RelatedSite, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1819898995U && ReadUInt16(ref b, 4) == 28265 && ReadByte(ref b, 6) == 103)
                {
                    result.styling = StylingUtf8Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt32(ref b, 0) == 1701667182U)
                {
                    result.name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt32(ref b, 0) == 1634298977U && ReadUInt16(ref b, 4) == 25971 && ReadByte(ref b, 6) == 115)
                {
                    result.aliases = StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Info.Site value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.site_type != null)
            {
                writer.WriteUtf8Verbatim(_site_typeName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.name, nestingLimit);
                writeSeparator = true;
            }

            if (value.logo_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_logo_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.logo_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.api_site_parameter != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_api_site_parameterName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.api_site_parameter, nestingLimit);
                writeSeparator = true;
            }

            if (value.site_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_site_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.audience != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_audienceName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.audience, nestingLimit);
                writeSeparator = true;
            }

            if (value.icon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_icon_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.icon_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.aliases != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_aliasesName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.aliases, nestingLimit);
                writeSeparator = true;
            }

            if (value.site_state != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_site_stateName);
                NullableFormatter<Info.Site.SiteState, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.site_state, nestingLimit);
                writeSeparator = true;
            }

            if (value.styling != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_stylingName);
                StylingUtf8Formatter.Default.Serialize(ref writer, value.styling, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_beta_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_closed_beta_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.closed_beta_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.open_beta_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_open_beta_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.open_beta_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.launch_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_launch_dateName);
                NullableDateTimeUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.launch_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.favicon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_favicon_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.favicon_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.related_sites != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_related_sitesName);
                ListFormatter<List<Info.RelatedSite>, Info.RelatedSite, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.related_sites,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.twitter_account != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_twitter_accountName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.twitter_account, nestingLimit);
                writeSeparator = true;
            }

            if (value.markdown_extensions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_markdown_extensionsName);
                StringUtf8ListFormatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.markdown_extensions, nestingLimit);
                writeSeparator = true;
            }

            if (value.high_resolution_icon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_high_resolution_icon_urlName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.high_resolution_icon_url, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf8EndObject();
        }
    }
}