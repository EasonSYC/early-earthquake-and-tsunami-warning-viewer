using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Dtos.Enum.WebSocket;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;
/// <summary>
/// Represents a start response.
/// </summary>
internal record StartResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Start"/>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Start;
    /// <summary>
    /// The property <c>time</c>, representing the time the WebSocket was started.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
    /// <summary>
    /// The property <c>socketId</c>, representing the ID of the socket.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("socketId")]
    public required int SocketId { get; init; }
    /// <summary>
    /// The property <c>classifications</c>. The classifications of telegrams that the WebSocket receives.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("classifications")]
    public required IEnumerable<Classification> Classifications { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the program receives.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when receiving all types from the classifications.
    /// </remarks>
    [JsonInclude]
    [JsonPropertyName("types")]
    public required IEnumerable<string>? Types { get; init; }
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("test")]
    public required TestStatus TestStatus { get; init; }
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when not indicated.
    /// </remarks>
    [JsonInclude]
    [JsonPropertyName("appName")]
    public required string? ApplicationName { get; init; }
    /// <summary>
    /// The property <c>formats</c>. A list of formats of telegrams the WebSocket receives.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("formats")]
    public required IEnumerable<FormatType> Formats { get; init; }
}
