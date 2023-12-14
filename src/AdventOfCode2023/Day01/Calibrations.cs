namespace AdventOfCode2023.Day01;

public class Calibrations
{
    public int SumOfDigits { get; }

    public Calibrations(string input)
    {
        var lines = input.SplitLines();
        SumOfDigits = lines
            .Select(GetDigits)
            .Select(digits => $"{digits.First()}{digits.Last()}")
            .Sum(int.Parse);
    }

    private static IEnumerable<int> GetDigits(string value) => value.Where(char.IsDigit).Select(x => x - '0');
}