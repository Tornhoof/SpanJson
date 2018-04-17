using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class Tag : IGenericEquality<Tag>
    {
        [ProtoMember(1)]
        public string name { get; set; }

        [ProtoMember(2)]
        public int? count { get; set; }

        [ProtoMember(3)]
        public bool? is_required { get; set; }

        [ProtoMember(4)]
        public bool? is_moderator_only { get; set; }

        [ProtoMember(5)]
        public int? user_id { get; set; }

        [ProtoMember(6)]
        public bool? has_synonyms { get; set; }

        [ProtoMember(7)]
        public DateTime? last_activity_date { get; set; }

        [ProtoMember(8)]
        public List<string> synonyms { get; set; }

        public bool Equals(Tag obj)
        {
            return
                count.TrueEquals(obj.count) &&
                has_synonyms.TrueEquals(obj.has_synonyms) &&
                is_moderator_only.TrueEquals(obj.is_moderator_only) &&
                is_required.TrueEquals(obj.is_required) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                name.TrueEqualsString(obj.name) &&
                synonyms.TrueEqualsString(obj.synonyms) &&
                user_id.TrueEquals(obj.user_id);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                count.TrueEquals((int?) obj.count) &&
                has_synonyms.TrueEquals((bool?) obj.has_synonyms) &&
                is_moderator_only.TrueEquals((bool?) obj.is_moderator_only) &&
                is_required.TrueEquals((bool?) obj.is_required) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                name.TrueEqualsString((string) obj.name) &&
                synonyms.TrueEqualsString((IEnumerable<string>) obj.synonyms) &&
                user_id.TrueEquals((int?) obj.user_id);
        }
    }
}