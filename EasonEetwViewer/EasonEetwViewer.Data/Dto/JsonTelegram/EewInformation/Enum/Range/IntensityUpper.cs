using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EasonEetwViewer.HttpRequest.Dto.JsonTelegram.EewInformation.Enum.Range;
public enum IntensityUpper
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>1</c>, representing intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("0")]
    Zero = 1,
    [JsonStringEnumMemberName("1")]
    One = 2,
    /// <summary>
    /// The value <c>2</c>, representing intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two = 3,
    /// <summary>
    /// The value <c>3</c>, representing intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three = 4,
    /// <summary>
    /// The value <c>4</c>, representing intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four = 5,
    /// <summary>
    /// The value <c>5-</c>, representing intensity 5 weak.
    /// </summary>
    [JsonStringEnumMemberName("5-")]
    FiveWeak = 6,
    /// <summary>
    /// The value <c>5+</c>, representing intensity 5 strong.
    /// </summary>
    [JsonStringEnumMemberName("5+")]
    FiveStrong = 7,
    /// <summary>
    /// The value <c>6-</c>, representing intensity 6 weak.
    /// </summary>
    [JsonStringEnumMemberName("6-")]
    SixWeak = 8,
    /// <summary>
    /// The value <c>6+</c>, representing intensity 6 strong.
    /// </summary>
    [JsonStringEnumMemberName("6+")]
    SixStrong = 9,
    /// <summary>
    /// The value <c>7</c>, representing intensity 7.
    /// </summary>
    [JsonStringEnumMemberName("7")]
    Seven = 10,
    [JsonStringEnumMemberName("不明")]
    Unclear = 11,
    [JsonStringEnumMemberName("over")]
    Above = 12
}
