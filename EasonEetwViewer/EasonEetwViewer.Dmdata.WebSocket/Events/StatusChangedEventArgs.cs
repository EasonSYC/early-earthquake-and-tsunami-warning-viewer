using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasonEetwViewer.WebSocket.Events
{
    /// <summary>
    /// The event arugments for the status changed event.
    /// </summary>
    public class StatusChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Whether the WebSocket is connected or not.
        /// </summary>
        public required bool IsConnected { get; init; }
    }
}
