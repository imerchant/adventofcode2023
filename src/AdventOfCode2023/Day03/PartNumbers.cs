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
        _partNumbers = [];
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
                    _partNumbers.Add(new PartNumber(value.ToString(), rowIndex, startCol.Value));
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
                _partNumbers.Add(new PartNumber(value.ToString(), rowIndex, startCol.Value));
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

        bool HasNearbySymbol(int col) =>
            Shifts.Any(shift =>
            {
                var x = partNumber.Index.Row + shift.X;
                var y = col + shift.Y;
                var isSymbol = IsSymbol(x, y, out var isGear);
                if (isGear) partNumber.GearIndex = (x, y);
                return isSymbol;
            });
    }

    private bool IsSymbol(int row, int col, out bool isGear)
    {
        isGear = false;
        if (row < 0 || row >= _rows.Count) return false;
        if (col < 0 || col >= _rows[0].Length) return false;
        var c = _rows[row][col];
        isGear = c == '*';
        return !char.IsDigit(c) && c != '.';
    }

    public IEnumerator<PartNumber> GetEnumerator() => _partNumbers.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PartNumber(string value, int row, int col)
{
    public (int Row, int Col) Index { get; } = (row, col);
    public long Value { get; } = long.Parse(value);
    public int Length { get; } = value.Length;
    public bool IsValid { get; set; }
    public (int Row, int Col)? GearIndex { get; set; }
}