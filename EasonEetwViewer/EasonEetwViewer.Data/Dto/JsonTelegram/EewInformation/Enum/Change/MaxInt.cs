using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Change;
[JsonConverter(typeof(JsonStringEnumConverter<MaxInt>))]
public enum MaxInt
{
    Unknown = 0,
    [JsonStringEnumMemberName("0")]
    NoChange = 1,
    [JsonStringEnumMemberName("1")]
    Increase = 2,
    [JsonStringEnumMemberName("2")]
    Decrease = 3
}
