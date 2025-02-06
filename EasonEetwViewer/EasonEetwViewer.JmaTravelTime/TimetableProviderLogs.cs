using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.JmaTravelTime;
internal static partial class TimetableProviderLogs
{
    /// <summary>
    /// Log when iterating between two indices.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(LookingLines),
        Level = LogLevel.Trace,
        Message = "Searching for time between `{StartIndex}` and `{EndIndex}`.")]
    public static partial void LookingLines(
        this ILogger<TimeTableProvider> logger, int startIndex, int endIndex);
    /// <summary>
    /// Log when the time was shorter than the data.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="time">The time elapsed.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(BeforeStart),
        Level = LogLevel.Debug,
        Message = "The time `{Time}` is before the data in first line.")]
    public static partial void BeforeStart(
        this ILogger<TimeTableProvider> logger, double time);
    /// <summary>
    /// Log when the time was longer than the data.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="time">The time elapsed.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(AfterEnd),
        Level = LogLevel.Debug,
        Message = "The time `{Time}` is after the data in final line.")]
    public static partial void AfterEnd(
        this ILogger<TimeTableProvider> logger, double time);
    /// <summary>
    /// Log the linear polation points.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="x1">The <c>x</c>-coordinate of the first point used.</param>
    /// <param name="y1">The <c>y</c>-coordinate of the first point used.</param>
    /// <param name="x2">The <c>x</c>-coordinate of the second point used.</param>
    /// <param name="y2">The <c>y</c>-coordinate of the second point used.></param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(LinearPolating),
        Level = LogLevel.Debug,
        Message = "Linear polating using `({X1}, {Y1})` and `({X2}, {Y2})`.")]
    public static partial void LinearPolating(
        this ILogger<TimeTableProvider> logger, double x1, double y1, double x2, double y2);

}
