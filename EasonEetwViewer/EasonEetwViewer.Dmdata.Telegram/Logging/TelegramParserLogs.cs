using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Dmdata.Telegram.Services;

/// <summary>
/// Represents the log messages used in <see cref="TelegramParser"/>.
/// </summary>
internal static partial class TelegramParserLogs
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
        this ILogger<TelegramParser> logger);
    /// <summary>
    /// Log when handling JSON.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="json">The JSON data that is being parsed.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(HandlingJson),
        Level = LogLevel.Trace,
        Message = "Handling JSON: `{Json}`.")]
    public static partial void HandlingJson(
        this ILogger<TelegramParser> logger, string json);
    /// <summary>
    /// Log when JSON has incorrect format.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="json">The JSON data that is being parsed.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(IncorrectJsonFormat),
        Level = LogLevel.Error,
        Message = "Incorrect Format JSON: `{Json}`.")]
    public static partial void IncorrectJsonFormat(
        this ILogger<TelegramParser> logger, string json);
    /// <summary>
    /// Log when JSON has unsupported schema.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <param name="schemaVersion">The version of the schema.</param>
    [LoggerMessage(
        EventId = 3,
        EventName = nameof(UnsupportedSchema),
        Level = LogLevel.Error,
        Message = "Unsupported schema: `{SchemaName}` `{SchemaVersion}`.")]
    public static partial void UnsupportedSchema(
        this ILogger<TelegramParser> logger, string schemaName, string schemaVersion);
    /// <summary>
    /// Log when JSON has supported schema.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="schemaName">The name of the schema.</param>
    /// <param name="schemaVersion">The version of the schema.</param>
    [LoggerMessage(
        EventId = 4,
        EventName = nameof(SupportedSchema),
        Level = LogLevel.Information,
        Message = "Supported schema: `{SchemaName}` `{SchemaVersion}`.")]
    public static partial void SupportedSchema(
        this ILogger<TelegramParser> logger, string schemaName, string schemaVersion);
}
