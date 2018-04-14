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

* Support more types directly (e.g. Dictionary)
* Error Handling
* Many edge cases
* dynamic
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
|                                                   Method |        Mean |         Error |        StdDev |  Gen 0 | Allocated |
|--------------------------------------------------------- |------------:|--------------:|--------------:|-------:|----------:|
|              DeserializeTagSynonymWithJsonSpanSerializer |    480.3 ns |     78.502 ns |     4.4355 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |    785.2 ns |    206.282 ns |    11.6553 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  3,086.4 ns |    862.278 ns |    48.7203 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  1,985.9 ns |  1,011.464 ns |    57.1496 ns | 0.2022 |     856 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  3,082.3 ns |    908.433 ns |    51.3281 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    601.2 ns |      3.486 ns |     0.1970 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    399.2 ns |    163.862 ns |     9.2585 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    523.6 ns |    233.351 ns |    13.1848 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  4,216.4 ns |  1,658.302 ns |    93.6971 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  2,870.5 ns |  1,787.863 ns |   101.0176 ns | 0.1526 |     656 B |
|                    DeserializeUserWithUtf8JsonSerializer |  3,385.1 ns |  1,139.274 ns |    64.3711 ns | 0.1411 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,524.6 ns | 10,391.576 ns |   587.1433 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,007.3 ns |      7.401 ns |     0.4182 ns | 0.0801 |     344 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,196.5 ns |    159.489 ns |     9.0114 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    712.3 ns |    295.562 ns |    16.6998 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    457.0 ns |     16.130 ns |     0.9113 ns | 0.0281 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    604.0 ns |    209.640 ns |    11.8450 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    306.3 ns |    126.921 ns |     7.1713 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    186.7 ns |     24.623 ns |     1.3913 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    394.5 ns |  2,696.386 ns |   152.3508 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  4,170.3 ns | 24,296.752 ns | 1,372.8115 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  2,419.7 ns |  2,378.328 ns |   134.3799 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  3,305.2 ns |    554.474 ns |    31.3288 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    388.6 ns |    205.741 ns |    11.6248 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    256.9 ns |     14.731 ns |     0.8324 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    488.1 ns |    236.753 ns |    13.3770 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  3,211.9 ns |  1,096.521 ns |    61.9555 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,311.8 ns |    486.003 ns |    27.4601 ns | 0.1774 |     752 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  2,219.6 ns |  2,826.706 ns |   159.7141 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    504.5 ns |    137.641 ns |     7.7770 ns | 0.0448 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    300.1 ns |    109.249 ns |     6.1728 ns | 0.0224 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    465.2 ns |    139.431 ns |     7.8781 ns | 0.0219 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,991.5 ns |    386.434 ns |    21.8342 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  2,661.9 ns |    958.688 ns |    54.1676 ns | 0.3700 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  3,989.8 ns |  3,335.940 ns |   188.4868 ns | 0.3471 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    294.5 ns |     99.037 ns |     5.5958 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    179.6 ns |     26.417 ns |     1.4926 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    261.7 ns |     49.230 ns |     2.7816 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    450.6 ns |  2,456.574 ns |   138.8010 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    225.3 ns |    134.134 ns |     7.5788 ns | 0.0436 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    385.8 ns |     36.702 ns |     2.0737 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    431.9 ns |    142.394 ns |     8.0455 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    270.0 ns |     72.960 ns |     4.1224 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    395.0 ns |    167.598 ns |     9.4696 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  1,808.7 ns |    473.679 ns |    26.7638 ns | 0.1698 |     720 B |
|                    SerializeRelatedSiteWithJilSerializer |    407.8 ns |     88.420 ns |     4.9959 ns | 0.2017 |     848 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    250.1 ns |     62.528 ns |     3.5330 ns | 0.0510 |     216 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    238.1 ns |     91.174 ns |     5.1515 ns | 0.0284 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,167.0 ns |    695.877 ns |    39.3184 ns | 0.5856 |    2464 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,293.7 ns |    331.075 ns |    18.7063 ns | 0.2632 |    1112 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,077.2 ns |    262.293 ns |    14.8200 ns | 0.1087 |     464 B |
|                         SerializeNoticeWithJilSerializer |    420.5 ns |    187.609 ns |    10.6003 ns | 0.2017 |     848 B |
|                    SerializeNoticeWithJsonSpanSerializer |    278.8 ns |     88.136 ns |     4.9799 ns | 0.0510 |     216 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    400.1 ns |     43.551 ns |     2.4607 ns | 0.0281 |     120 B |
|                  SerializeMigrationInfoWithJilSerializer |  2,419.9 ns |    497.447 ns |    28.1067 ns | 0.9995 |    4208 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  2,005.6 ns |    194.382 ns |    10.9829 ns | 0.4501 |    1904 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,141.6 ns |  2,167.138 ns |   122.4473 ns | 0.1869 |     800 B |
|                     SerializeBadgeCountWithJilSerializer |    223.7 ns |     41.504 ns |     2.3451 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    196.3 ns |     54.557 ns |     3.0826 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    237.4 ns |     65.506 ns |     3.7012 ns | 0.0207 |      88 B |
|                        SerializeStylingWithJilSerializer |    372.8 ns |    115.289 ns |     6.5140 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    193.2 ns |     29.119 ns |     1.6453 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    169.7 ns |     11.253 ns |     0.6358 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    325.8 ns |     32.145 ns |     1.8163 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    257.1 ns |     84.604 ns |     4.7803 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    264.3 ns |     30.607 ns |     1.7293 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    590.8 ns |    177.784 ns |    10.0451 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    390.6 ns |     45.816 ns |     2.5887 ns | 0.0587 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    665.4 ns |    225.955 ns |    12.7669 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    487.3 ns |    124.265 ns |     7.0212 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    296.1 ns |     47.382 ns |     2.6772 ns | 0.0110 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    434.5 ns |    176.914 ns |     9.9960 ns | 0.0110 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  9,481.6 ns |  2,285.134 ns |   129.1143 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer |  6,581.4 ns |  1,098.014 ns |    62.0398 ns | 0.5417 |    2304 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  8,098.9 ns |  2,325.518 ns |   131.3961 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,773.8 ns |    248.704 ns |    14.0522 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,333.7 ns |  1,071.833 ns |    60.5605 ns | 0.1450 |     616 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,896.6 ns |    841.255 ns |    47.5325 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,277.4 ns |     18.811 ns |     1.0629 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  2,479.6 ns |    450.915 ns |    25.4775 ns | 0.2251 |     952 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,137.4 ns |    443.320 ns |    25.0484 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    329.8 ns |    119.364 ns |     6.7443 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    204.6 ns |     18.715 ns |     1.0574 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    341.7 ns |      3.585 ns |     0.2026 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    628.5 ns |    407.860 ns |    23.0449 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    472.9 ns |     93.676 ns |     5.2929 ns | 0.0510 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |    729.3 ns |    136.377 ns |     7.7056 ns | 0.0372 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 32,833.0 ns |  6,121.500 ns |   345.8761 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 11,775.3 ns |    533.201 ns |    30.1268 ns | 1.2512 |    5264 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 14,670.6 ns |  7,345.983 ns |   415.0617 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,361.9 ns |    114.977 ns |     6.4964 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |    999.9 ns |    102.014 ns |     5.7640 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,116.6 ns |    310.661 ns |    17.5529 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    612.2 ns |    132.828 ns |     7.5050 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    372.8 ns |    134.792 ns |     7.6160 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    577.9 ns |    229.894 ns |    12.9894 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,133.4 ns |    466.838 ns |    26.3772 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |    910.2 ns |    181.643 ns |    10.2632 ns | 0.0849 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,076.2 ns |    321.705 ns |    18.1770 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    927.2 ns |    355.633 ns |    20.0939 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |    894.1 ns |    140.106 ns |     7.9163 ns | 0.0906 |     384 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |  1,030.1 ns |    201.110 ns |    11.3631 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    875.9 ns |     94.405 ns |     5.3341 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    601.6 ns |    101.984 ns |     5.7623 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    833.2 ns |    140.444 ns |     7.9353 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,546.9 ns |    599.537 ns |    33.8750 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,295.7 ns |    181.830 ns |    10.2738 ns | 0.1278 |     544 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,416.7 ns |    100.825 ns |     5.6968 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    431.6 ns |     59.061 ns |     3.3371 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    249.4 ns |     54.455 ns |     3.0768 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    380.2 ns |     40.500 ns |     2.2883 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    703.0 ns |     59.429 ns |     3.3578 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    447.3 ns |     45.590 ns |     2.5759 ns | 0.0644 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    684.4 ns |    111.074 ns |     6.2759 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    787.1 ns |    124.884 ns |     7.0562 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    455.7 ns |     48.997 ns |     2.7684 ns | 0.0682 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    728.3 ns |    240.939 ns |    13.6135 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    326.1 ns |     93.931 ns |     5.3073 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    205.3 ns |     20.056 ns |     1.1332 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    354.9 ns |    135.576 ns |     7.6603 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,787.3 ns |     11.010 ns |     0.6221 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,210.4 ns |    388.634 ns |    21.9585 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,610.5 ns |    196.922 ns |    11.1264 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  5,114.4 ns |    336.668 ns |    19.0224 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  3,420.9 ns |  1,193.034 ns |    67.4086 ns | 0.4272 |    1800 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  4,424.1 ns |     91.618 ns |     5.1766 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,803.4 ns |  1,280.327 ns |    72.3408 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  4,305.3 ns |    810.490 ns |    45.7942 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  5,448.2 ns |  1,172.021 ns |    66.2213 ns | 0.4272 |    1816 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,669.0 ns |    231.743 ns |    13.0939 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  1,238.6 ns |    488.057 ns |    27.5761 ns | 0.0725 |     312 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  1,503.2 ns |    500.052 ns |    28.2538 ns | 0.0591 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  5,314.0 ns |    303.896 ns |    17.1707 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  2,917.8 ns |    623.895 ns |    35.2512 ns | 0.4044 |    1712 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  4,159.2 ns |    358.107 ns |    20.2337 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  8,352.4 ns |  3,166.336 ns |   178.9039 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  5,942.3 ns |  1,603.665 ns |    90.6100 ns | 0.4959 |    2096 B |
|                    DeserializePostWithUtf8JsonSerializer |  7,193.6 ns |  1,884.267 ns |   106.4646 ns | 0.4196 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    340.0 ns |     62.261 ns |     3.5179 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    204.9 ns |     35.047 ns |     1.9802 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    341.9 ns |     48.374 ns |     2.7332 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 42,988.2 ns | 12,106.089 ns |   684.0165 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 23,307.4 ns |    839.938 ns |    47.4581 ns | 2.1057 |    8848 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 28,896.8 ns |  7,821.305 ns |   441.9182 ns | 1.9226 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,232.1 ns |    664.460 ns |    37.5432 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,365.4 ns |    674.404 ns |    38.1051 ns | 0.1984 |     840 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,089.3 ns |    725.066 ns |    40.9676 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    867.4 ns |    107.507 ns |     6.0743 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    737.1 ns |    138.871 ns |     7.8465 ns | 0.0658 |     280 B |
|              DeserializeReputationWithUtf8JsonSerializer |    980.0 ns |    114.372 ns |     6.4622 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    684.0 ns |    198.099 ns |    11.1930 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    506.4 ns |    338.634 ns |    19.1334 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    693.9 ns |    162.452 ns |     9.1788 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  3,825.3 ns |    630.599 ns |    35.6300 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  2,327.1 ns |    117.080 ns |     6.6152 ns | 0.2708 |    1144 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  2,907.9 ns |    423.757 ns |    23.9430 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,669.5 ns |  1,753.168 ns |    99.0572 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  3,538.5 ns |    639.188 ns |    36.1153 ns | 0.2785 |    1176 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  4,531.4 ns |  3,309.220 ns |   186.9771 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,044.3 ns |    286.920 ns |    16.2115 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    730.2 ns |     69.931 ns |     3.9512 ns | 0.0753 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,055.5 ns |     58.739 ns |     3.3189 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,734.7 ns |    748.716 ns |    42.3038 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  1,957.8 ns |    411.467 ns |    23.2486 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  2,557.9 ns |     71.891 ns |     4.0620 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    922.2 ns |     39.336 ns |     2.2225 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    675.2 ns |    128.147 ns |     7.2405 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |    955.9 ns |     60.532 ns |     3.4202 ns | 0.0658 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,327.8 ns |     85.626 ns |     4.8380 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    850.1 ns |    282.382 ns |    15.9551 ns | 0.0830 |     352 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,212.1 ns |    284.724 ns |    16.0874 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    768.8 ns |     91.836 ns |     5.1889 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    518.4 ns |    260.303 ns |    14.7076 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    412.6 ns |     11.845 ns |     0.6692 ns | 0.0801 |     336 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    470.4 ns |     49.242 ns |     2.7823 ns | 0.0339 |     144 B |
|                   SerializeAccountMergeWithJilSerializer |    378.5 ns |     13.725 ns |     0.7755 ns | 0.2055 |     864 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    279.1 ns |     22.637 ns |     1.2791 ns | 0.0529 |     224 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    414.9 ns |     87.162 ns |     4.9248 ns | 0.0300 |     128 B |
|                         SerializeAnswerWithJilSerializer |  4,610.7 ns |    704.155 ns |    39.7860 ns | 2.0981 |    8832 B |
|                    SerializeAnswerWithJsonSpanSerializer |  5,249.1 ns |  2,245.712 ns |   126.8869 ns | 1.0300 |    4352 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  5,245.7 ns |     88.665 ns |     5.0097 ns | 0.4425 |    1864 B |
|                          SerializeBadgeWithJilSerializer |    997.3 ns |    384.228 ns |    21.7096 ns | 0.5627 |    2368 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,088.4 ns |     16.618 ns |     0.9389 ns | 0.2213 |     936 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,025.7 ns |    227.418 ns |    12.8495 ns | 0.0973 |     416 B |
|                        SerializeCommentWithJilSerializer |  1,716.6 ns |    262.530 ns |    14.8335 ns | 1.0281 |    4320 B |
|                   SerializeCommentWithJsonSpanSerializer |  1,957.9 ns |    349.036 ns |    19.7212 ns | 0.4158 |    1752 B |
|                   SerializeCommentWithUtf8JsonSerializer |  1,913.7 ns |     83.979 ns |     4.7450 ns | 0.1774 |     752 B |
|                          SerializeErrorWithJilSerializer |    340.1 ns |     51.573 ns |     2.9140 ns | 0.1922 |     808 B |
|                     SerializeErrorWithJsonSpanSerializer |    202.1 ns |      7.724 ns |     0.4364 ns | 0.0417 |     176 B |
|                     SerializeErrorWithUtf8JsonSerializer |    191.1 ns |     45.057 ns |     2.5458 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    540.6 ns |    178.908 ns |    10.1086 ns | 0.3023 |    1272 B |
|                     SerializeEventWithJsonSpanSerializer |    410.5 ns |     22.925 ns |     1.2953 ns | 0.0720 |     304 B |
|                     SerializeEventWithUtf8JsonSerializer |    512.5 ns |    127.205 ns |     7.1873 ns | 0.0391 |     168 B |
|                     SerializeMobileFeedWithJilSerializer |  7,932.7 ns |  5,229.433 ns |   295.4727 ns | 3.7613 |   15808 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,749.2 ns |  1,549.177 ns |    87.5314 ns | 1.7700 |    7472 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,517.6 ns |  3,333.377 ns |   188.3420 ns | 0.7477 |    3192 B |
|                 SerializeMobileQuestionWithJilSerializer |    784.8 ns |     26.253 ns |     1.4833 ns | 0.5159 |    2168 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    715.2 ns |    263.785 ns |    14.9043 ns | 0.1612 |     680 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    694.8 ns |    134.047 ns |     7.5739 ns | 0.0753 |     320 B |
|                SerializeMobileRepChangeWithJilSerializer |    532.0 ns |    127.967 ns |     7.2303 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    371.3 ns |     68.276 ns |     3.8577 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    364.3 ns |     76.292 ns |     4.3106 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    780.4 ns |    330.257 ns |    18.6601 ns | 0.3605 |    1512 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    657.9 ns |     23.825 ns |     1.3462 ns | 0.1287 |     544 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    625.6 ns |    244.888 ns |    13.8366 ns | 0.0677 |     288 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    643.7 ns |    175.311 ns |     9.9054 ns | 0.3443 |    1448 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    600.2 ns |     47.340 ns |     2.6748 ns | 0.1135 |     480 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    582.1 ns |    152.956 ns |     8.6423 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    674.7 ns |    516.273 ns |    29.1704 ns | 0.3462 |    1456 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    512.8 ns |    126.673 ns |     7.1572 ns | 0.1154 |     488 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    463.4 ns |      1.302 ns |     0.0736 ns | 0.0606 |     256 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    915.6 ns |     91.264 ns |     5.1566 ns | 0.5388 |    2264 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    875.3 ns |    175.950 ns |     9.9415 ns | 0.1917 |     808 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    813.6 ns |    313.992 ns |    17.7412 ns | 0.0906 |     384 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    359.5 ns |    105.869 ns |     5.9818 ns | 0.2036 |     856 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    314.7 ns |    185.108 ns |    10.4590 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    297.7 ns |    136.566 ns |     7.7162 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    604.5 ns |    187.657 ns |    10.6030 ns | 0.3119 |    1312 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    445.8 ns |     85.390 ns |     4.8247 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    418.6 ns |    168.057 ns |     9.4955 ns | 0.0453 |     192 B |
|                 SerializeMobileBannerAdWithJilSerializer |    530.3 ns |     12.360 ns |     0.6984 ns | 0.3061 |    1288 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    485.2 ns |    149.570 ns |     8.4510 ns | 0.0906 |     384 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    458.1 ns |    345.892 ns |    19.5435 ns | 0.0410 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    344.7 ns |     22.821 ns |     1.2894 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    183.2 ns |     64.916 ns |     3.6679 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    165.9 ns |     21.707 ns |     1.2265 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    903.0 ns |     45.972 ns |     2.5975 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    792.9 ns |     45.100 ns |     2.5483 ns | 0.2012 |     848 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    760.1 ns |    104.277 ns |     5.8918 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  2,768.1 ns |    665.922 ns |    37.6258 ns | 1.0681 |    4496 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  2,347.5 ns |    707.139 ns |    39.9547 ns | 0.5226 |    2208 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  2,469.0 ns |    480.628 ns |    27.1564 ns | 0.2251 |     960 B |
|                           SerializeInfoWithJilSerializer |  3,079.1 ns |    353.651 ns |    19.9819 ns | 1.6861 |    7088 B |
|                      SerializeInfoWithJsonSpanSerializer |  2,851.5 ns |  1,198.313 ns |    67.7069 ns | 0.5913 |    2496 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,090.9 ns |    523.507 ns |    29.5791 ns | 0.3281 |    1392 B |
|                    SerializeNetworkUserWithJilSerializer |    926.9 ns |    160.919 ns |     9.0922 ns | 0.5484 |    2304 B |
|               SerializeNetworkUserWithJsonSpanSerializer |    958.6 ns |    202.149 ns |    11.4218 ns | 0.1926 |     816 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,150.4 ns |    439.501 ns |    24.8326 ns | 0.0916 |     392 B |
|                   SerializeNotificationWithJilSerializer |  2,491.9 ns |    690.121 ns |    38.9931 ns | 1.0376 |    4360 B |
|              SerializeNotificationWithJsonSpanSerializer |  2,212.1 ns |    455.624 ns |    25.7436 ns | 0.4883 |    2064 B |
|              SerializeNotificationWithUtf8JsonSerializer |  2,278.8 ns |    495.561 ns |    28.0001 ns | 0.2098 |     888 B |
|                           SerializePostWithJilSerializer |  3,881.1 ns |  2,075.410 ns |   117.2645 ns | 2.0218 |    8488 B |
|                      SerializePostWithJsonSpanSerializer |  4,314.7 ns |  1,181.275 ns |    66.7442 ns | 0.9308 |    3920 B |
|                      SerializePostWithUtf8JsonSerializer |  4,447.8 ns |  1,025.745 ns |    57.9565 ns | 0.3967 |    1680 B |
|                      SerializePrivilegeWithJilSerializer |    350.5 ns |    136.637 ns |     7.7203 ns | 0.1960 |     824 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    205.8 ns |     43.779 ns |     2.4736 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    195.6 ns |     21.231 ns |     1.1996 ns | 0.0246 |     104 B |
|                       SerializeQuestionWithJilSerializer | 14,882.7 ns |  1,736.391 ns |    98.1093 ns | 7.3395 |   30792 B |
|                  SerializeQuestionWithJsonSpanSerializer | 15,831.6 ns |  1,036.010 ns |    58.5365 ns | 3.5400 |   14872 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 17,511.4 ns |  6,295.555 ns |   355.7105 ns | 1.4648 |    6224 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,731.6 ns |    339.063 ns |    19.1577 ns | 1.0281 |    4320 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  1,964.8 ns |    198.387 ns |    11.2092 ns | 0.4158 |    1752 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  1,888.9 ns |    275.065 ns |    15.5417 ns | 0.1755 |     744 B |
|                     SerializeReputationWithJilSerializer |    614.7 ns |     77.610 ns |     4.3851 ns | 0.3271 |    1376 B |
|                SerializeReputationWithJsonSpanSerializer |    570.7 ns |    174.863 ns |     9.8801 ns | 0.1020 |     432 B |
|                SerializeReputationWithUtf8JsonSerializer |    691.0 ns |    153.474 ns |     8.6716 ns | 0.0525 |     224 B |
|              SerializeReputationHistoryWithJilSerializer |    456.9 ns |     76.789 ns |     4.3387 ns | 0.3142 |    1320 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    387.5 ns |    128.829 ns |     7.2791 ns | 0.0834 |     352 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    567.9 ns |    573.002 ns |    32.3757 ns | 0.0448 |     192 B |
|                       SerializeRevisionWithJilSerializer |  1,624.4 ns |    477.449 ns |    26.9768 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,709.0 ns |    180.573 ns |    10.2027 ns | 0.3605 |    1520 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,657.9 ns |    664.609 ns |    37.5517 ns | 0.1526 |     648 B |
|                  SerializeSearchExcerptWithJilSerializer |  2,395.7 ns |  1,009.888 ns |    57.0606 ns | 1.1444 |    4816 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  2,632.9 ns |    367.371 ns |    20.7572 ns | 0.5493 |    2320 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  2,951.1 ns |    336.255 ns |    18.9990 ns | 0.2365 |    1008 B |
|                    SerializeShallowUserWithJilSerializer |    644.2 ns |    475.090 ns |    26.8435 ns | 0.3519 |    1480 B |
|               SerializeShallowUserWithJsonSpanSerializer |    661.9 ns |    104.582 ns |     5.9091 ns | 0.1392 |     584 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    634.6 ns |    253.967 ns |    14.3496 ns | 0.0639 |     272 B |
|                  SerializeSuggestedEditWithJilSerializer |  1,568.3 ns |    290.572 ns |    16.4178 ns | 0.8965 |    3768 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  1,524.8 ns |    393.906 ns |    22.2564 ns | 0.3147 |    1328 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  1,765.8 ns |    430.467 ns |    24.3222 ns | 0.1354 |     576 B |
|                            SerializeTagWithJilSerializer |    618.1 ns |     65.221 ns |     3.6851 ns | 0.3309 |    1392 B |
|                       SerializeTagWithJsonSpanSerializer |    564.7 ns |    351.330 ns |    19.8508 ns | 0.1173 |     496 B |
|                       SerializeTagWithUtf8JsonSerializer |    627.8 ns |     32.058 ns |     1.8114 ns | 0.0525 |     224 B |
|                       SerializeTagScoreWithJilSerializer |    766.5 ns |     50.598 ns |     2.8589 ns | 0.5140 |    2160 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    879.1 ns |    209.910 ns |    11.8603 ns | 0.1764 |     744 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    779.3 ns |    104.838 ns |     5.9236 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |    687.4 ns |    399.246 ns |    22.5581 ns | 0.3157 |    1328 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    438.7 ns |    229.088 ns |    12.9439 ns | 0.0854 |     360 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |    642.9 ns |     47.237 ns |     2.6690 ns | 0.0448 |     192 B |
|                        SerializeTagWikiWithJilSerializer |  1,757.5 ns |    549.666 ns |    31.0571 ns | 1.0090 |    4240 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  1,804.8 ns |    650.072 ns |    36.7303 ns | 0.3986 |    1680 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  1,892.5 ns |    404.834 ns |    22.8739 ns | 0.1678 |     720 B |
|                         SerializeTopTagWithJilSerializer |    422.2 ns |    143.812 ns |     8.1256 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    391.9 ns |      5.933 ns |     0.3352 ns | 0.0758 |     320 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    407.2 ns |    154.341 ns |     8.7206 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  1,955.8 ns |    425.823 ns |    24.0598 ns | 1.0529 |    4424 B |
|                      SerializeUserWithJsonSpanSerializer |  1,946.4 ns |    304.730 ns |    17.2178 ns | 0.4387 |    1856 B |
|                      SerializeUserWithUtf8JsonSerializer |  2,448.6 ns |    108.541 ns |     6.1328 ns | 0.2136 |     904 B |
|                   SerializeUserTimelineWithJilSerializer |    775.3 ns |     75.157 ns |     4.2465 ns | 0.5045 |    2120 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    728.4 ns |    166.114 ns |     9.3858 ns | 0.1345 |     568 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |    837.6 ns |     36.145 ns |     2.0423 ns | 0.0677 |     288 B |
|                SerializeWritePermissionWithJilSerializer |    465.3 ns |    213.418 ns |    12.0585 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    378.2 ns |     60.213 ns |     3.4021 ns | 0.0854 |     360 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    366.1 ns |     74.361 ns |     4.2015 ns | 0.0453 |     192 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    266.6 ns |     34.851 ns |     1.9692 ns | 0.1407 |     592 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    205.2 ns |     31.987 ns |     1.8073 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    217.3 ns |     42.446 ns |     2.3983 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,110.2 ns |    503.032 ns |    28.4222 ns | 0.9651 |    4056 B |
|                      SerializeSiteWithJsonSpanSerializer |  1,692.2 ns |    362.843 ns |    20.5013 ns | 0.3986 |    1680 B |
