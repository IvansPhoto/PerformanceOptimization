using BenchmarkDotNet.Attributes;

namespace PerformanceOptimization.Mapper.TwoArrays;

[MemoryDiagnoser]
[KeepBenchmarkFiles]
[MarkdownExporterAttribute.GitHub]
public class BenchmarkStandard
{
    private readonly Generator _generator = new();
    private string _input = null!;
    
    [Params(10000)] 
    public int _n;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _input = _generator.GetInputString(_n).Input;
    }
    
    [Benchmark]
    public Output[] MapOriginal() => Original.Map(_input);

    [Benchmark(Baseline = true)]
    public Output[] MapOptimized() => Optimized.Map(_input);
    
    [Benchmark]
    public OutputSt[] MapOptimizedStruct() => OptimizedStruct.Map(_input);
}