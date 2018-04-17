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
|              DeserializeTagSynonymWithJsonSpanSerializer |    531.4 ns |    78.2599 ns |   4.4218 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |    749.2 ns |    11.4584 ns |   0.6474 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  2,993.0 ns |   147.9065 ns |   8.3570 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  2,535.2 ns | 1,035.9870 ns |  58.5352 ns | 0.2060 |     872 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  2,858.7 ns |     5.6487 ns |   0.3192 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    610.3 ns |    32.3952 ns |   1.8304 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    409.9 ns |     2.2619 ns |   0.1278 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    531.1 ns |   180.1204 ns |  10.1771 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,940.5 ns |   108.0999 ns |   6.1078 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  2,975.7 ns |   118.5044 ns |   6.6957 ns | 0.1526 |     648 B |
|                    DeserializeUserWithUtf8JsonSerializer |  3,218.9 ns |    41.3941 ns |   2.3388 ns | 0.1411 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,142.1 ns |    20.4858 ns |   1.1575 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,032.9 ns |    78.2455 ns |   4.4210 ns | 0.0801 |     336 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,178.9 ns |    13.1071 ns |   0.7406 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    654.8 ns |    13.2126 ns |   0.7465 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    494.9 ns |   112.8547 ns |   6.3765 ns | 0.0277 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    575.1 ns |     0.9361 ns |   0.0529 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    283.7 ns |     3.5585 ns |   0.2011 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    184.1 ns |     0.9180 ns |   0.0519 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    299.4 ns |     1.5623 ns |   0.0883 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,331.1 ns |   248.1289 ns |  14.0197 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  2,508.8 ns |    34.3032 ns |   1.9382 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  3,220.2 ns |   263.2969 ns |  14.8768 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    372.3 ns |     5.8810 ns |   0.3323 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    277.1 ns |     8.3196 ns |   0.4701 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    468.1 ns |     5.6320 ns |   0.3182 ns | 0.0453 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,241.2 ns |    43.3720 ns |   2.4506 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,458.8 ns |    55.2383 ns |   3.1211 ns | 0.1774 |     752 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  1,977.5 ns |    93.8988 ns |   5.3055 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    438.7 ns |    13.0187 ns |   0.7356 ns | 0.0453 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    293.4 ns |     9.4158 ns |   0.5320 ns | 0.0224 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    435.2 ns |    10.8766 ns |   0.6146 ns | 0.0224 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  4,009.7 ns |   114.6564 ns |   6.4783 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  2,857.7 ns |    32.3454 ns |   1.8276 ns | 0.3700 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  3,631.4 ns |    27.3572 ns |   1.5457 ns | 0.3471 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    275.4 ns |     1.5087 ns |   0.0852 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    178.1 ns |     2.1509 ns |   0.1215 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    259.2 ns |    88.5817 ns |   5.0050 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    351.7 ns |     0.7864 ns |   0.0444 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    241.1 ns |     1.5186 ns |   0.0858 ns | 0.0434 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    370.2 ns |     2.7345 ns |   0.1545 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    430.9 ns |     4.2044 ns |   0.2376 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    261.6 ns |     0.6394 ns |   0.0361 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    377.3 ns |     5.3700 ns |   0.3034 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  1,751.9 ns |   243.0211 ns |  13.7311 ns | 0.1717 |     728 B |
|                    SerializeRelatedSiteWithJilSerializer |    379.7 ns |     4.4087 ns |   0.2491 ns | 0.2036 |     856 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    234.7 ns |     0.3689 ns |   0.0208 ns | 0.0494 |     208 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    218.9 ns |     3.6158 ns |   0.2043 ns | 0.0284 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,090.7 ns |    21.4170 ns |   1.2101 ns | 0.5836 |    2456 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,157.8 ns |    13.6535 ns |   0.7714 ns | 0.2632 |    1112 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,042.6 ns |     3.7088 ns |   0.2096 ns | 0.1087 |     464 B |
|                         SerializeNoticeWithJilSerializer |    408.2 ns |    95.9006 ns |   5.4186 ns | 0.2017 |     848 B |
|                    SerializeNoticeWithJsonSpanSerializer |    257.9 ns |     1.7917 ns |   0.1012 ns | 0.0510 |     216 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    371.5 ns |     9.4559 ns |   0.5343 ns | 0.0281 |     120 B |
|                  SerializeMigrationInfoWithJilSerializer |  2,258.1 ns |    74.4222 ns |   4.2050 ns | 0.9995 |    4208 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  1,914.2 ns |    12.9367 ns |   0.7309 ns | 0.4501 |    1904 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,045.4 ns |    71.1412 ns |   4.0196 ns | 0.1907 |     808 B |
|                     SerializeBadgeCountWithJilSerializer |    216.9 ns |     0.9205 ns |   0.0520 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    188.4 ns |     0.1250 ns |   0.0071 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    237.3 ns |    71.5099 ns |   4.0404 ns | 0.0205 |      88 B |
|                        SerializeStylingWithJilSerializer |    360.2 ns |     1.9894 ns |   0.1124 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    184.6 ns |     0.9945 ns |   0.0562 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    165.2 ns |     3.4034 ns |   0.1923 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    318.4 ns |     1.8068 ns |   0.1021 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    241.6 ns |     0.5000 ns |   0.0282 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    264.6 ns |     0.7341 ns |   0.0415 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    616.9 ns |    12.7846 ns |   0.7224 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    404.8 ns |     1.6407 ns |   0.0927 ns | 0.0587 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    654.3 ns |     1.0579 ns |   0.0598 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    459.3 ns |     4.9468 ns |   0.2795 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    322.0 ns |     6.7010 ns |   0.3786 ns | 0.0110 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    410.0 ns |     5.0733 ns |   0.2867 ns | 0.0110 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  8,765.3 ns |   313.7638 ns |  17.7282 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer |  7,178.1 ns | 2,061.3869 ns | 116.4722 ns | 0.5417 |    2288 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  7,746.3 ns |   560.7451 ns |  31.6831 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,717.7 ns |    39.5459 ns |   2.2344 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,524.5 ns |    50.4596 ns |   2.8511 ns | 0.1450 |     616 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,812.5 ns |     1.1835 ns |   0.0669 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,185.4 ns |   168.7486 ns |   9.5346 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  2,897.1 ns |   294.6347 ns |  16.6474 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,115.1 ns |   129.1534 ns |   7.2974 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    327.9 ns |    14.6919 ns |   0.8301 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    219.3 ns |     0.9610 ns |   0.0543 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    336.2 ns |     2.2146 ns |   0.1251 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    617.6 ns |    15.4687 ns |   0.8740 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    480.0 ns |    13.4381 ns |   0.7593 ns | 0.0505 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |    704.9 ns |     0.9321 ns |   0.0527 ns | 0.0372 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 24,438.1 ns | 5,275.8651 ns | 298.0962 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 13,118.9 ns | 1,370.6005 ns |  77.4415 ns | 1.2512 |    5272 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 14,694.3 ns |   396.8132 ns |  22.4207 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,278.6 ns |    10.7367 ns |   0.6066 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |    980.3 ns |    34.8587 ns |   1.9696 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,065.7 ns |    37.6316 ns |   2.1263 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    593.6 ns |     9.9523 ns |   0.5623 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    426.2 ns |    55.4031 ns |   3.1304 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    560.8 ns |     3.3277 ns |   0.1880 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,041.4 ns |     6.3447 ns |   0.3585 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |    936.1 ns |    12.6875 ns |   0.7169 ns | 0.0849 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,010.4 ns |    12.0660 ns |   0.6818 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    917.3 ns |     6.6126 ns |   0.3736 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |    916.1 ns |     2.1706 ns |   0.1226 ns | 0.0906 |     384 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |  1,010.1 ns |    40.9602 ns |   2.3143 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    851.6 ns |    10.0412 ns |   0.5673 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    744.6 ns |     9.4345 ns |   0.5331 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    809.9 ns |     3.0501 ns |   0.1723 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,493.5 ns |    27.7317 ns |   1.5669 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,283.9 ns |    12.0126 ns |   0.6787 ns | 0.1259 |     536 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,397.5 ns |     4.9541 ns |   0.2799 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    442.2 ns |    64.6475 ns |   3.6527 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    263.2 ns |     0.9087 ns |   0.0513 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    362.4 ns |     2.1646 ns |   0.1223 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    689.1 ns |     8.6311 ns |   0.4877 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    651.4 ns |    25.3720 ns |   1.4336 ns | 0.0639 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    669.7 ns |    13.5142 ns |   0.7636 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    765.4 ns |    12.9861 ns |   0.7337 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    452.5 ns |     1.4300 ns |   0.0808 ns | 0.0682 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    685.9 ns |     9.8367 ns |   0.5558 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    315.6 ns |     9.1756 ns |   0.5184 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    215.2 ns |     0.8820 ns |   0.0498 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    338.8 ns |     4.1040 ns |   0.2319 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,692.8 ns |    24.0638 ns |   1.3596 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,422.7 ns |    91.9495 ns |   5.1953 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,600.8 ns |   494.6699 ns |  27.9498 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,846.1 ns |   102.7251 ns |   5.8042 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  3,688.8 ns |   113.5917 ns |   6.4181 ns | 0.4272 |    1808 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  4,234.6 ns |    23.7413 ns |   1.3414 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,722.4 ns |   276.4173 ns |  15.6181 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  4,555.7 ns |    86.3273 ns |   4.8777 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  5,309.8 ns |    25.4744 ns |   1.4394 ns | 0.4272 |    1816 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,623.4 ns |     9.0979 ns |   0.5140 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  1,338.9 ns |   116.1384 ns |   6.5620 ns | 0.0725 |     304 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  1,470.7 ns |     3.3593 ns |   0.1898 ns | 0.0591 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,296.6 ns |   224.8129 ns |  12.7023 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  3,087.4 ns |    94.3851 ns |   5.3329 ns | 0.4005 |    1688 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  4,006.0 ns |    14.6009 ns |   0.8250 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  7,978.8 ns |   423.8891 ns |  23.9505 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  6,258.6 ns |    96.3363 ns |   5.4432 ns | 0.4959 |    2096 B |
|                    DeserializePostWithUtf8JsonSerializer |  7,216.9 ns | 1,860.5435 ns | 105.1242 ns | 0.4196 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    324.8 ns |    15.0600 ns |   0.8509 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    222.1 ns |     0.2858 ns |   0.0162 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    333.3 ns |     3.2318 ns |   0.1826 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 41,423.6 ns |   543.2726 ns |  30.6959 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 25,088.2 ns | 1,621.5323 ns |  91.6196 ns | 2.1057 |    8872 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 27,634.1 ns | 1,425.4818 ns |  80.5424 ns | 1.9226 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,282.5 ns |    75.2717 ns |   4.2530 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,693.6 ns |    44.1608 ns |   2.4952 ns | 0.1984 |     848 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  2,920.3 ns |    74.6486 ns |   4.2178 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    831.4 ns |     3.0780 ns |   0.1739 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    877.9 ns |    10.5514 ns |   0.5962 ns | 0.0620 |     264 B |
|              DeserializeReputationWithUtf8JsonSerializer |    962.7 ns |    13.0543 ns |   0.7376 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    655.1 ns |     0.6422 ns |   0.0363 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    511.1 ns |   165.7252 ns |   9.3638 ns | 0.0334 |     144 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    650.7 ns |     3.1367 ns |   0.1772 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  2,979.5 ns |    65.7753 ns |   3.7164 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  2,430.7 ns |    26.0935 ns |   1.4743 ns | 0.2670 |    1136 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  2,866.2 ns |   187.8925 ns |  10.6163 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,526.6 ns |   149.7623 ns |   8.4618 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  3,683.1 ns |    54.1923 ns |   3.0620 ns | 0.2785 |    1176 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  4,309.5 ns |   600.2335 ns |  33.9143 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,038.7 ns |    47.5224 ns |   2.6851 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    850.8 ns |     6.2209 ns |   0.3515 ns | 0.0753 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,029.6 ns |     1.1501 ns |   0.0650 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,576.5 ns |   104.8769 ns |   5.9257 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  2,221.7 ns |    39.7186 ns |   2.2442 ns | 0.1831 |     784 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  2,484.0 ns |   185.9390 ns |  10.5059 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    931.1 ns |     2.2654 ns |   0.1280 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    721.2 ns |     4.4638 ns |   0.2522 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |    939.3 ns |     2.3737 ns |   0.1341 ns | 0.0658 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,303.0 ns |   140.3242 ns |   7.9286 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    980.9 ns |    48.6162 ns |   2.7469 ns | 0.0839 |     360 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,189.7 ns |     3.0726 ns |   0.1736 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    755.7 ns |     3.0189 ns |   0.1706 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    494.9 ns |     2.0084 ns |   0.1135 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    406.7 ns |    60.0513 ns |   3.3930 ns | 0.0801 |     336 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    448.6 ns |     0.9759 ns |   0.0551 ns | 0.0339 |     144 B |
|                   SerializeAccountMergeWithJilSerializer |    357.8 ns |     0.7640 ns |   0.0432 ns | 0.2055 |     864 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    256.3 ns |     1.2282 ns |   0.0694 ns | 0.0548 |     232 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    386.2 ns |     6.8896 ns |   0.3893 ns | 0.0300 |     128 B |
|                         SerializeAnswerWithJilSerializer |  4,223.9 ns |    61.3372 ns |   3.4657 ns | 2.1057 |    8848 B |
|                    SerializeAnswerWithJsonSpanSerializer |  4,528.7 ns |    43.7530 ns |   2.4721 ns | 1.0376 |    4360 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  5,074.2 ns |   178.6187 ns |  10.0923 ns | 0.4425 |    1864 B |
|                          SerializeBadgeWithJilSerializer |    974.6 ns |    71.6424 ns |   4.0479 ns | 0.5646 |    2376 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,087.4 ns |     2.5642 ns |   0.1449 ns | 0.2232 |     944 B |
|                     SerializeBadgeWithUtf8JsonSerializer |    997.5 ns |    14.2434 ns |   0.8048 ns | 0.0992 |     424 B |
|                        SerializeCommentWithJilSerializer |  1,710.8 ns |    30.1333 ns |   1.7026 ns | 1.0281 |    4320 B |
|                   SerializeCommentWithJsonSpanSerializer |  1,891.8 ns |     6.5381 ns |   0.3694 ns | 0.4139 |    1744 B |
|                   SerializeCommentWithUtf8JsonSerializer |  1,834.9 ns |     7.0355 ns |   0.3975 ns | 0.1793 |     760 B |
|                          SerializeErrorWithJilSerializer |    339.7 ns |     0.7091 ns |   0.0401 ns | 0.1941 |     816 B |
|                     SerializeErrorWithJsonSpanSerializer |    192.6 ns |     0.3639 ns |   0.0206 ns | 0.0398 |     168 B |
|                     SerializeErrorWithUtf8JsonSerializer |    186.8 ns |     0.8585 ns |   0.0485 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    529.1 ns |     2.4706 ns |   0.1396 ns | 0.3004 |    1264 B |
|                     SerializeEventWithJsonSpanSerializer |    374.8 ns |     0.5362 ns |   0.0303 ns | 0.0720 |     304 B |
|                     SerializeEventWithUtf8JsonSerializer |    509.4 ns |     4.1788 ns |   0.2361 ns | 0.0391 |     168 B |
|                     SerializeMobileFeedWithJilSerializer |  7,499.3 ns |   375.0519 ns |  21.1911 ns | 3.7537 |   15768 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,422.2 ns |   109.0297 ns |   6.1604 ns | 1.7624 |    7432 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,241.3 ns |   139.9256 ns |   7.9061 ns | 0.7553 |    3200 B |
|                 SerializeMobileQuestionWithJilSerializer |    756.1 ns |    72.6649 ns |   4.1057 ns | 0.5159 |    2168 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    668.1 ns |     4.9337 ns |   0.2788 ns | 0.1612 |     680 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    650.6 ns |     2.9161 ns |   0.1648 ns | 0.0753 |     320 B |
|                SerializeMobileRepChangeWithJilSerializer |    499.6 ns |    13.3416 ns |   0.7538 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    349.2 ns |     3.8429 ns |   0.2171 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    355.5 ns |     1.0051 ns |   0.0568 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    783.0 ns |     1.3827 ns |   0.0781 ns | 0.4988 |    2096 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    620.5 ns |     3.7180 ns |   0.2101 ns | 0.1287 |     544 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    607.6 ns |     0.5633 ns |   0.0318 ns | 0.0658 |     280 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    617.8 ns |     3.3329 ns |   0.1883 ns | 0.3443 |    1448 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    586.3 ns |    27.2832 ns |   1.5416 ns | 0.1116 |     472 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    560.4 ns |     4.6771 ns |   0.2643 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    610.8 ns |     3.5687 ns |   0.2016 ns | 0.3462 |    1456 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    472.4 ns |     0.8657 ns |   0.0489 ns | 0.1154 |     488 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    452.0 ns |     0.6843 ns |   0.0387 ns | 0.0606 |     256 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    882.6 ns |    16.5726 ns |   0.9364 ns | 0.5388 |    2264 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    798.5 ns |     3.4860 ns |   0.1970 ns | 0.1860 |     784 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    802.5 ns |     4.6101 ns |   0.2605 ns | 0.0906 |     384 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    338.0 ns |     0.2303 ns |   0.0130 ns | 0.2017 |     848 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    261.7 ns |     1.5115 ns |   0.0854 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    284.4 ns |    37.9511 ns |   2.1443 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    572.9 ns |     2.6814 ns |   0.1515 ns | 0.3138 |    1320 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    431.8 ns |     1.8623 ns |   0.1052 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    396.9 ns |     0.5060 ns |   0.0286 ns | 0.0453 |     192 B |
|                 SerializeMobileBannerAdWithJilSerializer |    537.7 ns |     1.0967 ns |   0.0620 ns | 0.3080 |    1296 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    473.7 ns |     9.7180 ns |   0.5491 ns | 0.0911 |     384 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    423.5 ns |     0.4952 ns |   0.0280 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    321.4 ns |    30.4930 ns |   1.7229 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    171.8 ns |     0.1561 ns |   0.0088 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    160.5 ns |     0.8297 ns |   0.0469 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    839.0 ns |    11.6923 ns |   0.6606 ns | 0.5865 |    2464 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    794.3 ns |     9.9298 ns |   0.5611 ns | 0.2012 |     848 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    732.2 ns |     0.6011 ns |   0.0340 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  2,541.1 ns |    83.6142 ns |   4.7244 ns | 1.0681 |    4496 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  2,264.5 ns |   462.9390 ns |  26.1569 ns | 0.5188 |    2184 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  2,418.3 ns |    46.1516 ns |   2.6077 ns | 0.2251 |     960 B |
|                           SerializeInfoWithJilSerializer |  3,005.5 ns |   163.9798 ns |   9.2652 ns | 1.6937 |    7112 B |
|                      SerializeInfoWithJsonSpanSerializer |  2,786.3 ns |    40.1342 ns |   2.2677 ns | 0.5913 |    2496 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,017.2 ns |    68.3928 ns |   3.8643 ns | 0.3319 |    1400 B |
|                    SerializeNetworkUserWithJilSerializer |    908.6 ns |    10.1497 ns |   0.5735 ns | 0.5465 |    2296 B |
|               SerializeNetworkUserWithJsonSpanSerializer |    936.1 ns |     6.0430 ns |   0.3414 ns | 0.1955 |     824 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,171.1 ns |   145.0797 ns |   8.1973 ns | 0.0916 |     392 B |
|                   SerializeNotificationWithJilSerializer |  2,330.1 ns |     8.4095 ns |   0.4752 ns | 1.0300 |    4336 B |
|              SerializeNotificationWithJsonSpanSerializer |  2,107.4 ns |    44.0309 ns |   2.4878 ns | 0.4807 |    2032 B |
|              SerializeNotificationWithUtf8JsonSerializer |  2,236.5 ns |    21.7224 ns |   1.2274 ns | 0.2060 |     872 B |
|                           SerializePostWithJilSerializer |  3,740.2 ns |    30.2376 ns |   1.7085 ns | 2.0180 |    8480 B |
|                      SerializePostWithJsonSpanSerializer |  4,207.5 ns |   317.9124 ns |  17.9626 ns | 0.9308 |    3928 B |
|                      SerializePostWithUtf8JsonSerializer |  4,346.1 ns |    37.0596 ns |   2.0939 ns | 0.3967 |    1680 B |
|                      SerializePrivilegeWithJilSerializer |    337.0 ns |     0.8680 ns |   0.0490 ns | 0.1979 |     832 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    198.0 ns |    37.1594 ns |   2.0996 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    186.1 ns |     0.3805 ns |   0.0215 ns | 0.0246 |     104 B |
|                       SerializeQuestionWithJilSerializer | 14,690.2 ns |    78.8587 ns |   4.4557 ns | 7.3242 |   30784 B |
|                  SerializeQuestionWithJsonSpanSerializer | 15,291.8 ns |   148.0299 ns |   8.3640 ns | 3.5400 |   14872 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 17,063.9 ns |   620.7641 ns |  35.0743 ns | 1.4648 |    6240 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,603.2 ns |    41.2930 ns |   2.3331 ns | 1.0242 |    4304 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  1,869.6 ns |    49.1796 ns |   2.7787 ns | 0.4120 |    1736 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  1,915.0 ns |   212.5762 ns |  12.0110 ns | 0.1774 |     752 B |
|                     SerializeReputationWithJilSerializer |    583.4 ns |     6.8297 ns |   0.3859 ns | 0.3290 |    1384 B |
|                SerializeReputationWithJsonSpanSerializer |    576.6 ns |     1.8712 ns |   0.1057 ns | 0.1020 |     432 B |
|                SerializeReputationWithUtf8JsonSerializer |    659.1 ns |    69.9314 ns |   3.9513 ns | 0.0505 |     216 B |
|              SerializeReputationHistoryWithJilSerializer |    444.7 ns |     3.0007 ns |   0.1695 ns | 0.3219 |    1352 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    391.5 ns |     3.2440 ns |   0.1833 ns | 0.0873 |     368 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    526.9 ns |     0.9623 ns |   0.0544 ns | 0.0448 |     192 B |
|                       SerializeRevisionWithJilSerializer |  1,820.1 ns |   677.9436 ns |  38.3051 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,663.0 ns |    46.2182 ns |   2.6114 ns | 0.3643 |    1536 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,732.9 ns |   192.2801 ns |  10.8642 ns | 0.1526 |     648 B |
|                  SerializeSearchExcerptWithJilSerializer |  2,351.2 ns |    25.5423 ns |   1.4432 ns | 1.1444 |    4816 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  2,498.7 ns |     5.5127 ns |   0.3115 ns | 0.5493 |    2320 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  2,928.6 ns |    18.9206 ns |   1.0690 ns | 0.2365 |    1008 B |
|                    SerializeShallowUserWithJilSerializer |    630.4 ns |   597.8905 ns |  33.7819 ns | 0.3519 |    1480 B |
|               SerializeShallowUserWithJsonSpanSerializer |    644.1 ns |     5.2289 ns |   0.2954 ns | 0.1345 |     568 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    615.2 ns |     1.9971 ns |   0.1128 ns | 0.0620 |     264 B |
|                  SerializeSuggestedEditWithJilSerializer |  1,524.2 ns |    72.6920 ns |   4.1072 ns | 0.9003 |    3784 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  1,450.0 ns |    25.1028 ns |   1.4184 ns | 0.3128 |    1320 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  1,731.0 ns |     2.1217 ns |   0.1199 ns | 0.1373 |     584 B |
|                            SerializeTagWithJilSerializer |    607.5 ns |    21.6044 ns |   1.2207 ns | 0.3309 |    1392 B |
|                       SerializeTagWithJsonSpanSerializer |    526.6 ns |    12.0505 ns |   0.6809 ns | 0.1173 |     496 B |
|                       SerializeTagWithUtf8JsonSerializer |    609.4 ns |     2.5606 ns |   0.1447 ns | 0.0544 |     232 B |
|                       SerializeTagScoreWithJilSerializer |    736.6 ns |    25.0949 ns |   1.4179 ns | 0.5140 |    2160 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    840.3 ns |    75.2939 ns |   4.2542 ns | 0.1745 |     736 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    759.0 ns |     0.9492 ns |   0.0536 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |    617.8 ns |     4.5456 ns |   0.2568 ns | 0.3157 |    1328 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    414.2 ns |     2.7196 ns |   0.1537 ns | 0.0873 |     368 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |    627.1 ns |     1.7755 ns |   0.1003 ns | 0.0448 |     192 B |
|                        SerializeTagWikiWithJilSerializer |  1,709.2 ns |   596.4274 ns |  33.6993 ns | 1.0109 |    4248 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  1,752.5 ns |    18.2809 ns |   1.0329 ns | 0.3967 |    1672 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  1,855.4 ns |    11.0348 ns |   0.6235 ns | 0.1698 |     720 B |
|                         SerializeTopTagWithJilSerializer |    431.5 ns |     2.6290 ns |   0.1485 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    373.3 ns |     1.3008 ns |   0.0735 ns | 0.0777 |     328 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    370.7 ns |     2.8074 ns |   0.1586 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  1,907.7 ns |    70.8209 ns |   4.0015 ns | 1.0490 |    4416 B |
|                      SerializeUserWithJsonSpanSerializer |  1,913.5 ns |   186.4128 ns |  10.5327 ns | 0.4444 |    1872 B |
|                      SerializeUserWithUtf8JsonSerializer |  2,339.9 ns |    16.3069 ns |   0.9214 ns | 0.2136 |     912 B |
|                   SerializeUserTimelineWithJilSerializer |    756.5 ns |     6.5582 ns |   0.3705 ns | 0.5045 |    2120 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    698.3 ns |     1.5735 ns |   0.0889 ns | 0.1345 |     568 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |    820.8 ns |     2.0618 ns |   0.1165 ns | 0.0696 |     296 B |
|                SerializeWritePermissionWithJilSerializer |    472.2 ns |     3.8241 ns |   0.2161 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    376.4 ns |     0.9121 ns |   0.0515 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    352.6 ns |     0.5634 ns |   0.0318 ns | 0.0453 |     192 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    269.6 ns |     2.4915 ns |   0.1408 ns | 0.1407 |     592 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    192.0 ns |     1.9511 ns |   0.1102 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    223.7 ns |    13.0645 ns |   0.7382 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,040.9 ns |     9.4875 ns |   0.5361 ns | 0.9651 |    4056 B |
|                      SerializeSiteWithJsonSpanSerializer |  1,626.9 ns |    32.0555 ns |   1.8112 ns | 0.3986 |    1680 B |