# SpanJson
[![Build status](https://ci.appveyor.com/api/projects/status/h49loskhn09g03in/branch/master?svg=true)](https://ci.appveyor.com/project/Tornhoof/spanjson/branch/master)

Sandbox for playing around with Span and JSON Serialization.
This is basically the ValueStringBuilder from CoreFx with the TryFormat API for formatting values with Span<char>.
The actual serializers are a T4 Template (BclFormatter.tt).

Performance Issues:
* Integer Formatting: derived from UTF8Json as the CoreCLR version is two times slower.
* Integer Parsing: derived from UTF8Json
* DateTime/DateTimeOffset Parser: derived from UTf8Parser with modifications to support less than 7 digit fractions

Note: Using shared code paths between UTF16 and UTF8 makes the library a bit slower, but otherwise the code is a large batch of if/then/else

Todo:

* Streams

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
|                                                       Method |        Mean |          Error |        StdDev |  Gen 0 | Allocated |
|------------------------------------------------------------- |------------:|---------------:|--------------:|-------:|----------:|
|                    DeserializeMigrationInfoWithJilSerializer |  4,035.6 ns |  2,029.5722 ns |   114.6746 ns | 0.3662 |    1568 B |
|               DeserializeMigrationInfoWithSpanJsonSerializer |  3,302.1 ns |  1,645.3010 ns |    92.9626 ns | 0.3700 |    1560 B |
|           DeserializeMigrationInfoWithSpanJsonUtf8Serializer |  4,156.0 ns | 11,363.2373 ns |   642.0440 ns | 0.3815 |    1608 B |
|               DeserializeMigrationInfoWithUtf8JsonSerializer |  3,773.0 ns |  1,390.6361 ns |    78.5735 ns | 0.3433 |    1472 B |
|                       DeserializeBadgeCountWithJilSerializer |    289.2 ns |     79.3006 ns |     4.4806 ns | 0.0319 |     136 B |
|                  DeserializeBadgeCountWithSpanJsonSerializer |    197.2 ns |      5.6156 ns |     0.3173 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithSpanJsonUtf8Serializer |    209.6 ns |     22.7167 ns |     1.2835 ns | 0.0226 |      96 B |
|                  DeserializeBadgeCountWithUtf8JsonSerializer |    273.5 ns |     38.6142 ns |     2.1818 ns | 0.0091 |      40 B |
|                          DeserializeStylingWithJilSerializer |    463.8 ns |  1,370.8042 ns |    77.4530 ns | 0.0663 |     280 B |
|                     DeserializeStylingWithSpanJsonSerializer |    287.1 ns |    192.9975 ns |    10.9047 ns | 0.0434 |     184 B |
|                 DeserializeStylingWithSpanJsonUtf8Serializer |    476.4 ns |    336.5106 ns |    19.0135 ns | 0.0567 |     240 B |
|                     DeserializeStylingWithUtf8JsonSerializer |    492.3 ns |    941.0654 ns |    53.1719 ns | 0.0429 |     184 B |
|                 DeserializeOriginalQuestionWithJilSerializer |    545.4 ns |    393.3294 ns |    22.2238 ns | 0.0448 |     192 B |
|            DeserializeOriginalQuestionWithSpanJsonSerializer |    362.6 ns |    141.4654 ns |     7.9931 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithSpanJsonUtf8Serializer |    453.0 ns |    408.3619 ns |    23.0732 ns | 0.0358 |     152 B |
|            DeserializeOriginalQuestionWithUtf8JsonSerializer |    487.6 ns |    329.3149 ns |    18.6069 ns | 0.0224 |      96 B |
|           DeserializeMobileAssociationBonusWithJilSerializer |    527.7 ns |    134.3744 ns |     7.5924 ns | 0.0467 |     200 B |
|      DeserializeMobileAssociationBonusWithSpanJsonSerializer |    351.5 ns |     90.3943 ns |     5.1074 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithSpanJsonUtf8Serializer |    375.8 ns |    188.6340 ns |    10.6582 ns | 0.0377 |     160 B |
|      DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    500.6 ns |    918.1994 ns |    51.8800 ns | 0.0238 |     104 B |
|               DeserializeMobileCareersJobAdWithJilSerializer |    856.3 ns |    355.8155 ns |    20.1042 ns | 0.0868 |     368 B |
|          DeserializeMobileCareersJobAdWithSpanJsonSerializer |    647.3 ns |    712.1272 ns |    40.2365 ns | 0.0639 |     272 B |
|      DeserializeMobileCareersJobAdWithSpanJsonUtf8Serializer |    741.9 ns |    241.7772 ns |    13.6609 ns | 0.0782 |     328 B |
|          DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    833.0 ns |    449.5057 ns |    25.3979 ns | 0.0639 |     272 B |
|                   DeserializeMobileBannerAdWithJilSerializer |    966.1 ns |    881.3726 ns |    49.7992 ns | 0.0906 |     384 B |
|              DeserializeMobileBannerAdWithSpanJsonSerializer |    586.2 ns |    386.0456 ns |    21.8123 ns | 0.0677 |     288 B |
|          DeserializeMobileBannerAdWithSpanJsonUtf8Serializer |    652.7 ns |    360.4214 ns |    20.3645 ns | 0.0811 |     344 B |
|              DeserializeMobileBannerAdWithUtf8JsonSerializer |    750.0 ns |    135.9730 ns |     7.6827 ns | 0.0677 |     288 B |
|               DeserializeMobileUpdateNoticeWithJilSerializer |    357.3 ns |     24.3048 ns |     1.3733 ns | 0.0548 |     232 B |
|          DeserializeMobileUpdateNoticeWithSpanJsonSerializer |    272.3 ns |     75.1876 ns |     4.2482 ns | 0.0319 |     136 B |
|      DeserializeMobileUpdateNoticeWithSpanJsonUtf8Serializer |    330.7 ns |     79.0340 ns |     4.4656 ns | 0.0453 |     192 B |
|          DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    390.8 ns |    166.0900 ns |     9.3844 ns | 0.0319 |     136 B |
|                       DeserializeFlagOptionWithJilSerializer |  1,862.8 ns |    681.1666 ns |    38.4872 ns | 0.1545 |     656 B |
|                  DeserializeFlagOptionWithSpanJsonSerializer |  1,353.0 ns |    500.3239 ns |    28.2692 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithSpanJsonUtf8Serializer |  1,500.0 ns |  1,427.7224 ns |    80.6690 ns | 0.1221 |     520 B |
|                  DeserializeFlagOptionWithUtf8JsonSerializer |  1,916.0 ns |  2,934.8027 ns |   165.8218 ns | 0.1068 |     464 B |
|                        DeserializeInboxItemWithJilSerializer |  5,138.3 ns |  2,619.4346 ns |   148.0029 ns | 0.4196 |    1768 B |
|                   DeserializeInboxItemWithSpanJsonSerializer |  4,052.0 ns |  1,200.8649 ns |    67.8511 ns | 0.4196 |    1792 B |
|               DeserializeInboxItemWithSpanJsonUtf8Serializer |  4,495.9 ns |  2,520.9563 ns |   142.4387 ns | 0.4425 |    1872 B |
|                   DeserializeInboxItemWithUtf8JsonSerializer |  5,008.6 ns |  2,280.8776 ns |   128.8738 ns | 0.3967 |    1672 B |
|                             DeserializeInfoWithJilSerializer |  6,050.0 ns |  1,241.5634 ns |    70.1506 ns | 0.4120 |    1744 B |
|                        DeserializeInfoWithSpanJsonSerializer |  5,175.4 ns |  1,388.6428 ns |    78.4609 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithSpanJsonUtf8Serializer |  5,929.5 ns |  2,807.3973 ns |   158.6232 ns | 0.4196 |    1792 B |
|                        DeserializeInfoWithUtf8JsonSerializer |  6,130.4 ns |  1,899.7711 ns |   107.3406 ns | 0.4272 |    1808 B |
|                      DeserializeNetworkUserWithJilSerializer |  1,827.6 ns |    539.0519 ns |    30.4574 ns | 0.0820 |     352 B |
|                 DeserializeNetworkUserWithSpanJsonSerializer |  1,360.3 ns |    513.8232 ns |    29.0320 ns | 0.0725 |     304 B |
|             DeserializeNetworkUserWithSpanJsonUtf8Serializer |  1,419.5 ns |    280.7001 ns |    15.8601 ns | 0.0839 |     360 B |
|                 DeserializeNetworkUserWithUtf8JsonSerializer |  1,592.0 ns |    328.7879 ns |    18.5771 ns | 0.0591 |     256 B |
|                     DeserializeNotificationWithJilSerializer |  4,892.8 ns |  7,696.3263 ns |   434.8567 ns | 0.3891 |    1640 B |
|                DeserializeNotificationWithSpanJsonSerializer |  3,854.0 ns |  2,609.8433 ns |   147.4610 ns | 0.3967 |    1688 B |
|            DeserializeNotificationWithSpanJsonUtf8Serializer |  4,335.3 ns |  4,790.7634 ns |   270.6870 ns | 0.4120 |    1744 B |
|                DeserializeNotificationWithUtf8JsonSerializer |  4,618.8 ns |  1,720.7174 ns |    97.2237 ns | 0.3662 |    1544 B |
|                             DeserializePostWithJilSerializer |  8,286.4 ns |  2,621.3820 ns |   148.1129 ns | 0.5341 |    2272 B |
|                        DeserializePostWithSpanJsonSerializer |  6,719.3 ns |  1,364.4049 ns |    77.0914 ns | 0.4959 |    2104 B |
|                    DeserializePostWithSpanJsonUtf8Serializer |  7,630.4 ns | 10,351.2021 ns |   584.8621 ns | 0.5035 |    2136 B |
|                        DeserializePostWithUtf8JsonSerializer |  7,840.8 ns |  1,037.3992 ns |    58.6150 ns | 0.4196 |    1792 B |
|                        DeserializePrivilegeWithJilSerializer |    377.4 ns |    174.1460 ns |     9.8396 ns | 0.0548 |     232 B |
|                   DeserializePrivilegeWithSpanJsonSerializer |    272.3 ns |     61.9574 ns |     3.5007 ns | 0.0319 |     136 B |
|               DeserializePrivilegeWithSpanJsonUtf8Serializer |    348.5 ns |    165.1971 ns |     9.3339 ns | 0.0453 |     192 B |
|                   DeserializePrivilegeWithUtf8JsonSerializer |    370.6 ns |    282.7001 ns |    15.9731 ns | 0.0319 |     136 B |
|                         DeserializeQuestionWithJilSerializer | 48,418.6 ns | 46,818.5659 ns | 2,645.3358 ns | 2.2583 |    9480 B |
|                    DeserializeQuestionWithSpanJsonSerializer | 28,268.6 ns | 20,011.0257 ns | 1,130.6601 ns | 2.1057 |    8848 B |
|                DeserializeQuestionWithSpanJsonUtf8Serializer | 29,838.1 ns | 13,459.9584 ns |   760.5126 ns | 2.1057 |    8936 B |
|                    DeserializeQuestionWithUtf8JsonSerializer | 30,130.9 ns | 23,554.1285 ns | 1,330.8519 ns | 1.9226 |    8136 B |
|                 DeserializeQuestionTimelineWithJilSerializer |  3,318.1 ns |  1,634.8746 ns |    92.3734 ns | 0.2289 |     968 B |
|            DeserializeQuestionTimelineWithSpanJsonSerializer |  2,983.0 ns |  6,769.1825 ns |   382.4714 ns | 0.1984 |     848 B |
|        DeserializeQuestionTimelineWithSpanJsonUtf8Serializer |  2,876.0 ns |     95.0695 ns |     5.3716 ns | 0.2098 |     888 B |
|            DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,489.6 ns |  4,310.9910 ns |   243.5790 ns | 0.1602 |     680 B |
|                       DeserializeReputationWithJilSerializer |    860.1 ns |    314.5874 ns |    17.7748 ns | 0.0658 |     280 B |
|                  DeserializeReputationWithSpanJsonSerializer |    821.4 ns |    550.7264 ns |    31.1171 ns | 0.0639 |     272 B |
|              DeserializeReputationWithSpanJsonUtf8Serializer |    932.4 ns |    168.8368 ns |     9.5396 ns | 0.0811 |     344 B |
|                  DeserializeReputationWithUtf8JsonSerializer |  1,068.3 ns |    611.2681 ns |    34.5378 ns | 0.0420 |     184 B |
|                DeserializeReputationHistoryWithJilSerializer |    673.3 ns |    128.5508 ns |     7.2634 ns | 0.0372 |     160 B |
|           DeserializeReputationHistoryWithSpanJsonSerializer |    596.7 ns |    208.3616 ns |    11.7728 ns | 0.0296 |     128 B |
|       DeserializeReputationHistoryWithSpanJsonUtf8Serializer |    627.6 ns |    305.1345 ns |    17.2407 ns | 0.0429 |     184 B |
|           DeserializeReputationHistoryWithUtf8JsonSerializer |    717.2 ns |     66.7799 ns |     3.7732 ns | 0.0143 |      64 B |
|                         DeserializeRevisionWithJilSerializer |  3,173.9 ns |    613.8581 ns |    34.6841 ns | 0.2556 |    1088 B |
|                    DeserializeRevisionWithSpanJsonSerializer |  2,483.6 ns |    278.1395 ns |    15.7154 ns | 0.2670 |    1136 B |
|                DeserializeRevisionWithSpanJsonUtf8Serializer |  2,841.3 ns |    896.4641 ns |    50.6519 ns | 0.2823 |    1192 B |
|                    DeserializeRevisionWithUtf8JsonSerializer |  3,289.6 ns |  2,173.6862 ns |   122.8173 ns | 0.2327 |     992 B |
|                    DeserializeSearchExcerptWithJilSerializer |  4,556.4 ns |  1,083.3373 ns |    61.2106 ns | 0.3052 |    1304 B |
|               DeserializeSearchExcerptWithSpanJsonSerializer |  3,779.9 ns |    584.6352 ns |    33.0330 ns | 0.2747 |    1168 B |
|           DeserializeSearchExcerptWithSpanJsonUtf8Serializer |  4,126.0 ns |  1,732.3767 ns |    97.8825 ns | 0.2823 |    1208 B |
|               DeserializeSearchExcerptWithUtf8JsonSerializer |  5,063.8 ns |  3,177.7306 ns |   179.5477 ns | 0.2365 |    1016 B |
|                      DeserializeShallowUserWithJilSerializer |  1,102.0 ns |    206.2519 ns |    11.6536 ns | 0.0839 |     360 B |
|                 DeserializeShallowUserWithSpanJsonSerializer |    875.9 ns |    940.7686 ns |    53.1552 ns | 0.0734 |     312 B |
|             DeserializeShallowUserWithSpanJsonUtf8Serializer |    987.4 ns |    581.8388 ns |    32.8750 ns | 0.0887 |     376 B |
|                 DeserializeShallowUserWithUtf8JsonSerializer |  1,106.0 ns |    747.5076 ns |    42.2356 ns | 0.0610 |     264 B |
|                    DeserializeSuggestedEditWithJilSerializer |  2,736.2 ns |  1,175.1189 ns |    66.3964 ns | 0.1831 |     776 B |
|               DeserializeSuggestedEditWithSpanJsonSerializer |  2,276.1 ns |  1,842.3939 ns |   104.0987 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithSpanJsonUtf8Serializer |  2,296.3 ns |  1,294.7824 ns |    73.1576 ns | 0.1945 |     832 B |
|               DeserializeSuggestedEditWithUtf8JsonSerializer |  2,990.6 ns |  4,337.0681 ns |   245.0524 ns | 0.1602 |     680 B |
|                              DeserializeTagWithJilSerializer |    963.5 ns |    562.0934 ns |    31.7593 ns | 0.0877 |     376 B |
|                         DeserializeTagWithSpanJsonSerializer |    735.2 ns |    164.0480 ns |     9.2690 ns | 0.0658 |     280 B |
|                     DeserializeTagWithSpanJsonUtf8Serializer |    797.4 ns |    178.2051 ns |    10.0689 ns | 0.0801 |     336 B |
|                         DeserializeTagWithUtf8JsonSerializer |  1,025.9 ns |    379.2181 ns |    21.4265 ns | 0.0658 |     280 B |
|                         DeserializeTagScoreWithJilSerializer |  1,453.4 ns |    211.4435 ns |    11.9470 ns | 0.0935 |     400 B |
|                    DeserializeTagScoreWithSpanJsonSerializer |  1,074.7 ns |    647.4003 ns |    36.5793 ns | 0.0839 |     360 B |
|                DeserializeTagScoreWithSpanJsonUtf8Serializer |  1,156.6 ns |    653.1742 ns |    36.9056 ns | 0.0973 |     416 B |
|                    DeserializeTagScoreWithUtf8JsonSerializer |  1,387.7 ns |    269.7727 ns |    15.2427 ns | 0.0725 |     304 B |
|                       DeserializeTagSynonymWithJilSerializer |    761.7 ns |    231.6392 ns |    13.0880 ns | 0.0620 |     264 B |
|                  DeserializeTagSynonymWithSpanJsonSerializer |    618.8 ns |    273.4119 ns |    15.4483 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithSpanJsonUtf8Serializer |    654.9 ns |    331.8209 ns |    18.7485 ns | 0.0525 |     224 B |
|                  DeserializeTagSynonymWithUtf8JsonSerializer |    767.1 ns |     58.5154 ns |     3.3062 ns | 0.0401 |     168 B |
|                          DeserializeTagWikiWithJilSerializer |  3,092.5 ns |  1,180.6118 ns |    66.7068 ns | 0.2480 |    1048 B |
|                     DeserializeTagWikiWithSpanJsonSerializer |  2,464.7 ns |  1,028.5048 ns |    58.1124 ns | 0.2022 |     864 B |
|                 DeserializeTagWikiWithSpanJsonUtf8Serializer |  2,577.9 ns |    628.2993 ns |    35.5001 ns | 0.2174 |     920 B |
|                     DeserializeTagWikiWithUtf8JsonSerializer |  3,361.9 ns |  2,561.9837 ns |   144.7568 ns | 0.1793 |     760 B |
|                           DeserializeTopTagWithJilSerializer |    631.6 ns |    122.3231 ns |     6.9115 ns | 0.0486 |     208 B |
|                      DeserializeTopTagWithSpanJsonSerializer |    474.7 ns |    572.9819 ns |    32.3745 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithSpanJsonUtf8Serializer |    485.9 ns |    607.6499 ns |    34.3333 ns | 0.0391 |     168 B |
|                      DeserializeTopTagWithUtf8JsonSerializer |    547.9 ns |    193.1817 ns |    10.9151 ns | 0.0257 |     112 B |
|                             DeserializeUserWithJilSerializer |  4,031.7 ns |    732.9458 ns |    41.4128 ns | 0.1602 |     696 B |
|                        DeserializeUserWithSpanJsonSerializer |  3,248.6 ns |  1,246.2625 ns |    70.4161 ns | 0.1526 |     656 B |
|                    DeserializeUserWithSpanJsonUtf8Serializer |  3,417.2 ns |    711.7753 ns |    40.2166 ns | 0.1678 |     712 B |
|                        DeserializeUserWithUtf8JsonSerializer |  3,422.4 ns |  1,037.1227 ns |    58.5994 ns | 0.1411 |     600 B |
|                     DeserializeUserTimelineWithJilSerializer |  1,192.3 ns |    231.6396 ns |    13.0881 ns | 0.0820 |     352 B |
|                DeserializeUserTimelineWithSpanJsonSerializer |    980.4 ns |    168.6344 ns |     9.5282 ns | 0.0801 |     344 B |
|            DeserializeUserTimelineWithSpanJsonUtf8Serializer |  1,144.1 ns |    239.4910 ns |    13.5317 ns | 0.0954 |     408 B |
|                DeserializeUserTimelineWithUtf8JsonSerializer |  1,310.1 ns |    530.3438 ns |    29.9654 ns | 0.0591 |     256 B |
|                  DeserializeWritePermissionWithJilSerializer |    705.8 ns |    280.3009 ns |    15.8375 ns | 0.0515 |     216 B |
|             DeserializeWritePermissionWithSpanJsonSerializer |    535.2 ns |    140.7369 ns |     7.9519 ns | 0.0277 |     120 B |
|         DeserializeWritePermissionWithSpanJsonUtf8Serializer |    582.6 ns |     82.7245 ns |     4.6741 ns | 0.0410 |     176 B |
|             DeserializeWritePermissionWithUtf8JsonSerializer |    693.3 ns |    323.6411 ns |    18.2863 ns | 0.0277 |     120 B |
|              DeserializeMobileBannerAdImageWithJilSerializer |    311.9 ns |     58.4778 ns |     3.3041 ns | 0.0434 |     184 B |
|         DeserializeMobileBannerAdImageWithSpanJsonSerializer |    216.8 ns |     39.8404 ns |     2.2511 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithSpanJsonUtf8Serializer |    278.2 ns |      6.4670 ns |     0.3654 ns | 0.0339 |     144 B |
|         DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    307.9 ns |     20.0833 ns |     1.1347 ns | 0.0205 |      88 B |
|                             DeserializeSiteWithJilSerializer |  3,332.7 ns |    195.6693 ns |    11.0557 ns | 0.3586 |    1520 B |
|                        DeserializeSiteWithSpanJsonSerializer |  2,930.3 ns |    651.3417 ns |    36.8020 ns | 0.3548 |    1504 B |
|                    DeserializeSiteWithSpanJsonUtf8Serializer |  3,426.9 ns |    967.4298 ns |    54.6616 ns | 0.3700 |    1568 B |
|                        DeserializeSiteWithUtf8JsonSerializer |  3,364.0 ns |    276.5573 ns |    15.6260 ns | 0.3357 |    1424 B |
|                      DeserializeRelatedSiteWithJilSerializer |    394.4 ns |    113.2962 ns |     6.4014 ns | 0.0682 |     288 B |
|                 DeserializeRelatedSiteWithSpanJsonSerializer |    350.4 ns |     53.3762 ns |     3.0159 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithSpanJsonUtf8Serializer |    423.1 ns |     74.1573 ns |     4.1900 ns | 0.0682 |     288 B |
|                 DeserializeRelatedSiteWithUtf8JsonSerializer |    488.3 ns |    142.0257 ns |     8.0247 ns | 0.0448 |     192 B |
|                    DeserializeClosedDetailsWithJilSerializer |  2,477.8 ns |  2,051.7611 ns |   115.9283 ns | 0.1869 |     800 B |
|               DeserializeClosedDetailsWithSpanJsonSerializer |  1,586.6 ns |    290.1738 ns |    16.3954 ns | 0.1774 |     752 B |
|           DeserializeClosedDetailsWithSpanJsonUtf8Serializer |  1,729.5 ns |    684.7825 ns |    38.6915 ns | 0.1907 |     808 B |
|               DeserializeClosedDetailsWithUtf8JsonSerializer |  2,118.3 ns |    322.1924 ns |    18.2045 ns | 0.1640 |     704 B |
|                           DeserializeNoticeWithJilSerializer |    447.1 ns |     66.1980 ns |     3.7403 ns | 0.0453 |     192 B |
|                      DeserializeNoticeWithSpanJsonSerializer |    339.0 ns |     86.4226 ns |     4.8830 ns | 0.0224 |      96 B |
|                  DeserializeNoticeWithSpanJsonUtf8Serializer |    365.1 ns |     46.2073 ns |     2.6108 ns | 0.0358 |     152 B |
|                      DeserializeNoticeWithUtf8JsonSerializer |    465.7 ns |    106.9844 ns |     6.0448 ns | 0.0224 |      96 B |
|                      SerializeSuggestedEditWithJilSerializer |  1,551.8 ns |    142.6545 ns |     8.0602 ns | 0.8888 |    3736 B |
|                 SerializeSuggestedEditWithSpanJsonSerializer |  1,322.0 ns |    224.6466 ns |    12.6929 ns | 0.2728 |    1152 B |
|             SerializeSuggestedEditWithSpanJsonUtf8Serializer |  1,365.1 ns |     35.3336 ns |     1.9964 ns | 0.1526 |     648 B |
|                 SerializeSuggestedEditWithUtf8JsonSerializer |  1,783.7 ns |    523.9357 ns |    29.6033 ns | 0.1335 |     568 B |
|                                SerializeTagWithJilSerializer |    612.5 ns |    217.4005 ns |    12.2835 ns | 0.3290 |    1384 B |
|                           SerializeTagWithSpanJsonSerializer |    505.3 ns |    349.2973 ns |    19.7359 ns | 0.1020 |     432 B |
|                       SerializeTagWithSpanJsonUtf8Serializer |    503.5 ns |    261.1953 ns |    14.7580 ns | 0.0677 |     288 B |
|                           SerializeTagWithUtf8JsonSerializer |    626.5 ns |    435.1718 ns |    24.5880 ns | 0.0525 |     224 B |
|                           SerializeTagScoreWithJilSerializer |    765.6 ns |    133.0069 ns |     7.5151 ns | 0.5178 |    2176 B |
|                      SerializeTagScoreWithSpanJsonSerializer |    726.7 ns |     85.6915 ns |     4.8417 ns | 0.1459 |     616 B |
|                  SerializeTagScoreWithSpanJsonUtf8Serializer |    765.9 ns |     83.3703 ns |     4.7106 ns | 0.0906 |     384 B |
|                      SerializeTagScoreWithUtf8JsonSerializer |    800.0 ns |    111.8655 ns |     6.3206 ns | 0.0772 |     328 B |
|                         SerializeTagSynonymWithJilSerializer |    632.3 ns |    259.9026 ns |    14.6850 ns | 0.3099 |    1304 B |
|                    SerializeTagSynonymWithSpanJsonSerializer |    468.0 ns |    177.6614 ns |    10.0382 ns | 0.0873 |     368 B |
|                SerializeTagSynonymWithSpanJsonUtf8Serializer |    484.1 ns |     59.0784 ns |     3.3380 ns | 0.0582 |     248 B |
|                    SerializeTagSynonymWithUtf8JsonSerializer |    634.9 ns |    156.4072 ns |     8.8373 ns | 0.0448 |     192 B |
|                            SerializeTagWikiWithJilSerializer |  1,815.9 ns |  1,139.1178 ns |    64.3623 ns | 1.0090 |    4240 B |
|                       SerializeTagWikiWithSpanJsonSerializer |  1,506.6 ns |    813.6427 ns |    45.9723 ns | 0.3433 |    1448 B |
|                   SerializeTagWikiWithSpanJsonUtf8Serializer |  1,604.1 ns |    183.3474 ns |    10.3595 ns | 0.1850 |     784 B |
|                       SerializeTagWikiWithUtf8JsonSerializer |  1,919.4 ns |    447.7708 ns |    25.2999 ns | 0.1717 |     728 B |
|                             SerializeTopTagWithJilSerializer |    428.7 ns |    157.7999 ns |     8.9160 ns | 0.3085 |    1296 B |
|                        SerializeTopTagWithSpanJsonSerializer |    377.3 ns |     53.9539 ns |     3.0485 ns | 0.0777 |     328 B |
|                    SerializeTopTagWithSpanJsonUtf8Serializer |    379.9 ns |    178.7625 ns |    10.1004 ns | 0.0548 |     232 B |
|                        SerializeTopTagWithUtf8JsonSerializer |    396.6 ns |     82.2479 ns |     4.6472 ns | 0.0434 |     184 B |
|                               SerializeUserWithJilSerializer |  1,937.9 ns |    474.0150 ns |    26.7827 ns | 1.0452 |    4400 B |
|                          SerializeUserWithSpanJsonSerializer |  1,896.6 ns |     48.2542 ns |     2.7265 ns | 0.4272 |    1808 B |
|                      SerializeUserWithSpanJsonUtf8Serializer |  1,956.8 ns |    297.3297 ns |    16.7997 ns | 0.2289 |     968 B |
|                          SerializeUserWithUtf8JsonSerializer |  2,435.9 ns |    444.9189 ns |    25.1387 ns | 0.2098 |     896 B |
|                       SerializeUserTimelineWithJilSerializer |    805.5 ns |    254.2840 ns |    14.3675 ns | 0.5045 |    2120 B |
|                  SerializeUserTimelineWithSpanJsonSerializer |    692.2 ns |    103.3277 ns |     5.8382 ns | 0.1383 |     584 B |
|              SerializeUserTimelineWithSpanJsonUtf8Serializer |    761.7 ns |    287.6236 ns |    16.2513 ns | 0.0849 |     360 B |
|                  SerializeUserTimelineWithUtf8JsonSerializer |    880.2 ns |    345.8368 ns |    19.5404 ns | 0.0715 |     304 B |
|                    SerializeWritePermissionWithJilSerializer |    441.3 ns |    121.3295 ns |     6.8553 ns | 0.3181 |    1336 B |
|               SerializeWritePermissionWithSpanJsonSerializer |    355.6 ns |     99.9181 ns |     5.6456 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithSpanJsonUtf8Serializer |    370.1 ns |     57.7562 ns |     3.2633 ns | 0.0606 |     256 B |
|               SerializeWritePermissionWithUtf8JsonSerializer |    381.8 ns |    187.7895 ns |    10.6105 ns | 0.0472 |     200 B |
|                SerializeMobileBannerAdImageWithJilSerializer |    273.6 ns |     21.0226 ns |     1.1878 ns | 0.1407 |     592 B |
|           SerializeMobileBannerAdImageWithSpanJsonSerializer |    239.8 ns |     37.1808 ns |     2.1008 ns | 0.0377 |     160 B |
|       SerializeMobileBannerAdImageWithSpanJsonUtf8Serializer |    261.7 ns |    100.6469 ns |     5.6867 ns | 0.0343 |     144 B |
|           SerializeMobileBannerAdImageWithUtf8JsonSerializer |    234.4 ns |     44.4277 ns |     2.5102 ns | 0.0207 |      88 B |
|                               SerializeSiteWithJilSerializer |  2,116.4 ns |  1,672.6829 ns |    94.5097 ns | 0.9575 |    4024 B |
|                          SerializeSiteWithSpanJsonSerializer |  1,475.0 ns |  1,697.5528 ns |    95.9149 ns | 0.3338 |    1408 B |
|                      SerializeSiteWithSpanJsonUtf8Serializer |  1,574.6 ns |    569.4475 ns |    32.1748 ns | 0.1831 |     776 B |
|                          SerializeSiteWithUtf8JsonSerializer |  1,710.7 ns |    125.9040 ns |     7.1138 ns | 0.1659 |     704 B |
|                        SerializeRelatedSiteWithJilSerializer |    386.3 ns |     13.5805 ns |     0.7673 ns | 0.2036 |     856 B |
|                   SerializeRelatedSiteWithSpanJsonSerializer |    254.6 ns |      2.2888 ns |     0.1293 ns | 0.0510 |     216 B |
|               SerializeRelatedSiteWithSpanJsonUtf8Serializer |    297.1 ns |     56.0475 ns |     3.1668 ns | 0.0415 |     176 B |
|                   SerializeRelatedSiteWithUtf8JsonSerializer |    226.5 ns |     12.2781 ns |     0.6937 ns | 0.0284 |     120 B |
|                      SerializeClosedDetailsWithJilSerializer |  1,121.9 ns |    801.5981 ns |    45.2918 ns | 0.5856 |    2464 B |
|                 SerializeClosedDetailsWithSpanJsonSerializer |    923.0 ns |     41.8992 ns |     2.3674 ns | 0.2184 |     920 B |
|             SerializeClosedDetailsWithSpanJsonUtf8Serializer |    994.9 ns |     72.4540 ns |     4.0938 ns | 0.1240 |     528 B |
|                 SerializeClosedDetailsWithUtf8JsonSerializer |  1,123.7 ns |     24.7807 ns |     1.4002 ns | 0.1125 |     480 B |
|                             SerializeNoticeWithJilSerializer |    390.5 ns |     23.3606 ns |     1.3199 ns | 0.1998 |     840 B |
|                        SerializeNoticeWithSpanJsonSerializer |    320.9 ns |     29.1975 ns |     1.6497 ns | 0.0510 |     216 B |
|                    SerializeNoticeWithSpanJsonUtf8Serializer |    318.2 ns |      9.5744 ns |     0.5410 ns | 0.0415 |     176 B |
|                        SerializeNoticeWithUtf8JsonSerializer |    386.0 ns |     10.5166 ns |     0.5942 ns | 0.0281 |     120 B |
|                      SerializeMigrationInfoWithJilSerializer |  2,153.8 ns |    119.3078 ns |     6.7411 ns | 0.9918 |    4168 B |
|                 SerializeMigrationInfoWithSpanJsonSerializer |  1,611.9 ns |     37.1140 ns |     2.0970 ns | 0.3738 |    1576 B |
|             SerializeMigrationInfoWithSpanJsonUtf8Serializer |  1,663.9 ns |    105.8180 ns |     5.9789 ns | 0.2022 |     856 B |
|                 SerializeMigrationInfoWithUtf8JsonSerializer |  1,944.2 ns |     20.8253 ns |     1.1767 ns | 0.1831 |     784 B |
|                         SerializeBadgeCountWithJilSerializer |    223.3 ns |      5.1048 ns |     0.2884 ns | 0.1392 |     584 B |
|                    SerializeBadgeCountWithSpanJsonSerializer |    221.9 ns |      2.3543 ns |     0.1330 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithSpanJsonUtf8Serializer |    232.6 ns |      6.8909 ns |     0.3893 ns | 0.0343 |     144 B |
|                    SerializeBadgeCountWithUtf8JsonSerializer |    223.7 ns |      2.2340 ns |     0.1262 ns | 0.0207 |      88 B |
|                            SerializeStylingWithJilSerializer |    359.0 ns |     15.5921 ns |     0.8810 ns | 0.1807 |     760 B |
|                       SerializeStylingWithSpanJsonSerializer |    214.3 ns |      2.5778 ns |     0.1456 ns | 0.0513 |     216 B |
|                   SerializeStylingWithSpanJsonUtf8Serializer |    249.6 ns |    181.5642 ns |    10.2587 ns | 0.0415 |     176 B |
|                       SerializeStylingWithUtf8JsonSerializer |    168.3 ns |      2.0950 ns |     0.1184 ns | 0.0284 |     120 B |
|                   SerializeOriginalQuestionWithJilSerializer |    318.2 ns |     14.1052 ns |     0.7970 ns | 0.2074 |     872 B |
|              SerializeOriginalQuestionWithSpanJsonSerializer |    275.4 ns |      4.2183 ns |     0.2383 ns | 0.0567 |     240 B |
|          SerializeOriginalQuestionWithSpanJsonUtf8Serializer |    288.5 ns |     17.6485 ns |     0.9972 ns | 0.0434 |     184 B |
|              SerializeOriginalQuestionWithUtf8JsonSerializer |    267.0 ns |     11.9966 ns |     0.6778 ns | 0.0300 |     128 B |
|                      DeserializeAccessTokenWithJilSerializer |    549.3 ns |      6.0304 ns |     0.3407 ns | 0.0811 |     344 B |
|                 DeserializeAccessTokenWithSpanJsonSerializer |    446.0 ns |     24.2433 ns |     1.3698 ns | 0.0587 |     248 B |
|             DeserializeAccessTokenWithSpanJsonUtf8Serializer |    500.7 ns |     24.4074 ns |     1.3791 ns | 0.0715 |     304 B |
|                 DeserializeAccessTokenWithUtf8JsonSerializer |    655.5 ns |      6.3423 ns |     0.3583 ns | 0.0582 |     248 B |
|                     DeserializeAccountMergeWithJilSerializer |    421.6 ns |     25.9902 ns |     1.4685 ns | 0.0343 |     144 B |
|                DeserializeAccountMergeWithSpanJsonSerializer |    360.2 ns |     40.0798 ns |     2.2646 ns | 0.0110 |      48 B |
|            DeserializeAccountMergeWithSpanJsonUtf8Serializer |    371.4 ns |     50.3386 ns |     2.8442 ns | 0.0243 |     104 B |
|                DeserializeAccountMergeWithUtf8JsonSerializer |    402.1 ns |     17.6059 ns |     0.9948 ns | 0.0110 |      48 B |
|                           DeserializeAnswerWithJilSerializer |  9,189.2 ns |    494.1523 ns |    27.9205 ns | 0.5951 |    2528 B |
|                      DeserializeAnswerWithSpanJsonSerializer |  7,046.7 ns |    279.7332 ns |    15.8054 ns | 0.5417 |    2296 B |
|                  DeserializeAnswerWithSpanJsonUtf8Serializer |  7,519.9 ns |    220.1805 ns |    12.4406 ns | 0.5646 |    2376 B |
|                      DeserializeAnswerWithUtf8JsonSerializer |  8,093.9 ns |    570.8608 ns |    32.2547 ns | 0.4730 |    2048 B |
|                            DeserializeBadgeWithJilSerializer |  1,814.9 ns |    262.6700 ns |    14.8413 ns | 0.1392 |     584 B |
|                       DeserializeBadgeWithSpanJsonSerializer |  1,383.5 ns |     62.7264 ns |     3.5442 ns | 0.1469 |     624 B |
|                   DeserializeBadgeWithSpanJsonUtf8Serializer |  1,532.4 ns |     27.4567 ns |     1.5514 ns | 0.1583 |     672 B |
|                       DeserializeBadgeWithUtf8JsonSerializer |  1,857.3 ns |     94.6072 ns |     5.3455 ns | 0.1144 |     488 B |
|                          DeserializeCommentWithJilSerializer |  3,265.2 ns |     43.0103 ns |     2.4302 ns | 0.2556 |    1080 B |
|                     DeserializeCommentWithSpanJsonSerializer |  2,588.9 ns |     59.1044 ns |     3.3395 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithSpanJsonUtf8Serializer |  2,872.6 ns |  1,222.8400 ns |    69.0927 ns | 0.2327 |     992 B |
|                     DeserializeCommentWithUtf8JsonSerializer |  3,204.3 ns |     80.0867 ns |     4.5250 ns | 0.1869 |     792 B |
|                            DeserializeErrorWithJilSerializer |    322.1 ns |     14.5682 ns |     0.8231 ns | 0.0548 |     232 B |
|                       DeserializeErrorWithSpanJsonSerializer |    235.9 ns |      4.9798 ns |     0.2814 ns | 0.0319 |     136 B |
|                   DeserializeErrorWithSpanJsonUtf8Serializer |    285.3 ns |     12.6048 ns |     0.7122 ns | 0.0453 |     192 B |
|                       DeserializeErrorWithUtf8JsonSerializer |    349.1 ns |      9.3910 ns |     0.5306 ns | 0.0319 |     136 B |
|                            DeserializeEventWithJilSerializer |    582.7 ns |     22.7622 ns |     1.2861 ns | 0.0601 |     256 B |
|                       DeserializeEventWithSpanJsonSerializer |    491.5 ns |    165.1674 ns |     9.3323 ns | 0.0505 |     216 B |
|                   DeserializeEventWithSpanJsonUtf8Serializer |    562.0 ns |     21.0918 ns |     1.1917 ns | 0.0639 |     272 B |
|                       DeserializeEventWithUtf8JsonSerializer |    687.6 ns |     27.7813 ns |     1.5697 ns | 0.0372 |     160 B |
|                       DeserializeMobileFeedWithJilSerializer | 23,894.5 ns |  1,034.5808 ns |    58.4557 ns | 1.3428 |    5712 B |
|                  DeserializeMobileFeedWithSpanJsonSerializer | 11,507.0 ns |    274.4042 ns |    15.5043 ns | 1.2512 |    5280 B |
|              DeserializeMobileFeedWithSpanJsonUtf8Serializer | 12,545.7 ns |    448.4851 ns |    25.3402 ns | 1.2665 |    5328 B |
|                  DeserializeMobileFeedWithUtf8JsonSerializer | 14,240.6 ns |  7,259.4918 ns |   410.1747 ns | 1.2207 |    5136 B |
|                   DeserializeMobileQuestionWithJilSerializer |  1,297.5 ns |      7.7488 ns |     0.4378 ns | 0.1087 |     464 B |
|              DeserializeMobileQuestionWithSpanJsonSerializer |    933.1 ns |     35.3160 ns |     1.9954 ns | 0.0868 |     368 B |
|          DeserializeMobileQuestionWithSpanJsonUtf8Serializer |    987.3 ns |    103.5569 ns |     5.8512 ns | 0.0992 |     424 B |
|              DeserializeMobileQuestionWithUtf8JsonSerializer |  1,102.7 ns |     92.1708 ns |     5.2078 ns | 0.0858 |     368 B |
|                  DeserializeMobileRepChangeWithJilSerializer |    600.2 ns |     15.9035 ns |     0.8986 ns | 0.0734 |     312 B |
|             DeserializeMobileRepChangeWithSpanJsonSerializer |    444.3 ns |     85.9015 ns |     4.8536 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithSpanJsonUtf8Serializer |    489.1 ns |     16.0609 ns |     0.9075 ns | 0.0639 |     272 B |
|             DeserializeMobileRepChangeWithUtf8JsonSerializer |    586.8 ns |     22.9024 ns |     1.2940 ns | 0.0515 |     216 B |
|                  DeserializeMobileInboxItemWithJilSerializer |  1,097.7 ns |     62.6017 ns |     3.5371 ns | 0.1087 |     456 B |
|             DeserializeMobileInboxItemWithSpanJsonSerializer |    820.5 ns |     18.7919 ns |     1.0618 ns | 0.0849 |     360 B |
|         DeserializeMobileInboxItemWithSpanJsonUtf8Serializer |    960.3 ns |     21.4665 ns |     1.2129 ns | 0.0973 |     416 B |
|             DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,039.3 ns |     23.7812 ns |     1.3437 ns | 0.0839 |     360 B |
|                 DeserializeMobileBadgeAwardWithJilSerializer |    937.9 ns |    258.1100 ns |    14.5837 ns | 0.0925 |     392 B |
|            DeserializeMobileBadgeAwardWithSpanJsonSerializer |    750.4 ns |     44.9212 ns |     2.5381 ns | 0.0906 |     384 B |
|        DeserializeMobileBadgeAwardWithSpanJsonUtf8Serializer |    873.7 ns |      4.9850 ns |     0.2817 ns | 0.1030 |     432 B |
|            DeserializeMobileBadgeAwardWithUtf8JsonSerializer |  1,030.8 ns |     80.9710 ns |     4.5750 ns | 0.0687 |     296 B |
|                  DeserializeMobilePrivilegeWithJilSerializer |    888.5 ns |     18.8607 ns |     1.0657 ns | 0.0887 |     376 B |
|             DeserializeMobilePrivilegeWithSpanJsonSerializer |    680.8 ns |     16.0193 ns |     0.9051 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithSpanJsonUtf8Serializer |    754.2 ns |     38.5878 ns |     2.1803 ns | 0.0801 |     336 B |
|             DeserializeMobilePrivilegeWithUtf8JsonSerializer |    831.6 ns |      6.8481 ns |     0.3869 ns | 0.0658 |     280 B |
|          DeserializeMobileCommunityBulletinWithJilSerializer |  1,520.2 ns |     25.5238 ns |     1.4421 ns | 0.1392 |     584 B |
|     DeserializeMobileCommunityBulletinWithSpanJsonSerializer |  1,236.2 ns |     19.0282 ns |     1.0751 ns | 0.1259 |     536 B |
| DeserializeMobileCommunityBulletinWithSpanJsonUtf8Serializer |  1,443.9 ns |     81.7598 ns |     4.6196 ns | 0.1411 |     600 B |
|     DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,476.4 ns |     39.7682 ns |     2.2470 ns | 0.1144 |     488 B |
|                        SerializeAccessTokenWithJilSerializer |    490.8 ns |     61.3212 ns |     3.4648 ns | 0.2127 |     896 B |
|                   SerializeAccessTokenWithSpanJsonSerializer |    365.7 ns |     65.3914 ns |     3.6947 ns | 0.0644 |     272 B |
|               SerializeAccessTokenWithSpanJsonUtf8Serializer |    395.5 ns |      9.1614 ns |     0.5176 ns | 0.0491 |     208 B |
|                   SerializeAccessTokenWithUtf8JsonSerializer |    505.0 ns |     13.0083 ns |     0.7350 ns | 0.0334 |     144 B |
|                       SerializeAccountMergeWithJilSerializer |    372.5 ns |      4.7423 ns |     0.2679 ns | 0.2036 |     856 B |
|                  SerializeAccountMergeWithSpanJsonSerializer |    323.5 ns |      6.1900 ns |     0.3497 ns | 0.0548 |     232 B |
|              SerializeAccountMergeWithSpanJsonUtf8Serializer |    312.8 ns |    160.7598 ns |     9.0832 ns | 0.0434 |     184 B |
|                  SerializeAccountMergeWithUtf8JsonSerializer |    361.9 ns |     15.0237 ns |     0.8489 ns | 0.0281 |     120 B |
|                             SerializeAnswerWithJilSerializer |  4,227.3 ns |    157.5672 ns |     8.9028 ns | 2.0981 |    8824 B |
|                        SerializeAnswerWithSpanJsonSerializer |  4,016.8 ns |    132.1627 ns |     7.4674 ns | 0.8926 |    3752 B |
|                    SerializeAnswerWithSpanJsonUtf8Serializer |  4,194.9 ns |    255.2071 ns |    14.4197 ns | 0.4578 |    1936 B |
|                        SerializeAnswerWithUtf8JsonSerializer |  5,138.5 ns |     87.7441 ns |     4.9577 ns | 0.4349 |    1848 B |
|                             
