namespace SpanJson.Shared.Models
{
    public sealed class MobileCareersJobAd : IMobileFeedBase<MobileCareersJobAd>
    {
        public int? job_id { get; set; }


        public string link { get; set; }


        public string company_name { get; set; }


        public string location { get; set; }


        public string title { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileCareersJobAd obj)
        {
            return
                added_date == obj.added_date &&
                company_name == obj.company_name &&
                group_id == obj.group_id &&
                job_id == obj.job_id &&
                link == obj.link &&
                location == obj.location &&
                title == obj.title;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                company_name == (string) obj.company_name &&
                group_id == (int?) obj.group_id &&
                job_id == (int?) obj.job_id &&
                link == (string) obj.link &&
                location == (string) obj.location &&
                title == (string) obj.title;
        }
    }
}