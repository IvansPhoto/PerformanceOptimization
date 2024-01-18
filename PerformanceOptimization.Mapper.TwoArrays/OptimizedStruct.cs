using System.Globalization;
using System.Text.Json;

namespace PerformanceOptimization.Mapper.TwoArrays;

public static class OptimizedStruct
{
    private static readonly DateTimeFormatInfo DateTimeFormatInfo = new()
    {
        ShortDatePattern = "MM/dd/yyyy",
        LongDatePattern = "MM/dd/yyyy"
    };
    
    public static OutputSt[] Map(string json)
    {
        var data = JsonSerializer.Deserialize<InputSt>(json);
        var places = data.Places.ToDictionary(GetDate, GetSeasonState);

        return data.Temperatures.Select(t =>
        {
            places.TryGetValue(DateOnly.FromDateTime(t.Date), out var result);
            return new OutputSt
            {
                Date = t.Date,
                Value = double.Round(t.Value, 3),
                Season = result.Season,
                State = result.State
            };
        }).ToArray();
    }

    public static List<OutputSt> MapFor(string json)
    {
        var data = JsonSerializer.Deserialize<InputSt>(json)!;
        var places = data.Places.ToDictionary(GetDate, GetSeasonState);

        var outputs = new List<OutputSt>(data.Temperatures.Length);
        foreach (var t in data.Temperatures)
        {
            places.TryGetValue(DateOnly.FromDateTime(t.Date), out var result);
            outputs.Add(new OutputSt
            {
                Date = t.Date,
                Value = double.Round(t.Value, 3),
                Season = result.Season,
                State = result.State
            });
        }

        return outputs;
    }
    
    private static DateOnly GetDate(string s)
    {
        return DateOnly.Parse(s.AsSpan()[..10], DateTimeFormatInfo);
    }

    private static (string? Season, string? State) GetSeasonState(string s)
    {
        return (GetSeason(s), GetState(s));
        
        string? GetState(string str)
        {
            if (str.Length < 21)
                return null;

            var state = str.AsSpan().Slice(20, 2);
            return state switch
            {
                { } when state.Equals("WA", StringComparison.OrdinalIgnoreCase) => "Washington",
                { } when state.Equals("OR", StringComparison.OrdinalIgnoreCase) => "Oregon",
                { } when state.Equals("CO", StringComparison.OrdinalIgnoreCase) => "Colorado",
                { } when state.Equals("AL", StringComparison.OrdinalIgnoreCase) => "Alaska",
                { } when state.Equals("NE", StringComparison.OrdinalIgnoreCase) => "New York",
                _ => null
            };
        }

        string? GetSeason(string str)
        {
            if (str.Length < 24)
                return null;

            var season = str.AsSpan()[23..];
            return season switch
            {
                { } when season.Equals("wi", StringComparison.OrdinalIgnoreCase) => "Winter",
                { } when season.Equals("sp", StringComparison.OrdinalIgnoreCase) => "Spring",
                { } when season.Equals("su", StringComparison.OrdinalIgnoreCase) => "Summer",
                { } when season.Equals("fall", StringComparison.OrdinalIgnoreCase) => "Autumn",
                _ => null
            };
        }
    }
}

public record struct OutputSt
{
    public DateTime Date { get; init; }
    public double Value { get; init; }
    public string? State { get; init; }
    public string? Season { get; init; }
}

public record struct InputSt(TemperatureSt[] Temperatures, string[] Places);

public record struct TemperatureSt(DateTime Date, double Value);