using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.ViewModels.ViewModelBases;
/// <summary>
/// Logs for <see cref="MainWindowViewModel"/>.
/// </summary>
internal static partial class MainWindowViewModelLogs
{
    /// <summary>
    /// Log when pane is triggered.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="isPaneOpen">The new state of the pane.</param>
    [LoggerMessage(
        EventId = 0,
        EventName = nameof(PaneTriggered),
        Level = LogLevel.Information,
        Message = "Pane triggered to state `{IsPaneOpen}`.")]
    public static partial void PaneTriggered(
        this ILogger<MainWindowViewModel> logger, bool isPaneOpen);
    /// <summary>
    /// Log when view model is switched.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="newViewModel">The new view model switched to.</param>
    [LoggerMessage(
        EventId = 1,
        EventName = nameof(ViewModelSwitched),
        Level = LogLevel.Debug,
        Message = "View model is switched to: `{NewViewModel}`.")]
    public static partial void ViewModelSwitched(
        this ILogger<MainWindowViewModel> logger, Type newViewModel);
    /// <summary>
    /// Log when switching to real time view model due to event received.
    /// </summary>
    /// <param name="logger">The logger to be used.</param>
    [LoggerMessage(
        EventId = 2,
        EventName = nameof(SwitchingToRealTime),
        Level = LogLevel.Debug,
        Message = "View model is switched to Realtime due to event received.")]
    public static partial void SwitchingToRealTime(
        this ILogger<MainWindowViewModel> logger);
}