using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos;
/// <summary>
/// The head information of the telegram.
/// </summary>
internal record HeadInfo
{
    /// <summary>
    /// The property <c>type</c>, the type code of the telegram.
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; init; }
    /// <summary>
    /// The property <c>author</c>, the author of the telegram.
    /// </summary>
    [JsonPropertyName("author")]
    public required string Author { get; init; }
    /// <summary>
    /// The property <c>target</c>, the target code of the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when no such target.
    /// </remarks>
    [JsonPropertyName("target")]
    public string? Target { get; init; }
    /// <summary>
    /// The property <c>time</c>, the time base of the telegram.
    /// </summary>
    [JsonPropertyName("time")]
    public required DateTimeOffset Time { get; init; }
    /// <summary>
    /// The property <c>designation</c>, the designation of the telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when normal.
    /// </remarks>
    [JsonPropertyName("designation")]
    public required string? Designation { get; init; }
    /// <summary>
    /// The property <c>test</c>, whether the telegram is a test telegram.
    /// </summary>
    [JsonPropertyName("test")]
    public required bool IsTest { get; init; }
    /// <summary>
    /// The property <c>xml</c>, whether the telegram is an XML telegram.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unknown.
    /// </remarks>
    [JsonPropertyName("xml")]
    public bool? IsXml { get; init; }
}
