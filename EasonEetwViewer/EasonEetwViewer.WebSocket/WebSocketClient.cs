using System.Data;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.WebSocket.Dto;

namespace EasonEetwViewer.WebSocket;

/// <summary>
/// Represents a WebSocket connection.
/// </summary>
public class WebSocketClient : IDisposable
{
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
    private ClientWebSocket _client;
    private Uri _serverUri;
    private CancellationTokenSource _tokenSource;
    private CancellationToken _token;



    public async Task ConnectWebSocketAsync(string webSocketUrl)
    {
        using ClientWebSocket client = new();
        Uri serverUri = new(webSocketUrl);

        await client.ConnectAsync(serverUri, CancellationToken.None);
        Console.WriteLine("Connected to the WebSocket server.");

        bool keepAlive = true;
        while (keepAlive) // Modified from https://stackoverflow.com/a/65761228, https://stackoverflow.com/a/63574016
        {
            byte[] receiveBuffer = new byte[32768]; // https://stackoverflow.com/a/41926694
            WebSocketReceiveResult result = await client.ReceiveAsync(
                new ArraySegment<byte>(receiveBuffer),
                CancellationToken.None);
            string responseBody = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);

            Response webSocketResponse = JsonSerializer.Deserialize<Response>(responseBody, _options) ?? throw new Exception();

            switch (webSocketResponse.Type)
            {
                case ResponseType.Start:
                    Console.WriteLine("This is a start response.");
                    break;

                case ResponseType.Ping:
                    PingResponse pingResponse = JsonSerializer.Deserialize<PingResponse>(responseBody, _options) ?? throw new Exception();
                    Console.WriteLine($"Ping Response: {pingResponse}");

                    PongRequest pongRequest = new()
                    {
                        PingId = pingResponse.PingId
                    };

                    string postDataJson = JsonSerializer.Serialize(pongRequest);
                    byte[] messageBuffer = Encoding.UTF8.GetBytes(postDataJson);

                    await client.SendAsync(
                        new ArraySegment<byte>(messageBuffer),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                    Console.WriteLine($"Sent: {pongRequest}");

                    break;

                case ResponseType.Pong:
                    PingResponse pongResponse = JsonSerializer.Deserialize<PingResponse>(responseBody, _options) ?? throw new Exception();
                    Console.WriteLine(value: $"Pong Response: {pongResponse}");
                    break;

                case ResponseType.Error:
                    ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody, _options) ?? throw new Exception();
                    Console.WriteLine(value: $"Error Response: {errorResponse}");

                    if (errorResponse.Close)
                    {
                        keepAlive = false;
                    }

                    break;

                case ResponseType.Data:
                    Console.WriteLine("This is a data response.");
                    break;

                case ResponseType.Unknown:
                default:
                    throw new Exception();
            }
        }

        await client.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "Closing",
                CancellationToken.None);
        Console.WriteLine("WebSocket connection closed.");
    }

    public WebSocketClient(string webSocketUrl)
    {
        _client = new();
        _serverUri = new(webSocketUrl);
        _tokenSource = new();
        _token = _tokenSource.Token;
    }

    public async Task ConnectAsync()
    {
        await _client.ConnectAsync(_serverUri, _token);
        Console.WriteLine("Connected to the WebSocket server.");
    }

    public void Dispose()
    {

    }

}