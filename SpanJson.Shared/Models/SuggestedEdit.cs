using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class SuggestedEdit : IGenericEquality<SuggestedEdit>
    {
        public int? suggested_edit_id { get; set; }


        public int? post_id { get; set; }


        public PostType? post_type { get; set; }


        public string body { get; set; }


        public string title { get; set; }


        public List<string> tags { get; set; }


        public string comment { get; set; }


        public DateTime? creation_date { get; set; }


        public DateTime? approval_date { get; set; }


        public DateTime? rejection_date { get; set; }


        public ShallowUser proposing_user { get; set; }

        public bool Equals(SuggestedEdit obj)
        {
            return
                approval_date.TrueEquals(obj.approval_date) &&
                body.TrueEqualsString(obj.body) &&
                comment.TrueEqualsString(obj.comment) &&
                creation_date.TrueEquals(obj.creation_date) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                proposing_user.TrueEquals(obj.proposing_user) &&
                rejection_date.TrueEquals(obj.rejection_date) &&
                suggested_edit_id.TrueEquals(obj.suggested_edit_id) &&
                tags.TrueEqualsString(obj.tags) &&
                title.TrueEqualsString(obj.title);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                approval_date.TrueEquals((DateTime?) obj.approval_date) &&
                body.TrueEqualsString((string) obj.body) &&
                comment.TrueEqualsString((string) obj.comment) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                (proposing_user == null && obj.proposing_user == null ||
                 proposing_user.EqualsDynamic(obj.proposing_user)) &&
                rejection_date.TrueEquals((DateTime?) obj.rejection_date) &&
                suggested_edit_id.TrueEquals((int?) obj.suggested_edit_id) &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title.TrueEqualsString((string) obj.title);
        }
    }
}