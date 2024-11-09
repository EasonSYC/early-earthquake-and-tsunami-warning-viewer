using System.Text.Json.Serialization;

namespace EasonEetwViewer.Dto;

/// <summary>
/// Represents the basic properties of a WebSocket connection.
/// Abstract and cannot be instantiated.
/// </summary>
public abstract record WebSocketBasics
{
    /// <summary>
    /// The property <c>ticket</c>. The ticket for the WebSocket connection.
    /// </summary>
    [JsonPropertyName("ticket")]
    public string Ticket { get; init; } = string.Empty;
    /// <summary>
    /// The property <c>classifications</c>. The classifications of telegrams that the WebSocket receives.
    /// </summary>
    [JsonPropertyName("classifications")]
    public List<ResponseEnums.Classification> Classifications { get; init; } = [];
    /// <summary>
    /// The property <c>test</c>. Whether the WebSocket receives test telegrams.
    /// </summary>
    [JsonPropertyName("test")]
    public ResponseEnums.TestStatus Test { get; init; }
    /// <summary>
    /// The property <c>types</c>. The types of telegrams the program receives.
    /// <c>null</c> when receiving all types from the classifications.
    /// </summary>
    [JsonPropertyName("types")]
    public List<ResponseEnums.TelegramType>? Types { get; init; }
    /// <summary>
    /// The propecty <c>formats</c>. A list of formats of telegrams the WebSocket receives.
    /// </summary>
    [JsonPropertyName("formats")]
    public List<ResponseEnums.FormatType> Formats { get; init; } = [];
    /// <summary>
    /// The property <c>appName</c>. The application name of the WebSocket connection.
    /// <c>null</c> when not indicated.
    /// </summary>
    [JsonPropertyName("appName")]
    public string? ApplicationName { get; init; }
}