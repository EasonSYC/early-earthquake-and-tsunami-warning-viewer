using System.Diagnostics;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.Schema;
using EasonEetwViewer.Dmdata.Dto.JsonTelegram.TelegramBase;
using EasonEetwViewer.WebSocket.Abstractions;
using EasonEetwViewer.WebSocket.Dtos;
using EasonEetwViewer.WebSocket.Dtos.Request;
using EasonEetwViewer.WebSocket.Dtos.Response;
using Microsoft.Extensions.Logging;

namespace EasonEetwViewer.WebSocket.Services;

/// <summary>
/// The default implementation of <see cref="IWebSocketClient"/>.
/// </summary>
public sealed class WebSocketClient : IWebSocketClient
{
    /// <summary>
    /// The JSON Serializer Options to be used.
    /// </summary>
    private readonly JsonSerializerOptions _options = new()
    {
        NumberHandling = JsonNumberHandling.AllowReadingFromString
    };
    /// <summary>
    /// The WebSocket client used for the connections.
    /// </summary>
    private ClientWebSocket _client;
    /// <summary>
    /// The cancellation token source.
    /// </summary>
    private CancellationTokenSource _tokenSource;
    /// <summary>
    /// The cancellation token associated with the cancellation token source.
    /// </summary>
    private CancellationToken _token;
    /// <summary>
    /// The queue of ping codes that have not yet received a response.
    /// </summary>
    private readonly Queue<string> _pingStrings;
    /// <summary>
    /// The time span between two user-initiated ping requests.
    /// </summary>
    private readonly TimeSpan _pingInterval = TimeSpan.FromSeconds(30);
    /// <summary>
    /// The time span between the disposal and the cancellation of the cancellation token source.
    /// </summary>
    private readonly TimeSpan _cancelWaitInterval = TimeSpan.FromSeconds(5);
    /// <summary>
    /// The time span between two checks of the number of unresponded ping requests.
    /// </summary>
    private readonly TimeSpan _checkPingInterval = TimeSpan.FromSeconds(15);
    private readonly ILogger<WebSocketClient> _logger;

    /// <summary>
    /// The receive buffer size.
    /// </summary>
    private const int _recieveBufferSize = 8192; // https://stackoverflow.com/a/41926694
    /// <summary>
    /// The maximum allowed client-initiated pings to be unresponded.
    /// </summary>
    private const int _maxPingUnresponded = 4;
    /// <summary>
    /// The allowed characters when generating a Ping Id.
    /// </summary>
    private const string _allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    /// <summary>
    /// The length of a Ping Id.
    /// </summary>
    private const int _pingIdLength = 4;

    /// <inheritdoc/>
    public event EventHandler<DataEventArgs>? DataReceived;
    /// <inheritdoc/>
    public event EventHandler<EventArgs>? WebSocketStatusChanged;
    /// <inheritdoc/>
    public bool IsWebSocketConnected
        => _client.State is WebSocketState.Open;

