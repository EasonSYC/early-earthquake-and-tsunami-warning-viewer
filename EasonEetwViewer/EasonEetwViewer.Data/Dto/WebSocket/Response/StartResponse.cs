using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.HttpRequest.Dto.ApiResponse.Enum;
using EasonEetwViewer.WebSocket.Dto;

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
    internal required List<Classification> Classifications { get; init; }

    [JsonInclude]
    [JsonPropertyName("types")]
    internal required List<string>? Types { get; init; }

    [JsonInclude]
    [JsonPropertyName("test")]
    internal required TestStatus TestStatus { get; init; }

    [JsonInclude]
    [JsonPropertyName("appName")]
    internal required string? AppName { get; init; }
    [JsonInclude]
    [JsonPropertyName("formats")]
    internal required List<FormatType> Formats { get; init; }
}
