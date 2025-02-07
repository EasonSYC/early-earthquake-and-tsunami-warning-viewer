using EasonEetwViewer.KyoshinMonitor.Services;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.KyoshinMonitor;
/// <summary>
/// Represents the log messages used in <c>ImageFetch</c>.
/// </summary>
internal static partial class ImageFetchLogs
{
    /// <summary>
    /// Log when instantiated.
    /// </summary>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(Instantiated),
        Level = LogLevel.Information,
        Message = "Instantiated.")]
    public static partial void Instantiated(
        this ILogger<ImageFetch> logger);

    /// <summary>
    /// Log when sending a request.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="baseUri">The base URI of request.</param>
    /// <param name="relativeUri">The relative URI of request.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(SendingReqeust),
        Level = LogLevel.Trace,
        Message = "Sending HTTP Request to `{BaseUri}` and `{RelativeURi}`.")]
    public static partial void SendingReqeust(
        this ILogger<ImageFetch> logger, string baseUri, string relativeUri);

    /// <summary>
    /// Log when a request was successful.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(RequestSuccessful),
        Level = LogLevel.Debug,
        Message = "HTTP Request was successful.")]
    public static partial void RequestSuccessful(
        this ILogger<ImageFetch> logger);

    /// <summary>
    /// Log when a request was unsuccessful.
    /// </summary>
    /// <param name="logger">The logger that was called.</param>
    /// <param name="baseUri">The base URI of request.</param>
    /// <param name="relativeUri">The relative URI of request.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(RequestUnsuccessful),
        Level = LogLevel.Warning,
        Message = "HTTP Request to `{BaseUri}` and `{RelativeURi}` was unsuccessful.")]
    public static partial void RequestUnsuccessful(
        this ILogger<ImageFetch> logger, string baseUri, string relativeUri);
}
