using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;

/// <summary>
/// The status of a station.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<StationStatus>))]
public enum StationStatus
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>新規</c>, representing a new station.
    /// </summary>
    [JsonStringEnumMemberName("新規")]
    New = 1,
    /// <summary>
    /// The value <c>変更</c>, representing a change in detail of the station.
    /// </summary>
    [JsonStringEnumMemberName("変更")]
    Changed = 2,
    /// <summary>
    /// The value <c>現</c>, representing the same station details.
    /// </summary>
    [JsonStringEnumMemberName("現")]
    InUse = 3,
    /// <summary>
    /// The value <c>廃止</c>, representing a discontinued station.
    /// </summary>
    [JsonStringEnumMemberName("廃止")]
    Discontinued = 4
}
