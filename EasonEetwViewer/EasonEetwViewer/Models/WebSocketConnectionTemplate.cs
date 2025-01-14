using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models;

internal partial class WebSocketConnectionTemplate : ObservableObject
{
    internal static WebSocketConnectionTemplate EmptyConnection => new();

    internal WebSocketConnectionTemplate(int connectionId, string appName, DateTime startTime, OnDisconnectAsync disconnectTask)
    {
        ConnectionId = connectionId;
        AppName = appName;
        StartTime = startTime;
        _disconnectTask = disconnectTask;
        IsEnabled = true;
    }
    private WebSocketConnectionTemplate()
    {
        ConnectionId = -1;
        AppName = "(Empty Connection)";
        StartTime = new();
        _disconnectTask = (x) => Task.CompletedTask;
        IsEnabled = false;
    }
    internal int ConnectionId { get; private init; }
    internal string AppName { get; private init; }
    internal DateTime StartTime { get; private init; }

    [ObservableProperty]
    private bool _isEnabled;

    private OnDisconnectAsync _disconnectTask;

    [RelayCommand]
    internal async Task Disconnect()
    {
        IsEnabled = false;
        await _disconnectTask(ConnectionId);
    }
}
