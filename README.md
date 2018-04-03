# SpanJson
SpanJson
Sandbox for playing around with Span and JSON Serialization
``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=3906246 Hz, Resolution=256.0003 ns, Timer=TSC
.NET Core SDK=2.1.300-preview1-008174
  [Host]   : .NET Core 2.1.0-preview3-26403-01 (CoreCLR 4.6.26402.06, CoreFX 4.6.26402.05), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26403-01 (CoreCLR 4.6.26402.06, CoreFX 4.6.26402.05), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                 Method |        Mean |         Error |      StdDev |  Gen 0 | Allocated |
|------------------------------------------------------- |------------:|--------------:|------------:|-------:|----------:|
|                    SerializeSiteWithUtf8JsonSerializer |  2,379.7 ns |   293.4178 ns |  16.5787 ns | 0.1755 |     744 B |
|                  SerializeRelatedSiteWithJilSerializer |    367.9 ns |     1.8134 ns |   0.1025 ns | 0.2017 |     848 B |
|             SerializeRelatedSiteWithSpanJsonSerializer |    284.5 ns |    11.5200 ns |   0.6509 ns | 0.0510 |     216 B |
|             SerializeRelatedSiteWithUtf8JsonSerializer |    219.9 ns |     4.3569 ns |   0.2462 ns | 0.0284 |     120 B |
|                SerializeClosedDetailsWithJilSerializer |  1,401.7 ns | 8,232.7958 ns | 465.1682 ns | 0.5875 |    2472 B |
|           SerializeClosedDetailsWithSpanJsonSerializer |  1,173.1 ns |     6.6566 ns |   0.3761 ns | 0.2155 |     912 B |
|           SerializeClosedDetailsWithUtf8JsonSerializer |  1,074.3 ns |     6.7780 ns |   0.3830 ns | 0.1106 |     472 B |
|                       SerializeNoticeWithJilSerializer |    667.1 ns |    21.0265 ns |   1.1880 ns | 0.2012 |     848 B |
|                  SerializeNoticeWithSpanJsonSerializer |    500.8 ns |     0.9640 ns |   0.0545 ns | 0.0525 |     224 B |
|                  SerializeNoticeWithUtf8JsonSerializer |    591.4 ns |     2.6132 ns |   0.1477 ns | 0.0296 |     128 B |
|                SerializeMigrationInfoWithJilSerializer |  2,993.3 ns |    18.1745 ns |   1.0269 ns | 1.0033 |    4216 B |
|           SerializeMigrationInfoWithSpanJsonSerializer |  2,480.8 ns |    38.1145 ns |   2.1535 ns | 0.3815 |    1616 B |
|           SerializeMigrationInfoWithUtf8JsonSerializer |  2,901.9 ns |    33.7189 ns |   1.9052 ns | 0.1945 |     824 B |
|                   SerializeBadgeCountWithJilSerializer |    223.2 ns |     2.5551 ns |   0.1444 ns | 0.1392 |     584 B |
|              SerializeBadgeCountWithSpanJsonSerializer |    279.5 ns |     0.4503 ns |   0.0254 ns | 0.0339 |     144 B |
|              SerializeBadgeCountWithUtf8JsonSerializer |    220.2 ns |     0.4545 ns |   0.0257 ns | 0.0207 |      88 B |
|                      SerializeStylingWithJilSerializer |    365.6 ns |     5.3839 ns |   0.3042 ns | 0.1807 |     760 B |
|                 SerializeStylingWithSpanJsonSerializer |    237.8 ns |    52.6565 ns |   2.9752 ns | 0.0513 |     216 B |
|                 SerializeStylingWithUtf8JsonSerializer |    169.5 ns |    12.0744 ns |   0.6822 ns | 0.0284 |     120 B |
|             SerializeOriginalQuestionWithJilSerializer |    333.0 ns |   146.0360 ns |   8.2513 ns | 0.2074 |     872 B |
|        SerializeOriginalQuestionWithSpanJsonSerializer |    345.5 ns |   288.9455 ns |  16.3260 ns | 0.0548 |     232 B |
|        SerializeOriginalQuestionWithUtf8JsonSerializer |    265.5 ns |    40.4636 ns |   2.2863 ns | 0.0300 |     128 B |
|             SerializeNetworkUserWithSpanJsonSerializer |  1,404.3 ns |     3.4729 ns |   0.1962 ns | 0.1812 |     768 B |
|             SerializeNetworkUserWithUtf8JsonSerializer |  1,574.1 ns |    10.5824 ns |   0.5979 ns | 0.0935 |     400 B |
|                 SerializeNotificationWithJilSerializer |  3,110.2 ns |    71.1729 ns |   4.0214 ns | 1.0376 |    4360 B |
|            SerializeNotificationWithSpanJsonSerializer |  2,590.1 ns |    25.5791 ns |   1.4453 ns | 0.4158 |    1752 B |
|            SerializeNotificationWithUtf8JsonSerializer |  3,145.8 ns |    88.4711 ns |   4.9988 ns | 0.2098 |     896 B |
|                         SerializePostWithJilSerializer |  4,629.7 ns | 1,923.6597 ns | 108.6903 ns | 2.0142 |    8480 B |
|                    SerializePostWithSpanJsonSerializer |  4,981.9 ns |   150.8151 ns |   8.5213 ns | 0.8011 |    3392 B |
|                    SerializePostWithUtf8JsonSerializer |  5,215.9 ns |    50.5250 ns |   2.8548 ns | 0.4044 |    1712 B |
|                    SerializePrivilegeWithJilSerializer |    326.4 ns |    24.9133 ns |   1.4076 ns | 0.1979 |     832 B |
|               SerializePrivilegeWithSpanJsonSerializer |    256.8 ns |     0.1511 ns |   0.0085 ns | 0.0453 |     192 B |
|               SerializePrivilegeWithUtf8JsonSerializer |    184.3 ns |     5.3606 ns |   0.3029 ns | 0.0246 |     104 B |
|                     SerializeQuestionWithJilSerializer | 19,116.7 ns |   196.4371 ns |  11.0991 ns | 7.3242 |   30808 B |
|                SerializeQuestionWithSpanJsonSerializer | 19,314.3 ns |   338.9127 ns |  19.1492 ns | 3.0212 |   12712 B |
|                SerializeQuestionWithUtf8JsonSerializer | 23,357.0 ns | 4,224.3009 ns | 238.6808 ns | 1.4954 |    6360 B |
|             SerializeQuestionTimelineWithJilSerializer |  1,799.9 ns |    28.3285 ns |   1.6006 ns | 1.0319 |    4336 B |
|        SerializeQuestionTimelineWithSpanJsonSerializer |  2,114.1 ns |   225.2162 ns |  12.7251 ns | 0.3510 |    1480 B |
|        SerializeQuestionTimelineWithUtf8JsonSerializer |  2,062.2 ns |    13.7467 ns |   0.7767 ns | 0.1793 |     768 B |
|                   SerializeReputationWithJilSerializer |    793.6 ns |     9.3715 ns |   0.5295 ns | 0.3328 |    1400 B |
|              SerializeReputationWithSpanJsonSerializer |    834.7 ns |   112.5561 ns |   6.3596 ns | 0.1020 |     432 B |
|              SerializeReputationWithUtf8JsonSerializer |    893.5 ns |    14.7454 ns |   0.8331 ns | 0.0544 |     232 B |
|            SerializeReputationHistoryWithJilSerializer |    686.5 ns |   446.6324 ns |  25.2356 ns | 0.3119 |    1312 B |
|       SerializeReputationHistoryWithSpanJsonSerializer |    647.2 ns |     3.3445 ns |   0.1890 ns | 0.0868 |     368 B |
|       SerializeReputationHistoryWithUtf8JsonSerializer |    746.5 ns |    28.8910 ns |   1.6324 ns | 0.0448 |     192 B |
|                     SerializeRevisionWithJilSerializer |  1,795.9 ns |    40.5441 ns |   2.2908 ns | 0.9327 |    3920 B |
|                SerializeRevisionWithSpanJsonSerializer |  1,809.3 ns |     2.8147 ns |   0.1590 ns | 0.3033 |    1280 B |
|                SerializeRevisionWithUtf8JsonSerializer |  1,794.5 ns |    75.9299 ns |   4.2902 ns | 0.1526 |     648 B |
|                SerializeSearchExcerptWithJilSerializer |  3,333.4 ns |    48.8861 ns |   2.7622 ns | 1.1444 |    4816 B |
|           SerializeSearchExcerptWithSpanJsonSerializer |  3,483.2 ns |   654.4405 ns |  36.9771 ns | 0.4845 |    2048 B |
|           SerializeSearchExcerptWithUtf8JsonSerializer |  4,066.9 ns |    46.9915 ns |   2.6551 ns | 0.2441 |    1048 B |
|                  SerializeShallowUserWithJilSerializer |    607.0 ns |     7.8932 ns |   0.4460 ns | 0.3519 |    1480 B |
|             SerializeShallowUserWithSpanJsonSerializer |    746.1 ns |   104.7563 ns |   5.9189 ns | 0.1211 |     512 B |
|             SerializeShallowUserWithUtf8JsonSerializer |    597.4 ns |     3.4414 ns |   0.1944 ns | 0.0620 |     264 B |
|                SerializeSuggestedEditWithJilSerializer |  2,096.3 ns |    10.7571 ns |   0.6078 ns | 0.8926 |    3760 B |
|           SerializeSuggestedEditWithSpanJsonSerializer |  2,027.4 ns |    37.3830 ns |   2.1122 ns | 0.2747 |    1160 B |
|           SerializeSuggestedEditWithUtf8JsonSerializer |  2,372.3 ns |    19.4457 ns |   1.0987 ns | 0.1411 |     600 B |
|                          SerializeTagWithJilSerializer |    834.9 ns |   219.9398 ns |  12.4270 ns | 0.3309 |    1392 B |
|                     SerializeTagWithSpanJsonSerializer |    755.6 ns |     5.5355 ns |   0.3128 ns | 0.1040 |     440 B |
|                     SerializeTagWithUtf8JsonSerializer |    816.3 ns |    43.7001 ns |   2.4691 ns | 0.0544 |     232 B |
|                     SerializeTagScoreWithJilSerializer |    738.6 ns |     5.5997 ns |   0.3164 ns | 0.5159 |    2168 B |
|                SerializeTagScoreWithSpanJsonSerializer |    885.6 ns |    28.9562 ns |   1.6361 ns | 0.1459 |     616 B |
|                SerializeTagScoreWithUtf8JsonSerializer |    756.3 ns |   330.9897 ns |  18.7015 ns | 0.0753 |     320 B |
|                   SerializeTagSynonymWithJilSerializer |  1,025.4 ns |     5.2889 ns |   0.2988 ns | 0.3128 |    1320 B |
|              SerializeTagSynonymWithSpanJsonSerializer |    853.5 ns |     5.2321 ns |   0.2956 ns | 0.0906 |     384 B |
|              SerializeTagSynonymWithUtf8JsonSerializer |  1,073.8 ns |    35.7084 ns |   2.0176 ns | 0.0477 |     208 B |
|                      SerializeTagWikiWithJilSerializer |  2,086.0 ns |    13.5267 ns |   0.7643 ns | 1.0109 |    4256 B |
|                 SerializeTagWikiWithSpanJsonSerializer |  2,127.4 ns |    16.8069 ns |   0.9496 ns | 0.3433 |    1448 B |
|                 SerializeTagWikiWithUtf8JsonSerializer |  2,242.0 ns |    20.6422 ns |   1.1663 ns | 0.1717 |     736 B |
|                       SerializeTopTagWithJilSerializer |    416.5 ns |    14.3571 ns |   0.8112 ns | 0.3085 |    1296 B |
|                  SerializeTopTagWithSpanJsonSerializer |    488.6 ns |    13.3824 ns |   0.7561 ns | 0.0772 |     328 B |
|                  SerializeTopTagWithUtf8JsonSerializer |    365.1 ns |     1.5823 ns |   0.0894 ns | 0.0415 |     176 B |
|                         SerializeUserWithJilSerializer |  2,727.9 ns |   346.8978 ns |  19.6004 ns | 1.0529 |    4432 B |
|                    SerializeUserWithSpanJsonSerializer |  2,869.9 ns |    44.9765 ns |   2.5413 ns | 0.4349 |    1840 B |
|                    SerializeUserWithUtf8JsonSerializer |  3,229.7 ns |     8.5047 ns |   0.4805 ns | 0.2174 |     928 B |
|                 SerializeUserTimelineWithJilSerializer |    971.3 ns |    17.3134 ns |   0.9782 ns | 0.5016 |    2112 B |
|            SerializeUserTimelineWithSpanJsonSerializer |    975.9 ns |     2.2053 ns |   0.1246 ns | 0.1354 |     576 B |
|            SerializeUserTimelineWithUtf8JsonSerializer |  1,029.5 ns |    44.9333 ns |   2.5388 ns | 0.0706 |     304 B |
|              SerializeWritePermissionWithJilSerializer |    413.1 ns |    11.3459 ns |   0.6411 ns | 0.3181 |    1336 B |
|         SerializeWritePermissionWithSpanJsonSerializer |    444.4 ns |     3.2374 ns |   0.1829 ns | 0.0873 |     368 B |
|         SerializeWritePermissionWithUtf8JsonSerializer |    354.2 ns |     9.9000 ns |   0.5594 ns | 0.0472 |     200 B |
|          SerializeMobileBannerAdImageWithJilSerializer |    255.8 ns |     0.0837 ns |   0.0047 ns | 0.1407 |     592 B |
|     SerializeMobileBannerAdImageWithSpanJsonSerializer |    274.1 ns |     2.7156 ns |   0.1534 ns | 0.0358 |     152 B |
|     SerializeMobileBannerAdImageWithUtf8JsonSerializer |    205.3 ns |     0.5607 ns |   0.0317 ns | 0.0207 |      88 B |
|                         SerializeSiteWithJilSerializer |  2,616.0 ns |    27.1621 ns |   1.5347 ns | 0.9613 |    4048 B |
|                    SerializeSiteWithSpanJsonSerializer |  2,051.4 ns |   133.1807 ns |   7.5250 ns | 0.3433 |    1448 B |
|                  SerializeAccessTokenWithJilSerializer |    700.0 ns |     9.1951 ns |   0.5195 ns | 0.2146 |     904 B |
|             SerializeAccessTokenWithSpanJsonSerializer |    623.2 ns |   145.1142 ns |   8.1992 ns | 0.0658 |     280 B |
|             SerializeAccessTokenWithUtf8JsonSerializer |    679.0 ns |   139.0270 ns |   7.8553 ns | 0.0353 |     152 B |
|                 SerializeAccountMergeWithJilSerializer |    574.7 ns |     2.2578 ns |   0.1276 ns | 0.2050 |     864 B |
|            SerializeAccountMergeWithSpanJsonSerializer |    513.8 ns |     1.4882 ns |   0.0841 ns | 0.0563 |     240 B |
|            SerializeAccountMergeWithUtf8JsonSerializer |    600.3 ns |     3.0514 ns |   0.1724 ns | 0.0315 |     136 B |
|                       SerializeAnswerWithJilSerializer |  5,399.0 ns |   185.4827 ns |  10.4801 ns | 2.0981 |    8824 B |
|                  SerializeAnswerWithSpanJsonSerializer |  5,710.1 ns |   137.2463 ns |   7.7547 ns | 0.9003 |    3784 B |
|                  SerializeAnswerWithUtf8JsonSerializer |  6,326.4 ns |    20.2217 ns |   1.1426 ns | 0.4501 |    1896 B |
|                        SerializeBadgeWithJilSerializer |    964.2 ns |     9.1812 ns |   0.5188 ns | 0.5627 |    2368 B |
|                   SerializeBadgeWithSpanJsonSerializer |  1,111.6 ns |     6.0292 ns |   0.3407 ns | 0.1945 |     824 B |
|                   SerializeBadgeWithUtf8JsonSerializer |  1,002.8 ns |   405.3104 ns |  22.9008 ns | 0.0992 |     424 B |
|                      SerializeCommentWithJilSerializer |  1,881.7 ns |    17.9457 ns |   1.0140 ns | 1.0281 |    4320 B |
|                 SerializeCommentWithSpanJsonSerializer |  2,184.9 ns | 1,163.4515 ns |  65.7372 ns | 0.3548 |    1504 B |
|                 SerializeCommentWithUtf8JsonSerializer |  2,067.5 ns |    44.4302 ns |   2.5104 ns | 0.1793 |     760 B |
|                        SerializeErrorWithJilSerializer |    328.1 ns |     1.0696 ns |   0.0604 ns | 0.1941 |     816 B |
|                   SerializeErrorWithSpanJsonSerializer |    253.6 ns |     1.5375 ns |   0.0869 ns | 0.0396 |     168 B |
|                   SerializeErrorWithUtf8JsonSerializer |    186.1 ns |     0.6301 ns |   0.0356 ns | 0.0226 |      96 B |
|                        SerializeEventWithJilSerializer |    737.1 ns |     2.7888 ns |   0.1576 ns | 0.3004 |    1264 B |
|                   SerializeEventWithSpanJsonSerializer |    627.1 ns |   100.1936 ns |   5.6611 ns | 0.0753 |     320 B |
|                   SerializeEventWithUtf8JsonSerializer |    708.9 ns |     0.5196 ns |   0.0294 ns | 0.0391 |     168 B |
|                   SerializeMobileFeedWithJilSerializer |  7,456.7 ns |   396.7620 ns |  22.4178 ns | 3.7537 |   15792 B |
|              SerializeMobileFeedWithSpanJsonSerializer |  7,515.6 ns |    42.8527 ns |   2.4213 ns | 1.5106 |    6352 B |
|              SerializeMobileFeedWithUtf8JsonSerializer |  7,250.7 ns |   144.9841 ns |   8.1919 ns | 0.7553 |    3192 B |
|               SerializeMobileQuestionWithJilSerializer |    757.8 ns |    12.0509 ns |   0.6809 ns | 0.5159 |    2168 B |
|          SerializeMobileQuestionWithSpanJsonSerializer |    809.8 ns |     1.3331 ns |   0.0753 ns | 0.1459 |     616 B |
|          SerializeMobileQuestionWithUtf8JsonSerializer |    662.5 ns |    40.5163 ns |   2.2892 ns | 0.0753 |     320 B |
|              SerializeMobileRepChangeWithJilSerializer |    502.8 ns |    11.6736 ns |   0.6596 ns | 0.3004 |    1264 B |
|         SerializeMobileRepChangeWithSpanJsonSerializer |    460.7 ns |     3.7398 ns |   0.2113 ns | 0.0701 |     296 B |
|         SerializeMobileRepChangeWithUtf8JsonSerializer |    351.0 ns |     1.4078 ns |   0.0795 ns | 0.0377 |     160 B |
|              SerializeMobileInboxItemWithJilSerializer |    779.6 ns |    14.5092 ns |   0.8198 ns | 0.4988 |    2096 B |
|         SerializeMobileInboxItemWithSpanJsonSerializer |    785.0 ns |     2.3453 ns |   0.1325 ns | 0.1287 |     544 B |
|         SerializeMobileInboxItemWithUtf8JsonSerializer |    615.1 ns |     1.5921 ns |   0.0900 ns | 0.0677 |     288 B |
|             SerializeMobileBadgeAwardWithJilSerializer |    636.8 ns |   133.0762 ns |   7.5191 ns | 0.3443 |    1448 B |
|        SerializeMobileBadgeAwardWithSpanJsonSerializer |    709.0 ns |     1.9159 ns |   0.1082 ns | 0.1154 |     488 B |
|        SerializeMobileBadgeAwardWithUtf8JsonSerializer |    560.8 ns |    22.1703 ns |   1.2527 ns | 0.0601 |     256 B |
|              SerializeMobilePrivilegeWithJilSerializer |    605.1 ns |     6.7333 ns |   0.3804 ns | 0.3462 |    1456 B |
|         SerializeMobilePrivilegeWithSpanJsonSerializer |    624.6 ns |     4.1370 ns |   0.2337 ns | 0.1154 |     488 B |
|         SerializeMobilePrivilegeWithUtf8JsonSerializer |    450.3 ns |     3.9113 ns |   0.2210 ns | 0.0606 |     256 B |
|      SerializeMobileCommunityBulletinWithJilSerializer |    888.2 ns |   459.7248 ns |  25.9753 ns | 0.5426 |    2280 B |
| SerializeMobileCommunityBulletinWithSpanJsonSerializer |    937.0 ns |   189.1869 ns |  10.6894 ns | 0.1726 |     728 B |
| SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    805.9 ns |    19.0647 ns |   1.0772 ns | 0.0887 |     376 B |
|       SerializeMobileAssociationBonusWithJilSerializer |    357.8 ns |    10.0038 ns |   0.5652 ns | 0.2036 |     856 B |
|  SerializeMobileAssociationBonusWithSpanJsonSerializer |    370.0 ns |     0.9882 ns |   0.0558 ns | 0.0510 |     216 B |
|  SerializeMobileAssociationBonusWithUtf8JsonSerializer |    283.9 ns |     1.2085 ns |   0.0683 ns | 0.0281 |     120 B |
|           SerializeMobileCareersJobAdWithJilSerializer |    557.3 ns |     3.5451 ns |   0.2003 ns | 0.3138 |    1320 B |
|      SerializeMobileCareersJobAdWithSpanJsonSerializer |    547.9 ns |     5.2270 ns |   0.2953 ns | 0.0811 |     344 B |
|      SerializeMobileCareersJobAdWithUtf8JsonSerializer |    393.4 ns |     2.4613 ns |   0.1391 ns | 0.0434 |     184 B |
|               SerializeMobileBannerAdWithJilSerializer |    537.1 ns |   159.2928 ns |   9.0003 ns | 0.3061 |    1288 B |
|          SerializeMobileBannerAdWithSpanJsonSerializer |    558.0 ns |     2.7782 ns |   0.1570 ns | 0.0772 |     328 B |
|          SerializeMobileBannerAdWithUtf8JsonSerializer |    432.9 ns |     2.0734 ns |   0.1171 ns | 0.0415 |     176 B |
|           SerializeMobileUpdateNoticeWithJilSerializer |    311.7 ns |     1.2233 ns |   0.0691 ns | 0.1750 |     736 B |
|      SerializeMobileUpdateNoticeWithSpanJsonSerializer |    222.4 ns |     0.7891 ns |   0.0446 ns | 0.0455 |     192 B |
|      SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    171.1 ns |     0.1970 ns |   0.0111 ns | 0.0265 |     112 B |
|                   SerializeFlagOptionWithJilSerializer |    855.5 ns |    37.4841 ns |   2.1179 ns | 0.5884 |    2472 B |
|              SerializeFlagOptionWithSpanJsonSerializer |    869.0 ns |   183.8723 ns |  10.3891 ns | 0.1974 |     832 B |
|              SerializeFlagOptionWithUtf8JsonSerializer |    766.5 ns |     3.2806 ns |   0.1854 ns | 0.1001 |     424 B |
|                    SerializeInboxItemWithJilSerializer |  3,269.2 ns |    12.4666 ns |   0.7044 ns | 1.0719 |    4512 B |
|               SerializeInboxItemWithSpanJsonSerializer |  2,835.1 ns |    25.8111 ns |   1.4584 ns | 0.4539 |    1912 B |
|               SerializeInboxItemWithUtf8JsonSerializer |  3,300.9 ns |    47.2505 ns |   2.6697 ns | 0.2289 |     968 B |
|                         SerializeInfoWithJilSerializer |  3,584.4 ns |    18.9511 ns |   1.0708 ns | 1.6937 |    7120 B |
|                    SerializeInfoWithSpanJsonSerializer |  2,803.1 ns |    14.0772 ns |   0.7954 ns | 0.5112 |    2160 B |
|                    SerializeInfoWithUtf8JsonSerializer |  3,798.5 ns | 1,047.8382 ns |  59.2048 ns | 0.3319 |    1408 B |
|                  SerializeNetworkUserWithJilSerializer |  1,326.2 ns |    38.1604 ns |   2.1561 ns | 0.5474 |    2304 B |
