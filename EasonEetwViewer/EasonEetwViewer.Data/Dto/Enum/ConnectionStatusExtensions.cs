namespace EasonEetwViewer.HttpRequest.Dto.Enum;
internal static class ConnectionStatusExtensions
{
    public static string ToUriString(this ConnectionStatus connectionStatus) => connectionStatus switch
    {
        ConnectionStatus.Waiting => "waiting",
        ConnectionStatus.Open => "open",
        ConnectionStatus.Closed => "closed",
        ConnectionStatus.Unknown or _ => "unknown"
    };
}
