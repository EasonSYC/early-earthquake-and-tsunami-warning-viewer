using EasonEetwViewer.Dmdata.Dto.WebSocket;

namespace EasonEetwViewer.Dmdata.Caller.Interfaces;
public interface IWebSocketClient
{
    public OnDataReceived DataReceivedAction { get; set; }
    public bool IsWebSocketConnected { get; }
    public Task ConnectAsync(string webSocketUrl);
    public Task DisconnectAsync();
    public Task SendAsync<T>(T message);
}
