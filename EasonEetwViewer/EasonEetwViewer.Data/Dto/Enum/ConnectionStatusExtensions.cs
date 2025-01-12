using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;
internal static class ConnectionStatusExtensions
{
    public static string ToUriString(this ConnectionStatus connectionStatus) => connectionStatus switch
    {
        ConnectionStatus.Waiting => "waiting",
        ConnectionStatus.Open => "open",
        ConnectionStatus.Closed => "closed",
        _ => "unknown"
    };
}
