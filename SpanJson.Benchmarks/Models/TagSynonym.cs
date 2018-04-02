using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class TagSynonym : IGenericEquality<TagSynonym>
    {
        [ProtoMember(1)]
        public string from_tag { get; set; }

        [ProtoMember(2)]
        public string to_tag { get; set; }

        [ProtoMember(3)]
        public int? applied_count { get; set; }

        [ProtoMember(4)]
        public DateTime? last_applied_date { get; set; }

        [ProtoMember(5)]
        public DateTime? creation_date { get; set; }

        public bool Equals(TagSynonym obj)
        {
            return
                applied_count.TrueEquals(obj.applied_count) &&
                creation_date.TrueEquals(obj.creation_date) &&
                from_tag.TrueEqualsString(obj.from_tag) &&
                last_applied_date.TrueEquals(obj.last_applied_date) &&
                to_tag.TrueEqualsString(obj.to_tag);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                applied_count.TrueEquals((int?) obj.applied_count) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                from_tag.TrueEqualsString((string) obj.from_tag) &&
                last_applied_date.TrueEquals((DateTime?) obj.last_applied_date) &&
                to_tag.TrueEqualsString((string) obj.to_tag);
        }
    }
}