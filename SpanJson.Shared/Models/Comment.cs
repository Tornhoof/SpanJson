using System;

namespace SpanJson.Shared.Models
{
    public class Comment : IGenericEquality<Comment>
    {
        public int? comment_id { get; set; }


        public int? post_id { get; set; }


        public DateTime? creation_date { get; set; }


        public PostType? post_type { get; set; }


        public int? score { get; set; }


        public bool? edited { get; set; }


        public string body { get; set; }


        public ShallowUser owner { get; set; }


        public ShallowUser reply_to_user { get; set; }


        public string link { get; set; }


        public string body_markdown { get; set; }


        public bool? upvoted { get; set; }

        public bool Equals(Comment obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                body_markdown.TrueEqualsString(obj.body_markdown) &&
                comment_id.TrueEquals(obj.comment_id) &&
                creation_date.TrueEquals(obj.creation_date) &&
                edited.TrueEquals(obj.edited) &&
                link.TrueEqualsString(obj.link) &&
                owner.TrueEquals(obj.owner) &&
                post_id.TrueEquals(obj.post_id) &&
                post_type.TrueEquals(obj.post_type) &&
                reply_to_user.TrueEquals(obj.reply_to_user) &&
                score.TrueEquals(obj.score) &&
                upvoted.TrueEquals(obj.upvoted);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                body_markdown.TrueEqualsString((string) obj.body_markdown) &&
                comment_id.TrueEquals((int?) obj.comment_id) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                edited.TrueEquals((bool?) obj.edited) &&
                link.TrueEqualsString((string) obj.link) &&
                (owner == null && obj.owner == null || owner.EqualsDynamic(obj.owner)) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                post_type.TrueEquals((PostType?) obj.post_type) &&
                (reply_to_user == null && obj.reply_to_user == null ||
                 reply_to_user.EqualsDynamic(obj.reply_to_user)) &&
                score.TrueEquals((int?) obj.score) &&
                upvoted.TrueEquals((bool?) obj.upvoted);
        }
    }
}