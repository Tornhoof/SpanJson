using System;

namespace SpanJson.Shared.Models
{
    public class Event : IGenericEquality<Event>
    {
        public EventType? event_type { get; set; }


        public int? event_id { get; set; }


        public DateTime? creation_date { get; set; }


        public string link { get; set; }


        public string excerpt { get; set; }

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