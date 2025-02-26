using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Range;
[JsonConverter(typeof(JsonStringEnumConverter<LgIntensityUpper>))]
public enum LgIntensityUpper
{
    /// <summary>
    /// The value <c>0</c>, representing LPGM intensity 0.
    /// </summary>
    [JsonStringEnumMemberName("0")]
    Zero = 0,
    /// <summary>
    /// The value <c>1</c>, representing LPGM intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One = 1,
    /// <summary>
    /// The value <c>2</c>, representing LPGM intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two = 2,
    /// <summary>
    /// The value <c>3</c>, representing LPGM intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three = 3,
    /// <summary>
    /// The value <c>4</c>, representing LPGM intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four = 4,
    [JsonStringEnumMemberName("不明")]
    Unclear = 5,
    [JsonStringEnumMemberName("over")]
    Above = 6
}
