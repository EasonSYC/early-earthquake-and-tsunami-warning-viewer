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
    /// <summary>
    /// Log when an HTTP Request failed.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="exception">The exception message.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(HttpRequestFails),
        Level = LogLevel.Error,
        Message = "Http Request Failed: `{Exception}`.")]
    public static partial void HttpRequestFails(
        this ILogger<ApiCaller> logger, string exception);
    /// <summary>
    /// Log when an HTTP Request was created.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="method">The HTTP Method.</param>
    /// <param name="relativePath">The relative path.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(RequestCreated),
        Level = LogLevel.Information,
        Message = "Http Request Created: `{Method}` `{RelativePath}`.")]
    public static partial void RequestCreated(
        this ILogger<ApiCaller> logger, HttpMethod method, string relativePath);
    /// <summary>
    /// Log when an HTTP Request with content was created.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="method">The HTTP Method.</param>
    /// <param name="relativePath">The relative path.</param>
    /// <param name="content">The content in the request.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(RequestWithContentCreated),
        Level = LogLevel.Information,
        Message = "Http Request Created: `{Method}` `{RelativePath}` `{Content}`.")]
    public static partial void RequestWithContentCreated(
        this ILogger<ApiCaller> logger, HttpMethod method, string relativePath, HttpContent content);
    /// <summary>
    /// Log when failed to parse a result string.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="json">The JSON string that is parsed.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(JsonParsingFailed),
        Level = LogLevel.Information,
        Message = "JSON Parsing Failed: `{Json}`.")]
    public static partial void JsonParsingFailed(
        this ILogger<ApiCaller> logger, string json);
    /// <summary>
    /// Log when an error is received from the API call.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="errorCode">The error code received.</param>
    /// <param name="errorMessage">The error message received.</param>
    [LoggerMessage(
        EventId = 5,
        EventName = nameof(ErrorReceived),
        Level = LogLevel.Error,
        Message = "Error received from API call: `{ErrorCode}` `{ErrorMessage}`")]
    public static partial void ErrorReceived(
        this ILogger<ApiCaller> logger, int errorCode, string errorMessage);
}
