namespace SpanJson.Shared.Models
{
    public class TopTag : IGenericEquality<TopTag>
    {
        public string tag_name { get; set; }


        public int? question_score { get; set; }


        public int? question_count { get; set; }


        public int? answer_score { get; set; }


        public int? answer_count { get; set; }


        public int? user_id { get; set; }

        public bool Equals(TopTag obj)
        {
            return
                answer_count.TrueEquals(obj.answer_count) &&
                answer_score.TrueEquals(obj.answer_score) &&
                question_count.TrueEquals(obj.question_count) &&
                question_score.TrueEquals(obj.question_score) &&
                tag_name.TrueEqualsString(obj.tag_name) &&
                user_id.TrueEquals(obj.user_id);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                answer_count.TrueEquals((int?) obj.answer_count) &&
                answer_score.TrueEquals((int?) obj.answer_score) &&
                question_count.TrueEquals((int?) obj.question_count) &&
                question_score.TrueEquals((int?) obj.question_score) &&
                tag_name.TrueEqualsString((string) obj.tag_name) &&
                user_id.TrueEquals((int?) obj.user_id);
        }
    }
}