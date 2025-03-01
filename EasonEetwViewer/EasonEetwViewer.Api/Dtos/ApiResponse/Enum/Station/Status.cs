using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dtos.Enum.Station;

/// <summary>
/// The status of a station.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Status>))]
public enum Status
{
    /// <summary>
    /// The value <c>新規</c>, representing a new station.
    /// </summary>
    [JsonStringEnumMemberName("新規")]
    New,
    /// <summary>
    /// The value <c>変更</c>, representing a change in detail of the station.
    /// </summary>
    [JsonStringEnumMemberName("変更")]
    Changed,
    /// <summary>
    /// The value <c>現</c>, representing the same station details.
    /// </summary>
    [JsonStringEnumMemberName("現")]
    InUse,
    /// <summary>
    /// The value <c>廃止</c>, representing a discontinued station.
    /// </summary>
    [JsonStringEnumMemberName("廃止")]
    Discontinued
}
