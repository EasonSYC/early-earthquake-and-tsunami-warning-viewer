using System.Diagnostics;
using EasonEetwViewer.Api.Dtos.Enum.WebSocket;

namespace EasonEetwViewer.Api.Extensions;
internal static class ConnectionStatusExtensions
{
    public static string ToUriString(this ConnectionStatus connectionStatus)
        => connectionStatus switch
        {
            ConnectionStatus.Waiting
                => "waiting",
            ConnectionStatus.Open
                => "open",
            ConnectionStatus.Closed
                => "closed",
            _
                => throw new UnreachableException()
        };
}
