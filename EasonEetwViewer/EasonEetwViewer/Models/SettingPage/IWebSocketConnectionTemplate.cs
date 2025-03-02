using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models.SettingPage;

internal interface IWebSocketConnectionTemplate
{
    int WebSocketId { get; }
    string ApplicationName { get; }
    DateTimeOffset StartTime { get; }
    bool IsEnabled { get; }
    IAsyncRelayCommand DisconnectCommand { get; }
}
