using System.Net.WebSockets;
using System.Security.Cryptography;
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
    private readonly ClientWebSocket _client;
    private readonly Uri _serverUri;
    private readonly CancellationTokenSource _tokenSource;
    private readonly CancellationToken _token;
    private readonly Queue<string> _pingStrings;
    private readonly TimeSpan _pingInterval = TimeSpan.FromSeconds(30);
    private readonly TimeSpan _cancelWaitInterval = TimeSpan.FromSeconds(5);
    private readonly TimeSpan _checkPingInterval = TimeSpan.FromSeconds(15);
    private bool _isDisposed;
    private const int _recieveBufferSize = 32768; // https://stackoverflow.com/a/41926694
    private const int _maxPingUnresponded = 4;
    /// <summary>
    /// The allowed characters when generating a Ping Id.
    /// </summary>
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    /// <summary>
    /// The length of a Ping Id.
    /// </summary>
    private const int _pingIdLength = 4;

    private async Task SendPingLoop()
    {
        while (!_token.IsCancellationRequested)
        {
            PingRequest pingRequest = new()
            {
                PingId = RandomNumberGenerator.GetString(_allowedChars, _pingIdLength)
            };
            _pingStrings.Enqueue(pingRequest.PingId);
            await SendAsync(pingRequest);
            Console.WriteLine("Ping request sent!");

            await Task.Delay(_pingInterval);
        }
    }
    private async Task CheckPingLoop()
    {
        while (!_token.IsCancellationRequested)
        {
            if (_pingStrings.Count > _maxPingUnresponded)
            {
                await DisconnectAsync();
            }

            await Task.Delay(_checkPingInterval);
        }
    }
    private async Task ReceiveLoop()
    {
        try
        {
            byte[] receiveBuffer = new byte[_recieveBufferSize];

            // Modified from https://stackoverflow.com/a/65761228, https://stackoverflow.com/a/63574016
            while (_client.State == WebSocketState.Open && !_token.IsCancellationRequested)
            {
                WebSocketReceiveResult result = await _client.ReceiveAsync(
                new ArraySegment<byte>(receiveBuffer),
                _token);

                string responseBody = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);

                Response webSocketResponse = JsonSerializer.Deserialize<Response>(responseBody, _options) ?? throw new Exception();
                Console.WriteLine($"Response received: {responseBody}");

                switch (webSocketResponse.Type)
                {
                    case ResponseType.Start:
                        Console.WriteLine("Start response received!");
                        break;

                    case ResponseType.Ping:
                        PingResponse pingResponse = JsonSerializer.Deserialize<PingResponse>(responseBody, _options) ?? throw new Exception();

                        PongRequest pongRequest = new()
                        {
                            PingId = pingResponse.PingId
                        };

                        await SendAsync(pongRequest);

                        break;

                    case ResponseType.Pong:
                        PongResponse pongResponse = JsonSerializer.Deserialize<PongResponse>(responseBody, _options) ?? throw new Exception();

                        if (_pingStrings.Contains(pongResponse.PingId!))
                        {
                            string lastDequeue;
                            do
                            {
                                lastDequeue = _pingStrings.Dequeue();
                            } while (lastDequeue != pongResponse.PingId);
                        }

                        break;

                    case ResponseType.Error:
                        ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody, _options) ?? throw new Exception();
                        Console.WriteLine(value: $"Error Response: {errorResponse}");

                        if (errorResponse.Close)
                        {
                            await DisconnectAsync();
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
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Task is cancelled!");
        }
    }

    public WebSocketClient(string webSocketUrl)
    {
        _client = new();
        _serverUri = new(webSocketUrl);
        _tokenSource = new();
        _token = _tokenSource.Token;
        _isDisposed = false;
        _pingStrings = new();
    }

    public async Task ConnectAsync()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        if (_client.State == WebSocketState.Open)
        {
            return;
        }

        await _client.ConnectAsync(_serverUri, _token);
        _ = await Task.Factory.StartNew(ReceiveLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(SendPingLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(CheckPingLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }

    public async Task DisconnectAsync()
    {
        ObjectDisposedException.ThrowIf(_isDisposed, this);

        Dispose();
    }

    public async Task SendAsync<T>(T message)
    {
        string dataJson = JsonSerializer.Serialize(message);
        byte[] messageBuffer = Encoding.UTF8.GetBytes(dataJson);

        await _client.SendAsync(
            new ArraySegment<byte>(messageBuffer),
            WebSocketMessageType.Text,
            true,
            _token);

        Console.WriteLine($"Sent: {dataJson}");
    }

    public void Dispose()
    {
        Console.WriteLine("Called Dispose!");

        if (_client.State != WebSocketState.Closed)
        {
            _tokenSource.CancelAfter(_cancelWaitInterval);
            _client.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Closing",
                    _token).Wait();
        }

        _client.Dispose();
        _tokenSource.Dispose();

        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}