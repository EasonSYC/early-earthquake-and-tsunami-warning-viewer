using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record Contract
{
    [JsonPropertyName("id")]
    public int? Id { get; init; } = 0;
    [JsonPropertyName("planId")]
    public int PlanId { get; init; } = 0;
    [JsonPropertyName("planName")]
    public string PlanName { get; init; } = string.Empty;
    [JsonPropertyName("classification")]
    public string Classification { get; init; } = string.Empty;
    [JsonPropertyName("price")]
    public ContractPrice Price { get; init; } = new();
    [JsonPropertyName("start")]
    public DateTime? Start { get; init; } = new();
    [JsonPropertyName("isValid")]
    public bool IsValid { get; init; } = false;
    [JsonPropertyName("connectionCounts")]
    public int ConnectionCounts { get; init; } = 0;
}