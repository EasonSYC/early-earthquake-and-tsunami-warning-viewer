using System.Text.RegularExpressions;

namespace EasonEetwViewer.JmaTravelTime;
public partial class TimeTableProvider : ITimeTableProvider
{
    private readonly IEnumerable<TimeTableEntry> _timeTable;
    private TimeTableProvider(IEnumerable<TimeTableEntry> timeTable) => _timeTable = timeTable;

    public static TimeTableProvider FromFile(string fileName)
    {
        string[] rows = File.ReadAllLines(fileName);

        string? unmatchingRow = rows.FirstOrDefault(r => !Pattern().IsMatch(r));
        return unmatchingRow is not null
            ? throw new FormatException($"{unmatchingRow} does not fit format of time table")
            : new(rows.Select(row =>
            new TimeTableEntry()
            {
                Depth = int.Parse(row[22..25]),
                Radius = int.Parse(row[27..32]),
                Times = [double.Parse(row[2..10]),
                    double.Parse(row[13..21])]
            }));
    }

    private const int _rowsPerDepth = 236;

    public (double pDistance, double sDistance) DistanceFromDepthTime(int depth, double timeSecond)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(timeSecond, nameof(timeSecond));
        int startIndex = depth switch
        {
            >= 0 and <= 50 => depth / 2 * _rowsPerDepth,
            >= 50 and <= 200 => (25 + ((depth - 50) / 5)) * _rowsPerDepth,
            >= 200 and <= 700 => (55 + ((depth - 200) / 10)) * _rowsPerDepth,
            _ => throw new ArgumentOutOfRangeException(nameof(depth)),
        };
        int endIndex = startIndex + _rowsPerDepth;

        return (SearchForDistance(startIndex, endIndex, timeSecond, 0), SearchForDistance(startIndex, endIndex, timeSecond, 1));
    }

    private double SearchForDistance(int startIndex, int endIndex, double timeSecond, int index)
    {
        int leftPoint;
        if (timeSecond <= _timeTable.ElementAt(startIndex).Times.ElementAt(index))
        {
            return 0;
        }
        else if (timeSecond >= _timeTable.ElementAt(endIndex - 1).Times.ElementAt(index))
        {
            leftPoint = endIndex - 2;
        }
        else
        {
            for (leftPoint = startIndex; leftPoint < endIndex && _timeTable.ElementAt(leftPoint).Times.ElementAt(index) <= timeSecond; ++leftPoint)
            {
                ;
            }

            leftPoint -= 1;
        }

        double x1 = _timeTable.ElementAt(leftPoint).Times.ElementAt(index);
        double y1 = _timeTable.ElementAt(leftPoint).Radius;
        double x2 = _timeTable.ElementAt(leftPoint + 1).Times.ElementAt(index);
        double y2 = _timeTable.ElementAt(leftPoint + 1).Radius;

        return y1 + ((timeSecond - x1) * (y2 - y1) / (x2 - x1));
    }

    [GeneratedRegex(@"^P [\d\s]{4}.\d{3} S [\d\s]{4}.\d{3} [\d\s]{3}  [\d\s]{5}$")]
    private static partial Regex Pattern();
}