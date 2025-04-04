﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Telegram.Dtos.EewInformation.Enum.Range;
[JsonConverter(typeof(JsonStringEnumConverter<IntensityLower>))]
public enum IntensityLower
{
    [JsonStringEnumMemberName("0")]
    Zero,
    /// <summary>
    /// The value <c>1</c>, representing intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One,
    /// <summary>
    /// The value <c>2</c>, representing intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two,
    /// <summary>
    /// The value <c>3</c>, representing intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three,
    /// <summary>
    /// The value <c>4</c>, representing intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four,
    /// <summary>
    /// The value <c>5-</c>, representing intensity 5 weak.
    /// </summary>
    [JsonStringEnumMemberName("5-")]
    FiveWeak,
    /// <summary>
    /// The value <c>5+</c>, representing intensity 5 strong.
    /// </summary>
    [JsonStringEnumMemberName("5+")]
    FiveStrong,
    /// <summary>
    /// The value <c>6-</c>, representing intensity 6 weak.
    /// </summary>
    [JsonStringEnumMemberName("6-")]
    SixWeak,
    /// <summary>
    /// The value <c>6+</c>, representing intensity 6 strong.
    /// </summary>
    [JsonStringEnumMemberName("6+")]
    SixStrong,
    /// <summary>
    /// The value <c>7</c>, representing intensity 7.
    /// </summary>
    [JsonStringEnumMemberName("7")]
    Seven,
    [JsonStringEnumMemberName("不明")]
    Unclear
}
