using BenchmarkDotNet.Attributes;

namespace PerformanceOptimization.Mapper.TwoArrays;

[MemoryDiagnoser]
[KeepBenchmarkFiles]
[MarkdownExporterAttribute.GitHub]
public class BenchmarkStandard
{
    private readonly Generator _generator = new();
    private Input _input = null!;
    
    [Params(10000)] 
    public int _n;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _input = _generator.GetInput(_n).input;
    }
    
    [Benchmark]
    public Output[] MapOriginal() => Original.Map(_input);

    [Benchmark(Baseline = true)]
    public Output[] MapOptimized() => Optimized.Map(_input);
}