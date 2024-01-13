using System.Globalization;

namespace PerformanceOptimization.Mapper.TwoArrays;

public static class Optimized
{
    private static readonly DateTimeFormatInfo DateTimeFormatInfo = new()
    {
        ShortDatePattern = "MM/dd/yyyy HH:mm:ss",
        LongDatePattern = "MM/dd/yyyy HH:mm:ss",
    };
    
    public static Output[] Map(Input data)
    {
        var places = data.Places.ToDictionary(GetDate, GetSeasonState);

        return data.Temperatures.Select(t =>
        {
            places.TryGetValue(t.Date, out var result);
            return new Output
            {
                Date = t.Date,
                Value = double.Round(t.Value, 3),
                Season = result.Season,
                State = result.State
            };
        }).ToArray();
    }

    private static DateTime GetDate(string s)
    {
        return DateTime.Parse(s.AsSpan()[..19], DateTimeFormatInfo);
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