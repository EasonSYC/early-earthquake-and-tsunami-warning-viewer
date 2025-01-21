namespace EasonEetwViewer.WebSocket;
public interface IWebSocketClient
{
    public Task ConnectAsync();
    public Task DisconnectAsync();
    public Task SendAsync<T>(T message);
}
