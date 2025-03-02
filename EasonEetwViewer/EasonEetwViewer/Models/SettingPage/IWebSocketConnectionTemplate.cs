using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models.SettingPage;
/// <summary>
/// Describes a template for a WebSocket connection.
/// </summary>
internal interface IWebSocketConnectionTemplate
{
    /// <summary>
    /// The WebSocket connection ID.
    /// </summary>
    int WebSocketId { get; }
    /// <summary>
    /// The name of the application.
    /// </summary>
    string ApplicationName { get; }
    /// <summary>
    /// The start time of the connection.
    /// </summary>
    DateTimeOffset StartTime { get; }
    /// <summary>
    /// Whether the disconnect button is enabled.
    /// </summary>
    bool IsEnabled { get; }
    /// <summary>
    /// The async relay command to disconnect the WebSocket connection.
    /// </summary>
    IAsyncRelayCommand DisconnectCommand { get; }
}
