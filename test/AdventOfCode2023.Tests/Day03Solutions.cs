using AdventOfCode2023.Day03;

namespace AdventOfCode2023.Tests;

public class Day03Solutions
{
    [Fact]
    public void Puzzle1_SumsValidPartNumbers()
    {
        var partNumbers = new PartNumbers(Input.Day03);
        partNumbers.Where(x => x.IsValid).Sum(x => x.Value).Should().Be(527_364);
    }

    public const string Example1 =
@"467..114..
...*......
..35..633.
......#...
617*......
.....+.588
..592.....
......755.
...$.*....
.664.598..";

    [Fact]
    public void Example1_ParsesPartNumbers()
    {
        var partNumbers = new PartNumbers(Example1);
        partNumbers.Should().HaveCount(10);
        partNumbers.Count(x => x.IsValid).Should().Be(8);
        partNumbers.Where(x => x.IsValid).Sum(x => x.Value).Should().Be(4361);
    }
}