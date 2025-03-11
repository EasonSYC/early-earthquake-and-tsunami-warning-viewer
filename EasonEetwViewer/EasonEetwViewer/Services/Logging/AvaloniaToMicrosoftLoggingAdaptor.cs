using Avalonia.Logging;
using EasonEetwViewer.Extensions;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.Services.Logging;

/// <summary>
/// Adapts Avalonia logging <see cref="ILogSink"/> to Microsoft logging <see cref="ILogger"/>.
/// </summary>
/// <param name="logger">The Microsoft <see cref="ILogger"/> to be used.</param>
internal class AvaloniaToMicrosoftLoggingAdaptor(ILogger<AvaloniaToMicrosoftLoggingAdaptor> logger) : ILogSink
{
    /// <summary>
    /// The logger to be used.
    /// </summary>
    private readonly ILogger<AvaloniaToMicrosoftLoggingAdaptor> _logger = logger;

    /// <inheritdoc/>
    public bool IsEnabled(LogEventLevel level, string area)
        => _logger.IsEnabled(level.ToMicrosoftLogLevel());

    /// <inheritdoc/>
    public void Log(LogEventLevel level, string area, object? source, string messageTemplate)
    {
        if (IsEnabled(level, area))
        {
            _logger.Log(
                level.ToMicrosoftLogLevel(),
                "[{Area}] {Source} : {MessageTemplate}",
                area,
                source,
                messageTemplate);
        }
    }

    /// <inheritdoc/>
    public void Log(LogEventLevel level, string area, object? source, string messageTemplate, params object?[] propertyValues)
    {
        if (IsEnabled(level, area))
        {
            _logger.Log(
                level.ToMicrosoftLogLevel(),
                "[{Area}] {Source} : {MessageTemplate} {PropertyValues}",
                area,
                source,
                messageTemplate,
                propertyValues);
        }
    }
}
