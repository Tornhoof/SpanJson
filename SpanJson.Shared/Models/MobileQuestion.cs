using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public sealed class MobileQuestion : IMobileFeedBase<MobileQuestion>
    {
        public int? question_id { get; set; }


        public long? question_creation_date { get; set; }


        public string title { get; set; }


        public long? last_activity_date { get; set; }


        public List<string> tags { get; set; }


        public string site { get; set; }


        public bool? is_deleted { get; set; }


        public bool? has_accepted_answer { get; set; }


        public int? answer_count { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileQuestion obj)
        {
            return
                added_date == obj.added_date &&
                answer_count == obj.answer_count &&
                group_id == obj.group_id &&
                has_accepted_answer == obj.has_accepted_answer &&
                is_deleted == obj.is_deleted &&
                last_activity_date == obj.last_activity_date &&
                question_creation_date == obj.question_creation_date &&
                question_id == obj.question_id &&
                site == obj.site &&
                tags.TrueEqualsString(obj.tags) &&
                title == obj.title;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                answer_count == (int?) obj.answer_count &&
                group_id == (int?) obj.group_id &&
                has_accepted_answer == (bool?) obj.has_accepted_answer &&
                is_deleted == (bool?) obj.is_deleted &&
                last_activity_date == (long?) obj.last_activity_date &&
                question_creation_date == (long?) obj.question_creation_date &&
                question_id == (int?) obj.question_id &&
                site == (string) obj.site &&
                tags.TrueEqualsString((IEnumerable<string>) obj.tags) &&
                title == (string) obj.title;
        }
    }
}