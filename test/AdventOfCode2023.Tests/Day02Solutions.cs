using AdventOfCode2023.Day02;

namespace AdventOfCode2023.Tests;

public class Day02Solutions
{
    [Fact]
    public void Puzzle1And2_GamesPossibleFor12Red13Green14Blue_AndPower()
    {
        var games = new Games(Input.Day02);
        games.TotalPossibleFor12Red13Green14Blue.Should().Be(2476);
        games.Sum(x => x.Power).Should().Be(54911);
    }

    public const string Example1 =
@"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

    [Fact]
    public void Example1_GamesPossibleFor12Red13Green14Blue()
    {
        var games = new Games(Example1);
        games.TotalPossibleFor12Red13Green14Blue.Should().Be(8);
        games.Sum(x => x.Power).Should().Be(2286);
    }
}