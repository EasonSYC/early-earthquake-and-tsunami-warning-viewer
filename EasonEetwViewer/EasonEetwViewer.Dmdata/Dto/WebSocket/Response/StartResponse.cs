using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;
internal record StartResponse : ResponseBase
{
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Start;

    [JsonInclude]
    [JsonPropertyName("time")]
    internal required DateTimeOffset Time { get; init; }

    [JsonInclude]
    [JsonPropertyName("socketId")]
    internal required int SocketId { get; init; }

    [JsonInclude]
    [JsonPropertyName("classifications")]
    internal required IEnumerable<Classification> Classifications { get; init; }

    [JsonInclude]
    [JsonPropertyName("types")]
    internal required IEnumerable<string>? Types { get; init; }

    [JsonInclude]
    [JsonPropertyName("test")]
    internal required TestStatus TestStatus { get; init; }

    [JsonInclude]
    [JsonPropertyName("appName")]
    internal required string? AppName { get; init; }
    [JsonInclude]
    [JsonPropertyName("formats")]
    internal required IEnumerable<FormatType> Formats { get; init; }
}
