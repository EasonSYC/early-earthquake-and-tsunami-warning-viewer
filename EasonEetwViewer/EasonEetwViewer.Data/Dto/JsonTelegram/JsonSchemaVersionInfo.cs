using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;

public record JsonSchemaVersionInfo
{
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    [JsonPropertyName("version")]
    public required string Version { get; init; }
}
