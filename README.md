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
Frequency=3906245 Hz, Resolution=256.0003 ns, Timer=TSC
.NET Core SDK=2.1.300-preview2-008530
  [Host]   : .NET Core 2.1.0-preview3-26411-01 (CoreCLR 4.6.26411.01, CoreFX 4.6.26411.01), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26411-01 (CoreCLR 4.6.26411.01, CoreFX 4.6.26411.01), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                   Method |        Mean |         Error |      StdDev |  Gen 0 | Allocated |
|--------------------------------------------------------- |------------:|--------------:|------------:|-------:|----------:|
|              DeserializeTagSynonymWithJsonSpanSerializer |  1,006.8 ns |    14.8390 ns |   0.8384 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |  1,261.3 ns |    12.9715 ns |   0.7329 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  3,011.0 ns |    36.4044 ns |   2.0569 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  2,407.8 ns |    21.9513 ns |   1.2403 ns | 0.2022 |     864 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  3,487.7 ns |   157.2580 ns |   8.8854 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    592.0 ns |    30.7624 ns |   1.7381 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    347.6 ns |     5.3986 ns |   0.3050 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    523.2 ns |    11.4546 ns |   0.6472 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,930.1 ns |   254.0496 ns |  14.3543 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  6,519.7 ns |    49.5951 ns |   2.8022 ns | 0.1526 |     648 B |
|                    DeserializeUserWithUtf8JsonSerializer |  4,264.9 ns |   116.6180 ns |   6.5891 ns | 0.1373 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,154.3 ns |    11.2753 ns |   0.6371 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,635.7 ns |    23.8684 ns |   1.3486 ns | 0.0801 |     344 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,429.9 ns |    14.3859 ns |   0.8128 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    660.2 ns |    11.1807 ns |   0.6317 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    411.3 ns |    14.3390 ns |   0.8102 ns | 0.0281 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    581.8 ns |     1.7716 ns |   0.1001 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    295.6 ns |     5.8057 ns |   0.3280 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    184.6 ns |     1.0095 ns |   0.0570 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    290.8 ns |     9.2534 ns |   0.5228 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,238.6 ns |   136.2887 ns |   7.7006 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  4,284.7 ns |    42.0327 ns |   2.3749 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  5,105.2 ns |   393.0693 ns |  22.2091 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    379.2 ns |     6.5909 ns |   0.3724 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    259.1 ns |     4.7521 ns |   0.2685 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    480.9 ns |    12.2231 ns |   0.6906 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,221.5 ns |    40.8183 ns |   2.3063 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,236.5 ns |    16.5574 ns |   0.9355 ns | 0.1793 |     760 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  1,965.5 ns |    71.2891 ns |   4.0280 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    454.0 ns |     7.3513 ns |   0.4154 ns | 0.0453 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    585.3 ns |     4.9508 ns |   0.2797 ns | 0.0219 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    702.1 ns |     7.5256 ns |   0.4252 ns | 0.0219 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,883.3 ns |    61.4920 ns |   3.4744 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  4,934.2 ns |    70.3583 ns |   3.9754 ns | 0.3662 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  4,691.2 ns |   166.4630 ns |   9.4055 ns | 0.3433 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    278.4 ns |     4.0597 ns |   0.2294 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    170.0 ns |     2.0569 ns |   0.1162 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    243.2 ns |     3.7349 ns |   0.2110 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    352.8 ns |     3.0947 ns |   0.1749 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    213.3 ns |     1.2301 ns |   0.0695 ns | 0.0436 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    371.4 ns |    14.2535 ns |   0.8053 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    412.1 ns |     2.2992 ns |   0.1299 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    242.9 ns |     7.4490 ns |   0.4209 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    388.3 ns |     1.9691 ns |   0.1113 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  2,432.3 ns |     2.6870 ns |   0.1518 ns | 0.1717 |     736 B |
|                    SerializeRelatedSiteWithJilSerializer |    381.5 ns |     8.1065 ns |   0.4580 ns | 0.2017 |     848 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    239.7 ns |     2.7088 ns |   0.1531 ns | 0.0510 |     216 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    222.1 ns |     2.5176 ns |   0.1422 ns | 0.0284 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,090.4 ns |    11.6869 ns |   0.6603 ns | 0.5856 |    2464 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,164.7 ns |    12.5965 ns |   0.7117 ns | 0.2632 |    1112 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,053.2 ns |    20.7707 ns |   1.1736 ns | 0.1106 |     472 B |
|                         SerializeNoticeWithJilSerializer |    647.2 ns |     7.3667 ns |   0.4162 ns | 0.2012 |     848 B |
|                    SerializeNoticeWithJsonSpanSerializer |    444.8 ns |     3.5117 ns |   0.1984 ns | 0.0529 |     224 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    596.5 ns |    12.3932 ns |   0.7002 ns | 0.0296 |     128 B |
|                  SerializeMigrationInfoWithJilSerializer |  3,066.2 ns |    16.7442 ns |   0.9461 ns | 0.9995 |    4208 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  2,731.6 ns |   256.3501 ns |  14.4843 ns | 0.4616 |    1944 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,985.6 ns |    23.6444 ns |   1.3360 ns | 0.1945 |     824 B |
|                     SerializeBadgeCountWithJilSerializer |    216.3 ns |     1.6579 ns |   0.0937 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    186.6 ns |     3.3397 ns |   0.1887 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    212.8 ns |     1.2477 ns |   0.0705 ns | 0.0207 |      88 B |
|                        SerializeStylingWithJilSerializer |    365.0 ns |     2.4614 ns |   0.1391 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    195.7 ns |     5.5481 ns |   0.3135 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    165.2 ns |     0.8507 ns |   0.0481 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    321.4 ns |    24.9685 ns |   1.4108 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    241.2 ns |     9.6346 ns |   0.5444 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    282.1 ns |     1.5695 ns |   0.0887 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    575.9 ns |     0.2666 ns |   0.0151 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    653.8 ns |     2.9785 ns |   0.1683 ns | 0.0582 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    916.8 ns |     1.8457 ns |   0.1043 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    489.6 ns |    20.1477 ns |   1.1384 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    555.0 ns |     3.1034 ns |   0.1753 ns | 0.0105 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |  1,077.9 ns |    28.2363 ns |   1.5954 ns | 0.0095 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  9,089.4 ns |   678.5336 ns |  38.3384 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer | 10,453.9 ns |   229.9439 ns |  12.9923 ns | 0.5341 |    2296 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  9,463.4 ns |    76.6904 ns |   4.3331 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,745.1 ns |    18.6633 ns |   1.0545 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,208.9 ns |     7.7792 ns |   0.4395 ns | 0.1488 |     632 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,766.4 ns |   176.6429 ns |   9.9806 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,353.0 ns |   152.1793 ns |   8.5984 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  3,064.2 ns |   100.2935 ns |   5.6668 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,325.0 ns |    25.5625 ns |   1.4443 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    315.9 ns |    12.4883 ns |   0.7056 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    198.6 ns |     4.2537 ns |   0.2403 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    339.0 ns |     9.3920 ns |   0.5307 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    596.4 ns |    11.8815 ns |   0.6713 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    673.3 ns |     9.3595 ns |   0.5288 ns | 0.0486 |     208 B |
|                   DeserializeEventWithUtf8JsonSerializer |    964.5 ns |    30.4644 ns |   1.7213 ns | 0.0362 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 23,620.2 ns |   527.7726 ns |  29.8201 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 14,164.7 ns |   154.9941 ns |   8.7575 ns | 1.2512 |    5280 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 13,623.3 ns |   282.3050 ns |  15.9508 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,278.3 ns |    17.9722 ns |   1.0155 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |  1,340.0 ns |    21.8407 ns |   1.2340 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,122.4 ns |    26.3448 ns |   1.4885 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    604.3 ns |    16.7993 ns |   0.9492 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    342.9 ns |     3.5687 ns |   0.2016 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    579.0 ns |    14.8672 ns |   0.8400 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,089.8 ns |    38.1915 ns |   2.1579 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |  1,253.3 ns |   101.2103 ns |   5.7186 ns | 0.0839 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,011.2 ns |    27.9285 ns |   1.5780 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    886.3 ns |    13.3678 ns |   0.7553 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |    945.3 ns |    51.9726 ns |   2.9365 ns | 0.0877 |     376 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |    999.4 ns |    14.9093 ns |   0.8424 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    861.7 ns |    80.2124 ns |   4.5321 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    541.1 ns |    51.0433 ns |   2.8840 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    819.6 ns |    50.8623 ns |   2.8738 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,573.3 ns |    20.7262 ns |   1.1711 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,926.2 ns |    34.6858 ns |   1.9598 ns | 0.1259 |     544 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,420.4 ns |    23.9911 ns |   1.3555 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    423.3 ns |     3.0613 ns |   0.1730 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    245.3 ns |     0.5416 ns |   0.0306 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    368.1 ns |     5.9422 ns |   0.3357 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    664.9 ns |     4.4835 ns |   0.2533 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    406.9 ns |     3.8686 ns |   0.2186 ns | 0.0644 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    694.0 ns |     3.1196 ns |   0.1763 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    763.2 ns |    32.3016 ns |   1.8251 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    437.3 ns |     1.1208 ns |   0.0633 ns | 0.0682 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    685.6 ns |     7.8739 ns |   0.4449 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    309.2 ns |     5.7823 ns |   0.3267 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    206.8 ns |     8.1334 ns |   0.4596 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    342.6 ns |     2.4620 ns |   0.1391 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,630.8 ns |    53.1688 ns |   3.0041 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,338.4 ns |    82.2228 ns |   4.6457 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,572.4 ns |    39.1272 ns |   2.2108 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,602.5 ns |    21.2655 ns |   1.2015 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  6,290.2 ns |   237.3818 ns |  13.4125 ns | 0.4272 |    1816 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  5,351.9 ns |    66.3220 ns |   3.7473 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,884.1 ns |   103.0529 ns |   5.8227 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  6,955.2 ns |    48.9046 ns |   2.7632 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  6,157.6 ns |   166.7712 ns |   9.4229 ns | 0.4272 |    1816 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,643.7 ns |    18.2073 ns |   1.0287 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  2,099.6 ns |   106.4189 ns |   6.0129 ns | 0.0725 |     312 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  1,980.5 ns |     7.0006 ns |   0.3955 ns | 0.0572 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,321.7 ns |    37.2791 ns |   2.1063 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  5,138.0 ns |   263.9493 ns |  14.9136 ns | 0.3967 |    1688 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  5,183.1 ns |   199.5263 ns |  11.2736 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  7,955.5 ns |   362.6805 ns |  20.4921 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  8,165.1 ns |   151.2670 ns |   8.5469 ns | 0.4883 |    2104 B |
|                    DeserializePostWithUtf8JsonSerializer |  8,109.9 ns |   127.3068 ns |   7.1931 ns | 0.4120 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    339.1 ns |     5.1409 ns |   0.2905 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    202.4 ns |     4.0113 ns |   0.2266 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    357.5 ns |     2.8089 ns |   0.1587 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 41,912.5 ns |   587.1393 ns |  33.1745 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 38,567.1 ns | 1,200.5239 ns |  67.8318 ns | 2.0752 |    8872 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 34,961.6 ns | 2,453.0668 ns | 138.6028 ns | 1.8921 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,226.0 ns |   118.7873 ns |   6.7117 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,684.1 ns |    56.2273 ns |   3.1769 ns | 0.1945 |     832 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,222.6 ns |    87.7012 ns |   4.9553 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    845.6 ns |     8.6301 ns |   0.4876 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    946.5 ns |    40.7755 ns |   2.3039 ns | 0.0629 |     272 B |
|              DeserializeReputationWithUtf8JsonSerializer |  1,247.5 ns |     5.1433 ns |   0.2906 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    693.7 ns |    10.2349 ns |   0.5783 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    714.2 ns |     8.5438 ns |   0.4827 ns | 0.0296 |     128 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    921.2 ns |     3.9972 ns |   0.2259 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  3,052.5 ns |   124.2438 ns |   7.0200 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  3,393.1 ns |    59.1557 ns |   3.3424 ns | 0.2708 |    1144 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  3,297.7 ns |   142.9571 ns |   8.0773 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,420.7 ns |    28.0123 ns |   1.5827 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  5,823.1 ns |   291.7250 ns |  16.4830 ns | 0.2747 |    1168 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  5,553.2 ns |   108.4254 ns |   6.1262 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,042.8 ns |    24.9753 ns |   1.4112 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    636.5 ns |    17.4269 ns |   0.9847 ns | 0.0753 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,020.2 ns |    42.0282 ns |   2.3747 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,555.3 ns |    87.4035 ns |   4.9385 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  3,022.2 ns |    10.3792 ns |   0.5864 ns | 0.1831 |     784 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  3,280.0 ns |    85.8745 ns |   4.8521 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    916.3 ns |     3.2863 ns |   0.1857 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    907.8 ns |     4.0058 ns |   0.2263 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |  1,175.4 ns |    20.5695 ns |   1.1622 ns | 0.0648 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,291.5 ns |    20.2983 ns |   1.1469 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    782.5 ns |     4.2277 ns |   0.2389 ns | 0.0830 |     352 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,203.8 ns |     6.5609 ns |   0.3707 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    747.5 ns |     3.4654 ns |   0.1958 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    707.6 ns |     4.7762 ns |   0.2699 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    623.3 ns |    13.8718 ns |   0.7838 ns | 0.0811 |     344 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    715.4 ns |    35.9130 ns |   2.0292 ns | 0.0353 |     152 B |
|                   SerializeAccountMergeWithJilSerializer |    572.9 ns |     3.3783 ns |   0.1909 ns | 0.2050 |     864 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    445.9 ns |     1.0818 ns |   0.0611 ns | 0.0567 |     240 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    595.3 ns |     9.0644 ns |   0.5122 ns | 0.0315 |     136 B |
|                         SerializeAnswerWithJilSerializer |  5,674.4 ns |   173.9917 ns |   9.8309 ns | 2.1057 |    8840 B |
|                    SerializeAnswerWithJsonSpanSerializer |  5,741.6 ns |    98.1870 ns |   5.5477 ns | 1.0452 |    4392 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  6,299.7 ns |    97.1405 ns |   5.4886 ns | 0.4501 |    1904 B |
|                          SerializeBadgeWithJilSerializer |    995.0 ns |     4.0619 ns |   0.2295 ns | 0.5608 |    2360 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,081.5 ns |     1.9098 ns |   0.1079 ns | 0.2193 |     928 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,012.5 ns |     5.8250 ns |   0.3291 ns | 0.0992 |     424 B |
|                        SerializeCommentWithJilSerializer |  1,926.4 ns |    27.3989 ns |   1.5481 ns | 1.0262 |    4320 B |
|                   SerializeCommentWithJsonSpanSerializer |  2,130.0 ns |    10.5634 ns |   0.5969 ns | 0.4158 |    1760 B |
|                   SerializeCommentWithUtf8JsonSerializer |  2,092.0 ns |    73.2871 ns |   4.1409 ns | 0.1793 |     760 B |
|                          SerializeErrorWithJilSerializer |    332.0 ns |     3.4466 ns |   0.1947 ns | 0.1922 |     808 B |
|                     SerializeErrorWithJsonSpanSerializer |    208.9 ns |     1.3789 ns |   0.0779 ns | 0.0417 |     176 B |
|                     SerializeErrorWithUtf8JsonSerializer |    187.7 ns |     0.6131 ns |   0.0346 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    896.6 ns |    20.6878 ns |   1.1689 ns | 0.3004 |    1264 B |
|                     SerializeEventWithJsonSpanSerializer |    617.6 ns |    12.6768 ns |   0.7163 ns | 0.0734 |     312 B |
|                     SerializeEventWithUtf8JsonSerializer |    735.5 ns |    38.4755 ns |   2.1739 ns | 0.0391 |     168 B |
|                     SerializeMobileFeedWithJilSerializer |  7,657.9 ns |    52.1076 ns |   2.9442 ns | 3.7537 |   15808 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,506.9 ns |    78.6823 ns |   4.4457 ns | 1.7776 |    7472 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,395.0 ns |    27.7614 ns |   1.5686 ns | 0.7553 |    3200 B |
|                 SerializeMobileQuestionWithJilSerializer |    758.4 ns |    13.3528 ns |   0.7545 ns | 0.5159 |    2168 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    719.6 ns |     6.8954 ns |   0.3896 ns | 0.1612 |     680 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    685.4 ns |     0.8706 ns |   0.0492 ns | 0.0753 |     320 B |
|                SerializeMobileRepChangeWithJilSerializer |    513.8 ns |    75.5125 ns |   4.2666 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    363.0 ns |     3.8857 ns |   0.2196 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    368.1 ns |     3.9103 ns |   0.2209 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    784.8 ns |     6.6365 ns |   0.3750 ns | 0.4988 |    2096 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    635.3 ns |     6.6811 ns |   0.3775 ns | 0.1287 |     544 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    620.0 ns |     4.7900 ns |   0.2706 ns | 0.0658 |     280 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    623.6 ns |    37.7204 ns |   2.1313 ns | 0.3462 |    1456 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    587.5 ns |     2.6796 ns |   0.1514 ns | 0.1135 |     480 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    571.1 ns |     0.8030 ns |   0.0454 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    609.5 ns |    11.1447 ns |   0.6297 ns | 0.3462 |    1456 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    489.9 ns |     2.1756 ns |   0.1229 ns | 0.1154 |     488 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    449.1 ns |     2.9762 ns |   0.1682 ns | 0.0606 |     256 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    886.8 ns |    12.6762 ns |   0.7162 ns | 0.5426 |    2280 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    814.6 ns |     1.8659 ns |   0.1054 ns | 0.1860 |     784 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    816.5 ns |    31.0429 ns |   1.7540 ns | 0.0868 |     368 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    342.8 ns |     9.9809 ns |   0.5639 ns | 0.2036 |     856 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    259.9 ns |     0.5254 ns |   0.0297 ns | 0.0491 |     208 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    287.2 ns |     1.3768 ns |   0.0778 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    572.8 ns |     6.4558 ns |   0.3648 ns | 0.3138 |    1320 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    425.6 ns |     1.3935 ns |   0.0787 ns | 0.0815 |     344 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    391.2 ns |     0.6265 ns |   0.0354 ns | 0.0434 |     184 B |
|                 SerializeMobileBannerAdWithJilSerializer |    525.3 ns |     0.7863 ns |   0.0444 ns | 0.3061 |    1288 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    477.1 ns |     4.8774 ns |   0.2756 ns | 0.0906 |     384 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    427.6 ns |     3.6004 ns |   0.2034 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    325.4 ns |     1.9425 ns |   0.1098 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    186.4 ns |     8.9885 ns |   0.5079 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    166.4 ns |     3.7667 ns |   0.2128 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    872.6 ns |    19.3584 ns |   1.0938 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    793.4 ns |    14.2963 ns |   0.8078 ns | 0.2012 |     848 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    768.0 ns |    12.0611 ns |   0.6815 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  3,400.1 ns |     5.4672 ns |   0.3089 ns | 1.0719 |    4504 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  3,053.3 ns |    51.3743 ns |   2.9027 ns | 0.5302 |    2232 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  3,358.9 ns |   115.8755 ns |   6.5472 ns | 0.2289 |     976 B |
|                           SerializeInfoWithJilSerializer |  3,631.2 ns |    48.1219 ns |   2.7190 ns | 1.6937 |    7112 B |
|                      SerializeInfoWithJsonSpanSerializer |  3,343.5 ns |    50.5460 ns |   2.8559 ns | 0.6027 |    2536 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,728.7 ns |   191.9536 ns |  10.8457 ns | 0.3319 |    1408 B |
|                    SerializeNetworkUserWithJilSerializer |  1,345.9 ns |    83.8429 ns |   4.7373 ns | 0.5474 |    2304 B |
|               SerializeNetworkUserWithJsonSpanSerializer |  1,332.2 ns |    56.8231 ns |   3.2106 ns | 0.1984 |     840 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,576.3 ns |    22.2080 ns |   1.2548 ns | 0.0935 |     400 B |
|                   SerializeNotificationWithJilSerializer |  3,184.3 ns |    66.0714 ns |   3.7332 ns | 1.0414 |    4376 B |
|              SerializeNotificationWithJsonSpanSerializer |  2,914.0 ns |    20.4840 ns |   1.1574 ns | 0.4883 |    2064 B |
|              SerializeNotificationWithUtf8JsonSerializer |  3,133.9 ns |    72.6223 ns |   4.1033 ns | 0.2098 |     888 B |
|                           SerializePostWithJilSerializer |  4,645.3 ns |    75.3293 ns |   4.2562 ns | 2.0218 |    8488 B |
|                      SerializePostWithJsonSpanSerializer |  4,938.9 ns |   206.0282 ns |  11.6410 ns | 0.9384 |    3968 B |
|                      SerializePostWithUtf8JsonSerializer |  5,236.3 ns |    35.2469 ns |   1.9915 ns | 0.4044 |    1704 B |
|                      SerializePrivilegeWithJilSerializer |    347.5 ns |     1.3236 ns |   0.0748 ns | 0.1979 |     832 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    199.6 ns |     2.2707 ns |   0.1283 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    191.7 ns |     3.1868 ns |   0.1801 ns | 0.0265 |     112 B |
|                       SerializeQuestionWithJilSerializer | 19,550.1 ns |   207.2558 ns |  11.7103 ns | 7.3242 |   30784 B |
|                  SerializeQuestionWithJsonSpanSerializer | 19,952.6 ns |   409.5623 ns |  23.1410 ns | 3.6011 |   15144 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 22,928.5 ns |   472.9796 ns |  26.7242 ns | 1.4954 |    6360 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,818.1 ns |    14.1436 ns |   0.7991 ns | 1.0300 |    4328 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  2,092.4 ns |    65.5807 ns |   3.7054 ns | 0.4158 |    1752 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  2,099.2 ns |    31.2095 ns |   1.7634 ns | 0.1793 |     760 B |
|                     SerializeReputationWithJilSerializer |    806.6 ns |     7.1662 ns |   0.4049 ns | 0.3309 |    1392 B |
|                SerializeReputationWithJsonSpanSerializer |    770.8 ns |     2.7733 ns |   0.1567 ns | 0.1001 |     424 B |
|                SerializeReputationWithUtf8JsonSerializer |    885.3 ns |     9.5449 ns |   0.5393 ns | 0.0544 |     232 B |
|              SerializeReputationHistoryWithJilSerializer |    648.8 ns |    13.5386 ns |   0.7650 ns | 0.3176 |    1336 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    576.9 ns |     7.2386 ns |   0.4090 ns | 0.0887 |     376 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    743.6 ns |     5.1154 ns |   0.2890 ns | 0.0448 |     192 B |
|                       SerializeRevisionWithJilSerializer |  1,850.4 ns |    64.6810 ns |   3.6546 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,874.4 ns |    25.5096 ns |   1.4413 ns | 0.3662 |    1544 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,867.4 ns |    27.7833 ns |   1.5698 ns | 0.1526 |     648 B |
|                  SerializeSearchExcerptWithJilSerializer |  3,360.8 ns |    53.1680 ns |   3.0041 ns | 1.1444 |    4816 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  3,453.8 ns |    35.3972 ns |   2.0000 ns | 0.5608 |    2368 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  4,071.0 ns |    54.0699 ns |   3.0550 ns | 0.2441 |    1040 B |
|                    SerializeShallowUserWithJilSerializer |    619.6 ns |     3.3838 ns |   0.1912 ns | 0.3500 |    1472 B |
|               SerializeShallowUserWithJsonSpanSerializer |    646.0 ns |    19.3566 ns |   1.0937 ns | 0.1364 |     576 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    608.3 ns |     8.4746 ns |   0.4788 ns | 0.0639 |     272 B |
|                  SerializeSuggestedEditWithJilSerializer |  2,137.8 ns |    25.2600 ns |   1.4272 ns | 0.8965 |    3768 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  2,039.8 ns |    22.3174 ns |   1.2610 ns | 0.3204 |    1360 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  2,397.0 ns |    24.5141 ns |   1.3851 ns | 0.1411 |     600 B |
|                            SerializeTagWithJilSerializer |    877.8 ns |    11.6623 ns |   0.6589 ns | 0.3290 |    1384 B |
|                       SerializeTagWithJsonSpanSerializer |    740.5 ns |    11.6661 ns |   0.6592 ns | 0.1173 |     496 B |
|                       SerializeTagWithUtf8JsonSerializer |    846.5 ns |    20.1156 ns |   1.1366 ns | 0.0544 |     232 B |
|                       SerializeTagScoreWithJilSerializer |    750.4 ns |    20.3012 ns |   1.1471 ns | 0.5140 |    2160 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    846.1 ns |     2.4418 ns |   0.1380 ns | 0.1783 |     752 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    737.5 ns |     6.4667 ns |   0.3654 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |  1,034.0 ns |    14.4054 ns |   0.8139 ns | 0.3147 |    1328 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    816.9 ns |    15.1904 ns |   0.8583 ns | 0.0906 |     384 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |  1,153.7 ns |    27.2209 ns |   1.5380 ns | 0.0477 |     208 B |
|                        SerializeTagWikiWithJilSerializer |  2,170.7 ns |    18.8598 ns |   1.0656 ns | 1.0071 |    4240 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  2,103.8 ns |     9.7054 ns |   0.5484 ns | 0.4005 |    1688 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  2,329.6 ns |   112.1320 ns |   6.3357 ns | 0.1717 |     736 B |
|                         SerializeTopTagWithJilSerializer |    418.9 ns |     4.6634 ns |   0.2635 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    390.8 ns |     3.5504 ns |   0.2006 ns | 0.0777 |     328 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    364.5 ns |     1.7059 ns |   0.0964 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  2,782.4 ns |    16.5581 ns |   0.9356 ns | 1.0529 |    4424 B |
|                      SerializeUserWithJsonSpanSerializer |  2,652.8 ns |   160.8539 ns |   9.0885 ns | 0.4501 |    1896 B |
|                      SerializeUserWithUtf8JsonSerializer |  3,208.1 ns |    44.8693 ns |   2.5352 ns | 0.2174 |     928 B |
|                   SerializeUserTimelineWithJilSerializer |    972.5 ns |    40.8537 ns |   2.3083 ns | 0.5016 |    2112 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    944.2 ns |    27.6559 ns |   1.5626 ns | 0.1364 |     576 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |  1,108.4 ns |    25.4373 ns |   1.4373 ns | 0.0706 |     304 B |
|                SerializeWritePermissionWithJilSerializer |    418.3 ns |     5.9661 ns |   0.3371 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    368.4 ns |     2.1738 ns |   0.1228 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    359.8 ns |     3.8283 ns |   0.2163 ns | 0.0453 |     192 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    264.2 ns |     3.6862 ns |   0.2083 ns | 0.1407 |     592 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    193.1 ns |     1.5914 ns |   0.0899 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    214.7 ns |     0.5574 ns |   0.0315 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,745.9 ns |    12.4200 ns |   0.7018 ns | 0.9651 |    4056 B |
|                      SerializeSiteWithJsonSpanSerializer |  2,282.0 ns |    22.4489 ns |   1.2684 ns | 0.4044 |    1704 B |
