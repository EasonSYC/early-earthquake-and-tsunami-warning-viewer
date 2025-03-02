using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dtos.Enum;
/// <summary>
/// Describes the LPGM intensity.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<LgIntensity>))]
public enum LgIntensity
{
    /// <summary>
    /// The value <c>0</c>, representing LPGM intensity 0.
    /// </summary>
    [JsonStringEnumMemberName("0")]
    Zero,
    /// <summary>
    /// The value <c>1</c>, representing LPGM intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One,
    /// <summary>
    /// The value <c>2</c>, representing LPGM intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two,
    /// <summary>
    /// The value <c>3</c>, representing LPGM intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three,
    /// <summary>
    /// The value <c>4</c>, representing LPGM intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four,
}