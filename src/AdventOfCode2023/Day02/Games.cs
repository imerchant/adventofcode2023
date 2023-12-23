namespace AdventOfCode2023.Day02;

public class Games: IEnumerable<Game>
{
    public static readonly Regex GameRegex = new(@"Game (\d+?): (?'content'.+)", RegexOptions.Compiled);

    private readonly List<Game> _games;

    public int TotalPossibleFor12Red13Green14Blue => _games.Sum(game => game.PossibleFor12Red13Green14Blue ? game.Id : 0);

    public Games(string input)
    {
        _games = input
            .SplitLines()
            .Select((game, k) => new Game(k+1, GameRegex.Match(game).Groups["content"].Value))
            .ToList();
    }

    public IEnumerator<Game> GetEnumerator() => _games.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Game
{
    private static readonly Regex Red = new(@"(?'value'\d+?) r", RegexOptions.Compiled);
    private static readonly Regex Blue = new(@"(?'value'\d+?) b", RegexOptions.Compiled);
    private static readonly Regex Green = new(@"(?'value'\d+?) g", RegexOptions.Compiled);
    private readonly List<Pull> pulls = [];

    public int Id { get; }
    public bool PossibleFor12Red13Green14Blue => pulls.All(pull => pull.PossibleFor12Red13Green14Blue);
    public long Power { get; }

    public Game(int id, string input)
    {
        Id = id;
        foreach (var pull in input.SplitOn("; "))
        {
            var blue  = Blue.TryMatch(pull, out var bm)  ? long.Parse(bm.Groups["value"].Value) : 0;
            var red   = Red.TryMatch(pull, out var rm)   ? long.Parse(rm.Groups["value"].Value) : 0;
            var green = Green.TryMatch(pull, out var gm) ? long.Parse(gm.Groups["value"].Value) : 0;
            pulls.Add(new Pull(red, green, blue));
        }
        Power = pulls.Select(pull => pull.Red).Max() *
                pulls.Select(pull => pull.Green).Max() *
                pulls.Select(pull => pull.Blue).Max();
    }
}

public class Pull(long red, long green, long blue)
{
    public long Red { get; } = red;
    public long Green { get; } = green;
    public long Blue { get; } = blue;
    public bool PossibleFor12Red13Green14Blue => Red <= 12 && Green <= 13 && Blue <= 14;
}