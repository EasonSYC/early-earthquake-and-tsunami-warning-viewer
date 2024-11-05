using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

public abstract record ListResponse<T> : SuccessResponse
{
    [JsonPropertyName("items")]
    public List<T> Items { get; init; } = [];
}
