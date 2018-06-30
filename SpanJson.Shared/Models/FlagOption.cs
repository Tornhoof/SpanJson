using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class FlagOption : IGenericEquality<FlagOption>
    {
        public int? option_id { get; set; }


        public bool? requires_comment { get; set; }


        public bool? requires_site { get; set; }


        public bool? requires_question_id { get; set; }


        public string title { get; set; }


        public string description { get; set; }


        public List<FlagOption> sub_options { get; set; }


        public bool? has_flagged { get; set; }


        public int? count { get; set; }

        public bool Equals(FlagOption obj)
        {
            return
                count.TrueEquals(obj.count) &&
                description.TrueEqualsString(obj.description) &&
                has_flagged.TrueEquals(obj.has_flagged) &&
                option_id.TrueEquals(obj.option_id) &&
                requires_comment.TrueEquals(obj.requires_comment) &&
                requires_question_id.TrueEquals(obj.requires_question_id) &&
                requires_site.TrueEquals(obj.requires_site) &&
                sub_options.TrueEqualsList(obj.sub_options) &&
                title.TrueEqualsString(obj.title);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            var equality =
                count.TrueEquals((int?) obj.count) &&
                description.TrueEqualsString((string) obj.description) &&
                has_flagged.TrueEquals((bool?) obj.has_flagged) &&
                option_id.TrueEquals((int?) obj.option_id) &&
                requires_comment.TrueEquals((bool?) obj.requires_comment) &&
                requires_question_id.TrueEquals((bool?) obj.requires_question_id) &&
                requires_site.TrueEquals((bool?) obj.requires_site) &&
                title.TrueEqualsString((string) obj.title);
            if (sub_options != null) // not sure how to solve that nicely, as the dynamic binding of sub_options will throw
            {
                equality &= obj.sub_options != null && sub_options.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.sub_options);
            }

            return equality;
        }
    }
}