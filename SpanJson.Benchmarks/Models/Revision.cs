using System;
using System.Collections.Generic;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum RevisionType : byte
    {
        single_user = 1,
        vote_based = 2
    }

    [ProtoContract]
    public class Revision : IGenericEquality<Revision>
    {
        [ProtoMember(1)]
        public string revision_guid { get; set; }

        [ProtoMember(2)]
        public int? revision_number { get; set; }

        [ProtoMember(3)]
        public RevisionType? revision_type { get; set; }

        [ProtoMember(4)]
        public PostType? post_type { get; set; }

        [ProtoMember(5)]
        public int? post_id { get; set; }

        [ProtoMember(6)]
        public string comment { get; set; }

        [ProtoMember(7)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(8)]
        public bool? is_rollback { get; set; }

        [ProtoMember(9)]
        public string last_body { get; set; }

        [ProtoMember(10)]
        public string last_title { get; set; }

        [ProtoMember(11)]
        public List<string> last_tags { get; set; }

        [ProtoMember(12)]
        public string body { get; set; }

        [ProtoMember(13)]
        public string title { get; set; }

        [ProtoMember(14)]
        public List<string> tags { get; set; }

        [ProtoMember(15)]
        public bool? set_community_wiki { get; set; }

        [ProtoMember(16)]
        public ShallowUser user { get; set; }

        public bool Equals(Revision obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                comment.TrueEqualsString(obj.comment) &&
                creation_date.TrueEquals(obj.creation_date) &&
                is_rollback.TrueEquals(obj.is_rollback) &&
                last_body.TrueEqualsString(obj.last_body) &&
                last_tags.TrueEqualsString(obj.last_tags) &&
                last_title.TrueEqualsString(obj.last_title) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                revision_guid.TrueEqualsString(obj.revision_guid) &&
                revision_number.TrueEquals(obj.revision_number) &&
                revision_type.TrueEquals(obj.revision_type) &&
                set_community_wiki.TrueEquals(obj.set_community_wiki) &&
                tags.TrueEqualsString(obj.tags) &&
                title.TrueEqualsString(obj.title) &&
                user.TrueEquals(obj.user);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                comment.TrueEqualsString((string) obj.comment) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                is_rollback.TrueEquals((bool?) obj.is_rollback) &&
                last_body.TrueEqualsString((string) obj.last_body) &&
                last_tags.TrueEqualsString((IEnumerable<string>) obj.last_tags) &&
                last_title.TrueEqualsString((string) obj.last_title) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                revision_guid.TrueEqualsString((string) obj.revision_guid) &&
                revision_number.TrueEquals((int?) obj.revision_number) &&
                revision_type.TrueEquals((RevisionType?) obj.revision_type) &&
                set_community_wiki.TrueEquals((bool?) obj.set_community_wiki) &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title.TrueEqualsString((string) obj.title) &&
                (user == null && obj.user == null || user.EqualsDynamic(obj.user));
        }
    }
}