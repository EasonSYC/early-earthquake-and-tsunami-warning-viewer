using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

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
