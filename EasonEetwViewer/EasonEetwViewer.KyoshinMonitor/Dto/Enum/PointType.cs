using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dto.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<PointType>))]
public enum PointType
{
    Unknown = 0,
    [JsonStringEnumMemberName("KiK_net")]
    KiK = 1,
    [JsonStringEnumMemberName("K_NET")]
    K = 2
}
