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

BenchmarkDotNet=v0.10.14, OS=Windows 10.0.17133.1 (1803/SpringCreatorsUpdate/Redstone4)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
Frequency=3906250 Hz, Resolution=256.0000 ns, Timer=TSC
.NET Core SDK=2.1.300-preview2-008530
  [Host]   : .NET Core 2.1.0-preview3-26416-01 (CoreCLR 4.6.26413.01, CoreFX 4.6.26413.02), 64bit RyuJIT
  ShortRun : .NET Core 2.1.0-preview3-26416-01 (CoreCLR 4.6.26413.01, CoreFX 4.6.26413.02), 64bit RyuJIT

Job=ShortRun  LaunchCount=1  TargetCount=3  
WarmupCount=3  

```
|                                                   Method |        Mean |         Error |      StdDev |  Gen 0 | Allocated |
|--------------------------------------------------------- |------------:|--------------:|------------:|-------:|----------:|
|              DeserializeTagSynonymWithJsonSpanSerializer |    523.6 ns |     6.1599 ns |   0.3480 ns | 0.0401 |     168 B |
|              DeserializeTagSynonymWithUtf8JsonSerializer |    785.8 ns |    43.4345 ns |   2.4541 ns | 0.0401 |     168 B |
|                      DeserializeTagWikiWithJilSerializer |  2,935.5 ns |   639.4539 ns |  36.1303 ns | 0.2480 |    1048 B |
|                 DeserializeTagWikiWithJsonSpanSerializer |  2,390.6 ns |   444.7968 ns |  25.1318 ns | 0.1984 |     864 B |
|                 DeserializeTagWikiWithUtf8JsonSerializer |  2,961.4 ns |    80.8167 ns |   4.5663 ns | 0.1793 |     760 B |
|                       DeserializeTopTagWithJilSerializer |    586.4 ns |    11.6635 ns |   0.6590 ns | 0.0486 |     208 B |
|                  DeserializeTopTagWithJsonSpanSerializer |    417.3 ns |     6.3714 ns |   0.3600 ns | 0.0262 |     112 B |
|                  DeserializeTopTagWithUtf8JsonSerializer |    520.1 ns |    26.4469 ns |   1.4943 ns | 0.0257 |     112 B |
|                         DeserializeUserWithJilSerializer |  3,794.7 ns |   138.0593 ns |   7.8006 ns | 0.1602 |     696 B |
|                    DeserializeUserWithJsonSpanSerializer |  2,924.3 ns |    43.1459 ns |   2.4378 ns | 0.1526 |     656 B |
|                    DeserializeUserWithUtf8JsonSerializer |  3,184.4 ns |   257.2497 ns |  14.5351 ns | 0.1411 |     600 B |
|                 DeserializeUserTimelineWithJilSerializer |  1,129.5 ns |    44.4736 ns |   2.5128 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithJsonSpanSerializer |  1,071.5 ns |    58.5886 ns |   3.3104 ns | 0.0820 |     352 B |
|            DeserializeUserTimelineWithUtf8JsonSerializer |  1,191.2 ns |     6.7671 ns |   0.3824 ns | 0.0591 |     256 B |
|              DeserializeWritePermissionWithJilSerializer |    654.4 ns |     1.3413 ns |   0.0758 ns | 0.0515 |     216 B |
|         DeserializeWritePermissionWithJsonSpanSerializer |    495.9 ns |     1.3290 ns |   0.0751 ns | 0.0277 |     120 B |
|         DeserializeWritePermissionWithUtf8JsonSerializer |    617.1 ns |     6.4195 ns |   0.3627 ns | 0.0277 |     120 B |
|          DeserializeMobileBannerAdImageWithJilSerializer |    302.7 ns |     3.2343 ns |   0.1827 ns | 0.0434 |     184 B |
|     DeserializeMobileBannerAdImageWithJsonSpanSerializer |    187.3 ns |     1.2920 ns |   0.0730 ns | 0.0207 |      88 B |
|     DeserializeMobileBannerAdImageWithUtf8JsonSerializer |    304.6 ns |     9.2580 ns |   0.5231 ns | 0.0205 |      88 B |
|                         DeserializeSiteWithJilSerializer |  3,225.3 ns |   188.9330 ns |  10.6751 ns | 0.3586 |    1520 B |
|                    DeserializeSiteWithJsonSpanSerializer |  2,511.2 ns |     2.5772 ns |   0.1456 ns | 0.3586 |    1512 B |
|                    DeserializeSiteWithUtf8JsonSerializer |  3,230.8 ns |   132.6380 ns |   7.4943 ns | 0.3357 |    1424 B |
|                  DeserializeRelatedSiteWithJilSerializer |    393.5 ns |     3.6333 ns |   0.2053 ns | 0.0682 |     288 B |
|             DeserializeRelatedSiteWithJsonSpanSerializer |    291.3 ns |     2.9720 ns |   0.1679 ns | 0.0548 |     232 B |
|             DeserializeRelatedSiteWithUtf8JsonSerializer |    493.6 ns |    27.5875 ns |   1.5587 ns | 0.0448 |     192 B |
|                DeserializeClosedDetailsWithJilSerializer |  2,284.4 ns |    31.5959 ns |   1.7852 ns | 0.1869 |     800 B |
|           DeserializeClosedDetailsWithJsonSpanSerializer |  1,576.2 ns |   120.3024 ns |   6.7973 ns | 0.1774 |     752 B |
|           DeserializeClosedDetailsWithUtf8JsonSerializer |  1,969.7 ns |    85.5146 ns |   4.8317 ns | 0.1640 |     704 B |
|                       DeserializeNoticeWithJilSerializer |    492.1 ns |     3.0258 ns |   0.1710 ns | 0.0448 |     192 B |
|                  DeserializeNoticeWithJsonSpanSerializer |    288.9 ns |    12.2317 ns |   0.6911 ns | 0.0224 |      96 B |
|                  DeserializeNoticeWithUtf8JsonSerializer |    412.4 ns |     0.7695 ns |   0.0435 ns | 0.0224 |      96 B |
|                DeserializeMigrationInfoWithJilSerializer |  3,909.4 ns |   159.1199 ns |   8.9906 ns | 0.3662 |    1568 B |
|           DeserializeMigrationInfoWithJsonSpanSerializer |  2,824.8 ns |    52.9752 ns |   2.9932 ns | 0.3662 |    1552 B |
|           DeserializeMigrationInfoWithUtf8JsonSerializer |  3,784.0 ns |   120.5713 ns |   6.8125 ns | 0.3471 |    1472 B |
|                   DeserializeBadgeCountWithJilSerializer |    285.0 ns |    31.0284 ns |   1.7532 ns | 0.0319 |     136 B |
|              DeserializeBadgeCountWithJsonSpanSerializer |    176.2 ns |     5.4889 ns |   0.3101 ns | 0.0093 |      40 B |
|              DeserializeBadgeCountWithUtf8JsonSerializer |    252.0 ns |    47.2163 ns |   2.6678 ns | 0.0091 |      40 B |
|                      DeserializeStylingWithJilSerializer |    354.2 ns |   142.3161 ns |   8.0411 ns | 0.0663 |     280 B |
|                 DeserializeStylingWithJsonSpanSerializer |    239.7 ns |    32.8779 ns |   1.8577 ns | 0.0434 |     184 B |
|                 DeserializeStylingWithUtf8JsonSerializer |    374.3 ns |    56.0604 ns |   3.1675 ns | 0.0434 |     184 B |
|             DeserializeOriginalQuestionWithJilSerializer |    417.6 ns |    15.3558 ns |   0.8676 ns | 0.0453 |     192 B |
|        DeserializeOriginalQuestionWithJsonSpanSerializer |    260.5 ns |    13.1499 ns |   0.7430 ns | 0.0224 |      96 B |
|        DeserializeOriginalQuestionWithUtf8JsonSerializer |    402.0 ns |     1.5736 ns |   0.0889 ns | 0.0224 |      96 B |
|                      SerializeSiteWithUtf8JsonSerializer |  1,743.8 ns |    90.8282 ns |   5.1320 ns | 0.1659 |     704 B |
|                    SerializeRelatedSiteWithJilSerializer |    384.7 ns |     8.6089 ns |   0.4864 ns | 0.2017 |     848 B |
|               SerializeRelatedSiteWithJsonSpanSerializer |    237.7 ns |     3.1431 ns |   0.1776 ns | 0.0491 |     208 B |
|               SerializeRelatedSiteWithUtf8JsonSerializer |    246.2 ns |   106.3357 ns |   6.0082 ns | 0.0281 |     120 B |
|                  SerializeClosedDetailsWithJilSerializer |  1,132.7 ns |    38.7078 ns |   2.1871 ns | 0.5856 |    2464 B |
|             SerializeClosedDetailsWithJsonSpanSerializer |  1,250.7 ns |    26.0270 ns |   1.4706 ns | 0.2155 |     912 B |
|             SerializeClosedDetailsWithUtf8JsonSerializer |  1,116.3 ns |   157.8090 ns |   8.9165 ns | 0.1106 |     472 B |
|                         SerializeNoticeWithJilSerializer |    388.2 ns |     1.2453 ns |   0.0704 ns | 0.1998 |     840 B |
|                    SerializeNoticeWithJsonSpanSerializer |    258.5 ns |     1.9914 ns |   0.1125 ns | 0.0510 |     216 B |
|                    SerializeNoticeWithUtf8JsonSerializer |    356.8 ns |     2.5953 ns |   0.1466 ns | 0.0262 |     112 B |
|                  SerializeMigrationInfoWithJilSerializer |  2,234.9 ns |    93.1420 ns |   5.2627 ns | 0.9880 |    4160 B |
|             SerializeMigrationInfoWithJsonSpanSerializer |  1,915.1 ns |    27.8170 ns |   1.5717 ns | 0.3738 |    1576 B |
|             SerializeMigrationInfoWithUtf8JsonSerializer |  2,082.3 ns |    28.5839 ns |   1.6150 ns | 0.1869 |     792 B |
|                     SerializeBadgeCountWithJilSerializer |    214.1 ns |     2.3727 ns |   0.1341 ns | 0.1392 |     584 B |
|                SerializeBadgeCountWithJsonSpanSerializer |    192.8 ns |     1.4535 ns |   0.0821 ns | 0.0360 |     152 B |
|                SerializeBadgeCountWithUtf8JsonSerializer |    231.3 ns |     1.3143 ns |   0.0743 ns | 0.0207 |      88 B |
|                        SerializeStylingWithJilSerializer |    359.7 ns |     1.0164 ns |   0.0574 ns | 0.1807 |     760 B |
|                   SerializeStylingWithJsonSpanSerializer |    185.0 ns |     7.3146 ns |   0.4133 ns | 0.0513 |     216 B |
|                   SerializeStylingWithUtf8JsonSerializer |    189.9 ns |    36.3823 ns |   2.0557 ns | 0.0284 |     120 B |
|               SerializeOriginalQuestionWithJilSerializer |    327.2 ns |     1.1340 ns |   0.0641 ns | 0.2093 |     880 B |
|          SerializeOriginalQuestionWithJsonSpanSerializer |    250.2 ns |    25.1841 ns |   1.4229 ns | 0.0548 |     232 B |
|          SerializeOriginalQuestionWithUtf8JsonSerializer |    269.5 ns |     2.6280 ns |   0.1485 ns | 0.0300 |     128 B |
|                  DeserializeAccessTokenWithJilSerializer |    546.6 ns |     3.3460 ns |   0.1891 ns | 0.0811 |     344 B |
|             DeserializeAccessTokenWithJsonSpanSerializer |    396.9 ns |     3.5841 ns |   0.2025 ns | 0.0587 |     248 B |
|             DeserializeAccessTokenWithUtf8JsonSerializer |    679.3 ns |    19.9729 ns |   1.1285 ns | 0.0582 |     248 B |
|                 DeserializeAccountMergeWithJilSerializer |    447.0 ns |     4.9315 ns |   0.2786 ns | 0.0343 |     144 B |
|            DeserializeAccountMergeWithJsonSpanSerializer |    317.2 ns |    16.7221 ns |   0.9448 ns | 0.0110 |      48 B |
|            DeserializeAccountMergeWithUtf8JsonSerializer |    390.4 ns |    12.0932 ns |   0.6833 ns | 0.0110 |      48 B |
|                       DeserializeAnswerWithJilSerializer |  8,956.6 ns |   185.8940 ns |  10.5034 ns | 0.5951 |    2528 B |
|                  DeserializeAnswerWithJsonSpanSerializer |  7,039.5 ns |   138.8548 ns |   7.8456 ns | 0.5417 |    2296 B |
|                  DeserializeAnswerWithUtf8JsonSerializer |  8,236.0 ns |   220.1275 ns |  12.4376 ns | 0.4730 |    2048 B |
|                        DeserializeBadgeWithJilSerializer |  1,933.4 ns |   109.6344 ns |   6.1945 ns | 0.1392 |     584 B |
|                   DeserializeBadgeWithJsonSpanSerializer |  1,603.8 ns |   229.3927 ns |  12.9611 ns | 0.1469 |     624 B |
|                   DeserializeBadgeWithUtf8JsonSerializer |  1,792.7 ns |   149.6050 ns |   8.4530 ns | 0.1144 |     488 B |
|                      DeserializeCommentWithJilSerializer |  3,165.5 ns |   164.8266 ns |   9.3130 ns | 0.2556 |    1080 B |
|                 DeserializeCommentWithJsonSpanSerializer |  3,051.4 ns |    65.8201 ns |   3.7190 ns | 0.2213 |     944 B |
|                 DeserializeCommentWithUtf8JsonSerializer |  3,122.8 ns |   233.8181 ns |  13.2112 ns | 0.1869 |     792 B |
|                        DeserializeErrorWithJilSerializer |    318.6 ns |     2.3125 ns |   0.1307 ns | 0.0548 |     232 B |
|                   DeserializeErrorWithJsonSpanSerializer |    208.2 ns |     1.7448 ns |   0.0986 ns | 0.0322 |     136 B |
|                   DeserializeErrorWithUtf8JsonSerializer |    340.0 ns |     1.0438 ns |   0.0590 ns | 0.0319 |     136 B |
|                        DeserializeEventWithJilSerializer |    571.1 ns |     8.7573 ns |   0.4948 ns | 0.0601 |     256 B |
|                   DeserializeEventWithJsonSpanSerializer |    453.4 ns |     3.2531 ns |   0.1838 ns | 0.0510 |     216 B |
|                   DeserializeEventWithUtf8JsonSerializer |    657.6 ns |     1.9244 ns |   0.1087 ns | 0.0372 |     160 B |
|                   DeserializeMobileFeedWithJilSerializer | 24,593.9 ns | 1,021.7822 ns |  57.7326 ns | 1.3428 |    5712 B |
|              DeserializeMobileFeedWithJsonSpanSerializer | 13,043.7 ns |   650.3491 ns |  36.7459 ns | 1.2512 |    5272 B |
|              DeserializeMobileFeedWithUtf8JsonSerializer | 13,648.0 ns |   477.0956 ns |  26.9568 ns | 1.2207 |    5136 B |
|               DeserializeMobileQuestionWithJilSerializer |  1,272.8 ns |    23.8997 ns |   1.3504 ns | 0.1087 |     464 B |
|          DeserializeMobileQuestionWithJsonSpanSerializer |  1,005.5 ns |    57.9769 ns |   3.2758 ns | 0.0858 |     368 B |
|          DeserializeMobileQuestionWithUtf8JsonSerializer |  1,081.3 ns |     8.9357 ns |   0.5049 ns | 0.0858 |     368 B |
|              DeserializeMobileRepChangeWithJilSerializer |    584.9 ns |     7.8512 ns |   0.4436 ns | 0.0734 |     312 B |
|         DeserializeMobileRepChangeWithJsonSpanSerializer |    427.6 ns |     2.0797 ns |   0.1175 ns | 0.0515 |     216 B |
|         DeserializeMobileRepChangeWithUtf8JsonSerializer |    564.4 ns |    17.6647 ns |   0.9981 ns | 0.0515 |     216 B |
|              DeserializeMobileInboxItemWithJilSerializer |  1,085.0 ns |    30.7475 ns |   1.7373 ns | 0.1087 |     456 B |
|         DeserializeMobileInboxItemWithJsonSpanSerializer |    950.8 ns |    30.7531 ns |   1.7376 ns | 0.0839 |     360 B |
|         DeserializeMobileInboxItemWithUtf8JsonSerializer |  1,012.0 ns |     7.3332 ns |   0.4143 ns | 0.0839 |     360 B |
|             DeserializeMobileBadgeAwardWithJilSerializer |    927.7 ns |    39.9423 ns |   2.2568 ns | 0.0925 |     392 B |
|        DeserializeMobileBadgeAwardWithJsonSpanSerializer |    898.9 ns |    12.0854 ns |   0.6828 ns | 0.0906 |     384 B |
|        DeserializeMobileBadgeAwardWithUtf8JsonSerializer |    997.1 ns |     9.7512 ns |   0.5510 ns | 0.0687 |     296 B |
|              DeserializeMobilePrivilegeWithJilSerializer |    864.3 ns |     2.1071 ns |   0.1191 ns | 0.0887 |     376 B |
|         DeserializeMobilePrivilegeWithJsonSpanSerializer |    720.5 ns |    46.1482 ns |   2.6075 ns | 0.0658 |     280 B |
|         DeserializeMobilePrivilegeWithUtf8JsonSerializer |    837.9 ns |   181.3856 ns |  10.2486 ns | 0.0658 |     280 B |
|      DeserializeMobileCommunityBulletinWithJilSerializer |  1,537.6 ns |    23.1618 ns |   1.3087 ns | 0.1392 |     584 B |
| DeserializeMobileCommunityBulletinWithJsonSpanSerializer |  1,350.0 ns |    67.2478 ns |   3.7996 ns | 0.1316 |     560 B |
| DeserializeMobileCommunityBulletinWithUtf8JsonSerializer |  1,400.0 ns |    41.2717 ns |   2.3319 ns | 0.1144 |     488 B |
|       DeserializeMobileAssociationBonusWithJilSerializer |    429.1 ns |     2.5820 ns |   0.1459 ns | 0.0472 |     200 B |
|  DeserializeMobileAssociationBonusWithJsonSpanSerializer |    257.8 ns |     6.5836 ns |   0.3720 ns | 0.0243 |     104 B |
|  DeserializeMobileAssociationBonusWithUtf8JsonSerializer |    373.0 ns |    12.6954 ns |   0.7173 ns | 0.0243 |     104 B |
|           DeserializeMobileCareersJobAdWithJilSerializer |    687.3 ns |    13.6123 ns |   0.7691 ns | 0.0868 |     368 B |
|      DeserializeMobileCareersJobAdWithJsonSpanSerializer |    509.0 ns |     4.3561 ns |   0.2461 ns | 0.0639 |     272 B |
|      DeserializeMobileCareersJobAdWithUtf8JsonSerializer |    681.7 ns |    17.4405 ns |   0.9854 ns | 0.0639 |     272 B |
|               DeserializeMobileBannerAdWithJilSerializer |    752.7 ns |     7.9632 ns |   0.4499 ns | 0.0906 |     384 B |
|          DeserializeMobileBannerAdWithJsonSpanSerializer |    449.8 ns |     2.6639 ns |   0.1505 ns | 0.0682 |     288 B |
|          DeserializeMobileBannerAdWithUtf8JsonSerializer |    684.3 ns |    80.8623 ns |   4.5689 ns | 0.0677 |     288 B |
|           DeserializeMobileUpdateNoticeWithJilSerializer |    315.3 ns |     3.8211 ns |   0.2159 ns | 0.0548 |     232 B |
|      DeserializeMobileUpdateNoticeWithJsonSpanSerializer |    212.5 ns |     2.1834 ns |   0.1234 ns | 0.0322 |     136 B |
|      DeserializeMobileUpdateNoticeWithUtf8JsonSerializer |    359.5 ns |    25.0762 ns |   1.4169 ns | 0.0319 |     136 B |
|                   DeserializeFlagOptionWithJilSerializer |  1,642.7 ns |    23.6006 ns |   1.3335 ns | 0.1545 |     656 B |
|              DeserializeFlagOptionWithJsonSpanSerializer |  1,406.2 ns |    53.7000 ns |   3.0341 ns | 0.1087 |     464 B |
|              DeserializeFlagOptionWithUtf8JsonSerializer |  1,591.3 ns |    23.9316 ns |   1.3522 ns | 0.1087 |     464 B |
|                    DeserializeInboxItemWithJilSerializer |  4,555.6 ns |   261.4982 ns |  14.7751 ns | 0.4196 |    1768 B |
|               DeserializeInboxItemWithJsonSpanSerializer |  3,808.7 ns |    23.4604 ns |   1.3256 ns | 0.4311 |    1816 B |
|               DeserializeInboxItemWithUtf8JsonSerializer |  4,316.9 ns |    17.4795 ns |   0.9876 ns | 0.3967 |    1672 B |
|                         DeserializeInfoWithJilSerializer |  5,697.1 ns |   242.3500 ns |  13.6932 ns | 0.4120 |    1744 B |
|                    DeserializeInfoWithJsonSpanSerializer |  4,459.8 ns |    92.6649 ns |   5.2357 ns | 0.4120 |    1736 B |
|                    DeserializeInfoWithUtf8JsonSerializer |  5,485.6 ns |   256.9857 ns |  14.5202 ns | 0.4272 |    1816 B |
|                  DeserializeNetworkUserWithJilSerializer |  1,569.5 ns |    89.4815 ns |   5.0559 ns | 0.0820 |     352 B |
|             DeserializeNetworkUserWithJsonSpanSerializer |  1,410.3 ns |    27.0300 ns |   1.5272 ns | 0.0725 |     304 B |
|             DeserializeNetworkUserWithUtf8JsonSerializer |  1,427.1 ns |    19.6241 ns |   1.1088 ns | 0.0591 |     256 B |
|                 DeserializeNotificationWithJilSerializer |  4,404.1 ns |   490.2419 ns |  27.6996 ns | 0.3891 |    1640 B |
|            DeserializeNotificationWithJsonSpanSerializer |  3,162.4 ns |    83.4473 ns |   4.7149 ns | 0.3929 |    1664 B |
|            DeserializeNotificationWithUtf8JsonSerializer |  3,991.1 ns |    80.8517 ns |   4.5683 ns | 0.3662 |    1544 B |
|                         DeserializePostWithJilSerializer |  8,236.7 ns |   295.2475 ns |  16.6820 ns | 0.5341 |    2272 B |
|                    DeserializePostWithJsonSpanSerializer |  6,405.1 ns |   259.2654 ns |  14.6490 ns | 0.4959 |    2088 B |
|                    DeserializePostWithUtf8JsonSerializer |  7,133.5 ns |   197.4148 ns |  11.1543 ns | 0.4196 |    1792 B |
|                    DeserializePrivilegeWithJilSerializer |    329.3 ns |     1.6400 ns |   0.0927 ns | 0.0548 |     232 B |
|               DeserializePrivilegeWithJsonSpanSerializer |    212.1 ns |     7.5743 ns |   0.4280 ns | 0.0322 |     136 B |
|               DeserializePrivilegeWithUtf8JsonSerializer |    348.1 ns |     0.5665 ns |   0.0320 ns | 0.0319 |     136 B |
|                     DeserializeQuestionWithJilSerializer | 42,734.1 ns |   124.6347 ns |   7.0421 ns | 2.2583 |    9480 B |
|                DeserializeQuestionWithJsonSpanSerializer | 27,137.1 ns | 1,408.0449 ns |  79.5571 ns | 2.1057 |    8856 B |
|                DeserializeQuestionWithUtf8JsonSerializer | 29,449.1 ns | 2,859.2032 ns | 161.5503 ns | 1.9226 |    8136 B |
|             DeserializeQuestionTimelineWithJilSerializer |  3,179.3 ns |   171.7536 ns |   9.7044 ns | 0.2289 |     968 B |
|        DeserializeQuestionTimelineWithJsonSpanSerializer |  2,621.3 ns |    12.4732 ns |   0.7048 ns | 0.1984 |     840 B |
|        DeserializeQuestionTimelineWithUtf8JsonSerializer |  3,044.2 ns |   255.7177 ns |  14.4485 ns | 0.1602 |     680 B |
|                   DeserializeReputationWithJilSerializer |    824.2 ns |     1.0719 ns |   0.0606 ns | 0.0658 |     280 B |
|              DeserializeReputationWithJsonSpanSerializer |    893.4 ns |     5.3326 ns |   0.3013 ns | 0.0677 |     288 B |
|              DeserializeReputationWithUtf8JsonSerializer |    991.0 ns |    34.7254 ns |   1.9621 ns | 0.0420 |     184 B |
|            DeserializeReputationHistoryWithJilSerializer |    618.1 ns |     0.2254 ns |   0.0127 ns | 0.0372 |     160 B |
|       DeserializeReputationHistoryWithJsonSpanSerializer |    483.2 ns |    24.9231 ns |   1.4082 ns | 0.0296 |     128 B |
|       DeserializeReputationHistoryWithUtf8JsonSerializer |    614.2 ns |    41.4845 ns |   2.3440 ns | 0.0143 |      64 B |
|                     DeserializeRevisionWithJilSerializer |  2,971.9 ns |   170.4639 ns |   9.6315 ns | 0.2556 |    1088 B |
|                DeserializeRevisionWithJsonSpanSerializer |  2,481.6 ns |   159.5684 ns |   9.0159 ns | 0.2708 |    1144 B |
|                DeserializeRevisionWithUtf8JsonSerializer |  2,941.5 ns |   229.4837 ns |  12.9663 ns | 0.2327 |     992 B |
|                DeserializeSearchExcerptWithJilSerializer |  4,519.5 ns |   275.6296 ns |  15.5736 ns | 0.3052 |    1304 B |
|           DeserializeSearchExcerptWithJsonSpanSerializer |  3,927.2 ns |   128.6944 ns |   7.2715 ns | 0.2747 |    1160 B |
|           DeserializeSearchExcerptWithUtf8JsonSerializer |  4,247.8 ns |    49.3699 ns |   2.7895 ns | 0.2365 |    1016 B |
|                  DeserializeShallowUserWithJilSerializer |  1,030.2 ns |    15.3944 ns |   0.8698 ns | 0.0839 |     360 B |
|             DeserializeShallowUserWithJsonSpanSerializer |    886.6 ns |    19.2152 ns |   1.0857 ns | 0.0753 |     320 B |
|             DeserializeShallowUserWithUtf8JsonSerializer |  1,018.9 ns |     2.0274 ns |   0.1145 ns | 0.0610 |     264 B |
|                DeserializeSuggestedEditWithJilSerializer |  2,631.2 ns |    20.2165 ns |   1.1423 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithJsonSpanSerializer |  2,234.2 ns |    53.1932 ns |   3.0055 ns | 0.1831 |     776 B |
|           DeserializeSuggestedEditWithUtf8JsonSerializer |  2,731.5 ns |    65.9864 ns |   3.7284 ns | 0.1602 |     680 B |
|                          DeserializeTagWithJilSerializer |    902.6 ns |    16.7067 ns |   0.9440 ns | 0.0887 |     376 B |
|                     DeserializeTagWithJsonSpanSerializer |    709.8 ns |    20.3540 ns |   1.1500 ns | 0.0658 |     280 B |
|                     DeserializeTagWithUtf8JsonSerializer |    874.7 ns |    73.4438 ns |   4.1497 ns | 0.0658 |     280 B |
|                     DeserializeTagScoreWithJilSerializer |  1,335.7 ns |     9.2047 ns |   0.5201 ns | 0.0935 |     400 B |
|                DeserializeTagScoreWithJsonSpanSerializer |    974.5 ns |    38.9314 ns |   2.1997 ns | 0.0839 |     360 B |
|                DeserializeTagScoreWithUtf8JsonSerializer |  1,224.1 ns |    65.9693 ns |   3.7274 ns | 0.0725 |     304 B |
|                   DeserializeTagSynonymWithJilSerializer |    703.8 ns |     9.4956 ns |   0.5365 ns | 0.0620 |     264 B |
|                    SerializeAccessTokenWithJilSerializer |    494.0 ns |     1.6029 ns |   0.0906 ns | 0.2127 |     896 B |
|               SerializeAccessTokenWithJsonSpanSerializer |    436.0 ns |   268.0442 ns |  15.1450 ns | 0.0644 |     272 B |
|               SerializeAccessTokenWithUtf8JsonSerializer |    458.0 ns |     2.4034 ns |   0.1358 ns | 0.0358 |     152 B |
|                   SerializeAccountMergeWithJilSerializer |    363.9 ns |     0.7072 ns |   0.0400 ns | 0.2036 |     856 B |
|              SerializeAccountMergeWithJsonSpanSerializer |    266.0 ns |     1.6594 ns |   0.0938 ns | 0.0548 |     232 B |
|              SerializeAccountMergeWithUtf8JsonSerializer |    357.5 ns |     1.6522 ns |   0.0934 ns | 0.0281 |     120 B |
|                         SerializeAnswerWithJilSerializer |  4,245.3 ns |    76.3637 ns |   4.3147 ns | 2.0905 |    8800 B |
|                    SerializeAnswerWithJsonSpanSerializer |  4,702.0 ns |    70.3462 ns |   3.9747 ns | 0.8850 |    3744 B |
|                    SerializeAnswerWithUtf8JsonSerializer |  5,119.9 ns |   142.5094 ns |   8.0520 ns | 0.4349 |    1856 B |
|                          SerializeBadgeWithJilSerializer |    987.6 ns |     3.0912 ns |   0.1747 ns | 0.5646 |    2376 B |
|                     SerializeBadgeWithJsonSpanSerializer |  1,156.8 ns |     9.8985 ns |   0.5593 ns | 0.1984 |     840 B |
|                     SerializeBadgeWithUtf8JsonSerializer |  1,017.1 ns |    21.5515 ns |   1.2177 ns | 0.0992 |     424 B |
|                        SerializeCommentWithJilSerializer |  1,718.8 ns |    16.2980 ns |   0.9209 ns | 1.0338 |    4344 B |
|                   SerializeCommentWithJsonSpanSerializer |  1,950.8 ns |     6.3223 ns |   0.3572 ns | 0.3548 |    1504 B |
|                   SerializeCommentWithUtf8JsonSerializer |  1,877.0 ns |    17.8008 ns |   1.0058 ns | 0.1831 |     776 B |
|                          SerializeErrorWithJilSerializer |    331.4 ns |     1.9708 ns |   0.1114 ns | 0.1941 |     816 B |
|                     SerializeErrorWithJsonSpanSerializer |    191.9 ns |     0.0812 ns |   0.0046 ns | 0.0417 |     176 B |
|                     SerializeErrorWithUtf8JsonSerializer |    198.2 ns |     0.2210 ns |   0.0125 ns | 0.0226 |      96 B |
|                          SerializeEventWithJilSerializer |    525.8 ns |     1.6710 ns |   0.0944 ns | 0.2985 |    1256 B |
|                     SerializeEventWithJsonSpanSerializer |    374.1 ns |     0.4194 ns |   0.0237 ns | 0.0720 |     304 B |
|                     SerializeEventWithUtf8JsonSerializer |    476.5 ns |     4.1745 ns |   0.2359 ns | 0.0372 |     160 B |
|                     SerializeMobileFeedWithJilSerializer |  7,579.4 ns |    53.6087 ns |   3.0290 ns | 3.7766 |   15856 B |
|                SerializeMobileFeedWithJsonSpanSerializer |  7,462.2 ns |   210.3987 ns |  11.8879 ns | 1.5259 |    6424 B |
|                SerializeMobileFeedWithUtf8JsonSerializer |  7,394.2 ns |   242.7777 ns |  13.7174 ns | 0.7629 |    3232 B |
|                 SerializeMobileQuestionWithJilSerializer |    791.3 ns |    70.5906 ns |   3.9885 ns | 0.5178 |    2176 B |
|            SerializeMobileQuestionWithJsonSpanSerializer |    743.8 ns |     5.0946 ns |   0.2879 ns | 0.1478 |     624 B |
|            SerializeMobileQuestionWithUtf8JsonSerializer |    675.9 ns |     0.4259 ns |   0.0241 ns | 0.0772 |     328 B |
|                SerializeMobileRepChangeWithJilSerializer |    506.0 ns |     2.5615 ns |   0.1447 ns | 0.3023 |    1272 B |
|           SerializeMobileRepChangeWithJsonSpanSerializer |    353.3 ns |     1.6676 ns |   0.0942 ns | 0.0701 |     296 B |
|           SerializeMobileRepChangeWithUtf8JsonSerializer |    361.9 ns |     0.7460 ns |   0.0422 ns | 0.0377 |     160 B |
|                SerializeMobileInboxItemWithJilSerializer |    789.7 ns |     4.0703 ns |   0.2300 ns | 0.5007 |    2104 B |
|           SerializeMobileInboxItemWithJsonSpanSerializer |    639.0 ns |     1.1836 ns |   0.0669 ns | 0.1307 |     552 B |
|           SerializeMobileInboxItemWithUtf8JsonSerializer |    631.3 ns |     4.3135 ns |   0.2437 ns | 0.0677 |     288 B |
|               SerializeMobileBadgeAwardWithJilSerializer |    631.8 ns |     3.4214 ns |   0.1933 ns | 0.3443 |    1448 B |
|          SerializeMobileBadgeAwardWithJsonSpanSerializer |    587.9 ns |     3.9453 ns |   0.2229 ns | 0.1154 |     488 B |
|          SerializeMobileBadgeAwardWithUtf8JsonSerializer |    577.2 ns |     5.3081 ns |   0.2999 ns | 0.0601 |     256 B |
|                SerializeMobilePrivilegeWithJilSerializer |    624.6 ns |     6.3489 ns |   0.3587 ns | 0.3481 |    1464 B |
|           SerializeMobilePrivilegeWithJsonSpanSerializer |    485.7 ns |     3.1875 ns |   0.1801 ns | 0.1173 |     496 B |
|           SerializeMobilePrivilegeWithUtf8JsonSerializer |    515.9 ns |     8.0637 ns |   0.4556 ns | 0.0620 |     264 B |
|        SerializeMobileCommunityBulletinWithJilSerializer |    895.4 ns |    17.8531 ns |   1.0087 ns | 0.5503 |    2312 B |
|   SerializeMobileCommunityBulletinWithJsonSpanSerializer |    862.9 ns |   103.8217 ns |   5.8661 ns | 0.1726 |     728 B |
|   SerializeMobileCommunityBulletinWithUtf8JsonSerializer |    810.2 ns |    42.8096 ns |   2.4188 ns | 0.0887 |     376 B |
|         SerializeMobileAssociationBonusWithJilSerializer |    346.0 ns |     0.6823 ns |   0.0385 ns | 0.2055 |     864 B |
|    SerializeMobileAssociationBonusWithJsonSpanSerializer |    259.6 ns |     1.5885 ns |   0.0898 ns | 0.0510 |     216 B |
|    SerializeMobileAssociationBonusWithUtf8JsonSerializer |    286.9 ns |     3.0886 ns |   0.1745 ns | 0.0281 |     120 B |
|             SerializeMobileCareersJobAdWithJilSerializer |    573.2 ns |     7.7926 ns |   0.4403 ns | 0.3138 |    1320 B |
|        SerializeMobileCareersJobAdWithJsonSpanSerializer |    437.5 ns |     0.4487 ns |   0.0254 ns | 0.0834 |     352 B |
|        SerializeMobileCareersJobAdWithUtf8JsonSerializer |    400.0 ns |    67.6709 ns |   3.8235 ns | 0.0453 |     192 B |
|                 SerializeMobileBannerAdWithJilSerializer |    535.5 ns |     4.5217 ns |   0.2555 ns | 0.3080 |    1296 B |
|            SerializeMobileBannerAdWithJsonSpanSerializer |    478.1 ns |    74.7645 ns |   4.2243 ns | 0.0758 |     320 B |
|            SerializeMobileBannerAdWithUtf8JsonSerializer |    440.1 ns |    14.6728 ns |   0.8290 ns | 0.0415 |     176 B |
|             SerializeMobileUpdateNoticeWithJilSerializer |    319.9 ns |     0.5149 ns |   0.0291 ns | 0.1750 |     736 B |
|        SerializeMobileUpdateNoticeWithJsonSpanSerializer |    176.7 ns |     0.4435 ns |   0.0251 ns | 0.0455 |     192 B |
|        SerializeMobileUpdateNoticeWithUtf8JsonSerializer |    159.1 ns |     0.4296 ns |   0.0243 ns | 0.0265 |     112 B |
|                     SerializeFlagOptionWithJilSerializer |    864.6 ns |     8.8882 ns |   0.5022 ns | 0.5884 |    2472 B |
|                SerializeFlagOptionWithJsonSpanSerializer |    775.4 ns |   100.7868 ns |   5.6946 ns | 0.1860 |     784 B |
|                SerializeFlagOptionWithUtf8JsonSerializer |    742.4 ns |     3.5823 ns |   0.2024 ns | 0.1001 |     424 B |
|                      SerializeInboxItemWithJilSerializer |  2,530.2 ns |    62.4677 ns |   3.5295 ns | 1.0643 |    4480 B |
|                 SerializeInboxItemWithJsonSpanSerializer |  2,269.9 ns |    97.9230 ns |   5.5328 ns | 0.4425 |    1872 B |
|                 SerializeInboxItemWithUtf8JsonSerializer |  2,376.9 ns |    10.3755 ns |   0.5862 ns | 0.2213 |     936 B |
|                           SerializeInfoWithJilSerializer |  2,993.2 ns |   501.3926 ns |  28.3296 ns | 1.6861 |    7088 B |
|                      SerializeInfoWithJsonSpanSerializer |  2,718.0 ns |    64.9519 ns |   3.6699 ns | 0.5188 |    2192 B |
|                      SerializeInfoWithUtf8JsonSerializer |  3,072.7 ns |    28.6550 ns |   1.6191 ns | 0.3281 |    1384 B |
|                    SerializeNetworkUserWithJilSerializer |    917.5 ns |    13.1979 ns |   0.7457 ns | 0.5465 |    2296 B |
|               SerializeNetworkUserWithJsonSpanSerializer |    939.0 ns |     7.3173 ns |   0.4134 ns | 0.1841 |     776 B |
|               SerializeNetworkUserWithUtf8JsonSerializer |  1,126.2 ns |     2.5432 ns |   0.1437 ns | 0.0916 |     392 B |
|                   SerializeNotificationWithJilSerializer |  2,365.4 ns |   122.8426 ns |   6.9408 ns | 1.0338 |    4344 B |
|              SerializeNotificationWithJsonSpanSerializer |  2,141.7 ns |    56.7999 ns |   3.2093 ns | 0.4158 |    1752 B |
|              SerializeNotificationWithUtf8JsonSerializer |  2,249.6 ns |    72.1057 ns |   4.0741 ns | 0.2022 |     864 B |
|                           SerializePostWithJilSerializer |  3,775.6 ns |   101.9986 ns |   5.7631 ns | 2.0218 |    8488 B |
|                      SerializePostWithJsonSpanSerializer |  4,160.4 ns |    26.9436 ns |   1.5224 ns | 0.8011 |    3376 B |
|                      SerializePostWithUtf8JsonSerializer |  4,340.7 ns |   126.5903 ns |   7.1526 ns | 0.3967 |    1688 B |
|                      SerializePrivilegeWithJilSerializer |    335.6 ns |     2.1243 ns |   0.1200 ns | 0.1979 |     832 B |
|                 SerializePrivilegeWithJsonSpanSerializer |    196.2 ns |     0.0301 ns |   0.0017 ns | 0.0455 |     192 B |
|                 SerializePrivilegeWithUtf8JsonSerializer |    193.2 ns |     0.5473 ns |   0.0309 ns | 0.0265 |     112 B |
|                       SerializeQuestionWithJilSerializer | 14,656.1 ns |   172.7361 ns |   9.7599 ns | 7.3090 |   30680 B |
|                  SerializeQuestionWithJsonSpanSerializer | 15,554.4 ns |   503.6725 ns |  28.4584 ns | 2.9907 |   12536 B |
|                  SerializeQuestionWithUtf8JsonSerializer | 17,130.5 ns |   834.7544 ns |  47.1652 ns | 1.4648 |    6176 B |
|               SerializeQuestionTimelineWithJilSerializer |  1,627.6 ns |    27.9904 ns |   1.5815 ns | 1.0262 |    4312 B |
|          SerializeQuestionTimelineWithJsonSpanSerializer |  1,922.3 ns |    23.3950 ns |   1.3219 ns | 0.3548 |    1496 B |
|          SerializeQuestionTimelineWithUtf8JsonSerializer |  1,921.7 ns |    35.8150 ns |   2.0236 ns | 0.1793 |     768 B |
|                     SerializeReputationWithJilSerializer |    590.6 ns |     7.1579 ns |   0.4044 ns | 0.3290 |    1384 B |
|                SerializeReputationWithJsonSpanSerializer |    559.7 ns |    25.5385 ns |   1.4430 ns | 0.1001 |     424 B |
|                SerializeReputationWithUtf8JsonSerializer |    679.6 ns |     8.2001 ns |   0.4633 ns | 0.0544 |     232 B |
|              SerializeReputationHistoryWithJilSerializer |    430.0 ns |     5.5285 ns |   0.3124 ns | 0.3104 |    1304 B |
|         SerializeReputationHistoryWithJsonSpanSerializer |    380.4 ns |     6.1403 ns |   0.3469 ns | 0.0854 |     360 B |
|         SerializeReputationHistoryWithUtf8JsonSerializer |    529.4 ns |     0.2486 ns |   0.0140 ns | 0.0448 |     192 B |
|                       SerializeRevisionWithJilSerializer |  1,593.5 ns |    18.6518 ns |   1.0539 ns | 0.9308 |    3912 B |
|                  SerializeRevisionWithJsonSpanSerializer |  1,714.9 ns |    63.3756 ns |   3.5808 ns | 0.3014 |    1272 B |
|                  SerializeRevisionWithUtf8JsonSerializer |  1,598.2 ns |    26.9141 ns |   1.5207 ns | 0.1526 |     648 B |
|                  SerializeSearchExcerptWithJilSerializer |  2,331.2 ns |    17.2460 ns |   0.9744 ns | 1.1406 |    4792 B |
|             SerializeSearchExcerptWithJsonSpanSerializer |  2,558.0 ns |    13.2842 ns |   0.7506 ns | 0.4807 |    2032 B |
|             SerializeSearchExcerptWithUtf8JsonSerializer |  2,910.2 ns |    24.0888 ns |   1.3611 ns | 0.2365 |    1000 B |
|                    SerializeShallowUserWithJilSerializer |    627.6 ns |    10.5999 ns |   0.5989 ns | 0.3538 |    1488 B |
|               SerializeShallowUserWithJsonSpanSerializer |    701.8 ns |     6.6815 ns |   0.3775 ns | 0.1211 |     512 B |
|               SerializeShallowUserWithUtf8JsonSerializer |    615.4 ns |     4.5951 ns |   0.2596 ns | 0.0639 |     272 B |
|                  SerializeSuggestedEditWithJilSerializer |  1,481.9 ns |    16.9515 ns |   0.9578 ns | 0.8907 |    3744 B |
|             SerializeSuggestedEditWithJsonSpanSerializer |  1,524.2 ns |     9.4719 ns |   0.5352 ns | 0.2728 |    1152 B |
|             SerializeSuggestedEditWithUtf8JsonSerializer |  1,700.3 ns |    75.3905 ns |   4.2597 ns | 0.1354 |     576 B |
|                            SerializeTagWithJilSerializer |    599.9 ns |     1.5677 ns |   0.0886 ns | 0.3290 |    1384 B |
|                       SerializeTagWithJsonSpanSerializer |    585.2 ns |    14.0444 ns |   0.7935 ns | 0.1020 |     432 B |
|                       SerializeTagWithUtf8JsonSerializer |    571.5 ns |     0.8297 ns |   0.0469 ns | 0.0505 |     216 B |
|                       SerializeTagScoreWithJilSerializer |    748.3 ns |    13.7181 ns |   0.7751 ns | 0.5159 |    2168 B |
|                  SerializeTagScoreWithJsonSpanSerializer |    892.8 ns |    13.2025 ns |   0.7460 ns | 0.1478 |     624 B |
|                  SerializeTagScoreWithUtf8JsonSerializer |    731.1 ns |     5.9911 ns |   0.3385 ns | 0.0753 |     320 B |
|                     SerializeTagSynonymWithJilSerializer |    614.5 ns |     3.1760 ns |   0.1795 ns | 0.3099 |    1304 B |
|                SerializeTagSynonymWithJsonSpanSerializer |    418.9 ns |     1.7369 ns |   0.0981 ns | 0.0854 |     360 B |
|                SerializeTagSynonymWithUtf8JsonSerializer |    600.1 ns |     0.5225 ns |   0.0295 ns | 0.0429 |     184 B |
|                        SerializeTagWikiWithJilSerializer |  1,691.9 ns |    31.9404 ns |   1.8047 ns | 1.0090 |    4240 B |
|                   SerializeTagWikiWithJsonSpanSerializer |  1,779.1 ns |    21.4480 ns |   1.2119 ns | 0.3376 |    1424 B |
|                   SerializeTagWikiWithUtf8JsonSerializer |  1,845.1 ns |    33.9473 ns |   1.9181 ns | 0.1717 |     728 B |
|                         SerializeTopTagWithJilSerializer |    417.9 ns |     1.0216 ns |   0.0577 ns | 0.3085 |    1296 B |
|                    SerializeTopTagWithJsonSpanSerializer |    386.7 ns |     0.5402 ns |   0.0305 ns | 0.0777 |     328 B |
|                    SerializeTopTagWithUtf8JsonSerializer |    375.2 ns |     0.3989 ns |   0.0225 ns | 0.0434 |     184 B |
|                           SerializeUserWithJilSerializer |  1,880.1 ns |    35.9645 ns |   2.0321 ns | 1.0471 |    4400 B |
|                      SerializeUserWithJsonSpanSerializer |  1,922.5 ns |    24.5448 ns |   1.3868 ns | 0.4311 |    1824 B |
|                      SerializeUserWithUtf8JsonSerializer |  2,324.9 ns |    30.9730 ns |   1.7500 ns | 0.2136 |     904 B |
|                   SerializeUserTimelineWithJilSerializer |    752.5 ns |     4.2936 ns |   0.2426 ns | 0.5026 |    2112 B |
|              SerializeUserTimelineWithJsonSpanSerializer |    763.2 ns |    56.4443 ns |   3.1892 ns | 0.1364 |     576 B |
|              SerializeUserTimelineWithUtf8JsonSerializer |    850.0 ns |     4.6792 ns |   0.2644 ns | 0.0715 |     304 B |
|                SerializeWritePermissionWithJilSerializer |    414.4 ns |     1.6260 ns |   0.0919 ns | 0.3181 |    1336 B |
|           SerializeWritePermissionWithJsonSpanSerializer |    374.0 ns |     0.0806 ns |   0.0046 ns | 0.0873 |     368 B |
|           SerializeWritePermissionWithUtf8JsonSerializer |    353.3 ns |     2.6859 ns |   0.1518 ns | 0.0472 |     200 B |
|            SerializeMobileBannerAdImageWithJilSerializer |    262.0 ns |     1.7947 ns |   0.1014 ns | 0.1426 |     600 B |
|       SerializeMobileBannerAdImageWithJsonSpanSerializer |    202.6 ns |     0.4441 ns |   0.0251 ns | 0.0379 |     160 B |
|       SerializeMobileBannerAdImageWithUtf8JsonSerializer |    212.0 ns |     0.4298 ns |   0.0243 ns | 0.0207 |      88 B |
|                           SerializeSiteWithJilSerializer |  2,026.2 ns |    26.9510 ns |   1.5228 ns | 0.9575 |    4024 B |
|                      SerializeSiteWithJsonSpanSerializer |  1,623.6 ns |    14.6481 ns |   0.8276 ns | 0.3376 |    1424 B |