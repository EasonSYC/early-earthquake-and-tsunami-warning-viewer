using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

public abstract record SuccessResponse : ApiResponse
{
    [JsonPropertyName("status")]
    public ResponseEnums.Status Status { get; init; } = ResponseEnums.Status.Ok;
}