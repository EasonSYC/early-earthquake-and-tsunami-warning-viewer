﻿using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.Dto.JsonTelegram.EarthquakeInformation.Enum;

[JsonConverter(typeof(JsonStringEnumConverter<IntensityWithUnreceived>))]
public enum IntensityWithUnreceived
{
    /// <summary>
    /// Unknown. Default value.
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// The value <c>1</c>, representing intensity 1.
    /// </summary>
    [JsonStringEnumMemberName("1")]
    One = 1,
    /// <summary>
    /// The value <c>2</c>, representing intensity 2.
    /// </summary>
    [JsonStringEnumMemberName("2")]
    Two = 2,
    /// <summary>
    /// The value <c>3</c>, representing intensity 3.
    /// </summary>
    [JsonStringEnumMemberName("3")]
    Three = 3,
    /// <summary>
    /// The value <c>4</c>, representing intensity 4.
    /// </summary>
    [JsonStringEnumMemberName("4")]
    Four = 4,
    /// <summary>
    /// The value <c>5-</c>, representing intensity 5 weak.
    /// </summary>
    [JsonStringEnumMemberName("5-")]
    FiveWeak = 5,
    /// <summary>
    /// The value <c>5+</c>, representing intensity 5 strong.
    /// </summary>
    [JsonStringEnumMemberName("5+")]
    FiveStrong = 6,
    /// <summary>
    /// The value <c>6-</c>, representing intensity 6 weak.
    /// </summary>
    [JsonStringEnumMemberName("6-")]
    SixWeak = 7,
    /// <summary>
    /// The value <c>6+</c>, representing intensity 6 strong.
    /// </summary>
    [JsonStringEnumMemberName("6+")]
    SixStrong = 8,
    /// <summary>
    /// The value <c>7</c>, representing intensity 7.
    /// </summary>
    [JsonStringEnumMemberName("7")]
    Seven = 9,
    [JsonStringEnumMemberName("!5-")]
    Unreceived = 10,

}
