using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

/// <summary>
/// 
/// </summary>
public record WebSocketConnectionDetails : WebSocketBasics
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }
}
