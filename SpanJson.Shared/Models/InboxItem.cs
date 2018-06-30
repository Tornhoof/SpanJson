using System;

namespace SpanJson.Shared.Models
{
    public class InboxItem : IGenericEquality<InboxItem>
    {
        public InboxItemType? item_type { get; set; }


        public int? question_id { get; set; }


        public int? answer_id { get; set; }


        public int? comment_id { get; set; }


        public string title { get; set; }


        public DateTime? creation_date { get; set; }


        public bool? is_unread { get; set; }


        public Info.Site site { get; set; }


        public string body { get; set; }


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