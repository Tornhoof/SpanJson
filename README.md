# SpanJson
SpanJson
Sandbox for playing around with Span and JSON Serialization.
This is basically the ValueStringBuilder from CoreFx with the TryFormat API for formatting values with Span<char>.
The number formatting is replaced with UTF8Json (or some other variation) as the CoreCLR version is two times slower.
The DateTime parser is slow too, but not yet replaced
The actual serializers are a T4 Template (BclFormatter.tt).

Deserialization works ok.

TO-DO
* Improve property selection (currently we compare the propertyname for each Switch again, instead of e.g switching on the first char first and then conpare the rest, which should improve Performance
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
|              DeserializeTagSynonymWithJsonSpanSerializer |  1,789.3 ns |    15.2695 ns |   0.8628 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |  1,398.2 ns |     7.2217 ns |   0.4080 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  3,016.5 ns |   204.2308 ns |  11.5394 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  4,178.7 ns |   212.2411 ns |  11.9920 ns | 0.1984 |     864 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  3,377.7 ns |    36.8758 ns |   2.0835 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    589.1 ns |     3.9781 ns |   0.2248 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    803.2 ns |     5.4689 ns |   0.3090 ns | 0.0257 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    520.2 ns |    15.5462 ns |   0.8784 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,974.5 ns |   169.4095 ns |   9.5720 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  9,808.2 ns |   471.4972 ns |  26.6405 ns | 0.1526 |     648 B |
|                    DeserializeUserWithUtf8JsonSerializer |  4,379.6 ns |   150.1562 ns |   8.4841 ns | 0.1373 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,163.1 ns |    12.8817 ns |   0.7278 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  2,134.4 ns |    72.2427 ns |   4.0818 ns | 0.0801 |     352 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,414.0 ns |     0.9074 ns |   0.0513 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    661.0 ns |    13.3734 ns |   0.7556 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    628.2 ns |     3.8596 ns |   0.2181 ns | 0.0277 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    644.3 ns |    36.0222 ns |   2.0353 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    296.5 ns |     1.3225 ns |   0.0747 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    326.7 ns |     3.1179 ns |   0.1762 ns | 0.0205 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    339.6 ns |    14.8693 ns |   0.8401 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,376.3 ns |    72.6416 ns |   4.1044 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  5,291.3 ns |   314.7872 ns |  17.7861 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  4,003.9 ns |    72.1823 ns |   4.0784 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    383.5 ns |    10.7944 ns |   0.6099 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    242.3 ns |   126.9198 ns |   7.1712 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    520.9 ns |   367.3227 ns |  20.7544 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,185.9 ns |    25.4144 ns |   1.4360 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  2,036.1 ns |    26.1012 ns |   1.4748 ns | 0.1793 |     760 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  1,928.7 ns |   128.9902 ns |   7.2882 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    436.5 ns |     5.5930 ns |   0.3160 ns | 0.0453 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |  1,005.9 ns |    29.1412 ns |   1.6465 ns | 0.0210 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    700.1 ns |     4.1706 ns |   0.2356 ns | 0.0219 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,869.9 ns |   171.1769 ns |   9.6718 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  6,285.4 ns |   112.3778 ns |   6.3496 ns | 0.3662 |    1560 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  4,986.9 ns |    45.1044 ns |   2.5485 ns | 0.3433 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    266.1 ns |     3.6467 ns |   0.2060 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    433.9 ns |    21.1987 ns |   1.1978 ns | 0.0091 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    243.5 ns |     1.5097 ns |   0.0853 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    350.3 ns |    10.0717 ns |   0.5691 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    186.7 ns |     1.0443 ns |   0.0590 ns | 0.0436 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    378.1 ns |    15.5858 ns |   0.8806 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    411.4 ns |     2.6633 ns |   0.1505 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    487.8 ns |    22.4775 ns |   1.2700 ns | 0.0219 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    380.7 ns |     1.4588 ns |   0.0824 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  2,382.1 ns |   375.3673 ns |  21.2089 ns | 0.1755 |     744 B |
|                    SerializeRelatedSiteWithJilSerializer |    379.4 ns |     4.8759 ns |   0.2755 ns | 0.2036 |     856 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    250.6 ns |     1.6690 ns |   0.0943 ns | 0.0491 |     208 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    224.7 ns |     0.3674 ns |   0.0208 ns | 0.0284 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,092.2 ns |    27.0374 ns |   1.5277 ns | 0.5875 |    2472 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,224.6 ns |    15.9420 ns |   0.9008 ns | 0.2594 |    1096 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,039.8 ns |     6.8858 ns |   0.3891 ns | 0.1106 |     472 B |
|                         SerializeNoticeWithJilSerializer |    607.8 ns |    33.6589 ns |   1.9018 ns | 0.2012 |     848 B |
|                    SerializeNoticeWithJsonSpanSerializer |    455.6 ns |    19.1786 ns |   1.0836 ns | 0.0529 |     224 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    590.8 ns |     3.6341 ns |   0.2053 ns | 0.0296 |     128 B |
|                  SerializeMigrationInfoWithJilSerializer |  3,069.4 ns |    79.9662 ns |   4.5182 ns | 0.9995 |    4208 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  2,796.2 ns |   118.2841 ns |   6.6833 ns | 0.4578 |    1936 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,937.9 ns |    17.2934 ns |   0.9771 ns | 0.1945 |     824 B |
|                     SerializeBadgeCountWithJilSerializer |    213.9 ns |     0.9453 ns |   0.0534 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    199.5 ns |     0.2299 ns |   0.0130 ns | 0.0341 |     144 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    218.9 ns |     3.1715 ns |   0.1792 ns | 0.0207 |      88 B |
|                        SerializeStylingWithJilSerializer |    369.7 ns |     1.2763 ns |   0.0721 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    196.1 ns |     1.2176 ns |   0.0688 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    164.8 ns |     1.5245 ns |   0.0861 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    323.4 ns |     3.2244 ns |   0.1822 ns | 0.2074 |     872 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    256.5 ns |     2.3620 ns |   0.1335 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    258.5 ns |     0.3651 ns |   0.0206 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    574.2 ns |    14.7143 ns |   0.8314 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |  1,114.6 ns |    17.2509 ns |   0.9747 ns | 0.0572 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    924.9 ns |     9.3681 ns |   0.5293 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    459.8 ns |     2.0344 ns |   0.1149 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |  1,111.3 ns |    68.2514 ns |   3.8563 ns | 0.0095 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    746.0 ns |    11.9015 ns |   0.6725 ns | 0.0105 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  8,975.7 ns |   191.0220 ns |  10.7931 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer | 15,966.9 ns | 1,117.6115 ns |  63.1471 ns | 0.5493 |    2312 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  9,535.9 ns |   543.9753 ns |  30.7356 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,711.4 ns |    72.2940 ns |   4.0847 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,752.1 ns |     3.4920 ns |   0.1973 ns | 0.1469 |     624 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,734.9 ns |    27.1633 ns |   1.5348 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,126.6 ns |   165.5212 ns |   9.3523 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  4,831.8 ns |   225.2941 ns |  12.7295 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,477.8 ns |    17.2041 ns |   0.9721 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    322.5 ns |     5.0742 ns |   0.2867 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    266.3 ns |     4.5081 ns |   0.2547 ns | 0.0319 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    409.5 ns |    23.5098 ns |   1.3283 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    611.1 ns |     3.7279 ns |   0.2106 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |  1,139.3 ns |    39.7463 ns |   2.2457 ns | 0.0496 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |  1,068.7 ns |     5.9725 ns |   0.3375 ns | 0.0362 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 23,406.7 ns |   180.7347 ns |  10.2118 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 20,876.5 ns |   975.2271 ns |  55.1021 ns | 1.2512 |    5264 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 13,658.0 ns |   275.6462 ns |  15.5745 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,276.5 ns |    28.1599 ns |   1.5911 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |  1,787.1 ns |   121.6537 ns |   6.8737 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,132.0 ns |    11.9755 ns |   0.6766 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    578.8 ns |     3.3855 ns |   0.1913 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    656.6 ns |    14.2976 ns |   0.8078 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    576.3 ns |    26.9241 ns |   1.5213 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,040.4 ns |    45.3569 ns |   2.5627 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |  1,576.1 ns |    54.4864 ns |   3.0786 ns | 0.0839 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |    992.6 ns |     8.9284 ns |   0.5045 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    890.5 ns |    38.3637 ns |   2.1676 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |  1,207.8 ns |    59.5478 ns |   3.3646 ns | 0.0877 |     376 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |  1,042.6 ns |   388.9105 ns |  21.9742 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    857.2 ns |    14.2788 ns |   0.8068 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    914.8 ns |    28.7103 ns |   1.6222 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    824.1 ns |    13.6366 ns |   0.7705 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,529.7 ns |   102.5008 ns |   5.7915 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  2,637.3 ns |    24.3463 ns |   1.3756 ns | 0.1259 |     536 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,424.0 ns |     8.4060 ns |   0.4750 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    422.5 ns |    23.1236 ns |   1.3065 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    537.0 ns |    85.6463 ns |   4.8392 ns | 0.0238 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    370.5 ns |     1.0397 ns |   0.0587 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    665.7 ns |    15.7957 ns |   0.8925 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    682.0 ns |     8.5674 ns |   0.4841 ns | 0.0639 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    680.6 ns |    67.1042 ns |   3.7915 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    754.7 ns |     7.9682 ns |   0.4502 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    783.4 ns |    28.9233 ns |   1.6342 ns | 0.0677 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    685.9 ns |    88.7948 ns |   5.0171 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    312.1 ns |     4.4486 ns |   0.2514 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    179.9 ns |     5.6051 ns |   0.3167 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    345.1 ns |     3.4645 ns |   0.1958 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,649.6 ns |    14.3487 ns |   0.8107 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,631.6 ns |   108.4492 ns |   6.1276 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,531.5 ns |    15.6446 ns |   0.8839 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,923.3 ns |    32.7735 ns |   1.8518 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  7,173.2 ns |   586.9929 ns |  33.1662 ns | 0.4272 |    1816 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  5,280.0 ns |    73.7051 ns |   4.1645 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,827.6 ns |   275.9519 ns |  15.5918 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  9,318.1 ns |   566.3573 ns |  32.0002 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  7,075.2 ns |   633.5013 ns |  35.7940 ns | 0.4272 |    1816 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,644.8 ns |    16.8679 ns |   0.9531 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  3,249.3 ns |    73.4049 ns |   4.1475 ns | 0.0725 |     312 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  2,228.7 ns |   123.4997 ns |   6.9780 ns | 0.0572 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,574.6 ns |    64.8320 ns |   3.6631 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  6,485.1 ns |   351.5372 ns |  19.8625 ns | 0.3967 |    1688 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  5,563.4 ns |   512.4823 ns |  28.9562 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  7,822.7 ns |   254.7931 ns |  14.3963 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer | 13,022.9 ns |   922.9302 ns |  52.1473 ns | 0.4883 |    2088 B |
|                    DeserializePostWithUtf8JsonSerializer |  8,171.0 ns |   747.1612 ns |  42.2160 ns | 0.4120 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    339.3 ns |     4.2022 ns |   0.2374 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    270.1 ns |     5.4325 ns |   0.3069 ns | 0.0319 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    339.4 ns |     3.0880 ns |   0.1745 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 42,350.9 ns |   866.7576 ns |  48.9734 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 58,950.8 ns | 9,402.3462 ns | 531.2500 ns | 2.0752 |    8856 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 36,029.2 ns |   114.8333 ns |   6.4883 ns | 1.8921 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,046.5 ns |    50.8868 ns |   2.8752 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  4,279.0 ns |   306.1046 ns |  17.2955 ns | 0.1907 |     816 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,282.7 ns |   180.5620 ns |  10.2021 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    831.6 ns |     6.0159 ns |   0.3399 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |  1,549.0 ns |    89.0258 ns |   5.0301 ns | 0.0687 |     296 B |
|              DeserializeReputationWithUtf8JsonSerializer |  1,218.7 ns |     3.9358 ns |   0.2224 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    667.0 ns |     2.2240 ns |   0.1257 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |  1,430.2 ns |    11.1974 ns |   0.6327 ns | 0.0305 |     136 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    979.7 ns |     6.1360 ns |   0.3467 ns | 0.0134 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  2,992.3 ns |   143.7501 ns |   8.1221 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  4,715.1 ns | 1,064.6779 ns |  60.1563 ns | 0.2670 |    1128 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  3,271.3 ns |    82.7183 ns |   4.6737 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,512.5 ns |    67.3510 ns |   3.8055 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  9,114.8 ns | 1,575.2282 ns |  89.0033 ns | 0.2747 |    1176 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  5,590.3 ns |    75.3437 ns |   4.2571 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,036.9 ns |    17.9178 ns |   1.0124 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |  1,131.0 ns |     4.0957 ns |   0.2314 ns | 0.0744 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,114.8 ns |    42.9722 ns |   2.4280 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,639.2 ns |   104.3617 ns |   5.8966 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  4,483.6 ns |   566.9887 ns |  32.0359 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  3,273.5 ns |   107.7514 ns |   6.0882 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    913.5 ns |    82.4765 ns |   4.6601 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |  1,464.9 ns |    35.1321 ns |   1.9850 ns | 0.0648 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |  1,305.0 ns |    21.4350 ns |   1.2111 ns | 0.0648 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,315.9 ns |    66.0167 ns |   3.7301 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |  1,464.5 ns |    49.1664 ns |   2.7780 ns | 0.0839 |     360 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,226.7 ns |    45.8723 ns |   2.5919 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    792.2 ns |    13.2030 ns |   0.7460 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    713.6 ns |    10.0815 ns |   0.5696 ns | 0.2146 |     904 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    605.3 ns |    14.7567 ns |   0.8338 ns | 0.0811 |     344 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    691.0 ns |     9.8886 ns |   0.5587 ns | 0.0353 |     152 B |
|                   SerializeAccountMergeWithJilSerializer |    573.0 ns |     2.2815 ns |   0.1289 ns | 0.2050 |     864 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    450.1 ns |     0.8761 ns |   0.0495 ns | 0.0567 |     240 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    611.0 ns |     5.2461 ns |   0.2964 ns | 0.0315 |     136 B |
|                         SerializeAnswerWithJilSerializer |  5,459.2 ns |   337.7715 ns |  19.0847 ns | 2.0981 |    8832 B |
|                    SerializeAnswerWithJsonSpanSerializer |  6,049.7 ns |    59.2272 ns |   3.3464 ns | 1.0452 |    4416 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  6,267.2 ns |    88.5341 ns |   5.0023 ns | 0.4501 |    1896 B |
|                          SerializeBadgeWithJilSerializer |    971.4 ns |     2.2967 ns |   0.1298 ns | 0.5665 |    2384 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,143.1 ns |     8.1593 ns |   0.4610 ns | 0.2251 |     952 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,003.9 ns |     5.2988 ns |   0.2994 ns | 0.0973 |     416 B |
|                        SerializeCommentWithJilSerializer |  1,910.6 ns |    43.4664 ns |   2.4559 ns | 1.0300 |    4328 B |
|                   SerializeCommentWithJsonSpanSerializer |  2,189.2 ns |     7.1748 ns |   0.4054 ns | 0.4158 |    1760 B |
|                   SerializeCommentWithUtf8JsonSerializer |  2,070.9 ns |    17.0437 ns |   0.9630 ns | 0.1793 |     760 B |
|                          SerializeErrorWithJilSerializer |    334.2 ns |     1.2475 ns |   0.0705 ns | 0.1941 |     816 B |
|                     SerializeErrorWithJsonSpanSerializer |    199.9 ns |     8.4693 ns |   0.4785 ns | 0.0398 |     168 B |
|                     SerializeErrorWithUtf8JsonSerializer |    185.6 ns |     0.4115 ns |   0.0232 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    740.0 ns |     2.6151 ns |   0.1478 ns | 0.2985 |    1256 B |
|                     SerializeEventWithJsonSpanSerializer |    573.3 ns |     2.5010 ns |   0.1413 ns | 0.0753 |     320 B |
|                     SerializeEventWithUtf8JsonSerializer |    707.8 ns |     2.5720 ns |   0.1453 ns | 0.0391 |     168 B |
|                     SerializeMobileFeedWithJilSerializer |  7,550.6 ns |   322.6009 ns |  18.2276 ns | 3.7537 |   15776 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,787.3 ns |    88.6415 ns |   5.0084 ns | 1.7700 |    7440 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,260.4 ns |   395.3613 ns |  22.3386 ns | 0.7553 |    3200 B |
|                 SerializeMobileQuestionWithJilSerializer |    759.0 ns |    43.0394 ns |   2.4318 ns | 0.5159 |    2168 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    714.4 ns |     2.9392 ns |   0.1661 ns | 0.1612 |     680 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    646.6 ns |     2.8589 ns |   0.1615 ns | 0.0753 |     320 B |
|                SerializeMobileRepChangeWithJilSerializer |    501.7 ns |     4.7021 ns |   0.2657 ns | 0.3004 |    1264 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    380.3 ns |     1.4246 ns |   0.0805 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    352.0 ns |     1.1607 ns |   0.0656 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    791.3 ns |    15.2270 ns |   0.8604 ns | 0.4988 |    2096 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    651.5 ns |     5.4338 ns |   0.3070 ns | 0.1307 |     552 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    603.2 ns |     5.1711 ns |   0.2922 ns | 0.0658 |     280 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    630.4 ns |     6.6637 ns |   0.3765 ns | 0.3443 |    1448 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    614.9 ns |     2.0669 ns |   0.1168 ns | 0.1135 |     480 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    565.9 ns |     1.4179 ns |   0.0801 ns | 0.0582 |     248 B |
|                SerializeMobilePrivilegeWithJilSerializer |    614.4 ns |    10.4408 ns |   0.5899 ns | 0.3462 |    1456 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    506.7 ns |     2.7391 ns |   0.1548 ns | 0.1173 |     496 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    444.6 ns |     2.0947 ns |   0.1184 ns | 0.0606 |     256 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    889.5 ns |    13.1407 ns |   0.7425 ns | 0.5426 |    2280 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    863.4 ns |     5.2667 ns |   0.2976 ns | 0.1879 |     792 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    776.8 ns |     2.1715 ns |   0.1227 ns | 0.0887 |     376 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    341.4 ns |     3.3161 ns |   0.1874 ns | 0.2036 |     856 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    287.2 ns |   318.7820 ns |  18.0118 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    275.2 ns |     2.1094 ns |   0.1192 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    578.4 ns |     2.0053 ns |   0.1133 ns | 0.3119 |    1312 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    441.2 ns |     2.7344 ns |   0.1545 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    390.8 ns |     1.4013 ns |   0.0792 ns | 0.0434 |     184 B |
|                 SerializeMobileBannerAdWithJilSerializer |    520.6 ns |     5.3715 ns |   0.3035 ns | 0.3061 |    1288 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    499.4 ns |     3.3114 ns |   0.1871 ns | 0.0906 |     384 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    419.8 ns |     4.0623 ns |   0.2295 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    321.8 ns |     1.8780 ns |   0.1061 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    187.3 ns |     2.3874 ns |   0.1349 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    163.7 ns |     0.4363 ns |   0.0247 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    854.3 ns |     9.6203 ns |   0.5436 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    846.8 ns |    14.6885 ns |   0.8299 ns | 0.2108 |     888 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    745.0 ns |    14.4781 ns |   0.8180 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  3,490.5 ns |    38.7370 ns |   2.1887 ns | 1.0757 |    4520 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  3,148.9 ns |    35.0578 ns |   1.9808 ns | 0.5302 |    2232 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  3,316.8 ns |   158.4847 ns |   8.9547 ns | 0.2289 |     968 B |
|                           SerializeInfoWithJilSerializer |  3,602.3 ns |    86.5641 ns |   4.8910 ns | 1.6899 |    7104 B |
|                      SerializeInfoWithJsonSpanSerializer |  3,428.9 ns |    44.9285 ns |   2.5385 ns | 0.5989 |    2528 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,647.0 ns |    10.5839 ns |   0.5980 ns | 0.3319 |    1408 B |
|                    SerializeNetworkUserWithJilSerializer |  1,336.6 ns |     2.5293 ns |   0.1429 ns | 0.5455 |    2296 B |
|               SerializeNetworkUserWithJsonSpanSerializer |  1,396.7 ns |     4.0435 ns |   0.2285 ns | 0.1965 |     832 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,574.1 ns |    70.5324 ns |   3.9852 ns | 0.0935 |     400 B |
|                   SerializeNotificationWithJilSerializer |  3,203.5 ns |    56.3324 ns |   3.1829 ns | 1.0338 |    4352 B |
|              SerializeNotificationWithJsonSpanSerializer |  3,029.7 ns |    71.2986 ns |   4.0285 ns | 0.4997 |    2104 B |
|              SerializeNotificationWithUtf8JsonSerializer |  3,107.9 ns |   175.8866 ns |   9.9379 ns | 0.2098 |     888 B |
|                           SerializePostWithJilSerializer |  4,625.4 ns |   262.5569 ns |  14.8349 ns | 2.0142 |    8480 B |
|                      SerializePostWithJsonSpanSerializer |  5,058.6 ns |    10.9890 ns |   0.6209 ns | 0.9384 |    3952 B |
|                      SerializePostWithUtf8JsonSerializer |  5,298.4 ns |    27.9670 ns |   1.5802 ns | 0.4044 |    1704 B |
|                      SerializePrivilegeWithJilSerializer |    335.8 ns |     0.3580 ns |   0.0202 ns | 0.1960 |     824 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    200.2 ns |     0.9707 ns |   0.0548 ns | 0.0436 |     184 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    190.1 ns |     0.8043 ns |   0.0454 ns | 0.0265 |     112 B |
|                       SerializeQuestionWithJilSerializer | 19,522.1 ns |   588.6665 ns |  33.2607 ns | 7.3242 |   30816 B |
|                  SerializeQuestionWithJsonSpanSerializer | 20,549.0 ns |   269.3787 ns |  15.2204 ns | 3.6011 |   15136 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 24,258.2 ns |   278.8715 ns |  15.7568 ns | 1.4954 |    6352 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,806.0 ns |    13.9465 ns |   0.7880 ns | 1.0300 |    4328 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  2,128.9 ns |    23.8648 ns |   1.3484 ns | 0.4120 |    1744 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  2,093.1 ns |    32.2161 ns |   1.8203 ns | 0.1831 |     776 B |
|                     SerializeReputationWithJilSerializer |    828.5 ns |     2.6317 ns |   0.1487 ns | 0.3290 |    1384 B |
|                SerializeReputationWithJsonSpanSerializer |    784.3 ns |     4.1865 ns |   0.2365 ns | 0.1059 |     448 B |
|                SerializeReputationWithUtf8JsonSerializer |    871.9 ns |     5.9925 ns |   0.3386 ns | 0.0544 |     232 B |
|              SerializeReputationHistoryWithJilSerializer |    654.6 ns |     2.2636 ns |   0.1279 ns | 0.3157 |    1328 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    587.6 ns |     4.4540 ns |   0.2517 ns | 0.0868 |     368 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    742.4 ns |     2.6684 ns |   0.1508 ns | 0.0448 |     192 B |
|                       SerializeRevisionWithJilSerializer |  1,841.8 ns |    33.7161 ns |   1.9050 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,892.4 ns |    16.1707 ns |   0.9137 ns | 0.3624 |    1528 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  2,191.7 ns |    36.6167 ns |   2.0689 ns | 0.1526 |     656 B |
|                  SerializeSearchExcerptWithJilSerializer |  3,347.3 ns |    20.6151 ns |   1.1648 ns | 1.1444 |    4808 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  3,556.3 ns |    37.5640 ns |   2.1224 ns | 0.5608 |    2360 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  4,004.5 ns |    39.9432 ns |   2.2569 ns | 0.2441 |    1040 B |
|                    SerializeShallowUserWithJilSerializer |    629.2 ns |   612.1439 ns |  34.5873 ns | 0.3519 |    1480 B |
|               SerializeShallowUserWithJsonSpanSerializer |    686.4 ns |     2.8355 ns |   0.1602 ns | 0.1364 |     576 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    597.6 ns |     2.0394 ns |   0.1152 ns | 0.0620 |     264 B |
|                  SerializeSuggestedEditWithJilSerializer |  2,137.4 ns |    78.1144 ns |   4.4136 ns | 0.8965 |    3768 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  2,114.0 ns |    12.9116 ns |   0.7295 ns | 0.3204 |    1352 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  2,343.4 ns |     4.8349 ns |   0.2732 ns | 0.1373 |     592 B |
|                            SerializeTagWithJilSerializer |    815.6 ns |     4.2994 ns |   0.2429 ns | 0.3328 |    1400 B |
|                       SerializeTagWithJsonSpanSerializer |    736.4 ns |    10.4794 ns |   0.5921 ns | 0.1202 |     504 B |
|                       SerializeTagWithUtf8JsonSerializer |    822.1 ns |     9.4303 ns |   0.5328 ns | 0.0544 |     232 B |
|                       SerializeTagScoreWithJilSerializer |    742.5 ns |     9.2783 ns |   0.5242 ns | 0.5140 |    2160 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    887.2 ns |    15.6560 ns |   0.8846 ns | 0.1764 |     744 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    753.1 ns |     5.7392 ns |   0.3243 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |  1,028.8 ns |     3.9306 ns |   0.2221 ns | 0.3128 |    1320 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    791.2 ns |     1.3045 ns |   0.0737 ns | 0.0906 |     384 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |  1,056.8 ns |     4.8407 ns |   0.2735 ns | 0.0477 |     208 B |
|                        SerializeTagWikiWithJilSerializer |  2,146.9 ns |     7.8030 ns |   0.4409 ns | 1.0109 |    4248 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  2,190.5 ns |    26.7665 ns |   1.5124 ns | 0.4005 |    1696 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  2,240.6 ns |    97.7190 ns |   5.5213 ns | 0.1717 |     728 B |
|                         SerializeTopTagWithJilSerializer |    413.6 ns |     8.4673 ns |   0.4784 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    387.9 ns |     2.5366 ns |   0.1433 ns | 0.0777 |     328 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    370.6 ns |     1.2473 ns |   0.0705 ns | 0.0415 |     176 B |
|                           SerializeUserWithJilSerializer |  2,774.4 ns |    30.3257 ns |   1.7135 ns | 1.0529 |    4424 B |
|                      SerializeUserWithJsonSpanSerializer |  2,683.8 ns |     2.7643 ns |   0.1562 ns | 0.4501 |    1896 B |
|                      SerializeUserWithUtf8JsonSerializer |  3,204.2 ns |    25.4482 ns |   1.4379 ns | 0.2174 |     928 B |
|                   SerializeUserTimelineWithJilSerializer |    964.4 ns |     4.7119 ns |   0.2662 ns | 0.5035 |    2120 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    906.0 ns |     2.6595 ns |   0.1503 ns | 0.1364 |     576 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |  1,042.4 ns |     6.0194 ns |   0.3401 ns | 0.0706 |     304 B |
|                SerializeWritePermissionWithJilSerializer |    415.3 ns |     4.9099 ns |   0.2774 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    385.1 ns |     2.7180 ns |   0.1536 ns | 0.0854 |     360 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    355.6 ns |     2.9148 ns |   0.1647 ns | 0.0453 |     192 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    253.6 ns |     3.0017 ns |   0.1696 ns | 0.1407 |     592 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    203.8 ns |     0.6592 ns |   0.0372 ns | 0.0360 |     152 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    203.5 ns |     2.1472 ns |   0.1213 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,733.6 ns |   140.2434 ns |   7.9240 ns | 0.9651 |    4056 B |
|                      SerializeSiteWithJsonSpanSerializer |  2,284.6 ns |    35.1329 ns |   1.9851 ns | 0.4044 |    1704 B |
