using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram;
public record EarthquakeInformationSchema : JsonSchemaHead
{
    [JsonPropertyName("body")]
    public required EarthquakeInformationBody Body { get; init; }
}