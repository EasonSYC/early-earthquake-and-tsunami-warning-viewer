using System.Diagnostics;
using EasonEetwViewer.Dmdata.Api.Dtos.Enum.WebSocket;

namespace EasonEetwViewer.Dmdata.Api.Extensions;
/// <summary>
/// Provides extensions for <see cref="ConnectionStatus"/> to convert to URI string to be used in API calls.
/// </summary>
internal static class ConnectionStatusExtensions
{
    /// <summary>
    /// Converts the <see cref="ConnectionStatus"/> to a URI string.
    /// </summary>
    /// <param name="connectionStatus">The connection status to be converted.</param>
    /// <returns>The URI string for the connection status</returns>
    /// <exception cref="UnreachableException">When the code reaches an unreachable state.</exception>
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
