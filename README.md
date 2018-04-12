# SpanJson
SpanJson
Sandbox for playing around with Span and JSON Serialization.
This is basically the ValueStringBuilder from CoreFx with the TryFormat API for formatting values with Span<char>.
The actual serializers are a T4 Template (BclFormatter.tt).

Performance Issues:
* Integer Formatting: derived from UTF8Json as the CoreCLR version is two times slower.
* Integer Parsing: derived from UTF8Json
* DateTime/DateTimeOffset Parser: derived from UTf8Parser with modifications to support less than 7 digit fractions

Still a few missing


TO-DO
* Improve property selection (currently we compare the propertyname for each Switch again, instead of e.g switching on the first char first and then conpare the rest, which should improve performance)
* Things like ``[IgnoreSerialize]`` and ``ShouldSerialize``
* Many edge cases
* dynamic
* More Performance work
* Adding direct utf8 support
* Streams

``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10.0.17133
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=3906250 Hz, Resolution=256.0000 ns, Timer=TSC
.NET Core SDK=2.1.300-preview2-008530
  [Host]   : .NET Core 2.1.0-preview3-26411-01 (CoreCLR 4.6.26411.01, CoreFX 4.6.26411.01), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26411-01 (CoreCLR 4.6.26411.01, CoreFX 4.6.26411.01), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                   Method |        Mean |         Error |      StdDev |  Gen 0 | Allocated |
