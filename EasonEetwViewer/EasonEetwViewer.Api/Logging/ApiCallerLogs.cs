using EasonEetwViewer.Api.Services;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Api.Logging;

/// <summary>
/// Represents the log messages used in <see cref="ApiCaller"/>.
/// </summary>
internal static partial class ApiCallerLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<ApiCaller> logger);
}
