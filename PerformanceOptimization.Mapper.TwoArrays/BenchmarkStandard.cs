using BenchmarkDotNet.Attributes;

namespace PerformanceOptimization.Mapper.TwoArrays;

[MemoryDiagnoser]
[KeepBenchmarkFiles]
[AsciiDocExporter]
[CsvExporter]
[CsvMeasurementsExporter]
[MarkdownExporterAttribute.Default]
public class BenchmarkStandard
{
    private readonly Generator _generator = new();

    public IEnumerable<object> DataSource()
    {
        yield return _generator.GetInput(10000).input;
        yield return _generator.GetInput(10000).input;
        yield return _generator.GetInput(10000).input;
        yield return _generator.GetInput(10000).input;
        yield return _generator.GetInput(10000).input;
    }
    
    [Benchmark]
    [ArgumentsSource(nameof(DataSource))]
    public Output[] MapOriginal(Input data) => Original.Map(data);

    [Benchmark(Baseline = true)]
    [ArgumentsSource(nameof(DataSource))]
    public Output[] MapOptimized1(Input data) => Optimized.Map(data);
}