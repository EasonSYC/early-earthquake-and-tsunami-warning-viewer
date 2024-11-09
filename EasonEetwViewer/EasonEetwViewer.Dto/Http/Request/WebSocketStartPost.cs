using System.Text.Json.Serialization;
using EasonEetwViewer.Dto.Http.Enum;
using EasonEetwViewer.Dto.Http.Request.Enum;

namespace EasonEetwViewer.Dto.Http.Request;
public record WebSocketStartPost
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("classifications")]
    public required List<Classification> Classifications { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("types")]
    public List<string>? Types { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("test")]
    public TestStatus? TestStatus { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("appName")]
    public string? AppName { get; init; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("formatMode")]
    public FormatMode? FormatMode { get; init; }
}