|--------------------------------------------------------- |------------:|--------------:|------------:|-------:|----------:|
|              DeserializeTagSynonymWithJsonSpanSerializer |    979.2 ns |     8.1005 ns |   0.4577 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |  1,277.4 ns |     7.5400 ns |   0.4260 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  2,969.6 ns |    46.3795 ns |   2.6205 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  2,268.7 ns |   282.8856 ns |  15.9836 ns | 0.2022 |     856 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  3,445.1 ns |   156.3165 ns |   8.8322 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    605.9 ns |    29.3632 ns |   1.6591 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    325.5 ns |     2.2264 ns |   0.1258 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    528.1 ns |    27.0350 ns |   1.5275 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,989.1 ns |   370.0152 ns |  20.9065 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  6,334.6 ns |    22.4066 ns |   1.2660 ns | 0.1526 |     656 B |
|                    DeserializeUserWithUtf8JsonSerializer |  4,480.1 ns |   186.9738 ns |  10.5644 ns | 0.1373 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,162.0 ns |    56.9780 ns |   3.2194 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,617.1 ns |    30.7536 ns |   1.7376 ns | 0.0801 |     344 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,440.5 ns |    23.1393 ns |   1.3074 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    664.6 ns |    18.5085 ns |   1.0458 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    378.3 ns |     2.2768 ns |   0.1286 ns | 0.0281 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    603.3 ns |    65.5736 ns |   3.7050 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    301.0 ns |    11.4981 ns |   0.6497 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    159.0 ns |     1.3907 ns |   0.0786 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    370.7 ns |     3.5573 ns |   0.2010 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,516.3 ns |   510.4686 ns |  28.8424 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  4,386.9 ns |   194.2901 ns |  10.9778 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  4,048.7 ns |   172.4708 ns |   9.7449 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    381.5 ns |     7.5181 ns |   0.4248 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    244.4 ns |     3.3663 ns |   0.1902 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    476.8 ns |     7.0307 ns |   0.3972 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,274.2 ns |    86.9415 ns |   4.9124 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,147.3 ns |    57.8672 ns |   3.2696 ns | 0.1793 |     760 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  2,013.3 ns |    28.0362 ns |   1.5841 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    439.2 ns |     8.5728 ns |   0.4844 ns | 0.0453 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    531.5 ns |     9.6426 ns |   0.5448 ns | 0.0219 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    795.6 ns |     9.9164 ns |   0.5603 ns | 0.0219 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,923.8 ns |   262.4143 ns |  14.8269 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  4,668.5 ns |   245.7974 ns |  13.8880 ns | 0.3662 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  4,853.8 ns |   100.1821 ns |   5.6605 ns | 0.3433 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    279.0 ns |     4.1969 ns |   0.2371 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    153.9 ns |     2.6656 ns |   0.1506 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    270.0 ns |     5.8163 ns |   0.3286 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    350.4 ns |    10.3966 ns |   0.5874 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    185.4 ns |     3.4502 ns |   0.1949 ns | 0.0436 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    571.8 ns |    54.2682 ns |   3.0663 ns | 0.0429 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    425.5 ns |     5.8950 ns |   0.3331 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    221.3 ns |     3.4424 ns |   0.1945 ns | 0.0226 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    374.8 ns |     6.0381 ns |   0.3412 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  2,431.6 ns |    47.5727 ns |   2.6879 ns | 0.1717 |     736 B |
|                    SerializeRelatedSiteWithJilSerializer |    439.6 ns |     5.7441 ns |   0.3246 ns | 0.2017 |     848 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    256.5 ns |    10.0773 ns |   0.5694 ns | 0.0510 |     216 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    220.3 ns |     3.1241 ns |   0.1765 ns | 0.0284 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,103.0 ns |     4.0328 ns |   0.2279 ns | 0.5856 |    2464 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,246.8 ns |    14.9984 ns |   0.8474 ns | 0.2594 |    1096 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,112.4 ns |    34.3659 ns |   1.9417 ns | 0.1106 |     472 B |
|                         SerializeNoticeWithJilSerializer |    625.8 ns |     5.9373 ns |   0.3355 ns | 0.2012 |     848 B |
|                    SerializeNoticeWithJsonSpanSerializer |    455.3 ns |     4.0263 ns |   0.2275 ns | 0.0529 |     224 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    599.6 ns |     2.0894 ns |   0.1181 ns | 0.0296 |     128 B |
|                  SerializeMigrationInfoWithJilSerializer |  3,129.6 ns |    24.6106 ns |   1.3905 ns | 0.9995 |    4208 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  2,719.2 ns |   102.9959 ns |   5.8195 ns | 0.4578 |    1936 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  3,033.6 ns |   519.8687 ns |  29.3735 ns | 0.1945 |     824 B |
|                     SerializeBadgeCountWithJilSerializer |    217.5 ns |     4.9239 ns |   0.2782 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    190.7 ns |    12.4179 ns |   0.7016 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    217.9 ns |     2.4415 ns |   0.1380 ns | 0.0188 |      80 B |
|                        SerializeStylingWithJilSerializer |    365.7 ns |     5.9609 ns |   0.3368 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    194.1 ns |     7.1462 ns |   0.4038 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    170.5 ns |     4.4842 ns |   0.2534 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    319.7 ns |    13.1596 ns |   0.7435 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    249.8 ns |     3.8258 ns |   0.2162 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    272.8 ns |     3.6813 ns |   0.2080 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    575.7 ns |     8.8594 ns |   0.5006 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    627.8 ns |    16.6656 ns |   0.9416 ns | 0.0582 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |  1,075.6 ns |    17.6791 ns |   0.9989 ns | 0.0572 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    463.4 ns |     3.6861 ns |   0.2083 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    558.5 ns |     8.3074 ns |   0.4694 ns | 0.0105 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    682.5 ns |     8.8916 ns |   0.5024 ns | 0.0105 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  9,314.9 ns |   160.6717 ns |   9.0782 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer |  9,947.3 ns |   458.5593 ns |  25.9095 ns | 0.5341 |    2296 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  9,767.7 ns |    98.1523 ns |   5.5458 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,761.7 ns |    30.0000 ns |   1.6951 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,095.8 ns |    73.5887 ns |   4.1579 ns | 0.1469 |     624 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,816.9 ns |    78.1990 ns |   4.4184 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,235.9 ns |    49.4366 ns |   2.7933 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  2,894.9 ns |    74.0060 ns |   4.1815 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,450.4 ns | 1,282.9705 ns |  72.4902 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    320.4 ns |    13.7564 ns |   0.7773 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    174.2 ns |     4.0177 ns |   0.2270 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    335.6 ns |    11.7712 ns |   0.6651 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    603.2 ns |    24.9636 ns |   1.4105 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    655.6 ns |     2.7857 ns |   0.1574 ns | 0.0505 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |  1,086.9 ns |    46.1673 ns |   2.6085 ns | 0.0362 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 24,517.3 ns |   109.4088 ns |   6.1818 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 15,572.3 ns | 3,333.9697 ns | 188.3755 ns | 1.2512 |    5264 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 13,558.9 ns |   171.3832 ns |   9.6835 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,262.7 ns |    11.0950 ns |   0.6269 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |  1,299.1 ns |    29.7964 ns |   1.6836 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,091.2 ns |    21.5067 ns |   1.2152 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    577.4 ns |    20.1185 ns |   1.1367 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    310.1 ns |     4.3216 ns |   0.2442 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    579.7 ns |     2.7440 ns |   0.1550 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,057.1 ns |     5.1803 ns |   0.2927 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |  1,214.0 ns |    22.1935 ns |   1.2540 ns | 0.0839 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,212.3 ns |   192.6366 ns |  10.8843 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    906.1 ns |    10.0198 ns |   0.5661 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |  1,078.6 ns |   168.2159 ns |   9.5045 ns | 0.0877 |     376 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |  1,178.7 ns |     4.9979 ns |   0.2824 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    862.5 ns |     2.4388 ns |   0.1378 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    630.0 ns |    13.3466 ns |   0.7541 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    858.2 ns |    39.8097 ns |   2.2493 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,534.0 ns |    63.3413 ns |   3.5789 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,902.7 ns |   141.4782 ns |   7.9938 ns | 0.1297 |     560 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,637.6 ns |    28.3625 ns |   1.6025 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    424.5 ns |     8.1246 ns |   0.4591 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    222.3 ns |     8.1341 ns |   0.4596 ns | 0.0246 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    388.9 ns |     2.6439 ns |   0.1494 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    685.8 ns |     7.0271 ns |   0.3970 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    380.6 ns |     6.0438 ns |   0.3415 ns | 0.0644 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    700.4 ns |    10.4953 ns |   0.5930 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    773.5 ns |    16.4580 ns |   0.9299 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    403.1 ns |    62.9045 ns |   3.5542 ns | 0.0682 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    699.5 ns |    39.8096 ns |   2.2493 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    310.5 ns |     6.9322 ns |   0.3917 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    177.5 ns |    13.6475 ns |   0.7711 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    339.4 ns |     9.6128 ns |   0.5431 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,662.3 ns |    66.5142 ns |   3.7582 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,403.0 ns |    30.2557 ns |   1.7095 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,881.4 ns |   161.2707 ns |   9.1121 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,638.9 ns |   509.7015 ns |  28.7991 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  5,666.3 ns |   520.2147 ns |  29.3931 ns | 0.4272 |    1800 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  5,412.9 ns |   381.8642 ns |  21.5760 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,658.8 ns |   364.2397 ns |  20.5802 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  7,310.4 ns | 3,344.1700 ns | 188.9518 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  6,150.8 ns |   167.0741 ns |   9.4400 ns | 0.4272 |    1808 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,601.9 ns |    55.1783 ns |   3.1177 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  2,133.1 ns |    94.1868 ns |   5.3217 ns | 0.0725 |     304 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  2,169.1 ns |   138.7736 ns |   7.8410 ns | 0.0572 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,239.3 ns |    54.4520 ns |   3.0766 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  4,935.1 ns |   298.5663 ns |  16.8695 ns | 0.3967 |    1688 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  5,083.2 ns |   112.0511 ns |   6.3311 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  7,553.1 ns |   195.6169 ns |  11.0527 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  7,832.6 ns |    89.5243 ns |   5.0583 ns | 0.4883 |    2088 B |
|                    DeserializePostWithUtf8JsonSerializer |  8,637.7 ns |   670.4753 ns |  37.8831 ns | 0.4120 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    344.9 ns |    20.3630 ns |   1.1505 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    175.4 ns |     3.9295 ns |   0.2220 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    349.7 ns |    21.6887 ns |   1.2255 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 42,600.8 ns | 1,227.3811 ns |  69.3493 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 37,953.2 ns | 7,595.1384 ns | 429.1394 ns | 2.0752 |    8872 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 35,517.4 ns | 2,281.1824 ns | 128.8910 ns | 1.8921 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,099.9 ns |   107.0382 ns |   6.0479 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,562.6 ns |    64.7412 ns |   3.6580 ns | 0.2022 |     856 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,330.4 ns |   689.2674 ns |  38.9449 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    859.1 ns |    18.2907 ns |   1.0335 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    946.5 ns |    50.4271 ns |   2.8492 ns | 0.0629 |     272 B |
|              DeserializeReputationWithUtf8JsonSerializer |  1,373.3 ns |    60.3927 ns |   3.4123 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    671.0 ns |    31.4946 ns |   1.7795 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    696.1 ns |    45.0654 ns |   2.5463 ns | 0.0277 |     120 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    928.3 ns |    19.9725 ns |   1.1285 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  2,926.2 ns |   174.1988 ns |   9.8426 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  3,345.7 ns |   101.1271 ns |   5.7139 ns | 0.2670 |    1136 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  3,221.3 ns |   143.0076 ns |   8.0802 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,620.4 ns |   350.2955 ns |  19.7923 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  5,609.4 ns |   166.7632 ns |   9.4224 ns | 0.2747 |    1160 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  5,722.3 ns |   339.9213 ns |  19.2062 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,036.2 ns |    20.0007 ns |   1.1301 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    610.3 ns |    12.9307 ns |   0.7306 ns | 0.0753 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,067.7 ns |    15.6589 ns |   0.8848 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,707.6 ns |   217.7771 ns |  12.3048 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  2,856.4 ns |    10.6132 ns |   0.5997 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  3,298.1 ns |    44.6196 ns |   2.5211 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    971.0 ns |    17.7027 ns |   1.0002 ns | 0.0877 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    907.4 ns |    20.6487 ns |   1.1667 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |  1,406.3 ns |   296.3143 ns |  16.7423 ns | 0.0648 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,311.1 ns |    13.0713 ns |   0.7386 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    734.4 ns |    33.8545 ns |   1.9128 ns | 0.0830 |     352 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,414.2 ns |    16.6876 ns |   0.9429 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    788.9 ns |    21.6778 ns |   1.2248 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    738.4 ns |    18.7066 ns |   1.0570 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    623.6 ns |    42.7433 ns |   2.4151 ns | 0.0811 |     344 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    682.5 ns |    21.8270 ns |   1.2333 ns | 0.0353 |     152 B |
|                   SerializeAccountMergeWithJilSerializer |    600.3 ns |    12.3642 ns |   0.6986 ns | 0.2050 |     864 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    481.1 ns |    12.4871 ns |   0.7055 ns | 0.0563 |     240 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    653.5 ns |    21.3156 ns |   1.2044 ns | 0.0296 |     128 B |
|                         SerializeAnswerWithJilSerializer |  5,452.3 ns |    78.5127 ns |   4.4361 ns | 2.1057 |    8848 B |
|                    SerializeAnswerWithJsonSpanSerializer |  5,841.2 ns |   226.6108 ns |  12.8039 ns | 1.0452 |    4408 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  6,530.0 ns |   188.4277 ns |  10.6465 ns | 0.4425 |    1888 B |
|                          SerializeBadgeWithJilSerializer |    981.5 ns |    22.9208 ns |   1.2951 ns | 0.5608 |    2360 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,118.2 ns |    44.9685 ns |   2.5408 ns | 0.2232 |     944 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,035.8 ns |    45.2343 ns |   2.5558 ns | 0.0992 |     424 B |
|                        SerializeCommentWithJilSerializer |  1,908.9 ns |    44.2358 ns |   2.4994 ns | 1.0300 |    4336 B |
|                   SerializeCommentWithJsonSpanSerializer |  2,191.3 ns |   198.1159 ns |  11.1939 ns | 0.4158 |    1752 B |
|                   SerializeCommentWithUtf8JsonSerializer |  2,176.2 ns |    57.9697 ns |   3.2754 ns | 0.1793 |     760 B |
|                          SerializeErrorWithJilSerializer |    335.3 ns |     7.9404 ns |   0.4486 ns | 0.1941 |     816 B |
|                     SerializeErrorWithJsonSpanSerializer |    201.5 ns |     3.2798 ns |   0.1853 ns | 0.0398 |     168 B |
|                     SerializeErrorWithUtf8JsonSerializer |    192.4 ns |    13.2178 ns |   0.7468 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    733.7 ns |    20.8435 ns |   1.1777 ns | 0.3004 |    1264 B |
|                     SerializeEventWithJsonSpanSerializer |    570.3 ns |    25.7044 ns |   1.4523 ns | 0.0734 |     312 B |
|                     SerializeEventWithUtf8JsonSerializer |    786.3 ns |     5.3654 ns |   0.3032 ns | 0.0391 |     168 B |
|                     SerializeMobileFeedWithJilSerializer |  7,587.7 ns |   272.7781 ns |  15.4125 ns | 3.7537 |   15784 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,723.0 ns |   218.0909 ns |  12.3225 ns | 1.7700 |    7464 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,470.0 ns |   127.3896 ns |   7.1978 ns | 0.7553 |    3200 B |
|                 SerializeMobileQuestionWithJilSerializer |    757.5 ns |    49.9078 ns |   2.8199 ns | 0.5159 |    2168 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    686.5 ns |    13.3763 ns |   0.7558 ns | 0.1631 |     688 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    673.7 ns |     6.0917 ns |   0.3442 ns | 0.0753 |     320 B |
|                SerializeMobileRepChangeWithJilSerializer |    507.6 ns |     1.5160 ns |   0.0857 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    372.6 ns |     5.9332 ns |   0.3352 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    381.2 ns |     4.2882 ns |   0.2423 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    796.3 ns |    12.0171 ns |   0.6790 ns | 0.5007 |    2104 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    671.6 ns |    55.8915 ns |   3.1580 ns | 0.1287 |     544 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    646.9 ns |     3.9580 ns |   0.2236 ns | 0.0677 |     288 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    633.4 ns |    26.7859 ns |   1.5135 ns | 0.3462 |    1456 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    605.4 ns |    19.3967 ns |   1.0960 ns | 0.1135 |     480 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    593.2 ns |    20.4878 ns |   1.1576 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    616.0 ns |    14.0295 ns |   0.7927 ns | 0.3462 |    1456 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    495.0 ns |    26.3695 ns |   1.4899 ns | 0.1154 |     488 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    461.7 ns |     8.9063 ns |   0.5032 ns | 0.0606 |     256 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    892.3 ns |    26.9458 ns |   1.5225 ns | 0.5465 |    2296 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    830.6 ns |    57.7535 ns |   3.2632 ns | 0.1860 |     784 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    794.6 ns |    18.9983 ns |   1.0734 ns | 0.0868 |     368 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    342.7 ns |     5.0714 ns |   0.2865 ns | 0.2017 |     848 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    276.9 ns |     4.5943 ns |   0.2596 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    290.8 ns |     4.7805 ns |   0.2701 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    566.8 ns |    10.6503 ns |   0.6018 ns | 0.3119 |    1312 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    454.1 ns |    15.6255 ns |   0.8829 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    400.3 ns |     5.9277 ns |   0.3349 ns | 0.0434 |     184 B |
|                 SerializeMobileBannerAdWithJilSerializer |    530.2 ns |     2.2285 ns |   0.1259 ns | 0.3080 |    1296 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    510.7 ns |     4.9628 ns |   0.2804 ns | 0.0925 |     392 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    455.7 ns |     5.8823 ns |   0.3324 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    321.2 ns |     8.4379 ns |   0.4768 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    190.3 ns |     6.6131 ns |   0.3737 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    165.9 ns |     6.1634 ns |   0.3482 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    857.8 ns |    13.9704 ns |   0.7894 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    825.6 ns |    30.3804 ns |   1.7165 ns | 0.2108 |     888 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    767.5 ns |    27.9660 ns |   1.5801 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  3,371.3 ns |    77.9306 ns |   4.4032 ns | 1.0719 |    4504 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  3,182.8 ns |    72.8379 ns |   4.1155 ns | 0.5302 |    2240 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  3,356.7 ns |   102.5147 ns |   5.7923 ns | 0.2289 |     976 B |
|                           SerializeInfoWithJilSerializer |  3,690.9 ns |    88.7677 ns |   5.0155 ns | 1.6937 |    7112 B |
|                      SerializeInfoWithJsonSpanSerializer |  3,447.2 ns |    81.2465 ns |   4.5906 ns | 0.5989 |    2528 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,685.8 ns |    51.5872 ns |   2.9148 ns | 0.3319 |    1400 B |
|                    SerializeNetworkUserWithJilSerializer |  1,360.5 ns |    92.5800 ns |   5.2309 ns | 0.5455 |    2296 B |
|               SerializeNetworkUserWithJsonSpanSerializer |  1,339.1 ns |    38.7038 ns |   2.1868 ns | 0.1984 |     840 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,617.7 ns |    37.4288 ns |   2.1148 ns | 0.0935 |     400 B |
|                   SerializeNotificationWithJilSerializer |  3,168.9 ns |    18.4439 ns |   1.0421 ns | 1.0338 |    4344 B |
|              SerializeNotificationWithJsonSpanSerializer |  2,939.1 ns |    56.8725 ns |   3.2134 ns | 0.4959 |    2096 B |
|              SerializeNotificationWithUtf8JsonSerializer |  3,159.7 ns |    34.0244 ns |   1.9224 ns | 0.2098 |     888 B |
|                           SerializePostWithJilSerializer |  4,927.3 ns |   283.1688 ns |  15.9996 ns | 2.0142 |    8480 B |
|                      SerializePostWithJsonSpanSerializer |  5,001.7 ns |    99.1381 ns |   5.6015 ns | 0.9384 |    3968 B |
|                      SerializePostWithUtf8JsonSerializer |  5,440.6 ns |   227.3910 ns |  12.8480 ns | 0.4044 |    1704 B |
|                      SerializePrivilegeWithJilSerializer |    337.8 ns |    10.5984 ns |   0.5988 ns | 0.1979 |     832 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    204.0 ns |     1.7706 ns |   0.1000 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    197.0 ns |     0.1171 ns |   0.0066 ns | 0.0246 |     104 B |
|                       SerializeQuestionWithJilSerializer | 19,367.6 ns |   268.8945 ns |  15.1930 ns | 7.3242 |   30792 B |
|                  SerializeQuestionWithJsonSpanSerializer | 20,279.4 ns |   198.7963 ns |  11.2324 ns | 3.5706 |   15112 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 23,385.0 ns |   196.8473 ns |  11.1222 ns | 1.4954 |    6376 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,919.7 ns |   113.6066 ns |   6.4190 ns | 1.0338 |    4344 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  2,110.0 ns |     9.5346 ns |   0.5387 ns | 0.4120 |    1736 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  2,112.9 ns |    12.4706 ns |   0.7046 ns | 0.1793 |     760 B |
|                     SerializeReputationWithJilSerializer |    799.6 ns |     4.1273 ns |   0.2332 ns | 0.3290 |    1384 B |
|                SerializeReputationWithJsonSpanSerializer |    745.1 ns |     1.8951 ns |   0.1071 ns | 0.1001 |     424 B |
|                SerializeReputationWithUtf8JsonSerializer |    901.1 ns |     2.5167 ns |   0.1422 ns | 0.0544 |     232 B |
|              SerializeReputationHistoryWithJilSerializer |    646.6 ns |    18.1970 ns |   1.0282 ns | 0.3138 |    1320 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    603.8 ns |     2.2389 ns |   0.1265 ns | 0.0906 |     384 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    742.2 ns |     2.7387 ns |   0.1547 ns | 0.0467 |     200 B |
|                       SerializeRevisionWithJilSerializer |  1,912.8 ns |    48.8316 ns |   2.7591 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,884.8 ns |   351.4288 ns |  19.8564 ns | 0.3643 |    1536 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,852.8 ns |    61.1264 ns |   3.4538 ns | 0.1526 |     648 B |
|                  SerializeSearchExcerptWithJilSerializer |  3,515.9 ns |   186.9480 ns |  10.5629 ns | 1.1444 |    4816 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  3,580.3 ns |    38.6739 ns |   2.1851 ns | 0.5646 |    2384 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  4,064.8 ns |    29.8686 ns |   1.6876 ns | 0.2441 |    1032 B |
|                    SerializeShallowUserWithJilSerializer |    607.4 ns |     5.2056 ns |   0.2941 ns | 0.3519 |    1480 B |
|               SerializeShallowUserWithJsonSpanSerializer |    683.5 ns |     3.3953 ns |   0.1918 ns | 0.1364 |     576 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    637.3 ns |     3.4056 ns |   0.1924 ns | 0.0639 |     272 B |
|                  SerializeSuggestedEditWithJilSerializer |  2,150.6 ns |    93.6101 ns |   5.2891 ns | 0.8965 |    3776 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  2,132.4 ns |    49.3294 ns |   2.7872 ns | 0.3166 |    1344 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  2,378.9 ns |    45.6878 ns |   2.5814 ns | 0.1373 |     592 B |
|                            SerializeTagWithJilSerializer |    815.6 ns |     1.5182 ns |   0.0858 ns | 0.3309 |    1392 B |
|                       SerializeTagWithJsonSpanSerializer |    783.4 ns |    14.2144 ns |   0.8031 ns | 0.1173 |     496 B |
|                       SerializeTagWithUtf8JsonSerializer |    824.0 ns |     0.7001 ns |   0.0396 ns | 0.0544 |     232 B |
|                       SerializeTagScoreWithJilSerializer |    742.2 ns |    10.3757 ns |   0.5862 ns | 0.5140 |    2160 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    841.2 ns |     3.9470 ns |   0.2230 ns | 0.1726 |     728 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    779.0 ns |     6.3634 ns |   0.3595 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |  1,038.8 ns |    15.3695 ns |   0.8684 ns | 0.3147 |    1328 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    796.7 ns |     1.5867 ns |   0.0897 ns | 0.0906 |     384 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |  1,070.1 ns |     8.4647 ns |   0.4783 ns | 0.0477 |     208 B |
|                        SerializeTagWikiWithJilSerializer |  2,077.7 ns |    11.7964 ns |   0.6665 ns | 1.0109 |    4256 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  2,143.4 ns |     5.4594 ns |   0.3085 ns | 0.4005 |    1696 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  2,348.2 ns |    15.4628 ns |   0.8737 ns | 0.1717 |     728 B |
|                         SerializeTopTagWithJilSerializer |    409.8 ns |    14.2975 ns |   0.8078 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    379.1 ns |     0.2373 ns |   0.0134 ns | 0.0758 |     320 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    377.2 ns |     0.0481 ns |   0.0027 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  2,721.0 ns |     9.5765 ns |   0.5411 ns | 1.0529 |    4424 B |
|                      SerializeUserWithJsonSpanSerializer |  2,744.0 ns |   116.4994 ns |   6.5824 ns | 0.4501 |    1896 B |
|                      SerializeUserWithUtf8JsonSerializer |  3,235.2 ns |     9.7947 ns |   0.5534 ns | 0.2174 |     928 B |
|                   SerializeUserTimelineWithJilSerializer |  1,037.9 ns |   118.9038 ns |   6.7183 ns | 0.5016 |    2112 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    908.9 ns |    20.9054 ns |   1.1812 ns | 0.1383 |     584 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |  1,068.4 ns |    11.5539 ns |   0.6528 ns | 0.0687 |     296 B |
|                SerializeWritePermissionWithJilSerializer |    415.9 ns |     8.4974 ns |   0.4801 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    375.7 ns |     3.6408 ns |   0.2057 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    371.5 ns |     6.3343 ns |   0.3579 ns | 0.0472 |     200 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    258.5 ns |     2.8965 ns |   0.1637 ns | 0.1407 |     592 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    200.7 ns |     0.9243 ns |   0.0522 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    204.1 ns |     1.1238 ns |   0.0635 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,737.8 ns |    30.7803 ns |   1.7391 ns | 0.9613 |    4048 B |
|                      SerializeSiteWithJsonSpanSerializer |  2,274.1 ns |    75.4844 ns |   4.2650 ns | 0.4044 |    1704 B |
