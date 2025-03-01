using System.Text.Json.Serialization;

namespace EasonEetwViewer.Api.Dtos.Record.WebSocket;

/// <summary>
/// Represents the URL of a WebSocket.
/// </summary>
public record WebSocketUrl
{
    /// <summary>
    /// The property <c>id</c>. The ID of the WebSocket.
    /// </summary>
    [JsonPropertyName("id")]
    public required int WebSocketId { get; init; }
    /// <summary>
    /// The property <c>url</c>. The url to the WebSocket connection, with the ticket.
    /// </summary>
    [JsonPropertyName("url")]
    public required Uri Url { get; init; }
    /// <summary>
    /// The property <c>protocol</c>. The protocol used by the WebSocket, a constant with value <c>dmdata.v2</c>.
    /// </summary>
    [JsonPropertyName("protocol")]
    public string Protocol { get; } = "dmdata.v2";
    /// <summary>
    /// The property <c>expiration</c>. The expiration of the key in seconds, a constant <c>300</c>.
    /// </summary>
    [JsonPropertyName("expiration")]
    public int Expiration { get; } = 300;
}
