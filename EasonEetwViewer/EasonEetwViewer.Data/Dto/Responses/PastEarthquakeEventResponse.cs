using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.HttpRequest.Dto.Model;
using EasonEetwViewer.HttpRequest.Dto.Record;

namespace EasonEetwViewer.HttpRequest.Dto.Responses;
public record PastEarthquakeEventResponse : SuccessResponse
{
    [JsonPropertyName("event")]
    public required EarthquakeInfoWithTelegrams EarthquakeEvent { get; init; }
}
