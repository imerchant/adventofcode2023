namespace AdventOfCode2023.Day01;

public class Calibrations
{
    public static readonly string[] Numbers = [ "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" ];

    public int SumOfDigits { get; }
    public int SumOfNumbers { get; }

    public Calibrations(string input)
    {
        var lines = input.SplitLines();
        SumOfDigits = lines
            .Select(GetDigits)
            .Select(digits => $"{digits.First()}{digits.Last()}")
            .Sum(int.Parse);
        SumOfNumbers = lines
            .Select(GetNumbers)
            .Select(digits => $"{digits.First()}{digits.Last()}")
            .Sum(int.Parse);
    }

    private static IEnumerable<int> GetDigits(string value) => value.Where(char.IsDigit).Select(x => x - '0');

    private static IEnumerable<int> GetNumbers(string value)
    {
        for (var k = 0; k < value.Length; ++k)
        {
            if (char.IsDigit(value[k]))
            {
                yield return value[k] - '0';
            }
            else
            {
                for (var j = 0; j < Numbers.Length; ++j)
                {
                    if (k+Numbers[j].Length > value.Length) continue;
                    var next = value.Substring(k, Numbers[j].Length);
                    if (next == Numbers[j])
                        yield return j+1;
                }
            }
        }
    }
}