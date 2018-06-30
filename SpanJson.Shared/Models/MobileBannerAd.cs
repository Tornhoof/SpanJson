using System.Collections.Generic;

namespace SpanJson.Shared.Models
{
    public sealed class MobileBannerAd : IMobileFeedBase<MobileBannerAd>
    {
        public string link { get; set; }


        public List<MobileBannerAdImage> images { get; set; }


        public int? group_id { get; set; }


        public long? added_date { get; set; }

        public bool Equals(MobileBannerAd obj)
        {
            return
                added_date == obj.added_date &&
                group_id == obj.group_id &&
                images.TrueEqualsList(obj.images) &&
                link == obj.link;
        }

        public bool EqualsDynamic(dynamic obj)
        {
            return
                added_date == (long?) obj.added_date &&
                group_id == (int?) obj.group_id &&
                images.TrueEqualsListDynamic((IEnumerable<dynamic>) obj.images) &&
                link == (string) obj.link;
        }


        public sealed class MobileBannerAdImage : IGenericEquality<MobileBannerAdImage>
        {
            public string image_url { get; set; }


            public int? width { get; set; }


            public int? height { get; set; }

            public bool Equals(MobileBannerAdImage obj)
            {
                return
                    height == obj.height &&
                    image_url == obj.image_url &&
                    width == obj.width;
            }

            public bool EqualsDynamic(dynamic obj)
            {
                return
                    height == (int?) obj.height &&
                    image_url == (string) obj.image_url &&
                    width == (int?) obj.width;
            }
        }
    }
}