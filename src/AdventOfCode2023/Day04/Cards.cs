
namespace AdventOfCode2023.Day04;

public class Cards : IEnumerable<Card>
{
    public static readonly Regex CardRegex = new(@"Card {1,3}(?'id'\d+?): (?'winning'[\d ]+) \| (?'have'[\d ]+)", RegexOptions.Compiled);
    private readonly List<Card> _cards;

    public Cards(string input)
    {
        _cards = [];

        foreach (var line in input.SplitLines())
        {
            var match = CardRegex.Match(line);
            if (!match.Success) throw new Exception($"could not parse <{line}>");

            var id = int.Parse(match.Groups["id"].Value);
            var winning = match.Groups["winning"].Value.SplitOn(' ').Select(int.Parse).ToList();
            var have = match.Groups["have"].Value.SplitOn(' ').Select(int.Parse).ToList();

            _cards.Add(new Card(id, winning, have));
        }
    }

    public IEnumerator<Card> GetEnumerator() => _cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Card(int id, List<int> winning, List<int> have)
{
    public int Id { get; } = id;
    public List<int> Winning { get; } = winning;
    public List<int> Have { get; } = have;
    public long Points { get; } = GetPoints(winning, have);

    private static long GetPoints(List<int> winning, List<int> have)
    {
        var intersection = have.Intersect(winning).Count() - 1;
        return intersection switch
        {
            >= 0 => (long)Math.Pow(2, intersection),
            _ => 0
        };
    }
}