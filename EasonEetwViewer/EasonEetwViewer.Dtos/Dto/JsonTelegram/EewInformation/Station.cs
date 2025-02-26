﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Dto.JsonTelegram.EewInformation;
public record Station
{
    [JsonPropertyName("code")]
    public required string Code { get; init; }
    [JsonPropertyName("name")]
    public required string Name { get; init; }
    [JsonPropertyName("int")]
    public required string Int { get; init; }
    [JsonPropertyName("k")]
    public required float K { get; init; }
}
