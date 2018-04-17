using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;
using SpanJson.Benchmarks.Models;
using SpanJson.Benchmarks.Serializers;

namespace SpanJson.Benchmarks
{
    [Config(typeof(MyConfig))]
    public class ModelBenchmark
    {
        private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
        private static readonly AccessToken AccessTokenInput = ExpressionTreeFixture.Create<AccessToken>();


        private static readonly JilSerializer JilSerializer =
            new JilSerializer();

        private static readonly string AccessTokenOutputOfJilSerializer = JilSerializer.Serialize(AccessTokenInput);

        private static readonly SpanJsonSerializer SpanJsonSerializer =
            new SpanJsonSerializer();

        private static readonly string AccessTokenOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(AccessTokenInput);

        private static readonly Utf8JsonSerializer Utf8JsonSerializer =
            new Utf8JsonSerializer();

        private static readonly byte[] AccessTokenOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(AccessTokenInput);

        private static readonly AccountMerge AccountMergeInput = ExpressionTreeFixture.Create<AccountMerge>();

        private static readonly string AccountMergeOutputOfJilSerializer = JilSerializer.Serialize(AccountMergeInput);

        private static readonly string AccountMergeOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(AccountMergeInput);

        private static readonly byte[] AccountMergeOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(AccountMergeInput);

        private static readonly Answer AnswerInput = ExpressionTreeFixture.Create<Answer>();

        private static readonly string AnswerOutputOfJilSerializer = JilSerializer.Serialize(AnswerInput);

        private static readonly string AnswerOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(AnswerInput);

        private static readonly byte[] AnswerOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AnswerInput);

        private static readonly User.BadgeCount BadgeCountInput = ExpressionTreeFixture.Create<User.BadgeCount>();

        private static readonly string BadgeCountOutputOfJilSerializer = JilSerializer.Serialize(BadgeCountInput);

        private static readonly string BadgeCountOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(BadgeCountInput);

        private static readonly byte[] BadgeCountOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(BadgeCountInput);

        private static readonly Badge BadgeInput = ExpressionTreeFixture.Create<Badge>();

        private static readonly string BadgeOutputOfJilSerializer = JilSerializer.Serialize(BadgeInput);

        private static readonly string BadgeOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(BadgeInput);

