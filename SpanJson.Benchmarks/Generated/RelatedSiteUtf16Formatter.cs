using System.Runtime.InteropServices;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class RelatedSiteUtf16Formatter : BaseGeneratedFormatter<Info.RelatedSite, char, ExcludeNullsOriginalCaseResolver<char>>,
        IJsonFormatter<Info.RelatedSite, char, ExcludeNullsOriginalCaseResolver<char>>
    {
        private const string _nameName = "\"name\":";
        private const string _site_urlName = "\"site_url\":";
        private const string _relationName = "\"relation\":";
        private const string _api_site_parameterName = "\"api_site_parameter\":";
        public static readonly RelatedSiteUtf16Formatter Default = new RelatedSiteUtf16Formatter();

        public Info.RelatedSite Deserialize(ref JsonReader<char> reader)
        {
            if (reader.ReadUtf16IsNull())
            {
                return null;
            }

            var result = new Info.RelatedSite();
            var count = 0;
            reader.ReadUtf16BeginObjectOrThrow();
            while (!reader.TryReadUtf16IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf16NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(name));
                if (length == 8 && ReadUInt64(ref b, 0) == 28429470870863987UL && ReadUInt64(ref b, 8) == 30399787118690399UL)
                {
                    result.site_url = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 4 && ReadUInt64(ref b, 0) == 28429440805568622UL)
                {
                    result.name = StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
                    continue;
                }

                if (length == 8 && ReadUInt64(ref b, 0) == 27303536604020850UL && ReadUInt64(ref b, 8) == 30962724186423412UL)
                {
                    result.relation = NullableFormatter<Info.RelatedSite.SiteRelation, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Deserialize(ref reader);
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

        public void Serialize(ref JsonWriter<char> writer, Info.RelatedSite value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf16Null();
                return;
            }

            writer.WriteUtf16BeginObject();
            var writeSeparator = false;
            if (value.name != null)
            {
                writer.WriteUtf16Verbatim(_nameName);
                StringUtf16Formatter<ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.name, nestingLimit);
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

            if (value.relation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf16ValueSeparator();
                }

                writer.WriteUtf16Verbatim(_relationName);
                NullableFormatter<Info.RelatedSite.SiteRelation, char, ExcludeNullsOriginalCaseResolver<char>>.Default.Serialize(ref writer, value.relation, nestingLimit);
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

            writer.WriteUtf16EndObject();
        }
    }
}