using System;
using BenchmarkDotNet.Attributes;
using SpanJson.Benchmarks.Fixture;

namespace SpanJson.Benchmarks
{  
  [Config(typeof(MyConfig))]
  public partial class ModelBenchmark
  {
	private static readonly ExpressionTreeFixture ExpressionTreeFixture = new ExpressionTreeFixture();
  
	private static readonly Serializers.JilSerializer JilSerializer = new Serializers.JilSerializer();
  
	private static readonly Serializers.SpanJsonSerializer SpanJsonSerializer = new Serializers.SpanJsonSerializer();
  
	private static readonly Serializers.SpanJsonUtf8Serializer SpanJsonUtf8Serializer = new Serializers.SpanJsonUtf8Serializer();
  
	private static readonly Serializers.Utf8JsonSerializer Utf8JsonSerializer = new Serializers.Utf8JsonSerializer();
  
  
	private static readonly Models.AccessToken AccessTokenInput = ExpressionTreeFixture.Create<Models.AccessToken>();
  
	private static readonly Models.AccountMerge AccountMergeInput = ExpressionTreeFixture.Create<Models.AccountMerge>();
  
	private static readonly Models.Answer AnswerInput = ExpressionTreeFixture.Create<Models.Answer>();
  
	private static readonly Models.Badge BadgeInput = ExpressionTreeFixture.Create<Models.Badge>();
  
	private static readonly Models.Comment CommentInput = ExpressionTreeFixture.Create<Models.Comment>();
  
	private static readonly Models.Error ErrorInput = ExpressionTreeFixture.Create<Models.Error>();
  
	private static readonly Models.Event EventInput = ExpressionTreeFixture.Create<Models.Event>();
  
	private static readonly Models.MobileFeed MobileFeedInput = ExpressionTreeFixture.Create<Models.MobileFeed>();
  
	private static readonly Models.MobileQuestion MobileQuestionInput = ExpressionTreeFixture.Create<Models.MobileQuestion>();
  
	private static readonly Models.MobileRepChange MobileRepChangeInput = ExpressionTreeFixture.Create<Models.MobileRepChange>();
  
	private static readonly Models.MobileInboxItem MobileInboxItemInput = ExpressionTreeFixture.Create<Models.MobileInboxItem>();
  
	private static readonly Models.MobileBadgeAward MobileBadgeAwardInput = ExpressionTreeFixture.Create<Models.MobileBadgeAward>();
  
	private static readonly Models.MobilePrivilege MobilePrivilegeInput = ExpressionTreeFixture.Create<Models.MobilePrivilege>();
  
	private static readonly Models.MobileCommunityBulletin MobileCommunityBulletinInput = ExpressionTreeFixture.Create<Models.MobileCommunityBulletin>();
  
	private static readonly Models.MobileAssociationBonus MobileAssociationBonusInput = ExpressionTreeFixture.Create<Models.MobileAssociationBonus>();
  
	private static readonly Models.MobileCareersJobAd MobileCareersJobAdInput = ExpressionTreeFixture.Create<Models.MobileCareersJobAd>();
  
	private static readonly Models.MobileBannerAd MobileBannerAdInput = ExpressionTreeFixture.Create<Models.MobileBannerAd>();
  
	private static readonly Models.MobileUpdateNotice MobileUpdateNoticeInput = ExpressionTreeFixture.Create<Models.MobileUpdateNotice>();
  
	private static readonly Models.FlagOption FlagOptionInput = ExpressionTreeFixture.Create<Models.FlagOption>();
  
	private static readonly Models.InboxItem InboxItemInput = ExpressionTreeFixture.Create<Models.InboxItem>();
  
	private static readonly Models.Info InfoInput = ExpressionTreeFixture.Create<Models.Info>();
  
	private static readonly Models.NetworkUser NetworkUserInput = ExpressionTreeFixture.Create<Models.NetworkUser>();
  
	private static readonly Models.Notification NotificationInput = ExpressionTreeFixture.Create<Models.Notification>();
  
	private static readonly Models.Post PostInput = ExpressionTreeFixture.Create<Models.Post>();
  
	private static readonly Models.Privilege PrivilegeInput = ExpressionTreeFixture.Create<Models.Privilege>();
  
	private static readonly Models.Question QuestionInput = ExpressionTreeFixture.Create<Models.Question>();
  
	private static readonly Models.QuestionTimeline QuestionTimelineInput = ExpressionTreeFixture.Create<Models.QuestionTimeline>();
  
	private static readonly Models.Reputation ReputationInput = ExpressionTreeFixture.Create<Models.Reputation>();
  
	private static readonly Models.ReputationHistory ReputationHistoryInput = ExpressionTreeFixture.Create<Models.ReputationHistory>();
  
	private static readonly Models.Revision RevisionInput = ExpressionTreeFixture.Create<Models.Revision>();
  
	private static readonly Models.SearchExcerpt SearchExcerptInput = ExpressionTreeFixture.Create<Models.SearchExcerpt>();
  
	private static readonly Models.ShallowUser ShallowUserInput = ExpressionTreeFixture.Create<Models.ShallowUser>();
  
	private static readonly Models.SuggestedEdit SuggestedEditInput = ExpressionTreeFixture.Create<Models.SuggestedEdit>();
  
	private static readonly Models.Tag TagInput = ExpressionTreeFixture.Create<Models.Tag>();
  
	private static readonly Models.TagScore TagScoreInput = ExpressionTreeFixture.Create<Models.TagScore>();
  
	private static readonly Models.TagSynonym TagSynonymInput = ExpressionTreeFixture.Create<Models.TagSynonym>();
  
	private static readonly Models.TagWiki TagWikiInput = ExpressionTreeFixture.Create<Models.TagWiki>();
  
	private static readonly Models.TopTag TopTagInput = ExpressionTreeFixture.Create<Models.TopTag>();
  
	private static readonly Models.User UserInput = ExpressionTreeFixture.Create<Models.User>();
  
	private static readonly Models.UserTimeline UserTimelineInput = ExpressionTreeFixture.Create<Models.UserTimeline>();
  
	private static readonly Models.WritePermission WritePermissionInput = ExpressionTreeFixture.Create<Models.WritePermission>();
  
	private static readonly Models.MobileBannerAd.MobileBannerAdImage MobileBannerAdImageInput = ExpressionTreeFixture.Create<Models.MobileBannerAd.MobileBannerAdImage>();
  
	private static readonly Models.Info.Site SiteInput = ExpressionTreeFixture.Create<Models.Info.Site>();
  
	private static readonly Models.Info.RelatedSite RelatedSiteInput = ExpressionTreeFixture.Create<Models.Info.RelatedSite>();
  
	private static readonly Models.Question.ClosedDetails ClosedDetailsInput = ExpressionTreeFixture.Create<Models.Question.ClosedDetails>();
  
	private static readonly Models.Question.Notice NoticeInput = ExpressionTreeFixture.Create<Models.Question.Notice>();
  
	private static readonly Models.Question.MigrationInfo MigrationInfoInput = ExpressionTreeFixture.Create<Models.Question.MigrationInfo>();
  
	private static readonly Models.User.BadgeCount BadgeCountInput = ExpressionTreeFixture.Create<Models.User.BadgeCount>();
  
	private static readonly Models.Info.Site.Styling StylingInput = ExpressionTreeFixture.Create<Models.Info.Site.Styling>();
  
	private static readonly Models.Question.ClosedDetails.OriginalQuestion OriginalQuestionInput = ExpressionTreeFixture.Create<Models.Question.ClosedDetails.OriginalQuestion>();
  
  

