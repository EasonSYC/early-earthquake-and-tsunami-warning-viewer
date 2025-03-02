using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Models.SettingPage;

internal partial class EmptyWebSocketConnectionTemplate : IWebSocketConnectionTemplate
{
    public int WebSocketId
        => -1;
    public string ApplicationName
        => SettingPageResources.WebSocketEmptyConnectionName;
    public DateTimeOffset StartTime
        => new();
    public bool IsEnabled
        => false;
    [RelayCommand]
    private Task Disconnect()
        => Task.CompletedTask;

    public static EmptyWebSocketConnectionTemplate Instance
        => new();
    private EmptyWebSocketConnectionTemplate() { }
}
