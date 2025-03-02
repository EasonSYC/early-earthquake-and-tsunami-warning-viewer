using EasonEetwViewer.Dmdata.Telegram.Dtos.TelegramBase;
namespace EasonEetwViewer.WebSocket.Events;
/// <summary>
/// Represents the event arguments for a data being received.
/// </summary>
public sealed class DataReceivedEventArgs : EventArgs
{
    /// <summary>
    /// The telegram that is received.
    /// </summary>
    public required Head Telegram { get; init; }
}
