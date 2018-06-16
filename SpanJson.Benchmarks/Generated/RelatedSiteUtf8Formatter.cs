using System.Runtime.InteropServices;
using System.Text;
using SpanJson.Benchmarks.Models;
using SpanJson.Codegen;
using SpanJson.Formatters;
using SpanJson.Resolvers;

namespace SpanJson.Benchmarks.Generated
{
    public sealed class RelatedSiteUtf8Formatter : BaseGeneratedFormatter<Info.RelatedSite, byte, ExcludeNullsOriginalCaseResolver<byte>>,
        IJsonFormatter<Info.RelatedSite, byte, ExcludeNullsOriginalCaseResolver<byte>>
    {
        public static readonly RelatedSiteUtf8Formatter Default = new RelatedSiteUtf8Formatter();
        private readonly byte[] _api_site_parameterName = Encoding.UTF8.GetBytes("\"api_site_parameter\":");
        private readonly byte[] _nameName = Encoding.UTF8.GetBytes("\"name\":");
        private readonly byte[] _relationName = Encoding.UTF8.GetBytes("\"relation\":");
        private readonly byte[] _site_urlName = Encoding.UTF8.GetBytes("\"site_url\":");

        public Info.RelatedSite Deserialize(ref JsonReader<byte> reader)
        {
            if (reader.ReadUtf8IsNull())
            {
                return null;
            }

            var result = new Info.RelatedSite();
            var count = 0;
            reader.ReadUtf8BeginObjectOrThrow();
            while (!reader.TryReadUtf8IsEndObjectOrValueSeparator(ref count))
            {
                var name = reader.ReadUtf8NameSpan();
                var length = name.Length;
                ref var b = ref MemoryMarshal.GetReference(name);
                if (length == 8 && ReadUInt64(ref b, 0) == 7957695015158572402UL)
                {
                    result.relation = NullableFormatter<Info.RelatedSite.SiteRelation, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
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

                if (length == 4 && ReadUInt32(ref b, 0) == 1701667182U)
                {
                    result.name = StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Deserialize(ref reader);
                    continue;
                }

                reader.SkipNextUtf8Segment();
            }

            return result;
        }

        public void Serialize(ref JsonWriter<byte> writer, Info.RelatedSite value, int nestingLimit)
        {
            if (value == null)
            {
                writer.WriteUtf8Null();
                return;
            }

            writer.WriteUtf8BeginObject();
            var writeSeparator = false;
            if (value.name != null)
            {
                writer.WriteUtf8Verbatim(_nameName);
                StringUtf8Formatter<ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.name, nestingLimit);
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

            if (value.relation != null)
            {
                if (writeSeparator)
                {
                    writer.WriteUtf8ValueSeparator();
                }

                writer.WriteUtf8Verbatim(_relationName);
                NullableFormatter<Info.RelatedSite.SiteRelation, byte, ExcludeNullsOriginalCaseResolver<byte>>.Default.Serialize(ref writer, value.relation, nestingLimit);
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

            writer.WriteUtf8EndObject();
        }
    }
}