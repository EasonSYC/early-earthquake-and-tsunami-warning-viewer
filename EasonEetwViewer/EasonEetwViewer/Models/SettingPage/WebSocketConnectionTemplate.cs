using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models;

internal partial class WebSocketConnectionTemplate : ObservableObject
{
    internal WebSocketConnectionTemplate(int connectionId, Func<string> appName, DateTimeOffset startTime, Func<int, Task> disconnectTask, bool isEnabled = true)
    {
        ConnectionId = connectionId;
        AppName = appName;
        StartTime = startTime;
        IsEnabled = isEnabled;
        _disconnectTask = disconnectTask;
    }

    internal int ConnectionId { get; private init; }
    internal Func<string> AppName { get; private init; }
    internal DateTimeOffset StartTime { get; private init; }

    [ObservableProperty]
    private bool _isEnabled;

    private readonly Func<int, Task> _disconnectTask;

    [RelayCommand]
    internal async Task Disconnect()
    {
        IsEnabled = false;
        await _disconnectTask(ConnectionId);
    }
}