	[Benchmark]
	public System.String SerializeAccessTokenWithJilSerializer()
	{
		return JilSerializer.Serialize(AccessTokenInput);
	}	
  

	[Benchmark]
	public System.String SerializeAccessTokenWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(AccessTokenInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAccessTokenWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(AccessTokenInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAccessTokenWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(AccessTokenInput);
	}	
  

	[Benchmark]
	public System.String SerializeAccountMergeWithJilSerializer()
	{
		return JilSerializer.Serialize(AccountMergeInput);
	}	
  

	[Benchmark]
	public System.String SerializeAccountMergeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(AccountMergeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAccountMergeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(AccountMergeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAccountMergeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(AccountMergeInput);
	}	
  

	[Benchmark]
	public System.String SerializeAnswerWithJilSerializer()
	{
		return JilSerializer.Serialize(AnswerInput);
	}	
  

	[Benchmark]
	public System.String SerializeAnswerWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(AnswerInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAnswerWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(AnswerInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeAnswerWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(AnswerInput);
	}	
  

	[Benchmark]
	public System.String SerializeBadgeWithJilSerializer()
	{
		return JilSerializer.Serialize(BadgeInput);
	}	
  

	[Benchmark]
	public System.String SerializeBadgeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(BadgeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeBadgeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(BadgeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeBadgeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(BadgeInput);
	}	
  

	[Benchmark]
	public System.String SerializeCommentWithJilSerializer()
	{
		return JilSerializer.Serialize(CommentInput);
	}	
  

	[Benchmark]
	public System.String SerializeCommentWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(CommentInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeCommentWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(CommentInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeCommentWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(CommentInput);
	}	
  

	[Benchmark]
	public System.String SerializeErrorWithJilSerializer()
	{
		return JilSerializer.Serialize(ErrorInput);
	}	
  

	[Benchmark]
	public System.String SerializeErrorWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(ErrorInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeErrorWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(ErrorInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeErrorWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(ErrorInput);
	}	
  

	[Benchmark]
	public System.String SerializeEventWithJilSerializer()
	{
		return JilSerializer.Serialize(EventInput);
	}	
  

	[Benchmark]
	public System.String SerializeEventWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(EventInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeEventWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(EventInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeEventWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(EventInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileFeedWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileFeedInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileFeedWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileFeedInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileFeedWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileFeedInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileFeedWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileFeedInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileQuestionWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileQuestionInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileQuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileQuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileQuestionInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileRepChangeWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileRepChangeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileRepChangeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileRepChangeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileRepChangeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileRepChangeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileRepChangeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileRepChangeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileInboxItemWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileInboxItemInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileInboxItemWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileInboxItemInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileInboxItemWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileInboxItemInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileInboxItemWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileInboxItemInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBadgeAwardWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileBadgeAwardInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBadgeAwardWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileBadgeAwardInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBadgeAwardWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileBadgeAwardInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBadgeAwardWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileBadgeAwardInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobilePrivilegeWithJilSerializer()
	{
		return JilSerializer.Serialize(MobilePrivilegeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobilePrivilegeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobilePrivilegeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobilePrivilegeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobilePrivilegeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobilePrivilegeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobilePrivilegeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileCommunityBulletinWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileCommunityBulletinInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileCommunityBulletinWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileCommunityBulletinWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileCommunityBulletinInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileCommunityBulletinWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileCommunityBulletinInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileAssociationBonusWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileAssociationBonusInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileAssociationBonusWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileAssociationBonusInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileAssociationBonusWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileAssociationBonusInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileAssociationBonusWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileAssociationBonusInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileCareersJobAdWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileCareersJobAdInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileCareersJobAdWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileCareersJobAdInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileCareersJobAdWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileCareersJobAdInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileCareersJobAdWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileCareersJobAdInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBannerAdWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileBannerAdInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBannerAdWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileBannerAdInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBannerAdWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileBannerAdInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBannerAdWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileBannerAdInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileUpdateNoticeWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileUpdateNoticeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileUpdateNoticeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileUpdateNoticeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileUpdateNoticeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileUpdateNoticeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileUpdateNoticeInput);
	}	
  

	[Benchmark]
	public System.String SerializeFlagOptionWithJilSerializer()
	{
		return JilSerializer.Serialize(FlagOptionInput);
	}	
  

	[Benchmark]
	public System.String SerializeFlagOptionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(FlagOptionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeFlagOptionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(FlagOptionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeFlagOptionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(FlagOptionInput);
	}	
  

	[Benchmark]
	public System.String SerializeInboxItemWithJilSerializer()
	{
		return JilSerializer.Serialize(InboxItemInput);
	}	
  

	[Benchmark]
	public System.String SerializeInboxItemWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(InboxItemInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeInboxItemWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(InboxItemInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeInboxItemWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(InboxItemInput);
	}	
  

	[Benchmark]
	public System.String SerializeInfoWithJilSerializer()
	{
		return JilSerializer.Serialize(InfoInput);
	}	
  

	[Benchmark]
	public System.String SerializeInfoWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(InfoInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeInfoWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(InfoInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeInfoWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(InfoInput);
	}	
  

	[Benchmark]
	public System.String SerializeNetworkUserWithJilSerializer()
	{
		return JilSerializer.Serialize(NetworkUserInput);
	}	
  

	[Benchmark]
	public System.String SerializeNetworkUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(NetworkUserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNetworkUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(NetworkUserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNetworkUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(NetworkUserInput);
	}	
  

	[Benchmark]
	public System.String SerializeNotificationWithJilSerializer()
	{
		return JilSerializer.Serialize(NotificationInput);
	}	
  

	[Benchmark]
	public System.String SerializeNotificationWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(NotificationInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNotificationWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(NotificationInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNotificationWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(NotificationInput);
	}	
  

	[Benchmark]
	public System.String SerializePostWithJilSerializer()
	{
		return JilSerializer.Serialize(PostInput);
	}	
  

	[Benchmark]
	public System.String SerializePostWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(PostInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializePostWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(PostInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializePostWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(PostInput);
	}	
  

	[Benchmark]
	public System.String SerializePrivilegeWithJilSerializer()
	{
		return JilSerializer.Serialize(PrivilegeInput);
	}	
  

	[Benchmark]
	public System.String SerializePrivilegeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(PrivilegeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializePrivilegeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(PrivilegeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializePrivilegeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(PrivilegeInput);
	}	
  

	[Benchmark]
	public System.String SerializeQuestionWithJilSerializer()
	{
		return JilSerializer.Serialize(QuestionInput);
	}	
  

	[Benchmark]
	public System.String SerializeQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(QuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(QuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(QuestionInput);
	}	
  

	[Benchmark]
	public System.String SerializeQuestionTimelineWithJilSerializer()
	{
		return JilSerializer.Serialize(QuestionTimelineInput);
	}	
  

	[Benchmark]
	public System.String SerializeQuestionTimelineWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(QuestionTimelineInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeQuestionTimelineWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(QuestionTimelineInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeQuestionTimelineWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(QuestionTimelineInput);
	}	
  

	[Benchmark]
	public System.String SerializeReputationWithJilSerializer()
	{
		return JilSerializer.Serialize(ReputationInput);
	}	
  

	[Benchmark]
	public System.String SerializeReputationWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(ReputationInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeReputationWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(ReputationInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeReputationWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(ReputationInput);
	}	
  

	[Benchmark]
	public System.String SerializeReputationHistoryWithJilSerializer()
	{
		return JilSerializer.Serialize(ReputationHistoryInput);
	}	
  

	[Benchmark]
	public System.String SerializeReputationHistoryWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(ReputationHistoryInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeReputationHistoryWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(ReputationHistoryInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeReputationHistoryWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(ReputationHistoryInput);
	}	
  

	[Benchmark]
	public System.String SerializeRevisionWithJilSerializer()
	{
		return JilSerializer.Serialize(RevisionInput);
	}	
  

	[Benchmark]
	public System.String SerializeRevisionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(RevisionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeRevisionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(RevisionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeRevisionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(RevisionInput);
	}	
  

	[Benchmark]
	public System.String SerializeSearchExcerptWithJilSerializer()
	{
		return JilSerializer.Serialize(SearchExcerptInput);
	}	
  

	[Benchmark]
	public System.String SerializeSearchExcerptWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(SearchExcerptInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSearchExcerptWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(SearchExcerptInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSearchExcerptWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(SearchExcerptInput);
	}	
  

	[Benchmark]
	public System.String SerializeShallowUserWithJilSerializer()
	{
		return JilSerializer.Serialize(ShallowUserInput);
	}	
  

	[Benchmark]
	public System.String SerializeShallowUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(ShallowUserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeShallowUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(ShallowUserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeShallowUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(ShallowUserInput);
	}	
  

	[Benchmark]
	public System.String SerializeSuggestedEditWithJilSerializer()
	{
		return JilSerializer.Serialize(SuggestedEditInput);
	}	
  

	[Benchmark]
	public System.String SerializeSuggestedEditWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(SuggestedEditInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSuggestedEditWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(SuggestedEditInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSuggestedEditWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(SuggestedEditInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagWithJilSerializer()
	{
		return JilSerializer.Serialize(TagInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(TagInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(TagInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(TagInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagScoreWithJilSerializer()
	{
		return JilSerializer.Serialize(TagScoreInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagScoreWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(TagScoreInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagScoreWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(TagScoreInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagScoreWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(TagScoreInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagSynonymWithJilSerializer()
	{
		return JilSerializer.Serialize(TagSynonymInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagSynonymWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(TagSynonymInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagSynonymWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(TagSynonymInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagSynonymWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(TagSynonymInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagWikiWithJilSerializer()
	{
		return JilSerializer.Serialize(TagWikiInput);
	}	
  

	[Benchmark]
	public System.String SerializeTagWikiWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(TagWikiInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagWikiWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(TagWikiInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTagWikiWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(TagWikiInput);
	}	
  

	[Benchmark]
	public System.String SerializeTopTagWithJilSerializer()
	{
		return JilSerializer.Serialize(TopTagInput);
	}	
  

	[Benchmark]
	public System.String SerializeTopTagWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(TopTagInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTopTagWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(TopTagInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeTopTagWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(TopTagInput);
	}	
  

	[Benchmark]
	public System.String SerializeUserWithJilSerializer()
	{
		return JilSerializer.Serialize(UserInput);
	}	
  

	[Benchmark]
	public System.String SerializeUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(UserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(UserInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(UserInput);
	}	
  

	[Benchmark]
	public System.String SerializeUserTimelineWithJilSerializer()
	{
		return JilSerializer.Serialize(UserTimelineInput);
	}	
  

	[Benchmark]
	public System.String SerializeUserTimelineWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(UserTimelineInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeUserTimelineWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(UserTimelineInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeUserTimelineWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(UserTimelineInput);
	}	
  

	[Benchmark]
	public System.String SerializeWritePermissionWithJilSerializer()
	{
		return JilSerializer.Serialize(WritePermissionInput);
	}	
  

	[Benchmark]
	public System.String SerializeWritePermissionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(WritePermissionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeWritePermissionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(WritePermissionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeWritePermissionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(WritePermissionInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBannerAdImageWithJilSerializer()
	{
		return JilSerializer.Serialize(MobileBannerAdImageInput);
	}	
  

	[Benchmark]
	public System.String SerializeMobileBannerAdImageWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MobileBannerAdImageInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBannerAdImageWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MobileBannerAdImageInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMobileBannerAdImageWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MobileBannerAdImageInput);
	}	
  

	[Benchmark]
	public System.String SerializeSiteWithJilSerializer()
	{
		return JilSerializer.Serialize(SiteInput);
	}	
  

	[Benchmark]
	public System.String SerializeSiteWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(SiteInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSiteWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(SiteInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeSiteWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(SiteInput);
	}	
  

	[Benchmark]
	public System.String SerializeRelatedSiteWithJilSerializer()
	{
		return JilSerializer.Serialize(RelatedSiteInput);
	}	
  

	[Benchmark]
	public System.String SerializeRelatedSiteWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(RelatedSiteInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeRelatedSiteWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(RelatedSiteInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeRelatedSiteWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(RelatedSiteInput);
	}	
  

	[Benchmark]
	public System.String SerializeClosedDetailsWithJilSerializer()
	{
		return JilSerializer.Serialize(ClosedDetailsInput);
	}	
  

	[Benchmark]
	public System.String SerializeClosedDetailsWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(ClosedDetailsInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeClosedDetailsWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(ClosedDetailsInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeClosedDetailsWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(ClosedDetailsInput);
	}	
  

	[Benchmark]
	public System.String SerializeNoticeWithJilSerializer()
	{
		return JilSerializer.Serialize(NoticeInput);
	}	
  

	[Benchmark]
	public System.String SerializeNoticeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(NoticeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNoticeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(NoticeInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeNoticeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(NoticeInput);
	}	
  

	[Benchmark]
	public System.String SerializeMigrationInfoWithJilSerializer()
	{
		return JilSerializer.Serialize(MigrationInfoInput);
	}	
  

	[Benchmark]
	public System.String SerializeMigrationInfoWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(MigrationInfoInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMigrationInfoWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(MigrationInfoInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeMigrationInfoWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(MigrationInfoInput);
	}	
  

	[Benchmark]
	public System.String SerializeBadgeCountWithJilSerializer()
	{
		return JilSerializer.Serialize(BadgeCountInput);
	}	
  

	[Benchmark]
	public System.String SerializeBadgeCountWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(BadgeCountInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeBadgeCountWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(BadgeCountInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeBadgeCountWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(BadgeCountInput);
	}	
  

	[Benchmark]
	public System.String SerializeStylingWithJilSerializer()
	{
		return JilSerializer.Serialize(StylingInput);
	}	
  

	[Benchmark]
	public System.String SerializeStylingWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(StylingInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeStylingWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(StylingInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeStylingWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(StylingInput);
	}	
  

	[Benchmark]
	public System.String SerializeOriginalQuestionWithJilSerializer()
	{
		return JilSerializer.Serialize(OriginalQuestionInput);
	}	
  

	[Benchmark]
	public System.String SerializeOriginalQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Serialize(OriginalQuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeOriginalQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Serialize(OriginalQuestionInput);
	}	
  

	[Benchmark]
	public System.Byte[] SerializeOriginalQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Serialize(OriginalQuestionInput);
	}	
 
  
	private static readonly String AccessTokenOutputOfJilSerializer = JilSerializer.Serialize(AccessTokenInput);
	[Benchmark]
	public Models.AccessToken DeserializeAccessTokenWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.AccessToken>(AccessTokenOutputOfJilSerializer);
	}
  
	private static readonly String AccessTokenOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AccessTokenInput);
	[Benchmark]
	public Models.AccessToken DeserializeAccessTokenWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.AccessToken>(AccessTokenOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] AccessTokenOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AccessTokenInput);
	[Benchmark]
	public Models.AccessToken DeserializeAccessTokenWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.AccessToken>(AccessTokenOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] AccessTokenOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AccessTokenInput);
	[Benchmark]
	public Models.AccessToken DeserializeAccessTokenWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.AccessToken>(AccessTokenOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String AccountMergeOutputOfJilSerializer = JilSerializer.Serialize(AccountMergeInput);
	[Benchmark]
	public Models.AccountMerge DeserializeAccountMergeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.AccountMerge>(AccountMergeOutputOfJilSerializer);
	}
  
	private static readonly String AccountMergeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AccountMergeInput);
	[Benchmark]
	public Models.AccountMerge DeserializeAccountMergeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.AccountMerge>(AccountMergeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] AccountMergeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AccountMergeInput);
	[Benchmark]
	public Models.AccountMerge DeserializeAccountMergeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.AccountMerge>(AccountMergeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] AccountMergeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AccountMergeInput);
	[Benchmark]
	public Models.AccountMerge DeserializeAccountMergeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.AccountMerge>(AccountMergeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String AnswerOutputOfJilSerializer = JilSerializer.Serialize(AnswerInput);
	[Benchmark]
	public Models.Answer DeserializeAnswerWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Answer>(AnswerOutputOfJilSerializer);
	}
  
	private static readonly String AnswerOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(AnswerInput);
	[Benchmark]
	public Models.Answer DeserializeAnswerWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Answer>(AnswerOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] AnswerOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(AnswerInput);
	[Benchmark]
	public Models.Answer DeserializeAnswerWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Answer>(AnswerOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] AnswerOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(AnswerInput);
	[Benchmark]
	public Models.Answer DeserializeAnswerWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Answer>(AnswerOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String BadgeOutputOfJilSerializer = JilSerializer.Serialize(BadgeInput);
	[Benchmark]
	public Models.Badge DeserializeBadgeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Badge>(BadgeOutputOfJilSerializer);
	}
  
	private static readonly String BadgeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(BadgeInput);
	[Benchmark]
	public Models.Badge DeserializeBadgeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Badge>(BadgeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] BadgeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(BadgeInput);
	[Benchmark]
	public Models.Badge DeserializeBadgeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Badge>(BadgeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] BadgeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BadgeInput);
	[Benchmark]
	public Models.Badge DeserializeBadgeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Badge>(BadgeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String CommentOutputOfJilSerializer = JilSerializer.Serialize(CommentInput);
	[Benchmark]
	public Models.Comment DeserializeCommentWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Comment>(CommentOutputOfJilSerializer);
	}
  
	private static readonly String CommentOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(CommentInput);
	[Benchmark]
	public Models.Comment DeserializeCommentWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Comment>(CommentOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] CommentOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(CommentInput);
	[Benchmark]
	public Models.Comment DeserializeCommentWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Comment>(CommentOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] CommentOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(CommentInput);
	[Benchmark]
	public Models.Comment DeserializeCommentWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Comment>(CommentOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String ErrorOutputOfJilSerializer = JilSerializer.Serialize(ErrorInput);
	[Benchmark]
	public Models.Error DeserializeErrorWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Error>(ErrorOutputOfJilSerializer);
	}
  
	private static readonly String ErrorOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ErrorInput);
	[Benchmark]
	public Models.Error DeserializeErrorWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Error>(ErrorOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] ErrorOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ErrorInput);
	[Benchmark]
	public Models.Error DeserializeErrorWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Error>(ErrorOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] ErrorOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ErrorInput);
	[Benchmark]
	public Models.Error DeserializeErrorWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Error>(ErrorOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String EventOutputOfJilSerializer = JilSerializer.Serialize(EventInput);
	[Benchmark]
	public Models.Event DeserializeEventWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Event>(EventOutputOfJilSerializer);
	}
  
	private static readonly String EventOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(EventInput);
	[Benchmark]
	public Models.Event DeserializeEventWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Event>(EventOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] EventOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(EventInput);
	[Benchmark]
	public Models.Event DeserializeEventWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Event>(EventOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] EventOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(EventInput);
	[Benchmark]
	public Models.Event DeserializeEventWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Event>(EventOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileFeedOutputOfJilSerializer = JilSerializer.Serialize(MobileFeedInput);
	[Benchmark]
	public Models.MobileFeed DeserializeMobileFeedWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileFeed>(MobileFeedOutputOfJilSerializer);
	}
  
	private static readonly String MobileFeedOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileFeedInput);
	[Benchmark]
	public Models.MobileFeed DeserializeMobileFeedWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileFeed>(MobileFeedOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileFeedOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileFeedInput);
	[Benchmark]
	public Models.MobileFeed DeserializeMobileFeedWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileFeed>(MobileFeedOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileFeedOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileFeedInput);
	[Benchmark]
	public Models.MobileFeed DeserializeMobileFeedWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileFeed>(MobileFeedOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileQuestionOutputOfJilSerializer = JilSerializer.Serialize(MobileQuestionInput);
	[Benchmark]
	public Models.MobileQuestion DeserializeMobileQuestionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileQuestion>(MobileQuestionOutputOfJilSerializer);
	}
  
	private static readonly String MobileQuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileQuestionInput);
	[Benchmark]
	public Models.MobileQuestion DeserializeMobileQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileQuestion>(MobileQuestionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileQuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileQuestionInput);
	[Benchmark]
	public Models.MobileQuestion DeserializeMobileQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileQuestion>(MobileQuestionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileQuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileQuestionInput);
	[Benchmark]
	public Models.MobileQuestion DeserializeMobileQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileQuestion>(MobileQuestionOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileRepChangeOutputOfJilSerializer = JilSerializer.Serialize(MobileRepChangeInput);
	[Benchmark]
	public Models.MobileRepChange DeserializeMobileRepChangeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileRepChange>(MobileRepChangeOutputOfJilSerializer);
	}
  
	private static readonly String MobileRepChangeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileRepChangeInput);
	[Benchmark]
	public Models.MobileRepChange DeserializeMobileRepChangeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileRepChange>(MobileRepChangeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileRepChangeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileRepChangeInput);
	[Benchmark]
	public Models.MobileRepChange DeserializeMobileRepChangeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileRepChange>(MobileRepChangeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileRepChangeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileRepChangeInput);
	[Benchmark]
	public Models.MobileRepChange DeserializeMobileRepChangeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileRepChange>(MobileRepChangeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileInboxItemOutputOfJilSerializer = JilSerializer.Serialize(MobileInboxItemInput);
	[Benchmark]
	public Models.MobileInboxItem DeserializeMobileInboxItemWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileInboxItem>(MobileInboxItemOutputOfJilSerializer);
	}
  
	private static readonly String MobileInboxItemOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileInboxItemInput);
	[Benchmark]
	public Models.MobileInboxItem DeserializeMobileInboxItemWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileInboxItem>(MobileInboxItemOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileInboxItemOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileInboxItemInput);
	[Benchmark]
	public Models.MobileInboxItem DeserializeMobileInboxItemWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileInboxItem>(MobileInboxItemOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileInboxItemOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileInboxItemInput);
	[Benchmark]
	public Models.MobileInboxItem DeserializeMobileInboxItemWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileInboxItem>(MobileInboxItemOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileBadgeAwardOutputOfJilSerializer = JilSerializer.Serialize(MobileBadgeAwardInput);
	[Benchmark]
	public Models.MobileBadgeAward DeserializeMobileBadgeAwardWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileBadgeAward>(MobileBadgeAwardOutputOfJilSerializer);
	}
  
	private static readonly String MobileBadgeAwardOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBadgeAwardInput);
	[Benchmark]
	public Models.MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileBadgeAward>(MobileBadgeAwardOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileBadgeAwardOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBadgeAwardInput);
	[Benchmark]
	public Models.MobileBadgeAward DeserializeMobileBadgeAwardWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileBadgeAward>(MobileBadgeAwardOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileBadgeAwardOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBadgeAwardInput);
	[Benchmark]
	public Models.MobileBadgeAward DeserializeMobileBadgeAwardWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileBadgeAward>(MobileBadgeAwardOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobilePrivilegeOutputOfJilSerializer = JilSerializer.Serialize(MobilePrivilegeInput);
	[Benchmark]
	public Models.MobilePrivilege DeserializeMobilePrivilegeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobilePrivilege>(MobilePrivilegeOutputOfJilSerializer);
	}
  
	private static readonly String MobilePrivilegeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobilePrivilegeInput);
	[Benchmark]
	public Models.MobilePrivilege DeserializeMobilePrivilegeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobilePrivilege>(MobilePrivilegeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobilePrivilegeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobilePrivilegeInput);
	[Benchmark]
	public Models.MobilePrivilege DeserializeMobilePrivilegeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobilePrivilege>(MobilePrivilegeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobilePrivilegeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobilePrivilegeInput);
	[Benchmark]
	public Models.MobilePrivilege DeserializeMobilePrivilegeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobilePrivilege>(MobilePrivilegeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileCommunityBulletinOutputOfJilSerializer = JilSerializer.Serialize(MobileCommunityBulletinInput);
	[Benchmark]
	public Models.MobileCommunityBulletin DeserializeMobileCommunityBulletinWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileCommunityBulletin>(MobileCommunityBulletinOutputOfJilSerializer);
	}
  
	private static readonly String MobileCommunityBulletinOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileCommunityBulletinInput);
	[Benchmark]
	public Models.MobileCommunityBulletin DeserializeMobileCommunityBulletinWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileCommunityBulletin>(MobileCommunityBulletinOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileCommunityBulletinOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileCommunityBulletinInput);
	[Benchmark]
	public Models.MobileCommunityBulletin DeserializeMobileCommunityBulletinWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileCommunityBulletin>(MobileCommunityBulletinOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileCommunityBulletinOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileCommunityBulletinInput);
	[Benchmark]
	public Models.MobileCommunityBulletin DeserializeMobileCommunityBulletinWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileCommunityBulletin>(MobileCommunityBulletinOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileAssociationBonusOutputOfJilSerializer = JilSerializer.Serialize(MobileAssociationBonusInput);
	[Benchmark]
	public Models.MobileAssociationBonus DeserializeMobileAssociationBonusWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileAssociationBonus>(MobileAssociationBonusOutputOfJilSerializer);
	}
  
	private static readonly String MobileAssociationBonusOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileAssociationBonusInput);
	[Benchmark]
	public Models.MobileAssociationBonus DeserializeMobileAssociationBonusWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileAssociationBonus>(MobileAssociationBonusOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileAssociationBonusOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileAssociationBonusInput);
	[Benchmark]
	public Models.MobileAssociationBonus DeserializeMobileAssociationBonusWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileAssociationBonus>(MobileAssociationBonusOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileAssociationBonusOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileAssociationBonusInput);
	[Benchmark]
	public Models.MobileAssociationBonus DeserializeMobileAssociationBonusWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileAssociationBonus>(MobileAssociationBonusOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileCareersJobAdOutputOfJilSerializer = JilSerializer.Serialize(MobileCareersJobAdInput);
	[Benchmark]
	public Models.MobileCareersJobAd DeserializeMobileCareersJobAdWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileCareersJobAd>(MobileCareersJobAdOutputOfJilSerializer);
	}
  
	private static readonly String MobileCareersJobAdOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileCareersJobAdInput);
	[Benchmark]
	public Models.MobileCareersJobAd DeserializeMobileCareersJobAdWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileCareersJobAd>(MobileCareersJobAdOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileCareersJobAdOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileCareersJobAdInput);
	[Benchmark]
	public Models.MobileCareersJobAd DeserializeMobileCareersJobAdWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileCareersJobAd>(MobileCareersJobAdOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileCareersJobAdOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileCareersJobAdInput);
	[Benchmark]
	public Models.MobileCareersJobAd DeserializeMobileCareersJobAdWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileCareersJobAd>(MobileCareersJobAdOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileBannerAdOutputOfJilSerializer = JilSerializer.Serialize(MobileBannerAdInput);
	[Benchmark]
	public Models.MobileBannerAd DeserializeMobileBannerAdWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileBannerAd>(MobileBannerAdOutputOfJilSerializer);
	}
  
	private static readonly String MobileBannerAdOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBannerAdInput);
	[Benchmark]
	public Models.MobileBannerAd DeserializeMobileBannerAdWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileBannerAd>(MobileBannerAdOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileBannerAdOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBannerAdInput);
	[Benchmark]
	public Models.MobileBannerAd DeserializeMobileBannerAdWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileBannerAd>(MobileBannerAdOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileBannerAdOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBannerAdInput);
	[Benchmark]
	public Models.MobileBannerAd DeserializeMobileBannerAdWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileBannerAd>(MobileBannerAdOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileUpdateNoticeOutputOfJilSerializer = JilSerializer.Serialize(MobileUpdateNoticeInput);
	[Benchmark]
	public Models.MobileUpdateNotice DeserializeMobileUpdateNoticeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileUpdateNotice>(MobileUpdateNoticeOutputOfJilSerializer);
	}
  
	private static readonly String MobileUpdateNoticeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileUpdateNoticeInput);
	[Benchmark]
	public Models.MobileUpdateNotice DeserializeMobileUpdateNoticeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileUpdateNotice>(MobileUpdateNoticeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileUpdateNoticeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileUpdateNoticeInput);
	[Benchmark]
	public Models.MobileUpdateNotice DeserializeMobileUpdateNoticeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileUpdateNotice>(MobileUpdateNoticeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileUpdateNoticeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileUpdateNoticeInput);
	[Benchmark]
	public Models.MobileUpdateNotice DeserializeMobileUpdateNoticeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileUpdateNotice>(MobileUpdateNoticeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String FlagOptionOutputOfJilSerializer = JilSerializer.Serialize(FlagOptionInput);
	[Benchmark]
	public Models.FlagOption DeserializeFlagOptionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.FlagOption>(FlagOptionOutputOfJilSerializer);
	}
  
	private static readonly String FlagOptionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(FlagOptionInput);
	[Benchmark]
	public Models.FlagOption DeserializeFlagOptionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.FlagOption>(FlagOptionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] FlagOptionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(FlagOptionInput);
	[Benchmark]
	public Models.FlagOption DeserializeFlagOptionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.FlagOption>(FlagOptionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] FlagOptionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(FlagOptionInput);
	[Benchmark]
	public Models.FlagOption DeserializeFlagOptionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.FlagOption>(FlagOptionOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String InboxItemOutputOfJilSerializer = JilSerializer.Serialize(InboxItemInput);
	[Benchmark]
	public Models.InboxItem DeserializeInboxItemWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.InboxItem>(InboxItemOutputOfJilSerializer);
	}
  
	private static readonly String InboxItemOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(InboxItemInput);
	[Benchmark]
	public Models.InboxItem DeserializeInboxItemWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.InboxItem>(InboxItemOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] InboxItemOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(InboxItemInput);
	[Benchmark]
	public Models.InboxItem DeserializeInboxItemWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.InboxItem>(InboxItemOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] InboxItemOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(InboxItemInput);
	[Benchmark]
	public Models.InboxItem DeserializeInboxItemWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.InboxItem>(InboxItemOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String InfoOutputOfJilSerializer = JilSerializer.Serialize(InfoInput);
	[Benchmark]
	public Models.Info DeserializeInfoWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Info>(InfoOutputOfJilSerializer);
	}
  
	private static readonly String InfoOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(InfoInput);
	[Benchmark]
	public Models.Info DeserializeInfoWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Info>(InfoOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] InfoOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(InfoInput);
	[Benchmark]
	public Models.Info DeserializeInfoWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Info>(InfoOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] InfoOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(InfoInput);
	[Benchmark]
	public Models.Info DeserializeInfoWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Info>(InfoOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String NetworkUserOutputOfJilSerializer = JilSerializer.Serialize(NetworkUserInput);
	[Benchmark]
	public Models.NetworkUser DeserializeNetworkUserWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.NetworkUser>(NetworkUserOutputOfJilSerializer);
	}
  
	private static readonly String NetworkUserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NetworkUserInput);
	[Benchmark]
	public Models.NetworkUser DeserializeNetworkUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.NetworkUser>(NetworkUserOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] NetworkUserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NetworkUserInput);
	[Benchmark]
	public Models.NetworkUser DeserializeNetworkUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.NetworkUser>(NetworkUserOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] NetworkUserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NetworkUserInput);
	[Benchmark]
	public Models.NetworkUser DeserializeNetworkUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.NetworkUser>(NetworkUserOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String NotificationOutputOfJilSerializer = JilSerializer.Serialize(NotificationInput);
	[Benchmark]
	public Models.Notification DeserializeNotificationWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Notification>(NotificationOutputOfJilSerializer);
	}
  
	private static readonly String NotificationOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NotificationInput);
	[Benchmark]
	public Models.Notification DeserializeNotificationWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Notification>(NotificationOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] NotificationOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NotificationInput);
	[Benchmark]
	public Models.Notification DeserializeNotificationWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Notification>(NotificationOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] NotificationOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NotificationInput);
	[Benchmark]
	public Models.Notification DeserializeNotificationWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Notification>(NotificationOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String PostOutputOfJilSerializer = JilSerializer.Serialize(PostInput);
	[Benchmark]
	public Models.Post DeserializePostWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Post>(PostOutputOfJilSerializer);
	}
  
	private static readonly String PostOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(PostInput);
	[Benchmark]
	public Models.Post DeserializePostWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Post>(PostOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] PostOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(PostInput);
	[Benchmark]
	public Models.Post DeserializePostWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Post>(PostOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] PostOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(PostInput);
	[Benchmark]
	public Models.Post DeserializePostWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Post>(PostOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String PrivilegeOutputOfJilSerializer = JilSerializer.Serialize(PrivilegeInput);
	[Benchmark]
	public Models.Privilege DeserializePrivilegeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Privilege>(PrivilegeOutputOfJilSerializer);
	}
  
	private static readonly String PrivilegeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(PrivilegeInput);
	[Benchmark]
	public Models.Privilege DeserializePrivilegeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Privilege>(PrivilegeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] PrivilegeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(PrivilegeInput);
	[Benchmark]
	public Models.Privilege DeserializePrivilegeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Privilege>(PrivilegeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] PrivilegeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(PrivilegeInput);
	[Benchmark]
	public Models.Privilege DeserializePrivilegeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Privilege>(PrivilegeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String QuestionOutputOfJilSerializer = JilSerializer.Serialize(QuestionInput);
	[Benchmark]
	public Models.Question DeserializeQuestionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Question>(QuestionOutputOfJilSerializer);
	}
  
	private static readonly String QuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(QuestionInput);
	[Benchmark]
	public Models.Question DeserializeQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Question>(QuestionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] QuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(QuestionInput);
	[Benchmark]
	public Models.Question DeserializeQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Question>(QuestionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] QuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(QuestionInput);
	[Benchmark]
	public Models.Question DeserializeQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Question>(QuestionOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String QuestionTimelineOutputOfJilSerializer = JilSerializer.Serialize(QuestionTimelineInput);
	[Benchmark]
	public Models.QuestionTimeline DeserializeQuestionTimelineWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.QuestionTimeline>(QuestionTimelineOutputOfJilSerializer);
	}
  
	private static readonly String QuestionTimelineOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(QuestionTimelineInput);
	[Benchmark]
	public Models.QuestionTimeline DeserializeQuestionTimelineWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.QuestionTimeline>(QuestionTimelineOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] QuestionTimelineOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(QuestionTimelineInput);
	[Benchmark]
	public Models.QuestionTimeline DeserializeQuestionTimelineWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.QuestionTimeline>(QuestionTimelineOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] QuestionTimelineOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(QuestionTimelineInput);
	[Benchmark]
	public Models.QuestionTimeline DeserializeQuestionTimelineWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.QuestionTimeline>(QuestionTimelineOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String ReputationOutputOfJilSerializer = JilSerializer.Serialize(ReputationInput);
	[Benchmark]
	public Models.Reputation DeserializeReputationWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Reputation>(ReputationOutputOfJilSerializer);
	}
  
	private static readonly String ReputationOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ReputationInput);
	[Benchmark]
	public Models.Reputation DeserializeReputationWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Reputation>(ReputationOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] ReputationOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ReputationInput);
	[Benchmark]
	public Models.Reputation DeserializeReputationWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Reputation>(ReputationOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] ReputationOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ReputationInput);
	[Benchmark]
	public Models.Reputation DeserializeReputationWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Reputation>(ReputationOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String ReputationHistoryOutputOfJilSerializer = JilSerializer.Serialize(ReputationHistoryInput);
	[Benchmark]
	public Models.ReputationHistory DeserializeReputationHistoryWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.ReputationHistory>(ReputationHistoryOutputOfJilSerializer);
	}
  
	private static readonly String ReputationHistoryOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ReputationHistoryInput);
	[Benchmark]
	public Models.ReputationHistory DeserializeReputationHistoryWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.ReputationHistory>(ReputationHistoryOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] ReputationHistoryOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ReputationHistoryInput);
	[Benchmark]
	public Models.ReputationHistory DeserializeReputationHistoryWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.ReputationHistory>(ReputationHistoryOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] ReputationHistoryOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ReputationHistoryInput);
	[Benchmark]
	public Models.ReputationHistory DeserializeReputationHistoryWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.ReputationHistory>(ReputationHistoryOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String RevisionOutputOfJilSerializer = JilSerializer.Serialize(RevisionInput);
	[Benchmark]
	public Models.Revision DeserializeRevisionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Revision>(RevisionOutputOfJilSerializer);
	}
  
	private static readonly String RevisionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(RevisionInput);
	[Benchmark]
	public Models.Revision DeserializeRevisionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Revision>(RevisionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] RevisionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(RevisionInput);
	[Benchmark]
	public Models.Revision DeserializeRevisionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Revision>(RevisionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] RevisionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(RevisionInput);
	[Benchmark]
	public Models.Revision DeserializeRevisionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Revision>(RevisionOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String SearchExcerptOutputOfJilSerializer = JilSerializer.Serialize(SearchExcerptInput);
	[Benchmark]
	public Models.SearchExcerpt DeserializeSearchExcerptWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.SearchExcerpt>(SearchExcerptOutputOfJilSerializer);
	}
  
	private static readonly String SearchExcerptOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SearchExcerptInput);
	[Benchmark]
	public Models.SearchExcerpt DeserializeSearchExcerptWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.SearchExcerpt>(SearchExcerptOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] SearchExcerptOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SearchExcerptInput);
	[Benchmark]
	public Models.SearchExcerpt DeserializeSearchExcerptWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.SearchExcerpt>(SearchExcerptOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] SearchExcerptOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SearchExcerptInput);
	[Benchmark]
	public Models.SearchExcerpt DeserializeSearchExcerptWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.SearchExcerpt>(SearchExcerptOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String ShallowUserOutputOfJilSerializer = JilSerializer.Serialize(ShallowUserInput);
	[Benchmark]
	public Models.ShallowUser DeserializeShallowUserWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.ShallowUser>(ShallowUserOutputOfJilSerializer);
	}
  
	private static readonly String ShallowUserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ShallowUserInput);
	[Benchmark]
	public Models.ShallowUser DeserializeShallowUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.ShallowUser>(ShallowUserOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] ShallowUserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ShallowUserInput);
	[Benchmark]
	public Models.ShallowUser DeserializeShallowUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.ShallowUser>(ShallowUserOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] ShallowUserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ShallowUserInput);
	[Benchmark]
	public Models.ShallowUser DeserializeShallowUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.ShallowUser>(ShallowUserOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String SuggestedEditOutputOfJilSerializer = JilSerializer.Serialize(SuggestedEditInput);
	[Benchmark]
	public Models.SuggestedEdit DeserializeSuggestedEditWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.SuggestedEdit>(SuggestedEditOutputOfJilSerializer);
	}
  
	private static readonly String SuggestedEditOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SuggestedEditInput);
	[Benchmark]
	public Models.SuggestedEdit DeserializeSuggestedEditWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.SuggestedEdit>(SuggestedEditOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] SuggestedEditOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SuggestedEditInput);
	[Benchmark]
	public Models.SuggestedEdit DeserializeSuggestedEditWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.SuggestedEdit>(SuggestedEditOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] SuggestedEditOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SuggestedEditInput);
	[Benchmark]
	public Models.SuggestedEdit DeserializeSuggestedEditWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.SuggestedEdit>(SuggestedEditOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String TagOutputOfJilSerializer = JilSerializer.Serialize(TagInput);
	[Benchmark]
	public Models.Tag DeserializeTagWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Tag>(TagOutputOfJilSerializer);
	}
  
	private static readonly String TagOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagInput);
	[Benchmark]
	public Models.Tag DeserializeTagWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Tag>(TagOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] TagOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagInput);
	[Benchmark]
	public Models.Tag DeserializeTagWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Tag>(TagOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] TagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagInput);
	[Benchmark]
	public Models.Tag DeserializeTagWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Tag>(TagOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String TagScoreOutputOfJilSerializer = JilSerializer.Serialize(TagScoreInput);
	[Benchmark]
	public Models.TagScore DeserializeTagScoreWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.TagScore>(TagScoreOutputOfJilSerializer);
	}
  
	private static readonly String TagScoreOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagScoreInput);
	[Benchmark]
	public Models.TagScore DeserializeTagScoreWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.TagScore>(TagScoreOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] TagScoreOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagScoreInput);
	[Benchmark]
	public Models.TagScore DeserializeTagScoreWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.TagScore>(TagScoreOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] TagScoreOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagScoreInput);
	[Benchmark]
	public Models.TagScore DeserializeTagScoreWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.TagScore>(TagScoreOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String TagSynonymOutputOfJilSerializer = JilSerializer.Serialize(TagSynonymInput);
	[Benchmark]
	public Models.TagSynonym DeserializeTagSynonymWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.TagSynonym>(TagSynonymOutputOfJilSerializer);
	}
  
	private static readonly String TagSynonymOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagSynonymInput);
	[Benchmark]
	public Models.TagSynonym DeserializeTagSynonymWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.TagSynonym>(TagSynonymOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] TagSynonymOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagSynonymInput);
	[Benchmark]
	public Models.TagSynonym DeserializeTagSynonymWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.TagSynonym>(TagSynonymOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] TagSynonymOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagSynonymInput);
	[Benchmark]
	public Models.TagSynonym DeserializeTagSynonymWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.TagSynonym>(TagSynonymOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String TagWikiOutputOfJilSerializer = JilSerializer.Serialize(TagWikiInput);
	[Benchmark]
	public Models.TagWiki DeserializeTagWikiWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.TagWiki>(TagWikiOutputOfJilSerializer);
	}
  
