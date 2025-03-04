using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Dmdata.Api.Dtos.Record.WebSocket;

namespace EasonEetwViewer.Models.SettingPage;
/// <summary>
/// Describes a normal WebSocket connection.
/// </summary>
/// <param name="disconnectTask">The action to execute when disconnecting the WebSocket.</param>
/// <param name="webSocket">The WebSocket connection details.</param>
internal partial class WebSocketConnectionTemplate(WebSocketDetails webSocket, Func<Task<bool>> disconnectTask) : ObservableObject, IWebSocketConnectionTemplate
{
    /// <inheritdoc/>
    public int WebSocketId { get; private init; } = webSocket.WebSocketId;
    /// <inheritdoc/>
    public string? ApplicationName { get; private init; } = webSocket.ApplicationName;
    /// <inheritdoc/>
    public DateTimeOffset StartTime { get; private init; } = webSocket.StartTime;
    /// <inheritdoc cref="IWebSocketConnectionTemplate.IsEnabled"/>
    [ObservableProperty]
    private bool _isEnabled = true;
    /// <summary>
    /// The action to execute when disconnecting the WebSocket.
    /// </summary>
    public Func<Task<bool>> DisconnectTask { get; private init; } = disconnectTask;
    /// <inheritdoc cref="IWebSocketConnectionTemplate.DisconnectCommand"/>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    [RelayCommand]
    private async Task Disconnect()
    {
        bool isSuccessful = await DisconnectTask();
        if (isSuccessful)
        {
            IsEnabled = false;
        }
    }
}
