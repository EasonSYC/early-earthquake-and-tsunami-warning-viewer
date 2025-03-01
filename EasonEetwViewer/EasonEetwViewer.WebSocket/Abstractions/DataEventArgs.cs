using EasonEetwViewer.Dtos.Dto.JsonTelegram.TelegramBase;
namespace EasonEetwViewer.WebSocket.Abstractions;
/// <summary>
/// Represents the event arguments for a data being received.
/// </summary>
public sealed class DataEventArgs : EventArgs
{
    /// <summary>
    /// The telegram that is received.
    /// </summary>
    public required Head Telegram { get; init; }
}
