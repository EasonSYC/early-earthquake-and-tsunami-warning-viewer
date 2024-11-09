using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EasonEetwViewer.Dto.Http.Response.Enum;

namespace EasonEetwViewer.Dto.Http.Response.Record;

/// <summary>
/// 
/// </summary>
public record EarthquakeStation
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("region")]
    public Position Region { get; init; } = new();
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("city")]
    public Position City { get; init; } = new();
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("noCode")]
    public string NoCode { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("code")]
    public string XmlCode { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    public string KanjiName { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("kana")]
    public string KanaName { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public StationStatus StationStatus { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("owner")]
    public string Owner { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("latitude")]
    public string Latitude { get; init; } = string.Empty;
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("longitude")]
    public string Longitude { get; init; } = string.Empty;
}
