using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class FlagOption : IGenericEquality<FlagOption>
    {
        [ProtoMember(1)] public int? option_id { get; set; }

        [ProtoMember(2)] public bool? requires_comment { get; set; }

        [ProtoMember(3)] public bool? requires_site { get; set; }

        [ProtoMember(4)] public bool? requires_question_id { get; set; }

        [ProtoMember(5)] public string title { get; set; }

        [ProtoMember(6)] public string description { get; set; }

        [ProtoMember(7)] public List<FlagOption> sub_options { get; set; }

        [ProtoMember(8)] public bool? has_flagged { get; set; }

        [ProtoMember(9)] public int? count { get; set; }

        public bool Equals(FlagOption obj)
        {
            return
                count.TrueEquals(obj.count) &&
                description.TrueEqualsString(obj.description) &&
                has_flagged.TrueEquals(obj.has_flagged) &&
                option_id.TrueEquals(obj.option_id) &&
                requires_comment.TrueEquals(obj.requires_comment) &&
                requires_question_id.TrueEquals(obj.requires_question_id) &&
                requires_site.TrueEquals(obj.requires_site) &&
                sub_options.TrueEqualsList(obj.sub_options) &&
                title.TrueEqualsString(obj.title);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                count.TrueEquals((int?) obj.count) &&
                description.TrueEqualsString((string) obj.description) &&
                has_flagged.TrueEquals((bool?) obj.has_flagged) &&
                option_id.TrueEquals((int?) obj.option_id) &&
                requires_comment.TrueEquals((bool?) obj.requires_comment) &&
                requires_question_id.TrueEquals((bool?) obj.requires_question_id) &&
                requires_site.TrueEquals((bool?) obj.requires_site) &&
                sub_options != null && obj.sub_options != null && sub_options.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.sub_options) &&
                title.TrueEqualsString((string) obj.title);
        }
    }
}