namespace SpanJson.Benchmarks
{
    public class Program
    {
        private static void Main(string[] args)
        {
            /*
                Those with ! are qutie a bit slower on some systems
                BadgeCount
                ClosedDetails
                Error
                FlagOption
                MobileAssociationBonus
                MobileBadgeAward
                MobileBannerAdImage
                MobileBannerAdImage
                MobileCareersJob
                MobileCommunityBulletin
                MobileInboxItem
                MobilePrivilege
                MobileQuestion
                MobileRepChange
                MobileUpdateNotice
                Notice!!!!
                OriginalQuestion
                QuestionTImeline
                RelatedSite
                Reputation
                ShallowUser
                Styling
                TagScore!!
                TopTag!!
                WritePermission
             */
            BenchmarkDotNet.Running.BenchmarkRunner.Run<ModelBenchmark>();
        }
    }
}