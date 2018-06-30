using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class Revision : IGenericEquality<Revision>
    {
        public string revision_guid { get; set; }


        public int? revision_number { get; set; }


        public RevisionType? revision_type { get; set; }


        public PostType? post_type { get; set; }


        public int? post_id { get; set; }


        public string comment { get; set; }


        public DateTime? creation_date { get; set; }


        public bool? is_rollback { get; set; }


        public string last_body { get; set; }


        public string last_title { get; set; }


        public List<string> last_tags { get; set; }


        public string body { get; set; }


        public string title { get; set; }


        public List<string> tags { get; set; }


        public bool? set_community_wiki { get; set; }


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