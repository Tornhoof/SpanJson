# SpanJson
SpanJson
Sandbox for playing around with Span and JSON Serialization.
This is basically the ValueStringBuilder from CoreFx with the TryFormat API for formatting values with Span<char>.
The actual serializers are a T4 Template (BclFormatter.tt).

Performance Issues:
* Integer Formatting: derived from UTF8Json as the CoreCLR version is two times slower.
* Integer Parsing: derived from UTF8Json
* DateTime/DateTimeOffset Parser: derived from UTf8Parser with modifications to support less than 7 digit fractions

What works so far:
Serialization/Deserialization of objects composed of basic base class library types and lists/arrays of them

Todo:

* Streams
* Adding direct utf8 support
``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17134
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3906246 Hz, Resolution=256.0003 ns, Timer=TSC
.NET Core SDK=2.1.300-preview2-008530
  [Host]   : .NET Core 2.1.0-preview2-26406-04 (CoreCLR 4.6.26406.07, CoreFX 4.6.26406.04), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview2-26406-04 (CoreCLR 4.6.26406.07, CoreFX 4.6.26406.04), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                   Method |        Mean |         Error |      StdDev |  Gen 0 | Allocated |
|--------------------------------------------------------- |------------:|--------------:|------------:|-------:|----------:|
|              DeserializeTagSynonymWithJsonSpanSerializer |    525.3 ns |    37.5760 ns |   2.1231 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |    754.7 ns |     6.0852 ns |   0.3438 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  3,019.1 ns |   109.6990 ns |   6.1982 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  2,325.0 ns |   508.6900 ns |  28.7419 ns | 0.2022 |     856 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  2,894.5 ns |   402.6573 ns |  22.7509 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    590.0 ns |     0.8016 ns |   0.0453 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    415.7 ns |    33.8236 ns |   1.9111 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    548.8 ns |    47.8597 ns |   2.7042 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,854.2 ns |   348.0500 ns |  19.6655 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  2,964.8 ns |   102.8116 ns |   5.8090 ns | 0.1526 |     648 B |
|                    DeserializeUserWithUtf8JsonSerializer |  3,249.5 ns |   450.9106 ns |  25.4773 ns | 0.1411 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,133.8 ns |   136.7779 ns |   7.7282 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,061.7 ns |   123.4869 ns |   6.9772 ns | 0.0801 |     336 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,216.5 ns |    21.0831 ns |   1.1912 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    659.1 ns |     2.0103 ns |   0.1136 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    502.7 ns |    24.9093 ns |   1.4074 ns | 0.0277 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    603.2 ns |     9.9721 ns |   0.5634 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    299.4 ns |    76.9633 ns |   4.3486 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    188.4 ns |    13.3205 ns |   0.7526 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    304.4 ns |   119.9897 ns |   6.7796 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,307.7 ns |   637.8668 ns |  36.0407 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  2,805.3 ns |    76.1582 ns |   4.3031 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  3,224.2 ns |    17.2978 ns |   0.9774 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    387.8 ns |    34.1226 ns |   1.9280 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    292.0 ns |    27.7024 ns |   1.5652 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    490.1 ns |     2.3750 ns |   0.1342 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,250.9 ns |   171.9204 ns |   9.7138 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,677.7 ns |   211.3203 ns |  11.9400 ns | 0.1793 |     760 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  1,989.4 ns |     7.4298 ns |   0.4198 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    469.4 ns |     5.9846 ns |   0.3381 ns | 0.0453 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    286.3 ns |     2.0469 ns |   0.1157 ns | 0.0224 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    416.5 ns |     0.4141 ns |   0.0234 ns | 0.0224 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,876.5 ns |   283.1346 ns |  15.9976 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  3,069.9 ns |   257.5684 ns |  14.5531 ns | 0.3700 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  3,657.1 ns |    71.5720 ns |   4.0440 ns | 0.3471 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    275.5 ns |     4.6894 ns |   0.2650 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    178.6 ns |    16.1923 ns |   0.9149 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    247.1 ns |     1.4018 ns |   0.0792 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    363.4 ns |     3.8500 ns |   0.2175 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    237.8 ns |     1.2012 ns |   0.0679 ns | 0.0434 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    374.3 ns |    38.6772 ns |   2.1853 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    411.6 ns |     2.7111 ns |   0.1532 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    262.5 ns |     4.1204 ns |   0.2328 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    399.4 ns |    25.6492 ns |   1.4492 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  1,766.1 ns |   552.9425 ns |  31.2423 ns | 0.1659 |     704 B |
|                    SerializeRelatedSiteWithJilSerializer |    373.0 ns |     4.8934 ns |   0.2765 ns | 0.2036 |     856 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    250.9 ns |     4.1054 ns |   0.2320 ns | 0.0510 |     216 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    233.4 ns |     0.8265 ns |   0.0467 ns | 0.0281 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,109.6 ns |    54.5386 ns |   3.0815 ns | 0.5875 |    2472 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,077.8 ns |    19.9147 ns |   1.1252 ns | 0.2174 |     920 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,091.8 ns |     5.4338 ns |   0.3070 ns | 0.1106 |     472 B |
|                         SerializeNoticeWithJilSerializer |    396.8 ns |     1.8344 ns |   0.1036 ns | 0.1998 |     840 B |
|                    SerializeNoticeWithJsonSpanSerializer |    276.1 ns |     8.7639 ns |   0.4952 ns | 0.0510 |     216 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    356.8 ns |     1.5636 ns |   0.0883 ns | 0.0262 |     112 B |
|                  SerializeMigrationInfoWithJilSerializer |  2,199.9 ns |    47.5830 ns |   2.6885 ns | 0.9918 |    4176 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  1,693.9 ns |    39.5428 ns |   2.2342 ns | 0.3757 |    1584 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,043.1 ns |    42.0496 ns |   2.3759 ns | 0.1831 |     784 B |
|                     SerializeBadgeCountWithJilSerializer |    221.1 ns |     9.0516 ns |   0.5114 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    204.9 ns |    10.9493 ns |   0.6187 ns | 0.0360 |     152 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    213.1 ns |     2.3424 ns |   0.1324 ns | 0.0207 |      88 B |
|                        SerializeStylingWithJilSerializer |    357.4 ns |     0.4427 ns |   0.0250 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    189.7 ns |     0.8422 ns |   0.0476 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    168.7 ns |     1.9569 ns |   0.1106 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    325.5 ns |    41.0034 ns |   2.3168 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    254.6 ns |     1.2581 ns |   0.0711 ns | 0.0567 |     240 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    273.5 ns |     1.8041 ns |   0.1019 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    549.3 ns |     3.3623 ns |   0.1900 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    455.0 ns |     2.1705 ns |   0.1226 ns | 0.0587 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    675.4 ns |     5.7700 ns |   0.3260 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    430.0 ns |     4.1442 ns |   0.2342 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    323.6 ns |     3.9666 ns |   0.2241 ns | 0.0110 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    392.4 ns |     1.2634 ns |   0.0714 ns | 0.0110 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  8,947.2 ns |    90.9825 ns |   5.1407 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer |  8,244.6 ns |   374.4550 ns |  21.1574 ns | 0.5341 |    2304 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  8,035.9 ns |   445.7950 ns |  25.1882 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,709.2 ns |    20.8187 ns |   1.1763 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,651.9 ns |   103.7803 ns |   5.8638 ns | 0.1450 |     616 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,781.5 ns |   157.5223 ns |   8.9003 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,206.7 ns |   120.1186 ns |   6.7869 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  3,039.5 ns |   194.8432 ns |  11.0090 ns | 0.2174 |     928 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,179.4 ns |   196.5587 ns |  11.1059 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    316.5 ns |     0.5187 ns |   0.0293 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    210.2 ns |     6.4476 ns |   0.3643 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    339.2 ns |     8.7848 ns |   0.4964 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    594.0 ns |     3.8346 ns |   0.2167 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    461.2 ns |     7.5709 ns |   0.4278 ns | 0.0510 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |    664.7 ns |    10.6167 ns |   0.5999 ns | 0.0372 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 23,931.2 ns | 1,356.2855 ns |  76.6326 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 14,490.2 ns |   382.5908 ns |  21.6171 ns | 1.2512 |    5296 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 13,844.0 ns |   303.2397 ns |  17.1336 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,290.6 ns |    23.8282 ns |   1.3463 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |  1,068.1 ns |    21.2781 ns |   1.2023 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,099.4 ns |     3.6464 ns |   0.2060 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    571.6 ns |     3.2365 ns |   0.1829 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    422.5 ns |     6.0085 ns |   0.3395 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    570.7 ns |    13.7203 ns |   0.7752 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,075.7 ns |     8.6244 ns |   0.4873 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |    959.0 ns |    29.0601 ns |   1.6420 ns | 0.0839 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |    991.4 ns |    46.1777 ns |   2.6091 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    893.3 ns |    13.8884 ns |   0.7847 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |    930.2 ns |    10.3297 ns |   0.5836 ns | 0.0887 |     376 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |    998.3 ns |     5.9401 ns |   0.3356 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    863.7 ns |    14.9019 ns |   0.8420 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    718.6 ns |    61.8554 ns |   3.4949 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    814.0 ns |    29.6406 ns |   1.6748 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,515.2 ns |    62.3670 ns |   3.5239 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,406.7 ns |   178.4094 ns |  10.0805 ns | 0.1316 |     560 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,410.7 ns |    56.6337 ns |   3.1999 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    423.1 ns |     3.1756 ns |   0.1794 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    262.8 ns |    92.4189 ns |   5.2218 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    374.4 ns |     4.7285 ns |   0.2672 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    661.8 ns |     5.0547 ns |   0.2856 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    535.7 ns |    10.8877 ns |   0.6152 ns | 0.0639 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    664.6 ns |     7.9809 ns |   0.4509 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    763.0 ns |     1.2611 ns |   0.0713 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    508.6 ns |    64.4926 ns |   3.6440 ns | 0.0677 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    685.3 ns |    10.4178 ns |   0.5886 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    315.2 ns |     0.8976 ns |   0.0507 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    211.0 ns |     0.4359 ns |   0.0246 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    349.8 ns |     3.7648 ns |   0.2127 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,637.2 ns |    18.1826 ns |   1.0274 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,497.8 ns |    26.9939 ns |   1.5252 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,566.6 ns |     9.4912 ns |   0.5363 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,634.8 ns |   195.1773 ns |  11.0279 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  4,045.5 ns |   160.6338 ns |   9.0761 ns | 0.4272 |    1808 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  4,357.8 ns |   126.8030 ns |   7.1646 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,561.1 ns |   126.6764 ns |   7.1575 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  4,728.1 ns |    55.2349 ns |   3.1209 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  5,387.7 ns |    11.0651 ns |   0.6252 ns | 0.4272 |    1800 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,549.2 ns |    35.4772 ns |   2.0045 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  1,368.5 ns |    11.8552 ns |   0.6698 ns | 0.0725 |     312 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  1,440.3 ns |    41.4366 ns |   2.3412 ns | 0.0591 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,295.3 ns |   153.3857 ns |   8.6666 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  3,286.9 ns |    29.1430 ns |   1.6466 ns | 0.3967 |    1680 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  3,930.0 ns |    85.0193 ns |   4.8037 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  7,923.2 ns |   714.0158 ns |  40.3432 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  6,665.3 ns |   592.1970 ns |  33.4602 ns | 0.4883 |    2072 B |
|                    DeserializePostWithUtf8JsonSerializer |  7,024.1 ns |   246.3227 ns |  13.9177 ns | 0.4196 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    332.8 ns |     0.7747 ns |   0.0438 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    211.3 ns |     1.1937 ns |   0.0674 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    340.9 ns |     1.1513 ns |   0.0651 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 42,404.4 ns | 2,289.6444 ns | 129.3692 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 27,025.4 ns | 6,017.5807 ns | 340.0045 ns | 2.1057 |    8848 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 28,411.5 ns |   320.4280 ns |  18.1048 ns | 1.9226 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,017.6 ns |    22.0567 ns |   1.2462 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,655.4 ns |   116.2581 ns |   6.5688 ns | 0.1984 |     848 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,058.6 ns |   259.1151 ns |  14.6405 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    836.1 ns |    22.3968 ns |   1.2655 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    852.4 ns |     5.8006 ns |   0.3277 ns | 0.0639 |     272 B |
|              DeserializeReputationWithUtf8JsonSerializer |    946.3 ns |    13.0793 ns |   0.7390 ns | 0.0429 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    653.4 ns |     2.2016 ns |   0.1244 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    479.3 ns |     1.3101 ns |   0.0740 ns | 0.0277 |     120 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    638.3 ns |     8.2408 ns |   0.4656 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  2,872.8 ns |    90.8698 ns |   5.1343 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  2,749.5 ns |   173.8692 ns |   9.8239 ns | 0.2670 |    1128 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  2,947.6 ns |    53.4018 ns |   3.0173 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,489.6 ns |   125.2900 ns |   7.0791 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  3,869.4 ns |   627.6165 ns |  35.4615 ns | 0.2747 |    1168 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  4,252.9 ns |    80.1681 ns |   4.5296 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,038.5 ns |     4.7760 ns |   0.2699 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    862.0 ns |    60.3687 ns |   3.4109 ns | 0.0734 |     312 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,045.7 ns |    52.7709 ns |   2.9817 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,596.7 ns |   233.6720 ns |  13.2029 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  2,287.2 ns |    87.0193 ns |   4.9168 ns | 0.1793 |     768 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  2,483.2 ns |    96.2080 ns |   5.4359 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    882.8 ns |    14.8965 ns |   0.8417 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    778.4 ns |     3.5306 ns |   0.1995 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |    886.0 ns |    30.2649 ns |   1.7100 ns | 0.0658 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,306.3 ns |     2.5560 ns |   0.1444 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    987.5 ns |    10.0477 ns |   0.5677 ns | 0.0839 |     360 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,216.1 ns |   168.0062 ns |   9.4927 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    808.2 ns |     7.3237 ns |   0.4138 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    492.8 ns |     9.2771 ns |   0.5242 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    376.4 ns |     2.6922 ns |   0.1521 ns | 0.0644 |     272 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    472.6 ns |     1.9327 ns |   0.1092 ns | 0.0358 |     152 B |
|                   SerializeAccountMergeWithJilSerializer |    361.6 ns |     4.5616 ns |   0.2577 ns | 0.2036 |     856 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    273.7 ns |     1.1166 ns |   0.0631 ns | 0.0548 |     232 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    371.5 ns |     0.7336 ns |   0.0414 ns | 0.0281 |     120 B |
|                         SerializeAnswerWithJilSerializer |  4,170.4 ns |    35.5069 ns |   2.0062 ns | 2.0981 |    8824 B |
|                    SerializeAnswerWithJsonSpanSerializer |  4,271.8 ns |    51.9292 ns |   2.9341 ns | 0.8926 |    3752 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  5,207.4 ns |   100.8750 ns |   5.6996 ns | 0.4349 |    1856 B |
|                          SerializeBadgeWithJilSerializer |    961.5 ns |     5.0858 ns |   0.2874 ns | 0.5665 |    2384 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,016.0 ns |    99.4836 ns |   5.6210 ns | 0.1926 |     816 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,047.3 ns |    26.3470 ns |   1.4887 ns | 0.1011 |     432 B |
|                        SerializeCommentWithJilSerializer |  1,680.1 ns |     9.4948 ns |   0.5365 ns | 1.0300 |    4328 B |
|                   SerializeCommentWithJsonSpanSerializer |  1,799.6 ns |    13.3379 ns |   0.7536 ns | 0.3605 |    1520 B |
|                   SerializeCommentWithUtf8JsonSerializer |  1,885.1 ns |     9.5318 ns |   0.5386 ns | 0.1793 |     760 B |
|                          SerializeErrorWithJilSerializer |    326.2 ns |     1.7257 ns |   0.0975 ns | 0.1922 |     808 B |
|                     SerializeErrorWithJsonSpanSerializer |    202.7 ns |     1.1140 ns |   0.0629 ns | 0.0417 |     176 B |
|                     SerializeErrorWithUtf8JsonSerializer |    190.5 ns |     3.1313 ns |   0.1769 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    479.2 ns |     6.2922 ns |   0.3555 ns | 0.2203 |     928 B |
|                     SerializeEventWithJsonSpanSerializer |    390.6 ns |     2.1300 ns |   0.1204 ns | 0.0739 |     312 B |
|                     SerializeEventWithUtf8JsonSerializer |    471.9 ns |     2.1312 ns |   0.1204 ns | 0.0372 |     160 B |
|                     SerializeMobileFeedWithJilSerializer |  7,386.0 ns |     1.4060 ns |   0.0794 ns | 3.7766 |   15856 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  6,895.0 ns |   144.5579 ns |   8.1678 ns | 1.5335 |    6448 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,654.6 ns |   303.5865 ns |  17.1532 ns | 0.7706 |    3240 B |
|                 SerializeMobileQuestionWithJilSerializer |    761.9 ns |    85.3761 ns |   4.8239 ns | 0.5178 |    2176 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    671.3 ns |     4.1247 ns |   0.2331 ns | 0.1478 |     624 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    676.0 ns |     3.3733 ns |   0.1906 ns | 0.0772 |     328 B |
|                SerializeMobileRepChangeWithJilSerializer |    498.1 ns |     2.0912 ns |   0.1182 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    361.4 ns |     2.0259 ns |   0.1145 ns | 0.0720 |     304 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    374.2 ns |    13.7176 ns |   0.7751 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    789.1 ns |     7.9869 ns |   0.4513 ns | 0.4988 |    2096 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    645.4 ns |     0.9523 ns |   0.0538 ns | 0.1307 |     552 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    662.7 ns |    52.9878 ns |   2.9939 ns | 0.0677 |     288 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    628.1 ns |     2.1276 ns |   0.1202 ns | 0.3462 |    1456 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    590.1 ns |     4.3527 ns |   0.2459 ns | 0.1135 |     480 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    598.2 ns |    13.2501 ns |   0.7487 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    609.5 ns |     3.8550 ns |   0.2178 ns | 0.3481 |    1464 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    491.8 ns |    33.6510 ns |   1.9013 ns | 0.1154 |     488 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    480.3 ns |    11.2056 ns |   0.6331 ns | 0.0620 |     264 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    875.6 ns |    17.3802 ns |   0.9820 ns | 0.5426 |    2280 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    786.8 ns |    11.5804 ns |   0.6543 ns | 0.1745 |     736 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    796.3 ns |    25.3448 ns |   1.4320 ns | 0.0887 |     376 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    342.4 ns |     1.6812 ns |   0.0950 ns | 0.2036 |     856 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    271.9 ns |     1.9714 ns |   0.1114 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    294.8 ns |     7.0512 ns |   0.3984 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    559.7 ns |     0.4179 ns |   0.0236 ns | 0.3138 |    1320 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    472.7 ns |     4.9077 ns |   0.2773 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    423.9 ns |    31.6521 ns |   1.7884 ns | 0.0453 |     192 B |
|                 SerializeMobileBannerAdWithJilSerializer |    534.3 ns |     9.5796 ns |   0.5413 ns | 0.3061 |    1288 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    456.6 ns |     1.8984 ns |   0.1073 ns | 0.0777 |     328 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    438.5 ns |     4.2198 ns |   0.2384 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    313.2 ns |     1.4042 ns |   0.0793 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    179.4 ns |     0.9403 ns |   0.0531 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    160.2 ns |     0.3087 ns |   0.0174 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    847.5 ns |    27.8767 ns |   1.5751 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    753.4 ns |     5.0609 ns |   0.2859 ns | 0.1879 |     792 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    762.8 ns |     5.1250 ns |   0.2896 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  2,451.6 ns |     9.5559 ns |   0.5399 ns | 1.0605 |    4464 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  2,070.9 ns |    59.3241 ns |   3.3519 ns | 0.4463 |    1880 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  2,450.6 ns |   147.3108 ns |   8.3233 ns | 0.2213 |     936 B |
|                           SerializeInfoWithJilSerializer |  2,976.4 ns |    86.4822 ns |   4.8864 ns | 1.6861 |    7080 B |
|                      SerializeInfoWithJsonSpanSerializer |  2,570.9 ns |    76.6233 ns |   4.3294 ns | 0.5188 |    2184 B |
|                      SerializeInfoWithUtf8JsonSerializer |  2,997.7 ns |    19.3240 ns |   1.0918 ns | 0.3281 |    1392 B |
|                    SerializeNetworkUserWithJilSerializer |    915.0 ns |    19.4205 ns |   1.0973 ns | 0.5445 |    2288 B |
|               SerializeNetworkUserWithJsonSpanSerializer |    922.1 ns |     1.5201 ns |   0.0859 ns | 0.1822 |     768 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,142.4 ns |    54.0888 ns |   3.0561 ns | 0.0896 |     384 B |
|                   SerializeNotificationWithJilSerializer |  2,254.2 ns |    43.3176 ns |   2.4475 ns | 1.0262 |    4320 B |
|              SerializeNotificationWithJsonSpanSerializer |  1,849.1 ns |    14.0118 ns |   0.7917 ns | 0.4063 |    1712 B |
|              SerializeNotificationWithUtf8JsonSerializer |  2,223.7 ns |     5.2386 ns |   0.2960 ns | 0.2022 |     856 B |
|                           SerializePostWithJilSerializer |  3,806.7 ns |    65.1450 ns |   3.6808 ns | 2.0142 |    8464 B |
|                      SerializePostWithJsonSpanSerializer |  3,929.3 ns |   126.2756 ns |   7.1348 ns | 0.8011 |    3384 B |
|                      SerializePostWithUtf8JsonSerializer |  4,605.7 ns |   216.3644 ns |  12.2250 ns | 0.3967 |    1680 B |
|                      SerializePrivilegeWithJilSerializer |    332.4 ns |     1.1646 ns |   0.0658 ns | 0.1979 |     832 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    203.2 ns |     0.4205 ns |   0.0238 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    184.8 ns |     2.3389 ns |   0.1321 ns | 0.0246 |     104 B |
|                       SerializeQuestionWithJilSerializer | 14,340.2 ns |    50.6322 ns |   2.8608 ns | 7.2937 |   30648 B |
|                  SerializeQuestionWithJsonSpanSerializer | 14,310.9 ns |   405.5522 ns |  22.9145 ns | 2.9907 |   12552 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 17,675.5 ns |   220.3815 ns |  12.4520 ns | 1.4648 |    6176 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,580.6 ns |    31.1529 ns |   1.7602 ns | 1.0281 |    4320 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  1,744.0 ns |   166.7483 ns |   9.4216 ns | 0.3529 |    1488 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  1,921.7 ns |    47.6881 ns |   2.6945 ns | 0.1793 |     760 B |
|                     SerializeReputationWithJilSerializer |    580.3 ns |     4.8191 ns |   0.2723 ns | 0.3290 |    1384 B |
|                SerializeReputationWithJsonSpanSerializer |    572.7 ns |    70.0754 ns |   3.9594 ns | 0.0982 |     416 B |
|                SerializeReputationWithUtf8JsonSerializer |    680.7 ns |     4.0720 ns |   0.2301 ns | 0.0544 |     232 B |
|              SerializeReputationHistoryWithJilSerializer |    435.8 ns |    11.3538 ns |   0.6415 ns | 0.3123 |    1312 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    387.0 ns |     1.4178 ns |   0.0801 ns | 0.0834 |     352 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    500.8 ns |    22.4663 ns |   1.2694 ns | 0.0429 |     184 B |
|                       SerializeRevisionWithJilSerializer |  1,544.0 ns |    17.6998 ns |   1.0001 ns | 0.9289 |    3904 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,505.9 ns |    48.4576 ns |   2.7379 ns | 0.3014 |    1272 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,643.8 ns |    76.6586 ns |   4.3314 ns | 0.1507 |     640 B |
|                  SerializeSearchExcerptWithJilSerializer |  2,292.7 ns |    27.8220 ns |   1.5720 ns | 1.1406 |    4792 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  2,385.6 ns |    40.1335 ns |   2.2676 ns | 0.4768 |    2016 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  2,973.9 ns |    12.0342 ns |   0.6800 ns | 0.2365 |    1008 B |
|                    SerializeShallowUserWithJilSerializer |    611.0 ns |     1.3258 ns |   0.0749 ns | 0.3538 |    1488 B |
|               SerializeShallowUserWithJsonSpanSerializer |    637.7 ns |    34.7960 ns |   1.9660 ns | 0.1211 |     512 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    610.0 ns |     6.2451 ns |   0.3529 ns | 0.0620 |     264 B |
|                  SerializeSuggestedEditWithJilSerializer |  1,467.1 ns |     9.0814 ns |   0.5131 ns | 0.8907 |    3744 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  1,425.2 ns |   169.3235 ns |   9.5671 ns | 0.2689 |    1136 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  1,709.9 ns |    53.4591 ns |   3.0205 ns | 0.1354 |     576 B |
|                            SerializeTagWithJilSerializer |    591.5 ns |     5.9220 ns |   0.3346 ns | 0.3290 |    1384 B |
|                       SerializeTagWithJsonSpanSerializer |    502.9 ns |     1.9886 ns |   0.1124 ns | 0.1001 |     424 B |
|                       SerializeTagWithUtf8JsonSerializer |    619.5 ns |     0.5671 ns |   0.0320 ns | 0.0525 |     224 B |
|                       SerializeTagScoreWithJilSerializer |    740.7 ns |    12.1743 ns |   0.6879 ns | 0.5178 |    2176 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    801.2 ns |    59.4243 ns |   3.3576 ns | 0.1478 |     624 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    764.8 ns |     4.7329 ns |   0.2674 ns | 0.0772 |     328 B |
|                     SerializeTagSynonymWithJilSerializer |    610.8 ns |     2.2257 ns |   0.1258 ns | 0.3099 |    1304 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    427.8 ns |     4.8872 ns |   0.2761 ns | 0.0873 |     368 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |    619.7 ns |     3.2519 ns |   0.1837 ns | 0.0429 |     184 B |
|                        SerializeTagWikiWithJilSerializer |  1,670.2 ns |    14.1848 ns |   0.8015 ns | 1.0109 |    4248 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  1,656.3 ns |    14.5622 ns |   0.8228 ns | 0.3395 |    1432 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  1,801.8 ns |    22.5829 ns |   1.2760 ns | 0.1698 |     720 B |
|                         SerializeTopTagWithJilSerializer |    412.3 ns |     2.1671 ns |   0.1224 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    385.6 ns |     1.1079 ns |   0.0626 ns | 0.0796 |     336 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    373.7 ns |     4.1947 ns |   0.2370 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  1,894.1 ns |     9.2045 ns |   0.5201 ns | 1.0490 |    4408 B |
|                      SerializeUserWithJsonSpanSerializer |  1,902.7 ns |   130.9110 ns |   7.3967 ns | 0.4311 |    1816 B |
|                      SerializeUserWithUtf8JsonSerializer |  2,279.4 ns |    13.0753 ns |   0.7388 ns | 0.2098 |     896 B |
|                   SerializeUserTimelineWithJilSerializer |    739.7 ns |     1.2580 ns |   0.0711 ns | 0.5026 |    2112 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    726.7 ns |    63.3847 ns |   3.5814 ns | 0.1345 |     568 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |    835.7 ns |     3.5915 ns |   0.2029 ns | 0.0715 |     304 B |
|                SerializeWritePermissionWithJilSerializer |    413.3 ns |    12.5944 ns |   0.7116 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    372.2 ns |     1.2879 ns |   0.0728 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    354.1 ns |     0.8134 ns |   0.0460 ns | 0.0453 |     192 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    270.3 ns |     0.8961 ns |   0.0506 ns | 0.1426 |     600 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    200.0 ns |     3.9274 ns |   0.2219 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    196.7 ns |     0.4570 ns |   0.0258 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  1,963.3 ns |     8.0025 ns |   0.4522 ns | 0.9537 |    4016 B |
|                      SerializeSiteWithJsonSpanSerializer |  1,466.2 ns |   137.7563 ns |   7.7835 ns | 0.3376 |    1424 B |
