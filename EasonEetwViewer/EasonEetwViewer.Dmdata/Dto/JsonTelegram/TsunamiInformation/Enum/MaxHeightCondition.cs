using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<MaxHeightCondition>))]
public enum MaxHeightCondition
{
    Unknown = 0,
    [JsonStringEnumMemberName("重要")]
    Important = 1
}
