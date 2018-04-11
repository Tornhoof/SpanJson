using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    [ProtoContract]
    public class TopTag : IGenericEquality<TopTag>
    {
        [ProtoMember(1)] public string tag_name { get; set; }

        [ProtoMember(2)] public int? question_score { get; set; }

        [ProtoMember(3)] public int? question_count { get; set; }

        [ProtoMember(4)] public int? answer_score { get; set; }

        [ProtoMember(5)] public int? answer_count { get; set; }

        [ProtoMember(6)] public int? user_id { get; set; }

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