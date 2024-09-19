using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record ContractPrice
{
    [JsonPropertyName("day")]
    public int Day { get; init; } = 0;
    [JsonPropertyName("month")]
    public int Month { get; init; } = 0;
}
