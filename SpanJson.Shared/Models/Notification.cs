using System;

namespace SpanJson.Shared.Models
{
    public class Notification : IGenericEquality<Notification>
    {
        public NotificationType? notification_type { get; set; }


        public Info.Site site { get; set; }


        public DateTime? creation_date { get; set; }


        public string body { get; set; }


        public int? post_id { get; set; }


        public bool? is_unread { get; set; }

        public bool Equals(Notification obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                site.TrueEquals(obj.site) &&
                creation_date.TrueEquals(obj.creation_date) &&
                post_id.TrueEquals(obj.post_id) &&
                is_unread.TrueEquals(obj.is_unread);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                (site == null && obj.site == null || site.EqualsDynamic(obj.site)) &&
                creation_date.TrueEquals((DateTime?) obj.creation_date) &&
                post_id.TrueEquals((int?) obj.post_id) &&
                is_unread.TrueEquals((bool?) obj.is_unread);
        }
    }
}