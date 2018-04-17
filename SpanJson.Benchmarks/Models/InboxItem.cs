using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum InboxItemType
    {
        comment = 1,
        chat_message = 2,
        new_answer = 3,
        careers_message = 4,
        careers_invitations = 5,
        meta_question = 6,
        post_notice = 7,
        moderator_message = 8
    }

    [ProtoContract]
    public class InboxItem : IGenericEquality<InboxItem>
    {
        [ProtoMember(1)]
        public InboxItemType? item_type { get; set; }

        [ProtoMember(2)]
        public int? question_id { get; set; }

        [ProtoMember(3)]
        public int? answer_id { get; set; }

        [ProtoMember(4)]
        public int? comment_id { get; set; }

        [ProtoMember(5)]
        public string title { get; set; }

        [ProtoMember(6)]
        public DateTime? creation_date { get; set; }

        [ProtoMember(7)]
        public bool? is_unread { get; set; }

        [ProtoMember(8)]
        public Info.Site site { get; set; }

        [ProtoMember(9)]
        public string body { get; set; }

        [ProtoMember(10)]
        public string link { get; set; }

        public bool Equals(InboxItem obj)
        {
            return
                answer_id.TrueEquals(obj.answer_id) &&
                body.TrueEqualsString(obj.body) &&
                comment_id.TrueEquals(obj.comment_id) &&
                creation_date.TrueEquals(obj.creation_date) &&
                is_unread.TrueEquals(obj.is_unread) &&
                item_type.TrueEquals(obj.item_type) &&
                link.TrueEqualsString(obj.link) &&
                question_id.TrueEquals(obj.question_id) &&
                site.TrueEquals(obj.site) &&
                title.TrueEqualsString(obj.title);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                answer_id.TrueEquals((int?) obj.answer_id) &&
                body.TrueEqualsString((string) obj.body) &&
                comment_id.TrueEquals((int?) obj.comment_id) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                is_unread.TrueEquals((bool?) obj.is_unread) &&
                item_type.TrueEquals((InboxItemType?) obj.item_type) &&
                link.TrueEqualsString((string) obj.link) &&
                question_id.TrueEquals((int?) obj.question_id) &&
                (site == null && obj.site == null || site.EqualsDynamic(obj.site)) &&
                title.TrueEqualsString((string) obj.title);
        }
    }
}