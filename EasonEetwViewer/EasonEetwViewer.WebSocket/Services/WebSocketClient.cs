using System.Diagnostics;
using System.IO.Compression;
using System.Net.WebSockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using EasonEetwViewer.Dtos.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Telegram.Dtos.Schema;
using EasonEetwViewer.Telegram.Dtos.TelegramBase;
using EasonEetwViewer.WebSocket.Abstractions;
using EasonEetwViewer.WebSocket.Dtos;
using EasonEetwViewer.WebSocket.Dtos.Data;
using EasonEetwViewer.WebSocket.Dtos.Request;
using EasonEetwViewer.WebSocket.Dtos.Response;
using EasonEetwViewer.WebSocket.Exceptions;
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
    private readonly JsonSerializerOptions _options;
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
    /// <summary>
    /// The logger to be used.
    /// </summary>
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
    public event EventHandler<EventArgs>? StatusChanged;
    /// <inheritdoc/>
    public bool IsWebSocketConnected
        => _client.State is WebSocketState.Open;

    /// <summary>
    /// A task that sends ping requests to the WebSocket server with interval <see cref="_pingInterval"/>, until the token has been cancelled.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private async Task SendPingLoop()
    {
        try
        {
            while (!_token.IsCancellationRequested)
            {
                PingRequest pingRequest = new()
                {
                    PingId = RandomNumberGenerator.GetString(_allowedChars, _pingIdLength)
                };
                _pingStrings.Enqueue(pingRequest.PingId);
                _logger.SendingPingRequest(pingRequest.PingId);
                await SendAsync(pingRequest);
                await Task.Delay(_pingInterval);
            }
        }
        catch (Exception ex)
        {
            _logger.UnexpectedException(ex.ToString());
            await DisconnectAsync();
        }
    }

    /// <summary>
    /// A task that checks number of unresponded ping requests with interval <see cref="_checkPingInterval"/>, until the token has been cancelled.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private async Task CheckPingLoop()
    {
        try
        {
            while (!_token.IsCancellationRequested)
            {
                if (_pingStrings.Count > _maxPingUnresponded)
                {
                    _logger.UnreceivedPingExceeding();
                    await DisconnectAsync();
                }

                await Task.Delay(_checkPingInterval);
            }
        }
        catch (Exception ex)
        {
            _logger.UnexpectedException(ex.ToString());
            await DisconnectAsync();
        }
    }

    /// <summary>
    /// A task that receives message from the WebSocket connection and deals with them as appropriate.
    /// </summary>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    private async Task ReceiveLoop()
    {
        try
        {
            byte[] receiveBuffer = new byte[_recieveBufferSize];

            // Modified from https://stackoverflow.com/a/65761228, https://stackoverflow.com/a/63574016
            while (_client.State is WebSocketState.Open && !_token.IsCancellationRequested)
            {
                WebSocketReceiveResult result = await _client.ReceiveAsync(
                new ArraySegment<byte>(receiveBuffer),
                _token);

                if (!result.EndOfMessage)
                {
                    _logger.UnsupportedFormat();
                }
                else
                {
                    string responseBody = Encoding.UTF8.GetString(receiveBuffer, 0, result.Count);
                    _logger.DataReveiced(responseBody);
                    await HandleResponse(responseBody);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.UnexpectedException(ex.ToString());
            await DisconnectAsync();
        }
    }

    /// <summary>
    /// Handles the response received from the WebSocket.
    /// </summary>
    /// <param name="responseBody">The response body received from the WebSocket</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
    /// <exception cref="UnreachableException">When reaching a code that is unreachable.</exception>
    private async Task HandleResponse(string responseBody)
    {
        try
        {
            ResponseBase webSocketResponse = JsonSerializer.Deserialize<ResponseBase>(responseBody, _options)
                ?? throw new WebSocketClientFormatException();
            switch (webSocketResponse.Type)
            {
                case MessageType.Start:
                    StartResponse startResponse = JsonSerializer.Deserialize<StartResponse>(responseBody, _options)
                        ?? throw new WebSocketClientFormatException();
                    break;

                case MessageType.Ping:
                    PingResponse pingResponse = JsonSerializer.Deserialize<PingResponse>(responseBody, _options)
                        ?? throw new WebSocketClientFormatException();

                    PongRequest pongRequest = new()
                    {
                        PingId = pingResponse.PingId
                    };
                    _logger.ReturningPong(pingResponse.PingId);
                    await SendAsync(pongRequest);

                    break;

                case MessageType.Pong:
                    PongResponse pongResponse = JsonSerializer.Deserialize<PongResponse>(responseBody, _options)
                        ?? throw new WebSocketClientFormatException();

                    if (pongResponse.PingId is not null)
                    {
                        _logger.RemovingPing(pongResponse.PingId);
                        RemovePings(pongResponse.PingId);
                    }

                    break;

                case MessageType.Error:
                    ErrorResponse errorResponse = JsonSerializer.Deserialize<ErrorResponse>(responseBody, _options)
                        ?? throw new WebSocketClientFormatException();

                    if (errorResponse.Close)
                    {
                        _logger.ErrorReceivedAndClosed(errorResponse.Error);
                        await DisconnectAsync();
                    }
                    else
                    {
                        _logger.ErrorReceived(errorResponse.Error);
                    }

                    break;

                case MessageType.Data:
                    DataResponse dataResponse = JsonSerializer.Deserialize<DataResponse>(responseBody, _options)
                        ?? throw new WebSocketClientFormatException();

                    _logger.HandlingDataResponse();
                    HandleDataResponse(dataResponse);

                    break;

                default:
                    throw new UnreachableException();
            }
        }
        catch (JsonException)
        {
            _logger.IncorrectFormat(responseBody);
        }
        catch (WebSocketClientFormatException)
        {
            _logger.IncorrectFormat(responseBody);
        }
        catch (WebSocketClientUnsupportedException)
        {
            _logger.UnsupportedFormat();
        }
    }

    /// <summary>
    /// Removes the ping IDs before the specified ping ID.
    /// </summary>
    /// <param name="pingId">The ping ID to be removed until.</param>
    private void RemovePings(string pingId)
    {
        if (_pingStrings.Contains(pingId))
        {
            string lastDequeue;
            do
            {
                lastDequeue = _pingStrings.Dequeue();
            } while (lastDequeue != pingId);
        }
    }

    /// <summary>
    /// Handles the data response received from the WebSocket.
    /// </summary>
    /// <param name="dataResponse">The data response to be handled.</param>
    /// <exception cref="UnreachableException">When reaching a code that is unreachable.</exception>
    /// <exception cref="WebSocketClientUnsupportedException">When the received data type is not supported.</exception>
    private void HandleDataResponse(DataResponse dataResponse)
    {
        string bodyString = dataResponse.Body;
        MemoryStream decodedStringStream = dataResponse.Encoding switch
        {
            EncodingType.Base64
                => ConvertBase64Response(bodyString),
            EncodingType.Utf8 or null
                => ConvertUtf8Response(bodyString),
            _
                => throw new UnreachableException(),
        };

        string finalString;
        switch (dataResponse.Compression)
        {
            case CompressionType.Gzip:
                finalString = UncompressGzip(decodedStringStream);
                break;
            case CompressionType.Zip:
                _logger.UnsupportedFormat();
                throw new WebSocketClientUnsupportedException();
            case null:
                finalString = bodyString;
                break;
            default:
                throw new UnreachableException();
        }

        _logger.DecodedAndDecompressedString(finalString);

        switch (dataResponse.Format)
        {
            case FormatType.Json:
                HandleJsonData(finalString);
                break;
            case FormatType.Xml:
            case FormatType.AlphaNumeric:
            case FormatType.Binary:
            case null:
                _logger.UnsupportedFormat();
                throw new WebSocketClientUnsupportedException();
            default:
                throw new UnreachableException();
        }
    }

    /// <summary>
    /// Handles the JSON data received from the WebSocket.
    /// </summary>
    /// <param name="finalString">The string of the JSON to be handled.</param>
    private void HandleJsonData(string finalString)
    {
        try
        {
            Head headData = JsonSerializer.Deserialize<Head>(finalString)
                ?? throw new WebSocketClientFormatException();

            if (headData.Schema.Type == "eew-information" && headData.Schema.Version == "1.0.0")
            {
                EewInformationSchema eew = JsonSerializer.Deserialize<EewInformationSchema>(finalString, _options)
                    ?? throw new WebSocketClientFormatException();
                DataReceived?.Invoke(this, new() { Telegram = eew });
                return;
            }

            if (headData.Schema.Type == "tsunami-information" && headData.Schema.Version == "1.0.0")
            {
                TsunamiInformationSchema tsunami = JsonSerializer.Deserialize<TsunamiInformationSchema>(finalString, _options)
                    ?? throw new WebSocketClientFormatException();
                DataReceived?.Invoke(this, new() { Telegram = tsunami });
                return;
            }
        }
        catch (JsonException)
        {
            _logger.IncorrectFormat(finalString);
        }
        catch (WebSocketClientFormatException)
        {
            _logger.IncorrectFormat(finalString);
        }
    }

    /// <summary>
    /// Converts the given string to a <see cref="MemoryStream"/> using UTF8.
    /// </summary>
    /// <param name="bodyString">The string to be converted.</param>
    /// <returns>The converted <see cref="MemoryStream"/>.</returns>
    private static MemoryStream ConvertUtf8Response(string bodyString)
        => new(Encoding.UTF8.GetBytes(bodyString));

    /// <summary>
    /// Converts the given string to a <see cref="MemoryStream"/> using Base64.
    /// </summary>
    /// <param name="bodyString">The string to be converted.</param>
    /// <returns>The converted <see cref="MemoryStream"/>.</returns>
    private static MemoryStream ConvertBase64Response(string bodyString)
        => new(Convert.FromBase64String(bodyString));

    /// <summary>
    /// Decompresses the given <see cref="MemoryStream"/> using GZip.
    /// </summary>
    /// <param name="stream">The given <see cref="MemoryStream"/>.</param>
    /// <returns>A string representing the decoded data.</returns>
    private static string UncompressGzip(MemoryStream stream)
    {
        using GZipStream gZipStream = new(stream, CompressionMode.Decompress);
        using MemoryStream outputStream = new();
        gZipStream.CopyTo(outputStream);
        return Encoding.UTF8.GetString(outputStream.ToArray());
    }

    /// <summary>
    /// Creates a new instance of the class with the given parameters.
    /// </summary>>
    /// <param name="logger">The logger to be used.</param>
    /// <param name="jsonSerializerOptions">The JSON Serialisation options to be used.</param>
    public WebSocketClient(ILogger<WebSocketClient> logger, JsonSerializerOptions jsonSerializerOptions)
    {
        _client = new();
        _pingStrings = [];
        _tokenSource = new();
        _token = _tokenSource.Token;
        _options = jsonSerializerOptions;
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
        StatusChanged?.Invoke(this, EventArgs.Empty);
        _logger.Connected(webSocketUrl);
    }
    /// <inheritdoc/>
    public async Task DisconnectAsync()
    {
        if (_client.State is WebSocketState.Closed)
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
        StatusChanged?.Invoke(this, EventArgs.Empty);
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