using System.Text.Json.Serialization;

namespace EasonEetwViewer.Authentication.OAuth2.Dto;

internal record Error
{
    [JsonInclude]
    [JsonPropertyName("error")]
    internal required string Short { get; init; }
    [JsonInclude]
    [JsonPropertyName("description")]
    internal required string Description { get; init; }
}