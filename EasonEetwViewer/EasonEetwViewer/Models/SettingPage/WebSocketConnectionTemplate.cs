using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace EasonEetwViewer.Models.SettingPage;

internal partial class WebSocketConnectionTemplate : ObservableObject, IWebSocketConnectionTemplate
{
    public required int WebSocketId { get; init; }
    public required string ApplicationName { get; init; }
    public required DateTimeOffset StartTime { get; init; }
    [ObservableProperty]
    private bool _isEnabled = true;
    public required Func<Task<bool>> DisconnectTask { get; init; }

    [RelayCommand]
    private async Task Disconnect()
    {
        bool isSuccessful = await DisconnectTask();
        if (isSuccessful)
        {
            IsEnabled = false;
        }
    }
}
