using BenchmarkDotNet.Running;
using PerformanceOptimization.Mapper.TwoArrays;

_ = BenchmarkRunner.Run<BenchmarkStandard>();
// _ = BenchmarkRunner.Run<BenchmarkMonitoring>();