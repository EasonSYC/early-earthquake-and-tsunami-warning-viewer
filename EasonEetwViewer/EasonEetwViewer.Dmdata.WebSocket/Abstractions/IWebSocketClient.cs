using EasonEetwViewer.WebSocket.Events;

namespace EasonEetwViewer.WebSocket.Abstractions;

/// <summary>
/// Represents a WebSocket connection to <see href="dmdata.jp"/>.
/// </summary>
public interface IWebSocketClient
{
    /// <summary>
    /// When data is received by the WebSocket connection.
    /// </summary>
    event EventHandler<DataReceivedEventArgs>? DataReceived;
    /// <summary>
    /// When the status of the WebSocket connection changes.
    /// </summary>
    event EventHandler<StatusChangedEventArgs>? StatusChanged;
    /// <summary>
    /// Indicates if the WebSocket is connected.
    /// </summary>
    bool IsWebSocketConnected { get; }
    /// <summary>
    /// Connect to the speficied URL for the WebSocket
    /// </summary>
    /// <param name="webSocketUrl">The URL of the WebSocket to connect to.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task ConnectAsync(Uri webSocketUrl);
    /// <summary>
    /// Disconnect from the current WebSocket connection.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    Task DisconnectAsync();
}
