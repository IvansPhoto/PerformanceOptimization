using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace PerformanceOptimization.Mapper.TwoArrays;

[SimpleJob(RunStrategy.Monitoring, launchCount: 5, warmupCount: 3, iterationCount: 35, invocationCount: 3)]
[MemoryDiagnoser]
[KeepBenchmarkFiles]
[MarkdownExporterAttribute.GitHub]
public class BenchmarkMonitoring
{
    private string _input = null!;
    private readonly Generator _generator = new();

    [IterationSetup]
    public void IterationSetup() => _input = _generator.GetInputString(10000).Input;
    
    [Benchmark]
    public Output[] MapOriginal() => Original.Map(_input);

    [Benchmark(Baseline = true)]
    public Output[] MapOptimized1() => Optimized.Map(_input);
}