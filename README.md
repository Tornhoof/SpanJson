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
|                    SerializeSiteWithUtf8JsonSerializer |  2,645.8 ns |   351.3906 ns |  19.8542 ns | 0.1717 |     736 B |
|                  SerializeRelatedSiteWithJilSerializer |    380.2 ns |     6.4826 ns |   0.3663 ns | 0.2017 |     848 B |
|             SerializeRelatedSiteWithSpanJsonSerializer |    239.8 ns |    15.4365 ns |   0.8722 ns | 0.0491 |     208 B |
|             SerializeRelatedSiteWithUtf8JsonSerializer |    224.3 ns |    13.9192 ns |   0.7865 ns | 0.0284 |     120 B |
|                SerializeClosedDetailsWithJilSerializer |  1,162.0 ns |    62.6923 ns |   3.5422 ns | 0.5856 |    2464 B |
|           SerializeClosedDetailsWithSpanJsonSerializer |  1,109.5 ns |    40.4079 ns |   2.2831 ns | 0.2155 |     912 B |
|           SerializeClosedDetailsWithUtf8JsonSerializer |  1,070.6 ns |    45.0366 ns |   2.5446 ns | 0.1087 |     464 B |
|                       SerializeNoticeWithJilSerializer |    651.3 ns |    17.8823 ns |   1.0104 ns | 0.2012 |     848 B |
|                  SerializeNoticeWithSpanJsonSerializer |    488.3 ns |   119.0061 ns |   6.7241 ns | 0.0529 |     224 B |
|                  SerializeNoticeWithUtf8JsonSerializer |    656.4 ns |    11.8030 ns |   0.6669 ns | 0.0277 |     120 B |
|                SerializeMigrationInfoWithJilSerializer |  3,076.2 ns |   104.1411 ns |   5.8842 ns | 0.9995 |    4200 B |
|           SerializeMigrationInfoWithSpanJsonSerializer |  2,446.0 ns |   103.8588 ns |   5.8682 ns | 0.3853 |    1624 B |
|           SerializeMigrationInfoWithUtf8JsonSerializer |  3,101.8 ns |    23.5480 ns |   1.3305 ns | 0.1907 |     816 B |
|                   SerializeBadgeCountWithJilSerializer |    223.3 ns |     5.5367 ns |   0.3128 ns | 0.1392 |     584 B |
|              SerializeBadgeCountWithSpanJsonSerializer |    231.2 ns |     8.5928 ns |   0.4855 ns | 0.0341 |     144 B |
|              SerializeBadgeCountWithUtf8JsonSerializer |    269.6 ns |   475.6958 ns |  26.8777 ns | 0.0188 |      80 B |
|                      SerializeStylingWithJilSerializer |    478.7 ns |   279.2320 ns |  15.7771 ns | 0.1807 |     760 B |
|                 SerializeStylingWithSpanJsonSerializer |    212.4 ns |   189.3840 ns |  10.7005 ns | 0.0510 |     216 B |
|                 SerializeStylingWithUtf8JsonSerializer |    171.0 ns |    71.6290 ns |   4.0472 ns | 0.0284 |     120 B |
|             SerializeOriginalQuestionWithJilSerializer |    347.2 ns |   263.1104 ns |  14.8662 ns | 0.2074 |     872 B |
|        SerializeOriginalQuestionWithSpanJsonSerializer |    297.8 ns |   106.7937 ns |   6.0340 ns | 0.0548 |     232 B |
|        SerializeOriginalQuestionWithUtf8JsonSerializer |    269.8 ns |    84.7082 ns |   4.7862 ns | 0.0300 |     128 B |
|             SerializeNetworkUserWithSpanJsonSerializer |  1,483.1 ns | 1,356.8801 ns |  76.6662 ns | 0.1850 |     784 B |
|             SerializeNetworkUserWithUtf8JsonSerializer |  1,639.9 ns |    45.9964 ns |   2.5989 ns | 0.0935 |     400 B |
|                 SerializeNotificationWithJilSerializer |  3,337.0 ns | 2,183.1896 ns | 123.3543 ns | 1.0338 |    4344 B |
|            SerializeNotificationWithSpanJsonSerializer |  2,631.9 ns |   622.5463 ns |  35.1750 ns | 0.4158 |    1760 B |
|            SerializeNotificationWithUtf8JsonSerializer |  3,348.8 ns |   293.8603 ns |  16.6037 ns | 0.2136 |     904 B |
|                         SerializePostWithJilSerializer |  4,860.8 ns | 1,176.5893 ns |  66.4795 ns | 2.0142 |    8472 B |
|                    SerializePostWithSpanJsonSerializer |  5,271.8 ns | 1,396.5535 ns |  78.9079 ns | 0.8011 |    3392 B |
|                    SerializePostWithUtf8JsonSerializer |  6,018.0 ns |   857.6029 ns |  48.4562 ns | 0.4044 |    1704 B |
|                    SerializePrivilegeWithJilSerializer |    353.3 ns |    21.5337 ns |   1.2167 ns | 0.1979 |     832 B |
|               SerializePrivilegeWithSpanJsonSerializer |    233.0 ns |    72.1085 ns |   4.0743 ns | 0.0455 |     192 B |
|               SerializePrivilegeWithUtf8JsonSerializer |    205.4 ns |    57.8824 ns |   3.2705 ns | 0.0246 |     104 B |
|                     SerializeQuestionWithJilSerializer | 20,328.5 ns | 1,473.0799 ns |  83.2317 ns | 7.3242 |   30800 B |
|                SerializeQuestionWithSpanJsonSerializer | 19,661.0 ns | 1,356.6280 ns |  76.6520 ns | 3.0212 |   12688 B |
|                SerializeQuestionWithUtf8JsonSerializer | 24,357.2 ns | 8,769.3086 ns | 495.4822 ns | 1.4954 |    6360 B |
|             SerializeQuestionTimelineWithJilSerializer |  2,064.8 ns | 1,366.5396 ns |  77.2120 ns | 1.0300 |    4336 B |
|        SerializeQuestionTimelineWithSpanJsonSerializer |  2,126.1 ns |   484.5453 ns |  27.3777 ns | 0.3548 |    1504 B |
|        SerializeQuestionTimelineWithUtf8JsonSerializer |  2,304.4 ns |   404.3745 ns |  22.8479 ns | 0.1793 |     768 B |
|                   SerializeReputationWithJilSerializer |    834.4 ns |   191.5561 ns |  10.8233 ns | 0.3290 |    1384 B |
|              SerializeReputationWithSpanJsonSerializer |    815.3 ns |   272.2754 ns |  15.3841 ns | 0.1040 |     440 B |
|              SerializeReputationWithUtf8JsonSerializer |    958.9 ns |    98.5288 ns |   5.5671 ns | 0.0534 |     232 B |
|            SerializeReputationHistoryWithJilSerializer |    658.4 ns |    35.5123 ns |   2.0065 ns | 0.3195 |    1344 B |
|       SerializeReputationHistoryWithSpanJsonSerializer |    605.8 ns |    13.7064 ns |   0.7744 ns | 0.0849 |     360 B |
|       SerializeReputationHistoryWithUtf8JsonSerializer |    837.7 ns |   127.0848 ns |   7.1805 ns | 0.0467 |     200 B |
|                     SerializeRevisionWithJilSerializer |  1,823.3 ns |    40.6461 ns |   2.2966 ns | 0.9308 |    3912 B |
|                SerializeRevisionWithSpanJsonSerializer |  1,715.3 ns |    77.0284 ns |   4.3522 ns | 0.3033 |    1280 B |
|                SerializeRevisionWithUtf8JsonSerializer |  1,871.3 ns |    48.4965 ns |   2.7401 ns | 0.1526 |     648 B |
|                SerializeSearchExcerptWithJilSerializer |  3,382.4 ns |   307.3567 ns |  17.3662 ns | 1.1444 |    4816 B |
|           SerializeSearchExcerptWithSpanJsonSerializer |  3,445.2 ns |   499.2962 ns |  28.2112 ns | 0.4845 |    2048 B |
|           SerializeSearchExcerptWithUtf8JsonSerializer |  4,159.7 ns |   217.0907 ns |  12.2660 ns | 0.2441 |    1040 B |
|                  SerializeShallowUserWithJilSerializer |    633.5 ns |    52.8313 ns |   2.9851 ns | 0.3538 |    1488 B |
|             SerializeShallowUserWithSpanJsonSerializer |    683.4 ns |    23.1881 ns |   1.3102 ns | 0.1211 |     512 B |
|             SerializeShallowUserWithUtf8JsonSerializer |    632.5 ns |    14.8574 ns |   0.8395 ns | 0.0620 |     264 B |
|                SerializeSuggestedEditWithJilSerializer |  2,147.0 ns |    71.0222 ns |   4.0129 ns | 0.8926 |    3760 B |
|           SerializeSuggestedEditWithSpanJsonSerializer |  1,978.4 ns |    35.6328 ns |   2.0133 ns | 0.2785 |    1176 B |
|           SerializeSuggestedEditWithUtf8JsonSerializer |  2,384.4 ns |   183.7870 ns |  10.3843 ns | 0.1373 |     592 B |
|                          SerializeTagWithJilSerializer |    831.5 ns |    11.8934 ns |   0.6720 ns | 0.3309 |    1392 B |
|                     SerializeTagWithSpanJsonSerializer |    717.5 ns |   329.4485 ns |  18.6144 ns | 0.1040 |     440 B |
|                     SerializeTagWithUtf8JsonSerializer |    881.1 ns |    24.5101 ns |   1.3849 ns | 0.0544 |     232 B |
|                     SerializeTagScoreWithJilSerializer |    833.5 ns |    91.6180 ns |   5.1766 ns | 0.5131 |    2152 B |
|                SerializeTagScoreWithSpanJsonSerializer |    839.4 ns |    69.0021 ns |   3.8987 ns | 0.1459 |     616 B |
|                SerializeTagScoreWithUtf8JsonSerializer |    771.2 ns |    10.7260 ns |   0.6060 ns | 0.0753 |     320 B |
|                   SerializeTagSynonymWithJilSerializer |  1,034.4 ns |    53.0201 ns |   2.9957 ns | 0.3128 |    1320 B |
|              SerializeTagSynonymWithSpanJsonSerializer |    808.2 ns |    13.9501 ns |   0.7882 ns | 0.0906 |     384 B |
|              SerializeTagSynonymWithUtf8JsonSerializer |  1,087.5 ns |   334.3085 ns |  18.8891 ns | 0.0477 |     208 B |
|                      SerializeTagWikiWithJilSerializer |  2,128.4 ns |   136.9640 ns |   7.7387 ns | 1.0109 |    4248 B |
|                 SerializeTagWikiWithSpanJsonSerializer |  2,093.5 ns |   135.2073 ns |   7.6395 ns | 0.3395 |    1432 B |
|                 SerializeTagWikiWithUtf8JsonSerializer |  2,452.6 ns |   125.0325 ns |   7.0646 ns | 0.1717 |     728 B |
|                       SerializeTopTagWithJilSerializer |    420.9 ns |    22.2573 ns |   1.2576 ns | 0.3085 |    1296 B |
|                  SerializeTopTagWithSpanJsonSerializer |    444.8 ns |     5.5360 ns |   0.3128 ns | 0.0777 |     328 B |
|                  SerializeTopTagWithUtf8JsonSerializer |    384.0 ns |     9.0171 ns |   0.5095 ns | 0.0415 |     176 B |
|                         SerializeUserWithJilSerializer |  2,762.1 ns |    57.5775 ns |   3.2532 ns | 1.0529 |    4424 B |
|                    SerializeUserWithSpanJsonSerializer |  2,960.6 ns |    52.4760 ns |   2.9650 ns | 0.4349 |    1832 B |
|                    SerializeUserWithUtf8JsonSerializer |  3,411.1 ns |   207.8555 ns |  11.7442 ns | 0.2174 |     928 B |
|                 SerializeUserTimelineWithJilSerializer |    968.1 ns |    45.0171 ns |   2.5435 ns | 0.5035 |    2120 B |
|            SerializeUserTimelineWithSpanJsonSerializer |    930.1 ns |    54.2497 ns |   3.0652 ns | 0.1364 |     576 B |
|            SerializeUserTimelineWithUtf8JsonSerializer |  1,076.8 ns |    44.2683 ns |   2.5012 ns | 0.0706 |     304 B |
|              SerializeWritePermissionWithJilSerializer |    424.8 ns |     5.6334 ns |   0.3183 ns | 0.3161 |    1328 B |
|         SerializeWritePermissionWithSpanJsonSerializer |    394.0 ns |    13.3448 ns |   0.7540 ns | 0.0873 |     368 B |
|         SerializeWritePermissionWithUtf8JsonSerializer |    383.3 ns |   207.1713 ns |  11.7056 ns | 0.0472 |     200 B |
|          SerializeMobileBannerAdImageWithJilSerializer |    265.7 ns |    31.7521 ns |   1.7941 ns | 0.1407 |     592 B |
|     SerializeMobileBannerAdImageWithSpanJsonSerializer |    230.2 ns |     8.8715 ns |   0.5013 ns | 0.0360 |     152 B |
|     SerializeMobileBannerAdImageWithUtf8JsonSerializer |    218.4 ns |     3.8562 ns |   0.2179 ns | 0.0207 |      88 B |
|                         SerializeSiteWithJilSerializer |  2,661.3 ns |    18.1477 ns |   1.0254 ns | 0.9651 |    4056 B |
|                    SerializeSiteWithSpanJsonSerializer |  2,013.7 ns |    93.0800 ns |   5.2592 ns | 0.3433 |    1456 B |
|                  SerializeAccessTokenWithJilSerializer |    717.4 ns |    55.0286 ns |   3.1092 ns | 0.2146 |     904 B |
|             SerializeAccessTokenWithSpanJsonSerializer |    558.8 ns |     8.8154 ns |   0.4981 ns | 0.0658 |     280 B |
|             SerializeAccessTokenWithUtf8JsonSerializer |    799.0 ns |    21.6930 ns |   1.2257 ns | 0.0353 |     152 B |
|                 SerializeAccountMergeWithJilSerializer |    576.3 ns |    30.0123 ns |   1.6957 ns | 0.2050 |     864 B |
|            SerializeAccountMergeWithSpanJsonSerializer |    473.0 ns |    14.7235 ns |   0.8319 ns | 0.0567 |     240 B |
|            SerializeAccountMergeWithUtf8JsonSerializer |    618.9 ns |     9.1663 ns |   0.5179 ns | 0.0315 |     136 B |
|                       SerializeAnswerWithJilSerializer |  5,638.3 ns | 3,393.6676 ns | 191.7485 ns | 2.0981 |    8824 B |
|                  SerializeAnswerWithSpanJsonSerializer |  5,541.7 ns |   132.1017 ns |   7.4640 ns | 0.8926 |    3760 B |
|                  SerializeAnswerWithUtf8JsonSerializer |  6,696.3 ns |   164.5416 ns |   9.2969 ns | 0.4501 |    1896 B |
|                        SerializeBadgeWithJilSerializer |  1,001.6 ns |   440.5301 ns |  24.8908 ns | 0.5627 |    2368 B |
|                   SerializeBadgeWithSpanJsonSerializer |  1,103.4 ns |    25.2949 ns |   1.4292 ns | 0.1965 |     832 B |
|                   SerializeBadgeWithUtf8JsonSerializer |    999.0 ns |     7.5094 ns |   0.4243 ns | 0.0973 |     416 B |
|                      SerializeCommentWithJilSerializer |  1,912.0 ns |    69.8983 ns |   3.9494 ns | 1.0300 |    4328 B |
|                 SerializeCommentWithSpanJsonSerializer |  2,053.6 ns |    62.5300 ns |   3.5331 ns | 0.3510 |    1488 B |
|                 SerializeCommentWithUtf8JsonSerializer |  2,220.4 ns |    34.0159 ns |   1.9220 ns | 0.1793 |     768 B |
|                        SerializeErrorWithJilSerializer |    340.8 ns |     8.4938 ns |   0.4799 ns | 0.1941 |     816 B |
|                   SerializeErrorWithSpanJsonSerializer |    211.5 ns |     2.9601 ns |   0.1672 ns | 0.0417 |     176 B |
|                   SerializeErrorWithUtf8JsonSerializer |    187.8 ns |    13.7419 ns |   0.7764 ns | 0.0226 |      96 B |
|                        SerializeEventWithJilSerializer |    744.2 ns |    36.2946 ns |   2.0507 ns | 0.3023 |    1272 B |
|                   SerializeEventWithSpanJsonSerializer |    566.7 ns |     7.7019 ns |   0.4352 ns | 0.0734 |     312 B |
|                   SerializeEventWithUtf8JsonSerializer |    785.6 ns |     2.6181 ns |   0.1479 ns | 0.0410 |     176 B |
|                   SerializeMobileFeedWithJilSerializer |  7,437.0 ns |    59.5125 ns |   3.3626 ns | 3.7537 |   15776 B |
|              SerializeMobileFeedWithSpanJsonSerializer |  7,463.0 ns |   148.4056 ns |   8.3852 ns | 1.5106 |    6360 B |
|              SerializeMobileFeedWithUtf8JsonSerializer |  7,393.4 ns |   185.4444 ns |  10.4780 ns | 0.7553 |    3200 B |
|               SerializeMobileQuestionWithJilSerializer |    770.5 ns |   171.3687 ns |   9.6826 ns | 0.5159 |    2168 B |
|          SerializeMobileQuestionWithSpanJsonSerializer |    761.6 ns |    30.7755 ns |   1.7389 ns | 0.1459 |     616 B |
|          SerializeMobileQuestionWithUtf8JsonSerializer |    661.7 ns |    12.0070 ns |   0.6784 ns | 0.0753 |     320 B |
|              SerializeMobileRepChangeWithJilSerializer |    503.9 ns |    39.3970 ns |   2.2260 ns | 0.3004 |    1264 B |
|         SerializeMobileRepChangeWithSpanJsonSerializer |    421.3 ns |    13.1219 ns |   0.7414 ns | 0.0701 |     296 B |
|         SerializeMobileRepChangeWithUtf8JsonSerializer |    354.3 ns |    10.0235 ns |   0.5663 ns | 0.0377 |     160 B |
|              SerializeMobileInboxItemWithJilSerializer |    738.3 ns |    28.2181 ns |   1.5944 ns | 0.3605 |    1512 B |
|         SerializeMobileInboxItemWithSpanJsonSerializer |    777.9 ns |    31.6234 ns |   1.7868 ns | 0.1287 |     544 B |
|         SerializeMobileInboxItemWithUtf8JsonSerializer |    615.4 ns |    11.2076 ns |   0.6332 ns | 0.0677 |     288 B |
|             SerializeMobileBadgeAwardWithJilSerializer |    623.8 ns |    26.9661 ns |   1.5236 ns | 0.3443 |    1448 B |
|        SerializeMobileBadgeAwardWithSpanJsonSerializer |    653.2 ns |    47.5063 ns |   2.6842 ns | 0.1135 |     480 B |
|        SerializeMobileBadgeAwardWithUtf8JsonSerializer |    561.5 ns |    27.0789 ns |   1.5300 ns | 0.0582 |     248 B |
|              SerializeMobilePrivilegeWithJilSerializer |    605.5 ns |    10.9101 ns |   0.6164 ns | 0.3443 |    1448 B |
|         SerializeMobilePrivilegeWithSpanJsonSerializer |    563.8 ns |     9.5877 ns |   0.5417 ns | 0.1154 |     488 B |
|         SerializeMobilePrivilegeWithUtf8JsonSerializer |    478.7 ns |     8.7367 ns |   0.4936 ns | 0.0601 |     256 B |
|      SerializeMobileCommunityBulletinWithJilSerializer |    953.4 ns |   263.0631 ns |  14.8636 ns | 0.5426 |    2280 B |
| SerializeMobileCommunityBulletinWithSpanJsonSerializer |    878.6 ns |    47.0240 ns |   2.6569 ns | 0.1764 |     744 B |
| SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    781.5 ns |    23.3472 ns |   1.3192 ns | 0.0887 |     376 B |
|       SerializeMobileAssociationBonusWithJilSerializer |    347.7 ns |     8.5119 ns |   0.4809 ns | 0.2036 |     856 B |
|  SerializeMobileAssociationBonusWithSpanJsonSerializer |    329.5 ns |   291.6313 ns |  16.4777 ns | 0.0510 |     216 B |
|  SerializeMobileAssociationBonusWithUtf8JsonSerializer |    282.2 ns |     7.9108 ns |   0.4470 ns | 0.0281 |     120 B |
|           SerializeMobileCareersJobAdWithJilSerializer |    575.3 ns |    13.9644 ns |   0.7890 ns | 0.3119 |    1312 B |
|      SerializeMobileCareersJobAdWithSpanJsonSerializer |    504.6 ns |    10.3460 ns |   0.5846 ns | 0.0830 |     352 B |
|      SerializeMobileCareersJobAdWithUtf8JsonSerializer |    418.5 ns |   144.6618 ns |   8.1737 ns | 0.0453 |     192 B |
|               SerializeMobileBannerAdWithJilSerializer |    516.3 ns |    15.8101 ns |   0.8933 ns | 0.3061 |    1288 B |
|          SerializeMobileBannerAdWithSpanJsonSerializer |    491.9 ns |    18.2144 ns |   1.0291 ns | 0.0753 |     320 B |
|          SerializeMobileBannerAdWithUtf8JsonSerializer |    439.6 ns |    19.3077 ns |   1.0909 ns | 0.0415 |     176 B |
|           SerializeMobileUpdateNoticeWithJilSerializer |    318.4 ns |     9.0330 ns |   0.5104 ns | 0.1750 |     736 B |
|      SerializeMobileUpdateNoticeWithSpanJsonSerializer |    180.3 ns |     2.8065 ns |   0.1586 ns | 0.0455 |     192 B |
|      SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    160.5 ns |     0.7995 ns |   0.0452 ns | 0.0265 |     112 B |
|                   SerializeFlagOptionWithJilSerializer |    877.1 ns |   118.2467 ns |   6.6812 ns | 0.5865 |    2464 B |
|              SerializeFlagOptionWithSpanJsonSerializer |    796.8 ns |    13.3801 ns |   0.7560 ns | 0.1955 |     824 B |
|              SerializeFlagOptionWithUtf8JsonSerializer |    747.8 ns |     3.1082 ns |   0.1756 ns | 0.1001 |     424 B |
|                    SerializeInboxItemWithJilSerializer |  3,331.2 ns |     5.4290 ns |   0.3068 ns | 1.0719 |    4504 B |
|               SerializeInboxItemWithSpanJsonSerializer |  2,772.8 ns |    23.7879 ns |   1.3441 ns | 0.4539 |    1920 B |
|               SerializeInboxItemWithUtf8JsonSerializer |  3,505.9 ns |   268.4316 ns |  15.1669 ns | 0.2289 |     976 B |
|                         SerializeInfoWithJilSerializer |  3,604.0 ns |    76.2476 ns |   4.3081 ns | 1.6899 |    7104 B |
|                    SerializeInfoWithSpanJsonSerializer |  2,852.2 ns |   937.0535 ns |  52.9453 ns | 0.5074 |    2144 B |
|                    SerializeInfoWithUtf8JsonSerializer |  3,700.7 ns |   194.4595 ns |  10.9873 ns | 0.3319 |    1408 B |
|                  SerializeNetworkUserWithJilSerializer |  1,340.0 ns |    36.3023 ns |   2.0511 ns | 0.5455 |    2296 B |