	private static readonly String TagWikiOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TagWikiInput);
	[Benchmark]
	public Models.TagWiki DeserializeTagWikiWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.TagWiki>(TagWikiOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] TagWikiOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TagWikiInput);
	[Benchmark]
	public Models.TagWiki DeserializeTagWikiWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.TagWiki>(TagWikiOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] TagWikiOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TagWikiInput);
	[Benchmark]
	public Models.TagWiki DeserializeTagWikiWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.TagWiki>(TagWikiOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String TopTagOutputOfJilSerializer = JilSerializer.Serialize(TopTagInput);
	[Benchmark]
	public Models.TopTag DeserializeTopTagWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.TopTag>(TopTagOutputOfJilSerializer);
	}
  
	private static readonly String TopTagOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(TopTagInput);
	[Benchmark]
	public Models.TopTag DeserializeTopTagWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.TopTag>(TopTagOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] TopTagOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(TopTagInput);
	[Benchmark]
	public Models.TopTag DeserializeTopTagWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.TopTag>(TopTagOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] TopTagOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(TopTagInput);
	[Benchmark]
	public Models.TopTag DeserializeTopTagWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.TopTag>(TopTagOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String UserOutputOfJilSerializer = JilSerializer.Serialize(UserInput);
	[Benchmark]
	public Models.User DeserializeUserWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.User>(UserOutputOfJilSerializer);
	}
  
	private static readonly String UserOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UserInput);
	[Benchmark]
	public Models.User DeserializeUserWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.User>(UserOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] UserOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UserInput);
	[Benchmark]
	public Models.User DeserializeUserWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.User>(UserOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] UserOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UserInput);
	[Benchmark]
	public Models.User DeserializeUserWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.User>(UserOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String UserTimelineOutputOfJilSerializer = JilSerializer.Serialize(UserTimelineInput);
	[Benchmark]
	public Models.UserTimeline DeserializeUserTimelineWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.UserTimeline>(UserTimelineOutputOfJilSerializer);
	}
  
	private static readonly String UserTimelineOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(UserTimelineInput);
	[Benchmark]
	public Models.UserTimeline DeserializeUserTimelineWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.UserTimeline>(UserTimelineOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] UserTimelineOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(UserTimelineInput);
	[Benchmark]
	public Models.UserTimeline DeserializeUserTimelineWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.UserTimeline>(UserTimelineOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] UserTimelineOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(UserTimelineInput);
	[Benchmark]
	public Models.UserTimeline DeserializeUserTimelineWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.UserTimeline>(UserTimelineOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String WritePermissionOutputOfJilSerializer = JilSerializer.Serialize(WritePermissionInput);
	[Benchmark]
	public Models.WritePermission DeserializeWritePermissionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.WritePermission>(WritePermissionOutputOfJilSerializer);
	}
  
	private static readonly String WritePermissionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(WritePermissionInput);
	[Benchmark]
	public Models.WritePermission DeserializeWritePermissionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.WritePermission>(WritePermissionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] WritePermissionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(WritePermissionInput);
	[Benchmark]
	public Models.WritePermission DeserializeWritePermissionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.WritePermission>(WritePermissionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] WritePermissionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(WritePermissionInput);
	[Benchmark]
	public Models.WritePermission DeserializeWritePermissionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.WritePermission>(WritePermissionOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MobileBannerAdImageOutputOfJilSerializer = JilSerializer.Serialize(MobileBannerAdImageInput);
	[Benchmark]
	public Models.MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfJilSerializer);
	}
  
	private static readonly String MobileBannerAdImageOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MobileBannerAdImageInput);
	[Benchmark]
	public Models.MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MobileBannerAdImageOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MobileBannerAdImageInput);
	[Benchmark]
	public Models.MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MobileBannerAdImageOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MobileBannerAdImageInput);
	[Benchmark]
	public Models.MobileBannerAd.MobileBannerAdImage DeserializeMobileBannerAdImageWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.MobileBannerAd.MobileBannerAdImage>(MobileBannerAdImageOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String SiteOutputOfJilSerializer = JilSerializer.Serialize(SiteInput);
	[Benchmark]
	public Models.Info.Site DeserializeSiteWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Info.Site>(SiteOutputOfJilSerializer);
	}
  
	private static readonly String SiteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(SiteInput);
	[Benchmark]
	public Models.Info.Site DeserializeSiteWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Info.Site>(SiteOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] SiteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(SiteInput);
	[Benchmark]
	public Models.Info.Site DeserializeSiteWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Info.Site>(SiteOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] SiteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(SiteInput);
	[Benchmark]
	public Models.Info.Site DeserializeSiteWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Info.Site>(SiteOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String RelatedSiteOutputOfJilSerializer = JilSerializer.Serialize(RelatedSiteInput);
	[Benchmark]
	public Models.Info.RelatedSite DeserializeRelatedSiteWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Info.RelatedSite>(RelatedSiteOutputOfJilSerializer);
	}
  
	private static readonly String RelatedSiteOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(RelatedSiteInput);
	[Benchmark]
	public Models.Info.RelatedSite DeserializeRelatedSiteWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Info.RelatedSite>(RelatedSiteOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] RelatedSiteOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(RelatedSiteInput);
	[Benchmark]
	public Models.Info.RelatedSite DeserializeRelatedSiteWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Info.RelatedSite>(RelatedSiteOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] RelatedSiteOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(RelatedSiteInput);
	[Benchmark]
	public Models.Info.RelatedSite DeserializeRelatedSiteWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Info.RelatedSite>(RelatedSiteOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String ClosedDetailsOutputOfJilSerializer = JilSerializer.Serialize(ClosedDetailsInput);
	[Benchmark]
	public Models.Question.ClosedDetails DeserializeClosedDetailsWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Question.ClosedDetails>(ClosedDetailsOutputOfJilSerializer);
	}
  
	private static readonly String ClosedDetailsOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(ClosedDetailsInput);
	[Benchmark]
	public Models.Question.ClosedDetails DeserializeClosedDetailsWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Question.ClosedDetails>(ClosedDetailsOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] ClosedDetailsOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(ClosedDetailsInput);
	[Benchmark]
	public Models.Question.ClosedDetails DeserializeClosedDetailsWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Question.ClosedDetails>(ClosedDetailsOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] ClosedDetailsOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(ClosedDetailsInput);
	[Benchmark]
	public Models.Question.ClosedDetails DeserializeClosedDetailsWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Question.ClosedDetails>(ClosedDetailsOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String NoticeOutputOfJilSerializer = JilSerializer.Serialize(NoticeInput);
	[Benchmark]
	public Models.Question.Notice DeserializeNoticeWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Question.Notice>(NoticeOutputOfJilSerializer);
	}
  
	private static readonly String NoticeOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(NoticeInput);
	[Benchmark]
	public Models.Question.Notice DeserializeNoticeWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Question.Notice>(NoticeOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] NoticeOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(NoticeInput);
	[Benchmark]
	public Models.Question.Notice DeserializeNoticeWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Question.Notice>(NoticeOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] NoticeOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(NoticeInput);
	[Benchmark]
	public Models.Question.Notice DeserializeNoticeWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Question.Notice>(NoticeOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String MigrationInfoOutputOfJilSerializer = JilSerializer.Serialize(MigrationInfoInput);
	[Benchmark]
	public Models.Question.MigrationInfo DeserializeMigrationInfoWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Question.MigrationInfo>(MigrationInfoOutputOfJilSerializer);
	}
  
	private static readonly String MigrationInfoOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(MigrationInfoInput);
	[Benchmark]
	public Models.Question.MigrationInfo DeserializeMigrationInfoWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Question.MigrationInfo>(MigrationInfoOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] MigrationInfoOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(MigrationInfoInput);
	[Benchmark]
	public Models.Question.MigrationInfo DeserializeMigrationInfoWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Question.MigrationInfo>(MigrationInfoOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] MigrationInfoOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(MigrationInfoInput);
	[Benchmark]
	public Models.Question.MigrationInfo DeserializeMigrationInfoWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Question.MigrationInfo>(MigrationInfoOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String BadgeCountOutputOfJilSerializer = JilSerializer.Serialize(BadgeCountInput);
	[Benchmark]
	public Models.User.BadgeCount DeserializeBadgeCountWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.User.BadgeCount>(BadgeCountOutputOfJilSerializer);
	}
  
	private static readonly String BadgeCountOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(BadgeCountInput);
	[Benchmark]
	public Models.User.BadgeCount DeserializeBadgeCountWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.User.BadgeCount>(BadgeCountOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] BadgeCountOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(BadgeCountInput);
	[Benchmark]
	public Models.User.BadgeCount DeserializeBadgeCountWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.User.BadgeCount>(BadgeCountOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] BadgeCountOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(BadgeCountInput);
	[Benchmark]
	public Models.User.BadgeCount DeserializeBadgeCountWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.User.BadgeCount>(BadgeCountOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String StylingOutputOfJilSerializer = JilSerializer.Serialize(StylingInput);
	[Benchmark]
	public Models.Info.Site.Styling DeserializeStylingWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Info.Site.Styling>(StylingOutputOfJilSerializer);
	}
  
	private static readonly String StylingOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(StylingInput);
	[Benchmark]
	public Models.Info.Site.Styling DeserializeStylingWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Info.Site.Styling>(StylingOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] StylingOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(StylingInput);
	[Benchmark]
	public Models.Info.Site.Styling DeserializeStylingWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Info.Site.Styling>(StylingOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] StylingOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(StylingInput);
	[Benchmark]
	public Models.Info.Site.Styling DeserializeStylingWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Info.Site.Styling>(StylingOutputOfUtf8JsonSerializer);
	}
  
	private static readonly String OriginalQuestionOutputOfJilSerializer = JilSerializer.Serialize(OriginalQuestionInput);
	[Benchmark]
	public Models.Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithJilSerializer()
	{
		return JilSerializer.Deserialize<Models.Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfJilSerializer);
	}
  
	private static readonly String OriginalQuestionOutputOfSpanJsonSerializer = SpanJsonSerializer.Serialize(OriginalQuestionInput);
	[Benchmark]
	public Models.Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithSpanJsonSerializer()
	{
		return SpanJsonSerializer.Deserialize<Models.Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfSpanJsonSerializer);
	}
  
	private static readonly Byte[] OriginalQuestionOutputOfSpanJsonUtf8Serializer = SpanJsonUtf8Serializer.Serialize(OriginalQuestionInput);
	[Benchmark]
	public Models.Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithSpanJsonUtf8Serializer()
	{
		return SpanJsonUtf8Serializer.Deserialize<Models.Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfSpanJsonUtf8Serializer);
	}
  
	private static readonly Byte[] OriginalQuestionOutputOfUtf8JsonSerializer = Utf8JsonSerializer.Serialize(OriginalQuestionInput);
	[Benchmark]
	public Models.Question.ClosedDetails.OriginalQuestion DeserializeOriginalQuestionWithUtf8JsonSerializer()
	{
		return Utf8JsonSerializer.Deserialize<Models.Question.ClosedDetails.OriginalQuestion>(OriginalQuestionOutputOfUtf8JsonSerializer);
	}
 
  }
}
  