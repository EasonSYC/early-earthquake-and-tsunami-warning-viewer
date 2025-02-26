﻿using System.Diagnostics;
using EasonEetwViewer.Dtos.Dto.ApiResponse.Enum.WebSocket;

namespace EasonEetwViewer.Dtos.Caller.Extensions;
internal static class ConnectionStatusExtensions
{
    public static string ToUriString(this ConnectionStatus connectionStatus) => connectionStatus switch
    {
        ConnectionStatus.Waiting => "waiting",
        ConnectionStatus.Open => "open",
        ConnectionStatus.Closed => "closed",
        _ => throw new UnreachableException()
    };
}
