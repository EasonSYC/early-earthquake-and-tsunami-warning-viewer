using System.Text.Json.Serialization;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Enum.WebSocket;
using EasonEetwViewer.Dmdata.Dto.ApiResponse.Record.GdEarthquake;

namespace EasonEetwViewer.Dmdata.Dto.WebSocket.Response;
internal record DataResponse : ResponseBase
{
    [JsonInclude]
    [JsonPropertyName("type")]
    internal override ResponseType Type { get; init; } = ResponseType.Data;

    [JsonInclude]
    [JsonPropertyName("version")]
    internal required string Version { get; init; }

    [JsonInclude]
    [JsonPropertyName("classification")]
    internal required Classification Classification { get; init; }

    [JsonInclude]
    [JsonPropertyName("id")]
    internal required string Id { get; init; }

    [JsonInclude]
    [JsonPropertyName("passing")]
    internal required List<PassingDetail> PassingRoute { get; init; }

    [JsonInclude]
    [JsonPropertyName("head")]
    internal required HeadInfo DataInfo { get; init; }

    [JsonInclude]
    [JsonPropertyName("xmlReport")]
    internal XmlReport? XmlInfo { get; init; }

    [JsonInclude]
    [JsonPropertyName("format")]
    internal required FormatType? Format { get; init; }

    [JsonInclude]
    [JsonPropertyName("compression")]
    internal required CompressionType? Compression { get; init; }

    [JsonInclude]
    [JsonPropertyName("encoding")]
    internal required EncodingType? Encoding { get; init; }

    [JsonInclude]
    [JsonPropertyName("body")]
    internal required string Body { get; init; }
}