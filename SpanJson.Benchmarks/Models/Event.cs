using System;
using ProtoBuf;

namespace SpanJson.Benchmarks.Models
{
    public enum EventType : byte
    {
        question_posted = 1,
        answer_posted = 2,
        comment_posted = 3,
        post_edited = 4,
        user_created = 5
    }

    [ProtoContract]
    public class Event : IGenericEquality<Event>
    {
        [ProtoMember(1)] public EventType? event_type { get; set; }

        [ProtoMember(2)] public int? event_id { get; set; }

        [ProtoMember(3)] public DateTime? creation_date { get; set; }

        [ProtoMember(4)] public string link { get; set; }

        [ProtoMember(5)] public string excerpt { get; set; }

        public bool Equals(Event obj)
        {
            return
                creation_date.TrueEquals(obj.creation_date) &&
                event_id.TrueEquals(obj.event_id) &&
                event_type.TrueEquals(obj.event_type) &&
                excerpt.TrueEqualsString(obj.excerpt) &&
                link.TrueEqualsString(obj.link);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                event_id.TrueEquals((int?) obj.event_id) &&
                event_type.TrueEquals((EventType?) obj.event_type) &&
                excerpt.TrueEqualsString((string) obj.excerpt) &&
                link.TrueEqualsString((string) obj.link);
        }
    }
}