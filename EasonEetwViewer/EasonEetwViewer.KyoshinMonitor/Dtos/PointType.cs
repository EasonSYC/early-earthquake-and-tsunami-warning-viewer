using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dtos;
/// <summary>
/// Represents the type of an observation point.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<PointType>))]
public enum PointType
{
    /// <summary>
    /// KiK-net. The value <c>KiK_net</c>.
    /// </summary>
    [JsonStringEnumMemberName("KiK_net")]
    KiK,
    /// <summary>
    /// K-NET. The value <c>K_NET</c>.
    /// </summary>
    [JsonStringEnumMemberName("K_NET")]
    K
}
