using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.TsunamiInformation.Enum;
[JsonConverter(typeof(JsonStringEnumConverter<Revise>))]
public enum Revise
{
    Unknown = 0,
    [JsonStringEnumMemberName("追加")]
    Add = 1,
    [JsonStringEnumMemberName("更新")]
    Renew = 2
}
