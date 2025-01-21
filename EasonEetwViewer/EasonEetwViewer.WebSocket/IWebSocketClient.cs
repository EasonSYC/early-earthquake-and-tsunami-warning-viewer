﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.WebSocket;
public interface IWebSocketClient
{
    public Task ConnectAsync();
    public Task DisconnectAsync();
    public Task SendAsync<T>(T message);
}
