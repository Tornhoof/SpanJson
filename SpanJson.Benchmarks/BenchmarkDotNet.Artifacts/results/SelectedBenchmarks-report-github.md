``` ini

BenchmarkDotNet=v0.10.13, OS=Windows 10 Redstone 3 [1709, Fall Creators Update] (10.0.16299.309)
Intel Core i7-4790K CPU 4.00GHz (Haswell), 1 CPU, 8 logical cores and 4 physical cores
Frequency=3906246 Hz, Resolution=256.0003 ns, Timer=TSC
.NET Core SDK=2.1.300-preview1-008174
  [Host]     : .NET Core 2.1.0-preview3-26329-05 (CoreCLR 4.6.26329.01, CoreFX 4.6.26329.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.0-preview3-26329-05 (CoreCLR 4.6.26329.01, CoreFX 4.6.26329.01), 64bit RyuJIT


```
|                                                      Method |     Mean |     Error |    StdDev |  Gen 0 | Allocated |
|------------------------------------------------------------ |---------:|----------:|----------:|-------:|----------:|
| SerializeMobileCommunityBulletinInputWithSpanJsonSerializer | 959.4 ns | 0.3213 ns | 0.2509 ns | 0.1698 |     720 B |