        private static readonly byte[] BadgeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BadgeInput);

        private static readonly Question.ClosedDetails ClosedDetailsInput =
            ExpressionTreeFixture.Create<Question.ClosedDetails>();

        private static readonly string ClosedDetailsOutputOfJilSerializer = JilSerializer.Serialize(ClosedDetailsInput);

        private static readonly string ClosedDetailsOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(ClosedDetailsInput);

        private static readonly byte[] ClosedDetailsOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(ClosedDetailsInput);

        private static readonly Comment CommentInput = ExpressionTreeFixture.Create<Comment>();

        private static readonly string CommentOutputOfJilSerializer = JilSerializer.Serialize(CommentInput);

        private static readonly string CommentOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(CommentInput);

        private static readonly byte[] CommentOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(CommentInput);

        private static readonly Error ErrorInput = ExpressionTreeFixture.Create<Error>();

        private static readonly string ErrorOutputOfJilSerializer = JilSerializer.Serialize(ErrorInput);

        private static readonly string ErrorOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(ErrorInput);

        private static readonly byte[] ErrorOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ErrorInput);

        private static readonly Event EventInput = ExpressionTreeFixture.Create<Event>();

        private static readonly string EventOutputOfJilSerializer = JilSerializer.Serialize(EventInput);

        private static readonly string EventOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(EventInput);

        private static readonly byte[] EventOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(EventInput);


        private static readonly FlagOption FlagOptionInput = ExpressionTreeFixture.Create<FlagOption>();

        private static readonly string FlagOptionOutputOfJilSerializer = JilSerializer.Serialize(FlagOptionInput);

        private static readonly string FlagOptionOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(FlagOptionInput);

        private static readonly byte[] FlagOptionOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(FlagOptionInput);

        private static readonly InboxItem InboxItemInput = ExpressionTreeFixture.Create<InboxItem>();

        private static readonly string InboxItemOutputOfJilSerializer = JilSerializer.Serialize(InboxItemInput);

        private static readonly string InboxItemOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(InboxItemInput);

        private static readonly byte[] InboxItemOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(InboxItemInput);

        private static readonly Info InfoInput = ExpressionTreeFixture.Create<Info>();

        private static readonly string InfoOutputOfJilSerializer = JilSerializer.Serialize(InfoInput);

        private static readonly string InfoOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(InfoInput);

        private static readonly byte[] InfoOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(InfoInput);


        private static readonly Question.MigrationInfo MigrationInfoInput =
            ExpressionTreeFixture.Create<Question.MigrationInfo>();

        private static readonly string MigrationInfoOutputOfJilSerializer = JilSerializer.Serialize(MigrationInfoInput);

        private static readonly string MigrationInfoOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MigrationInfoInput);

        private static readonly byte[] MigrationInfoOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MigrationInfoInput);

        private static readonly MobileAssociationBonus MobileAssociationBonusInput =
            ExpressionTreeFixture.Create<MobileAssociationBonus>();

        private static readonly string MobileAssociationBonusOutputOfJilSerializer =
            JilSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly string MobileAssociationBonusOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly byte[] MobileAssociationBonusOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly MobileBadgeAward MobileBadgeAwardInput =
            ExpressionTreeFixture.Create<MobileBadgeAward>();

        private static readonly string MobileBadgeAwardOutputOfJilSerializer =
            JilSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly string MobileBadgeAwardOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly byte[] MobileBadgeAwardOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly MobileBannerAd.MobileBannerAdImage MobileBannerAdImageInput =
            ExpressionTreeFixture.Create<MobileBannerAd.MobileBannerAdImage>();

        private static readonly string MobileBannerAdImageOutputOfJilSerializer =
            JilSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly string MobileBannerAdImageOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly byte[] MobileBannerAdImageOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly MobileBannerAd MobileBannerAdInput = ExpressionTreeFixture.Create<MobileBannerAd>();

        private static readonly string MobileBannerAdOutputOfJilSerializer =
            JilSerializer.Serialize(MobileBannerAdInput);

        private static readonly string MobileBannerAdOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileBannerAdInput);

        private static readonly byte[] MobileBannerAdOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileBannerAdInput);

        private static readonly MobileCareersJobAd MobileCareersJobAdInput =
            ExpressionTreeFixture.Create<MobileCareersJobAd>();

        private static readonly string MobileCareersJobAdOutputOfJilSerializer =
            JilSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly string MobileCareersJobAdOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly byte[] MobileCareersJobAdOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly MobileCommunityBulletin MobileCommunityBulletinInput =
            ExpressionTreeFixture.Create<MobileCommunityBulletin>();

        private static readonly string MobileCommunityBulletinOutputOfJilSerializer =
            JilSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly string MobileCommunityBulletinOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly byte[] MobileCommunityBulletinOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly MobileFeed MobileFeedInput = ExpressionTreeFixture.Create<MobileFeed>();

        private static readonly string MobileFeedOutputOfJilSerializer = JilSerializer.Serialize(MobileFeedInput);

        private static readonly string MobileFeedOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileFeedInput);

        private static readonly byte[] MobileFeedOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileFeedInput);

        private static readonly MobileInboxItem MobileInboxItemInput = ExpressionTreeFixture.Create<MobileInboxItem>();

        private static readonly string MobileInboxItemOutputOfJilSerializer =
            JilSerializer.Serialize(MobileInboxItemInput);

        private static readonly string MobileInboxItemOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileInboxItemInput);

        private static readonly byte[] MobileInboxItemOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileInboxItemInput);

        private static readonly MobilePrivilege MobilePrivilegeInput = ExpressionTreeFixture.Create<MobilePrivilege>();

        private static readonly string MobilePrivilegeOutputOfJilSerializer =
            JilSerializer.Serialize(MobilePrivilegeInput);

        private static readonly string MobilePrivilegeOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobilePrivilegeInput);

        private static readonly byte[] MobilePrivilegeOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobilePrivilegeInput);

        private static readonly MobileQuestion MobileQuestionInput = ExpressionTreeFixture.Create<MobileQuestion>();

        private static readonly string MobileQuestionOutputOfJilSerializer =
            JilSerializer.Serialize(MobileQuestionInput);

        private static readonly string MobileQuestionOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileQuestionInput);

        private static readonly byte[] MobileQuestionOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileQuestionInput);

        private static readonly MobileRepChange MobileRepChangeInput = ExpressionTreeFixture.Create<MobileRepChange>();

        private static readonly string MobileRepChangeOutputOfJilSerializer =
            JilSerializer.Serialize(MobileRepChangeInput);

        private static readonly string MobileRepChangeOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileRepChangeInput);

        private static readonly byte[] MobileRepChangeOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileRepChangeInput);

        private static readonly MobileUpdateNotice MobileUpdateNoticeInput =
            ExpressionTreeFixture.Create<MobileUpdateNotice>();

        private static readonly string MobileUpdateNoticeOutputOfJilSerializer =
            JilSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly string MobileUpdateNoticeOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly byte[] MobileUpdateNoticeOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly NetworkUser NetworkUserInput = ExpressionTreeFixture.Create<NetworkUser>();

        private static readonly string NetworkUserOutputOfJilSerializer = JilSerializer.Serialize(NetworkUserInput);

        private static readonly string NetworkUserOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(NetworkUserInput);

        private static readonly byte[] NetworkUserOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(NetworkUserInput);

        private static readonly Question.Notice NoticeInput = ExpressionTreeFixture.Create<Question.Notice>();

        private static readonly string NoticeOutputOfJilSerializer = JilSerializer.Serialize(NoticeInput);

        private static readonly string NoticeOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(NoticeInput);

        private static readonly byte[] NoticeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NoticeInput);

        private static readonly Notification NotificationInput = ExpressionTreeFixture.Create<Notification>();

        private static readonly string NotificationOutputOfJilSerializer = JilSerializer.Serialize(NotificationInput);

        private static readonly string NotificationOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(NotificationInput);

        private static readonly byte[] NotificationOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(NotificationInput);

        private static readonly Question.ClosedDetails.OriginalQuestion OriginalQuestionInput =
            ExpressionTreeFixture.Create<Question.ClosedDetails.OriginalQuestion>();

        private static readonly string OriginalQuestionOutputOfJilSerializer =
            JilSerializer.Serialize(OriginalQuestionInput);

        private static readonly string OriginalQuestionOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(OriginalQuestionInput);

        private static readonly byte[] OriginalQuestionOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(OriginalQuestionInput);

        private static readonly Post PostInput = ExpressionTreeFixture.Create<Post>();

        private static readonly string PostOutputOfJilSerializer = JilSerializer.Serialize(PostInput);

        private static readonly string PostOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(PostInput);

        private static readonly byte[] PostOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(PostInput);

        private static readonly Privilege PrivilegeInput = ExpressionTreeFixture.Create<Privilege>();

        private static readonly string PrivilegeOutputOfJilSerializer = JilSerializer.Serialize(PrivilegeInput);

        private static readonly string PrivilegeOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(PrivilegeInput);

        private static readonly byte[] PrivilegeOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(PrivilegeInput);

        private static readonly Question QuestionInput = ExpressionTreeFixture.Create<Question>();

        private static readonly string QuestionOutputOfJilSerializer = JilSerializer.Serialize(QuestionInput);

        private static readonly string QuestionOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(QuestionInput);

        private static readonly byte[] QuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(QuestionInput);

        private static readonly QuestionTimeline QuestionTimelineInput =
            ExpressionTreeFixture.Create<QuestionTimeline>();

        private static readonly string QuestionTimelineOutputOfJilSerializer =
            JilSerializer.Serialize(QuestionTimelineInput);

        private static readonly string QuestionTimelineOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(QuestionTimelineInput);

        private static readonly byte[] QuestionTimelineOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(QuestionTimelineInput);

        private static readonly Info.RelatedSite RelatedSiteInput = ExpressionTreeFixture.Create<Info.RelatedSite>();

        private static readonly string RelatedSiteOutputOfJilSerializer = JilSerializer.Serialize(RelatedSiteInput);

        private static readonly string RelatedSiteOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(RelatedSiteInput);

        private static readonly byte[] RelatedSiteOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(RelatedSiteInput);

        private static readonly ReputationHistory ReputationHistoryInput =
            ExpressionTreeFixture.Create<ReputationHistory>();

        private static readonly string ReputationHistoryOutputOfJilSerializer =
            JilSerializer.Serialize(ReputationHistoryInput);

        private static readonly string ReputationHistoryOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(ReputationHistoryInput);

        private static readonly byte[] ReputationHistoryOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(ReputationHistoryInput);

        private static readonly Reputation ReputationInput = ExpressionTreeFixture.Create<Reputation>();

        private static readonly string ReputationOutputOfJilSerializer = JilSerializer.Serialize(ReputationInput);

        private static readonly string ReputationOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(ReputationInput);

        private static readonly byte[] ReputationOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(ReputationInput);

        private static readonly Revision RevisionInput = ExpressionTreeFixture.Create<Revision>();

        private static readonly string RevisionOutputOfJilSerializer = JilSerializer.Serialize(RevisionInput);

        private static readonly string RevisionOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(RevisionInput);

        private static readonly byte[] RevisionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(RevisionInput);

        private static readonly SearchExcerpt SearchExcerptInput = ExpressionTreeFixture.Create<SearchExcerpt>();

        private static readonly string SearchExcerptOutputOfJilSerializer = JilSerializer.Serialize(SearchExcerptInput);

        private static readonly string SearchExcerptOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(SearchExcerptInput);

        private static readonly byte[] SearchExcerptOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(SearchExcerptInput);

        private static readonly ShallowUser ShallowUserInput = ExpressionTreeFixture.Create<ShallowUser>();

        private static readonly string ShallowUserOutputOfJilSerializer = JilSerializer.Serialize(ShallowUserInput);

        private static readonly string ShallowUserOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(ShallowUserInput);

        private static readonly byte[] ShallowUserOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(ShallowUserInput);

        private static readonly Info.Site SiteInput = ExpressionTreeFixture.Create<Info.Site>();

        private static readonly string SiteOutputOfJilSerializer = JilSerializer.Serialize(SiteInput);

        private static readonly string SiteOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(SiteInput);

        private static readonly byte[] SiteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SiteInput);


        private static readonly Info.Site.Styling StylingInput = ExpressionTreeFixture.Create<Info.Site.Styling>();

        private static readonly string StylingOutputOfJilSerializer = JilSerializer.Serialize(StylingInput);

        private static readonly string StylingOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(StylingInput);

        private static readonly byte[] StylingOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StylingInput);

        private static readonly SuggestedEdit SuggestedEditInput = ExpressionTreeFixture.Create<SuggestedEdit>();

        private static readonly string SuggestedEditOutputOfJilSerializer = JilSerializer.Serialize(SuggestedEditInput);

        private static readonly string SuggestedEditOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(SuggestedEditInput);

        private static readonly byte[] SuggestedEditOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(SuggestedEditInput);

        private static readonly Tag TagInput = ExpressionTreeFixture.Create<Tag>();

        private static readonly string TagOutputOfJilSerializer = JilSerializer.Serialize(TagInput);

        private static readonly string TagOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(TagInput);

        private static readonly byte[] TagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagInput);

        private static readonly TagScore TagScoreInput = ExpressionTreeFixture.Create<TagScore>();

        private static readonly string TagScoreOutputOfJilSerializer = JilSerializer.Serialize(TagScoreInput);

        private static readonly string TagScoreOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(TagScoreInput);

        private static readonly byte[] TagScoreOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagScoreInput);

        private static readonly TagSynonym TagSynonymInput = ExpressionTreeFixture.Create<TagSynonym>();

        private static readonly string TagSynonymOutputOfJilSerializer = JilSerializer.Serialize(TagSynonymInput);

        private static readonly string TagSynonymOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(TagSynonymInput);

        private static readonly byte[] TagSynonymOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(TagSynonymInput);

        private static readonly TagWiki TagWikiInput = ExpressionTreeFixture.Create<TagWiki>();

        private static readonly string TagWikiOutputOfJilSerializer = JilSerializer.Serialize(TagWikiInput);

        private static readonly string TagWikiOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(TagWikiInput);

        private static readonly byte[] TagWikiOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagWikiInput);

        private static readonly TopTag TopTagInput = ExpressionTreeFixture.Create<TopTag>();

        private static readonly string TopTagOutputOfJilSerializer = JilSerializer.Serialize(TopTagInput);

        private static readonly string TopTagOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(TopTagInput);

        private static readonly byte[] TopTagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TopTagInput);

        private static readonly User UserInput = ExpressionTreeFixture.Create<User>();

        private static readonly string UserOutputOfJilSerializer = JilSerializer.Serialize(UserInput);

        private static readonly string UserOutputOfJsonSpanSerializer = SpanJsonSerializer.Serialize(UserInput);

        private static readonly byte[] UserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UserInput);

        private static readonly UserTimeline UserTimelineInput = ExpressionTreeFixture.Create<UserTimeline>();

        private static readonly string UserTimelineOutputOfJilSerializer = JilSerializer.Serialize(UserTimelineInput);

        private static readonly string UserTimelineOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(UserTimelineInput);

        private static readonly byte[] UserTimelineOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(UserTimelineInput);


        private static readonly WritePermission WritePermissionInput = ExpressionTreeFixture.Create<WritePermission>();

        private static readonly string WritePermissionOutputOfJilSerializer =
            JilSerializer.Serialize(WritePermissionInput);

        private static readonly string WritePermissionOutputOfJsonSpanSerializer =
            SpanJsonSerializer.Serialize(WritePermissionInput);

        private static readonly byte[] WritePermissionOutputOfUtf8JsonSerializer =
            Utf8JsonSerializer.Serialize(WritePermissionInput);


        [Benchmark]
        public string SerializeAccessTokenWithJilSerializer()
        {
            return JilSerializer.Serialize(AccessTokenInput);
        }


        [Benchmark]
        public string SerializeAccessTokenWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(AccessTokenInput);
        }


        [Benchmark]
        public byte[] SerializeAccessTokenWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(AccessTokenInput);
        }


        [Benchmark]
        public string SerializeAccountMergeWithJilSerializer()
        {
            return JilSerializer.Serialize(AccountMergeInput);
        }


        [Benchmark]
        public string SerializeAccountMergeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(AccountMergeInput);
        }


        [Benchmark]
        public byte[] SerializeAccountMergeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(AccountMergeInput);
        }


        [Benchmark]
        public string SerializeAnswerWithJilSerializer()
        {
            return JilSerializer.Serialize(AnswerInput);
        }


        [Benchmark]
        public string SerializeAnswerWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(AnswerInput);
        }


        [Benchmark]
        public byte[] SerializeAnswerWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(AnswerInput);
        }


        [Benchmark]
        public string SerializeBadgeWithJilSerializer()
        {
            return JilSerializer.Serialize(BadgeInput);
        }


        [Benchmark]
        public string SerializeBadgeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(BadgeInput);
        }


        [Benchmark]
        public byte[] SerializeBadgeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(BadgeInput);
        }


        [Benchmark]
        public string SerializeCommentWithJilSerializer()
        {
            return JilSerializer.Serialize(CommentInput);
        }


        [Benchmark]
        public string SerializeCommentWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(CommentInput);
        }


        [Benchmark]
        public byte[] SerializeCommentWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(CommentInput);
        }


        [Benchmark]
        public string SerializeErrorWithJilSerializer()
        {
            return JilSerializer.Serialize(ErrorInput);
        }


        [Benchmark]
        public string SerializeErrorWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ErrorInput);
        }


        [Benchmark]
        public byte[] SerializeErrorWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ErrorInput);
        }


        [Benchmark]
        public string SerializeEventWithJilSerializer()
        {
            return JilSerializer.Serialize(EventInput);
        }


        [Benchmark]
        public string SerializeEventWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(EventInput);
        }


        [Benchmark]
        public byte[] SerializeEventWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(EventInput);
        }


        [Benchmark]
        public string SerializeMobileFeedWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileFeedInput);
        }


        [Benchmark]
        public string SerializeMobileFeedWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileFeedInput);
        }


        [Benchmark]
        public byte[] SerializeMobileFeedWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileFeedInput);
        }


        [Benchmark]
        public string SerializeMobileQuestionWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileQuestionInput);
        }


        [Benchmark]
        public string SerializeMobileQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileQuestionInput);
        }


        [Benchmark]
        public byte[] SerializeMobileQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileQuestionInput);
        }


        [Benchmark]
        public string SerializeMobileRepChangeWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileRepChangeInput);
        }


        [Benchmark]
        public string SerializeMobileRepChangeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileRepChangeInput);
        }


        [Benchmark]
        public byte[] SerializeMobileRepChangeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileRepChangeInput);
        }


        [Benchmark]
        public string SerializeMobileInboxItemWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileInboxItemInput);
        }


        [Benchmark]
        public string SerializeMobileInboxItemWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileInboxItemInput);
        }


        [Benchmark]
        public byte[] SerializeMobileInboxItemWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileInboxItemInput);
        }


        [Benchmark]
        public string SerializeMobileBadgeAwardWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileBadgeAwardInput);
        }


        [Benchmark]
        public string SerializeMobileBadgeAwardWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBadgeAwardInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBadgeAwardWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileBadgeAwardInput);
        }


        [Benchmark]
        public string SerializeMobilePrivilegeWithJilSerializer()
        {
            return JilSerializer.Serialize(MobilePrivilegeInput);
        }


        [Benchmark]
        public string SerializeMobilePrivilegeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobilePrivilegeInput);
        }


        [Benchmark]
        public byte[] SerializeMobilePrivilegeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobilePrivilegeInput);
        }


        [Benchmark]
        public string SerializeMobileCommunityBulletinWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileCommunityBulletinInput);
        }


        [Benchmark]
        public string SerializeMobileCommunityBulletinWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);
        }


        [Benchmark]
        public byte[] SerializeMobileCommunityBulletinWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileCommunityBulletinInput);
        }


        [Benchmark]
        public string SerializeMobileAssociationBonusWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileAssociationBonusInput);
        }


        [Benchmark]
        public string SerializeMobileAssociationBonusWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileAssociationBonusInput);
        }


        [Benchmark]
        public byte[] SerializeMobileAssociationBonusWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileAssociationBonusInput);
        }


        [Benchmark]
        public string SerializeMobileCareersJobAdWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileCareersJobAdInput);
        }


        [Benchmark]
        public string SerializeMobileCareersJobAdWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileCareersJobAdInput);
        }


        [Benchmark]
        public byte[] SerializeMobileCareersJobAdWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileCareersJobAdInput);
        }


        [Benchmark]
        public string SerializeMobileBannerAdWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileBannerAdInput);
        }


        [Benchmark]
        public string SerializeMobileBannerAdWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBannerAdInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBannerAdWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileBannerAdInput);
        }


        [Benchmark]
        public string SerializeMobileUpdateNoticeWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileUpdateNoticeInput);
        }


        [Benchmark]
        public string SerializeMobileUpdateNoticeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);
        }


        [Benchmark]
        public byte[] SerializeMobileUpdateNoticeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileUpdateNoticeInput);
        }


        [Benchmark]
        public string SerializeFlagOptionWithJilSerializer()
        {
            return JilSerializer.Serialize(FlagOptionInput);
        }


        [Benchmark]
        public string SerializeFlagOptionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(FlagOptionInput);
        }


        [Benchmark]
        public byte[] SerializeFlagOptionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(FlagOptionInput);
        }


        [Benchmark]
        public string SerializeInboxItemWithJilSerializer()
        {
            return JilSerializer.Serialize(InboxItemInput);
        }


        [Benchmark]
        public string SerializeInboxItemWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(InboxItemInput);
        }


        [Benchmark]
        public byte[] SerializeInboxItemWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(InboxItemInput);
        }


        [Benchmark]
        public string SerializeInfoWithJilSerializer()
        {
            return JilSerializer.Serialize(InfoInput);
        }


        [Benchmark]
        public string SerializeInfoWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(InfoInput);
        }


        [Benchmark]
        public byte[] SerializeInfoWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(InfoInput);
        }


        [Benchmark]
        public string SerializeNetworkUserWithJilSerializer()
        {
            return JilSerializer.Serialize(NetworkUserInput);
        }


        [Benchmark]
        public string SerializeNetworkUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(NetworkUserInput);
        }


        [Benchmark]
        public byte[] SerializeNetworkUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(NetworkUserInput);
        }


        [Benchmark]
        public string SerializeNotificationWithJilSerializer()
        {
            return JilSerializer.Serialize(NotificationInput);
        }


        [Benchmark]
        public string SerializeNotificationWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(NotificationInput);
        }


        [Benchmark]
        public byte[] SerializeNotificationWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(NotificationInput);
        }


        [Benchmark]
        public string SerializePostWithJilSerializer()
        {
            return JilSerializer.Serialize(PostInput);
        }


        [Benchmark]
        public string SerializePostWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(PostInput);
        }


        [Benchmark]
        public byte[] SerializePostWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(PostInput);
        }


        [Benchmark]
        public string SerializePrivilegeWithJilSerializer()
        {
            return JilSerializer.Serialize(PrivilegeInput);
        }


        [Benchmark]
        public string SerializePrivilegeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(PrivilegeInput);
        }


        [Benchmark]
        public byte[] SerializePrivilegeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(PrivilegeInput);
        }


        [Benchmark]
        public string SerializeQuestionWithJilSerializer()
        {
            return JilSerializer.Serialize(QuestionInput);
        }


        [Benchmark]
        public string SerializeQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(QuestionInput);
        }


        [Benchmark]
        public byte[] SerializeQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(QuestionInput);
        }


        [Benchmark]
        public string SerializeQuestionTimelineWithJilSerializer()
        {
            return JilSerializer.Serialize(QuestionTimelineInput);
        }


        [Benchmark]
        public string SerializeQuestionTimelineWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(QuestionTimelineInput);
        }


        [Benchmark]
        public byte[] SerializeQuestionTimelineWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(QuestionTimelineInput);
        }


        [Benchmark]
        public string SerializeReputationWithJilSerializer()
        {
            return JilSerializer.Serialize(ReputationInput);
        }


        [Benchmark]
        public string SerializeReputationWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ReputationInput);
        }


        [Benchmark]
        public byte[] SerializeReputationWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ReputationInput);
        }


        [Benchmark]
        public string SerializeReputationHistoryWithJilSerializer()
        {
            return JilSerializer.Serialize(ReputationHistoryInput);
        }


        [Benchmark]
        public string SerializeReputationHistoryWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ReputationHistoryInput);
        }


        [Benchmark]
        public byte[] SerializeReputationHistoryWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ReputationHistoryInput);
        }


        [Benchmark]
        public string SerializeRevisionWithJilSerializer()
        {
            return JilSerializer.Serialize(RevisionInput);
        }


        [Benchmark]
        public string SerializeRevisionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(RevisionInput);
        }


        [Benchmark]
        public byte[] SerializeRevisionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(RevisionInput);
        }


        [Benchmark]
        public string SerializeSearchExcerptWithJilSerializer()
        {
            return JilSerializer.Serialize(SearchExcerptInput);
        }


        [Benchmark]
        public string SerializeSearchExcerptWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(SearchExcerptInput);
        }


        [Benchmark]
        public byte[] SerializeSearchExcerptWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SearchExcerptInput);
        }


        [Benchmark]
        public string SerializeShallowUserWithJilSerializer()
        {
            return JilSerializer.Serialize(ShallowUserInput);
        }


        [Benchmark]
        public string SerializeShallowUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ShallowUserInput);
        }


        [Benchmark]
        public byte[] SerializeShallowUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ShallowUserInput);
        }


        [Benchmark]
        public string SerializeSuggestedEditWithJilSerializer()
        {
            return JilSerializer.Serialize(SuggestedEditInput);
        }


        [Benchmark]
        public string SerializeSuggestedEditWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(SuggestedEditInput);
        }


        [Benchmark]
        public byte[] SerializeSuggestedEditWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SuggestedEditInput);
        }


        [Benchmark]
        public string SerializeTagWithJilSerializer()
        {
            return JilSerializer.Serialize(TagInput);
        }


        [Benchmark]
        public string SerializeTagWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TagInput);
        }


        [Benchmark]
        public byte[] SerializeTagWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TagInput);
        }


        [Benchmark]
        public string SerializeTagScoreWithJilSerializer()
        {
            return JilSerializer.Serialize(TagScoreInput);
        }


        [Benchmark]
        public string SerializeTagScoreWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TagScoreInput);
        }


        [Benchmark]
        public byte[] SerializeTagScoreWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TagScoreInput);
        }


        [Benchmark]
        public string SerializeTagSynonymWithJilSerializer()
        {
            return JilSerializer.Serialize(TagSynonymInput);
        }


        [Benchmark]
        public string SerializeTagSynonymWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TagSynonymInput);
        }


        [Benchmark]
        public byte[] SerializeTagSynonymWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TagSynonymInput);
        }


        [Benchmark]
        public string SerializeTagWikiWithJilSerializer()
        {
            return JilSerializer.Serialize(TagWikiInput);
        }


        [Benchmark]
        public string SerializeTagWikiWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TagWikiInput);
        }


        [Benchmark]
        public byte[] SerializeTagWikiWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TagWikiInput);
        }


        [Benchmark]
        public string SerializeTopTagWithJilSerializer()
        {
            return JilSerializer.Serialize(TopTagInput);
        }


        [Benchmark]
        public string SerializeTopTagWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(TopTagInput);
        }


        [Benchmark]
        public byte[] SerializeTopTagWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(TopTagInput);
        }


        [Benchmark]
        public string SerializeUserWithJilSerializer()
        {
            return JilSerializer.Serialize(UserInput);
        }


        [Benchmark]
        public string SerializeUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UserInput);
        }


        [Benchmark]
        public byte[] SerializeUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UserInput);
        }


        [Benchmark]
        public string SerializeUserTimelineWithJilSerializer()
        {
            return JilSerializer.Serialize(UserTimelineInput);
        }


        [Benchmark]
        public string SerializeUserTimelineWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(UserTimelineInput);
        }


        [Benchmark]
        public byte[] SerializeUserTimelineWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(UserTimelineInput);
        }


        [Benchmark]
        public string SerializeWritePermissionWithJilSerializer()
        {
            return JilSerializer.Serialize(WritePermissionInput);
        }


        [Benchmark]
        public string SerializeWritePermissionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(WritePermissionInput);
        }


        [Benchmark]
        public byte[] SerializeWritePermissionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(WritePermissionInput);
        }


        [Benchmark]
        public string SerializeMobileBannerAdImageWithJilSerializer()
        {
            return JilSerializer.Serialize(MobileBannerAdImageInput);
        }


        [Benchmark]
        public string SerializeMobileBannerAdImageWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBannerAdImageInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBannerAdImageWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MobileBannerAdImageInput);
        }


        [Benchmark]
        public string SerializeSiteWithJilSerializer()
        {
            return JilSerializer.Serialize(SiteInput);
        }


        [Benchmark]
        public string SerializeSiteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(SiteInput);
        }


        [Benchmark]
        public byte[] SerializeSiteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(SiteInput);
        }


        [Benchmark]
        public string SerializeRelatedSiteWithJilSerializer()
        {
            return JilSerializer.Serialize(RelatedSiteInput);
        }


        [Benchmark]
        public string SerializeRelatedSiteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(RelatedSiteInput);
        }


        [Benchmark]
        public byte[] SerializeRelatedSiteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(RelatedSiteInput);
        }


        [Benchmark]
        public string SerializeClosedDetailsWithJilSerializer()
        {
            return JilSerializer.Serialize(ClosedDetailsInput);
        }


        [Benchmark]
        public string SerializeClosedDetailsWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(ClosedDetailsInput);
        }


        [Benchmark]
        public byte[] SerializeClosedDetailsWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(ClosedDetailsInput);
        }


        [Benchmark]
        public string SerializeNoticeWithJilSerializer()
        {
            return JilSerializer.Serialize(NoticeInput);
        }


        [Benchmark]
        public string SerializeNoticeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(NoticeInput);
        }


        [Benchmark]
        public byte[] SerializeNoticeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(NoticeInput);
        }


        [Benchmark]
        public string SerializeMigrationInfoWithJilSerializer()
        {
            return JilSerializer.Serialize(MigrationInfoInput);
        }


        [Benchmark]
        public string SerializeMigrationInfoWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(MigrationInfoInput);
        }


        [Benchmark]
        public byte[] SerializeMigrationInfoWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(MigrationInfoInput);
        }


        [Benchmark]
        public string SerializeBadgeCountWithJilSerializer()
        {
            return JilSerializer.Serialize(BadgeCountInput);
        }


        [Benchmark]
        public string SerializeBadgeCountWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(BadgeCountInput);
        }


        [Benchmark]
        public byte[] SerializeBadgeCountWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(BadgeCountInput);
        }


        [Benchmark]
        public string SerializeStylingWithJilSerializer()
        {
            return JilSerializer.Serialize(StylingInput);
        }


        [Benchmark]
        public string SerializeStylingWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(StylingInput);
        }


        [Benchmark]
        public byte[] SerializeStylingWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(StylingInput);
        }


        [Benchmark]
        public string SerializeOriginalQuestionWithJilSerializer()
        {
            return JilSerializer.Serialize(OriginalQuestionInput);
        }


        [Benchmark]
        public string SerializeOriginalQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Serialize(OriginalQuestionInput);
        }


        [Benchmark]
        public byte[] SerializeOriginalQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Serialize(OriginalQuestionInput);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithJilSerializer()
        {
            return JilSerializer.Deserialize<AccessToken>(AccessTokenOutputOfJilSerializer);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<AccessToken>(AccessTokenOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<AccessToken>(AccessTokenOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public AccountMerge DeserializeAccountMergeWithJilSerializer()
        {
            return JilSerializer.Deserialize<AccountMerge>(AccountMergeOutputOfJilSerializer);
        }

        [Benchmark]
        public AccountMerge DeserializeAccountMergeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<AccountMerge>(AccountMergeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public AccountMerge DeserializeAccountMergeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<AccountMerge>(AccountMergeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithJilSerializer()
        {
            return JilSerializer.Deserialize<Answer>(AnswerOutputOfJilSerializer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Answer>(AnswerOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Badge DeserializeBadgeWithJilSerializer()
        {
            return JilSerializer.Deserialize<Badge>(BadgeOutputOfJilSerializer);
        }

        [Benchmark]
        public Badge DeserializeBadgeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Badge>(BadgeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Badge DeserializeBadgeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Badge>(BadgeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Comment DeserializeCommentWithJilSerializer()
        {
            return JilSerializer.Deserialize<Comment>(CommentOutputOfJilSerializer);
        }

        [Benchmark]
        public Comment DeserializeCommentWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Comment>(CommentOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Comment DeserializeCommentWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Comment>(CommentOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Error DeserializeErrorWithJilSerializer()
        {
            return JilSerializer.Deserialize<Error>(ErrorOutputOfJilSerializer);
        }

        [Benchmark]
        public Error DeserializeErrorWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Error>(ErrorOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Error DeserializeErrorWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Error>(ErrorOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Event DeserializeEventWithJilSerializer()
        {
            return JilSerializer.Deserialize<Event>(EventOutputOfJilSerializer);
        }

        [Benchmark]
        public Event DeserializeEventWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Event>(EventOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Event DeserializeEventWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Event>(EventOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileFeed DeserializeMobileFeedWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileFeed>(MobileFeedOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileFeed DeserializeMobileFeedWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileFeed>(MobileFeedOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileFeed DeserializeMobileFeedWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileFeed>(MobileFeedOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileQuestion DeserializeMobileQuestionWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileQuestion>(MobileQuestionOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileQuestion DeserializeMobileQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileQuestion>(MobileQuestionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileQuestion DeserializeMobileQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileQuestion>(MobileQuestionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileRepChange DeserializeMobileRepChangeWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileRepChange>(MobileRepChangeOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileRepChange DeserializeMobileRepChangeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileRepChange>(MobileRepChangeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileRepChange DeserializeMobileRepChangeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileRepChange>(MobileRepChangeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileInboxItem DeserializeMobileInboxItemWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileInboxItem>(MobileInboxItemOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileInboxItem DeserializeMobileInboxItemWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileInboxItem>(MobileInboxItemOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileInboxItem DeserializeMobileInboxItemWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileInboxItem>(MobileInboxItemOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileBadgeAward DeserializeMobileBadgeAwardWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileBadgeAward DeserializeMobileBadgeAwardWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileBadgeAward DeserializeMobileBadgeAwardWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobilePrivilege DeserializeMobilePrivilegeWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobilePrivilege>(MobilePrivilegeOutputOfJilSerializer);
        }

        [Benchmark]
        public MobilePrivilege DeserializeMobilePrivilegeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobilePrivilege>(MobilePrivilegeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobilePrivilege DeserializeMobilePrivilegeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobilePrivilege>(MobilePrivilegeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileCommunityBulletin>(MobileCommunityBulletinOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileCommunityBulletin>(
                MobileCommunityBulletinOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileCommunityBulletin>(
                MobileCommunityBulletinOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileAssociationBonus>(MobileAssociationBonusOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileAssociationBonus>(
                MobileAssociationBonusOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileAssociationBonus>(
                MobileAssociationBonusOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileBannerAd DeserializeMobileBannerAdWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileBannerAd>(MobileBannerAdOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileBannerAd DeserializeMobileBannerAdWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBannerAd>(MobileBannerAdOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileBannerAd DeserializeMobileBannerAdWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileBannerAd>(MobileBannerAdOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileUpdateNotice DeserializeMobileUpdateNoticeWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileUpdateNotice>(MobileUpdateNoticeOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileUpdateNotice DeserializeMobileUpdateNoticeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileUpdateNotice>(MobileUpdateNoticeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileUpdateNotice DeserializeMobileUpdateNoticeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileUpdateNotice>(MobileUpdateNoticeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public FlagOption DeserializeFlagOptionWithJilSerializer()
        {
            return JilSerializer.Deserialize<FlagOption>(FlagOptionOutputOfJilSerializer);
        }

        [Benchmark]
        public FlagOption DeserializeFlagOptionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<FlagOption>(FlagOptionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public FlagOption DeserializeFlagOptionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<FlagOption>(FlagOptionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public InboxItem DeserializeInboxItemWithJilSerializer()
        {
            return JilSerializer.Deserialize<InboxItem>(InboxItemOutputOfJilSerializer);
        }

        [Benchmark]
        public InboxItem DeserializeInboxItemWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<InboxItem>(InboxItemOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public InboxItem DeserializeInboxItemWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<InboxItem>(InboxItemOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Info DeserializeInfoWithJilSerializer()
        {
            return JilSerializer.Deserialize<Info>(InfoOutputOfJilSerializer);
        }

        [Benchmark]
        public Info DeserializeInfoWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info>(InfoOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Info DeserializeInfoWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Info>(InfoOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public NetworkUser DeserializeNetworkUserWithJilSerializer()
        {
            return JilSerializer.Deserialize<NetworkUser>(NetworkUserOutputOfJilSerializer);
        }

        [Benchmark]
        public NetworkUser DeserializeNetworkUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<NetworkUser>(NetworkUserOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public NetworkUser DeserializeNetworkUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<NetworkUser>(NetworkUserOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Notification DeserializeNotificationWithJilSerializer()
        {
            return JilSerializer.Deserialize<Notification>(NotificationOutputOfJilSerializer);
        }

        [Benchmark]
        public Notification DeserializeNotificationWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Notification>(NotificationOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Notification DeserializeNotificationWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Notification>(NotificationOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Post DeserializePostWithJilSerializer()
        {
            return JilSerializer.Deserialize<Post>(PostOutputOfJilSerializer);
        }

        [Benchmark]
        public Post DeserializePostWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Post>(PostOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Post DeserializePostWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Post>(PostOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Privilege DeserializePrivilegeWithJilSerializer()
        {
            return JilSerializer.Deserialize<Privilege>(PrivilegeOutputOfJilSerializer);
        }

        [Benchmark]
        public Privilege DeserializePrivilegeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Privilege>(PrivilegeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Privilege DeserializePrivilegeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Privilege>(PrivilegeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question DeserializeQuestionWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question>(QuestionOutputOfJilSerializer);
        }

        [Benchmark]
        public Question DeserializeQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question>(QuestionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Question DeserializeQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question>(QuestionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public QuestionTimeline DeserializeQuestionTimelineWithJilSerializer()
        {
            return JilSerializer.Deserialize<QuestionTimeline>(QuestionTimelineOutputOfJilSerializer);
        }

        [Benchmark]
        public QuestionTimeline DeserializeQuestionTimelineWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<QuestionTimeline>(QuestionTimelineOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public QuestionTimeline DeserializeQuestionTimelineWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<QuestionTimeline>(QuestionTimelineOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Reputation DeserializeReputationWithJilSerializer()
        {
            return JilSerializer.Deserialize<Reputation>(ReputationOutputOfJilSerializer);
        }

        [Benchmark]
        public Reputation DeserializeReputationWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Reputation>(ReputationOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Reputation DeserializeReputationWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Reputation>(ReputationOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public ReputationHistory DeserializeReputationHistoryWithJilSerializer()
        {
            return JilSerializer.Deserialize<ReputationHistory>(ReputationHistoryOutputOfJilSerializer);
        }

        [Benchmark]
        public ReputationHistory DeserializeReputationHistoryWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<ReputationHistory>(ReputationHistoryOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public ReputationHistory DeserializeReputationHistoryWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<ReputationHistory>(ReputationHistoryOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Revision DeserializeRevisionWithJilSerializer()
        {
            return JilSerializer.Deserialize<Revision>(RevisionOutputOfJilSerializer);
        }

        [Benchmark]
        public Revision DeserializeRevisionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Revision>(RevisionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Revision DeserializeRevisionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Revision>(RevisionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public SearchExcerpt DeserializeSearchExcerptWithJilSerializer()
        {
            return JilSerializer.Deserialize<SearchExcerpt>(SearchExcerptOutputOfJilSerializer);
        }

        [Benchmark]
        public SearchExcerpt DeserializeSearchExcerptWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<SearchExcerpt>(SearchExcerptOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public SearchExcerpt DeserializeSearchExcerptWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<SearchExcerpt>(SearchExcerptOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public ShallowUser DeserializeShallowUserWithJilSerializer()
        {
            return JilSerializer.Deserialize<ShallowUser>(ShallowUserOutputOfJilSerializer);
        }

        [Benchmark]
        public ShallowUser DeserializeShallowUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<ShallowUser>(ShallowUserOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public ShallowUser DeserializeShallowUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<ShallowUser>(ShallowUserOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public SuggestedEdit DeserializeSuggestedEditWithJilSerializer()
        {
            return JilSerializer.Deserialize<SuggestedEdit>(SuggestedEditOutputOfJilSerializer);
        }

        [Benchmark]
        public SuggestedEdit DeserializeSuggestedEditWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<SuggestedEdit>(SuggestedEditOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public SuggestedEdit DeserializeSuggestedEditWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<SuggestedEdit>(SuggestedEditOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Tag DeserializeTagWithJilSerializer()
        {
            return JilSerializer.Deserialize<Tag>(TagOutputOfJilSerializer);
        }

        [Benchmark]
        public Tag DeserializeTagWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Tag>(TagOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Tag DeserializeTagWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Tag>(TagOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public TagScore DeserializeTagScoreWithJilSerializer()
        {
            return JilSerializer.Deserialize<TagScore>(TagScoreOutputOfJilSerializer);
        }

        [Benchmark]
        public TagScore DeserializeTagScoreWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagScore>(TagScoreOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public TagScore DeserializeTagScoreWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<TagScore>(TagScoreOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public TagSynonym DeserializeTagSynonymWithJilSerializer()
        {
            return JilSerializer.Deserialize<TagSynonym>(TagSynonymOutputOfJilSerializer);
        }

        [Benchmark]
        public TagSynonym DeserializeTagSynonymWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagSynonym>(TagSynonymOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public TagSynonym DeserializeTagSynonymWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<TagSynonym>(TagSynonymOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public TagWiki DeserializeTagWikiWithJilSerializer()
        {
            return JilSerializer.Deserialize<TagWiki>(TagWikiOutputOfJilSerializer);
        }

        [Benchmark]
        public TagWiki DeserializeTagWikiWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagWiki>(TagWikiOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public TagWiki DeserializeTagWikiWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<TagWiki>(TagWikiOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public TopTag DeserializeTopTagWithJilSerializer()
        {
            return JilSerializer.Deserialize<TopTag>(TopTagOutputOfJilSerializer);
        }

        [Benchmark]
        public TopTag DeserializeTopTagWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<TopTag>(TopTagOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public TopTag DeserializeTopTagWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<TopTag>(TopTagOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public User DeserializeUserWithJilSerializer()
        {
            return JilSerializer.Deserialize<User>(UserOutputOfJilSerializer);
        }

        [Benchmark]
        public User DeserializeUserWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<User>(UserOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public User DeserializeUserWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<User>(UserOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public UserTimeline DeserializeUserTimelineWithJilSerializer()
        {
            return JilSerializer.Deserialize<UserTimeline>(UserTimelineOutputOfJilSerializer);
        }

        [Benchmark]
        public UserTimeline DeserializeUserTimelineWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<UserTimeline>(UserTimelineOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public UserTimeline DeserializeUserTimelineWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<UserTimeline>(UserTimelineOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public WritePermission DeserializeWritePermissionWithJilSerializer()
        {
            return JilSerializer.Deserialize<WritePermission>(WritePermissionOutputOfJilSerializer);
        }

        [Benchmark]
        public WritePermission DeserializeWritePermissionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<WritePermission>(WritePermissionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public WritePermission DeserializeWritePermissionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<WritePermission>(WritePermissionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(
                MobileBannerAdImageOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(
                MobileBannerAdImageOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(
                MobileBannerAdImageOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithJilSerializer()
        {
            return JilSerializer.Deserialize<Info.Site>(SiteOutputOfJilSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.Site>(SiteOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Info.Site>(SiteOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Info.RelatedSite DeserializeRelatedSiteWithJilSerializer()
        {
            return JilSerializer.Deserialize<Info.RelatedSite>(RelatedSiteOutputOfJilSerializer);
        }

        [Benchmark]
        public Info.RelatedSite DeserializeRelatedSiteWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.RelatedSite>(RelatedSiteOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Info.RelatedSite DeserializeRelatedSiteWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Info.RelatedSite>(RelatedSiteOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails DeserializeClosedDetailsWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question.ClosedDetails>(ClosedDetailsOutputOfJilSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails DeserializeClosedDetailsWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.ClosedDetails>(ClosedDetailsOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails DeserializeClosedDetailsWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question.ClosedDetails>(ClosedDetailsOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question.Notice DeserializeNoticeWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question.Notice>(NoticeOutputOfJilSerializer);
        }

        [Benchmark]
        public Question.Notice DeserializeNoticeWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.Notice>(NoticeOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Question.Notice DeserializeNoticeWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question.Notice>(NoticeOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question.MigrationInfo DeserializeMigrationInfoWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question.MigrationInfo>(MigrationInfoOutputOfJilSerializer);
        }

        [Benchmark]
        public Question.MigrationInfo DeserializeMigrationInfoWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.MigrationInfo>(MigrationInfoOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Question.MigrationInfo DeserializeMigrationInfoWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question.MigrationInfo>(MigrationInfoOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public User.BadgeCount DeserializeBadgeCountWithJilSerializer()
        {
            return JilSerializer.Deserialize<User.BadgeCount>(BadgeCountOutputOfJilSerializer);
        }

        [Benchmark]
        public User.BadgeCount DeserializeBadgeCountWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<User.BadgeCount>(BadgeCountOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public User.BadgeCount DeserializeBadgeCountWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<User.BadgeCount>(BadgeCountOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Info.Site.Styling DeserializeStylingWithJilSerializer()
        {
            return JilSerializer.Deserialize<Info.Site.Styling>(StylingOutputOfJilSerializer);
        }

        [Benchmark]
        public Info.Site.Styling DeserializeStylingWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.Site.Styling>(StylingOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Info.Site.Styling DeserializeStylingWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Info.Site.Styling>(StylingOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(
                OriginalQuestionOutputOfJilSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithJsonSpanSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(
                OriginalQuestionOutputOfJsonSpanSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(
                OriginalQuestionOutputOfUtf8JsonSerializer);
        }
    }
}