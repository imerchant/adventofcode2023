using AdventOfCode2023.Day05;

namespace AdventOfCode2023.Tests;

public class Day05Solutions
{
    [Fact]
    public void Puzzle1_FindLowestLocation()
    {
        var garden = new Garden(Input.Day05);
        garden.Seeds.Select(garden.SeedToLocation).Min().Should().Be(346433842L);
    }

    public const string Example =
@"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    [Fact]
    public void Example_GardenInitializesCorrectly()
    {
        var garden = new Garden(Example);
        garden.Seeds.Should().BeEquivalentTo([79, 14, 55, 13]);
    }

    [Theory]
    [InlineData(79, 81)]
    [InlineData(14, 14)]
    [InlineData(55, 57)]
    [InlineData(13, 13)]
    public void Example_SeedToSoil(int seed, int soil)
    {
        var garden = new Garden(Example);
        garden.Maps[Garden.Seed][seed].Should().Be(soil);
    }

    [Theory]
    [InlineData(79, 82)]
    [InlineData(14, 43)]
    [InlineData(55, 86)]
    [InlineData(13, 35)]
    public void Example_SeedToLocation(int seed, int location)
    {
        var garden = new Garden(Example);
        garden.SeedToLocation(seed).Should().Be(location);
    }

    [Fact]
    public void Example_FindLowestLocation()
    {
        var garden = new Garden(Example);
        garden.Seeds.Select(garden.SeedToLocation).Min().Should().Be(35);
    }
}