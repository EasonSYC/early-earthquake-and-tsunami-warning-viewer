using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;
using EasonEetwViewer.WebSocket.Dtos.Data;

namespace EasonEetwViewer.WebSocket.Dtos.Response;
/// <summary>
/// Represents a data response.
/// </summary>
internal record DataResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Data"/>.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Data;
    /// <summary>
    /// The property <c>version</c>, representing the version of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("version")]
    public required string Version { get; init; }
    /// <summary>
    /// The property <c>classification</c>, representing the classification of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("classification")]
    public required Classification Classification { get; init; }
    /// <summary>
    /// The property <c>id</c>, representing the ID of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    /// <summary>
    /// The property <c>passing</c>, representing the passing route of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("passing")]
    public required IEnumerable<PassingDetail> PassingRoute { get; init; }
    /// <summary>
    /// The property <c>head</c>, representing the head information of the telegram.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("head")]
    public required HeadInfo DataInfo { get; init; }
    /// <summary>
    /// The property <c>xmlReport</c>, representing the head and control information of the data.
    /// <see langword="null"/> when not an XML report or a JSON converted format.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("xmlReport")]
    public XmlReport? XmlInfo { get; init; }
    /// <summary>
    /// The property <c>format</c>, representing the format of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("format")]
    public required FormatType? Format { get; init; }
    /// <summary>
    /// The property <c>compression</c>, representing the compression format of the data.
    /// <see langword="null"/> when data is uncompressed.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("compression")]
    public required CompressionType? Compression { get; init; }
    /// <summary>
    /// The property <c>encoding</c>, representing the encoding format of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("encoding")]
    public required EncodingType? Encoding { get; init; }
    /// <summary>
    /// The property <c>body</c>, representing the body of the data.
    /// </summary>
    [JsonInclude]
    [JsonPropertyName("body")]
    public required string Body { get; init; }
}