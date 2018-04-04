# SpanJson
SpanJson
Sandbox for playing around with Span and JSON Serialization.
This is basically the ValueStringBuilder from CoreFx with the TryFormat API for formatting values with Span<char>.
The number formatting is replaced with UTF8Json (or some other variation) as the CoreCLR version is two times slower.
The actual serializers are a T4 Template (BclFormatter.tt)

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10.0.17133
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=3906250 Hz, Resolution=256.0000 ns, Timer=TSC
.NET Core SDK=2.1.300-preview1-008174
  [Host]   : .NET Core 2.1.0-preview3-26403-01 (CoreCLR 4.6.26402.06, CoreFX 4.6.26402.05), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26403-01 (CoreCLR 4.6.26402.06, CoreFX 4.6.26402.05), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                 Method |        Mean |         Error |        StdDev |  Gen 0 | Allocated |
|------------------------------------------------------- |------------:|--------------:|--------------:|-------:|----------:|
|                    SerializeSiteWithUtf8JsonSerializer |  2,427.3 ns |    153.003 ns |     8.6449 ns | 0.1717 |     736 B |
|                  SerializeRelatedSiteWithJilSerializer |    376.2 ns |     41.016 ns |     2.3175 ns | 0.2017 |     848 B |
|             SerializeRelatedSiteWithSpanJsonSerializer |    242.1 ns |      3.160 ns |     0.1786 ns | 0.0510 |     216 B |
|             SerializeRelatedSiteWithUtf8JsonSerializer |    222.9 ns |     10.698 ns |     0.6045 ns | 0.0284 |     120 B |
|                SerializeClosedDetailsWithJilSerializer |  1,131.1 ns |     14.969 ns |     0.8457 ns | 0.5875 |    2472 B |
|           SerializeClosedDetailsWithSpanJsonSerializer |    961.6 ns |     18.731 ns |     1.0583 ns | 0.2174 |     920 B |
|           SerializeClosedDetailsWithUtf8JsonSerializer |  1,032.9 ns |    405.219 ns |    22.8956 ns | 0.1087 |     464 B |
|                       SerializeNoticeWithJilSerializer |    633.1 ns |     49.385 ns |     2.7903 ns | 0.2012 |     848 B |
|                  SerializeNoticeWithSpanJsonSerializer |    479.2 ns |     73.788 ns |     4.1692 ns | 0.0529 |     224 B |
|                  SerializeNoticeWithUtf8JsonSerializer |    609.2 ns |     14.793 ns |     0.8359 ns | 0.0296 |     128 B |
|                SerializeMigrationInfoWithJilSerializer |  3,754.0 ns | 22,172.561 ns | 1,252.7908 ns | 0.9995 |    4208 B |
|           SerializeMigrationInfoWithSpanJsonSerializer |  2,389.7 ns |     33.761 ns |     1.9076 ns | 0.3815 |    1616 B |
|           SerializeMigrationInfoWithUtf8JsonSerializer |  2,985.9 ns |  1,387.781 ns |    78.4122 ns | 0.1945 |     824 B |
|                   SerializeBadgeCountWithJilSerializer |    218.4 ns |      2.948 ns |     0.1666 ns | 0.1392 |     584 B |
|              SerializeBadgeCountWithSpanJsonSerializer |    168.6 ns |     11.517 ns |     0.6507 ns | 0.0322 |     136 B |
|              SerializeBadgeCountWithUtf8JsonSerializer |    217.6 ns |      1.861 ns |     0.1051 ns | 0.0207 |      88 B |
|                      SerializeStylingWithJilSerializer |    357.7 ns |     23.622 ns |     1.3347 ns | 0.1807 |     760 B |
|                 SerializeStylingWithSpanJsonSerializer |    190.3 ns |     15.923 ns |     0.8997 ns | 0.0513 |     216 B |
|                 SerializeStylingWithUtf8JsonSerializer |    166.2 ns |     23.850 ns |     1.3475 ns | 0.0284 |     120 B |
|             SerializeOriginalQuestionWithJilSerializer |    329.8 ns |     25.959 ns |     1.4668 ns | 0.2074 |     872 B |
|        SerializeOriginalQuestionWithSpanJsonSerializer |    241.0 ns |    100.082 ns |     5.6548 ns | 0.0548 |     232 B |
|        SerializeOriginalQuestionWithUtf8JsonSerializer |    262.4 ns |      9.929 ns |     0.5610 ns | 0.0300 |     128 B |
|             SerializeNetworkUserWithSpanJsonSerializer |  1,243.7 ns |    116.574 ns |     6.5866 ns | 0.1831 |     776 B |
|             SerializeNetworkUserWithUtf8JsonSerializer |  1,616.7 ns |    168.400 ns |     9.5149 ns | 0.0954 |     408 B |
|                 SerializeNotificationWithJilSerializer |  3,157.9 ns |    773.838 ns |    43.7233 ns | 1.0338 |    4344 B |
|            SerializeNotificationWithSpanJsonSerializer |  2,531.5 ns |    112.833 ns |     6.3753 ns | 0.4158 |    1752 B |
|            SerializeNotificationWithUtf8JsonSerializer |  3,211.0 ns |     50.182 ns |     2.8354 ns | 0.2098 |     896 B |
|                         SerializePostWithJilSerializer |  4,617.6 ns |    359.314 ns |    20.3019 ns | 2.0218 |    8496 B |
|                    SerializePostWithSpanJsonSerializer |  4,301.5 ns |    140.002 ns |     7.9104 ns | 0.8087 |    3400 B |
|                    SerializePostWithUtf8JsonSerializer |  5,297.4 ns |    330.177 ns |    18.6556 ns | 0.4044 |    1704 B |
|                    SerializePrivilegeWithJilSerializer |    347.8 ns |     50.190 ns |     2.8358 ns | 0.1979 |     832 B |
|               SerializePrivilegeWithSpanJsonSerializer |    206.8 ns |     14.203 ns |     0.8025 ns | 0.0455 |     192 B |
|               SerializePrivilegeWithUtf8JsonSerializer |    191.4 ns |     33.848 ns |     1.9125 ns | 0.0265 |     112 B |
|                     SerializeQuestionWithJilSerializer | 19,807.2 ns |  3,953.171 ns |   223.3615 ns | 7.3242 |   30792 B |
|                SerializeQuestionWithSpanJsonSerializer | 18,147.3 ns | 11,540.220 ns |   652.0438 ns | 2.9907 |   12680 B |
|                SerializeQuestionWithUtf8JsonSerializer | 23,109.6 ns | 11,810.031 ns |   667.2886 ns | 1.4954 |    6360 B |
|             SerializeQuestionTimelineWithJilSerializer |  1,930.7 ns |    232.431 ns |    13.1328 ns | 1.0262 |    4312 B |
|        SerializeQuestionTimelineWithSpanJsonSerializer |  1,778.7 ns |    428.621 ns |    24.2179 ns | 0.3548 |    1496 B |
|        SerializeQuestionTimelineWithUtf8JsonSerializer |  2,247.3 ns |  2,765.549 ns |   156.2587 ns | 0.1793 |     760 B |
|                   SerializeReputationWithJilSerializer |    825.4 ns |    531.553 ns |    30.0337 ns | 0.3290 |    1384 B |
|              SerializeReputationWithSpanJsonSerializer |    748.1 ns |    105.870 ns |     5.9819 ns | 0.1040 |     440 B |
|              SerializeReputationWithUtf8JsonSerializer |    900.2 ns |    142.289 ns |     8.0396 ns | 0.0544 |     232 B |
|            SerializeReputationHistoryWithJilSerializer |    653.9 ns |      9.487 ns |     0.5360 ns | 0.3176 |    1336 B |
|       SerializeReputationHistoryWithSpanJsonSerializer |    552.2 ns |      6.674 ns |     0.3771 ns | 0.0849 |     360 B |
|       SerializeReputationHistoryWithUtf8JsonSerializer |    755.0 ns |    159.105 ns |     8.9897 ns | 0.0467 |     200 B |
|                     SerializeRevisionWithJilSerializer |  1,828.1 ns |    122.209 ns |     6.9050 ns | 0.9308 |    3912 B |
|                SerializeRevisionWithSpanJsonSerializer |  1,554.2 ns |     82.792 ns |     4.6779 ns | 0.3014 |    1272 B |
|                SerializeRevisionWithUtf8JsonSerializer |  1,790.0 ns |    174.891 ns |     9.8816 ns | 0.1545 |     656 B |
|                SerializeSearchExcerptWithJilSerializer |  3,397.4 ns |     77.202 ns |     4.3620 ns | 1.1444 |    4816 B |
|           SerializeSearchExcerptWithSpanJsonSerializer |  3,035.8 ns |     29.309 ns |     1.6560 ns | 0.4845 |    2040 B |
|           SerializeSearchExcerptWithUtf8JsonSerializer |  4,042.5 ns |    272.166 ns |    15.3779 ns | 0.2441 |    1048 B |
|                  SerializeShallowUserWithJilSerializer |    604.7 ns |      7.001 ns |     0.3956 ns | 0.3519 |    1480 B |
|             SerializeShallowUserWithSpanJsonSerializer |    567.3 ns |     15.615 ns |     0.8823 ns | 0.1211 |     512 B |
|             SerializeShallowUserWithUtf8JsonSerializer |    596.4 ns |     10.330 ns |     0.5837 ns | 0.0639 |     272 B |
|                SerializeSuggestedEditWithJilSerializer |  2,159.0 ns |    131.601 ns |     7.4357 ns | 0.8965 |    3768 B |
|           SerializeSuggestedEditWithSpanJsonSerializer |  1,833.8 ns |    172.654 ns |     9.7553 ns | 0.2747 |    1160 B |
|           SerializeSuggestedEditWithUtf8JsonSerializer |  2,356.5 ns |     34.587 ns |     1.9542 ns | 0.1411 |     600 B |
|                          SerializeTagWithJilSerializer |    844.8 ns |    187.949 ns |    10.6194 ns | 0.3309 |    1392 B |
|                     SerializeTagWithSpanJsonSerializer |    677.9 ns |     64.543 ns |     3.6468 ns | 0.1020 |     432 B |
|                     SerializeTagWithUtf8JsonSerializer |    815.5 ns |     61.879 ns |     3.4963 ns | 0.0544 |     232 B |
|                     SerializeTagScoreWithJilSerializer |    755.4 ns |     73.062 ns |     4.1281 ns | 0.5159 |    2168 B |
|                SerializeTagScoreWithSpanJsonSerializer |    672.4 ns |     35.938 ns |     2.0306 ns | 0.1440 |     608 B |
|                SerializeTagScoreWithUtf8JsonSerializer |    758.2 ns |    164.045 ns |     9.2689 ns | 0.0734 |     312 B |
|                   SerializeTagSynonymWithJilSerializer |  1,030.4 ns |     30.394 ns |     1.7173 ns | 0.3147 |    1328 B |
|              SerializeTagSynonymWithSpanJsonSerializer |    798.6 ns |     60.270 ns |     3.4054 ns | 0.0906 |     384 B |
|              SerializeTagSynonymWithUtf8JsonSerializer |  1,071.3 ns |      9.631 ns |     0.5442 ns | 0.0477 |     208 B |
|                      SerializeTagWikiWithJilSerializer |  2,110.6 ns |     64.548 ns |     3.6471 ns | 1.0109 |    4256 B |
|                 SerializeTagWikiWithSpanJsonSerializer |  1,863.9 ns |     37.304 ns |     2.1078 ns | 0.3395 |    1432 B |
|                 SerializeTagWikiWithUtf8JsonSerializer |  2,284.9 ns |    104.276 ns |     5.8918 ns | 0.1717 |     728 B |
|                       SerializeTopTagWithJilSerializer |    418.5 ns |     32.030 ns |     1.8097 ns | 0.3085 |    1296 B |
|                  SerializeTopTagWithSpanJsonSerializer |    343.7 ns |     12.582 ns |     0.7109 ns | 0.0777 |     328 B |
|                  SerializeTopTagWithUtf8JsonSerializer |    363.0 ns |     17.465 ns |     0.9868 ns | 0.0415 |     176 B |
|                         SerializeUserWithJilSerializer |  2,868.0 ns |    209.456 ns |    11.8346 ns | 1.0529 |    4424 B |
|                    SerializeUserWithSpanJsonSerializer |  2,588.9 ns |     67.971 ns |     3.8405 ns | 0.4349 |    1832 B |
|                    SerializeUserWithUtf8JsonSerializer |  3,774.0 ns |    131.174 ns |     7.4116 ns | 0.2174 |     928 B |
|                 SerializeUserTimelineWithJilSerializer |    964.9 ns |     19.680 ns |     1.1120 ns | 0.5016 |    2112 B |
|            SerializeUserTimelineWithSpanJsonSerializer |    840.3 ns |      5.946 ns |     0.3359 ns | 0.1364 |     576 B |
|            SerializeUserTimelineWithUtf8JsonSerializer |  1,083.3 ns |     32.610 ns |     1.8425 ns | 0.0706 |     304 B |
|              SerializeWritePermissionWithJilSerializer |    426.1 ns |     11.314 ns |     0.6393 ns | 0.3161 |    1328 B |
|         SerializeWritePermissionWithSpanJsonSerializer |    339.0 ns |     40.184 ns |     2.2705 ns | 0.0873 |     368 B |
|         SerializeWritePermissionWithUtf8JsonSerializer |    397.5 ns |     17.100 ns |     0.9662 ns | 0.0453 |     192 B |
|          SerializeMobileBannerAdImageWithJilSerializer |    259.6 ns |      6.173 ns |     0.3488 ns | 0.1407 |     592 B |
|     SerializeMobileBannerAdImageWithSpanJsonSerializer |    187.4 ns |     11.985 ns |     0.6772 ns | 0.0360 |     152 B |
|     SerializeMobileBannerAdImageWithUtf8JsonSerializer |    199.8 ns |     18.886 ns |     1.0671 ns | 0.0207 |      88 B |
|                         SerializeSiteWithJilSerializer |  2,637.9 ns |    111.444 ns |     6.2968 ns | 0.9651 |    4056 B |
|                    SerializeSiteWithSpanJsonSerializer |  2,058.3 ns |     40.588 ns |     2.2933 ns | 0.3433 |    1456 B |
|                  SerializeAccessTokenWithJilSerializer |    717.7 ns |    363.718 ns |    20.5507 ns | 0.2146 |     904 B |
|             SerializeAccessTokenWithSpanJsonSerializer |    548.4 ns |     27.921 ns |     1.5776 ns | 0.0658 |     280 B |
|             SerializeAccessTokenWithUtf8JsonSerializer |    670.0 ns |     25.686 ns |     1.4513 ns | 0.0353 |     152 B |
|                 SerializeAccountMergeWithJilSerializer |    583.1 ns |     43.306 ns |     2.4468 ns | 0.2050 |     864 B |
|            SerializeAccountMergeWithSpanJsonSerializer |    445.3 ns |     56.748 ns |     3.2063 ns | 0.0567 |     240 B |
|            SerializeAccountMergeWithUtf8JsonSerializer |    608.2 ns |    130.634 ns |     7.3810 ns | 0.0296 |     128 B |
|                       SerializeAnswerWithJilSerializer |  5,412.1 ns |    151.280 ns |     8.5476 ns | 2.1057 |    8848 B |
|                  SerializeAnswerWithSpanJsonSerializer |  5,084.8 ns |    203.220 ns |    11.4823 ns | 0.8850 |    3744 B |
|                  SerializeAnswerWithUtf8JsonSerializer |  6,595.7 ns |  6,526.241 ns |   368.7447 ns | 0.4501 |    1904 B |
|                        SerializeBadgeWithJilSerializer |    969.3 ns |     18.875 ns |     1.0665 ns | 0.5646 |    2376 B |
|                   SerializeBadgeWithSpanJsonSerializer |    902.0 ns |     60.539 ns |     3.4206 ns | 0.1936 |     816 B |
|                   SerializeBadgeWithUtf8JsonSerializer |    987.5 ns |     44.018 ns |     2.4871 ns | 0.0992 |     424 B |
|                      SerializeCommentWithJilSerializer |  1,877.1 ns |     92.001 ns |     5.1983 ns | 1.0300 |    4328 B |
|                 SerializeCommentWithSpanJsonSerializer |  1,800.7 ns |     46.210 ns |     2.6110 ns | 0.3605 |    1520 B |
|                 SerializeCommentWithUtf8JsonSerializer |  2,100.8 ns |     64.382 ns |     3.6377 ns | 0.1793 |     760 B |
|                        SerializeErrorWithJilSerializer |    328.4 ns |      9.637 ns |     0.5445 ns | 0.1922 |     808 B |
|                   SerializeErrorWithSpanJsonSerializer |    200.7 ns |     15.246 ns |     0.8614 ns | 0.0398 |     168 B |
|                   SerializeErrorWithUtf8JsonSerializer |    189.1 ns |      1.278 ns |     0.0722 ns | 0.0226 |      96 B |
|                        SerializeEventWithJilSerializer |    733.3 ns |     32.929 ns |     1.8606 ns | 0.3004 |    1264 B |
|                   SerializeEventWithSpanJsonSerializer |    560.2 ns |      5.998 ns |     0.3389 ns | 0.0734 |     312 B |
|                   SerializeEventWithUtf8JsonSerializer |    724.4 ns |      2.571 ns |     0.1452 ns | 0.0391 |     168 B |
|                   SerializeMobileFeedWithJilSerializer |  7,364.3 ns |    461.239 ns |    26.0609 ns | 3.7537 |   15776 B |
|              SerializeMobileFeedWithSpanJsonSerializer |  6,142.6 ns |    113.515 ns |     6.4138 ns | 1.5106 |    6360 B |
|              SerializeMobileFeedWithUtf8JsonSerializer |  7,232.7 ns |  1,126.501 ns |    63.6494 ns | 0.7553 |    3192 B |
|               SerializeMobileQuestionWithJilSerializer |    742.9 ns |     18.473 ns |     1.0438 ns | 0.5159 |    2168 B |
|          SerializeMobileQuestionWithSpanJsonSerializer |    594.2 ns |     20.856 ns |     1.1784 ns | 0.1440 |     608 B |
|          SerializeMobileQuestionWithUtf8JsonSerializer |    660.0 ns |      4.234 ns |     0.2392 ns | 0.0753 |     320 B |
|              SerializeMobileRepChangeWithJilSerializer |    499.2 ns |      3.300 ns |     0.1864 ns | 0.3004 |    1264 B |
|         SerializeMobileRepChangeWithSpanJsonSerializer |    335.5 ns |      8.639 ns |     0.4881 ns | 0.0701 |     296 B |
|         SerializeMobileRepChangeWithUtf8JsonSerializer |    357.0 ns |      4.091 ns |     0.2312 ns | 0.0377 |     160 B |
|              SerializeMobileInboxItemWithJilSerializer |    796.3 ns |     85.536 ns |     4.8329 ns | 0.4988 |    2096 B |
|         SerializeMobileInboxItemWithSpanJsonSerializer |    616.5 ns |     19.703 ns |     1.1133 ns | 0.1287 |     544 B |
|         SerializeMobileInboxItemWithUtf8JsonSerializer |    617.3 ns |     80.785 ns |     4.5645 ns | 0.0677 |     288 B |
|             SerializeMobileBadgeAwardWithJilSerializer |    625.9 ns |     48.207 ns |     2.7238 ns | 0.3443 |    1448 B |
|        SerializeMobileBadgeAwardWithSpanJsonSerializer |    566.8 ns |     15.025 ns |     0.8489 ns | 0.1154 |     488 B |
|        SerializeMobileBadgeAwardWithUtf8JsonSerializer |    580.0 ns |     10.229 ns |     0.5780 ns | 0.0601 |     256 B |
|              SerializeMobilePrivilegeWithJilSerializer |    617.1 ns |     22.766 ns |     1.2863 ns | 0.3481 |    1464 B |
|         SerializeMobilePrivilegeWithSpanJsonSerializer |    468.0 ns |     12.157 ns |     0.6869 ns | 0.1159 |     488 B |
|         SerializeMobilePrivilegeWithUtf8JsonSerializer |    468.0 ns |     11.693 ns |     0.6607 ns | 0.0601 |     256 B |
|      SerializeMobileCommunityBulletinWithJilSerializer |    904.6 ns |      9.251 ns |     0.5227 ns | 0.5465 |    2296 B |
| SerializeMobileCommunityBulletinWithSpanJsonSerializer |    727.1 ns |     37.680 ns |     2.1290 ns | 0.1726 |     728 B |
| SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    793.5 ns |     47.085 ns |     2.6604 ns | 0.0887 |     376 B |
|       SerializeMobileAssociationBonusWithJilSerializer |    348.6 ns |     37.645 ns |     2.1270 ns | 0.2036 |     856 B |
|  SerializeMobileAssociationBonusWithSpanJsonSerializer |    250.9 ns |     24.576 ns |     1.3886 ns | 0.0510 |     216 B |
|  SerializeMobileAssociationBonusWithUtf8JsonSerializer |    274.9 ns |      9.444 ns |     0.5336 ns | 0.0281 |     120 B |
|           SerializeMobileCareersJobAdWithJilSerializer |    565.6 ns |     79.103 ns |     4.4695 ns | 0.3138 |    1320 B |
|      SerializeMobileCareersJobAdWithSpanJsonSerializer |    411.8 ns |     15.082 ns |     0.8522 ns | 0.0815 |     344 B |
|      SerializeMobileCareersJobAdWithUtf8JsonSerializer |    389.2 ns |     13.531 ns |     0.7646 ns | 0.0434 |     184 B |
|               SerializeMobileBannerAdWithJilSerializer |    522.3 ns |     15.113 ns |     0.8539 ns | 0.3061 |    1288 B |
|          SerializeMobileBannerAdWithSpanJsonSerializer |    419.8 ns |     22.872 ns |     1.2923 ns | 0.0777 |     328 B |
|          SerializeMobileBannerAdWithUtf8JsonSerializer |    425.5 ns |     12.299 ns |     0.6949 ns | 0.0415 |     176 B |
|           SerializeMobileUpdateNoticeWithJilSerializer |    320.7 ns |     14.443 ns |     0.8161 ns | 0.1750 |     736 B |
|      SerializeMobileUpdateNoticeWithSpanJsonSerializer |    182.9 ns |     18.748 ns |     1.0593 ns | 0.0455 |     192 B |
|      SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    161.0 ns |     14.091 ns |     0.7962 ns | 0.0265 |     112 B |
|                   SerializeFlagOptionWithJilSerializer |    860.5 ns |     86.922 ns |     4.9113 ns | 0.5903 |    2480 B |
|              SerializeFlagOptionWithSpanJsonSerializer |    741.3 ns |     74.915 ns |     4.2328 ns | 0.1955 |     824 B |
|              SerializeFlagOptionWithUtf8JsonSerializer |    735.8 ns |     61.232 ns |     3.4597 ns | 0.0982 |     416 B |
|                    SerializeInboxItemWithJilSerializer |  3,341.7 ns |    542.357 ns |    30.6442 ns | 1.0719 |    4504 B |
|               SerializeInboxItemWithSpanJsonSerializer |  2,759.6 ns |    135.978 ns |     7.6830 ns | 0.4539 |    1912 B |
|               SerializeInboxItemWithUtf8JsonSerializer |  3,367.9 ns |    134.526 ns |     7.6010 ns | 0.2289 |     968 B |
|                         SerializeInfoWithJilSerializer |  3,701.2 ns |    229.408 ns |    12.9620 ns | 1.6937 |    7120 B |
|                    SerializeInfoWithSpanJsonSerializer |  2,619.8 ns |    284.591 ns |    16.0799 ns | 0.5074 |    2144 B |
|                    SerializeInfoWithUtf8JsonSerializer |  3,676.0 ns |     53.611 ns |     3.0291 ns | 0.3281 |    1392 B |
|                  SerializeNetworkUserWithJilSerializer |  1,366.7 ns |    144.750 ns |     8.1786 ns | 0.5474 |    2304 B |
