using System;
using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public class Info : IGenericEquality<Info>
    {
        public int? total_questions { get; set; }


        public int? total_unanswered { get; set; }


        public int? total_accepted { get; set; }


        public int? total_answers { get; set; }


        public decimal? questions_per_minute { get; set; }


        public decimal? answers_per_minute { get; set; }


        public int? total_comments { get; set; }


        public int? total_votes { get; set; }


        public int? total_badges { get; set; }


        public decimal? badges_per_minute { get; set; }


        public int? total_users { get; set; }


        public int? new_active_users { get; set; }


        public string api_revision { get; set; }


        public Site site { get; set; }

        public bool Equals(Info obj)
        {
            return
                answers_per_minute.TrueEquals(obj.answers_per_minute) &&
                api_revision.TrueEqualsString(obj.api_revision) &&
                badges_per_minute.TrueEquals(obj.badges_per_minute) &&
                new_active_users.TrueEquals(obj.new_active_users) &&
                questions_per_minute.TrueEquals(obj.questions_per_minute) &&
                site.TrueEquals(obj.site) &&
                total_accepted.TrueEquals(obj.total_accepted) &&
                total_answers.TrueEquals(obj.total_answers) &&
                total_badges.TrueEquals(obj.total_badges) &&
                total_comments.TrueEquals(obj.total_comments) &&
                total_questions.TrueEquals(obj.total_questions) &&
                total_unanswered.TrueEquals(obj.total_unanswered) &&
                total_users.TrueEquals(obj.total_users) &&
                total_votes.TrueEquals(obj.total_votes);
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                answers_per_minute.TrueEquals((decimal?) obj.answers_per_minute) &&
                api_revision.TrueEqualsString((string) obj.api_revision) &&
                badges_per_minute.TrueEquals((decimal?) obj.badges_per_minute) &&
                new_active_users.TrueEquals((int?) obj.new_active_users) &&
                questions_per_minute.TrueEquals((decimal?) obj.questions_per_minute) &&
                (site == null && obj.site == null || site.EqualsDynamic(obj.site)) &&
                total_accepted.TrueEquals((int?) obj.total_accepted) &&
                total_answers.TrueEquals((int?) obj.total_answers) &&
                total_badges.TrueEquals((int?) obj.total_badges) &&
                total_comments.TrueEquals((int?) obj.total_comments) &&
                total_questions.TrueEquals((int?) obj.total_questions) &&
                total_unanswered.TrueEquals((int?) obj.total_unanswered) &&
                total_users.TrueEquals((int?) obj.total_users) &&
                total_votes.TrueEquals((int?) obj.total_votes);
        }


        public class RelatedSite : IGenericEquality<RelatedSite>
        {
            public enum SiteRelation
            {
                parent,
                meta,
                chat
            }


            public string name { get; set; }


            public string site_url { get; set; }


            public SiteRelation? relation { get; set; }


            public string api_site_parameter { get; set; }

            public bool Equals(RelatedSite obj)
            {
                return
                    name.TrueEqualsString(obj.name) &&
                    relation.TrueEquals(obj.relation) &&
                    api_site_parameter.TrueEqualsString(obj.api_site_parameter);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    name.TrueEqualsString((string) obj.name) &&
                    relation.TrueEquals((SiteRelation?) obj.relation) &&
                    api_site_parameter.TrueEqualsString((string) obj.api_site_parameter);
            }
        }


        public class Site : IGenericEquality<Site>
        {
            public enum SiteState
            {
                normal,
                closed_beta,
                open_beta,
                linked_meta
            }


            public string site_type { get; set; }


            public string name { get; set; }


            public string logo_url { get; set; }


            public string api_site_parameter { get; set; }


            public string site_url { get; set; }


            public string audience { get; set; }


            public string icon_url { get; set; }


            public List<string> aliases { get; set; }


            public SiteState? site_state { get; set; }


            public Styling styling { get; set; }


            public DateTime? closed_beta_date { get; set; }


            public DateTime? open_beta_date { get; set; }


            public DateTime? launch_date { get; set; }


            public string favicon_url { get; set; }


            public List<RelatedSite> related_sites { get; set; }


            public string twitter_account { get; set; }


            public List<string> markdown_extensions { get; set; }


            public string high_resolution_icon_url { get; set; }

            public bool Equals(Site obj)
            {
                return
                    aliases.TrueEqualsString(obj.aliases) &&
                    api_site_parameter.TrueEqualsString(obj.api_site_parameter) &&
                    audience.TrueEqualsString(obj.audience) &&
                    closed_beta_date.TrueEquals(obj.closed_beta_date) &&
                    favicon_url.TrueEqualsString(obj.favicon_url) &&
                    high_resolution_icon_url.TrueEqualsString(obj.high_resolution_icon_url) &&
                    icon_url.TrueEqualsString(obj.icon_url) &&
                    launch_date.TrueEquals(obj.launch_date) &&
                    logo_url.TrueEqualsString(obj.logo_url) &&
                    markdown_extensions.TrueEqualsString(obj.markdown_extensions) &&
                    name.TrueEqualsString(obj.name) &&
                    open_beta_date.TrueEquals(obj.open_beta_date) &&
                    related_sites.TrueEqualsList(obj.related_sites) &&
                    site_state.TrueEquals(obj.site_state) &&
                    site_type.TrueEqualsString(obj.site_type) &&
                    site_url.TrueEqualsString(obj.site_url) &&
                    styling.TrueEquals(obj.styling) &&
                    twitter_account.TrueEqualsString(obj.twitter_account);
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    aliases.TrueEqualsString((IEnumerable<string>) obj.aliases) &&
                    api_site_parameter.TrueEqualsString((string) obj.api_site_parameter) &&
                    audience.TrueEqualsString((string) obj.audience) &&
                    closed_beta_date.TrueEquals((DateTime?) obj.closed_beta_date) &&
                    favicon_url.TrueEqualsString((string) obj.favicon_url) &&
                    high_resolution_icon_url.TrueEqualsString((string) obj.high_resolution_icon_url) &&
                    icon_url.TrueEqualsString((string) obj.icon_url) &&
                    launch_date.TrueEquals((DateTime?) obj.launch_date) &&
                    logo_url.TrueEqualsString((string) obj.logo_url) &&
                    markdown_extensions.TrueEqualsString((IEnumerable<string>) obj.markdown_extensions) &&
                    name.TrueEqualsString((string) obj.name) &&
                    open_beta_date.TrueEquals((DateTime?) obj.open_beta_date) &&
                    related_sites.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.related_sites) &&
                    site_state.TrueEquals((SiteState?) obj.site_state) &&
                    site_type.TrueEqualsString((string) obj.site_type) &&
                    site_url.TrueEqualsString((string) obj.site_url) &&
                    (styling == null && obj.styling == null || styling.EqualsDynamic(obj.styling)) &&
                    twitter_account.TrueEqualsString((string) obj.twitter_account);
            }


            public class Styling : IGenericEquality<Styling>
            {
                public string link_color { get; set; }


                public string tag_foreground_color { get; set; }


                public string tag_background_color { get; set; }

                public bool Equals(Styling obj)
                {
                    return
                        link_color.TrueEqualsString(obj.link_color) &&
                        tag_background_color.TrueEqualsString(obj.tag_background_color) &&
                        tag_foreground_color.TrueEqualsString(obj.tag_foreground_color);
                }

                public bool EqualsDynamic(dynamic obj)
                {
                    return
                        link_color.TrueEqualsString((string) obj.link_color) &&
                        tag_background_color.TrueEqualsString((string) obj.tag_background_color) &&
                        tag_foreground_color.TrueEqualsString((string) obj.tag_foreground_color);
                }
            }
        }
    }
}