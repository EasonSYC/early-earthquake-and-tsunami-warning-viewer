using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EewInformation.Enum.Range;
public enum LgIntensityUpper
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
    [JsonStringEnumMemberName("不明")]
    Unclear = 6,
    [JsonStringEnumMemberName("over")]
    Above = 7
}
