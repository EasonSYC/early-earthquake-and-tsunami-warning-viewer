using EasonEetwViewer.WebSocket.Dtos;

namespace EasonEetwViewer.WebSocket.Abstractions;

/// <summary>
/// Represents a WebSocket connection to <see href="dmdata.jp"/>.
/// </summary>
public interface IWebSocketClient
{
    /// <summary>
    /// When data is received by the WebSocket connection.
    /// </summary>
    public event EventHandler<DataEventArgs>? DataReceived;
    /// <summary>
    /// When the status of the WebSocket connection changes.
    /// </summary>
    public event EventHandler<EventArgs>? WebSocketStatusChanged;
    /// <summary>
    /// Indicates if the WebSocket is connected.
    /// </summary>
    public bool IsWebSocketConnected { get; }
    /// <summary>
    /// Connect to the speficied URL for the WebSocket
    /// </summary>
    /// <param name="webSocketUrl">The URL of the WebSocket to connect to.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public Task ConnectAsync(Uri webSocketUrl);
    /// <summary>
    /// Disconnect from the current WebSocket connection.
    /// </summary>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    public Task DisconnectAsync();
}
