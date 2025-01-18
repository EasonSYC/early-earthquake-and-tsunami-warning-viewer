using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models;

internal partial class WebSocketConnectionTemplate : ObservableObject
{
    internal WebSocketConnectionTemplate(int connectionId, string appName, DateTime startTime, OnDisconnectAsync disconnectTask, bool isEnabled = true)
    {
        ConnectionId = connectionId;
        AppName = appName;
        StartTime = startTime;
        IsEnabled = isEnabled;
        _disconnectTask = disconnectTask;
    }

    internal int ConnectionId { get; private init; }
    internal string AppName { get; private init; }
    internal DateTime StartTime { get; private init; }

    [ObservableProperty]
    private bool _isEnabled;

    private readonly OnDisconnectAsync _disconnectTask;

    [RelayCommand]
    internal async Task Disconnect()
    {
        IsEnabled = false;
        await _disconnectTask(ConnectionId);
    }
}
