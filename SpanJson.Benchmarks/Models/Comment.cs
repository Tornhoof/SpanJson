using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum PostType : byte
    {
        question = 1,
        answer = 2
    }

    [ProtoContract]
    public class Comment : IGenericEquality<Comment>
    {
        [ProtoMember(1)] public int? comment_id { get; set; }

        [ProtoMember(2)] public int? post_id { get; set; }

        [ProtoMember(3)] public DateTime? creation_date { get; set; }

        [ProtoMember(4)] public PostType? post_type { get; set; }

        [ProtoMember(5)] public int? score { get; set; }

        [ProtoMember(6)] public bool? edited { get; set; }

        [ProtoMember(7)] public string body { get; set; }

        [ProtoMember(8)] public ShallowUser owner { get; set; }

        [ProtoMember(9)] public ShallowUser reply_to_user { get; set; }

        [ProtoMember(10)] public string link { get; set; }

        [ProtoMember(11)] public string body_markdown { get; set; }

        [ProtoMember(12)] public bool? upvoted { get; set; }

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