namespace AdventOfCode2023.Day05;

public class Garden
{
    public static readonly string[] Items = ["seed", "soil", "fertilizer", "water", "light", "temperature", "humidity"];
    public const string Seed = "seed";
    public const string Soil = "soil";
    public const string Fertilizer = "fertilizer";
    public const string Water = "water";
    public const string Light = "light";
    public const string Temperature = "temperature";
    public const string Humidity = "humidity";
    public const string Location = "location";

    public static readonly Regex SeedsRegex = new(@"seeds: (?'digits'[\d ]+)", RegexOptions.Compiled);
    public static readonly Regex MapStartRegex = new(@"(?'source'\w+)-to-(?'destination'\w+) map:", RegexOptions.Compiled);
    public static readonly Regex MapRangeRegex = new(@"(?'destinationStart'\d+) (?'sourceStart'\d+) (?'length'\d+)", RegexOptions.Compiled);

    public Dictionary<string, Map> Maps { get; } = new();
    public List<long> Seeds { get; }

    public Garden(string input)
    {
        Map? map = null;
        foreach (var line in input.SplitLines())
        {
            if (SeedsRegex.TryMatch(line, out var seedsMatch))
            {
                Seeds = seedsMatch.Groups["digits"].Value.SplitOn(' ').Select(long.Parse).ToList();
            }
            else if (MapStartRegex.TryMatch(line, out var mapStartMatch))
            {
                var source = mapStartMatch.Groups["source"].Value;
                var destination = mapStartMatch.Groups["destination"].Value;
                map = Maps[source] = new Map(source, destination);
            }
            else if (MapRangeRegex.TryMatch(line, out var mapRangeMatch))
            {
                var sourceStart = long.Parse(mapRangeMatch.Groups["sourceStart"].Value);
                var destinationStart = long.Parse(mapRangeMatch.Groups["destinationStart"].Value);
                var length = int.Parse(mapRangeMatch.Groups["length"].Value);
                map?.Ranges.Add(new MapRange(sourceStart, destinationStart, length));
            }
        }
    }

    public long SeedToLocation(long seed)
    {
        return Items.Aggregate(seed, (state, item) => Maps[item][state]);
    }
}

public class Map(string source, string destination)
{
    public string Source { get; } = source;
    public string Destination { get; } = destination;
    public List<MapRange> Ranges { get; } = [];

    public long this[long num] => Get(num);

    private long Get(long num)
    {
        MapRange? value = Ranges.FirstOrDefault(range => num >= range.SourceStart && num < range.SourceStart + range.Length);
        return value is null ? num : num - value.SourceStart + value.DestinationStart;
    }
}

public record MapRange(long SourceStart, long DestinationStart, int Length);