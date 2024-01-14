```

BenchmarkDotNet v0.13.11, Windows 10 (10.0.19045.3930/22H2/2022Update)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  Job-HQGIJX : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2

InvocationCount=3  IterationCount=35  LaunchCount=5  
RunStrategy=Monitoring  UnrollFactor=1  WarmupCount=3  

```
| Method        | Mean          | Error         | StdDev       | Median        | Ratio    | RatioSD  | Gen0         | Gen1      | Allocated      | Alloc Ratio |
|-------------- |--------------:|--------------:|-------------:|--------------:|---------:|---------:|-------------:|----------:|---------------:|------------:|
| MapOriginal   | 14,915.521 ms | 2,228.5821 ms | 8,807.459 ms | 14,298.955 ms | 4,542.78 | 3,479.11 | 1564666.6667 | 1666.6667 | 12781099.72 KB |   14,160.58 |
| MapOptimized1 |      3.875 ms |     0.3303 ms |     1.305 ms |      3.565 ms |     1.00 |     0.00 |            - |         - |      902.58 KB |        1.00 |
