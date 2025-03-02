using CommunityToolkit.Mvvm.Input;
using EasonEetwViewer.Lang;

namespace EasonEetwViewer.Models.SettingPage;
/// <summary>
/// Describes an empty WebSocket connection.
/// </summary>
internal partial class EmptyWebSocketConnectionTemplate : IWebSocketConnectionTemplate
{
    /// <inheritdoc/>
    public int WebSocketId
        => -1;
    /// <inheritdoc/>
    public string ApplicationName
        => SettingPageResources.WebSocketListTextEmptyConnection;
    /// <inheritdoc/>
    public DateTimeOffset StartTime
        => new();
    /// <inheritdoc/>
    public bool IsEnabled
        => false;
    /// <inheritdoc cref="IWebSocketConnectionTemplate.DisconnectCommand"/>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    [RelayCommand]
    private static Task Disconnect()
        => Task.CompletedTask;
    /// <summary>
    /// The instance of <see cref="EmptyWebSocketConnectionTemplate"/>.
    /// </summary>
    public static EmptyWebSocketConnectionTemplate Instance
        => new();
    /// <summary>
    /// Initializes a new instance of the <see cref="EmptyWebSocketConnectionTemplate"/> class.
    /// </summary>
    private EmptyWebSocketConnectionTemplate() { }
}
