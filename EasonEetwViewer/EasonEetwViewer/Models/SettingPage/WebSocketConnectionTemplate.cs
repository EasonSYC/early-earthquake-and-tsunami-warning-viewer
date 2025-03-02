using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models.SettingPage;
/// <summary>
/// Describes a normal WebSocket connection.
/// </summary>
internal partial class WebSocketConnectionTemplate : ObservableObject, IWebSocketConnectionTemplate
{
    /// <inheritdoc/>
    public required int WebSocketId { get; init; }
    /// <inheritdoc/>
    public required string ApplicationName { get; init; }
    /// <inheritdoc/>
    public required DateTimeOffset StartTime { get; init; }
    /// <inheritdoc cref="IWebSocketConnectionTemplate.IsEnabled"/>
    [ObservableProperty]
    private bool _isEnabled = true;
    /// <summary>
    /// The action to execute when disconnecting the WebSocket.
    /// </summary>
    public required Func<Task<bool>> DisconnectTask { get; init; }
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
