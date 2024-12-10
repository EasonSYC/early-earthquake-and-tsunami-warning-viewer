using System.Text.Json.Serialization;

namespace EasonEetwViewer.HttpRequest.Dto.Enum;
/// <summary>
/// Describes the LPGM intensity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<LgIntensity>))]
public enum LgIntensity
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>0</c>, representing LPGM intensity 0.
    /// </summary>
    [JsonStringEnumMemberName("0")]
    Zero = 1,
    /// <summary>
    /// The value <c>1</c>, representing LPGM intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One = 2,
    /// <summary>
    /// The value <c>2</c>, representing LPGM intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two = 3,
    /// <summary>
    /// The value <c>3</c>, representing LPGM intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three = 4,
    /// <summary>
    /// The value <c>4</c>, representing LPGM intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four = 5,
}