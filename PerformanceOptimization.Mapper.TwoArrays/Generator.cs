using System.Globalization;
using Bogus;

namespace PerformanceOptimization.Mapper.TwoArrays;

public class Generator
{
    private readonly string[] _seasons = ["Winter", "Spring", "Summer", "Autumn"];
    private readonly string[] _states = ["Washington", "Oregon", "New York", "Colorado", "Alaska"];
    private readonly Faker<Output> _faker;

    public Generator()
    {
        _faker = new Faker<Output>()
            .RuleFor(output => output.Value, f => f.Random.Double(-30, 30))
            .RuleFor(output => output.State, f => f.PickRandom(_states))
            .RuleFor(output => output.Season, f => f.PickRandom(_seasons));
    }

    public (Input input, Output[] Ouput) GetInput(int length)
    {
        var rd = new Random();
        var dates = Enumerable.Range(0, length).Select(i => DateTime.Today.AddDays(i * -1)).ToArray();
        var outputs = _faker
            .Generate(length)
            .Select((output, i) =>
            {
                var random = rd.Next(0, 10);
                return new Output
                {
                    Value = double.Round(output.Value, 3),
                    Date = dates[i],
                    State = random > 8 ? null : output.State,
                    Season = random > 6 ? null : output.Season
                };
            }).ToArray();

        var places = outputs.Select(o =>
        {
            const string format = "MM/dd/yyyy HH:mm:ss";
            
            if (o.Season is not null && o.State is not null)
                return $"{o.Date.ToString(format, CultureInfo.InvariantCulture)};{o.State?[..2].ToUpper()};{MapSeason(o.Season)}";

            if (o.Season is null && o.State is not null)
                return $"{o.Date.ToString(format, CultureInfo.InvariantCulture)};{o.State?[..2].ToUpper()};";           
    
            return $"{o.Date.ToString(format, CultureInfo.InvariantCulture)};";
        }).ToArray();

        var temperatures = outputs
            .Select(o => new Temperature(o.Date, double.Round(o.Value, 3)))
            .ToArray();
        
        return (new Input(temperatures, places), outputs);
    }

    string MapSeason(string src) => src switch
    {
        "Winter" => "Wi",
        "Spring" => "Sp",
        "Summer" => "Su",
        "Autumn" => "fall",
        _ => throw new ArgumentOutOfRangeException(nameof(src), src, null)
    };
}