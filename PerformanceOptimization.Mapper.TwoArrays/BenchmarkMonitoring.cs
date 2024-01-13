using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Exporters.Csv;

namespace PerformanceOptimization.Mapper.TwoArrays;

[SimpleJob(RunStrategy.Monitoring, launchCount: 3, warmupCount: 3, iterationCount: 15, invocationCount: 3)]
[MemoryDiagnoser]

[KeepBenchmarkFiles]
[AsciiDocExporter]
[CsvExporter]
[CsvMeasurementsExporter]
[MarkdownExporterAttribute.Default]
public class BenchmarkMonitoring
{
    private Input _input = null!;
    private readonly Generator _generator = new();

    [IterationSetup]
    public void IterationSetup() => _input = _generator.GetInput(10000).input;
    
    [Benchmark]
    public Output[] MapOriginal() => Original.Map(_input);

    [Benchmark(Baseline = true)]
    public Output[] MapOptimized1() => Optimized.Map(_input);
}