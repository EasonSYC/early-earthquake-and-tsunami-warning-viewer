using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

public abstract record PoolingResponse<T> : ListResponse<T>
{
    [JsonPropertyName("nextToken")]
    public string? NextToken { get; init; }
    [JsonPropertyName("nextPooling")]
    public string NextPooling { get; init; } = string.Empty;
    [JsonPropertyName("nextPoolingInterval")]
    public int NextPoolingInterval { get; init; }
}