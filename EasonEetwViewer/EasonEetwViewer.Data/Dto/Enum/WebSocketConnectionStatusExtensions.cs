namespace EasonEetwViewer.HttpRequest.Dto.Enum;
internal static class WebSocketConnectionStatusExtensions
{
    public static string ToUriString(this WebSocketConnectionStatus connectionStatus) => connectionStatus switch
    {
        WebSocketConnectionStatus.Waiting => "waiting",
        WebSocketConnectionStatus.Open => "open",
        WebSocketConnectionStatus.Closed => "closed",
        WebSocketConnectionStatus.Unknown or _ => "unknown"
    };
}
