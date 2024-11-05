using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record ErrorResponse : ResponseModels.ApiResponse
{
    [JsonPropertyName("status")]
    public ResponseEnums.Status Status { get; init; } = ResponseEnums.Status.Error;
    [JsonPropertyName("error")]
    public HttpError Error { get; init; } = new();
}