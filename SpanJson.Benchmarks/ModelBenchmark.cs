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


        private static readonly JilSerializer JilSerializer = new JilSerializer();
        private static readonly string AccessTokenOutputOfJilSerializer = JilSerializer.Serialize(AccessTokenInput);

        private static readonly SpanJsonSerializer SpanJsonSerializer = new SpanJsonSerializer();
        private static readonly string AccessTokenOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AccessTokenInput);

        private static readonly SpanJsonUtf8Serializer SpanJsonUtf8Serializer = new SpanJsonUtf8Serializer();
        private static readonly byte[] AccessTokenOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AccessTokenInput);

        private static readonly Utf8JsonSerializer Utf8JsonSerializer = new Utf8JsonSerializer();
        private static readonly byte[] AccessTokenOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AccessTokenInput);

        private static readonly AccountMerge AccountMergeInput = ExpressionTreeFixture.Create<AccountMerge>();

        private static readonly string AccountMergeOutputOfJilSerializer = JilSerializer.Serialize(AccountMergeInput);

        private static readonly string AccountMergeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AccountMergeInput);

        private static readonly byte[] AccountMergeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AccountMergeInput);

        private static readonly byte[] AccountMergeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AccountMergeInput);

        private static readonly Answer AnswerInput = ExpressionTreeFixture.Create<Answer>();

        private static readonly string AnswerOutputOfJilSerializer = JilSerializer.Serialize(AnswerInput);

        private static readonly string AnswerOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AnswerInput);

        private static readonly byte[] AnswerOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AnswerInput);

        private static readonly byte[] AnswerOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AnswerInput);

        private static readonly User.BadgeCount BadgeCountInput = ExpressionTreeFixture.Create<User.BadgeCount>();

        private static readonly string BadgeCountOutputOfJilSerializer = JilSerializer.Serialize(BadgeCountInput);

        private static readonly string BadgeCountOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(BadgeCountInput);

        private static readonly byte[] BadgeCountOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(BadgeCountInput);

        private static readonly byte[] BadgeCountOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BadgeCountInput);

        private static readonly Badge BadgeInput = ExpressionTreeFixture.Create<Badge>();

        private static readonly string BadgeOutputOfJilSerializer = JilSerializer.Serialize(BadgeInput);

        private static readonly string BadgeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(BadgeInput);

        private static readonly byte[] BadgeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(BadgeInput);

        private static readonly byte[] BadgeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BadgeInput);

        private static readonly Question.ClosedDetails ClosedDetailsInput = ExpressionTreeFixture.Create<Question.ClosedDetails>();

        private static readonly string ClosedDetailsOutputOfJilSerializer = JilSerializer.Serialize(ClosedDetailsInput);

        private static readonly string ClosedDetailsOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ClosedDetailsInput);

        private static readonly byte[] ClosedDetailsOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ClosedDetailsInput);

        private static readonly byte[] ClosedDetailsOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ClosedDetailsInput);

        private static readonly Comment CommentInput = ExpressionTreeFixture.Create<Comment>();

        private static readonly string CommentOutputOfJilSerializer = JilSerializer.Serialize(CommentInput);

        private static readonly string CommentOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(CommentInput);

        private static readonly byte[] CommentOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(CommentInput);

        private static readonly byte[] CommentOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(CommentInput);

        private static readonly Error ErrorInput = ExpressionTreeFixture.Create<Error>();

        private static readonly string ErrorOutputOfJilSerializer = JilSerializer.Serialize(ErrorInput);

        private static readonly string ErrorOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ErrorInput);

        private static readonly byte[] ErrorOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ErrorInput);

        private static readonly byte[] ErrorOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ErrorInput);

        private static readonly Event EventInput = ExpressionTreeFixture.Create<Event>();

        private static readonly string EventOutputOfJilSerializer = JilSerializer.Serialize(EventInput);

        private static readonly string EventOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(EventInput);

        private static readonly byte[] EventOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(EventInput);

        private static readonly byte[] EventOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(EventInput);


        private static readonly FlagOption FlagOptionInput = ExpressionTreeFixture.Create<FlagOption>();

        private static readonly string FlagOptionOutputOfJilSerializer = JilSerializer.Serialize(FlagOptionInput);

        private static readonly string FlagOptionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(FlagOptionInput);

        private static readonly byte[] FlagOptionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(FlagOptionInput);

        private static readonly byte[] FlagOptionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(FlagOptionInput);

        private static readonly InboxItem InboxItemInput = ExpressionTreeFixture.Create<InboxItem>();

        private static readonly string InboxItemOutputOfJilSerializer = JilSerializer.Serialize(InboxItemInput);

        private static readonly string InboxItemOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(InboxItemInput);

        private static readonly byte[] InboxItemOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(InboxItemInput);

        private static readonly byte[] InboxItemOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(InboxItemInput);

        private static readonly Info InfoInput = ExpressionTreeFixture.Create<Info>();

        private static readonly string InfoOutputOfJilSerializer = JilSerializer.Serialize(InfoInput);

        private static readonly string InfoOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(InfoInput);

        private static readonly byte[] InfoOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(InfoInput);

        private static readonly byte[] InfoOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(InfoInput);


        private static readonly Question.MigrationInfo MigrationInfoInput = ExpressionTreeFixture.Create<Question.MigrationInfo>();

        private static readonly string MigrationInfoOutputOfJilSerializer = JilSerializer.Serialize(MigrationInfoInput);

        private static readonly string MigrationInfoOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MigrationInfoInput);

        private static readonly byte[] MigrationInfoOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MigrationInfoInput);

        private static readonly byte[] MigrationInfoOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MigrationInfoInput);

        private static readonly MobileAssociationBonus MobileAssociationBonusInput = ExpressionTreeFixture.Create<MobileAssociationBonus>();

        private static readonly string MobileAssociationBonusOutputOfJilSerializer = JilSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly string MobileAssociationBonusOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly byte[] MobileAssociationBonusOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileAssociationBonusInput);

        private static readonly byte[] MobileAssociationBonusOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileAssociationBonusInput);

        private static readonly MobileBadgeAward MobileBadgeAwardInput = ExpressionTreeFixture.Create<MobileBadgeAward>();

        private static readonly string MobileBadgeAwardOutputOfJilSerializer = JilSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly string MobileBadgeAwardOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly byte[] MobileBadgeAwardOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBadgeAwardInput);

        private static readonly byte[] MobileBadgeAwardOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBadgeAwardInput);

        private static readonly MobileBannerAd.MobileBannerAdImage
            MobileBannerAdImageInput = ExpressionTreeFixture.Create<MobileBannerAd.MobileBannerAdImage>();

        private static readonly string MobileBannerAdImageOutputOfJilSerializer = JilSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly string MobileBannerAdImageOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly byte[] MobileBannerAdImageOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBannerAdImageInput);

        private static readonly byte[] MobileBannerAdImageOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBannerAdImageInput);

        private static readonly MobileBannerAd MobileBannerAdInput = ExpressionTreeFixture.Create<MobileBannerAd>();

        private static readonly string MobileBannerAdOutputOfJilSerializer = JilSerializer.Serialize(MobileBannerAdInput);

        private static readonly string MobileBannerAdOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBannerAdInput);

        private static readonly byte[] MobileBannerAdOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBannerAdInput);

        private static readonly byte[] MobileBannerAdOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBannerAdInput);

        private static readonly MobileCareersJobAd MobileCareersJobAdInput = ExpressionTreeFixture.Create<MobileCareersJobAd>();

        private static readonly string MobileCareersJobAdOutputOfJilSerializer = JilSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly string MobileCareersJobAdOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly byte[] MobileCareersJobAdOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileCareersJobAdInput);

        private static readonly byte[] MobileCareersJobAdOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileCareersJobAdInput);

        private static readonly MobileCommunityBulletin MobileCommunityBulletinInput = ExpressionTreeFixture.Create<MobileCommunityBulletin>();

        private static readonly string MobileCommunityBulletinOutputOfJilSerializer = JilSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly string MobileCommunityBulletinOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly byte[] MobileCommunityBulletinOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileCommunityBulletinInput);

        private static readonly byte[] MobileCommunityBulletinOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileCommunityBulletinInput);

        private static readonly MobileFeed MobileFeedInput = ExpressionTreeFixture.Create<MobileFeed>();

        private static readonly string MobileFeedOutputOfJilSerializer = JilSerializer.Serialize(MobileFeedInput);

        private static readonly string MobileFeedOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileFeedInput);

        private static readonly byte[] MobileFeedOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileFeedInput);

        private static readonly byte[] MobileFeedOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileFeedInput);

        private static readonly MobileInboxItem MobileInboxItemInput = ExpressionTreeFixture.Create<MobileInboxItem>();

        private static readonly string MobileInboxItemOutputOfJilSerializer = JilSerializer.Serialize(MobileInboxItemInput);

        private static readonly string MobileInboxItemOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileInboxItemInput);

        private static readonly byte[] MobileInboxItemOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileInboxItemInput);

        private static readonly byte[] MobileInboxItemOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileInboxItemInput);

        private static readonly MobilePrivilege MobilePrivilegeInput = ExpressionTreeFixture.Create<MobilePrivilege>();

        private static readonly string MobilePrivilegeOutputOfJilSerializer = JilSerializer.Serialize(MobilePrivilegeInput);

        private static readonly string MobilePrivilegeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobilePrivilegeInput);

        private static readonly byte[] MobilePrivilegeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobilePrivilegeInput);

        private static readonly byte[] MobilePrivilegeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobilePrivilegeInput);

        private static readonly MobileQuestion MobileQuestionInput = ExpressionTreeFixture.Create<MobileQuestion>();

        private static readonly string MobileQuestionOutputOfJilSerializer = JilSerializer.Serialize(MobileQuestionInput);

        private static readonly string MobileQuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileQuestionInput);

        private static readonly byte[] MobileQuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileQuestionInput);

        private static readonly byte[] MobileQuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileQuestionInput);

        private static readonly MobileRepChange MobileRepChangeInput = ExpressionTreeFixture.Create<MobileRepChange>();

        private static readonly string MobileRepChangeOutputOfJilSerializer = JilSerializer.Serialize(MobileRepChangeInput);

        private static readonly string MobileRepChangeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileRepChangeInput);

        private static readonly byte[] MobileRepChangeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileRepChangeInput);

        private static readonly byte[] MobileRepChangeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileRepChangeInput);

        private static readonly MobileUpdateNotice MobileUpdateNoticeInput = ExpressionTreeFixture.Create<MobileUpdateNotice>();

        private static readonly string MobileUpdateNoticeOutputOfJilSerializer = JilSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly string MobileUpdateNoticeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly byte[] MobileUpdateNoticeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileUpdateNoticeInput);

        private static readonly byte[] MobileUpdateNoticeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileUpdateNoticeInput);

        private static readonly NetworkUser NetworkUserInput = ExpressionTreeFixture.Create<NetworkUser>();

        private static readonly string NetworkUserOutputOfJilSerializer = JilSerializer.Serialize(NetworkUserInput);

        private static readonly string NetworkUserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NetworkUserInput);

        private static readonly byte[] NetworkUserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NetworkUserInput);

        private static readonly byte[] NetworkUserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NetworkUserInput);

        private static readonly Question.Notice NoticeInput = ExpressionTreeFixture.Create<Question.Notice>();

        private static readonly string NoticeOutputOfJilSerializer = JilSerializer.Serialize(NoticeInput);

        private static readonly string NoticeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NoticeInput);

        private static readonly byte[] NoticeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NoticeInput);

        private static readonly byte[] NoticeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NoticeInput);

        private static readonly Notification NotificationInput = ExpressionTreeFixture.Create<Notification>();

        private static readonly string NotificationOutputOfJilSerializer = JilSerializer.Serialize(NotificationInput);

        private static readonly string NotificationOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NotificationInput);

        private static readonly byte[] NotificationOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NotificationInput);

        private static readonly byte[] NotificationOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NotificationInput);

        private static readonly Question.ClosedDetails.OriginalQuestion OriginalQuestionInput =
            ExpressionTreeFixture.Create<Question.ClosedDetails.OriginalQuestion>();

        private static readonly string OriginalQuestionOutputOfJilSerializer = JilSerializer.Serialize(OriginalQuestionInput);

        private static readonly string OriginalQuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(OriginalQuestionInput);

        private static readonly byte[] OriginalQuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(OriginalQuestionInput);

        private static readonly byte[] OriginalQuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(OriginalQuestionInput);

        private static readonly Post PostInput = ExpressionTreeFixture.Create<Post>();

        private static readonly string PostOutputOfJilSerializer = JilSerializer.Serialize(PostInput);

        private static readonly string PostOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(PostInput);

        private static readonly byte[] PostOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(PostInput);

        private static readonly byte[] PostOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(PostInput);

        private static readonly Privilege PrivilegeInput = ExpressionTreeFixture.Create<Privilege>();

        private static readonly string PrivilegeOutputOfJilSerializer = JilSerializer.Serialize(PrivilegeInput);

        private static readonly string PrivilegeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(PrivilegeInput);

        private static readonly byte[] PrivilegeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(PrivilegeInput);

        private static readonly byte[] PrivilegeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(PrivilegeInput);

        private static readonly Question QuestionInput = ExpressionTreeFixture.Create<Question>();

        private static readonly string QuestionOutputOfJilSerializer = JilSerializer.Serialize(QuestionInput);

        private static readonly string QuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(QuestionInput);

        private static readonly byte[] QuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(QuestionInput);

        private static readonly byte[] QuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(QuestionInput);

        private static readonly QuestionTimeline QuestionTimelineInput = ExpressionTreeFixture.Create<QuestionTimeline>();

        private static readonly string QuestionTimelineOutputOfJilSerializer = JilSerializer.Serialize(QuestionTimelineInput);

        private static readonly string QuestionTimelineOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(QuestionTimelineInput);

        private static readonly byte[] QuestionTimelineOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(QuestionTimelineInput);

        private static readonly byte[] QuestionTimelineOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(QuestionTimelineInput);

        private static readonly Info.RelatedSite RelatedSiteInput = ExpressionTreeFixture.Create<Info.RelatedSite>();

        private static readonly string RelatedSiteOutputOfJilSerializer = JilSerializer.Serialize(RelatedSiteInput);

        private static readonly string RelatedSiteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(RelatedSiteInput);

        private static readonly byte[] RelatedSiteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(RelatedSiteInput);

        private static readonly byte[] RelatedSiteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(RelatedSiteInput);

        private static readonly ReputationHistory ReputationHistoryInput = ExpressionTreeFixture.Create<ReputationHistory>();

        private static readonly string ReputationHistoryOutputOfJilSerializer = JilSerializer.Serialize(ReputationHistoryInput);

        private static readonly string ReputationHistoryOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ReputationHistoryInput);

        private static readonly byte[] ReputationHistoryOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ReputationHistoryInput);

        private static readonly byte[] ReputationHistoryOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ReputationHistoryInput);

        private static readonly Reputation ReputationInput = ExpressionTreeFixture.Create<Reputation>();

        private static readonly string ReputationOutputOfJilSerializer = JilSerializer.Serialize(ReputationInput);

        private static readonly string ReputationOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ReputationInput);

        private static readonly byte[] ReputationOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ReputationInput);

        private static readonly byte[] ReputationOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ReputationInput);

        private static readonly Revision RevisionInput = ExpressionTreeFixture.Create<Revision>();

        private static readonly string RevisionOutputOfJilSerializer = JilSerializer.Serialize(RevisionInput);

        private static readonly string RevisionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(RevisionInput);

        private static readonly byte[] RevisionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(RevisionInput);

        private static readonly byte[] RevisionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(RevisionInput);

        private static readonly SearchExcerpt SearchExcerptInput = ExpressionTreeFixture.Create<SearchExcerpt>();

        private static readonly string SearchExcerptOutputOfJilSerializer = JilSerializer.Serialize(SearchExcerptInput);

        private static readonly string SearchExcerptOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SearchExcerptInput);

        private static readonly byte[] SearchExcerptOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SearchExcerptInput);

        private static readonly byte[] SearchExcerptOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SearchExcerptInput);

        private static readonly ShallowUser ShallowUserInput = ExpressionTreeFixture.Create<ShallowUser>();

        private static readonly string ShallowUserOutputOfJilSerializer = JilSerializer.Serialize(ShallowUserInput);

        private static readonly string ShallowUserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ShallowUserInput);

        private static readonly byte[] ShallowUserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ShallowUserInput);

        private static readonly byte[] ShallowUserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ShallowUserInput);

        private static readonly Info.Site SiteInput = ExpressionTreeFixture.Create<Info.Site>();

        private static readonly string SiteOutputOfJilSerializer = JilSerializer.Serialize(SiteInput);

        private static readonly string SiteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SiteInput);

        private static readonly byte[] SiteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SiteInput);

        private static readonly byte[] SiteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SiteInput);


        private static readonly Info.Site.Styling StylingInput = ExpressionTreeFixture.Create<Info.Site.Styling>();

        private static readonly string StylingOutputOfJilSerializer = JilSerializer.Serialize(StylingInput);

        private static readonly string StylingOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(StylingInput);

        private static readonly byte[] StylingOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(StylingInput);

        private static readonly byte[] StylingOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StylingInput);

        private static readonly SuggestedEdit SuggestedEditInput = ExpressionTreeFixture.Create<SuggestedEdit>();

        private static readonly string SuggestedEditOutputOfJilSerializer = JilSerializer.Serialize(SuggestedEditInput);

        private static readonly string SuggestedEditOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SuggestedEditInput);

        private static readonly byte[] SuggestedEditOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SuggestedEditInput);

        private static readonly byte[] SuggestedEditOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SuggestedEditInput);

        private static readonly Tag TagInput = ExpressionTreeFixture.Create<Tag>();

        private static readonly string TagOutputOfJilSerializer = JilSerializer.Serialize(TagInput);

        private static readonly string TagOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagInput);

        private static readonly byte[] TagOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagInput);

        private static readonly byte[] TagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagInput);

        private static readonly TagScore TagScoreInput = ExpressionTreeFixture.Create<TagScore>();

        private static readonly string TagScoreOutputOfJilSerializer = JilSerializer.Serialize(TagScoreInput);

        private static readonly string TagScoreOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagScoreInput);

        private static readonly byte[] TagScoreOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagScoreInput);

        private static readonly byte[] TagScoreOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagScoreInput);

        private static readonly TagSynonym TagSynonymInput = ExpressionTreeFixture.Create<TagSynonym>();

        private static readonly string TagSynonymOutputOfJilSerializer = JilSerializer.Serialize(TagSynonymInput);

        private static readonly string TagSynonymOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagSynonymInput);

        private static readonly byte[] TagSynonymOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagSynonymInput);

        private static readonly byte[] TagSynonymOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagSynonymInput);

        private static readonly TagWiki TagWikiInput = ExpressionTreeFixture.Create<TagWiki>();

        private static readonly string TagWikiOutputOfJilSerializer = JilSerializer.Serialize(TagWikiInput);

        private static readonly string TagWikiOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagWikiInput);

        private static readonly byte[] TagWikiOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagWikiInput);

        private static readonly byte[] TagWikiOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagWikiInput);

        private static readonly TopTag TopTagInput = ExpressionTreeFixture.Create<TopTag>();

        private static readonly string TopTagOutputOfJilSerializer = JilSerializer.Serialize(TopTagInput);

        private static readonly string TopTagOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TopTagInput);

        private static readonly byte[] TopTagOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TopTagInput);

        private static readonly byte[] TopTagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TopTagInput);

        private static readonly User UserInput = ExpressionTreeFixture.Create<User>();

        private static readonly string UserOutputOfJilSerializer = JilSerializer.Serialize(UserInput);

        private static readonly string UserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UserInput);

        private static readonly byte[] UserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UserInput);

        private static readonly byte[] UserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UserInput);

        private static readonly UserTimeline UserTimelineInput = ExpressionTreeFixture.Create<UserTimeline>();

        private static readonly string UserTimelineOutputOfJilSerializer = JilSerializer.Serialize(UserTimelineInput);

        private static readonly string UserTimelineOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UserTimelineInput);

        private static readonly byte[] UserTimelineOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UserTimelineInput);

        private static readonly byte[] UserTimelineOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UserTimelineInput);


        private static readonly WritePermission WritePermissionInput = ExpressionTreeFixture.Create<WritePermission>();

        private static readonly string WritePermissionOutputOfJilSerializer = JilSerializer.Serialize(WritePermissionInput);

        private static readonly string WritePermissionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(WritePermissionInput);

        private static readonly byte[] WritePermissionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(WritePermissionInput);

        private static readonly byte[] WritePermissionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(WritePermissionInput);


        [Benchmark]
        public string SerializeAccessTokenWithJilSerializer()
        {
            return JilSerializer.Serialize(AccessTokenInput);
        }


        [Benchmark]
        public string SerializeAccessTokenWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(AccessTokenInput);
        }


        [Benchmark]
        public byte[] SerializeAccessTokenWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(AccessTokenInput);
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
        public string SerializeAccountMergeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(AccountMergeInput);
        }


        [Benchmark]
        public byte[] SerializeAccountMergeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(AccountMergeInput);
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
        public string SerializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(AnswerInput);
        }


        [Benchmark]
        public byte[] SerializeAnswerWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(AnswerInput);
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
        public string SerializeBadgeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(BadgeInput);
        }


        [Benchmark]
        public byte[] SerializeBadgeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(BadgeInput);
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
        public string SerializeCommentWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(CommentInput);
        }


        [Benchmark]
        public byte[] SerializeCommentWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(CommentInput);
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
        public string SerializeErrorWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ErrorInput);
        }


        [Benchmark]
        public byte[] SerializeErrorWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ErrorInput);
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
        public string SerializeEventWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(EventInput);
        }


        [Benchmark]
        public byte[] SerializeEventWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(EventInput);
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
        public string SerializeMobileFeedWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileFeedInput);
        }


        [Benchmark]
        public byte[] SerializeMobileFeedWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileFeedInput);
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
        public string SerializeMobileQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileQuestionInput);
        }


        [Benchmark]
        public byte[] SerializeMobileQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileQuestionInput);
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
        public string SerializeMobileRepChangeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileRepChangeInput);
        }


        [Benchmark]
        public byte[] SerializeMobileRepChangeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileRepChangeInput);
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
        public string SerializeMobileInboxItemWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileInboxItemInput);
        }


        [Benchmark]
        public byte[] SerializeMobileInboxItemWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileInboxItemInput);
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
        public string SerializeMobileBadgeAwardWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBadgeAwardInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBadgeAwardWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileBadgeAwardInput);
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
        public string SerializeMobilePrivilegeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobilePrivilegeInput);
        }


        [Benchmark]
        public byte[] SerializeMobilePrivilegeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobilePrivilegeInput);
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
        public string SerializeMobileCommunityBulletinWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);
        }


        [Benchmark]
        public byte[] SerializeMobileCommunityBulletinWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileCommunityBulletinInput);
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
        public string SerializeMobileAssociationBonusWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileAssociationBonusInput);
        }


        [Benchmark]
        public byte[] SerializeMobileAssociationBonusWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileAssociationBonusInput);
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
        public string SerializeMobileCareersJobAdWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileCareersJobAdInput);
        }


        [Benchmark]
        public byte[] SerializeMobileCareersJobAdWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileCareersJobAdInput);
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
        public string SerializeMobileBannerAdWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBannerAdInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBannerAdWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileBannerAdInput);
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
        public string SerializeMobileUpdateNoticeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);
        }


        [Benchmark]
        public byte[] SerializeMobileUpdateNoticeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileUpdateNoticeInput);
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
        public string SerializeFlagOptionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(FlagOptionInput);
        }


        [Benchmark]
        public byte[] SerializeFlagOptionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(FlagOptionInput);
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
        public string SerializeInboxItemWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(InboxItemInput);
        }


        [Benchmark]
        public byte[] SerializeInboxItemWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(InboxItemInput);
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
        public string SerializeInfoWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(InfoInput);
        }


        [Benchmark]
        public byte[] SerializeInfoWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(InfoInput);
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
        public string SerializeNetworkUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(NetworkUserInput);
        }


        [Benchmark]
        public byte[] SerializeNetworkUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(NetworkUserInput);
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
        public string SerializeNotificationWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(NotificationInput);
        }


        [Benchmark]
        public byte[] SerializeNotificationWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(NotificationInput);
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
        public string SerializePostWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(PostInput);
        }


        [Benchmark]
        public byte[] SerializePostWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(PostInput);
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
        public string SerializePrivilegeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(PrivilegeInput);
        }


        [Benchmark]
        public byte[] SerializePrivilegeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(PrivilegeInput);
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
        public string SerializeQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(QuestionInput);
        }


        [Benchmark]
        public byte[] SerializeQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(QuestionInput);
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
        public string SerializeQuestionTimelineWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(QuestionTimelineInput);
        }


        [Benchmark]
        public byte[] SerializeQuestionTimelineWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(QuestionTimelineInput);
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
        public string SerializeReputationWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ReputationInput);
        }


        [Benchmark]
        public byte[] SerializeReputationWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ReputationInput);
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
        public string SerializeReputationHistoryWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ReputationHistoryInput);
        }


        [Benchmark]
        public byte[] SerializeReputationHistoryWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ReputationHistoryInput);
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
        public string SerializeRevisionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(RevisionInput);
        }


        [Benchmark]
        public byte[] SerializeRevisionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(RevisionInput);
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
        public string SerializeSearchExcerptWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(SearchExcerptInput);
        }


        [Benchmark]
        public byte[] SerializeSearchExcerptWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(SearchExcerptInput);
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
        public string SerializeShallowUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ShallowUserInput);
        }


        [Benchmark]
        public byte[] SerializeShallowUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ShallowUserInput);
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
        public string SerializeSuggestedEditWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(SuggestedEditInput);
        }


        [Benchmark]
        public byte[] SerializeSuggestedEditWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(SuggestedEditInput);
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
        public string SerializeTagWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TagInput);
        }


        [Benchmark]
        public byte[] SerializeTagWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TagInput);
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
        public string SerializeTagScoreWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TagScoreInput);
        }


        [Benchmark]
        public byte[] SerializeTagScoreWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TagScoreInput);
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
        public string SerializeTagSynonymWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TagSynonymInput);
        }


        [Benchmark]
        public byte[] SerializeTagSynonymWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TagSynonymInput);
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
        public string SerializeTagWikiWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TagWikiInput);
        }


        [Benchmark]
        public byte[] SerializeTagWikiWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TagWikiInput);
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
        public string SerializeTopTagWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(TopTagInput);
        }


        [Benchmark]
        public byte[] SerializeTopTagWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(TopTagInput);
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
        public string SerializeUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UserInput);
        }


        [Benchmark]
        public byte[] SerializeUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UserInput);
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
        public string SerializeUserTimelineWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(UserTimelineInput);
        }


        [Benchmark]
        public byte[] SerializeUserTimelineWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(UserTimelineInput);
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
        public string SerializeWritePermissionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(WritePermissionInput);
        }


        [Benchmark]
        public byte[] SerializeWritePermissionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(WritePermissionInput);
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
        public string SerializeMobileBannerAdImageWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MobileBannerAdImageInput);
        }


        [Benchmark]
        public byte[] SerializeMobileBannerAdImageWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MobileBannerAdImageInput);
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
        public string SerializeSiteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(SiteInput);
        }


        [Benchmark]
        public byte[] SerializeSiteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(SiteInput);
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
        public string SerializeRelatedSiteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(RelatedSiteInput);
        }


        [Benchmark]
        public byte[] SerializeRelatedSiteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(RelatedSiteInput);
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
        public string SerializeClosedDetailsWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(ClosedDetailsInput);
        }


        [Benchmark]
        public byte[] SerializeClosedDetailsWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(ClosedDetailsInput);
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
        public string SerializeNoticeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(NoticeInput);
        }


        [Benchmark]
        public byte[] SerializeNoticeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(NoticeInput);
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
        public string SerializeMigrationInfoWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(MigrationInfoInput);
        }


        [Benchmark]
        public byte[] SerializeMigrationInfoWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(MigrationInfoInput);
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
        public string SerializeBadgeCountWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(BadgeCountInput);
        }


        [Benchmark]
        public byte[] SerializeBadgeCountWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(BadgeCountInput);
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
        public string SerializeStylingWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(StylingInput);
        }


        [Benchmark]
        public byte[] SerializeStylingWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(StylingInput);
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
        public string SerializeOriginalQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Serialize(OriginalQuestionInput);
        }


        [Benchmark]
        public byte[] SerializeOriginalQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Serialize(OriginalQuestionInput);
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
        public AccessToken DeserializeAccessTokenWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<AccessToken>(AccessTokenOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public AccessToken DeserializeAccessTokenWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<AccessToken>(AccessTokenOutputOfSpanJsonUtf8Serializer);
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
        public AccountMerge DeserializeAccountMergeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<AccountMerge>(AccountMergeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public AccountMerge DeserializeAccountMergeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<AccountMerge>(AccountMergeOutputOfSpanJsonUtf8Serializer);
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
        public Answer DeserializeAnswerWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Answer>(AnswerOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Answer DeserializeAnswerWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Answer>(AnswerOutputOfSpanJsonUtf8Serializer);
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
        public Badge DeserializeBadgeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Badge>(BadgeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Badge DeserializeBadgeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Badge>(BadgeOutputOfSpanJsonUtf8Serializer);
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
        public Comment DeserializeCommentWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Comment>(CommentOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Comment DeserializeCommentWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Comment>(CommentOutputOfSpanJsonUtf8Serializer);
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
        public Error DeserializeErrorWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Error>(ErrorOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Error DeserializeErrorWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Error>(ErrorOutputOfSpanJsonUtf8Serializer);
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
        public Event DeserializeEventWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Event>(EventOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Event DeserializeEventWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Event>(EventOutputOfSpanJsonUtf8Serializer);
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
        public MobileFeed DeserializeMobileFeedWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileFeed>(MobileFeedOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileFeed DeserializeMobileFeedWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileFeed>(MobileFeedOutputOfSpanJsonUtf8Serializer);
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
        public MobileQuestion DeserializeMobileQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileQuestion>(MobileQuestionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileQuestion DeserializeMobileQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileQuestion>(MobileQuestionOutputOfSpanJsonUtf8Serializer);
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
        public MobileRepChange DeserializeMobileRepChangeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileRepChange>(MobileRepChangeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileRepChange DeserializeMobileRepChangeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileRepChange>(MobileRepChangeOutputOfSpanJsonUtf8Serializer);
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
        public MobileInboxItem DeserializeMobileInboxItemWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileInboxItem>(MobileInboxItemOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileInboxItem DeserializeMobileInboxItemWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileInboxItem>(MobileInboxItemOutputOfSpanJsonUtf8Serializer);
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
        public MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileBadgeAward>(MobileBadgeAwardOutputOfSpanJsonUtf8Serializer);
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
        public MobilePrivilege DeserializeMobilePrivilegeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobilePrivilege>(MobilePrivilegeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobilePrivilege DeserializeMobilePrivilegeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobilePrivilege>(MobilePrivilegeOutputOfSpanJsonUtf8Serializer);
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
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileCommunityBulletin>(MobileCommunityBulletinOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileCommunityBulletin>(MobileCommunityBulletinOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public MobileCommunityBulletin DeserializeMobileCommunityBulletinWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileCommunityBulletin>(MobileCommunityBulletinOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileAssociationBonus>(MobileAssociationBonusOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileAssociationBonus>(MobileAssociationBonusOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileAssociationBonus>(MobileAssociationBonusOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public MobileAssociationBonus DeserializeMobileAssociationBonusWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileAssociationBonus>(MobileAssociationBonusOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileCareersJobAd DeserializeMobileCareersJobAdWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileCareersJobAd>(MobileCareersJobAdOutputOfSpanJsonUtf8Serializer);
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
        public MobileBannerAd DeserializeMobileBannerAdWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBannerAd>(MobileBannerAdOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileBannerAd DeserializeMobileBannerAdWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileBannerAd>(MobileBannerAdOutputOfSpanJsonUtf8Serializer);
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
        public MobileUpdateNotice DeserializeMobileUpdateNoticeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileUpdateNotice>(MobileUpdateNoticeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileUpdateNotice DeserializeMobileUpdateNoticeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileUpdateNotice>(MobileUpdateNoticeOutputOfSpanJsonUtf8Serializer);
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
        public FlagOption DeserializeFlagOptionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<FlagOption>(FlagOptionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public FlagOption DeserializeFlagOptionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<FlagOption>(FlagOptionOutputOfSpanJsonUtf8Serializer);
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
        public InboxItem DeserializeInboxItemWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<InboxItem>(InboxItemOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public InboxItem DeserializeInboxItemWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<InboxItem>(InboxItemOutputOfSpanJsonUtf8Serializer);
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
        public Info DeserializeInfoWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info>(InfoOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Info DeserializeInfoWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Info>(InfoOutputOfSpanJsonUtf8Serializer);
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
        public NetworkUser DeserializeNetworkUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<NetworkUser>(NetworkUserOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public NetworkUser DeserializeNetworkUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<NetworkUser>(NetworkUserOutputOfSpanJsonUtf8Serializer);
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
        public Notification DeserializeNotificationWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Notification>(NotificationOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Notification DeserializeNotificationWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Notification>(NotificationOutputOfSpanJsonUtf8Serializer);
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
        public Post DeserializePostWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Post>(PostOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Post DeserializePostWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Post>(PostOutputOfSpanJsonUtf8Serializer);
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
        public Privilege DeserializePrivilegeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Privilege>(PrivilegeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Privilege DeserializePrivilegeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Privilege>(PrivilegeOutputOfSpanJsonUtf8Serializer);
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
        public Question DeserializeQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question>(QuestionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Question DeserializeQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Question>(QuestionOutputOfSpanJsonUtf8Serializer);
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
        public QuestionTimeline DeserializeQuestionTimelineWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<QuestionTimeline>(QuestionTimelineOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public QuestionTimeline DeserializeQuestionTimelineWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<QuestionTimeline>(QuestionTimelineOutputOfSpanJsonUtf8Serializer);
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
        public Reputation DeserializeReputationWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Reputation>(ReputationOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Reputation DeserializeReputationWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Reputation>(ReputationOutputOfSpanJsonUtf8Serializer);
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
        public ReputationHistory DeserializeReputationHistoryWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<ReputationHistory>(ReputationHistoryOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public ReputationHistory DeserializeReputationHistoryWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<ReputationHistory>(ReputationHistoryOutputOfSpanJsonUtf8Serializer);
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
        public Revision DeserializeRevisionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Revision>(RevisionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Revision DeserializeRevisionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Revision>(RevisionOutputOfSpanJsonUtf8Serializer);
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
        public SearchExcerpt DeserializeSearchExcerptWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<SearchExcerpt>(SearchExcerptOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public SearchExcerpt DeserializeSearchExcerptWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<SearchExcerpt>(SearchExcerptOutputOfSpanJsonUtf8Serializer);
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
        public ShallowUser DeserializeShallowUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<ShallowUser>(ShallowUserOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public ShallowUser DeserializeShallowUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<ShallowUser>(ShallowUserOutputOfSpanJsonUtf8Serializer);
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
        public SuggestedEdit DeserializeSuggestedEditWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<SuggestedEdit>(SuggestedEditOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public SuggestedEdit DeserializeSuggestedEditWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<SuggestedEdit>(SuggestedEditOutputOfSpanJsonUtf8Serializer);
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
        public Tag DeserializeTagWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Tag>(TagOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Tag DeserializeTagWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Tag>(TagOutputOfSpanJsonUtf8Serializer);
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
        public TagScore DeserializeTagScoreWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagScore>(TagScoreOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public TagScore DeserializeTagScoreWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<TagScore>(TagScoreOutputOfSpanJsonUtf8Serializer);
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
        public TagSynonym DeserializeTagSynonymWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagSynonym>(TagSynonymOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public TagSynonym DeserializeTagSynonymWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<TagSynonym>(TagSynonymOutputOfSpanJsonUtf8Serializer);
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
        public TagWiki DeserializeTagWikiWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<TagWiki>(TagWikiOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public TagWiki DeserializeTagWikiWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<TagWiki>(TagWikiOutputOfSpanJsonUtf8Serializer);
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
        public TopTag DeserializeTopTagWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<TopTag>(TopTagOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public TopTag DeserializeTopTagWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<TopTag>(TopTagOutputOfSpanJsonUtf8Serializer);
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
        public User DeserializeUserWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<User>(UserOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public User DeserializeUserWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<User>(UserOutputOfSpanJsonUtf8Serializer);
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
        public UserTimeline DeserializeUserTimelineWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<UserTimeline>(UserTimelineOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public UserTimeline DeserializeUserTimelineWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<UserTimeline>(UserTimelineOutputOfSpanJsonUtf8Serializer);
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
        public WritePermission DeserializeWritePermissionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<WritePermission>(WritePermissionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public WritePermission DeserializeWritePermissionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<WritePermission>(WritePermissionOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public WritePermission DeserializeWritePermissionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<WritePermission>(WritePermissionOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithJilSerializer()
        {
            return JilSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfJilSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithJilSerializer()
        {
            return JilSerializer.Deserialize<Info.Site>(SiteOutputOfJilSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.Site>(SiteOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Info.Site DeserializeSiteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Info.Site>(SiteOutputOfSpanJsonUtf8Serializer);
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
        public Info.RelatedSite DeserializeRelatedSiteWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.RelatedSite>(RelatedSiteOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Info.RelatedSite DeserializeRelatedSiteWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Info.RelatedSite>(RelatedSiteOutputOfSpanJsonUtf8Serializer);
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
        public Question.ClosedDetails DeserializeClosedDetailsWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.ClosedDetails>(ClosedDetailsOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails DeserializeClosedDetailsWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Question.ClosedDetails>(ClosedDetailsOutputOfSpanJsonUtf8Serializer);
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
        public Question.Notice DeserializeNoticeWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.Notice>(NoticeOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Question.Notice DeserializeNoticeWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Question.Notice>(NoticeOutputOfSpanJsonUtf8Serializer);
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
        public Question.MigrationInfo DeserializeMigrationInfoWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.MigrationInfo>(MigrationInfoOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Question.MigrationInfo DeserializeMigrationInfoWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Question.MigrationInfo>(MigrationInfoOutputOfSpanJsonUtf8Serializer);
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
        public User.BadgeCount DeserializeBadgeCountWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<User.BadgeCount>(BadgeCountOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public User.BadgeCount DeserializeBadgeCountWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<User.BadgeCount>(BadgeCountOutputOfSpanJsonUtf8Serializer);
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
        public Info.Site.Styling DeserializeStylingWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Info.Site.Styling>(StylingOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Info.Site.Styling DeserializeStylingWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Info.Site.Styling>(StylingOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public Info.Site.Styling DeserializeStylingWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Info.Site.Styling>(StylingOutputOfUtf8JsonSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithJilSerializer()
        {
            return JilSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfJilSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithSpanJsonSerializer()
        {
            return SpanJsonSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfSpanJsonSerializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithSpanJsonUtf8Serializer()
        {
            return SpanJsonUtf8Serializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfSpanJsonUtf8Serializer);
        }

        [Benchmark]
        public Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithUtf8JsonSerializer()
        {
            return Utf8JsonSerializer.Deserialize<Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfUtf8JsonSerializer);
        }
    }
}