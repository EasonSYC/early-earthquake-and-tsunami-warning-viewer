using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.WebSocket;

namespace EasonEetwViewer.Data;

public class WebSocketClient
{
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
    public async Task ConnectWebSocketAsync(string webSocketUrl)
    {
        using ClientWebSocket client = new();
        try
        {
            Uri serverUri = new(webSocketUrl);

            await client.ConnectAsync(serverUri, CancellationToken.None);
            Console.WriteLine("Connected to the WebSocket server.");

            bool keepAlive = true;
            while (keepAlive)
            {
                byte[] receiveBuffer = new byte[32768];
                WebSocketReceiveResult result = await client.ReceiveAsync(
                    new ArraySegment<byte>(receiveBuffer),
                    CancellationToken.None);
                string responseBody = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);

                WebSocketResponse webSocketResponse = JsonSerializer.Deserialize<WebSocketResponse>(responseBody, _options) ?? throw new Exception();

                switch (webSocketResponse.Type)
                {
                    case WebSocketResponseType.Start:
                        Console.WriteLine("This is a start response.");
                        break;

                    case WebSocketResponseType.Ping:
                        WebSocketPingResponse pingResponse = JsonSerializer.Deserialize<WebSocketPingResponse>(responseBody, _options) ?? throw new Exception();
                        Console.WriteLine($"Ping Response: {pingResponse}");

                        WebSocketPongRequest pongRequest = new()
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

                    case WebSocketResponseType.Pong:
                        WebSocketPingResponse pongResponse = JsonSerializer.Deserialize<WebSocketPingResponse>(responseBody, _options) ?? throw new Exception();
                        Console.WriteLine(value: $"Pong Response: {pongResponse}");
                        break;

                    case WebSocketResponseType.Error:
                        WebSocketErrorResponse errorResponse = JsonSerializer.Deserialize<WebSocketErrorResponse>(responseBody, _options) ?? throw new Exception();
                        Console.WriteLine(value: $"Error Response: {errorResponse}");

                        if (errorResponse.Close)
                        {
                            keepAlive = false;
                        }

                        break;

                    case WebSocketResponseType.Data:
                        Console.WriteLine("This is a data response.");
                        break;

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
        catch (WebSocketException wse)
        {
            Console.WriteLine($"WebSocketException: {wse.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");

        }
    }
}