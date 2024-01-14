```

BenchmarkDotNet v0.13.11, Windows 10 (10.0.19045.3930/22H2/2022Update)
AMD Ryzen 7 6800H with Radeon Graphics, 1 CPU, 16 logical and 8 physical cores
.NET SDK 8.0.100
  [Host]     : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2
  DefaultJob : .NET 8.0.0 (8.0.23.53103), X64 RyuJIT AVX2


```
| Method       | _n    | Mean          | Error       | StdDev      | Ratio    | RatioSD | Gen0         | Gen1      | Gen2    | Allocated      | Alloc Ratio |
|------------- |------ |--------------:|------------:|------------:|---------:|--------:|-------------:|----------:|--------:|---------------:|------------:|
| MapOriginal  | 10000 | 14,519.252 ms | 286.1450 ms | 267.6602 ms | 9,039.77 |  238.40 | 1560000.0000 | 4000.0000 |       - | 12746505.34 KB |   14,121.60 |
| MapOptimized | 10000 |      1.607 ms |   0.0262 ms |   0.0232 ms |     1.00 |    0.00 |     140.6250 |   91.7969 | 70.3125 |      902.63 KB |        1.00 |
