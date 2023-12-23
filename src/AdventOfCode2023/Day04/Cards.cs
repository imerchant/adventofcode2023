
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

        foreach (var card in _cards)
        {
            foreach (var i in Enumerable.Range(card.Id, card.Intersections))
            {
                _cards[i].Instances += card.Instances;
            }
        }
    }

    public IEnumerator<Card> GetEnumerator() => _cards.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Card
{
    public int Id { get; }
    public List<int> Winning { get; }
    public List<int> Have { get; }
    public long Points { get; }
    public int Intersections { get; }
    public long Instances { get; set; } = 1;

    public Card(int id, List<int> winning, List<int> have)
    {
        Id = id;
        Winning = winning;
        Have = have;
        Intersections = have.Intersect(winning).Count();
        Points = Intersections > 0 ? (long)Math.Pow(2, Intersections - 1) : 0;
    }
}