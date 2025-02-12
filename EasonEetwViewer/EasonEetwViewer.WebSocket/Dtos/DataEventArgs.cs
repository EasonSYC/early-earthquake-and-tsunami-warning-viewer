using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;

namespace EasonEetwViewer.WebSocket.Dtos;
public sealed class DataEventArgs : EventArgs
{
    public required Head Telegram { get; init; }
}