    /// <summary>
    /// A task that sends ping requests to the WebSocket server with interval <c>_pingInterval</c>, until the token has been cancelled.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
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
            Debug.WriteLine("Ping request sent!");
            await Task.Delay(_pingInterval);
        }
    }

    /// <summary>
    /// A task that checks number of unresponded ping requests with interval <c>_checkPingInterval</c>, until the token has been cancelled.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
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
    /// <summary>
    /// A task that receives message from the WebSocket connection and deals with them as appropriate.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    /// <exception cref="Exception">I don't know when this will be thrown honestly.</exception>
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

                if (!result.EndOfMessage)
                {
                    throw new Exception();
                }

                string responseBody = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);

                ResponseBase webSocketResponse = JsonSerializer.Deserialize<ResponseBase>(responseBody, _options) ?? throw new FormatException();
                Debug.WriteLine($"Response received: {responseBody}");

                switch (webSocketResponse.Type)
                {
                    case ResponseType.Start:
                        StartResponse startResponse = JsonSerializer.Deserialize<StartResponse>(responseBody, _options) ?? throw new FormatException();
                        Debug.WriteLine(startResponse);

                        break;

                    case ResponseType.Ping:
                        PingResponse pingResponse = JsonSerializer.Deserialize<PingResponse>(responseBody, _options) ?? throw new FormatException();

                        PongRequest pongRequest = new()
                        {
                            PingId = pingResponse.PingId
                        };

                        await SendAsync(pongRequest);

                        break;

                    case ResponseType.Pong:
                        PongResponse pongResponse = JsonSerializer.Deserialize<PongResponse>(responseBody, _options) ?? throw new FormatException();

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
                        ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody, _options) ?? throw new FormatException();
                        Debug.WriteLine(value: $"Error Response: {errorResponse}");

                        if (errorResponse.Close)
                        {
                            await DisconnectAsync();
                        }

                        break;

                    case ResponseType.Data:
                        DataResponse dataResponse = JsonSerializer.Deserialize<DataResponse>(responseBody, _options) ?? throw new FormatException();
                        Debug.WriteLine("This is a data response.");

                        string bodyString = dataResponse.Body;
                        string finalString;

                        switch (dataResponse.Encoding)
                        {
                            case EncodingType.Base64:
                                byte[] bytes = Convert.FromBase64String(bodyString);
                                using (MemoryStream stream = new(bytes))
                                {
                                    switch (dataResponse.Compression)
                                    {
                                        case CompressionType.Gzip:
                                            using (GZipStream gZipStream = new(stream, CompressionMode.Decompress))
                                            {
                                                using MemoryStream outputStream = new();
                                                gZipStream.CopyTo(outputStream);
                                                byte[] uncompressed = outputStream.ToArray();
                                                finalString = Encoding.UTF8.GetString(uncompressed);
                                            }

                                            break;

                                        case CompressionType.Zip:
                                            throw new NotImplementedException();

                                        case CompressionType.Unknown:
                                        case null:
                                        default:
                                            throw new FormatException();
                                    }
                                }

                                break;

                            case EncodingType.Utf8:
                            case null:
                                finalString = bodyString;
                                break;

                            case EncodingType.Unknown:
                            default:
                                throw new FormatException();
                        }

                        switch (dataResponse.Format)
                        {
                            case FormatType.Json:
                                Head headData = JsonSerializer.Deserialize<Head>(finalString);
                                if (headData.Status is not Status.Normal)
                                {
                                    break;
                                }

                                if (headData.Schema.Type == "eew-information" && headData.Schema.Version == "1.0.0")
                                {
                                    EewInformationSchema eew = JsonSerializer.Deserialize<EewInformationSchema>(finalString, _options);
                                    DataReceived?.Invoke(this, new() { Telegram = eew } );
                                    break;
                                }

                                if (headData.Schema.Type == "tsunami-information" && headData.Schema.Version == "1.0.0")
                                {
                                    TsunamiInformationSchema tsunami = JsonSerializer.Deserialize<TsunamiInformationSchema>(finalString, _options);
                                    DataReceived?.Invoke(this, new() { Telegram = tsunami });
                                    break;
                                }

                                break;
                            case FormatType.Xml:
                            case FormatType.AlphaNumeric:
                            case FormatType.Binary:
                                break;
                            case FormatType.Unknown:
                            case null:
                            default:
                                break;
                        }

                        break;

                    case ResponseType.Unknown:
                    default:
                        throw new FormatException();
                }
            }
        }
        catch (TaskCanceledException)
        {
            Debug.WriteLine("Task is cancelled!");
        }
    }

    /// <summary>
    /// Creates a new instance of the class with the given parameters.
    /// </summary>>
    /// <param name="logger">The logger to be used.</param>
    public WebSocketClient(ILogger<WebSocketClient> logger)
    {
        _client = new();
        _pingStrings = new();
        _tokenSource = new();
        _token = _tokenSource.Token;
        _logger = logger;
        _logger.Instantiated();
    }
    /// <inheritdoc/>
    public async Task ConnectAsync(Uri webSocketUrl)
    {
        if (_client.State == WebSocketState.Open)
        {
            await DisconnectAsync();
        }

        _logger.Connecting(webSocketUrl);
        await _client.ConnectAsync(webSocketUrl, _token);
        _ = await Task.Factory.StartNew(ReceiveLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(SendPingLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        _ = await Task.Factory.StartNew(CheckPingLoop, _token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        WebSocketStatusChanged?.Invoke(this, EventArgs.Empty);
        _logger.Connected(webSocketUrl);
    }
    /// <inheritdoc/>
    public async Task DisconnectAsync()
    {
        if (_client.State == WebSocketState.Closed)
        {
            return;
        }

        _logger.Disconnecting();
        _tokenSource.CancelAfter(_cancelWaitInterval);
        await _client.CloseAsync(
                WebSocketCloseStatus.NormalClosure,
                "WebSocket is closing.",
                _token);

        _tokenSource = new();
        _token = _tokenSource.Token;
        _pingStrings.Clear();
        _client.Dispose();
        _client = new ClientWebSocket();
        WebSocketStatusChanged?.Invoke(this, EventArgs.Empty);
        _logger.Disconnected();
    }
    /// <summary>
    /// Sends the specified message to the client WebSocket.
    /// </summary>
    /// <typeparam name="T">The type of the message to be sent.</typeparam>
    /// <param name="message">The message to be sent.</param>
    /// <returns>A <see cref="Task"/> object that represents the asynchronous operation.</returns>
    private async Task SendAsync<T>(T message)
    {
        string dataJson = JsonSerializer.Serialize(message);
        byte[] messageBuffer = Encoding.UTF8.GetBytes(dataJson);

        _logger.Sending(dataJson);
        await _client.SendAsync(
            new ArraySegment<byte>(messageBuffer),
            WebSocketMessageType.Text,
            true,
            _token);

        _logger.Sent(dataJson);
    }
}