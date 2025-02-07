using System.Text.Json.Serialization;

namespace EasonEetwViewer.KyoshinMonitor.Dtos;
/// <summary>
/// Represents the type of an observation point.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<PointType>))]
public enum PointType
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// KiK-net. The value <c>KiK_net</c>.
    /// </summary>
    [JsonStringEnumMemberName("KiK_net")]
    KiK = 1,
    /// <summary>
    /// K-NET. The value <c>K_NET</c>.
    /// </summary>
    [JsonStringEnumMemberName("K_NET")]
    K = 2
}
