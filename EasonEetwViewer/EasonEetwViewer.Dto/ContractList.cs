using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

public record ContractList : ApiResponse
{
    [JsonPropertyName("items")]
    public List<Contract> Contracts { get; init; } = [];
}
