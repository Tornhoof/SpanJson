using System.Collections.Generic;
using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Generated
{
    public sealed class SiteUtf16Formatter : BaseGeneratedFormatter<Info.Site, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Info.Site, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _site_typeName = "\"site_type\":";
        private const string _nameName = "\"name\":";
        private const string _logo_urlName = "\"logo_url\":";
        private const string _api_site_parameterName = "\"api_site_parameter\":";
        private const string _site_urlName = "\"site_url\":";
        private const string _audienceName = "\"audience\":";
        private const string _icon_urlName = "\"icon_url\":";
        private const string _aliasesName = "\"aliases\":";
        private const string _site_stateName = "\"site_state\":";
        private const string _stylingName = "\"styling\":";
        private const string _closed_beta_dateName = "\"closed_beta_date\":";
        private const string _open_beta_dateName = "\"open_beta_date\":";
        private const string _launch_dateName = "\"launch_date\":";
        private const string _favicon_urlName = "\"favicon_url\":";
        private const string _related_sitesName = "\"related_sites\":";
        private const string _twitter_accountName = "\"twitter_account\":";
        private const string _markdown_extensionsName = "\"markdown_extensions\":";
        private const string _high_resolution_icon_urlName = "\"high_resolution_icon_url\":";
        public static readonly SiteUtf16Formatter Default = new SiteUtf16Formatter();

        public Info.Site Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Info.Site();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 15 && ReadUInt64(ref b, 0) == 32651548277801076UL && ReadUInt64(ref b, 8) == 26740612420403316UL &&
                    ReadUInt64(ref b, 16) == 31244147623133281UL && ReadUInt32(ref b, 24) == 7209077U && ReadUInt16(ref b, 28) == 116)
                {
                    result.twitter_account = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 16 && ReadUInt64(ref b, 0) == 32370099070173283UL && ReadUInt64(ref b, 8) == 27584955746091109UL &&
                    ReadUInt64(ref b, 16) == 26740539406942309UL && ReadUInt64(ref b, 24) == 28429470870339684UL)
                {
                    result.closed_beta_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 31244164803788908UL && ReadUInt64(ref b, 8) == 30399787118690399UL)
                {
                    result.logo_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 30962749955702892UL && ReadUInt64(ref b, 8) == 28147905699774563UL &&
                    ReadUInt32(ref b, 16) == 7602273U && ReadUInt16(ref b, 20) == 101)
                {
                    result.launch_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 30962724186030185UL && ReadUInt64(ref b, 8) == 30399787118690399UL)
                {
                    result.icon_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 14 && ReadUInt64(ref b, 0) == 30962681237209199UL && ReadUInt64(ref b, 8) == 32651531096555615UL &&
                    ReadUInt64(ref b, 16) == 27303502243889249UL && ReadUInt32(ref b, 24) == 6619252U)
                {
                    result.open_beta_date = NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 30399817183395955UL && ReadUInt32(ref b, 8) == 7209065U && ReadUInt16(ref b, 12) == 103)
                {
                    result.styling = StylingUtf16Formatter.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 19 && ReadUInt64(ref b, 0) == 30118312140669037UL && ReadUInt64(ref b, 8) == 30962758546554980UL &&
                    ReadUInt64(ref b, 16) == 32651612701130847UL && ReadUInt64(ref b, 24) == 29555366483066981UL && ReadUInt32(ref b, 32) == 7209071U &&
                    ReadUInt16(ref b, 36) == 115)
                {
                    result.markdown_extensions = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 11 && ReadUInt64(ref b, 0) == 29555379367116902UL && ReadUInt64(ref b, 8) == 26740595241189475UL &&
                    ReadUInt32(ref b, 16) == 7471221U && ReadUInt16(ref b, 20) == 108)
                {
                    result.favicon_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 29555302059016289UL && ReadUInt64(ref b, 8) == 28429397856747621UL)
                {
                    result.audience = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 24 && ReadUInt64(ref b, 0) == 29273839966421096UL && ReadUInt64(ref b, 8) == 32370056120893535UL &&
                    ReadUInt64(ref b, 16) == 32651599816687727UL && ReadUInt64(ref b, 24) == 26740595241189481UL &&
                    ReadUInt64(ref b, 32) == 30962724186030185UL && ReadUInt64(ref b, 40) == 30399787118690399UL)
                {
                    result.high_resolution_icon_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length >= 4 && ReadUInt64(ref b, 0) == 28429470870863987UL)
                {
                    if (length == 9 && ReadUInt64(ref b, 8) == 31525717090238559UL && ReadUInt16(ref b, 16) == 101)
                    {
                        result.site_type = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 8 && ReadUInt64(ref b, 8) == 30399787118690399UL)
                    {
                        result.site_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    if (length == 10 && ReadUInt64(ref b, 8) == 27303570964676703UL && ReadUInt32(ref b, 16) == 6619252U)
                    {
                        result.site_state = NullableFormatter<Info.Site.SiteState, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                        continue;
                    }

                    reader.SkipNextUtf16Segment();
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429440805568622UL)
                {
                    result.name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 13 && ReadUInt64(ref b, 0) == 27303536604020850UL && ReadUInt64(ref b, 8) == 26740552290861172UL &&
                    ReadUInt64(ref b, 16) == 28429470870863987UL && ReadUInt16(ref b, 24) == 115)
                {
                    result.related_sites =
                        ListFormatter<List<Info.RelatedSite>, Info.RelatedSite, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 7 && ReadUInt64(ref b, 0) == 27303523719577697UL && ReadUInt32(ref b, 8) == 6619251U && ReadUInt16(ref b, 12) == 115)
                {
                    result.aliases = StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 18 && ReadUInt64(ref b, 0) == 26740573766418529UL && ReadUInt64(ref b, 8) == 28429470870863987UL &&
                    ReadUInt64(ref b, 16) == 32088563964182623UL && ReadUInt64(ref b, 24) == 32651531097276513UL && ReadUInt32(ref b, 32) == 7471205U)
                {
                    result.api_site_parameter = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf16Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<char> writer, Info.Site value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.site_type != null)
            {
                writer.WriteUtf16Verbatim(_site_typeName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site_type, nestingLimit);
                writeSeparator = true;
            }

            if (value.name != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.name, nestingLimit);
                writeSeparator = true;
            }

            if (value.logo_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_logo_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.logo_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.api_site_parameter != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_api_site_parameterName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.api_site_parameter, nestingLimit);
                writeSeparator = true;
            }

            if (value.site_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_site_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.audience != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_audienceName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.audience, nestingLimit);
                writeSeparator = true;
            }

            if (value.icon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_icon_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.icon_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.aliases != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_aliasesName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.aliases, nestingLimit);
                writeSeparator = true;
            }

            if (value.site_state != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_site_stateName);
                NullableFormatter<Info.Site.SiteState, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.site_state, nestingLimit);
                writeSeparator = true;
            }

            if (value.styling != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_stylingName);
                StylingUtf16Formatter.Default.Serialize(ref writer, value.styling, nestingLimit);
                writeSeparator = true;
            }

            if (value.closed_beta_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_closed_beta_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.closed_beta_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.open_beta_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_open_beta_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.open_beta_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.launch_date != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_launch_dateName);
                NullableDateTimeUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.launch_date, nestingLimit);
                writeSeparator = true;
            }

            if (value.favicon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_favicon_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.favicon_url, nestingLimit);
                writeSeparator = true;
            }

            if (value.related_sites != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_related_sitesName);
                ListFormatter<List<Info.RelatedSite>, Info.RelatedSite, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.related_sites,
                    nestingLimit);
                writeSeparator = true;
            }

            if (value.twitter_account != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_twitter_accountName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.twitter_account, nestingLimit);
                writeSeparator = true;
            }

            if (value.markdown_extensions != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_markdown_extensionsName);
                StringUtf16ListFormatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.markdown_extensions, nestingLimit);
                writeSeparator = true;
            }

            if (value.high_resolution_icon_url != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_high_resolution_icon_urlName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.high_resolution_icon_url, nestingLimit);
                writeSeparator = true;
            }

            writer.WriteUtf16EndObject();
        }
    }
}