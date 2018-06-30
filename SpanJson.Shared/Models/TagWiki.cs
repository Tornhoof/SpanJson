using System;

namespace SpanJson.Shared.Models
{
    public class TagWiki : IGenericEquality<TagWiki>
    {
        public string tag_name { get; set; }


        public string body { get; set; }


        public string excerpt { get; set; }


        public DateTime? body_last_edit_date { get; set; }


        public DateTime? excerpt_last_edit_date { get; set; }


        public ShallowUser last_body_editor { get; set; }


        public ShallowUser last_excerpt_editor { get; set; }

        public bool Equals(TagWiki obj)
        {
            return
                body.TrueEqualsString(obj.body) &&
                body_last_edit_date.TrueEquals(obj.body_last_edit_date) &&
                excerpt.TrueEqualsString(obj.excerpt) &&
                excerpt_last_edit_date.TrueEquals(obj.excerpt_last_edit_date) &&
                last_body_editor.TrueEquals(obj.last_body_editor) &&
                last_excerpt_editor.TrueEquals(obj.last_excerpt_editor) &&
                tag_name.TrueEqualsString(obj.tag_name);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                body.TrueEqualsString((string) obj.body) &&
                body_last_edit_date.TrueEquals((DateTime?) obj.body_last_edit_date) &&
                excerpt.TrueEqualsString((string) obj.excerpt) &&
                excerpt_last_edit_date.TrueEquals((DateTime?) obj.excerpt_last_edit_date) &&
                (last_body_editor == null && obj.last_body_editor == null ||
                 last_body_editor.EqualsDynamic(obj.last_body_editor)) &&
                (last_excerpt_editor == null && obj.last_excerpt_editor == null ||
                 last_excerpt_editor.EqualsDynamic(obj.last_excerpt_editor)) &&
                tag_name.TrueEqualsString((string) obj.tag_name);
        }
    }
}