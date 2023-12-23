using System.Text;

namespace AdventOfCode2023.Day03;

public class PartNumbers : IEnumerable<PartNumber>
{
    private static readonly List<(int X, int Y)> Shifts =
    [
        (-1,  0),
        (-1,  1),
        ( 0,  1),
        ( 1,  1),
        ( 1,  0),
        ( 1, -1),
        ( 0, -1),
        (-1, -1),
    ];
    private readonly List<PartNumber> _partNumbers;
    private readonly List<string> _rows;

    public PartNumbers(string input)
    {
        _partNumbers = new List<PartNumber>();
        _rows = input.SplitLines().ToList();

        for (var rowIndex = 0; rowIndex < _rows.Count; ++rowIndex)
        {
            var row = _rows[rowIndex];
            StringBuilder value = new();
            int? startCol = null;
            for (var col = 0; col < row.Length; ++col)
            {
                var c = row[col];
                if (!char.IsDigit(c) && startCol is null) continue;
                else if (!char.IsDigit(c) && startCol is not null)
                {
                    _partNumbers.Add(new PartNumber(value.ToString(), (rowIndex, startCol.Value)));
                    value.Clear();
                    startCol = null;
                }
                else if (char.IsDigit(c))
                {
                    value.Append(c);
                    startCol ??= col;
                }
            }
            if (startCol is not null)
            {
                _partNumbers.Add(new PartNumber(value.ToString(), (rowIndex, startCol.Value)));
            }
        }

        foreach (var partNumber in _partNumbers)
        {
            partNumber.IsValid = IsValid(partNumber);
        }
    }

    private bool IsValid(PartNumber partNumber)
    {
        return Enumerable
            .Range(partNumber.Index.Col, partNumber.Length)
            .Any(HasNearbySymbol);

        bool HasNearbySymbol(int col) => Shifts.Any(shift => IsSymbol(partNumber.Index.Row + shift.X, col + shift.Y));
    }

    private bool IsSymbol(int row, int col)
    {
        if (row < 0 || row >= _rows.Count) return false;
        if (col < 0 || col >= _rows[0].Length) return false;
        return !char.IsDigit(_rows[row][col]) && _rows[row][col] != '.';
    }

    public IEnumerator<PartNumber> GetEnumerator() => _partNumbers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PartNumber(string value, (int Row, int Col) index)
{
    public (int Row, int Col) Index { get; } = index;
    public int Value { get; } = int.Parse(value);
    public int Length { get; } = value.Length;
    public bool IsValid { get; set; }
}