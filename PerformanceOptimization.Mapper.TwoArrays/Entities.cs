namespace PerformanceOptimization.Mapper.TwoArrays;


public record Output
{
    public DateTime Date { get; init; }
    public double Value { get; init; }
    public string? State { get; init; }
    public string? Season { get; init; }
}

public record Input(Temperature[] Temperatures, string[] Places);

public record Temperature(DateTime Date, double Value);