using AdventOfCode2023.Day01;

namespace AdventOfCode2023.Tests;

public class Day01Solutions
{
    [Fact]
    public void Puzzle1And2_SumCalibrationValues()
    {
        var calibrations = new Calibrations(Input.Day01);
        calibrations.SumOfDigits.Should().Be(55_621);
        calibrations.SumOfNumbers.Should().Be(53_592);
    }

    public const string Example2 =
@"two1nine
eightwo1three
abcone2threexyz
xtwone3four
4nineeightseven2
zoneight234
7pqrstsixteen";

    [Fact]
    public void Example2_SumsCalibrationValues()
    {
        var calibrations = new Calibrations(Example2);
        calibrations.SumOfNumbers.Should().Be(281);
    }

    public const string Example1 =
@"1abc2
pqr3stu8vwx
a1b2c3d4e5f
treb7uchet";

    [Fact]
    public void Example1_SumsCalibrationValues()
    {
        var calibrations = new Calibrations(Example1);
        calibrations.SumOfDigits.Should().Be(142);
    }
}