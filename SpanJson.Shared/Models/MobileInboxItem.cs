namespace SpanJson.Shared.Models
{
    public sealed class MobileInboxItem : IMobileFeedBase<MobileInboxItem>
    {
        public int? answer_id { get; set; }


        public string body { get; set; }


        public int? comment_id { get; set; }


        public long? creation_date { get; set; }


        public string item_type { get; set; }


        public string link { get; set; }


        public int? question_id { get; set; }


        public string title { get; set; }


        public string site { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileInboxItem obj)
        {
            return
                added_date == obj.added_date &&
                answer_id == obj.answer_id &&
                body == obj.body &&
                comment_id == obj.comment_id &&
                creation_date == obj.creation_date &&
                group_id == obj.group_id &&
                item_type == obj.item_type &&
                link == obj.link &&
                question_id == obj.question_id &&
                site == obj.site &&
                title == obj.title;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                answer_id == (int?) obj.answer_id &&
                body == (string) obj.body &&
                comment_id == (int?) obj.comment_id &&
                creation_date == (long?) obj.creation_date &&
                group_id == (int?) obj.group_id &&
                item_type == (string) obj.item_type &&
                link == (string) obj.link &&
                question_id == (int?) obj.question_id &&
                site == (string) obj.site &&
                title == (string) obj.title;
        }
    }
}