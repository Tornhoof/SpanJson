using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class Tag : IGenericEquality<Tag>
    {
        public string name { get; set; }


        public int? count { get; set; }


        public bool? is_required { get; set; }


        public bool? is_moderator_only { get; set; }


        public int? user_id { get; set; }


        public bool? has_synonyms { get; set; }


        public DateTime? last_activity_date { get; set; }


        public List<string> synonyms { get; set; }

        public bool Equals(Tag obj)
        {
            return
                count.TrueEquals(obj.count) &&
                has_synonyms.TrueEquals(obj.has_synonyms) &&
                is_moderator_only.TrueEquals(obj.is_moderator_only) &&
                is_required.TrueEquals(obj.is_required) &&
                last_activity_date.TrueEquals(obj.last_activity_date) &&
                name.TrueEqualsString(obj.name) &&
                synonyms.TrueEqualsString(obj.synonyms) &&
                user_id.TrueEquals(obj.user_id);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                count.TrueEquals((int?) obj.count) &&
                has_synonyms.TrueEquals((bool?) obj.has_synonyms) &&
                is_moderator_only.TrueEquals((bool?) obj.is_moderator_only) &&
                is_required.TrueEquals((bool?) obj.is_required) &&
                last_activity_date.TrueEquals((DateTime?) obj.last_activity_date) &&
                name.TrueEqualsString((string) obj.name) &&
                synonyms.TrueEqualsString((IEnumerable<string>) obj.synonyms) &&
                user_id.TrueEquals((int?) obj.user_id);
        }
    }
}