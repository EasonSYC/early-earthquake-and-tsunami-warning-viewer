using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto.ResponseModels;

public abstract record TokenResponse<T> : ListResponse<T>
{
    [JsonPropertyName("nextToken")]
    public string NextToken { get; init; } = string.Empty;
}