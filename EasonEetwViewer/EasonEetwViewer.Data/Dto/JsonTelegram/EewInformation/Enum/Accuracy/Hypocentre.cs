using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Accuracy;

[JsonConverter(typeof(JsonStringEnumConverter<Hypocentre>))]
public enum Hypocentre
{
    Unknown = 0,
    [JsonStringEnumMemberName("内陸")]
    Land = 1,
    [JsonStringEnumMemberName("海域")]
    Sea = 2
}
