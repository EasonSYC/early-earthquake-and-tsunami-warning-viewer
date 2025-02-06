using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<HeightCondition>))]
public enum HeightCondition
{
    Unknown = 0,
    [JsonStringEnumMemberName("高い")]
    High = 1,
    [JsonStringEnumMemberName("巨大")]
    Huge = 2
}
