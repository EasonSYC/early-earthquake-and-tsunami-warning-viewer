namespace EasonEetwViewer.Dmdata.WebSocket.Events;

/// <summary>
/// The event arugments for the status changed event.
/// </summary>
public class StatusChangedEventArgs : EventArgs
{
    /// <summary>
    /// Whether the WebSocket is connected or not.
    /// </summary>
    public required bool IsConnected { get; init; }
}
