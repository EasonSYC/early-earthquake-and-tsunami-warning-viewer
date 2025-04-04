﻿using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dtos.Enum;
using EasonEetwViewer.Dmdata.Dtos.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dtos.Telegram;
using EasonEetwViewer.Dmdata.WebSocket.Dtos.Data;

namespace EasonEetwViewer.Dmdata.WebSocket.Dtos.Response;
/// <summary>
/// Represents a data response.
/// </summary>
internal record DataResponse : ResponseBase
{
    /// <summary>
    /// The property <c>type</c>, a constant <see cref="MessageType.Data"/>.
    /// </summary>
    [JsonPropertyName("type")]
    public override MessageType Type { get; init; } = MessageType.Data;
    /// <summary>
    /// The property <c>version</c>, representing the version of the data.
    /// </summary>
    [JsonPropertyName("version")]
    public required string Version { get; init; }
    /// <summary>
    /// The property <c>classification</c>, representing the classification of the data.
    /// </summary>
    [JsonPropertyName("classification")]
    public required Classification Classification { get; init; }
    /// <summary>
    /// The property <c>id</c>, representing the ID of the data.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; init; }
    /// <summary>
    /// The property <c>passing</c>, representing the passing route of the data.
    /// </summary>
    [JsonPropertyName("passing")]
    public required IEnumerable<PassingDetail> PassingRoute { get; init; }
    /// <summary>
    /// The property <c>head</c>, representing the head information of the telegram.
    /// </summary>
    [JsonPropertyName("head")]
    public required HeadInfo DataInfo { get; init; }
    /// <summary>
    /// The property <c>xmlReport</c>, representing the head and control information of the data.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when not an XML report or a JSON converted format.
    /// </remarks>
    [JsonPropertyName("xmlReport")]
    public XmlReport? XmlInfo { get; init; }
    /// <summary>
    /// The property <c>format</c>, representing the format of the data.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when unknown.
    /// </remarks>
    [JsonPropertyName("format")]
    public required FormatType? Format { get; init; }
    /// <summary>
    /// The property <c>compression</c>, representing the compression format of the data.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when data is uncompressed.
    /// </remarks>
    [JsonPropertyName("compression")]
    public required CompressionType? Compression { get; init; }
    /// <summary>
    /// The property <c>encoding</c>, representing the encoding format of the data.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> when not encoded.
    /// </remarks>
    [JsonPropertyName("encoding")]
    public required EncodingType? Encoding { get; init; }
    /// <summary>
    /// The property <c>body</c>, representing the body of the data.
    /// </summary>
    [JsonPropertyName("body")]
    public required string Body { get; init; }
}