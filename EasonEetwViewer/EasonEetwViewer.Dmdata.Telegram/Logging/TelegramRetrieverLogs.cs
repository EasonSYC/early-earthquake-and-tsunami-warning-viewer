using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Telegram.Services;

/// <summary>
/// Represents the log messages used in <see cref="TelegramRetriever"/>.
/// </summary>
internal static partial class TelegramRetrieverLogs
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
        this ILogger<TelegramRetriever> logger);
    /// <summary>
    /// Log when parser experiences exception.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="exception">The exception message thrown by the parser.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(ParserExceptionIgnored),
        Level = LogLevel.Warning,
        Message = "Parser Exception Ignored: `{Exception}`.")]
    public static partial void ParserExceptionIgnored(
        this ILogger<TelegramRetriever> logger, string exception);
    /// <summary>
    /// Log when retriever experiences exception.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="error">The error message returned by the API Call.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(ApiErrorIgnored),
        Level = LogLevel.Warning,
        Message = "Error Message from API Ignored: `{Error}`.")]
    public static partial void ApiErrorIgnored(
        this ILogger<TelegramRetriever> logger, string error);
    /// <summary>
    /// Log when sending request.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="id">The telegram ID.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(Requesting),
        Level = LogLevel.Debug,
        Message = "Requesting telegram `{Id}`.")]
    public static partial void Requesting(
        this ILogger<TelegramRetriever> logger, string id);
    /// <summary>
    /// Log when request successfully sent.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="id">The telegram ID.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(Requested),
        Level = LogLevel.Information,
        Message = "Requested telegram `{Id}`.")]
    public static partial void Requested(
        this ILogger<TelegramRetriever> logger, string id);
    /// <summary>
    /// Log when not authenticated.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(NotAuthenticated),
        Level = LogLevel.Warning,
        Message = "Not authenticated.")]
    public static partial void NotAuthenticated(
        this ILogger<TelegramRetriever> logger);
}
