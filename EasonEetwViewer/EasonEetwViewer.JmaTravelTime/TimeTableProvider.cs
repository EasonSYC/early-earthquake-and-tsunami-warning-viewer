﻿using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.JmaTravelTime;
/// <summary>
/// Default implementation for <see cref="ITimeTableProvider"/> for JMA 2001 Time Table.
/// </summary>
public sealed partial class TimeTableProvider : ITimeTableProvider
{
    /// <summary>
    /// The collection of <see cref="TimeTableEntry"/> for the timetable.
    /// </summary>
    private readonly IEnumerable<TimeTableEntry> _timeTable;
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<TimeTableProvider> _logger;
    /// <summary>
    /// Creates the instance of the class with the specified timetable.
    /// </summary>
    /// <param name="timeTable">The collection of <see cref="TimeTableEntry"/> for the timetable.</param>
    /// <param name="logger">The logger to be used.</param>
    private TimeTableProvider(IEnumerable<TimeTableEntry> timeTable, ILogger<TimeTableProvider> logger)
    {
        _timeTable = timeTable;
        _logger = logger;
    }
    /// <summary>
    /// Create a new instance of <see cref="TimeTableProvider"/> from a file.
    /// </summary>
    /// <param name="fileName">The file that the time table is stored in.</param>
    /// <param name="logger">The logger to be used.</param>
    /// <returns>The instance of <see cref="TimeTableProvider"/> created.</returns>
    /// <exception cref="FormatException"></exception>
    public static TimeTableProvider FromFile(string fileName, ILogger<TimeTableProvider> logger)
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
            }), logger);
    }
    /// <summary>
    /// The number of rows for each depth in the time table.
    /// </summary>
    private const int _rowsPerDepth = 236;

    /// <inheritdoc/>
    /// <exception cref="ArgumentOutOfRangeException">When the depth is not between the range of 0 to 700, or when the time second is negative.</exception>
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
        _logger.LookingLines(startIndex, endIndex);
        return (SearchForDistance(startIndex, endIndex, timeSecond, 0), SearchForDistance(startIndex, endIndex, timeSecond, 1));
    }

    /// <summary>
    /// Find the distance that is possible to travel within the given range, for the given index of the distances array.
    /// </summary>
    /// <param name="startIndex">The start index to look at.</param>
    /// <param name="endIndex">The end index to look at.</param>
    /// <param name="timeSecond">The time elapsed.</param>
    /// <param name="index">The element to use to refer to for the distance.</param>
    /// <returns></returns>
    private double SearchForDistance(int startIndex, int endIndex, double timeSecond, int index)
    {
        int leftPoint;
        if (timeSecond <= _timeTable.ElementAt(startIndex).Times.ElementAt(index))
        {
            _logger.BeforeStart(timeSecond);
            return 0;
        }
        else if (timeSecond >= _timeTable.ElementAt(endIndex - 1).Times.ElementAt(index))
        {
            _logger.AfterEnd(timeSecond);
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
        _logger.LinearPolating(x1, y1, x2, y2);

        return y1 + ((timeSecond - x1) * (y2 - y1) / (x2 - x1));
    }

    [GeneratedRegex(@"^P [\d\s]{4}.\d{3} S [\d\s]{4}.\d{3} [\d\s]{3}  [\d\s]{5}$")]
    private static partial Regex Pattern();
}