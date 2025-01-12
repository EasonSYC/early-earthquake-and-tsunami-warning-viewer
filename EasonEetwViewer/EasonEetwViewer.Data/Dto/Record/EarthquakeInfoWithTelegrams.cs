using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.Record;
public record EarthquakeInfoWithTelegrams : EarthquakeInfo
{
    [JsonPropertyName("telegrams")]
    public required List<EarthquakeTelegram> Telegrams { get; init; }
}
